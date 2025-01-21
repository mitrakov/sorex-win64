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
        MessageBox.Show(e.PropertyName);
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
}
