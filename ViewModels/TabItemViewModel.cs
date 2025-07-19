using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels{
    // タブ付きエディタの各タブ（ファイルやドキュメント）を表すViewModel。
    // タブのタイトル（Header）と内容（Text）を保持し、バインディングや編集状態の管理に利用する。
    public class TabItemViewModel : INotifyPropertyChanged{
        private string header = string.Empty;
        private string text = string.Empty;

        public string Header{
            get { return header; }
            set { header = value; OnPropertyChanged(); }
        }

        public string Text{
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        public TabItemViewModel(string header, string text){
            Header = header;
            Text = text;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
