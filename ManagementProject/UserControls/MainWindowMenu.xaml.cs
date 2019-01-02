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
        public MainWindowMenu()
        {            
            InitializeComponent();           
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
                if (IsOpened == true)
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
    }
    public class MainWindowMenuViewModel : MainWindowMenuModel
    {
        private MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand OpenClickCommand { get; set; }
        public DelegateCommand MapSelectedCommand { get; set;}
        public DelegateCommand CollageSelectedCommand { get; set; }
        public DelegateCommand TrackSelectedCommand{ get; set; }
        public DelegateCommand HistorySelectedCommand { get; set; }

        private void CommandInit()
        {
            OpenClickCommand = new DelegateCommand();
            OpenClickCommand.ExecuteCommand = new Action<object>(OpenClick);
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
        }
        #region IsShowing
        private void OpenClick(object obj)
        {
            
        }
        public void ShowMenu()
        {
            IsVisbility = Visibility.Visible;
        }
        public void HiddenMenu()
        {
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
            newwindow.Topmost = true;
            newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
            newwindow.Left = 23;
            newwindow.Top = 165;
            newwindow.Show();
        }



    }
}
