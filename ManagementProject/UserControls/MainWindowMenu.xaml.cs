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
        public MainWindowMenuViewModel MainWindowMenuViewModel;
        public MainWindowMenu()
        {
            
            InitializeComponent();
            MainWindowMenuViewModel = new MainWindowMenuViewModel();
            DataContext = MainWindowMenuViewModel;
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
            }
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

        private Visibility _isShowPanel;
        public Visibility IsShowPanel
        {
            get
            {
                return _isShowPanel;
            }
            set
            {
                _isShowPanel = value;
                NotifyPropertyChanged("IsShowPanel");
            }
        }

        
    }
    public class MainWindowMenuViewModel : MainWindowMenuModel
    {
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
        public MainWindowMenuViewModel()
        {

            HiddenPanel();
            CommandInit();

        }
        #region OpenClick
        private void OpenClick(object obj)
        {
            if (IsOpened == false)
            {

                ShowPanel();
            }
            else
            {

                HiddenPanel();
            }
        }

        private void ShowPanel()
        {
            MenubtIcon = "/ManagementProject;component/ImageSource/Icon/menuicon/menuopen.png";
            IsShowPanel = Visibility.Visible;
            IsOpened = true;
        }
        private void HiddenPanel()
        {
            MenubtIcon = "/ManagementProject;component/ImageSource/Icon/menuicon/menuclose.png";
            IsShowPanel = Visibility.Collapsed;
            IsOpened = false;
        }
        #endregion

        private void MapSelected(object obj)
        {

        }
        private void CollageSelected(object obj)
        {

        }

        private void TrackSelected(object obj)
        {

        }

        private void HistorySelected(object obj)
        {

        }



    }
}
