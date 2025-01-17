using System.Windows.Controls;

namespace SorexMarkdownLibrary;
public partial class SorexMarkdown : UserControl {
    public SorexMarkdown() {
        InitializeComponent();
    }

    public string Markdown {
        set {
            if (DataContext is Data data)
                data.Markdown = value;
        }
    }
}
