using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

using System.Reflection;
namespace ManagementProject
{
    public enum BAK_RES_STATUS_E : uint
    {
        BAK_RES_UNFORMATTED,
        BAK_RES_FORMATTING,
        BAK_RES_ONLINE,
        BAK_RES_OFFLINE,
        BAK_RES_STATUS_MAX,
        BAK_RES_STATUS_INVALID = 4294967295u
    }

    public enum CAMERA_TYPE_E : uint
    {
        [Description("标清固定摄像机")]
        CAMERA_TYPE_FIX = 1,            /**< 标清固定摄像机 */
        [Description("标清固定摄像机")]
        CAMERA_TYPE_PTZ = 2,            /**< 标清固定摄像机 */
        [Description("高清固定摄像机")]
        CAMERA_HD_TYPE_FIX = 3,            /**< 高清固定摄像机 */
        [Description("高清云台摄像机")]
        CAMERA_HD_TYPE_PTZ = 4,            /**< 高清云台摄像机 */
        [Description("车载摄像机")]
        CAMERA_TYPE_CAR = 5,            /**< 车载摄像机 */
        [Description("虚拟摄像机")]
        CAMERA_TYPE_VIRTUAL = 6,            /**< 虚拟摄像机 */
        [Description("不可控标清球机")]
        CAMERA_TYPE_BALL_NOT_CONTROL = 7,            /**< 不可控标清球机 */
        [Description("不可控标清球机")]
        CAMERA_HD_TYPE_BALL_NOT_CONTROL = 8,            /**< 不可控标清球机 */

        /* BEGIN: Added by kf0092 for 安全接入外域, 2013年3月22日 */
        [Description("VM安全接入摄像机")]
        CAMERA_TYPE_SAFE_VM = 9,            /**< VM安全接入摄像机 */
        [Description("DVR安全接入摄像机")]
        CAMERA_TYPE_SAFE_DVR = 10,           /**< DVR安全接入摄像机 */
        [Description("矩阵安全接入摄像机")]
        CAMERA_TYPE_SAFE_MATRIX = 11,           /**< 矩阵安全接入摄像机 */

        /* END  : Added by kf0092 for 安全接入外域, 2013年3月22日 */
        [Description("变焦枪机")]
        CAMERA_TYPE_FIX_BOX = 12,                /**<变焦枪机 */

        /* Begin added by y01359, 2014-12-11 for A8 */
        [Description("VGA输入摄像机")]
        CAMERA_TYPE_VGA = 13,                   /**< VGA输入摄像机 */
        /* End added by y01359, 2014-12-11 for A8 */
        [Description("摄像机类型枚举最大值")]
        CAMERA_TYPE_MAX,                        /**< 摄像机类型枚举最大值 */
        [Description("无效值")]
        CAMERA_TYPE_INVALID = 0xFFFFFFFF    /**< 无效值 */
    }
    public enum CS_OPERATE_CODE_E
    {
        USER_OPERATE_SERVICE = 0,                               /**< 用户主动发起操作 */

        /*****************服务器主动发起的动作类型(开始)*******************/

        SWITCH_OPERATE = 1,    /**< 轮切操作 */
        PLAN_SWITCH_OPERATE = 2,    /**< 计划轮切操作 */
        SERVICE_REAVE = 3,    /**< 业务抢占 */
        ALARM_LINKAGE_START_SERVICE = 4,    /**< 告警联动 */
        EXT_DOMAIN_OPER_SERVICE = 5,    /**< 外域发起操作 */

        EC_ONLINE = 6,    /**< EC上线 */
        EC_OFFLINE = 7,    /**< EC下线 */
        EC_DELETE = 8,    /**< EC删除 */
        CAMERA_ONLINE = 9,    /**< 摄像机上线 */
        CAMERA_OFFLINE = 10,   /**< 摄像机下线 */

        DC_ONLINE = 11,   /**< DC上线 */
        DC_OFFLINE = 12,   /**< DC下线 */
        DC_DELETE = 13,   /**< DC删除 */

        MS_ONLINE = 14,   /**< MS上线 */
        MS_OFFLINE = 15,   /**< MS下线 */
        MS_OFFLINE_TRANSFER = 16,   /**< MS下线引起的转移 */
        MS_DELETE_TRANSFER = 17,   /**< MS删除引起的转移 */
        MS_DELETE = 18,   /**< MS删除 */

        VX500_ONLINE = 19,   /**< VX500上线 */
        VX500_OFFLINE = 20,   /**< VX500下线 */
        VX500_DELETE = 21,   /**< VX500删除 */

        ISC_ONLINE = 22,   /**< ISC3000-E上线 */
        ISC_OFFLINE = 23,   /**< ISC3000-E下线 */

        EX_DOMAIN_JUNIOR_ONLINE = 24,   /**< 下级域上线 */
        EX_DOMAIN_SUPERIOR_ONLINE = 25,   /**< 上级域上线 */
        EX_DOMAIN_JUNIOR_OFFLINE = 26,   /**< 下级域下线 */
        EX_DOMAIN_SUPERIOR_OFFLINE = 27,   /**< 上级域下线 */
        EXT_DOMAIN_DELETE = 28,   /**< 外域删除 */

        EXT_DOM_CANCEL_SHR_CAM = 29,   /**< 外域取消共享摄像机给本域 */
        LOC_DOM_CANCEL_SHR_CAM = 30,   /**< 本域取消共享摄像机给外域 */

        CAMERA_DELETE = 31,   /**< CAMERA删除 */
        MONITOR_DELETE = 32,   /**< MONITOR删除 */

        SWITCH_RES_MDF = 33,   /**< 轮切资源修改 */
        SWITCH_RES_DEL = 34,   /**< 轮切资源删除 */
        SWITCH_PLAN_DEL = 35,   /**< 轮切计划删除 */
        SWITCH_PLAN_MODIFY = 36,   /**< 轮切计划修改 */

        XP_ONLINE = 37,   /**< XP上线 */
        XP_OFFLINE = 38,   /**< XP下线 */
        USER_KEEPALIVE_FAIL = 39,   /**< 用户保活失败 */
        USER_DELETE = 40,   /**< 用户被删除 */
        USER_QUIT = 41,   /**< 用户退出 */
        USER_KICKED_OUT = 42,   /**< 用户被强制下线 */

        SYSMANGER_OPERATE_SERVICE = 43,   /**< 管理员操作 */
        PTZ_LINK_TIMER_OUT = 44,   /**< 云台连接超时 */
        PTZ_FIRST_LINK = 45,   /**< 云台首次连接 */
        PTZ_LOW_AUTHORITY = 46,   /**< 云台控制权限低 */
        PTZ_NO_AUTHORITY = 47,   /**< 无云台控制权限 */
        PTZ_HAS_LOCKED = 48,   /**< 云台被锁定 */

        DEV_MEDIA_PARAM_CHANGE = 49,   /**< 设备媒体参数修改 */

        SALVO_OPERATE = 50,   /**< 组显示操作 */

        SALVO_RES_DEL = 51,   /**< 组显示删除 */
        SALVO_RES_MDF = 52,   /**< 组显示修改 */

        GROUPSWITCH_RES_DEL = 53,   /**< 组轮巡资源删除 */
        GROUPSWITCH_RES_MDF = 54,   /**< 组轮巡资源修改 */

        GROUPSWITCH_PLAN_DEL = 55,   /**< 组轮巡计划删除 */
        GROUPSWITCH_PLAN_MODIFY = 56,   /**< 组轮巡计划修改 */

        GROUPSALVO_OPERATE = 57,   /**< 组轮巡操作 */
        GROUPSALVO_STOP = 58,   /**< 组轮巡停止 */
        GROUPSALVO_PLAN_OPERATE = 59,   /**< 组轮巡计划操作 */

        INTERNAL_ERR_OPERATE = 60,   /**< 内部错误引起操作 */

        PTZ_CCB_FULL = 61,   /**< 控制块已满 */

        MONITOR_SPLIT_SCREEN_DELETE = 62,   /**< 监视器分屏删除 */
        MONITOR_SPLIT_SCREEN_SWITCH = 63, /**< 监视器分屏切换 */

        ALARM_LINKAGE_RESUME_SERVICE = 64,  /**< 告警联动恢复 */
        MONITOR_SPLIT_SCREEN_SWITCH_STOP = 65, /**< 监视器分屏切换引起的"停止业务"操作 */
        MONITOR_SPLIT_SCREEN_SWITCH_START = 66, /**< 监视器分屏切换引起的"启动业务"操作 */
        MONITOR_SPLIT_SCREEN_SWITCH_START_FULL = 67, /**< 监视器分屏切换引起的"启动业务"操作 - 切至全屏 */
        MONITOR_SPLIT_SCREEN_SWITCH_START_EXIT = 68, /**< 监视器分屏切换引起的"启动业务"操作 - 退出全屏 */

        CALLEE_USER_OPERATE_SERVICE = 70,   /**< 被叫用户发起操作 */
        SYSTEM_OPERATE_SERVICE = 71,   /**< 系统发起操作 */
        CALLEE_NOT_SUPPORT_SERVICE = 72,   /**< 被叫不支持此业务操作 */

        EXDOMAIN_CRUISE_PATH_DELETE = 73,   /**< 跨域巡航路线删除 */

        TS_OFFLINE = 74,   /**< TS下线 */
        TS_OFFLINE_TRANSFER = 75,   /**< TS下线引起的转移 */
        TS_DELETE_TRANSFER = 76,   /**< TS删除引起的转移 */
        TS_DELETE = 77,   /**< TS删除 */
        TS_ONLINE = 78,   /**< TS上线 */
        VOD_OFFLINEORDELETE = 79,   /**< VOD服务器离线或删除 */
        /* Begin Added by dengshuhua00673, 2012-11-27 of 自动布局轮巡 */
        AUTOSALVO_OPERATE = 80,   /**< 自动布局组轮巡操作 */
        AUTOSALVO_STOP = 81,   /**< 自动布局组轮巡停止 */
        AUTOSWITCH_RES_MDF = 82,   /**< 自动布局组轮巡资源修改 */
        AUTOSWITCH_RES_DEL = 83,   /**< 自动布局组轮巡资源删除 */
        /* End Added by dengshuhua00673, 2012-11-27 of 自动布局轮巡 */

        DC_CONFIGURE_DC_INST = 84,   /**< 设置DC拼接、模拟输入等信息 */

        /* Begin: added by zkf0134, 2013.11.23 for 分屏切换支持主辅流自适应*/
        MONITOR_SWITCH_SPLIT_SCREEN_START = 85,   /**< 监视器分屏切换引起的"启动业务操作"*/
        /* End: added by zkf0134, 2013.11.23 for 分屏切换支持主辅流自适应*/

        PLAN_GUARD_OPERATE = 86,/**<计划看守操作*/
        LS_OFFLINE = 87,   /**< LS下线 */
        LS_OFFLINE_TRANSFER = 88,   /**< LS下线引起的转移 */
        LS_DELETE_TRANSFER = 89,   /**<LS删除引起的转移 */
        LS_DELETE = 90,   /**< LS删除 */
        LS_ONLINE = 91,   /**< LS上线 */

        VOD_ONLINE = 92,   /**< VOD上线 */
        VOD_OFFLINE = 93,   /**< VOD下线 */
        VOD_OFFLINE_TRANSFER = 94,   /**< VOD下线引起的转移 */
        VOD_DELETE_TRANSFER = 95,   /**< VOD删除引起的转移 */
        VOD_DELETE = 96,   /**<VOD删除 */

        TS_RULE_CHANGE = 97,   /**< TS规则改变 */
        /* Begin: added by y01359, 2015-01-28 for A8 */
        A8_ONLINE = 98,  /**< A8上线 */
        A8_OFFLINE = 99,  /**< A8下线 */
        A8_DELETE = 100,  /**< A8删除 */
        A8_WINDOW_DELETE = 101,  /**< A8窗口删除 */
        /* End: added by y01359, 2015-01-28 for A8 */

        /*****************服务器主动发起的动作类型(结束)*******************/

        CS_OPERATE_CODE_MAX,
        CS_OPERATE_CODE_INVALID = 0xFF
    }

    public enum AlARM_TYPE_E
    {
        /** MIB告警 1~200 */
        AlARM_TYPE_HIGH_TEMPERATURE = 1,    /**< 高温告警 */
        AlARM_TYPE_LOW_TEMPERATURE = 2,    /**< 低温告警 */
        AlARM_TYPE_TEMPERATURE_RESUME = 3,    /**< 温度告警恢复 */
        AlARM_TYPE_FAN_FAULT = 4,    /**< 风扇故障告警 */
        AlARM_TYPE_FAN_FAULT_RESUME = 5,    /**< 风扇故障告警恢复 */
        ALARM_TYPE_FLASH_OPERATE = 6,    /**< Flash操作告警 */
        ALARM_TYPE_CPU_EXCEED = 7,    /**< CPU利用率过高告警 */
        ALARM_TYPE_MEMOY_EXCEED = 8,    /**< 内存利用率过高告警 */
        ALARM_TYPE_REBOOT = 9,    /**< 设备重启 */
        ALARM_TYPE_DEVICE_CONFIG_CHANGE = 10,   /**< 设备配置变更 */
        AlARM_TYPE_DISK_ERR = 11,   /**< 磁盘故障 */
        AlARM_TYPE_DISK_ERR_RECOVER = 12,   /**< 磁盘故障恢复 */
        AlARM_TYPE_DISK_POWER_ON = 13,   /**< 磁盘上线 */
        AlARM_TYPE_DISK_POWER_OFF = 14,   /**< 磁盘下线 */
        AlARM_TYPE_RAID_EXCEPTION = 15,   /**< 阵列异常告警 */
        AlARM_TYPE_RAID_REBUILD_START = 16,   /**< 进入重建状态 */
        AlARM_TYPE_RAID_REBUILD_FINISH = 17,   /**< 退出重建 */
        AlARM_TYPE_STP_EXEC_EXCEPTION = 18,   /**< 未按计划录像告警 */
        AlARM_TYPE_STP_EXEC_RECOVER = 19,   /**< 未按计划录像恢复告警 */
        AlARM_TYPE_IPSAN_NO_ACCESS = 20,   /**< 无法访问IPSAN存储设备告警 */
        AlARM_TYPE_IPSAN_NO_ACCESS_RECOVER = 21,   /**< 无法访问IPSAN存储设备恢复告警 */
        AlARM_TYPE_STORED_DATA_READ_ERR = 22,   /**< 读取存储数据失败 */
        AlARM_TYPE_STORED_DATA_SEEK_ERR = 23,   /**< 定位存储设备失败 */
        AlARM_TYPE_VOD_OVER_THRESHOLD = 24,   /**< 点播路数超出阈值 */
        AlARM_TYPE_VOD_UNDER_THRESHOLD = 25,   /**< 点播流数量恢复 */
        ALARM_TYPE_TEMPERATURE = 26,   /**< 温度告警 */
        AlARM_TYPE_CAM_BAK_OWN_UNDER_THR = 27,   /**< 摄像机独享备份资源容量使用达到阈值恢复 */
        AlARM_TYPE_CAM_BAK_SHARD_OVER_THR = 28,   /**< 摄像机共享备份资源容量使用达到阈值 */
        AlARM_TYPE_CAM_BAK_SHARD_UNDER_THR = 29,   /**< 摄像机共享备份资源容量使用达到阈值恢复 */
        AlARM_TYPE_CAM_BAK_OWN_CAP_SCANT = 30,   /**< 摄像机独享备份资源配置满停止策略时，资源剩余容量不足 */
        AlARM_TYPE_CAM_BAK_OWN_CAP_ENOUGH = 31,   /**< 摄像机独享备份资源配置满停止策略时，资源剩余容量不足恢复 */
        AlARM_TYPE_BAK_RES_CAP_SCANT = 32,   /**< 全局备份配置满停止策略时，全局资源剩余容量不足 */
        AlARM_TYPE_BAK_RES_CAP_ENOUGH = 33,   /**< 全局备份配置满停止策略时，全局资源剩余容量不足恢复 */
        AlARM_TYPE_BAK_RES_ABNORMAL = 34,   /**< 备份资源异常 */
        AlARM_TYPE_BAK_RES_NORMAL = 35,   /**< 备份资源异常恢复 */
        AlARM_TYPE_BAK_FAILED = 36,   /**< 备份任务执行失败 */
        AlARM_TYPE_CAM_BAK_OWN_OVER_THR = 37,   /**< 摄像机独享备份资源容量使用达到阈值 */
        /* Begin: Added by mozhanfei(kf0149), 2013-9-9 for 新增异常恢复告警 */
        AlARM_TYPE_RAID_EXCEPTION_RECOVER = 38,   /**< 阵列异常恢复告警 */
        /* End: Added by mozhanfei(kf0149), 2013-7-18 for 新增异常恢复告警 */

