using Microsoft.Data.Sqlite;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SorexUI.model;

internal class SQLiteDatabase {
    private SqliteConnection db = new SqliteConnection();

    void openDb(string path) {
        closeDb();
        db = new SqliteConnection($"Filename={path}");
        //db.ForeignKeys = true; // TODO
    }

    void closeDb() {
        db.Close();
    }

    bool isConnected() {
        return db.State == System.Data.ConnectionState.Open;
    }

    void createDb(string path) {
        openDb(path);
        // TODO: transaction
        using var tx = db.BeginTransaction();
        var sql = """
                    CREATE TABLE note (
                      note_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                      author VARCHAR(64) NOT NULL DEFAULT '',
                      client VARCHAR(255) NOT NULL DEFAULT '',
                      user_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      colour INTEGER NOT NULL DEFAULT 16777215,
                      rank TINYINT NOT NULL DEFAULT 0,
                      is_visible BOOLEAN NOT NULL DEFAULT true,
                      is_favourite BOOLEAN NOT NULL DEFAULT false,
                      is_deleted BOOLEAN NOT NULL DEFAULT false,
                      created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                    );
                    CREATE VIRTUAL TABLE notedata USING FTS5(data);
                    CREATE TABLE tag (
                      tag_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                      name VARCHAR(64) UNIQUE NOT NULL,
                      created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                    );
                    CREATE TABLE image (
                      guid UUID PRIMARY KEY NOT NULL,
                      data BLOB NOT NULL,
                      created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
                    );
                    CREATE TABLE note_to_tag (
                      note_id INTEGER NOT NULL REFERENCES note (note_id) ON UPDATE RESTRICT ON DELETE CASCADE,
                      tag_id  INTEGER NOT NULL REFERENCES tag (tag_id) ON UPDATE RESTRICT ON DELETE CASCADE,
                      created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                      PRIMARY KEY (note_id, tag_id)
                    );
                    """;
        //db.UserVersion = 3; // TODO
        new SqliteCommand(sql, db, tx).ExecuteNonQuery();

        tx.Commit(); // TODO check if necessary
    }

    Int64 insertNote(string data) {
        using var tx = db.BeginTransaction();
        var opt = new SqliteCommand("INSERT INTO note DEFAULT VALUES;", db, tx).ExecuteScalar() as Int64?;
        var noteId = opt ?? -1;
        var cmd = new SqliteCommand("INSERT INTO notedata (rowid, data) VALUES (?, ?);", db, tx);
        cmd.Parameters.AddRange(new[] { new SqliteParameter().Value = noteId, new SqliteParameter().Value = data });
        cmd.ExecuteScalar();

        tx.Commit();
        return noteId;
    }

    void updateNote(Int64 noteId, string data) {
        using var tx = db.BeginTransaction();
        var cmd1 = new SqliteCommand("UPDATE notedata SET data = ? WHERE rowid = ?;", db, tx);
        cmd1.Parameters.AddRange(new[] { new SqliteParameter().Value = data, new SqliteParameter().Value = noteId });
        var cmd2 = new SqliteCommand("UPDATE note SET updated_at = CURRENT_TIMESTAMP WHERE note_id = ?;", db, tx);
        cmd2.Parameters.AddRange(new[] { new SqliteParameter().Value = noteId });

        new List<SqliteCommand>([cmd1, cmd2]).ForEach(cmd => cmd.ExecuteNonQuery());

        tx.Commit();
    }

    void softDeleteNote(Int64 noteId, bool deleted) {
        var cmd = new SqliteCommand("UPDATE note SET is_deleted = ?, updated_at = CURRENT_TIMESTAMP WHERE note_id = ?;", db);
        cmd.Parameters.AddRange(new[] { new SqliteParameter().Value = deleted, new SqliteParameter().Value = noteId });
        cmd.ExecuteNonQuery();
    }

