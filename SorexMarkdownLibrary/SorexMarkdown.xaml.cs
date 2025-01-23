using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MdXaml;

namespace SorexMarkdownLibrary;

public record class MarkdownContext(string markdown, Action onEdit, Action onArchive, Action onDelete);

public partial class SorexMarkdown : UserControl
{
    public SorexMarkdown()
    {
        InitializeComponent();
    }

    public void SetMarkdown(IEnumerable<MarkdownContext> markdowns)
    {
        MainStackPanel.Children.Clear();
        foreach (var md in markdowns)
        {
            // markdown viewer
            var viewer = new MarkdownScrollViewer
            {
                Markdown = md.markdown,
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
                Columns = 3,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
            };
            buttons.Children.Add(MakeButton("#8eb5f7", "edit.png", md.onEdit));
            buttons.Children.Add(MakeButton("#fcc18a", "archive.png", md.onArchive));
            buttons.Children.Add(MakeButton("#ff655a", "delete.png", md.onDelete));

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
