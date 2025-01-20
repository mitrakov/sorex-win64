using SorexUI.model;

namespace SorexUI.viewmodel;

internal class MainViewModel
{
    private SQLiteDatabase db = new();
    private string? currentPath;

    internal void OpenFile(string path)
    {
        if (File.Exists(path))
        {
            Console.WriteLine($"Opening file {path}");
            db.OpenDb(path);
            currentPath = path;
            AddToRecentFilesList(path);
        }
        else
        {
            MessageBox.Show($"File not found:\n{path}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            RemoveFromRecentFilesList(path);
        }
    }

    internal void OpenFile()
    {
        var dialog = new OpenFileDialog() { Title = "Select a DB file", DefaultExt = "db" }; 
        if (dialog.ShowDialog() == DialogResult.OK) 
            OpenFile(dialog.FileName);
    }

    internal void NewFile()
    {
        var dialog = new SaveFileDialog() { Title = "New DB file", FileName = "mydb", DefaultExt = "db"};
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var path = dialog.FileName;
            if (File.Exists(path)) {
                if (MessageBox.Show($"File already exists:\n{path}\n\nDo you want to erase it?\nIt will remove all data", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    db.CloseDb();
                    File.Delete(path);
                }
            }

            Console.WriteLine($"Creating file {path}");
            db.CreateDb(path);
            currentPath = path;
            AddToRecentFilesList(path);
        }
    }

    internal void CloseFile()
    {
        db.CloseDb();
        currentPath = null;
    }

    internal void GetRecentFiles()
    {
        // TODO
    }

    internal IEnumerable<string> GetTags()
    {
        if (!db.IsConnected) return [];
        return db.GetTags();
    }

    internal IEnumerable<Note> GetNotes(bool showArchive)
    {
        if (!db.IsConnected) return [];
        return db.GetNotes(showArchive);
    }

    internal Note? SearchByID(Int64 noteId)
    {
        if (!db.IsConnected) return null;
        return db.SearchByID(noteId);
    }

    internal IEnumerable<Note> SearchByTag(string tag, bool showArchive)
    {
        if (tag.Length == 0) return [];
        if (!db.IsConnected) return [];
        return db.SearchByTag(tag, showArchive);
    }

    internal IEnumerable<Note> SearchByKeyword(string word, bool showArchive)
    {
        if (word.Length == 0) return [];
        if (!db.IsConnected) return [];
        return db.SearchByKeyword(word, showArchive);
    }

    internal void ArchiveNoteById(Int64 noteId)
    {
        if (!db.IsConnected) return;
        if (MessageBox.Show("Are you sure you want to archive this note? It can be restored later", "Archive note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            db.SoftDeleteNote(noteId, true);
    }

    internal void RestoreNoteById(Int64 noteId)
    {
        if (!db.IsConnected) return;
        db.SoftDeleteNote(noteId, false);
    }

    internal void DeleteNoteById(Int64 noteId)
    {
        if (!db.IsConnected) return;
        if (MessageBox.Show("Are you sure you want to delete this note?", "Delete note", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            db.DeleteNote(noteId);
    }

    internal Int64? SaveNote(Int64? noteId, string data, string newTags, string oldTags)
    {
        var tags = newTags.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (!db.IsConnected) return null;
        if (data.Length == 0) return null;
        if (tags.Count() == 0)
        {
            MessageBox.Show("Please add at least 1 tag\ne.g. \"Work\" or \"TODO\"", "Tag required", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        if (noteId is Int64 id)
        {
            // UPDATE
            db.UpdateNote(id, data);
            UpdateTags(id, newTags, oldTags);
            MessageBox.Show("Note updated", "Done");
            return noteId;
        }
        else
        {
            // INSERT
            var newNoteId = db.InsertNote(data);
            db.LinkTagsToNote(newNoteId, tags);
            MessageBox.Show("Note added", "Done");
            return newNoteId;
        }
    }

    private void UpdateTags(Int64 noteId, string newTags, string oldTags)
    {
        var oldtags = oldTags.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet();
        var newtags = newTags.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToHashSet();
        var rmTags = oldtags.Except(newtags);
        var addTags = newtags.Except(oldtags);

        db.UnlinkTagsFromNote(noteId, rmTags);
        db.LinkTagsToNote(noteId, addTags);
    }

    private void AddToRecentFilesList(string item)
    {
        // TODO not impl
    }

    private void RemoveFromRecentFilesList(string item)
    {
        // TODO not impl
    }
}