    void deleteNote(Int64 noteId) {
        using var tx = db.BeginTransaction();
        var cmd1 = new SqliteCommand("DELETE FROM note     WHERE note_id = ?;", db, tx);
        cmd1.Parameters.AddRange(new[] { new SqliteParameter().Value = noteId });
        var cmd2 = new SqliteCommand("DELETE FROM notedata WHERE rowid = ?;", db, tx);
        cmd2.Parameters.AddRange(new[] { new SqliteParameter().Value = noteId });
        var cmd3 = new SqliteCommand("DELETE FROM tag      WHERE tag_id NOT IN (SELECT DISTINCT tag_id FROM note_to_tag);", db, tx);

        new List<SqliteCommand>([cmd1, cmd2, cmd3]).ForEach(cmd => cmd.ExecuteNonQuery());

        tx.Commit();
    }

    List<Note> getNotes(bool fetchDeleted) {
        var result = new List<Note>();
        
        var sql = """
          SELECT note_id, data, GROUP_CONCAT(name, ', ') AS tags, is_deleted
          FROM note
          INNER JOIN notedata ON note_id = notedata.rowid
          INNER JOIN note_to_tag USING (note_id)
          INNER JOIN tag         USING (tag_id)
        """
        + (fetchDeleted ? "" : "WHERE NOT is_deleted ") +
        """
          GROUP BY note_id
          ORDER BY note_id ASC
          ;
        """;

        using var reader = new SqliteCommand(sql, db).ExecuteReader();
        while(reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    List<string> getTags() {
        var result = new List<string>();
        
        using var reader = new SqliteCommand("SELECT name FROM tag ORDER BY name;", db).ExecuteReader();
        while (reader.Read())
            result.Add(reader.GetString(0));

        return result;
    }

    Note? searchByID(Int64 id) {
        Note? result = null;
        
        var sql = """
          SELECT note_id, data, GROUP_CONCAT(name, ', ') AS tags, is_deleted
          FROM note
          INNER JOIN notedata ON note_id = notedata.rowid
          INNER JOIN note_to_tag USING (note_id)
          INNER JOIN tag         USING (tag_id)
          WHERE note_id = ?
          GROUP BY note_id
          ;
        """;
        var cmd = new SqliteCommand(sql, db);
        cmd.Parameters.AddRange(new[] { new SqliteParameter().Value = id });
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            result = new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3));

        return result;
    }

    List<Note> searchByTag(string tag, bool fetchDeleted)
    {
        var result = new List<Note>();

        var sql = """
          SELECT note_id, data, GROUP_CONCAT(name, ', ') AS tags, is_deleted
          FROM note
          INNER JOIN notedata ON note_id = notedata.rowid
          INNER JOIN note_to_tag USING (note_id)
          INNER JOIN tag         USING (tag_id)
          WHERE note_id IN (SELECT note_id FROM tag INNER JOIN note_to_tag USING (tag_id) WHERE name = ?)
        """
        + (fetchDeleted ? "" : "AND NOT is_deleted ") +
        """
          GROUP BY note_id
          ORDER BY note.updated_at DESC
          ;
        """;
        var cmd  = new SqliteCommand(sql, db);
        cmd.Parameters.AddRange(new[] { new SqliteParameter().Value = tag });
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    List<Note> searchByKeyword(string word, bool fetchDeleted)
    {
        if (word == "") return [];
        var result = new List<Note>();

        var sql = """
          SELECT note_id, data, GROUP_CONCAT(name, ', ') AS tags, is_deleted
          FROM note
          INNER JOIN notedata ON note_id = notedata.rowid
          INNER JOIN note_to_tag USING (note_id)
          INNER JOIN tag         USING (tag_id)
          WHERE data MATCH ?
        """
        + (fetchDeleted ? "" : "AND NOT is_deleted ") +
        """
          GROUP BY note_id
          ORDER BY notedata.rank ASC, note.updated_at DESC
          ;
        """;
        var cmd = new SqliteCommand(sql, db);
        cmd.Parameters.AddRange(new[] { new SqliteParameter().Value = word });
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    void linkTagsToNote(Int64 noteId, List<string> tags)
    {
        using var tx = db.BeginTransaction();
        tags.ForEach(tags =>
        {
            var tagIdOpt = new SqliteCommand("SELECT tag_id FROM tag WHERE name = ?;", db, tx).ExecuteScalar() as Int64?;
            //var tagId = tagIdOpt ?? {222}();
        });

        tx.Commit();
    }
}
