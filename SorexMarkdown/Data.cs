using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SorexMarkdown {
    internal class Data: INotifyPropertyChanged {
        private string _markdown = "";

        public string Markdown {
            get { return _markdown; }
            set {
                if (_markdown != value) { 
                    _markdown = value;
                    FirePropertyChanged();
                }
            }
        }

        protected void FirePropertyChanged([CallerMemberName] string propertyName = "") {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
