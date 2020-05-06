using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ManagementProject.PageView
{
    /// <summary>
    /// CollagePage.xaml 的交互逻辑
    /// </summary>
    public partial class CollagePage : Page
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public CollagePage()
        {
            InitializeComponent();
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            DataContext = MainWindowViewModel.collagePageViewModel;            
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e) => PopScenes.IsOpen = false;

        private void Bt_MouseEnter(object sender, MouseEventArgs e) => bt.IsChecked = true;
    }
    
}