        AlARM_TYPE_PREVENT_REMOVAL = 71,   /**< 防拆告警 */
        AlARM_TYPE_PREVENT_REMOVAL_RESUME = 72,   /**< 防拆告警恢复 */

        /* Begin added by baoyihui02795, 2011-04-28 of 卡口项目 */
        ALARM_TYPE_FLASHLIGHT_FAULT = 194,   /**< 闪光灯故障 */
        ALARM_TYPE_FLASHLIGHT_FAULT_RESUME = 195,   /**< 闪光灯故障恢复 */
        ALARM_TYPE_STOR_RES_ABNORMAL = 196,   /**< 存储资源异常 */
        ALARM_TYPE_STOR_RES_NORMAL = 197,   /**< 存储资源异常恢复 */
        ALARM_TYPE_COIL_DISABLED = 198,   /**< 线圈失效 */
        ALARM_TYPE_COIL_ENABLED = 199,   /**< 线圈失效恢复 */
        /* End added by baoyihui02795, 2011-04-28 of 卡口项目 */

        /** SIP告警 201~ */
        AlARM_TYPE_VIDEO_LOST = 201            ,  /**< 视频丢失告警 */
        AlARM_TYPE_VIDEO_LOST_RESUME = 202     ,  /**< 视频丢失告警恢复 */
        [Description("运动检测告警")]
        AlARM_TYPE_MOVE_DETECT = 203           ,  /**< 运动检测告警 */
        [Description("运动检测告警恢复")]
        AlARM_TYPE_MOVE_DETECT_RESUME = 204    ,  /**< 运动检测告警恢复 */
        [Description("遮挡侦测告警")]
        AlARM_TYPE_MASK_DETECT = 205           ,  /**< 遮挡侦测告警 */
        [Description("遮挡侦测告警恢复")]
        AlARM_TYPE_MASK_DETECT_RESUME = 206    ,  /**< 遮挡侦测告警恢复 */
        AlARM_TYPE_INPUT_SWITCH = 207          ,  /**< 输入开关量告警 */
        AlARM_TYPE_INPUT_SWITCH_RESUME = 208   ,  /**< 输入开关量告警恢复 */
        AlARM_TYPE_SHORT_CIRCUIT = 209         ,  /**< 开关量线路短路告警 */
        AlARM_TYPE_BREAKER_CIRCUIT = 210       ,  /**< 开关量线路断路告警 */
        AlARM_TYPE_SHORT_CIRCUIT_RESUME = 211  ,  /**< 开关量线路短路告警恢复 */
        AlARM_TYPE_STOR_FULL_PRE = 212         ,  /**< 存储即将满告警 */
        AlARM_TYPE_STOR_FULL = 213             ,  /**< 满存储告警 */
        AlARM_TYPE_STOR_FAILED = 214           ,  /**< 存储读写失败告警 */
        AlARM_TYPE_STOR_FAILED_RESUME = 215    ,  /**< 存储读写失败告警恢复 */
        AlARM_TYPE_DEVICE_ONLINE = 216         ,  /**< 设备上线告警 */
        AlARM_TYPE_DEVICE_OFFLINE = 217        ,  /**< 设备下线告警 */
        AlARM_TYPE_BREAKER_CIRCUIT_RESUME = 219,  /**< 开关量线路断路告警恢复 */

        AlARM_TYPE_STREAM_STOR_CAM_WARN = 222,  /**< 摄像机存储停止告警 */

        AlARM_TYPE_EXT_STOR_FULL_PRE = 223,  /**< 远端存储即将满告警 */
        AlARM_TYPE_EXT_STOR_FULL = 224,  /**< 远端存储满告警 */
        AlARM_TYPE_EXT_STOR_FAILED = 225,  /**< 远端存储读写失败告警 */
        AlARM_TYPE_EXT_STOR_FAILED_RESUME = 226,  /**< 远端存储读写失败告警恢复 */

        AlARM_TYPE_STOR_NO_ENOUGH_SPACE = 229,  /**< 摄像机所在磁盘存储空间不足告警 */
        AlARM_TYPE_STOR_DEL_FILE_FAILED = 230,  /**< 删除老文件失败告警 */

        AlARM_TYPE_BEHAVIOR = 231,  /**< 行为告警 */
        AlARM_TYPE_BEHAVIOR_RESUME = 232,  /**< 行为告警恢复 */

        ALARM_TYPE_STREAM_BREAK = 233,  /**< 摄像机断流告警 */

        AlARM_TYPE_SOUND_ABNORMAL = 241,  /**< 异常声音告警 */
        AlARM_TYPE_SOUND_ABNORMAL_RESUME = 242,  /**< 异常声音告警恢复 */

        ALARM_TYPE_CROSS_LINE = 301,  /**< 触发智能拌线 */
        ALARM_TYPE_INTROSION_ZONE = 302,  /**< 区域入侵 */
        ALARM_TYPE_ACCESS_ZONE = 303,  /**< 进入区域 */
        ALARM_TYPE_LEAVE_ZONE = 304,  /**< 离开区域 */
        ALARM_TYPE_HOVER_ZONE = 305,  /**< 区域徘徊 */
        ALARM_TYPE_OVER_FENCE = 306,  /**< 翻越围栏 */
        ALARM_TYPE_CARE_ARTICLE = 307,  /**< 物品看护 */
        ALARM_TYPE_REMAIN_ARTICLE = 308,  /**< 物品遗留 */



        /* 手工告警 401~ */
        AlARM_TYPE_IMPERATIVE_EVENT = 401,  /**< 紧急告警 */

        AlARM_TYPE_NM_PROTECT_EVENT = 800,  /**< 跨域N+M保护类告警, 主要用于跨域N+M保护告警订阅,
                                                     订阅后相当于订阅了存储失败、存储停止两个告警 */
        /* Begin added by baoyihui02795, 2011-04-28 of 卡口项目 */
        /** 卡口业务告警 2001~ */
        ALARM_TYPE_DISPOSITION_STOLEN_VEHICLE = 2001,   /**< 被盗车 */
        ALARM_TYPE_DISPOSITION_ROBBED_VEHICLE = 2002,   /**< 被抢车 */
        ALARM_TYPE_DISPOSITION_SUSPICION_VEHICLE = 2003,   /**< 嫌疑车 */
        ALARM_TYPE_DISPOSITION_TRAFFIC_VIOLATION = 2004,   /**< 交通违法车 */
        ALARM_TYPE_DISPOSITION_EMERGENCY_SURVEILLANCE = 2005,   /**< 紧急查控车 */
        ALARM_TYPE_VEHICLE_BLACKLIST = 2006,   /**< 黑名单 */
        ALARM_TYPE_OTHER_VEHICLE_ALARM = 2007,   /**< 其他车辆违法行为 */
        ALARM_TYPE_P2P_SPEED_DETECTION = 2008,   /**< 区间测速违章 */
        ALARM_TYPE_NOT_WHITELIST = 2009,   /**< 非白名单车辆 */
        /* End added by baoyihui02795, 2011-04-28 of 卡口项目 */
        AlARM_TYPE_MAX,                         /**< 最大值 */

        AlARM_TYPE_ALL = 0xFFFE,           /**< 所有告警的特殊标记 */
        AlARM_TYPE_INVALID = 0xFFFF            /**< 无效值 */
    };


    public enum IMOS_PICTURE_FORMAT_E
    {
        IMOS_PF_PAL = 0,                            /* PAL 制式 */
        IMOS_PF_NTSC,                               /* NTSC 制式 */
        IMOS_PF_720P24HZ,
        IMOS_PF_720P25HZ,
        IMOS_PF_720P30HZ,
        IMOS_PF_720P50HZ,
        IMOS_PF_720P60HZ,
        IMOS_PF_1080I48HZ,
        IMOS_PF_1080I50HZ,
        IMOS_PF_1080I60HZ,
        IMOS_PF_1080P24HZ,
        IMOS_PF_1080P25HZ,
        IMOS_PF_1080P30HZ,
        IMOS_PF_1080P50HZ,
        IMOS_PF_1080P60HZ,
        IMOS_PF_AUTO,
        IMOS_PF_INVALID
    };

    /* 前端设备类型 */
    public enum IMOS_DEVICE_TYPE_E
    {
        IMOS_DT_EC1001_HF = 0,
        IMOS_DT_EC1002_HD,                          /* 软型号 由 EC1004_HC 配置而成，对应 EC1004-HC[2CH] */
        IMOS_DT_EC1004_HC,
        IMOS_DT_EC2004_HF,
        IMOS_DT_ER3304_HF,
        IMOS_DT_ER3308_HD = 5,
        IMOS_DT_ER3316_HC,
        IMOS_DT_DC1001_FF = 7,
        IMOS_DT_EC3016_HF,
        IMOS_DT_ISC3100_ER,
        IMOS_DT_EC1001_EF = 10,
        IMOS_DT_EC3004_HF_ER,
        IMOS_DT_EC3008_HD_ER,

        /* 枚举值 200 -- 500 */
        IMOS_DT_EC2016_HC = 200,
        IMOS_DT_EC2016_HC_8CH,
        IMOS_DT_EC2016_HC_4CH,
        IMOS_DT_EC1501_HF,                          /* 6437真双流 */
        IMOS_DT_EC1004_MF,                          /* 多路EC */
        IMOS_DT_ECR2216_HC,                         /* ECR预研，1U */
        IMOS_DT_EC1304_HC,                          /* 带PCMCIA */

        /* 枚举值 500 -- 800 */
        IMOS_DT_DC3016_FC = 500,                    /* 目前V1R5，2U，和ISC3000-M共用设计 */
        IMOS_DT_DC2016_FC,                          /* DC3016-FC降成本 */
        IMOS_DT_DC1016_MF,                          /* 多路DC */
        IMOS_DT_DC2004_FF,                          /* 带数字矩阵的多路DC */

        IMOS_DT_EC1001 = 1000,                      /* R1 编码板，为了方便扩展，从1000开始计数 */
        IMOS_DT_DC1001 = 1001,                      /* R1 解码板 */
        IMOS_DT_EC1101_HF = 1002,
        IMOS_DT_EC1102_HF = 1003,
        IMOS_DT_EC1801_HH = 1004,
        IMOS_DT_DC1801_FH = 1005,

        /* OEM产品型号 */
        IMOS_DT_VR2004 = 10003,
        IMOS_DT_R1000 = 10203,
        IMOS_DT_VL2004 = 10503,
        IMOS_DT_VR1102 = 11003,
        /* IPC产品型号 */
        IMOS_DT_HIC5201 = 12001,
        IMOS_DT_HIC5221 = 12002,
        IMOS_DT_BUTT
    };

    /**
* @enum tagMediaFileFormat
* @brief 媒体文件格式枚举定义
* @attention 无
*/
    public enum XP_MEDIA_FILE_FORMAT_E
    {
        XP_MEDIA_FILE_TS = 0,              /**< TS格式的媒体文件 */
        XP_MEDIA_FILE_FLV = 1               /**< FLV格式的媒体文件 */
    };

    public enum IMOS_STREAM_RELATION_SET_E
    {
        IMOS_SR_MPEG4_MPEG4 = 0,                    /* MPEG4[主码流] + MPEG4[辅码流] */
        IMOS_SR_H264_SHARE = 1,                     /* H.264[主码流] */
        IMOS_SR_MPEG2_MPEG4 = 2,                    /* MPEG2[主码流] + MPEG4[辅码流] */
        IMOS_SR_H264_MJPEG = 3,                     /* H.264[主码流] + MJPEG[辅码流] */
        IMOS_SR_MPEG4_SHARE = 4,                    /* MPEG4[主码流] */
        IMOS_SR_MPEG2_SHARE = 5,                    /* MPEG2[主码流] */
        IMOS_SR_STREAM_MPEG4_8D1 = 8,          /* MPEG4[主码流_D1] 8D1 套餐*/
        IMOS_SR_MPEG2_MPEG2 = 9,                    /* MPEG2[主码流] + MPEG2[辅码流] */
        IMOS_SR_H264_H264 = 11,                     /* H.264[主码流] + H.264[辅码流] */

        IMOS_SR_BUTT
    };


