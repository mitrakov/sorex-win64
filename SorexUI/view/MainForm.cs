using System.ComponentModel;
using SorexUI.viewmodel;

namespace SorexUI.view;

partial class MainForm : Form
{
    private MainViewModel vm;

    internal MainForm(MainViewModel vm)
    {
        InitializeComponent();
        InitializeComponents();
        this.vm = vm;
        vm.PropertyChanged += OnCurrentPathChanged;
    }

    private void OnCurrentPathChanged(object? sender, PropertyChangedEventArgs e)
    {
        Text = e.PropertyName;
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
            sorexMarkdown.SetMarkdown(vm.SearchByTag(btn.Text, checkShowArchive.Checked).Select(note => note.data));
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

    private void onNewButtonClick(object sender, EventArgs e)
    {
        vm.OpenFile(@"C:\Users\Tommy\db\it.db");
    }
}
