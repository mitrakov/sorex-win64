using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MdXaml;

namespace SorexMarkdownLibrary;

public record class ContextMenu(string Markdown, bool IsArchived, string[] Tags, Action OnEdit, Action OnArchive, Action OnRestore, Action OnDelete);

public partial class SorexMarkdown : UserControl
{
    public SorexMarkdown()
    {
        InitializeComponent();
    }

    public void SetMarkdown(IEnumerable<ContextMenu> markdowns)
    {
        MainStackPanel.Children.Clear();
        foreach (var md in markdowns)
        {
            // markdown viewer
            var viewer = new MarkdownScrollViewer
            {
                Markdown = md.Markdown,
                MarkdownStyle = MarkdownStyle.GithubLike,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                ClickAction = ClickAction.SafetyDisplayWithRelativePath,
            };
            viewer.PreviewMouseWheel += MarkdownScrollViewer_PreviewMouseWheel;

            // buttons panel
            var buttons = new UniformGrid
            {
                Rows = 1,
                Columns = md.IsArchived ? 1 : 3,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
            };
            if (md.IsArchived)
                buttons.Children.Add(MakeButton("#90ee90", "restore.png", md.OnRestore));
            else
            {
                buttons.Children.Add(MakeButton("#8eb5f7", "edit.png", md.OnEdit));
                buttons.Children.Add(MakeButton("#fcc18a", "archive.png", md.OnArchive));
                buttons.Children.Add(MakeButton("#ff655a", "delete.png", md.OnDelete));
            }

            // main grid
            var mainGrid = new Grid();
            mainGrid.Children.Add(viewer);
            mainGrid.Children.Add(buttons);

            // add to VStack
            MainStackPanel.Children.Add(mainGrid);
        }
    }

    // https://stackoverflow.com/a/16110178/2212849
    private void MarkdownScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is MarkdownScrollViewer sv)
        {
            var parent = sv.Parent as UIElement;
            parent?.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta) { RoutedEvent = MouseWheelEvent });
        }
    }

    private static UIElement MakeButton(string hexColour, string imageName, Action onClick)
    {
        var border = new Border
        {
            CornerRadius = new(16),
            Background = new BrushConverter().ConvertFromString(hexColour) as Brush,
            Margin = new(4),
            Padding = new(6),
            Child = new Image
            {
                Source = new BitmapImage(new Uri($"/SorexMarkdownLibrary;component/images/{imageName}", UriKind.Relative)),
                VerticalAlignment = VerticalAlignment.Center,
                Height = 18,
                Width = 18,
            },
        };
        border.MouseDown += (s, e) => onClick();

        return border;
    }
}
