using System.ComponentModel;
using SorexMarkdownLibrary;
using SorexUI.viewmodel;

namespace SorexUI.view;

partial class MainForm : Form
{
    private readonly MainViewModel vm;
    private bool editorMode = false;

    internal MainForm(MainViewModel vm)
    {
        InitializeComponent();
        InitializeComponents();
        this.vm = vm;
        vm.PropertyChanged += OnCurrentPathChanged;
    }

    private void TextBoxEditTextChanged(object sender, EventArgs e) => sorexMarkdownSingle.Markdown = textBoxEdit.Text;

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
        {
            var ctx = vm.SearchByTag(btn.Text, checkShowArchive.Checked).Select(note =>
                new ContextMenu(
                    note.data,
                    note.isDeleted,
                    note.tags.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
                    () => { if (editorMode) SetReadMode(); else SetEditMode(note.data); },
                    () => vm.ArchiveNoteById(note.id),
                    () => vm.RestoreNoteById(note.id),
                    () => vm.DeleteNoteById(note.id)
                )
            );
            sorexMarkdownMulti.SetMarkdown(ctx);
            SetReadMode();
        }
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

    private void OnNewButtonClick(object sender, EventArgs e)
    {
        vm.OpenFile(@"C:\Users\Tommy\db\it.db");
    }

    protected void SetEditMode(string data)
    {
        editorMode = true;
        textBoxEdit.Text = data.ReplaceLineEndings();
        contentPanel.Controls.Clear();
        contentPanel.Controls.Add(editModePanel);
    }

    protected void SetReadMode()
    {
        editorMode = false;
        contentPanel.Controls.Clear();
        contentPanel.Controls.Add(wpfHostMulti);
    }
}
