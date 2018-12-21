using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.PageView
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            choosetb.message.MouseLeftButtonDown += Message_MouseLeftButtonDown;
            Loaded += MainPage_Loaded;
            MainPageInit();
            ControlInit();
        }

        private void MainPageInit()
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            mainPageViewModel = MainWindowViewModel.mainPageViewModel;
            DataContext = mainPageViewModel;         
        }

        private void ControlInit()
        {
            camerastatistics.DataContext = mainPageViewModel.CameraStatisticsViewModel;
            waterstatistics.DataContext = mainPageViewModel.WaterStatisticsViewModel;
            alarmbt.DataContext = mainPageViewModel.carAlarmViewModel;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Message_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SchoolMessage scm = new SchoolMessage();
            scm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scm.ShowDialog();
        }
    }
}
