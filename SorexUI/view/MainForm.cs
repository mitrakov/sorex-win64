using System.ComponentModel;
using SorexMarkdownLibrary;
using SorexUI.model;
using SorexUI.viewmodel;

namespace SorexUI.view;

partial class MainForm : Form
{
    private readonly MainViewModel vm;

    private long? currentNoteId;                        // if present, it's an ID of the note in edit mode
    private string oldTags = "";                        // old comma-separated tags for edit mode (to calc tags diff)
    private string search = "";                         // search by tag name (SearchMode.tag), keyword (.keyword) or ID (.id)
    private SearchMode searchMode = SearchMode.tag;     // how to search notes (by clicking tag, by full-text, etc)

    internal MainForm(MainViewModel vm)
    {
        InitializeComponent();
        InitializeComponents();
        this.vm = vm;
        vm.PropertyChanged += OnCurrentPathChanged;
    }

    private void OnTextboxEditChange(object sender, EventArgs e) => sorexMarkdownSingle.Markdown = textboxEdit.Text;

    private void OnTextboxTagsChange(object sender, EventArgs e) => oldTags = textboxTags.Text.Trim();

    private void OnCheckboxShowArchiveChange(object sender, EventArgs e) => SetReadMode(search, searchMode);

    private void OnNewButtonClick(object sender, EventArgs e)
    {
        SetEditMode();
    }

    private void OnSaveNoteClick(object sender, EventArgs e)
    {
        var newId = vm.SaveNote(currentNoteId, textboxEdit.Text.ReplaceLineEndings("\n"), textboxTags.Text.Trim(), oldTags);
        if (newId != null)
            SetReadMode($"{newId}", SearchMode.id);
    }

    private void OnCurrentPathChanged(object? sender, PropertyChangedEventArgs e)
    {
        Text = $"Sorex ({e.PropertyName})";
        tagsPanel.Controls.Clear();
        tagsPanel.Controls.AddRange(vm.GetTags().Select(tag =>
        {
            var btn = new Button { Text = tag, Size = new(170, 30), TextAlign = ContentAlignment.MiddleLeft };
            btn.Click += OnTagClick;
            return btn;
        }).ToArray());
    }

    private void OnTagClick(object? sender, EventArgs e)
    {
        if (sender is Button btn)
            SetReadMode(btn.Text, SearchMode.tag);
    }

    private void OnRecentFileClick(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item)
            vm.OpenFile(item.Text ?? "");
    }

    private void OnNewFileClick(object sender, EventArgs e) => vm.NewFile();

    private void OnOpenFileClick(object sender, EventArgs e) => vm.OpenFile();

    private void OnCloseFileClick(object sender, EventArgs e) => vm.CloseFile();

    private void OnQuitSorexClick(object sender, EventArgs e)
    {
        vm.CloseFile();
        Application.Exit();
    }

    private void OnAboutSorexClick(object sender, EventArgs e)
    {
        MessageBox.Show("Sorex App"); // TODO FIXME message
    }

    protected void SetEditMode(long? noteId = null, string text = "", string tags = "")
    {
        contentPanel.Controls.Clear();
        contentPanel.Controls.Add(editModePanel);

        textboxEdit.Text = text;
        textboxTags.Text = tags;
        textboxSearch.Text = "";
        currentNoteId = noteId;
        oldTags = tags;
        sorexMarkdownMulti.SetMarkdown([]);
        search = "";
        searchMode = SearchMode.tag;
    }

    protected void SetReadMode(string search, SearchMode by)
    {
        contentPanel.Controls.Clear();
        contentPanel.Controls.Add(wpfHostMulti);

        textboxEdit.Text = "";
        textboxTags.Text = "";
        textboxSearch.Text = "";
        currentNoteId = null;
        oldTags = "";
        sorexMarkdownSingle.Markdown = "";
        this.search = search;
        searchMode = by;

        var notes = by == SearchMode.tag ? vm.SearchByTag(search, checkShowArchive.Checked) :
                    by == SearchMode.keyword ? vm.SearchByKeyword(search, checkShowArchive.Checked) :
                    by == SearchMode.id ? new[] { vm.SearchByID(long.Parse(search)) }.OfType<Note>() : [];
        var ctx = notes.Select(note => new ContextMenu(
            note.data,
            note.isDeleted,
            note.tags.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
            () => SetEditMode(note.id, note.data, note.tags),
            () => { vm.ArchiveNoteById(note.id); SetReadMode(search, searchMode); },
            () => { vm.RestoreNoteById(note.id); SetReadMode(search, searchMode); },
            () => { vm.DeleteNoteById(note.id); SetReadMode(search, searchMode); }
        ));
        sorexMarkdownMulti.SetMarkdown(ctx);
    }
}

enum SearchMode { tag, keyword, id }
