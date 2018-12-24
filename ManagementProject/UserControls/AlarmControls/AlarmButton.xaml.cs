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
        private string _alarmIcon;
        public string AlarmIcon
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
                AlarmTypeInit();
            }
        }

        private void AlarmTypeInit()
        {
            switch (AlarmType)
            {
                case AlarmType.CarAlarm:
                    AlarmIcon = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/车辆违停.png";
                    break;
                case AlarmType.FireAlarm:
                    AlarmIcon = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/火灾报警.png";
                    break;
                case AlarmType.WaterAlarm:
                    AlarmIcon = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/水压报警.png";
                    break;
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
            mainWindowViewModel.PageUrl = "/ManagementProject;component/PageView/AlarmPage.xaml";
        }

        
    }

}
