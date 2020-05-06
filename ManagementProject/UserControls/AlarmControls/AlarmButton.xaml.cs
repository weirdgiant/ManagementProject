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
    /// AlarmButton.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmButton : UserControl
    {
        public AlarmButton()
        {
            InitializeComponent();           
        }
    }
    public class AlarmButtonModel : INotifyPropertyChangedClass
    {
        private string _alarmCount;
        public string AlarmCount
        {
            get
            {
                return _alarmCount;
            }
            set
            {
                _alarmCount = value;
                NotifyPropertyChanged("AlarmCount");
            }
        }
        private BitmapImage _alarmIcon;
        public BitmapImage AlarmIcon
        {
            get
            {
                return _alarmIcon;
            }
            set
            {
                _alarmIcon = value;
                NotifyPropertyChanged("AlarmIcon");
            }
        }

        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

        private AlarmType _alarmType;
        public  AlarmType AlarmType
        {
            get
            {
                return _alarmType;
            }
            set
            {
                _alarmType = value;
                NotifyPropertyChanged("AlarmType");
            }
        }

    }
    public class AlarmButtonViewModel : AlarmButtonModel
    {
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand AlarmPageInitCommand { get; set; }
        public AlarmButtonViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            mainWindowViewModel = _mainWindowViewModel;
            AlarmPageInitCommand = new DelegateCommand();
            AlarmPageInitCommand.ExecuteCommand = new Action<object>(AlarmPageInit);
        }

        private void AlarmPageInit(object obj)
        {
            GlobalVariable.AlarmPageType = Type.Trim();// AlarmType;
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/AlarmPage.xaml";           
            mainWindowViewModel.mainWindowMenuViewModel.HiddenMenu();
        }

        
    }

}
