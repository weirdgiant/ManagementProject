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
    /// CollageButton.xaml 的交互逻辑
    /// </summary>
    public partial class CollageButton : UserControl
    {
        private CollageButtonViewModel _collageButtonViewModel;
        public CollageButton()
        {
            InitializeComponent();
            _collageButtonViewModel = new CollageButtonViewModel();
            DataContext = _collageButtonViewModel;
        }
    }
    public class CollageButtonModel:INotifyPropertyChangedClass 
    {
        private bool _isOpen;
        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                _isOpen = value;
                NotifyPropertyChanged("IsOpen");
            }
        }

        private bool _screenControl;
        /// <summary>
        /// 投屏开关
        /// </summary>
        public bool ScreenControl
        {
            get
            {
                return _screenControl;
            }
            set
            {
                _screenControl = value;
                NotifyPropertyChanged("ScreenControl");
                if (value)
                {
                    ScreenState = "投屏已开启";
                }else
                {
                    ScreenState = "投屏已关闭";
                }
            }
        }

        private string _screenState;
        public string ScreenState
        {
            get
            {
                return _screenState;
            }
            set
            {
                _screenState = value;
                NotifyPropertyChanged("ScreenState");
            }
        }

        private bool _alarmScreenControl;
        /// <summary>
        /// 报警投屏开关
        /// </summary>
        public bool AlarmScreenControl
        {
            get
            {
                return _alarmScreenControl;
            }
            set
            {
                _alarmScreenControl = value;
                NotifyPropertyChanged("AlarmScreenControl");
                if (value)
                {
                    AlarmScreenState = "报警投屏已开启";
                }
                else
                {
                    AlarmScreenState = "报警投屏已关闭";
                }
            }
        }

        private string _alarmScreenState;
        public string AlarmScreenState
        {
            get
            {
                return _alarmScreenState;
            }
            set
            {
                _alarmScreenState = value;
                NotifyPropertyChanged("AlarmScreenState");
            }
        }
    }
    public class CollageButtonViewModel:CollageButtonModel
    {
        public CollageButtonViewModel()
        {
            ScreenState = "投屏已关闭";
            AlarmScreenState = "报警投屏已关闭";
        }
    }
}
