﻿using System;
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
