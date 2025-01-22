using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MdXaml;

namespace SorexMarkdownLibrary;
public partial class SorexMarkdown : UserControl
{
    public SorexMarkdown()
    {
        InitializeComponent();
    }

    public void SetMarkdown(IEnumerable<string> mds)
    {
        MainStackPanel.Children.Clear();
        foreach (var md in mds)
        {
            var viewer = new MarkdownScrollViewer
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                ClickAction = ClickAction.SafetyDisplayWithRelativePath,
                MarkdownStyle = MarkdownStyle.GithubLike,
                Markdown = md
            };
            viewer.PreviewMouseWheel += MarkdownScrollViewer_PreviewMouseWheel; // https://stackoverflow.com/a/16110178/2212849
            MainStackPanel.Children.Add(viewer);
        }
    }

    private void MarkdownScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is MarkdownScrollViewer sv)
        {
            var parent = sv.Parent as UIElement;
            parent?.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) { RoutedEvent = MouseWheelEvent });
        }
    }
}