    /**
 * @enum tagIMOSFavoriteStream
 * @brief  流配置策略类型
 * @attention 无
 */
    public enum IMOS_FAVORITE_STREAM_E
    {
        IMOS_FAVORITE_STREAM_ANY = 0,        /**< 不指定 */
        IMOS_FAVORITE_STREAM_PRIMARY = 1,        /**< 指定主流 */
        IMOS_FAVORITE_STREAM_SECONDERY = 2,        /**< 指定辅流 */
        IMOS_FAVORITE_STREAM_THIRD = 3,        /**< 指定三流 */
        IMOS_FAVORITE_STREAM_FOURTH = 4,        /**< 指定四流 */
        IMOS_FAVORITE_STREAM_FIFTH = 5,        /**< 指定五流 */
        IMOS_FAVORITE_STREAM_BI_AUDIO = 6,        /**< 指定语音对讲 */
        IMOS_FAVORITE_STREAM_VOICE_BR = 7,        /**< 指定语音广播 */
        IMOS_FAVORITE_STREAM_BUTT,
        IMOS_FAVORITE_STREAM_INVALID = 0xFFFF    /**< 无效值 */
    }

    public enum XP_PROTOCOL_E
    {
        XP_PROTOCOL_UDP = 0,                   /**< UDP协议 */
        XP_PROTOCOL_TCP = 1,                   /**< TCP协议Client端*/
        XP_PROTOCOL_TCP_SERVER = 2             /**< TCP协议Server端*/
    }

    /**
* @enum tagDownMediaSpeed
* @brief 媒体数据下载速度的枚举定义
* @attention 无
*/
    public enum XP_DOWN_MEDIA_SPEED_E
    {
        XP_DOWN_MEDIA_SPEED_ONE = 0,            /**< 一倍速下载媒体文件 */
        XP_DOWN_MEDIA_SPEED_TWO = 1,            /**< 二倍速下载媒体文件 */
        XP_DOWN_MEDIA_SPEED_FOUR = 2,           /**< 四倍速下载媒体文件 */
        XP_DOWN_MEDIA_SPEED_EIGHT = 3           /**< 八倍速下载媒体文件 */
       
    }


    /// <summary>
    /// 通用的云台操作枚举值
    /// </summary>
    public enum PTZ_CMD_E
    {
        PTZ_UP,
        PTZ_UP_STOP,
        PTZ_DOWN,
        PTZ_DOWN_STOP,
        PTZ_LEFT,
        PTZ_LEFT_STOP,
        PTZ_RIGHT,
        PTZ_RIGHT_STOP,
        PTZ_LEFT_UP,
        PTZ_LEFT_UP_STOP,
        PTZ_LEFT_DOWN,
        PTZ_LEFT_DOWN_STOP,
        PTZ_RIGHT_UP,
        PTZ_RIGHT_UP_STOP,
        PTZ_RIGHT_DOWN,
        PTZ_RIGHT_DOWN_STOP,

        PTZ_ALL_STOP,           /**< 全停 */

        PTZ_IRIS_OPEN,          /**< 光圈开 */
        PTZ_IRIS_OPEN_STOP,
        PTZ_IRIS_CLOSE,         /**< 光圈关 */
        PTZ_IRIS_CLOSE_STOP,

        PTZ_FOCUS_FAR,          /**< 聚焦远 */
        PTZ_FOCUS_FAR_STOP,
        PTZ_FOCUS_NEAR,         /**< 聚焦近 */
        PTZ_FOCUS_NEAR_STOP,

        PTZ_ZOOM_TELE,          /**< 放大 */
        PTZ_ZOOM_TELE_STOP,
        PTZ_ZOOM_WIDE,          /**< 缩小 */
        PTZ_ZOOM_WIDE_STOP,

        PTZ_PRE_SAVE,           /**< 保存预置位 */
        PTZ_PRE_CALL,           /**< 调用预置位 */
        PTZ_PRE_DELETE,         /**< 删除预置位 */

        PTZ_BRUSH_ON,           /**< 雨刷开 */
        PTZ_BRUSH_OFF,
        PTZ_LIGHT_ON,           /**< 灯光开 */
        PTZ_LIGHT_OFF,
        PTZ_HEAT_ON,            /**< 加热开 */
        PTZ_HEAT_OFF,

        PTZ_CRUISE_START,       /**< 启动巡航 */
        PTZ_CRUISE_STOP,        /**< 停止巡航 */
    }

    /// <summary>
    /// 告警推送类型
    /// </summary>
    public enum SUBSCRIBE_PUSH_TYPE_E
    {
        SUBSCRIBE_PUSH_TYPE_ALL,         //接受告警推送和设备状态推送  
        SUBSCRIBE_PUSH_TYPE_ALARM,       //只接收告警推送  
        SUBSCRIBE_PUSH_TYPE_ALARM_STATUS,//只接收设备状态推送  
        SUBSCRIBE_PUSH_TYPE_MAX,         //   
        SUBSCRIBE_PUSH_TYPE_INVALID
    }

    /** 回调函数处理信息类型 */
    public enum CALL_BACK_PROC_TYPE_E : uint
    {
        PROC_TYPE_DEV_STATUS = 0,            /**< 设备状态，对应结构 : AS_STAPUSH_UI_S */
        PROC_TYPE_ALARM = 1,            /**< 告警，对应结构 : AS_ALARMPUSH_UI_S */
        PROC_TYPE_DEV_CAM_SHEAR = 2,            /**< 共享摄像机，对应结构 : AS_DEVPUSH_UI_S */
        PROC_TYPE_MONITOR_BE_REAVED = 3,            /**< 实况被抢占，对应结构 : CS_MONITOR_REAVE_NOTIFY_S */
        PROC_TYPE_SWITCH_BE_REAVED = 4,            /**< 轮切被抢占，对应结构 : CS_SWITCH_REAVE_NOTIFY_S */
        PROC_TYPE_MONITOR_BE_STOPPED = 5,            /**< 实况被停止，对应结构 : CS_MONITOR_REAVE_NOTIFY_S */
        PROC_TYPE_SWITCH_BE_STOPPED = 6,            /**< 轮切被停止，对应结构 : CS_SWITCH_REAVE_NOTIFY_S */
        PROC_TYPE_VOCSRV_BE_REAVED = 7,            /**< 语音被抢占，对应结构 : CS_VOCSRV_REAVE_NOTIFY_S */
        PROC_TYPE_PTZ_EVENT = 8,            /**< 云台事件通知，对应结构 : CS_PTZ_REAVE_NOTIFY_S */
        PROC_TYPE_TRB_PROC = 9,            /**< 故障处理通知，对应结构 : CS_NOTIFY_UI_TRB_EVENT_PROC_S */
        PROC_TYPE_SRV_SETUP = 10,           /**< 故障恢复业务建立通知，对应结构 : CS_NOTIFY_UI_SRV_SETUP_S */
        PROC_TYPE_XP_ALARM_SETUP = 11,           /**< 告警联动到XP窗格通知，对应结构 : CS_NOTIFY_UI_SRV_SETUP_S */

        PROC_TYPE_LOGOUT = 12,           /**< 退出登陆，对应结构 :无 */

        PROC_TYPE_MEDIA_PARAM_CHANGE = 13,           /**< 媒体参数改变，对应结构 : CS_NOTIFY_UI_MEDIA_PARAM_CHANGE_PROC_S */

        PROC_TYPE_EXDOMAIN_STATUS = 14,           /**< 外域状态，对应结构 : AS_EXDOMAIN_STAPUSH_UI_S */

        PROC_TYPE_BACKUP_DATA_FINISH = 15,           /**< 信息备份完成通知, 对应结构 : CS_BACKUP_FINISH_INFO_S */

        PROC_TYPE_TL_EVENT = 16,           /**< 透明通道事件通知，对应结构 : CS_TL_REAVE_NOTIFY_S */

        PROC_TYPE_SALVO_UNIT_EVENT = 17,           /**< 组显示单元事件通知, 对应结构 : CS_NOTIFY_SALVO_UNIT_EVENT_S */
        PROC_TYPE_SALVO_EVENT = 18,           /**< 组切业务事件通知, 对应结构 : CS_NOTIFY_UI_SALVO_EVENT_S */
        PROC_TYPE_START_XP_SALVO = 19,           /**< 通知UI启动组轮巡的组显示, 对应结构: CS_NOTIFY_START_XP_SALVO_S */

        PROC_TYPE_VODWALL_BE_REAVED = 20,           /**< 通知回放上墙被抢占，对应结构：CS_VODWALL_REAVE_NOTIFY_S */
        PROC_TYPE_VODWALL_BE_STOPPED = 21,           /**< 通知回放上墙被停止，对应结构：CS_VODWALL_REAVE_NOTIFY_S */

        PROC_TYPE_VOD_BE_REAVED = 22,           /**< 回放被抢占，对应结构 : CS_VOD_REAVE_NOTIFY_S */

        PROC_TYPE_DOMAIN_SYN_RESULT = 23,           /**< 域间同步的结果，对应结构 : AS_DOMAIN_SYN_PUSHUI_S */

        PROC_TYPE_VOC_REQ = 24,           /**< 客户端语音请求，对应结构 : CS_VOC_REQ_NOTIFY_S */
        PROC_TYPE_VOC_STATE_NOTIFY = 25,           /**< 语音业务状态通知，对应结构 : CS_VOC_STATE_NOTIFY_S */

        /*******************************************************************************
        SS新增定义 新增回调函数
        *******************************************************************************/
        PROC_TYPE_ACCEPT_SPEAK_YESORNO = 100,           /**< 发言申请， 对应结构 ：CONF_SITE_INFO_EX_S */
        PROC_TYPE_CONF_STATUS_CHANGE = 101,           /**< 会议状态改变， 对应结构 ：CONF_STATUS_INFO_EX_S 如果是周期会议且非最后一个周期，上报会议未开始/其它上报会议已经结束 */
        PROC_TYPE_DEVICE_CODE_CHANGE = 102,           /**< 设备编码改变， 对应结构 ：DEVICE_CODE_CHANGE_INFO_EX_S */
        PROC_TYPE_DEVICE_CHANGE = 103,           /**< 终端设备更新信息， 当存在设备新增、删除时， 上报更新消息， 对应的结构 ：DEVICE_CHANGE_INFO_EX_S */
        PROC_TYPE_MODIFY_TERM = 104,           /**< 修改终端消息， 对应的结构 ：MODIFY_TERM_REP_EX_S */
        PROC_TYPE_CHAIR_CHANGE = 105,           /**< 当前主席发生改变， 主席会场释放则会场编码为空。对应的结构 ：CONF_SITE_INFO_EX_S */
        PROC_TYPE_SPEAKER_CHANGE = 106,           /**< 当前发言人发生改变， 对应的结构 ：CONF_SITE_INFO_EX_S */
        PROC_TYPE_TERM_STATUS_CHANGE = 107,           /**< 会场终端状态改变， 对应的结构 ：TERM_STATUS_CHANGE_EX_S */
        PROC_TYPE_DELAY_CONF = 108,           /**< 延迟会议， 对应结构 ：DELAY_CONF_INFO_EX_S */
        PROC_TYPE_SYNCHRONIZE_WITH_WEB = 109,           /**< 上报广播会场， 主席、主场观看会场 对应的结构 ： MC_SYNCHRONIZE_WITH_WEB_EX_S  */
        PROC_TYPE_MCU_BACKUP_CHANGE_TO_MASTER = 110,    /**< MCU备份通知， 对应结构 ：BACKUP_MCU_REPORT_S  */
        PROC_TYPE_SEND_ROLE_SITE_CHANGE = 111,           /**< 当前辅流发送者变化通知， 对应结构 ：CONF_SEND_ROLE_SITE_CHANGE_S  */
        PROC_TYPE_AUTOMULTIPIC_CHANGE = 112,           /**< 多画面自动切换值改变通知， 对应结构 ：CONF_AUTOMULTIPIC_CHANGE_S  */
        PROC_TYPE_SET_TURN_BROADCAST_CHANGE = 113,       /**< 设置画面轮询模式改变通知， 对应结构 ：CONF_SET_TURN_BROADCAST_CHANGE_S */
        PROC_TYPE_SET_PIC_MODE_CHANGE = 114,           /**< 设置画面模式改变通知， 对应结构 ：CONF_SET_PIC_MODE_CHANGE_S */
        PROC_TYPE_MCU_SYNC_STATUS_CHANGE = 115,          /**< MCU同步状态改变通知， 对应结构 ：MCU_SYNC_STATUS_CHANGE_S */
        PROC_TYPE_CUR_BROADCAST_CHANGE = 116,          /**< 当前实际广播会场改变通知，对应结构：CUR_BROADCAST_INFO_EX_S */
        PROC_TYPE_CUR_CHAIR_BROWSE_CHANGE = 117,     /**< 当前主席或主场实际观看的会场改变通知，对应结构：CUR_CHAIR_BROWSE_INFO_EX_S */
        PROC_TYPE_CONF_FECC_CHANGE = 118,          /**< 当前FECC控制者或被控者变化通知，对应结构：CONF_FECC_CHANGE_S */
        PROC_TYPE_CONF_MCU_BACKUP_CHANGE = 119,     /**< 当前会议中MCU备份变化通知，对应结构：CONF_MCU_BACKUP_CHANGE_S */
        PROC_TYPE_CALL_SITE_RESULT = 120,          /**< 呼叫会场结果通知，对应结构：CALL_SITE_INFO_EX_S */
        PROC_TYPE_GK_REG_STATE = 121,          /**< GK注册结果通知，对应结构：GK_REG_STATE_INFO_EX_S */
        PROC_TYPE_MG_SESSION_STATUS_CHANGE = 122,     /**< 终端会话状态，对应结构：MG_SESSION_STATUS_EX_S */
        PROC_TYPE_MAX,                                   /**< 回调函数处理信息类型最大值 */
        PROC_TYPE_INVALID = 0xFFFFFFFE    /**< 无效值 */
    }




    /**
* @enum tagRunInfoType
* @brief 上报消息类型的枚举定义
* @attention 无
*/
    public enum XP_RUN_INFO_TYPE_E
    {
        XP_RUN_INFO_SERIES_SNATCH = 0,        /**< 连续抓拍过程中上报运行信息 */
        XP_RUN_INFO_RECORD_VIDEO = 1,         /**< 本地录像过程中上报运行信息 */
        XP_RUN_INFO_MEDIA_PROCESS = 2,        /**< 视频媒体处理过程中的上报运行信息 */
        XP_RUN_INFO_DOWN_MEDIA_PROCESS = 3,   /**< 媒体流下载过程中上报运行信息 */
        XP_RUN_INFO_VOICE_MEDIA_PROCESS = 4,  /**< 语音媒体处理过程中的上报运行信息 */
        XP_RUN_INFO_RTSP_PROTOCOL = 5,        /**< RTSP协议组件运行的错误信息 */
        XP_RUN_INFO_DOWN_RTSP_PROTOCOL = 6,   /**< 下载录像过程中RTSP协议的错误信息 */
        XP_RUN_INFO_SIP_LIVE_TIMEOUT = 7,     /**< SIP注册保活超时 */
        XP_RUN_INFO_PASSIVE_MONITOR = 8,      /**< 被动实况停止操作信息 */
        XP_RUN_INFO_PASSIVE_START_MONITOR = 9,/**< 被动实况启动操作信息 */
        XP_RUN_INFO_MEDIA_NOT_IDENTIFY = 10,  /**< 码流无法识别 */
        XP_RUN_INFO_RECV_PACKET_NUM = 11,     /**< 周期内接收到的包数 */
        XP_RUN_INFO_RECV_BYTE_NUM = 12,       /**< 周期内接收到的字节数 */
        XP_RUN_INFO_VIDEO_FRAME_NUM = 13,     /**< 周期内解析的视频帧数 */
        XP_RUN_INFO_AUDIO_FRAME_NUM = 14,     /**< 周期内解析的音频帧数 */
        XP_RUN_INFO_LOST_PACKET_RATIO = 15,   /**< 周期内丢包率统计信息（单位为0.01%） */
        XP_RUN_INFO_MEDIA_PLAY_PROGRESS = 16, /**< 媒体中携带的进度信息 */
        XP_RUN_INFO_MEDIA_PLAY_END = 17,      /**< 媒体中携带的播放结束 */
        XP_RUN_INFO_MEDIA_ABNORMAL = 18       /**< 媒体处理异常 */
    }

