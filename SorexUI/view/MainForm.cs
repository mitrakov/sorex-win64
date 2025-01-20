using SorexUI.viewmodel;

namespace SorexUI.view;

public partial class MainForm : Form
{
    private readonly MainViewModel vm = new();

    public MainForm()
    {
        InitializeComponent();
        InitializeComponents();
    }

    private void EditBox_TextChanged(object sender, EventArgs e)
    {
        markdown.Markdown = editBox.Text;
    }

    private void OnRecentFileClick(object sender, EventArgs e)
    {
        if (sender is ToolStripMenuItem item)
            vm.OpenFile(item.Text ?? "");
    }

    private void OnNewFileClick(object sender, EventArgs e) => vm.NewFile();

    private void OnOpenFileClick(object sender, EventArgs e) => vm.OpenFile();

    private void OnCloseFile(object sender, EventArgs e) => vm.CloseFile();

    private void OnExitClick(object sender, EventArgs e)
    {
        vm.CloseFile();
        Application.Exit();
    }

    private void OnAboutSorexClick(object sender, EventArgs e)
    {
        MessageBox.Show("Sorex App"); // TODO FIXME message
    }
}
