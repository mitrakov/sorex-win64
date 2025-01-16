using System;
using System.Windows.Forms;

namespace Sorex
{
    public partial class Sorex : Form {
        public Sorex() {
            InitializeComponent();
        }

        private void editBox_TextChanged(object sender, EventArgs e) {
            markdown.Markdown = editBox.Text;
        }
    }
}