    public enum MW_PTZ_CMD_E
    {
        MW_PTZ_IRISCLOSESTOP = 0x0101, /**< 光圈关停止 */
        MW_PTZ_IRISCLOSE = 0x0102,         /**< 光圈关 */
        MW_PTZ_IRISOPENSTOP = 0x0103,   /**< 光圈开停止 */
        MW_PTZ_IRISOPEN = 0x0104,   /**< 光圈开 */

        MW_PTZ_FOCUSNEARSTOP = 0x0201, /**< 近聚集停止 */
        MW_PTZ_FOCUSNEAR = 0x0202,    /**< 近聚集 */
        MW_PTZ_FOCUSFARSTOP = 0x0203,/**< 远聚集 停止*/
        MW_PTZ_FOCUSFAR = 0x0204,        /**< 远聚集 */

        MW_PTZ_ZOOMTELESTOP = 0x0301,/**< 放大停止 */
        MW_PTZ_ZOOMTELE = 0x0302,/**< 放大 */
        MW_PTZ_ZOOMWIDESTOP = 0x0303,/**< 缩小停止 */
        MW_PTZ_ZOOMWIDE = 0x0304,/**< 缩小 */

        MW_PTZ_TILTUPSTOP = 0x0401,/**< 向上停止 */
        MW_PTZ_TILTUP = 0x0402,/**< 向上 */
        MW_PTZ_TILTDOWNSTOP = 0x0403,/**< 向下停止 */
        MW_PTZ_TILTDOWN = 0x0404,/**< 向下 */

        MW_PTZ_PANRIGHTSTOP = 0x0501,/**< 向右停止 */
        MW_PTZ_PANRIGHT = 0x0502,/**< 向右 */
        MW_PTZ_PANLEFTSTOP = 0x0503,/**< 向左停止 */
        MW_PTZ_PANLEFT = 0x0504,/**< 向左 */

        MW_PTZ_PRESAVE = 0x0601,/**< 预置位保存 */
        MW_PTZ_PRECALL = 0x0602,/**< 预置位调用 */
        MW_PTZ_PREDEL = 0x0603,/**< 预置位删除 */

        MW_PTZ_LEFTUPSTOP = 0x0701,/**< 左上停止 */
        MW_PTZ_LEFTUP = 0x0702,/**< 左上 */
        MW_PTZ_LEFTDOWNSTOP = 0x0703,/**< 左下停止 */
        MW_PTZ_LEFTDOWN = 0x0704,/**< 左下 */

        MW_PTZ_RIGHTUPSTOP = 0x0801,/**< 右上停止 */
        MW_PTZ_RIGHTUP = 0x0802,/**< 右上 */
        MW_PTZ_RIGHTDOWNSTOP = 0x0803,/**< 右下停止 */
        MW_PTZ_RIGHTDOWN = 0x0804,/**< 右下 */

        MW_PTZ_ALLSTOP = 0x0901,/**< 全停命令字 */

        MW_PTZ_BRUSHON = 0x0A01,/**< 雨刷开 */
        MW_PTZ_BRUSHOFF = 0x0A02,/**< 雨刷关 */

        MW_PTZ_LIGHTON = 0x0B01,/**< 灯开 */
        MW_PTZ_LIGHTOFF = 0x0B02,/**< 灯关 */

        MW_PTZ_HEATON = 0x0C01,/**< 加热开 */
        MW_PTZ_HEATOFF = 0x0C02,/**< 加热关 */

        MW_PTZ_INFRAREDON = 0x0D01,/**< 红外开 */
        MW_PTZ_INFRAREDOFF = 0x0D02,/**< 红外关 */

        MW_PTZ_SCANCRUISE = 0x0E01,/**< 云台线性扫猫 */
        MW_PTZ_SCANCRUISESTOP = 0x0E02,/**< 云台线性扫猫 */

        MW_PTZ_TRACKCRUISE = 0x0F01,/**<  云台轨迹巡航 */
        MW_PTZ_TRACKCRUISESTOP = 0x0F02,/**<  云台轨迹巡航 */

        MW_PTZ_PRESETCRUISE = 0x1001,/**<  云台按预置位巡航 ，该命令字不在云台模板体现 */
        MW_PTZ_PRESETCRUISESTOP = 0x1002,/**<  云台按预置位巡航 停止，该命令字不在云台模板体现 */

        PTZ_RELEASE,            /**< 释放云台 */
        PTZ_LOCK,               /**< 锁定云台 */
        PTZ_UNLOCK,             /**< 解锁云台 */
        MW_PTZ_CMD_BUTT

    }


    public enum IMOS_TYPE_E
    {
        IMOS_TYPE_ORG = 1,                     /**< 组织域 */
        IMOS_TYPE_OUTER_DOMAIN = 2,            /**< 外域 */
        IMOS_TYPE_LOCAL_DOMAIN = 3,            /**< 本域 */

        IMOS_TYPE_DM = 11,                     /**< DM */
        IMOS_TYPE_MS = 12,                     /**< MS */
        IMOS_TYPE_VX500 = 13,                  /**< VX500 */
        IMOS_TYPE_MONITOR = 14,                /**< 监视器 */

        IMOS_TYPE_EC = 15,                     /**< EC */
        IMOS_TYPE_DC = 16,                     /**< DC */

        IMOS_TYPE_GENERAL = 17,                /**< 通用设备 */

        IMOS_TYPE_MCU = 201,                   /**< MCU */
        IMOS_TYPE_MG = 202,                    /**< MG */

        IMOS_TYPE_CAMERA = 1001,               /**< 摄像机 */
        IMOS_TYPE_ALARM_SOURCE = 1003,         /**< 告警源 */

        IMOS_TYPE_STORAGE_DEV = 1004,          /**< 存储设备 */
        IMOS_TYPE_TRANS_CHANNEL = 1005,        /**< 透明通道 */

        IMOS_TYPE_ALARM_OUTPUT = 1200,         /**< 告警输出 */

        IMOS_TYPE_GUARD_TOUR_RESOURCE = 2001,  /**< 轮切资源 */
        IMOS_TYPE_GUARD_TOUR_PLAN = 2002,      /**< 轮切计划 */
        IMOS_TYPE_MAP = 2003,                  /**< 地图 */

        IMOS_TYPE_XP = 2005,                   /**< XP */
        IMOS_TYPE_XP_WIN = 2006,               /**< XP窗格 */
        IMOS_TYPE_GUARD_PLAN = 2007,           /**< 布防计划 */

        IMOS_TYPE_DEV_ALL = 2008,              /**< 所有的设备类型(EC/DC/MS/DM/VX500/摄像头/监视器) */
        IMOS_TYPE_TV_WALL = 3001,              /**< 电视墙 */

        IMOS_TYPE_CONFERENCE = 4001,           /**< 会议资源 */

        IMOS_TYPE_MAX
    }
    /**
* @enum tagScreenSplitType
* @brief 分屏类型
* @attention
*/
    public enum SCREEN_SPLIT_TYPE_E
    {
        SCREEN_SPLIT_TYPE_ONE = 1,                 /** 单分屏 */
        SCREEN_SPLIT_TYPE_FOUR = 4,                 /** 4分屏 */
        SCREEN_SPLIT_TYPE_SIX = 6,                 /** 6分屏 */
        SCREEN_SPLIT_TYPE_EIGHT = 8,                 /** 8分屏 */
        SCREEN_SPLIT_TYPE_NINE = 9,                 /** 9分屏 */
        SCREEN_SPLIT_TYPE_TEN = 10,                /** 10分屏 */
        SCREEN_SPLIT_TYPE_THIRTEEN = 13,                /** 13分屏 */
        SCREEN_SPLIT_TYPE_SIXTEEN = 16,                /** 16分屏 */
        SCREEN_SPLIT_TYPE_MAX,                                              /**< 最大值 */
        SCREEN_SPLIT_TYPE_INVALID = 0x0FFFFFFF       /**< 无效值 */
    }

    /**
* @enum tagSplitScrMode
* @brief 监视器分屏模式
* @attention
*/
    public enum SPLIT_SCR_MODE_E
    {
        SPLIT_SCR_MODE_0 = 0,    /**< 保留字符 */
        SPLIT_SCR_MODE_1 = 1,    /**< 全屏 */
        SPLIT_SCR_MODE_2 = 2,    /**< 2 分屏*/
        SPLIT_SCR_MODE_3 = 3,    /**< 3 分屏*/
        SPLIT_SCR_MODE_4 = 4,    /**< 4分屏 */
        SPLIT_SCR_MODE_5 = 5,    /**< 5 分屏*/
        SPLIT_SCR_MODE_6 = 6,    /**< 6分屏 */
        SPLIT_SCR_MODE_7 = 7,    /**< 7 分屏*/
        SPLIT_SCR_MODE_8 = 8,    /**< 8 分屏 */
        SPLIT_SCR_MODE_9 = 9,    /**< 9 分屏 */
        SPLIT_SCR_MODE_10 = 10,   /**< 10 分屏 */
        SPLIT_SCR_MODE_11 = 11,    /**< 11 分屏*/
        SPLIT_SCR_MODE_12 = 12,    /**< 12 分屏*/
        SPLIT_SCR_MODE_13 = 13,   /**< 13 分屏 */
        SPLIT_SCR_MODE_14 = 14,    /**< 14 分屏*/
        SPLIT_SCR_MODE_15 = 15,    /**< 15 分屏*/
        SPLIT_SCR_MODE_16 = 16,   /**< 16 分屏 */
        SPLIT_SRC_MODE_25 = 25,   /**< 25分屏*/
        SPLIT_SRC_MODE_36 = 36,   /**< 36分屏*/

        SPLIT_SCR_MODE_MAX,                     /**< 最大值 */

        /* Begin modified by l01420 for MPPD08374, 2014-4-8 */
        /**<SPLIT_SCR_MODE_MAX枚举值以上定义最新分屏数的模式，
        以下所定义的值表示特殊的分屏模式(分屏数重复，模式不同)*/
        SPLIT_SCR_MODE_80 = 80,    /**< 8 分屏 走廊模式 */

        SPLIT_SCR_MODE_LIMIT_VALUE,/**<分屏模式最大值*/
        /* End modified by l01420 for MPPD08374, 2014-4-8 */

        SPLIT_SCR_MODE_INVALID = 0x0FFFFFFF    /**< 无效值 */
    }

    /* added by z06806 for tollgate struct */
    /**
     * @struct tagReservedInfo
     * @brief 预留字段结构
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct RESERVED_INFO_S
    {
        /** 预留字段1:已使用，用于表示触发原因 */
        public UInt32 ulReserved1;

        /** 预留字段2 */
        public UInt32 ulReserved2;

        /** 预留字段3 */
        public UInt32 ulReserved3;

        /** 预留字段4 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved4;

        /** 预留字段5 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved5;

        /** 预留字段6 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RESERVED_LEN)]
        public byte[] szReserved6;
    }


    /**
     * @struct tagA8Info
     * @brief A8信息
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct A8_INFO_S
    {
        /** A8编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8Code;

        /** A8 EC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8ECCode;

        /** A8 DC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szA8DCCode;

        /** A8名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szA8Name;

        /** A8类型，取值为#IMOS_DEVICE_TYPE_E */
        public UInt32 ulA8Type;

        /** 设备地址类型，1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** 设备地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** 所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 所属组织名称，查询返回，其他情况下可以不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOrgName;

        /** 设备是否在线取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义 */
        public UInt32 ulIsOnline;

        /** 设备扩展状态，预留 */
        public UInt32 ulDevExtStatus;

