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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// MainWindowMenu.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowMenu : UserControl
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public MainWindowMenu()
        {
            InitializeComponent();
            Loaded += MainWindowMenu_Loaded;
        }

        private void MainWindowMenu_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            DataContext = MainWindowViewModel.mainWindowMenuViewModel ;
        }
    }

    public class MainWindowMenuModel:INotifyPropertyChangedClass 
    {
        private bool _isOpened;
        public bool IsOpened
        {
            get
            {
                return _isOpened;
            }
            set
            {
                _isOpened = value;
                NotifyPropertyChanged("IsOpened");
                if (IsOpened)
                {

                    ShowPanel();
                }
                else
                {

                    HiddenPanel();
                }
            }
        }
        public void ShowPanel()
        {
            MenubtIcon = "/ManagementProject;component/ImageSource/Icon/menuicon/menuopen.png";
        }
        public void HiddenPanel()
        {
            MenubtIcon = "/ManagementProject;component/ImageSource/Icon/menuicon/menuclose.png";
        }

        private string _menubtIcon;
        public string MenubtIcon
        {
            get
            {
                return _menubtIcon;
            }
            set
            {
                _menubtIcon = value;
                NotifyPropertyChanged("MenubtIcon");
            }
        }

        private Visibility _isVisbility;
        public Visibility IsVisbility
        {
            get
            {
                return _isVisbility;
            }
            set
            {
                _isVisbility = value;
                NotifyPropertyChanged("IsVisbility");
            }
        }
        private Visibility _isMap;
        public Visibility IsMap
        {
            get
            {
                return _isMap;
            }
            set
            {
                _isMap = value;
                NotifyPropertyChanged("IsMap");
            }
        }
        private Visibility _isCollage;
        public Visibility IsCollage
        {
            get
            {
                return _isCollage;
            }
            set
            {
                _isCollage = value;
                NotifyPropertyChanged("IsCollage");
            }
        }
        private Visibility _isTracker;
        public Visibility IsTracker
        {
            get
            {
                return _isTracker;
            }
            set
            {
                _isTracker = value;
                NotifyPropertyChanged("IsTracker");
            }
        }
        private Visibility _isHistory;
        public Visibility IsHistory
        {
            get
            {
                return _isHistory;
            }
            set
            {
                _isHistory = value;
                NotifyPropertyChanged("IsHistory");
            }
        }

        private Visibility _isTrafficRecord;
        public Visibility IsTrafficRecord
        {
            get
            {
                return _isTrafficRecord;
            }
            set
            {
                _isTrafficRecord = value;
                NotifyPropertyChanged("IsTrafficRecord");
            }
        }

        private bool _isMapSelected;
        public bool IsMapSelected
        {
            get
            {
                return _isMapSelected;
            }
            set
            {
                _isMapSelected = value;
                NotifyPropertyChanged("IsMapSelected");
            }
        }
        private bool _isCollageSelected;
        public bool IsCollageSelected
        {
            get
            {
                return _isCollageSelected;
            }
            set
            {
                _isCollageSelected = value;
                NotifyPropertyChanged("IsCollageSelected");
            }
        }
        private bool _isTrackerSelected;
        public bool IsTrackerSelected
        {
            get
            {
                return _isTrackerSelected;
            }
            set
            {
                _isTrackerSelected = value;
                NotifyPropertyChanged("IsTrackerSelected");
            }
        }
        private bool _isTrafficRecordSelected;
        public bool IsTrafficRecordSelected
        {
            get
            {
                return _isTrafficRecordSelected;
            }
            set
            {
                _isTrafficRecordSelected = value;
                NotifyPropertyChanged("IsTrafficRecordSelected");
            }
        }

        private bool _isHistorySelected;
        public bool IsHistorySelected
        {
            get
            {
                return _isHistorySelected;
            }
            set
            {
                _isHistorySelected = value;
                NotifyPropertyChanged("IsHistorySelected");
            }
        }

    }
    public class MainWindowMenuViewModel : MainWindowMenuModel
    {
        public MainMenuIndex SelectionIndex { get; set; } 
        public MainWindow _mainWin = (MainWindow)Application.Current.MainWindow;
        private MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand MapSelectedCommand { get; set;}
        public DelegateCommand CollageSelectedCommand { get; set; }
        public DelegateCommand TrackSelectedCommand{ get; set; }
        public DelegateCommand HistorySelectedCommand { get; set; }
        public DelegateCommand TrafficRecordSelectedCommand { get; set; }

        private void CommandInit()
        {
            MapSelectedCommand = new DelegateCommand();
            MapSelectedCommand.ExecuteCommand = new Action<object>(MapSelected);
            CollageSelectedCommand = new DelegateCommand();
            CollageSelectedCommand.ExecuteCommand = new Action<object>(CollageSelected);
            TrackSelectedCommand = new DelegateCommand();
            TrackSelectedCommand.ExecuteCommand = new Action<object>(TrackSelected);
            HistorySelectedCommand = new DelegateCommand();
            HistorySelectedCommand.ExecuteCommand = new Action<object>(HistorySelected);
            TrafficRecordSelectedCommand = new DelegateCommand();
            TrafficRecordSelectedCommand.ExecuteCommand = new Action<object>(TrafficRecordSelect);
        }
        public MainWindowMenuViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            HiddenPanel();
            CommandInit();
            ShowMenu();
            LoadMenu();
            SelectionIndex = MainMenuIndex.Map;
            SetSelection(SelectionIndex);
        }

        public void SetSelection(MainMenuIndex index)
        {
            switch (index)
            {
                case MainMenuIndex.Map:
                    IsMapSelected = true;
                    break;
                case MainMenuIndex.Collage:
                    IsCollageSelected = true;
                    break;
                case MainMenuIndex.Track:
                    IsTrackerSelected = true;
                    break;
                case MainMenuIndex.History:
                    IsHistorySelected = true;
                    break;
                case MainMenuIndex.TrafficRecord:
                    IsTrafficRecordSelected = true;
                    break;
                default:
                    IsMapSelected = true;
                    break;
            }
        }

        private void LoadMenu()
        {
            HiddenAllSelection();
            if (GlobalVariable.CurrenClientConfig == null)
            {
                MessageBox.Show("读取客户端配置失败，请检查配置端设置！","错误");
                return;
            }
            string menu = GlobalVariable.CurrenClientConfig.menu;
            if (menu == "AL")
            {
                string[] arr = { "1", "2", "3", "4", "5" };
                foreach (var item in arr)
                {
                    int selectid = int.Parse(item);
                    ShowSelection(selectid);
                }
            }
            else if (menu == "GAS")
            {
                string[] arr = { "1", "2", "3", "4", "5" };
                foreach (var item in arr)
                {
                    int selectid = int.Parse(item);
                    ShowSelection(selectid);
                }
            }
            else
            {
                string[] arr = menu.Split(',');
                foreach (var item in arr)
                {
                    if (item != null)
                    {
                        int selectid = int.Parse(item);
                        ShowSelection(selectid);
                    }
                }
            }
        }

        private void HiddenAllSelection()
        {
            IsMap = Visibility.Collapsed;
            IsCollage = Visibility.Collapsed;
            IsTracker = Visibility.Collapsed;
            IsHistory = Visibility.Collapsed;
            IsTrafficRecord= Visibility.Collapsed;

            mainWindowViewModel.IsShowCollageButton = IsCollage;
        }

        private void ShowSelection(int selectid)
        {
            switch (selectid)
            {
                case 1:
                    IsMap = Visibility.Visible;
                    break;
                case 2:
                    IsCollage = Visibility.Visible;
                    mainWindowViewModel.IsShowCollageButton = IsCollage;
                    break;
                case 3:
                    IsTracker = Visibility.Visible;
                    break;
                case 4:
                    IsHistory = Visibility.Visible;
                    break;
                case 5:
                    IsTrafficRecord  = Visibility.Visible;
                    break;
            }
        }
        
        #region IsShowing
        public void ShowMenu()
        {
            IsOpened = false;
            IsVisbility = Visibility.Visible;
        }
        public void HiddenMenu()
        {
            IsOpened = false;
            IsVisbility = Visibility.Collapsed;
        }

        #endregion

        /// <summary>
        /// 打开电子地图
        /// </summary>
        /// <param name="obj"></param>
        private void MapSelected(object obj)
        {
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/MainPage.xaml";
            SelectionIndex = MainMenuIndex.Map;
            ShowMenu();
        }
        /// <summary>
        /// 打开拼控
        /// </summary>
        /// <param name="obj"></param>
        private void CollageSelected(object obj)
        {
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/CollagePage.xaml";
            SelectionIndex = MainMenuIndex.Collage;
            ShowMenu();
        }
        /// <summary>
        /// 打开车辆追踪
        /// </summary>
        /// <param name="obj"></param>
        private void TrackSelected(object obj)
        {
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/TrackPage.xaml";
            SelectionIndex = MainMenuIndex.Track;
            ShowMenu();
        }
        /// <summary>
        /// 打开历史事件
        /// </summary>
        /// <param name="obj"></param>
        private void HistorySelected(object obj)
        {
            mainWindowViewModel.EventHistoryWin = new EventHistory();
            mainWindowViewModel.EventHistoryWin.Topmost = true;
            mainWindowViewModel.EventHistoryWin.Owner = _mainWin;
            mainWindowViewModel.EventHistoryWin.WindowStartupLocation = WindowStartupLocation.Manual;
            mainWindowViewModel.EventHistoryWin.Left = 23;
            mainWindowViewModel.EventHistoryWin.Top = 165;
            mainWindowViewModel.EventHistoryWin.ShowDialog();
        }
        /// <summary>
        /// 交通违规记录
        /// </summary>
        /// <param name="obj"></param>
        private void TrafficRecordSelect(object obj)
        {
            mainWindowViewModel.TrafficWinow = new TrafficWin();
            mainWindowViewModel.TrafficWinow.Topmost = true;
            mainWindowViewModel.TrafficWinow.Owner = _mainWin;
            mainWindowViewModel.TrafficWinow.WindowStartupLocation = WindowStartupLocation.Manual;
            mainWindowViewModel.TrafficWinow.Left = 23;
            mainWindowViewModel.TrafficWinow.Top = 165;
            mainWindowViewModel.TrafficWinow.ShowDialog();
        }
    }
}
