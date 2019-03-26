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
            //menubt.Click += Menubt_Click;
            //mapbt.Click += Mapbt_Click;
            //collagebt.Click += Collagebt_Click;
            //carbt.Click += Carbt_Click;
            //historybt.Click += Historybt_Click;
            //Unloaded += MainWindowMenu_Unloaded;
        }

        private void MainWindowMenu_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            DataContext = MainWindowViewModel.mainWindowMenuViewModel ;
           // menubtimage.Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/menuicon/menuclose.png", UriKind.Relative));
        }

        #region WindowsFormsHost封装方法 白色背景无法去除 暂时弃用
        private void MainWindowMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            LoadCloseMenu();
        }
        private void Historybt_Click(object sender, RoutedEventArgs e)
        {
            HistorySelected();
            LoadCloseMenu();
        }

        private void Carbt_Click(object sender, RoutedEventArgs e)
        {
            TrackSelected();
            LoadCloseMenu();
        }

        private void Collagebt_Click(object sender, RoutedEventArgs e)
        {
            CollageSelected();
            LoadCloseMenu();
        }

        private void Mapbt_Click(object sender, RoutedEventArgs e)
        {
            MapSelected();
            LoadCloseMenu();
        }

       
        private void Menubt_Click(object sender, RoutedEventArgs e)
        {
            if (pop.IsOpen)
            {
                LoadCloseMenu();
            }else
            {
                LoadOpenMenu();
            }
        }

        private void LoadOpenMenu()
        {
            menubtimage.Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/menuicon/menuopen.png", UriKind.Relative));
            pop.IsOpen = true;
        }
        private void LoadCloseMenu()
        {
            menubtimage.Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/menuicon/menuclose.png", UriKind.Relative));
            pop.IsOpen = false;
        }

        /// <summary>
        /// 打开电子地图
        /// </summary>
        /// <param name="obj"></param>
        private void MapSelected()
        {
            MainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/MainPage.xaml";
        }
        /// <summary>
        /// 打开拼控
        /// </summary>
        /// <param name="obj"></param>
        private void CollageSelected()
        {
            MainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/CollagePage.xaml";
        }
        /// <summary>
        /// 打开车辆追踪
        /// </summary>
        /// <param name="obj"></param>
        private void TrackSelected()
        {
            MainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/TrackPage.xaml";
        }
        /// <summary>
        /// 打开历史事件
        /// </summary>
        /// <param name="obj"></param>
        private void HistorySelected()
        {
            EventHistory newwindow = new EventHistory();
            newwindow.Topmost = true;
            newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newwindow.Left = 23;
            newwindow.Top = 165;
            newwindow.Show();
        }

        private void ShowMenu()
        {
           // host.Visibility = Visibility.Visible;
        }
        private void HiddenMenu()
        {
           // host.Visibility = Visibility.Hidden;
        }
        #endregion

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

    }
    public class MainWindowMenuViewModel : MainWindowMenuModel
    {
        private MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand MapSelectedCommand { get; set;}
        public DelegateCommand CollageSelectedCommand { get; set; }
        public DelegateCommand TrackSelectedCommand{ get; set; }
        public DelegateCommand HistorySelectedCommand { get; set; }

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
        }
        public MainWindowMenuViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            HiddenPanel();
            CommandInit();
            ShowMenu();
            LoadMenu();
        }

        private void LoadMenu()
        {
            HiddenAllSelection();
            string menu = GlobalVariable.CurrenClientConfig.menu;
            string[] arr = menu.Split(',');
            foreach (var item in arr)
            {
                int selectid = int.Parse(item);
                ShowSelection(selectid);
            }
        }

        private void HiddenAllSelection()
        {
            IsMap = Visibility.Collapsed;
            IsCollage = Visibility.Collapsed;
            IsTracker = Visibility.Collapsed;
            IsHistory = Visibility.Collapsed;
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
                    break;
                case 3:
                    IsTracker = Visibility.Visible;
                    break;
                case 4:
                    IsHistory = Visibility.Visible;
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
            ShowMenu();
        }
        /// <summary>
        /// 打开拼控
        /// </summary>
        /// <param name="obj"></param>
        private void CollageSelected(object obj)
        {
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/CollagePage.xaml";
            ShowMenu();
        }
        /// <summary>
        /// 打开车辆追踪
        /// </summary>
        /// <param name="obj"></param>
        private void TrackSelected(object obj)
        {
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/TrackPage.xaml";
            ShowMenu();
        }
        /// <summary>
        /// 打开历史事件
        /// </summary>
        /// <param name="obj"></param>
        private void HistorySelected(object obj)
        {
            EventHistory  newwindow = new EventHistory();
            //newwindow.Topmost = true;
            newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newwindow.Left = 23;
            newwindow.Top = 165;
            newwindow.ShowDialog();
        }



    }
}
