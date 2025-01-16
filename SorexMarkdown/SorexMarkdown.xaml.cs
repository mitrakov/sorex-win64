using System.Windows.Controls;

namespace SorexMarkdown
{
    public partial class MdControl : UserControl {
        public MdControl() {
            InitializeComponent();
        }

        public string Markdown {
            set { (DataContext as Data).Markdown = value; }
        }
    }
}
