using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;
using Utils;

namespace Controls{
    // タブの追加・削除・選択のみを管理するコントロール。
    public partial class TabControls : UserControl{
        private ObservableCollection<TabItemViewModel> tabs { get; } = new ObservableCollection<TabItemViewModel>();

        public TabItemViewModel SelectedTab{
            get { return (TabItemViewModel)GetValue(SelectedTabProperty); }
            set {
                if (value != null && value.Header == "＋")
                {
                    AddTab();
                }
                else
                {
                    SetValue(SelectedTabProperty, value);
                }
            }
        }

        public static readonly DependencyProperty TabsProperty =
            DependencyProperty.Register(
                "Tabs",
                typeof(ObservableCollection<TabItemViewModel>),
                typeof(TabControls),
                new PropertyMetadata(new ObservableCollection<TabItemViewModel>())
            );

        public ObservableCollection<TabItemViewModel> Tabs{
            get { return (ObservableCollection<TabItemViewModel>)GetValue(TabsProperty); }
            set { SetValue(TabsProperty, value); }
        }

        public static readonly DependencyProperty SelectedTabProperty =
            DependencyProperty.Register("SelectedTab", typeof(TabItemViewModel), typeof(TabControls),
                new PropertyMetadata(null));

        public ICommand AddTabCommand { get; }
        public ICommand CloseTabCommand { get; }

        public TabControls(){
            InitializeComponent();

            // 通常タブ＋追加用タブ
            Tabs.Add(new TabItemViewModel("Tab1", ""));
            Tabs.Add(new TabItemViewModel("＋", ""));
            SelectedTab = Tabs[0];

            AddTabCommand = new RelayCommand(_ => AddTab());
            CloseTabCommand = new RelayCommand(param => CloseTab(param as TabItemViewModel), _ => Tabs.Count > 2); // 追加用タブを除外

            DataContext = this;
        }

        private void AddTab(){
            int idx = Tabs.Count; // 追加用タブの直前に追加
            TabItemViewModel tab = new TabItemViewModel($"Tab{idx}", "");
            Tabs.Insert(Tabs.Count - 1, tab);
            SelectedTab = tab;
        }

        private void CloseTab(TabItemViewModel? tab){
            if (tab == null || Tabs.Count <= 2) return; // 追加用タブを除外

            int idx = Tabs.IndexOf(tab);
            if (idx < 0) return;

            bool wasSelected = SelectedTab == tab;
            Tabs.Remove(tab);

            if (wasSelected && Tabs.Count > 1){
                SelectedTab = Tabs[Math.Max(0, idx - 1)];
            }
        }

    }
}
