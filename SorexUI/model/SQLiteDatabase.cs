using Microsoft.Data.Sqlite;

namespace SorexUI.model;

internal class SQLiteDatabase
{
    private SqliteConnection db = new();

    void OpenDb(string path)
    {
        CloseDb();
        db = new SqliteConnection($"Filename={path}");
        //db.ForeignKeys = true; // TODO
    }

    void CloseDb()
    {
        db.Close();
    }

    bool IsConnected()
    {
        return db.State == System.Data.ConnectionState.Open;
    }

    void CreateDb(string path)
    {
        OpenDb(path);
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
        SqlCmd(sql, tx).ExecuteNonQuery();

        tx.Commit(); // TODO check if necessary
    }

    Int64 InsertNote(string data)
    {
        using var tx = db.BeginTransaction();
        var noteId = SqlCmd("INSERT INTO note DEFAULT VALUES;", tx).ExecuteScalar() as Int64? ?? -1;
        SqlCmd("INSERT INTO notedata (rowid, data) VALUES (?, ?);", tx, noteId, data).ExecuteScalar();

        tx.Commit();
        return noteId;
    }

    void UpdateNote(Int64 noteId, string data)
    {
        using var tx = db.BeginTransaction();

        new List<SqliteCommand>([
            SqlCmd("UPDATE notedata SET data = ? WHERE rowid = ?;", tx, data, noteId),
            SqlCmd("UPDATE note SET updated_at = CURRENT_TIMESTAMP WHERE note_id = ?;", tx, noteId)
        ]).ForEach(cmd => cmd.ExecuteNonQuery());

        tx.Commit();
    }

    void SoftDeleteNote(Int64 noteId, bool deleted)
    {
        SqlCmd("UPDATE note SET is_deleted = ?, updated_at = CURRENT_TIMESTAMP WHERE note_id = ?;", deleted, noteId).ExecuteNonQuery();
    }

    void DeleteNote(Int64 noteId)
    {
        using var tx = db.BeginTransaction();

        new List<SqliteCommand>([
            SqlCmd("DELETE FROM note     WHERE note_id = ?;", tx, noteId),
            SqlCmd("DELETE FROM notedata WHERE rowid = ?;", tx, noteId),
            SqlCmd("DELETE FROM tag      WHERE tag_id NOT IN (SELECT DISTINCT tag_id FROM note_to_tag);", tx)
        ]).ForEach(cmd => cmd.ExecuteNonQuery());

        tx.Commit();
    }


    List<Note> GetNotes(bool fetchDeleted)
    {
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

        using var reader = SqlCmd(sql).ExecuteReader();
        while (reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    List<string> GetTags()
    {
        var result = new List<string>();

        using var reader = SqlCmd("SELECT name FROM tag ORDER BY name;").ExecuteReader();
        while (reader.Read())
            result.Add(reader.GetString(0));

        return result;
    }

    Note? SearchByID(Int64 id)
    {
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
        using var reader = SqlCmd(sql, id).ExecuteReader();
        while (reader.Read())
            result = new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3));

        return result;
    }

    List<Note> SearchByTag(string tag, bool fetchDeleted)
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
        using var reader = SqlCmd(sql, tag).ExecuteReader();
        while (reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    List<Note> SearchByKeyword(string word, bool fetchDeleted)
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
        using var reader = SqlCmd(sql, word).ExecuteReader();
        while (reader.Read())
            result.Add(new Note(reader.GetInt64(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3)));

        return result;
    }

    void LinkTagsToNote(Int64 noteId, List<string> tags)
    {
        if (tags.Count == 0) return;

        using var tx = db.BeginTransaction();
        tags.ForEach(tag =>
        {
            var tagIdOpt = SqlCmd("SELECT tag_id FROM tag WHERE name = ?;", tx, tag).ExecuteScalar() as Int64?;
            var tagId = tagIdOpt.IfNull(() => SqlCmd("INSERT INTO tag (name) VALUES (?);", tx, tag).ExecuteScalar() as Int64? ?? -1) ?? -1;
            SqlCmd("INSERT INTO note_to_tag (note_id, tag_id) VALUES (?, ?);", tx, noteId, tagId).ExecuteNonQuery();
        });

        tx.Commit();
    }

    void UnlinkTagsFromNote(Int64 noteId, List<string> tags)
    {
        if (tags.Count == 0) return;

        using var tx = db.BeginTransaction();
        var IN = string.Join(",", Enumerable.Repeat("?", tags.Count)); // "?,?,?,?"
        new List<SqliteCommand>([
            SqlCmd($"DELETE FROM note_to_tag WHERE note_id = ? AND tag_id IN (SELECT tag_id FROM tag WHERE name IN (${IN}));", tx, noteId, tags),
            SqlCmd( "DELETE FROM tag WHERE tag_id NOT IN (SELECT DISTINCT tag_id FROM note_to_tag);", tx)
        ]).ForEach(cmd => cmd.ExecuteNonQuery());

        tx.Commit();
    }

    protected SqliteCommand SqlCmd(string query, params object[] args)
    {
        var cmd = new SqliteCommand(query, db);
        cmd.Parameters.AddRange(args.Select(arg => new SqliteParameter { Value = arg }));
        return cmd;
    }

    protected SqliteCommand SqlCmd(string query, SqliteTransaction tx, params object[] args)
    {
        var cmd = new SqliteCommand(query, db, tx);
        cmd.Parameters.AddRange(args.Select(arg => new SqliteParameter { Value = arg }));
        return cmd;
    }
}
