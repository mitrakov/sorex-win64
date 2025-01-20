namespace SorexUI.view;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    private void editBox_TextChanged(object sender, EventArgs e)
    {
        markdown.Markdown = editBox.Text;
    }
}
