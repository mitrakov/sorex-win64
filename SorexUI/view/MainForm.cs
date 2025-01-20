namespace SorexUI.view;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        InitializeComponents();
    }

    private void editBox_TextChanged(object sender, EventArgs e)
    {
        markdown.Markdown = editBox.Text;
    }

    private void onRecentFileClick(object sender, EventArgs e)
    {
        MessageBox.Show(">" + sender);
    }

    private void onNewFileClick(object sender, EventArgs e)
    {
        //MessageBox.Show("fjose");
    }
}
