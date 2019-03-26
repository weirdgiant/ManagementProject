using ManagementProject.FunctionalWindows;
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
        public MainWindow _mainWin;
        private AlarmButtonControl alarmControl;
        private DeviceControl deviceControl;
        private SearchBoxControl searchBoxControl;
        private ErrorDeviceControl errorDeviceControl;
        public static MainPage mainPage;
        public MainPage()
        {
            InitializeComponent();
            mainPage = this;
            MainPageInit();
            ControlInit();
            mapcontrolbt.bt.Click += Button2_Click;
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (alarmControl!=null)
            {
                alarmControl.Close();
            }
            if (deviceControl != null)
            {
                deviceControl.Close();
            }
            if (searchBoxControl!=null)
            {
                searchBoxControl.Close();
            }
            if (errorDeviceControl !=null)
            {
                errorDeviceControl.Close();
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SearchBoxControlInit();
            AlarmControlInit();
            DeviceControlInit();
            ErrorDeviceControlInit();
        }
        /// <summary>
        /// 初始化搜索控件
        /// </summary>
        private void SearchBoxControlInit()
        {
            searchBoxControl = new SearchBoxControl
            {
                DataContext = mainPageViewModel,
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 23,
                Top = 80
            };
            searchBoxControl.Show();
        }

        /// <summary>
        /// 初始化报警按钮
        /// </summary>
        private void AlarmControlInit()
        {
            alarmControl = new AlarmButtonControl
            {
                DataContext = mainPageViewModel,
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 295,
                Top = 75
            };
            alarmControl.Show();
        }
        /// <summary>
        /// 初始化设备显示按钮
        /// </summary>
        public void DeviceControlInit()
        {
            deviceControl = new DeviceControl
            {
                DataContext = mainPageViewModel,
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 23,
                Top = 980
            };
            deviceControl.Show();
        }

        public void ErrorDeviceControlInit()
        {
            errorDeviceControl = new ErrorDeviceControl()
            {
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 1820,
                Top = 880
            };
            errorDeviceControl.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable.PlayerWindowIsOpened == false)
            {
                PlayerWindow newwindow = new PlayerWindow(PlayerWindowType.Track);
                newwindow.Topmost = true;
                newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
                newwindow.Left = 23;
                newwindow.Top = 165;
                newwindow.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
        }

        private void MainPageInit()
        {
            _mainWin = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = MainWindowViewModel.mainPageViewModel;
            DataContext = mainPageViewModel;         
        }

        private void ControlInit()
        {
            mapcontrolbt .BtImage = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/mainwindowicon/2d.png", UriKind.Relative));
            deviceinfobt .BtImage = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/mainwindowicon/设备信息.png", UriKind.Relative));
        }

    }
}
