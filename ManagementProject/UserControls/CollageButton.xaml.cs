using ManagementProject.Helper;
using System.Windows.Controls;

namespace ManagementProject.UserControls
{
    #region CollageButton
    /// <summary>
    /// CollageButton.xaml 的交互逻辑
    /// </summary>
    public partial class CollageButton : UserControl
    {
        #region Private Field
        private CollageButtonViewModel _collageButtonViewModel;
        #endregion

        #region Constructor method
        public CollageButton()
        {
            InitializeComponent();
            _collageButtonViewModel = new CollageButtonViewModel();
            DataContext = _collageButtonViewModel;
        }
        #endregion
    } 
    #endregion

    #region ViewModel
    public class CollageButtonViewModel : CollageButtonModel
    {
        public CollageButtonViewModel()
        {
            AlarmScreenControl = GlobalVariable.IsPushAlarmWindow;
            ScreenState = "投屏已关闭";
            AlarmScreenState = "报警投屏已开启";
        }
    }
    #endregion

    #region Model
    public class CollageButtonModel : INotifyPropertyChangedClass
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
                OperateScreen(value, ScreenType.Screen);
            }
        }

        /// <summary>
        /// 操作投屏
        /// </summary>
        /// <param name="isOpen">true 打开投屏,false 关闭投屏</param>
        /// <param name="screenType">投屏类型</param>
        private void OperateScreen(bool isOpen, ScreenType screenType)
        {
            if (screenType == ScreenType.Screen)
            {
                ScreenState = isOpen ? "投屏已开启" : "投屏已关闭";
                GlobalVariable.IsPushWindow = isOpen;
                if (isOpen)
                    PinkongHelper.ClientUpWall();
                else
                    PinkongHelper.ClientDownWall();
            }
            else
            {
                AlarmScreenState = isOpen ? "报警投屏已开启" : "报警投屏已关闭";
                GlobalVariable.IsPushAlarmWindow = isOpen;
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
                OperateScreen(value, ScreenType.AlarmScreen);
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
    #endregion

    #region Screen Type

    /// <summary>
    /// 投屏类型
    /// </summary>
    public enum ScreenType
    {
        /// <summary>
        /// 投屏
        /// </summary>
        Screen,

        /// <summary>
        /// 报警投屏
        /// </summary>
        AlarmScreen,
    }

    #endregion
}
