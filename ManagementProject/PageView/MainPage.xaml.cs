using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
            //deviceinfobt.bt.Click += Bt_Click;
            chartbt.bt.Click += Bt_Click1;
        }



        private void Bt_Click1(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.LoadChartPage();
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            mainPageViewModel.IsOpenTag = !mainPageViewModel.IsOpenTag;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.CloseFunWin();
            if (alarmControl!=null)
            {
                alarmControl.Close();
            }
            if (deviceControl != null)
            {
                deviceControl.Close();
                GlobalVariable.deviceControl = null;
            }
            if (searchBoxControl!=null)
            {
                searchBoxControl.Close();
            }
            if (errorDeviceControl !=null)
            {
                errorDeviceControl.Close();
            }
            ClearBindingValue();
            mainPageViewModel.alarmControl = null;
            DataContext = null;
        }

        private void ClearBindingValue()
        {
            mainPageViewModel.Sid = 0;
            mainPageViewModel.SelectedElementCode = "";
            mainPageViewModel.SelectedDeviceCode = "";
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
            searchBoxControl = new SearchBoxControl();
            searchBoxControl.DataContext = mainPageViewModel;
            searchBoxControl.Owner = _mainWin;
            searchBoxControl.Topmost = true;
            searchBoxControl.WindowStartupLocation = WindowStartupLocation.Manual;
            searchBoxControl.Left = 23;
            searchBoxControl.Top = 80;
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
            mainPageViewModel.alarmControl = alarmControl;
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
            GlobalVariable.deviceControl = deviceControl;
            mainPageViewModel.deviceControl = deviceControl;
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
            mainPageViewModel.errorDeviceControl = errorDeviceControl;
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
          //  deviceinfobt .BtImage = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/mainwindowicon/设备信息.png", UriKind.Relative));
            chartbt.BtImage = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/返回图表.png", UriKind.Relative));
        }

    }
}
