using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Services;
using Utils;

namespace ViewModels{
    public class MainWindowViewModel : INotifyPropertyChanged{
        private readonly IJsonConvertService jsonService;

        private TabItemViewModel? selectedTab;
        public TabItemViewModel? SelectedTab{
            get { return selectedTab; }
            set { selectedTab = value; OnPropertyChanged(); }
        }

        private string indentOption = "2";
        public string IndentOption{
            get { return indentOption; }
            set { indentOption = value; OnPropertyChanged(); }
        }

        private string? statusMessage;
        public string? StatusMessage{
            get { return statusMessage; }
            set { statusMessage = value; OnPropertyChanged(); }
        }

        public ICommand ConvertCommand { get; }
        public ICommand PrettyPrintCommand { get; }
        public ICommand MinifyCommand { get; }

        public MainWindowViewModel(IJsonConvertService jsonService){
            this.jsonService = jsonService;
            // TabTextEditorコントロールからSelectedTabをバインド
            ConvertCommand = new RelayCommand(ConvertTabText);
            PrettyPrintCommand = new RelayCommand(PrettyPrintTabText);
            MinifyCommand = new RelayCommand(MinifyTabText);
        }

        private void ConvertTabText(object? obj){
            if (SelectedTab == null) return;
            string error;
            string result = jsonService.TabSeparatedToJson(SelectedTab.Text, IndentOption, out error);
            if (!string.IsNullOrEmpty(error)){
                StatusMessage = "変換エラー: " + error;
            } else {
                SelectedTab.Text = result;
                StatusMessage = "変換成功";
            }
        }

        private void PrettyPrintTabText(object? obj){
            if (SelectedTab == null) return;
            string error;
            string result = jsonService.PrettyPrintJson(SelectedTab.Text, IndentOption, out error);
            if (!string.IsNullOrEmpty(error)){
                StatusMessage = "整形エラー: " + error;
            } else {
                SelectedTab.Text = result;
                StatusMessage = "整形完了";
            }
        }

        private void MinifyTabText(object? obj){
            if (SelectedTab == null) return;
            string error;
            string result = jsonService.MinifyJson(SelectedTab.Text, out error);
            if (!string.IsNullOrEmpty(error)){
                StatusMessage = "圧縮エラー: " + error;
            } else {
                SelectedTab.Text = result;
                StatusMessage = "圧縮完了";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
