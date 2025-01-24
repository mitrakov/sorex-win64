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

            // buttons/tags panel
            var tagsAndButtons = new Grid
            {
                //Rows = 1,
                //Columns = md.Tags.Length + (md.IsArchived ? 1 : 3), // all tags + 1 or 3 tagsAndButtons
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
            };
            tagsAndButtons.RowDefinitions.Add(new());
            for (int j = 0; j < md.Tags.Length + (md.IsArchived ? 1 : 3); j++)
                tagsAndButtons.ColumnDefinitions.Add(new());

            // tags
            var i = 0;
            for (; i < md.Tags.Length; i++)
            {
                var label = new TextBlock { Text = $"🏷️  {md.Tags[i]}" };
                var border = new Border
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    BorderBrush = new LinearGradientBrush(new([new(Colors.Purple, 0), new(Colors.Blue, 0.5), new(Colors.Purple, 1)]), 45),
                    Child = label,
                    CornerRadius = new(10),
                    BorderThickness = new(0.7),
                    Margin = new(4),
                    Padding = new(4, 1, 4, 2),
                };
                border.SetValue(Grid.RowProperty, 0);
                border.SetValue(Grid.ColumnProperty, i);
                tagsAndButtons.Children.Add(border);
            }

            // buttons
            if (md.IsArchived)
                tagsAndButtons.Children.Add(MakeButton(i++, "#90ee90", "restore.png", md.OnRestore));
            else
            {
                tagsAndButtons.Children.Add(MakeButton(i++, "#8eb5f7", "edit.png", md.OnEdit));
                tagsAndButtons.Children.Add(MakeButton(i++, "#fcc18a", "archive.png", md.OnArchive));
                tagsAndButtons.Children.Add(MakeButton(i++, "#ff655a", "delete.png", md.OnDelete));
            }

            // main grid
            var mainGrid = new Grid();
            mainGrid.Children.Add(viewer);
            mainGrid.Children.Add(tagsAndButtons);
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

    private static UIElement MakeButton(int columnIdx, string hexColour, string imageName, Action onClick)
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
        border.SetValue(Grid.RowProperty, 0);
        border.SetValue(Grid.ColumnProperty, columnIdx);
        border.MouseDown += (s, e) => onClick();

        return border;
    }
}
