using System.Windows;
using ViewModels;
using Services;

public partial class MainWindow : Window{
    public MainWindow(){
        InitializeComponent();
        IJsonConvertService jsonService = new JsonConvertService();
        DataContext = new MainWindowViewModel(jsonService);
    }
}