        /** 预留字段信息 */
        public RESERVED_INFO_S stReservedInfo;
    }


    /**
* @struct tagA8ScreenUnitInfo
* @brief A8监视器单元信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct A8_SCREEN_UNIT_INFO_S
    {
        /** 监视器名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScreenName;

        /** 监视器编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szScreenCode;

        /** 状态信息:在线为#IMOS_DEV_STATUS_ONLINE，离线为#IMOS_DEV_STATUS_OFFLINE，添加修改时状态信息无须填写 */
        public UInt32 ulStatus;

        /** 监视器分屏数目，该值仅在查询时有效 */
        public UInt32 ulSplitScreenNum;

        /** 监视器拨码*/
        public UInt32 ulDialCode;

        /** 扩展状态信息:0表示为监视器，1表示为DC */
        public UInt32 ulExtStatus;
    }

    /**
* @struct tagTimerParam
* @brief 定时开关机参数信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TIMER_PARAM_S
    {
        /** 倒计时使能 */
        public UInt32 ulCountdownEnable;

        /** 定时开机使能 */
        public UInt32 ulStartupEnable;

        /** 定时关机使能 */
        public UInt32 ulShutdownEnable;

        /** 倒计时时长 */
        public UInt32 ulCountdownTime;

        /** 星期枚举组合 */
        public UInt32 ulWeekDay;       /** #TV_WEEK_DAY_E */

        /** 使能计划开始时间 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_SIMPLE_TIME_LEN)]
        public byte[] szBeginTime;

        /** 使能计划结束时间 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_SIMPLE_TIME_LEN)]
        public byte[] szEndTime;

    }

    /**
 * @struct tagTVWallBaseInfo
 * @brief 电视墙信息扩展信息
 * @attention
 */
    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_BASE_INFO_EXT_S
    {
        /** 电视墙编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** 场景ID */
        public UInt32 ulSceneId;

        /** 定时参数相关信息 */
        public TIMER_PARAM_S stTimerParam;

        /** 是否静音 */
        public UInt32 ulAudioMute;

        /** 输出音量  */
        public UInt32 ulAudioVolume;

    }


    /**
    * @struct tagTVWallBaseInfoA8
    * @brief 电视墙信息
    * @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_BASE_INFO_A8_S
    {
        /** 电视墙名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szTVWallName;

        /** 电视墙场景ID*/
        public UInt32 ulSceneId;

        /** 电视墙编码，增加电视墙时填写为空则平台自己生成，增加电视墙成功后返回; 若填写则按照填写的编码增加 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** 所属组织域编码，增加修改时需要填写，平台返回不处理该值 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szOrgCode;

        /** 电视墙行数 */
        public UInt32 ulTvWallRows;

        /** 电视墙列数 */
        public UInt32 ulTvWallColumns;

        /** 是否开启边框补偿，0：关闭 1：开启 */
        public UInt32 ulCompEnable;

        /** 垂直补偿宽度，单位mm */
        public UInt32 ulVerOffset;

        /** 水平补偿宽度，单位mm */
        public UInt32 ulHorOffset;

        /** 网格密度 0: 表示没有网格  其他表示对应网格，例：4表示 4*4 */
        public UInt32 ulMeshDensity;

        /** 电视墙分辨率 */
        public UInt32 ulResolution;

        /** 电视墙控制类型 */
        public UInt32 ulControlType;  /* #TV_WALL_CONTROL_TYPE_E */

        /** IP地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szIPAddress;

        /** 端口号 */
        public UInt32 ulPort;

        /** 使用串口号*/
        public UInt32 ulComId;

        /** 使用串口通信协议*/
        public UInt32 ulComProtocal;

        /** 音频输入编码*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szAudioInputCode;

        /** 音频输出编码*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szAudioOutputCode;

        /** 时钟延迟 */
        public UInt32 ulClockDelay;

        /** 输入信号类型 */
        public UInt32 ulInputType;

        /** 是否启用底图 */
        public UInt32 ulBasePicEnable;

        /** 底图ID */
        public UInt32 ulBasePicId;

        /** 电视墙扩展信息 */
        public TVWALL_BASE_INFO_EXT_S stTVWallBaseInfoExt;

        /* 绑定监视器信息 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_SCREEN_MAX)]
        public A8_SCREEN_UNIT_INFO_S[] aulScreenInfo;

    }


    /**
    * @struct tagTVWallA8Info
    * @brief 电视墙信息信息
    * @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_A8_INFO
    {
        /** 电视墙基本信息 */
        public TV_WALL_BASE_INFO_A8_S stTVWallBaseInfo;

        /** 电视墙是否在线 */
        public UInt32 ulTVWallOnline;

    }


    /**
* @struct tagCameraUnitInfo
* @brief 摄像机单元信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct CAMERA_UNIT_INFO_S
    {
        /** 摄像机名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCamName;

        /** 摄像机编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szCamCode;

        /** 状态信息:在线为#IMOS_DEV_STATUS_ONLINE，离线为#IMOS_DEV_STATUS_OFFLINE，添加修改时状态信息无须填写 */
        public UInt32 ulStatus;
    }


    /**
* @struct tagTVWallWindowInfo
* @brief 开窗信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_WINDOWS_INFO
    {
        /** 窗口编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szWindowCode;

        /** 电视墙编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** 分屏类型 #SCREEN_SPLIT_TYPE_E */
        public UInt32 ulSplitType;

        /** 窗口透明度 */
        public UInt32 ulTransparency;

        /** 窗口叠加层次 */
        public UInt32 ulLevel;

        /** 虚拟屏位置，这里以像素为单位，以屏左上角为原点 */
        public AREA_SCOPE_S stPosition;

        /** 视频输入源信息，如果没有对应编码为空 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public CAMERA_UNIT_INFO_S[] stCamUnitInfo;


    }
    /**
* @struct tagCharContentInfo
* @brief LED内容信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct CHAR_CONTENT_INFO_S
    {
        /** 虚拟屏位置，这里以像素为单位，以屏左上角为原点 */
        public AREA_SCOPE_S stPosition;

        /** 字体类型 */
        public UInt32 ulFonts;

        /** 字体大小 */
        public UInt32 ulFontSize;

        /** 字体颜色 */
        public UInt32 ulFontColor;

        /** 字间距 */
        public UInt32 ulFontGap;

        /** 内容是否启用 */
        public UInt32 ulContentEnable;

        /** LED内容 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        byte[] szContent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_VIRTUAL_LED_INFO_S
    {
        /** 电视墙编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** LED内容 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_LED_CONTENT_MAX)]
        public CHAR_CONTENT_INFO_S[] astContentInfo;

        /** 虚拟屏位置，这里以像素为单位，以屏左上角为原点 */
        AREA_SCOPE_S stPosition;

        /** 背景边框 */
        public UInt32 ulBackgroundFrame;

        /** 背景颜色 */
        public UInt32 ulBackgroundColor;

        /** 是否启用 */
        public UInt32 ulEnable;

        /** 场景ID */
        public UInt32 ulSceneId;

    }


    /**
     * @struct tagTVWallBasePicInfo
     * @brief 电视墙底图信息
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct TVWALL_BASE_PIC_INFO_S
    {
        /** 电视墙编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szTVWallCode;

        /** 底图名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szBasePicName;

        /** 底图路径 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szBasePicPath;

        /** 底图ID */
        public UInt32 ulBasePicID;

        /** 场景ID */
        public UInt32 ulSceneID;

        /** 虚拟屏位置，这里以像素为单位，以屏左上角为原点 */
        public AREA_SCOPE_S stPosition;

    }



    /**
* @struct tagTVWallAllInfo
* @brief 电视墙信息
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct TV_WALL_ALL_INFO_S
    {
        /** 设备编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szDevCode;

        /** 电视墙基本信息 */
        TV_WALL_BASE_INFO_A8_S stBaseInfo;

        /** 轮询状态 */
        public UInt32 ulTVWallPollStatus;

        /** 是否自动吸附 */
        public UInt32 ulIsAutoAdsorbent;

        /** 窗口个数 */
        public UInt32 ulWindowsNum;

        /** 底图个数 */
        public UInt32 ulBasePicNum;

        /** 窗口信息 */
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_SCREEN_MAX * IMOSSDK.IMOS_TV_WALL_SCREEN_WINDOWS_MAX+50)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public TV_WALL_WINDOWS_INFO[] stMosaicScreenInfo;

        /** LED信息 */
        public TVWALL_VIRTUAL_LED_INFO_S stTVWallVirtualLEDInfo;

        /** 底图信息 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TV_WALL_BASE_PICTURE_MAX)]
        public TVWALL_BASE_PIC_INFO_S[] stTVWallBasePicInfo;
    }
    ;



    /**
      * @struct tagUserLoginIDInfo
      * @brief 用户登录ID信息结构
      * @attention
      */
    [StructLayout(LayoutKind.Sequential)]
    public struct USER_LOGIN_ID_INFO_S
    {
        /** 用户编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_USER_CODE_LEN)]
        public byte[] szUserCode;

        /** 用户登录ID，是用户登录后服务器分配的，它是标记一次用户登录的唯一标识 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szUserLoginCode;

        /** 用户登录的客户端IP地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szUserIpAddress;
    }

    /**
    * @struct tagLoginInfo
    * @brief 用户登录信息结构体
    * @attention 无
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct LOGIN_INFO_S
    {
        /** 用户登录ID信息 */
        public USER_LOGIN_ID_INFO_S stUserLoginIDInfo;

        /** 用户所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 用户所属域名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDomainName;

        /** 用户所属域类型, 取值为#MM_DOMAIN_SUBTYPE_LOCAL_PHYSICAL和#MM_DOMAIN_SUBTYPE_LOCAL_VIRTUAL */
        public UInt32 ulDomainType;
    }

    /**
    * @struct tagXpInfo
    * @brief XP信息结构体
    * @attention 无
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct XP_INFO_S
    {
        /** XP编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szXpCode;

        /** 屏号 */
        public UInt32 ulScreenIndex;

        /** XP第一窗格编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szXpFirstWndCode;

        /** 语音对讲编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szVoiceTalkCode;

        /** 语音广播编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szVoiceBroadcastCode;

        /** SIP通信地址类型，#IMOS_IPADDR_TYPE_IPV4为IPv4类型; #IMOS_IPADDR_TYPE_IPV6为IPv6类型 */
        public UInt32 ulSipAddrType;

        /** SIP服务器通信IP地址，仅在使用XP的时候有效 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szSipIpAddress;

        /** SIP服务器通信端口号 */
        public UInt32 ulSipPort;

        /** 本域服务器编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szServerCode;

    }

    /**
     * @struct tagQueryConditionItem
     * @brief 查询条件项
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct QUERY_CONDITION_ITEM_S
    {
        /** 查询条件类型: #QUERY_TYPE_E */
        public UInt32 ulQueryType;

        /** 查询条件逻辑关系类型: #LOGIC_FLAG_E */
        public UInt32 ulLogicFlag;

        /** 查询条件 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_DATA_MAX_LEN)]
        public byte[] szQueryData;
    }


    /**
     * @struct tagCommonQueryCondition
     * @brief 通用查询条件
     * @attention
    */
    [StructLayout(LayoutKind.Sequential)]
    public struct COMMON_QUERY_CONDITION_S
    {
        /** 查询条件数组中查询条件的实际个数, 最大取值为#IMOS_QUERY_ITEM_MAX_NUM */
        public UInt32 ulItemNum;

        /** 查询条件数组 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_ITEM_MAX_NUM)]
        public QUERY_CONDITION_ITEM_S[] astQueryConditionList;
    }

    /**
     * @struct tagQueryPageInfo
     * @brief 分页请求信息
     * @brief 待查询数据的每条数据项对应一个序号。序号从1开始，连续增加。
     * - 查询到的结果以页的形式返回，每次查询只能返回一页。页包含的行数由ulPageRowNum设定，范围为1~200。
     * - 每次查询，可设置从待查询数据中特定序号（ulPageFirstRowNumber）开始
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct QUERY_PAGE_INFO_S
    {
        /** 分页查询中每页的最大条目数, 不能为0, 也不能大于#IMOS_PAGE_QUERY_ROW_MAX_NUM */
        public UInt32 ulPageRowNum;

        /** 分页查询中第一条数据的序号(即查询从第ulPageFirstRowNumber条数据开始的符合条件的数据), 取值符合ULONG类型的范围即可 */
        public UInt32 ulPageFirstRowNumber;

        /** 是否查询条目总数, BOOL_TRUE时查询; BOOL_FALSE时不查询 */
        public UInt32 bQueryCount;
    }

    /**
     * @struct tagRspPageInfo
     * @brief 分页响应信息
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct RSP_PAGE_INFO_S
    {
        /** 实际返回的条目数 */
        public UInt32 ulRowNum;

        /** 符合条件的总条目数 */
        public UInt32 ulTotalRowNum;
    }

    /**
     * @struct tagOrgResQueryItem
     * @brief 组织节点下资源信息项(查询资源列表时返回)
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct ORG_RES_QUERY_ITEM_S
    {
        /** 资源编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szResCode;

        /** 资源名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szResName;

        /** 资源类型，取值范围为#IMOS_TYPE_E */
        public UInt32 ulResType;

        /** 资源子类型,目前资源子类型只对摄像机和组织有效，对摄像机而言为云台/非云台;
            对组织而言为:1-本物理域，2-本域的虚拟域，3-外域的虚拟域. 4-上级外物理域.
            5-下级外物理域.6-平级外物理域. */
        public UInt32 ulResSubType;

        /** 资源状态，目前只针对物理设备和外域，对外域来说, 该字段代表接收注册状态，取值为
            #IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE */
        public UInt32 ulResStatus;

        /** 资源额外状态，对物理设备来说，枚举为#DEV_EXT_STATUS_E; 对外域来说, 该字段代表主动注册状态:
            取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE */
        public UInt32 ulResExtStatus;

        /** 该资源是否是被划归的资源, 1为被划归的资源; 0为非划归的资源 */
        public UInt32 ulResIsBeShare;

        /** 资源所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 支持的流数目，仅当资源类型为摄像机时有效，0:无效值，1:单流，2:双流 */
        public UInt32 ulStreamNum;

        /** 是否为外域资源，1为外域资源; 0为非外域资源 */
        public UInt32 ulResIsForeign;

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct PRESET_INFO_S
    {
        /** 预置位值, 取值范围为#PTZ_PRESET_MINVALUE~服务器配置文件里配置的预置位最大值 */
        public UInt32 ulPresetValue;

        /** 预置位描述, 需要填写 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szPresetDesc;
    }

    /**
     * @struct tagPTZCtrlCommand
     * @brief 云台控制指令
     * @attention
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct PTZ_CTRL_COMMAND_S
    {
        /** 云台控制命令类型, 取值为#MW_PTZ_CMD_E */
        public UInt32 ulPTZCmdID;

        /** 云台横向转速 */
        public UInt32 ulPTZCmdPara1;

        /** 云台纵向变速 */
        public UInt32 ulPTZCmdPara2;

        /** 控制命令的参数值,保留字段 */
        public UInt32 ulPTZCmdPara3;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PLAY_WND_INFO_S
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szPlayWndCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TIME_SLICE_S
    {
        /** 开始时间 格式为"hh:mm:ss"或"YYYY-MM-DD hh:mm:ss", 视使用情况而定 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szBeginTime;

        /** 结束时间 格式为"hh:mm:ss"或"YYYY-MM-DD hh:mm:ss", 视使用情况而定 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szEndTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct REC_QUERY_INFO_S
    {
        /** 摄像头编码*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** 检索的起始/结束时间 */
        public TIME_SLICE_S stQueryTimeSlice;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECORD_FILE_INFO_S
    {
        /** 文件名 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
        public byte[] szFileName;

        /** 文件起始时间, 满足"%Y-%m-%d %H:%M:%S"格式, 长度限定为24字符 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szStartTime;

        /** 文件结束时间, 满足"%Y-%m-%d %H:%M:%S"格式, 长度限定为24字符 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] szEndTime;

        /** 文件大小, 目前暂不使用 */
        public UInt32 ulSize;

        /** 描述信息, 可不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szSpec;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GET_URL_INFO_S
    {
        /** 摄像机编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** 录像文件名 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
        public byte[] szFileName;

        /** 录像的起始/结束时间, 其中的时间格式为"YYYY-MM-DD hh:mm:ss" */
        public TIME_SLICE_S stRecTimeSlice;

        /** 客户端IP地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szClientIp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VOD_SEVER_IPADDR_S
    {
        /** RTSP服务器IP地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szServerIp;

        /** RTSP服务器端口 */
        public UInt16 usServerPort;

        /** 补齐位, 用于字节对齐, 无实际含义 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] acReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct URL_INFO_S
    {
        /** URL地址*/
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IE_URL_LEN)]
        public byte[] szURL;

        /** 点播服务器的IP地址和端口 */
        public VOD_SEVER_IPADDR_S stVodSeverIP;

        /** 解码插件类型 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] szDecoderTag;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EC_INFO_S
    {
        /** EC编码, EC的唯一标识 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szECName;

        /** EC类型，取值为#IMOS_DEVICE_TYPE_E, 合法取值参见#ulChannum参数的说明 */
        public UInt32 ulECType;

        /** EC通道数量:
            几种常见EC类型对应的通道个数如下:
            EC1101(#IMOS_DT_EC1101_HF)/EC1001(#IMOS_DT_EC1001)/EC1001-HF(#IMOS_DT_EC1001_HF): 1
            EC1501(#IMOS_DT_EC1501_HF)/R1000 (#IMOS_DT_R1000) : 1
            EC2004(#IMOS_DT_EC2004_HF)/VR2004(#IMOS_DT_VR2004): 4
            EC1102(#IMOS_DT_EC1102_HF)/VR1102(#IMOS_DT_VR1102): 2
            EC1801(#IMOS_DT_EC1801_HH): 1
            EC2016(#IMOS_DT_EC2016_HC): 16
            EC2016[8CH](#IMOS_DT_EC2016_HC_8CH): 8
            EC2016[4CH](#IMOS_DT_EC2016_HC_4CH): 4
            HIC5201-H(#IMOS_DT_HIC5201)/HIC5221-H(#IMOS_DT_HIC5221): 1
        */
        public UInt32 ulChannum;

        /** 是否支持组播, 1为支持; 0为不支持 */
        public UInt32 ulIsMulticast;

        /** 低温告警温度上限, 取值为-100~49 */
        public Int32 lTemperatureMax;

        /** 高温告警温度下限, 取值为50~100 */
        public Int32 lTemperatureMin;

        /** 告警使能, 1为使能; 0为不使能 */
        public UInt32 ulEnableAlarm;

        /** EC所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 时间同步方式，默认为1，表示使用H3C的私有同步方式；2表示NTP的同步方式 */
        public UInt32 ulTimeSyncMode;

        /** 时区, 取值为-12~12 */
        public Int32 lTimeZone;

        /** 语言设置，由中心服务器来配置，取值为:#TD_LANGUAGE_E */
        public UInt32 ulLanguage;

        /** 是否启用本地缓存，1表示启用; 0表示不启动，默认值为0 */
        public UInt32 ulEnableLocalCache;

        /** 流套餐, 取值为:#IMOS_STREAM_RELATION_SET_E
            0：MPEG4+MPEG4(#IMOS_SR_MPEG4_MPEG4)
            1：H264主码流(#IMOS_SR_H264_SHARE)
            2：MPEG2+MPEG4(#IMOS_SR_MPEG2_MPEG4)
            3：H264+MJPEG(#IMOS_SR_H264_MJPEG)
            4：MPEG4主码流(#IMOS_SR_MPEG4_SHARE)
            5：MPEG2主码流(#IMOS_SR_MPEG2_SHARE)
            8: MPEG4主码流_D1(#IMOS_SR_STREAM_MPEG4_8D1)
            9：MPEG2+MPEG2(#IMOS_SR_MPEG2_MPEG2)
            11：H264+H264(#IMOS_SR_H264_H264)
        */
        public UInt32 ulEncodeSet;

        /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** 音频输入源，取值为#IMOS_AUDIO_INPUT_SOURCE_E */
        public UInt32 ulAudioinSource;

        /** 语音对讲资源编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szAudioCommCode;

        /** 语音广播资源编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szAudioBroadcastCode;

        /** 设备访问密码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
        public byte[] szDevPasswd;

        /** 设备描述, 目前该字段未使用 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** EC的IP地址, 添加及修改EC不需填写该参数, 查询EC信息时返回该字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szECIPAddr;

        /** EC的在线状态,添加及修改EC不需填写该参数, 查询EC信息时返回该字段, 1为在线; 0为离线 */
        public UInt32 ulIsECOnline;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
        public byte[] szReserve;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct EC_QUERY_ITEM_S
    {
        /** EC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szECName;

        /** EC类型，取值为#IMOS_DEVICE_TYPE_E */
        public UInt32 ulECType;

        /** 设备地址类型，1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** 设备地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** 所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 设备是否在线取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义 */
        public UInt32 ulIsOnline;

        /** 设备扩展状态，取值为#DEV_EXT_STATUS_E */
        public UInt32 ulDevExtStatus;

        /** 是否支持组播, 1为支持组播; 0为不支持组播 */
        public UInt32 ulIsMulticast;

        /** 告警使能, 1为使能告警; 0为不使能告警 */
        public UInt32 ulEnableAlarm;

        /** 流套餐类型，取值为#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulEncodeType;

        /** 制式，取值为#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DC_INFO_S
    {
        /** DC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDCCode;

        /** DC名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDCName;

        /** DC类型, 取值为#IMOS_DEVICE_TYPE_E, 合法取值参见#ulChannum参数的说明 */
        public UInt32 ulDCType;

        /** DC通道数量:
            几种常见DC类型对应的通道个数如下:
            DC1001(#IMOS_DT_DC1001): 1
            DC2004(#IMOS_DT_DC2004_FF)/VL2004(#IMOS_DT_VL2004): 4
            DC1801(#IMOS_DT_DC1801_FH): 1
        */
        public UInt32 ulChannum;

        /** 是否支持组播, 1为支持组播; 0为不支持组播 */
        public UInt32 ulIsMulticast;

        /** 低温告警温度上限, 取值为-100~49 */
        public Int32 lTemperatureMax;

        /** 高温告警温度下限, 取值为50~100 */
        public Int32 lTemperatureMin;

        /** 告警使能, 1为使能告警; 0为不使能告警 */
        public UInt32 ulEnableAlarm;

        /** 所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 时间同步方式，默认为1，表示使用H3C的私有同步方式；2表示NTP的同步方式 */
        public UInt32 ulTimeSyncMode;

        /** 时区, 取值为-12~12 */
        public Int32 lTimeZone;

        /** 语言设置，由中心服务器来配置，取值为:#TD_LANGUAGE_E */
        public UInt32 ulLanguage;

        /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** 流套餐，取值为#IMOS_STREAM_RELATION_SET_E
            以下为解码器流套餐值：
            1：H264(#IMOS_SR_H264_SHARE)
            3: MJPEG(#IMOS_SR_H264_MJPEG)
            4：MEPG4(#IMOS_SR_MPEG4_SHARE)
            5：MEPG2(#IMOS_SR_MPEG2_SHARE)
        */
        public UInt32 ulEncodeSet;

        /** 设备访问密码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
        public byte[] szDevPasswd;

        /** 设备描述, 目前该字段未使用 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** DC的IP地址,添加及修改DC不需填写该参数,查询DC信息时会返回该字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDCIPAddr;

        /** EC的在线状态,添加及修改EC不需填写该参数, 查询EC信息时返回该字段, 1为在线; 0为离线 */
        public UInt32 ulIsDCOnline;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DC_QUERY_ITEM_S
    {
        /** DC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDCCode;

        /** DC名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szDCName;

        /** DC类型，取值为#IMOS_DEVICE_TYPE_E */
        public UInt32 ulDCType;

        /** DC设备地址类型，1-IPv4 2-IPv6 */
        public UInt32 ulDevaddrtype;

        /** DC设备地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDevAddr;

        /** DC所属组织编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
        public byte[] szOrgCode;

        /** 设备是否在线, 取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义 */
        public UInt32 ulIsOnline;

        /** 设备扩展状态，枚举值为#DEV_EXT_STATUS_E */
        public UInt32 ulDevExtStatus;

        /** 是否支持组播, 1为支持组播; 0为不支持组播 */
        public UInt32 ulIsMulticast;

        /** 告警使能, 1为使能告警; 0为不使能告警 */
        public UInt32 ulEnableAlarm;

        /** 流套餐类型，取值为#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulEncodeType;

        /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
        public UInt32 ulStandard;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CAMERA_INFO_S
    {
        /** 摄像机编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCameraCode;

        /** 摄像机名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCameraName;

        /** 摄像机类型, 取值为#CAMERA_TYPE_E */
        public UInt32 ulCameraType;

        /** 摄像机描述, 可不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szCameraDesc;

        /** 云台控制协议, 目前支持的包括:PELCO-D, PELCO-P, ALEC, VISCA, ALEC_PELCO-D, ALEC_PELCO-P, MINKING_PELCO-D, MINKING_PELCO-P */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szPtzProtocol;

        /** 云台地址码, 取值为0~255, 具体取值视云台摄像机的实际地址码而定 */
        public UInt32 ulPtzAddrCode;

        /** 云台协议翻译模式,目前只能填写为#PTZ_TRANSLATE_EP(终端翻译模式) */
        public UInt32 ulPtzTranslateMode;

        /** 经度 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szLongitude;

        /** 纬度 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szLatitude;

        /** 看守位，与设定的预置位的编号相对应 */
        public UInt32 ulGuardPosition;

        /** 自动看守时间, 单位为秒, 最大不超过3600秒, 0表示不看守 */
        public UInt32 ulAutoGuard;

        /** 设备描述, 可不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /** EC编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szECCode;

        /** EC的IP地址,在绑定及修改Camera时,不需填写,查询Camera信息时会返回该字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szECIPAddr;

        /** 所在EC通道索引号, 视具体情况而定 */
        public UInt32 ulChannelIndex;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }


    /**
* @struct tagXpStreamInfo
* @brief XP实时监控流信息结构
* @attention
*/
    [StructLayout(LayoutKind.Sequential)]
    public struct XP_STREAM_INFO_S
    {
        /** 支持的单组播类型，0为仅支持单播，1为既支持单播也支持组播 */
        public UInt32 ulStreamType;

        /** 支持的流传输协议 参见#IMOS_TRANS_TYPE_E。目前XP只支持自适应和TCP */
        public UInt32 ulStreamTransProtocol;

        /** 支持的流传输方式 参见#IMOS_STREAM_SERVER_MODE_E。目前XP只支持自适应和直连优先 */
        public UInt32 ulStreamServerMode;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIN_CHANNEL_S
    {
        /** 视频输入通道描述 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szVinChannelDesc;

        /** 组播地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szMulticastAddr;

        /** 组播端口,范围为：10002-65534，且必须为偶数 */
        public UInt32 ulMulticastPort;

        /** MS选择的适应策略, 1为自适应; 0为非自适应 */
        public UInt32 ulIsAutofit;

        /** 使用MS数目, 视实际情况而定, 当适应策略#ulIsAutofit为自适应, 该值为0;
            当适应策略#ulIsAutofit为非自适应(即指定), 该值为1 */
        public UInt32 ulUseMSNum;

        /** MS编码列表 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (IMOSSDK.IMOS_MS_MAX_NUM_PER_CHANNEL * IMOSSDK.IMOS_DEVICE_CODE_LEN))]
        public byte[,] aszMSCode;

        /** 是否启动图像遮挡检测告警, 1为启动; 0为不启动 */
        public UInt32 ulEnableKeepout;

        /** 是否启动运动检测告警, 1为启动运动检测告警; 0为不启动运动检测告警 */
        public UInt32 ulEnableMotionDetect;

        /** 是否启动视频丢失告警, 1为启动视频丢失告警; 0为不启动视频丢失告警 */
        public UInt32 ulEnableVideoLost;

        /** 绑定的串口编号，如无则填写0 */
        public UInt32 ulSerialIndex;

        /** 亮度，取值为（0~255） */
        public UInt32 ulBrightness;

        /** 对比度，取值为（0~255） */
        public UInt32 ulContrast;

        /** 饱和度，取值为（0~255） */
        public UInt32 ulSaturation;

        /** 色调，取值为（0~255） */
        public UInt32 ulTone;

        /** 是否启动静音, 1为启动静音; 0为不启动静音 */
        public UInt32 ulAudioEnabled;

        /** 音频编码, 取值为#IMOS_AUDIO_FORMAT_E */
        public UInt32 ulAudioCoding;

        /** 音频声道, 取值为#IMOS_AUDIO_CHANNEL_TYPE_E */
        public UInt32 ulAudioTrack;

        /** 音频采样率, 取值为#IMOS_AUDIO_SAMPLING_E */
        public UInt32 ulSamplingRate;

        /** 音频码率, 不可配置 */
        public UInt32 ulAudioCodeRate;

        /** 音频增益值，取值为（0~255） */
        public UInt32 ulIncrement;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEO_STREAM_S
    {
        /** 码流类型, 取值为#IMOS_STREAM_TYPE_E, 目前仅支持#IMOS_ST_TS */
        public UInt32 ulStreamType;

        /** 流索引，1为主流，2为副流 */
        public UInt32 ulStreamIndex;

        /** 流使能标识, 1为使能; 0为非使能 */
        public UInt32 ulEnabledFlag;

        /** 流传输方式, 取值为#IMOS_TRANS_TYPE_E */
        public UInt32 ulTranType;

        /** 编码格式, 取决于具体的流套餐值, 取值为#IMOS_VIDEO_FORMAT_E */
        public UInt32 ulEncodeFormat;

        /** 分辨率, 取值为#IMOS_PICTURE_SIZE_E */
        public UInt32 ulResolution;

        /** 码率 */
        public UInt32 ulBitRate;

        /** 帧率,可取的值有1, 3, 5, 8, 10, 15, 20, 25, 30 */
        public UInt32 ulFrameRate;

        /** GOP模式, 取值为#IMOS_GOP_TYPE_E */
        public UInt32 ulGopMode;

        /** I帧间隔, 取决于GOP模式值, 当GOP模式为#IMOS_GT_I时, I帧间隔为1; 当GOP模式为#IMOS_GT_IP时, I帧间隔为10~50 */
        public UInt32 ulIFrameInterval;

        /** 图像质量, 取值为#IMOS_VIDEO_QUALITY_E */
        public UInt32 ulImageQuality;

        /** 流编码模式, 取值为#IMOS_ENC_MODE_E */
        public UInt32 ulEncodeMode;

        /** 优先级, 仅在编码模式为#IMOS_EM_CBR时可设置该值, 取值为#IMOS_CBR_ENC_MODE_E */
        public UInt32 ulPriority;

        /** 码流平滑，取值为#IMOS_STREAM_SMOOTH_E */
        public UInt32 ulSmoothValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AREA_SCOPE_S
    {
        /** 左上角x坐标, 取值为0~100 */
        public UInt32 ulTopLeftX;

        /** 左上角y坐标, 取值为0~100 */
        public UInt32 ulTopLeftY;

        /** 右下角x坐标, 取值为0~100 */
        public UInt32 ulBottomRightX;

        /** 右下角y坐标, 取值为0~100 */
        public UInt32 ulBottomRightY;

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEO_AREA_S
    {
        /** 区域索引, 取值为1~4 */
        public UInt32 ulAreaIndex;

        /** 是否使能, 1为使能; 0为非使能 */
        public UInt32 ulEnabledFlag;

        /** 灵敏度, 1～5级，1级灵敏度最高。该值仅对运动检测区域有效 */
        public UInt32 ulSensitivity;

        /** 区域坐标 */
        public AREA_SCOPE_S stAreaScope;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DETECT_AREA_S
    {
        /** 遮挡检测区域 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astCoverDetecArea;

        /** 运动检测区域 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astMotionDetecArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_CHANNEL_INDEX_S
    {
        /** 设备编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] szDevCode;

        /** 设备类型，当设备为编码器时, 取值为#IMOS_TYPE_EC; 当设备为解码器时, 取值为#IMOS_TYPE_DC */
        public UInt32 ulDevType;

        /** 通道索引号，分为:音频视频通道、串口通道、开关量通道(输入/输出), 取值视具体情况定 */
        public UInt32 ulChannelIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCREEN_INFO_S
    {
        /** 监视器编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szScreenCode;

        /** 监视器名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScreenName;

        /** 监视器描述, 可不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szDevDesc;

        /**  DC的IP地址, 在绑定及修改Screen时, 不需填写; 查询Screen信息时会返回该字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szDCIPAddr;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VOUT_CHANNEL_S
    {
        /** 逻辑输出通道索引, 取值为1~#IMOS_DC_LOGIC_CHANNEL_MAXNUM */
        public UInt32 ulVoutChannelindex;

        /** 逻辑输出通道描述, 可不填 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] szVoutChannelDesc;

        /** 是否使能, 1为使能; 0为不使能 */
        public UInt32 ulEnable;

        /** 码流类型, 取值为#IMOS_STREAM_TYPE_E, 目前仅支持#IMOS_ST_TS */
        public UInt32 ulStreamType;

        /** 流传输模式, 取值为#IMOS_TRANS_TYPE_E */
        public UInt32 ulTranType;

        /** 是否启动防抖动, 1为启动; 0为不启动 */
        public UInt32 ulEnableJitterBuff;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_TIME_S
    {
        /** 时间OSD索引, 固定为1 */
        public UInt32 ulOsdTimeIndex;

        /** 时间OSD使能, 1为使能; 0为非使能 */
        public UInt32 ulEnableFlag;

        /** 时间OSD时间格式 */
        public UInt32 ulOsdTimeFormat;

        /** 时间OSD日期格式 */
        public UInt32 ulOsdDateFormat;

        /** 时间OSD颜色, 取值为#IMOS_OSD_COLOR_E */
        public UInt32 ulOsdColor;

        /** 时间OSD透明度, 取值为#IMOS_OSD_ALPHA_E */
        public UInt32 ulTransparence;

        /** 时间OSD区域坐标 */
        public AREA_SCOPE_S stAreaScope;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_NAME_S
    {
        /** 是否使能场名OSD, 1为使能; 0为非使能 */
        public UInt32 ulEnabledFlag;

        /** 场名OSD索引, 固定为1 */
        public UInt32 ulOsdNameIndex;

        /** 场名OSD颜色, 取值为#IMOS_OSD_COLOR_E */
        public UInt32 ulOsdColor;

        /** 场名OSD透明度, 取值为#IMOS_OSD_ALPHA_E */
        public UInt32 ulTransparence;

        /** 场名OSD区域坐标 */
        public AREA_SCOPE_S stAreaScope;

        /** 第一个(主)场名OSD类型, 取值为#IMOS_INFO_OSD_TYPE_E */
        public UInt32 ulOsdType1;

        /** 第一个(主)场名OSD内容，对文字，该值为字符串，最长为20字符。对图片，该值为OSD图片编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOsdString1;

        /** 第二个(辅)场名OSD类型, 取值为#IMOS_INFO_OSD_TYPE_E */
        public UInt32 ulOsdType2;

        /** 第二个(辅)场名OSD内容，对文字，该值为字符串，最长为20字符。对图片，该值为OSD图片编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOsdString2;

        /** (第一个和第二个)场名OSD之间的切换时间, 单位为秒, 取值为0~300。取值为0, 表示只显示第一个(主)OSD */
        public UInt32 ulSwitchIntval;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] szReserve;
    }

    /// <summary>
    /// 告警的信息结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AS_ALARMPUSH_UI_S
    {
        /// <summary>
        /// 告警事件编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] byAlarmEventCode;

        /// <summary>
        /// 告警源编码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
        public byte[] byAlarmSrcCode;

        /// <summary>
        /// 告警源名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] byAlarmSrcName;

        /// <summary>
        /// 使能后名字
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] byActiveName;

        /// <summary>
        /// 告警类型 ALARM_TYPE_E 在sdk_def.h中定义
        /// </summary>
        public UInt32 ulAlarmType;

        /// <summary>
        /// 告警级别 ALARM_SEVERITY_LEVEL_E 在sdk_def.h中定义
        /// </summary>
        public UInt32 ulAlarmLevel;

        /// <summary>
        /// 告警触发时间
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
        public byte[] byAlarmTime;

        /// <summary>
        /// 告警描述信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
        public byte[] byAlarmDesc;

    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_MASK_AREA_S
    {
        /** 遮盖区域 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_MASK_AREA_MAXNUM)]
        public VIDEO_AREA_S[] astMaskArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSD_INFO_S
    {
        /** 时间OSD */
        public OSD_TIME_S stOSDTime;

        /** 场名OSD */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_OSD_NAME_MAXNUM)]
        public OSD_NAME_S[] astOSDName;

        /** 遮盖区域 */
        public OSD_MASK_AREA_S stOSDMaskArea;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PHYOUT_CHANNEL_S
    {
        /** 工作模式，取值为1或4，代表BNC口的分屏数 */
        public UInt32 ulPhyoutMode;

        /** 视频解码格式，取值为#IMOS_VIDEO_FORMAT_E */
        public UInt32 ulDecodeFormat;

        /** 音频格式，取值为#IMOS_AUDIO_FORMAT_E */
        public UInt32 ulAudioFormat;

        /** 声道设置，取值为#IMOS_AUDIO_CHANNEL_TYPE_E */
        public UInt32 ulAudioTrack;

        /** 是否启用语音功能, 1为启用; 0为不启用 */
        public UInt32 ulAudioEnabled;

        /** 输出音量, 取值为1~7 */
        public UInt32 ulVolume;

        /** 音频输出选择, 视工作模式参数#ulPhyoutMode而定。如果工作模式取值为1, 则该值为1; 如果工作模式取值为4, 则该值取值为1~4 */
        public UInt32 ulOutputIndex;

        /** 最多监视器数量, 表示该物理通道最多可绑定的监视器数量, 目前固定为1 */
        public UInt32 ulMaxScreenNum;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VINCHNL_BIND_CAMERA_S
    {
        /** 设备通道索引信息 */
        public DEV_CHANNEL_INDEX_S stECChannelIndex;

        /** 摄像机信息 */
        public CAMERA_INFO_S stCameraInfo;

        /** 视频输入通道信息 */
        public VIN_CHANNEL_S stVinChannel;

        /** OSD信息 */
        public OSD_INFO_S stOSDInfo;

        /** 视频流数组中视频流的实际数目, 最大取值为#IMOS_STREAM_MAXNUM, 视具体流套餐值而定 */
        public UInt32 ulVideoStreamNum;

        /** 视频流信息数组 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_STREAM_MAXNUM)]
        public VIDEO_STREAM_S[] astVideoStream;

        /** 检测区域，包含运动检测以及遮挡检测区域 */
        public DETECT_AREA_S stDetectArea;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct VOUTCHNL_BIND_SCREEN_S
    {
        /** 设备通道索引信息 */
        public DEV_CHANNEL_INDEX_S stDCChannelIndex;

        /** 监视器信息 */
        public SCREEN_INFO_S stScreenInfo;

        /** 逻辑输出通道信息 */
        public VOUT_CHANNEL_S stVoutChannel;

        /** OSD信息 */
        public OSD_INFO_S stOSDInfo;

        /** 物理输出通道信息 */
        public PHYOUT_CHANNEL_S stPhyoutChannel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct XP_RUN_INFO_EX_S
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public Byte[] szPortCode;     /**< 通道资源编码 */

        public UInt32 ulErrCode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CAM_AND_CHANNEL_QUERY_ITEM_S
    {
        /** 设备通道索引信息 */
        public DEV_CHANNEL_INDEX_S stECChannelIndex;

        /** 摄像机编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szCamCode;

        /** 摄像机名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szCamName;

        /** 摄像机类型, 取值为#CAMERA_TYPE_E */
        public UInt32 ulCamType;

        /** 云台控制协议 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szPtzProtocol;

        /** 云台地址码, 取值为0~255, 具体取值视云台摄像机的实际地址码而定 */
        public UInt32 ulPtzAddrCode;

        /** 组播地址 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
        public byte[] szMulticastAddr;

        /** 组播端口, 范围为：10002-65534 */
        public UInt32 ulMulticastPort;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SCR_AND_CHANNEL_QUERY_ITEM_S
    {
        /** 设备通道索引信息 */
        public DEV_CHANNEL_INDEX_S stDCChannelIndex;

        /** 监视器编码 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
        public byte[] szScrCode;

        /** 监视器名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szScrName;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SPLIT_SCR_INFO_S
    {
        /** 分屏模式,取值为#SPLIT_SCR_MODE_E */
        public UInt32 ulSplitScrMode;

        /** 分屏编码(全屏时有效) */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
        public byte[] szSplitScrCode;

        /** 是否"自动切换主辅流"(#BOOL_TRUE 是,#BOOL_FALSE 否)  */
        public UInt32 bSwitchStream;

        /** 预留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] szReserve;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RES_ITEM_V2_S
    {
        /** V1资源信息项 */
        public ORG_RES_QUERY_ITEM_S stResItemV1;

        /** 资源所属组织的名称 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
        public byte[] szOrgName;

        /** 资源属性信息，对于资源类型是摄像机时，取值为#CAMERA_ATTRIBUTE_E，其他资源类型该字段暂未使用 */
        public UInt32 ulResAttribute;

        /** 本域非ECR HF系列的摄像机或者监视器所在的设备的流套餐，
            其他资源类型,或者通用查询条件IS_QUERY_ENCODESET没有填写, 或者填了"不查询", 该字段均为无效值#IMOS_SR_INVALID
            取值为#IMOS_STREAM_RELATION_SET_E */
        public UInt32 ulDevEncodeSet;

        /** 保留字段 */
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 184)]
        public byte[] szReserve;

        public static RES_ITEM_V2_S GetInstance(ORG_RES_QUERY_ITEM_S stRes)
        {
            RES_ITEM_V2_S stRes_V2 = new RES_ITEM_V2_S();
            stRes_V2.stResItemV1 = stRes;
            stRes_V2.szOrgName = new byte[IMOSSDK.IMOS_NAME_LEN];
            stRes_V2.szReserve = new byte[184];
            stRes_V2.ulDevEncodeSet = 0;
            stRes_V2.ulResAttribute = 0;
            return stRes_V2;
        }
    }
    public struct XP_RECT_S
    {
        public int lLeft;

        public int lTop;

        public int lRight;

        public int lBottom;
    }

    public class IMOSSDK
    {


        /*@brief 扩展字段长度 */
        public const int IMOS_RESERVED_LEN = 64;

        /** A8设备通道最大数量 */
        public const int A8_CHANNEL_MAX_NUM = 32;
        /** 单个A8设备电视墙最大数量 */
        public const int A8_TV_WALL_MAX_NUM = 8;
        /** 单个电视墙最大显示器(拼接屏)数 */
        public const int IMOS_TV_WALL_SCREEN_MAX = 128;
        /** @brief A设备最大通道号数量 */
        public const int A_CHANNEL_MAX_NUM = 512;

        /** @brief A设备通道号及类型最大长度 */
        public const int A_CHANNEL_INDEX_TYPE_LEN = 15;


        /** 单个屏最大显示窗口个数 */
        public const int IMOS_TV_WALL_SCREEN_WINDOWS_MAX = 4;

        /** 单个电视墙LED 最大数量*/
        public const int IMOS_TV_WALL_LED_CONTENT_MAX = 4;

        /** 单个电视墙最大窗口个数 */
        public const int IMOS_TV_WALL_WINDOWS_MAX = (IMOS_TV_WALL_SCREEN_MAX * IMOS_TV_WALL_SCREEN_WINDOWS_MAX);

        /** 单个电视墙最大上传底图数 */
        public const int IMOS_TV_WALL_BASE_PICTURE_MAX = 8;


        /*@brief imos_simple_time 时间信息字符串长度 "hh:mm:ss" */
        public const int IMOS_SIMPLE_TIME_LEN = 12;
        /*@brief imos_simple_date 日期信息字符串长度 "YYYY-MM-DD"*/
        public const int IMOS_SIMPLE_DATE_LEN = 12;
        /** A8 EDID文件最大大小 */
        public const int A8_EDID_MAX_SIZE = 256;
        /** 默认电视墙场景ID */
        public const int CS_TVWALL_SCENE_ID = 99;
        /** 电视墙场景最大数量 */
        public const int TVWALL_SCENE_MAX_NUM = 32;


        /** 电视墙编码默认头 */
        public const string TVWALL_DEFAULT_CODE = "tvwall";

        /** 电视墙窗口编码默认头 */
        public const string WINDOW_DEFAULT_CODE = "window";

        /** 电视墙摄像机编码默认头 */
        public const string CAMERA_DEFAULT_CODE = "cam";

        /** 电视墙监视器编码默认头 */
        public const string SCREEN_DEFAULT_CODE = "screen";

        /** 电视墙场景组编码默认头 */
        public const string TVWALL_SCENE_GROUP_DEFAULT_CODE = "scenegroup";

        /** 电视墙不可用编码 */
        public const string A8_TVWALL_DISABLE_CODE = "DISABLE_TVWALL_CODE";

        /** 电视墙监视屏不可用编码 */
        public const string A8_INVILD_SCREEN_CODE = "INVALID_SCREEN_CODE";

        /** 电视墙摄像机不可用编码 */
        public const string A8_INVILD_CAMERA_CODE = "INVALID_CAMERA_CODE";

        /* End added by y01359 2014-10-09 for A8 */

        /** @brief 选项值内存块长度最小值 */
        public const int IMOS_OPTION_BUFFER_LEN_MIN = 1;

        /** @brief 选项值内存块长度最大值 */
        public const int IMOS_OPTION_BUFFER_LEN_MAX = 64;


        /** @brief 一个电视墙包含监视器的最大数目 */
        public const int IMOS_MONITOR_MAXNUM_PER_WALL = 256;


        /** @brief 一个电视墙包含的分屏的最大数目 */
        public const int IMOS_SPLIT_MAXNUM_PER_WALL = ((int)SPLIT_SCR_MODE_E.SPLIT_SCR_MODE_MAX * IMOS_MONITOR_MAXNUM_PER_WALL);


        public const int IMOS_NAME_LEN = 64;


        public const int IMOS_PHONE_LEN = 64;

        public const int IMOS_CODE_LEN = 48;

        public const int IMOS_IPADDR_LEN = 64;

        public const int IMOS_STRING_LEN_256 = 256;

        /*@brief 资源编码信息字符串长度*/
        public const int IMOS_RES_CODE_LEN = IMOS_CODE_LEN;

        /*@brief 设备编码信息字符串长度*/
        public const int IMOS_DEVICE_CODE_LEN = IMOS_CODE_LEN;

        /*@brief 用户编码信息字符串长度*/
        public const int IMOS_USER_CODE_LEN = IMOS_CODE_LEN;

        /*@brief 域编码信息字符串长度*/
        public const int IMOS_DOMAIN_CODE_LEN = IMOS_CODE_LEN;

        /*@brief 域名称信息字符串长度 */
        public const int IMOS_DOMAIN_NAME_LEN = IMOS_NAME_LEN;

        /*@brief 权限编码信息字符串长度*/
        public const int IMOS_AUTH_CODE_LEN = IMOS_CODE_LEN;

        //每次查询时返回的最大符合条件的结果的个数
        public const int QUERY_ITEM_MAX = 200;

        /*@brief imos_time 时间信息字符串长度 "2008-10-02 09:25:33.001 GMT" */
        public const int IMOS_TIME_LEN = 32;

        /*@brief 文件名长度 */
        public const int IMOS_FILE_NAME_LEN = 64;

        public const uint ERR_XP_FAIL_TO_SETUP_PROTOCOL = 0x0007B0;      /**< 建立流控协商失败 */
        public const uint ERR_XP_FAIL_TO_PLAY_PROTOCOL = 0x0007B1;      /**< 流控协商播放失败 */
        public const uint ERR_XP_FAIL_TO_PAUSE_PROTOCOL = 0x0007B2;      /**< 流控协商暂停失败 */
        public const uint ERR_XP_FAIL_TO_STOP_PROTOCOL = 0x0007B3;      /**< 停止流控协商失败 */
        public const uint ERR_XP_RTSP_COMPLETE = 0x0007B4;      /**< RTSP播放或下载完成 */
        public const uint ERR_XP_RTSP_ABNORMAL_TEATDOWN = 0x0007B5;      /**< RTSP异常下线，服务器读取文件错误或数据被覆写 */
        public const uint ERR_XP_RTSP_KEEP_LIVE_TIME_OUT = 0x0007B6;      /**< RTSP保活失败 */
        public const uint ERR_XP_RTSP_ENCODE_CHANGE = 0x0007B7;      /**< RTSP中码流格式切换 */
        public const uint ERR_XP_RTSP_DISCONNECT = 0x0007B8;      /**< RTSP连接断开，点播回放或下载已自动终止，请检查网络 */

        public const uint ERR_XP_DISK_CAPACITY_WARN = 0x00079B;      /**< 硬盘剩余空间低于阈值 */
        public const uint ERR_XP_DISK_CAPACITY_NOT_ENOUGH = 0x00079C;     /**< 硬盘剩余空间不足，无法继续业务 */
        public const uint ERR_XP_FAIL_TO_WRITE_FILE = 0x000723;     /**< 写文件操作失败 */
        public const uint ERR_XP_FAIL_TO_PROCESS_MEDIA_DATA = 0x0007A9;   /**< 媒体数据处理失败 */
        public const uint ERR_XP_NOT_SUPPORT_MEDIA_ENCODE_TYPE = 0x000735;/**< 播放通道的媒体编码格式不支持此操作 */
        public const uint ERR_XP_MEDIA_RESOLUTION_CHANGE = 0x000736;      /**< 播放通道的媒体流分辨率发生变化 */

        /*@brief imos_description 描述信息字符串长度 */
        public const int IMOS_DESC_LEN = (128 * 3);

        public const int IMOS_IE_URL_LEN = 512;

        public const int IMOS_PASSWD_ENCRYPT_LEN = 64;

        public const int IMOS_QUERY_ITEM_MAX_NUM = 16;

        public const int IMOS_QUERY_DATA_MAX_LEN = 64;

        public const int IMOS_DEV_STATUS_ONLINE = 1;

        public const int IMOS_DEV_STATUS_OFFLINE = 2;

        public const int IMOS_STREAM_MAXNUM = 2;

        public const int IMOS_MS_MAX_NUM_PER_CHANNEL = 1;

        public const int IMOS_DETECT_AREA_MAXNUM = 4;

        public const int IMOS_MASK_AREA_MAXNUM = 4;

        public const int IMOS_OSD_NAME_MAXNUM = 1;



        public static LOGIN_INFO_S stLoginInfo;
        public static XP_INFO_S stXpInfo;
        public static PLAY_WND_INFO_S[] astPlayWndInfo = new PLAY_WND_INFO_S[25];


        public static System.Timers.Timer timerKeepalive;

        public delegate void MethodInvoke1<T>(T Param);

        public static string UTF8ToUnicode(byte[] bufferIn)
        {

            byte[] buffer = Encoding.Convert(Encoding.UTF8, Encoding.Default, bufferIn, 0, bufferIn.Length);
            return Encoding.Default.GetString(buffer, 0, buffer.Length);
        }

        public static byte[] UnicodeToUTF8(string buffIn)
        {
            byte[] buffer = Encoding.Default.GetBytes(buffIn);
            return Encoding.Convert(Encoding.Default, Encoding.UTF8, buffer, 0, buffer.Length);
        }

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Initiate(String strServerIP, UInt32 ulServerPort, UInt32 bUIFlag, UInt32 bXPFlag);

        /// <summary>
        /// xp信息回调，主要用于接收一些XP相关信息
        /// </summary>
        /// <param name="stUserLoginIDInfo"></param>
        /// <param name="ulRunInfoType"></param>
        /// <param name="ptrInfo"></param>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void XP_RUN_INFO_CALLBACK_EX_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, UInt32 ulRunInfoType, IntPtr pParam);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Encrypt(String strInput, UInt32 ulInLen, IntPtr ptrOutput);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Login(String strUserLoginName, String strPassword, String strIpAddr, IntPtr ptrSDKLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_LoginEx(String strUserLoginName, String strPassword, String srvIpAddr, String cltIpAddr, IntPtr ptrSDKLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PlaySound(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_CleanUp(IntPtr pstUserLogIDInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UserKeepAlive(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCameraCode, byte[] szMonitorCode, UInt32 ulStreamType, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szMonitorCode, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryResourceList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, UInt32 ulResType, UInt32 ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrResList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]

        public static extern UInt32 IMOS_QueryResourceListV2(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szOrgCode, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, ref RSP_PAGE_INFO_S pstRspPageInfo, IntPtr pstResList);

        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryResourceListV2(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, string orgCode, ref COMMON_QUERY_CONDITION_S stQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, ref RSP_PAGE_INFO_S stRspPage, IntPtr ptrResList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_Logout(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void IMOS_LogoutEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void IMOS_GetChannelCode(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, IntPtr pcChannelCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ReleasePtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, UInt32 bReleaseSelf);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryPresetList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref QUERY_PAGE_INFO_S stQueryPageInfo, ref RSP_PAGE_INFO_S ptrRspPage, IntPtr pstPresetList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, ref PRESET_INFO_S pstPreset);

        /// <summary>
        /// 删除预置位
        /// </summary>
        /// <param name="pstUserLogIDInfo">用户登录ID信息标识</param>
        /// <param name="szCamCode">摄像机编码</param>
        /// <param name="ulPresetValue">预置位值</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetValue);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UsePreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetNum);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PtzCtrlCommand(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref PTZ_CTRL_COMMAND_S stPTZCommand);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulPlayWndNum, IntPtr ptrPlayWndInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayWnd(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr hWnd);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_RecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref REC_QUERY_INFO_S stSDKRecQueryInfo, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrSDKRecordFileInfo);

        /// <summary>
        /// 注册回调函数
        /// </summary>
        /// <param name="pstUserLoginIDInfo">登入信息</param>
        /// <param name="ptrCallBack">回调函数</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_RegCallBackPrcFunc(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, IntPtr pfnCallBackProc);

        /// <summary>
        /// 告警回调函数
        /// </summary>
        /// <param name="ulProcType">告警类型</param>
        /// <param name="ptrParam">返回的数据指针</param>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void CALL_BACK_PROC_PF(UInt32 ulProcType, IntPtr ptrParam);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetRecordFileURL(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref GET_URL_INFO_S stSDKGetUrlInfo, ref URL_INFO_S stUrlInfo);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_FreeChannelCode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, Byte[] pcChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenVodStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szVodUrl, byte[] szServerIP, UInt16 usServerPort, UInt32 ulProtl);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_PausePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResumePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlaySpeed(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulPlaySpeed);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRunMsgCB(IntPtr ptrRunInfoFunc);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetDownloadTime(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, String pcDownloadID, byte[] pszTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OneByOne(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownloadID);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SnatchOnce(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SnatchOnceEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat, UInt32 ulInterval);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartRecordEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat, IntPtr ptrFilePostfix);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetVideoEncodeType(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrVideoEncodeType);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrRecvPktNum, IntPtr ptrLostPktNum);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrAllFrameNum, IntPtr ptrLostFrameNum);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ResetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownUrl, byte[] pcServerIP, ushort usServerPort, UInt32 ulProtl, UInt32 ulDownMediaSpeed, byte[] pcFileName, UInt32 ulFileFormat, byte[] pcChannelCode);


        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);



        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetDecoderTag(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, byte[] pcDecorderTag);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrFrameRate);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetBitRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrBitRate);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopSound(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetVolume(UInt32 ulVolume);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetVolume(IntPtr ptrVolume);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetFieldMode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulFieldMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AdjustAllWaveAudio(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulCoefficient);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AdjustPktSeq(Boolean bAdjust);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRenderMode(UInt32 ulRenderMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetRealtimeFluency(UInt32 ulFluency);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_XP_SetDeinterlaceMode(UInt32 ulPort, UInt32 ulDeinterlaceMode);

        [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetPixelFormat(UInt32 ulPixelFormat);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigXpStreamInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref XP_STREAM_INFO_S pstXpStreamInfo);



        //EC Camera 配置接口

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AddEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo, UInt32 IsEncodeChange);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryEcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrEcList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode, IntPtr ptrEcInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szEcCode, ref EC_INFO_S pstEcInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, string ecCode, ref EC_INFO_S pstEcInfo);
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_BindCameraToVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VINCHNL_BIND_CAMERA_S stVinChnlAndCamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCameraAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr stQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrCamAndChannelList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref CAMERA_INFO_S stCamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UnBindCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, IntPtr ptrCameraInfo);
        [DllImport("imos_sdk.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_QueryCamera(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, string cameraCode, ref CAMERA_INFO_S pstCameraInfo);
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryCameraEX(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, IntPtr ptrCameraInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIN_CHANNEL_S stVideoInChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoInChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIDEO_STREAM_S stVideoStreamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrStreamNum, IntPtr ptrVideoStreamInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMaskAreaNum, ref VIDEO_AREA_S stMaskArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMaskAreaNum, IntPtr ptrMaskArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMotionDetectAreaNum, ref VIDEO_AREA_S stMotionDetectArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMotionDetectAreaNum, IntPtr ptrMotionDetectArea);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref OSD_TIME_S stTimeOSD);

        /// <summary>
        /// 完成订阅推送功能
        /// </summary>
        /// <param name="stUserLoginIDInfo">登入信息</param>
        /// <param name="ulSubscribePushType">订阅类型</param>
        /// <returns></returns>
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SubscribePushInfo(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, UInt32 ulSubscribePushType);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrTimeOSD);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulNameOSDNum, ref OSD_NAME_S stNameOSD);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrNameOSDNum, IntPtr ptrNameOSD);



        //DC Screen 配置接口

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_AddDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo, UInt32 IsEncodeChange);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_DelDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrDcList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode, IntPtr ptrDcInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_BindScreenToVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VOUTCHNL_BIND_SCREEN_S stVOUTChnlAndScrInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryScreenAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrVOUTChnlAndScrList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModifyScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref SCREEN_INFO_S stScrInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_UnBindScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, IntPtr ptrScreenInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulVideoOutNum, ref VOUT_CHANNEL_S stVideoOutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoOutNum, IntPtr ptrVideoOutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ConfigDCPhyOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref PHYOUT_CHANNEL_S stPhyoutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrPhyoutChannelInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QuerySplitScrInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, ref SPLIT_SCR_INFO_S ptrSplitInfo);


        //A8大屏控制
        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_OpenWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO st_TV_Wall_Windows_Info);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_CloseWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcWindowCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_ModWindow(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO st_TV_Wall_Windows_Info, bool bSingleDB);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetWindowLevel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref TV_WALL_WINDOWS_INFO pstTVWallInfo, UInt32 ulWindowNum);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StopA8Monitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szMonitorCode, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_StartA8Monitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCameraCode, byte[] szMonitorCode, UInt32 ulStreamType, UInt32 ulOperateCode);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, IntPtr pstTVWallInfo);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallListA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, IntPtr pstRspPageInfo, IntPtr pstTVWallList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetTVWallTransparency(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcWindowCode, UInt32 ulTransparency);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_TVWallSceneEnable(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, UInt32 ulSceneID);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryTVWallSceneListA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] pcTVWallCode, UInt32 pulSceneNum, IntPtr pstTVWallSceneList);

        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryA8(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szA8Code, IntPtr pstA8Info);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_QueryA8List(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, ref RSP_PAGE_INFO_S pstRspPageInfo, ref A8_INFO_S pstA8List);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetA8SmoothDisplay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, UInt32 ulIsSmoothDisplay);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_GetA8SmoothDisplay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr pulIsSmoothDisplay);


        [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 IMOS_SetTVWallAutoAdsorbent(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szTVWallCode, UInt32 ulIsAutoAdsorbent);

        [DllImport("xp_frame.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_SetDigitalZoom(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, string strChannelCode, IntPtr hWnd, ref XP_RECT_S stRect);

        [DllImport("xp_frame.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern uint IMOS_SetDigitalZoom(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, string strChannelCode, IntPtr hWnd, IntPtr stRect);

    }
}
