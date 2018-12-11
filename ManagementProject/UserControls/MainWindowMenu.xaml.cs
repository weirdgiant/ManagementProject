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
        private bool isOpened;
        public bool IsOpened
        {
            get
            {
                return isOpened;
            }
            set
            {
                isOpened = value;
                NotifyPropertyChanged("IsOpened");
            }
        }

        private string menubtIcon;
        public string MenubtIcon
        {
            get
            {
                return menubtIcon;
            }
            set
            {
                menubtIcon = value;
                NotifyPropertyChanged("MenubtIcon");
            }
        }

        private Visibility isShowPanel;
        public Visibility IsShowPanel
        {
            get
            {
                return isShowPanel;
            }
            set
            {
                isShowPanel = value;
                NotifyPropertyChanged("IsShowPanel");
            }
        }
    }
    public class MainWindowMenuViewModel : MainWindowMenuModel
    {
        public DelegateCommand OpenClickCommand { get; set; }
        public MainWindowMenuViewModel()
        {

            HiddenPanel();

            OpenClickCommand = new DelegateCommand();
            OpenClickCommand.ExecuteCommand = new Action<object>(OpenClick);
        }

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
    }
}
