using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject
{
    public static class GlobalVariable
    {
        //ControlStatus
        private static MainMenu _mainMenuSelectedIndex;
        public static MainMenu MainMenuSelectedIndex
        {
            get
            {
                return _mainMenuSelectedIndex;
            }
            set
            {
                _mainMenuSelectedIndex = value;
                Changed("MainMenuSelectedIndex");
            }
        }

        public static void Changed(string AAA)
        {

        }

        //WindowStatus
        public static bool PlayerWindowIsOpened { get; set; }


        //PageStatus
    }

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
    public enum PlayerControlTape
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
    public enum MainMenu
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
}
