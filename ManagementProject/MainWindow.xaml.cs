using ManagementProject.FunctionalWindows;

using ManagementProject.PageView;
using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;


namespace ManagementProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainWindowViewModel { get; set; }
        private MainWindowMenuViewModel mainWindowMenuViewModel { get; set; }
        public int windowstate = 0;//0-最大化 ,1正常
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel  = new MainWindowViewModel();
            DataContext =MainWindowViewModel;
            ControlsInit();
        }

        /// <summary>
        /// 初始化自定义控件
        /// </summary>
        private void ControlsInit()
        {
            mainWindowMenuViewModel = new MainWindowMenuViewModel(MainWindowViewModel);
            mainMenu.DataContext = mainWindowMenuViewModel;
        }



        public void _uri(string uri)
        {
            this.mainframe.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }



        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }


        private void recoverbt_Click(object sender, RoutedEventArgs e)
        {
            if (windowstate==0)
            {
                recoverbt.Content = "Max";
                WindowState = WindowState.Normal;
                windowstate = 1;
            }
            else if (windowstate == 1)
            {
                recoverbt.Content = "Normal";
                WindowState = WindowState.Maximized;
                windowstate = 0;
            }

        }
        
        private void button3_Click(object sender, RoutedEventArgs e)
        {
           
            if (GlobalVariable.PlayerWindowIsOpened==false)
            {
                PlayerWindow newwindow = new PlayerWindow(PlayerWindowType.Track );
                newwindow.Topmost = true;
                newwindow.Owner = this;
                newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
                newwindow.Left = 23;
                newwindow.Top = 165;
                newwindow.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
           
        }
    }
}
