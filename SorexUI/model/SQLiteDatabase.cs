using Microsoft.Data.Sqlite;

namespace SorexUI.model;

internal class SQLiteDatabase {
    private SqliteConnection db = new SqliteConnection();

    void openDb(string path) {
        closeDb();
        // TODO
    }

    void closeDb() {
        db.Close();
    }
}
