using ManagementProject.PageView;
using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject
{
    public static class GlobalVariable
    {
        /// <summary>
        /// 当前客户端配置信息
        /// </summary>
        public static ClientConfig CurrenClientConfig { get; set; }
        /// <summary>
        /// 当前选中地图ID
        /// </summary>
        public static int CurrentMapId;

        private static int _selectedSchoolId;
        /// <summary>
        /// 当前选择校区ID
        /// </summary>
        public static int SelectedSchoolId
        {
            get
            {
                return _selectedSchoolId;
            }
            set
            {
                _selectedSchoolId = value;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)main.DataContext;
                mainWindowViewModel.mainPageViewModel.Sid = value;
            }
        }
        //ControlStatus
        private static bool _isMapLoaded;
        /// <summary>
        /// 地图加载完成
        /// </summary>
        public static bool IsMapLoaded
        {
            get
            {
                return _isMapLoaded;
            }
            set
            {
                _isMapLoaded = value;
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.IsLoadMainMenu = value;
            }
        }
        /// <summary>
        /// 指示当前页面是否是活动界面
        /// </summary>
        public static bool IsTrackPage
        {
            set
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.IsTrackPage = value;
            }
        }
        /// <summary>
        /// 指示当前页面是否是报警界面
        /// </summary>
        public static bool IsAlarmPage
        {
            set
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.IsAlarmPage = value;
            }
        }

        //WindowStatus
        public static bool PlayerWindowIsOpened { get; set; }


        //PageStatus
        /// <summary>
        /// 报警页面类型
        /// </summary>
        public static AlarmType AlarmPageType;
    }
    #region 枚举类

    /// <summary>
    /// 播放窗口类型
    /// </summary>
    public enum PlayerWindowType
    {
        /// <summary>
        /// 回放
        /// </summary>
        Playerback,
        /// <summary>
        /// 追踪
        /// </summary>
        Track
    }
    /// <summary>
    /// 播放器状态
    /// </summary>
    public enum PlayerControlType
    {
        /// <summary>
        /// 播放
        /// </summary>
        Playback,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 结束
        /// </summary>
        End
    }

    /// <summary>
    /// 主菜单选择状态
    /// </summary>
    public enum MainMenuConfig
    {
        /// <summary>
        /// 电子地图
        /// </summary>
        Map,
        /// <summary>
        /// 拼控
        /// </summary>
        Collage,
        /// <summary>
        /// 车辆追踪
        /// </summary>
        Track,
        /// <summary>
        /// 历史事件
        /// </summary>
        History
    }
    /// <summary>
    /// 报警类型
    /// </summary>
    public enum AlarmType
    {
        /// <summary>
        /// 火灾警报
        /// </summary>
        FireAlarm,
        /// <summary>
        /// 水压警报
        /// </summary>
        WaterAlarm,
        /// <summary>
        /// 车辆警报
        /// </summary>
        CarAlarm
    }

    /// <summary>
    /// 播放器大小状态
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal,
        /// <summary>
        /// 最大化状态
        /// </summary>
        Max
    }
    #endregion
}
