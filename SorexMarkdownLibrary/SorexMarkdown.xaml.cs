using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                Markdown = md,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            };
            viewer.PreviewMouseWheel += MarkdownScrollViewer_PreviewMouseWheel;

            var buttonsPanel = new UniformGrid
            {
                Rows = 1,
                Columns = 3,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top
            };
            buttonsPanel.Children.Add(MakeButton("#8eb5f7", "edit.png"));
            buttonsPanel.Children.Add(MakeButton("#fcc18a", "archive.png"));
            buttonsPanel.Children.Add(MakeButton("#ff655a", "delete.png"));

            var mainGrid = new Grid();
            mainGrid.Children.Add(viewer);
            mainGrid.Children.Add(buttonsPanel);

            MainStackPanel.Children.Add(mainGrid);
        }
    }

    private void TitleBorder_MouseDown(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("Hey hey");
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

    private Border MakeButton(string hexColour, string imageName)
    {
        var image = new Image
        {
            Source = new BitmapImage(new Uri($"/SorexMarkdownLibrary;component/images/{imageName}", UriKind.Relative)),
            VerticalAlignment = VerticalAlignment.Center,
            Height = 24,
            Width = 24,
        };

        var border = new Border
        {
            CornerRadius = new CornerRadius(20),
            Background = new BrushConverter().ConvertFromString(hexColour) as Brush,
            Margin = new Thickness(6),
            Padding = new Thickness(6),
            Child = image,
        };
        border.MouseDown += TitleBorder_MouseDown;

        return border;
    }
}
