using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject
{
    /// <summary>
    /// 获取系统配置信息
    /// </summary>
    public static class AppConfig
    {
        
        #region 首页图表类接口
        /// <summary>
        /// 根据设备分类统计计数
        /// </summary>
        public static readonly string GetErrorDeviceCount = ConfigurationManager.AppSettings["API_GET_ERROR_DEVICE_COUNT"];

        public static readonly string GetErrorDeviceTypeCount = ConfigurationManager.AppSettings["API_GET_ERROR_DEVICE_TYPE_COUNT"];
        /// <summary>
        /// 获取事件计数
        /// </summary>
        public static readonly string GetEventCount = ConfigurationManager.AppSettings["API_GET_EVENT_COUNT"];
        /// <summary>
        /// 按事件类别-本年度
        /// </summary>
        public static readonly string GetEventByType = ConfigurationManager.AppSettings["API_GET_EVENT_BYTYPE"];
        /// <summary>
        /// 事件比率-本年度
        /// </summary>
        public static readonly string GetEventRate = ConfigurationManager.AppSettings["API_GET_EVENT_RATE"];
        /// <summary>
        /// 根据设备类型楼宇分布
        /// </summary>
        public static readonly string GetBuildingCount = ConfigurationManager.AppSettings["API_GET_BUILDING_COUNT"];
        /// <summary>
        /// 根据设备分类楼宇分布
        /// </summary>
        public static readonly string GetBuildingCountByClass = ConfigurationManager.AppSettings["API_GET_BUILDING_COUNT_BY_CLASS"];
        /// <summary>
        /// 根据设备分类查询发生趋势
        /// </summary>
        public static readonly string GetEventRateByType= ConfigurationManager.AppSettings["API_GET_EVENT_RATE_BYTYPE"];
        /// <summary>
        /// 根据设备类型查询发生趋势
        /// </summary>
        public static readonly string GetEventRateByClass = ConfigurationManager.AppSettings["API_GET_EVENT_RATE_BYCLASS"];
        /// <summary>
        /// 根据设备类型故障设备清单
        /// </summary>
        public static readonly string GetErrorDeviceByType = ConfigurationManager.AppSettings["API_GET_ERROR_DEVICE_BYTYPE"];
        /// <summary>
        /// 根据设备分类故障设备清单
        /// </summary>
        public static readonly string GetErrorDeviceByClass = ConfigurationManager.AppSettings["API_GET_ERROR_DEVICE_BYCLASS"];
        /// <summary>
        /// 设备正常率
        /// </summary>
        public static readonly string GetErrorRate = ConfigurationManager.AppSettings["API_GET_ERROR_RATE"];
        /// <summary>
        /// 错误时间分布（小类）
        /// </summary>
        public static readonly string GetErrorTimeByType = ConfigurationManager.AppSettings["API_GET_ERROR_TIME_BYTYPE"];
        /// <summary>
        /// 错误时间分布（大类）
        /// </summary>
        public static readonly string GetErrorTimeByClass = ConfigurationManager.AppSettings["API_GET_ERROR_TIME_BYCLASS"];


        #endregion

        #region SystemInfo
        /// <summary>
        /// 获取产品名称
        /// </summary>
        public static readonly string ProductName = ConfigurationManager.AppSettings["ProductName"];
        /// <summary>
        /// 获取标题栏名称
        /// </summary>
        public static readonly string TitleText = ConfigurationManager.AppSettings["TitleText"];

        public static readonly string PlayerUrl = ConfigurationManager.AppSettings["PlayerUrl"];
        #endregion SystemInfo

        #region Server
        /// <summary>
        /// 服务器IP
        /// </summary>
        public static readonly string ServerIP = ConfigurationManager.AppSettings["ServerIp"];
        /// <summary>
        /// 端口
        /// </summary>
        public static readonly string ServerPort = ConfigurationManager.AppSettings["ServerPort"];
        /// <summary>
        /// 视频服务器
        /// </summary>
        public static readonly string SDKServerIp = ConfigurationManager.AppSettings["SDKServerIp"];
        /// <summary>
        /// 视频服务器用户名
        /// </summary>
        public static readonly string SDKUserName = ConfigurationManager.AppSettings["SDKUserName"];
        /// <summary>
        /// 视频服务器密码
        /// </summary>
        public static readonly string SDKPassword = ConfigurationManager.AppSettings["SDKPassword"];

        public static readonly string NvrIP = ConfigurationManager.AppSettings["NVRIP"];
        public static readonly string NvrPort = ConfigurationManager.AppSettings["NVRPort"];
        public static readonly string NvrUser = ConfigurationManager.AppSettings["NVRUser"];
        public static readonly string NvrPassword = ConfigurationManager.AppSettings["NVRPassword"];
        public static readonly string NvrChannel = ConfigurationManager.AppSettings["NVRChannel"];
        #endregion


        #region APIUrl
        /// <summary>
        /// 配置端地址
        /// </summary>
        public static readonly string ServerBaseUri = ConfigurationManager.AppSettings["SERVER_URL_BASE"];
        /// <summary>
        /// 图片地址
        /// </summary>
        public static readonly string ImageBaseUri = ConfigurationManager.AppSettings["IMAGE_URL_BASE"];
        /// <summary>
        /// 地图资源位置
        /// </summary>
        public static readonly string MapPath = ConfigurationManager.AppSettings["MAP_PATH"];
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        public static readonly string GetAllDeviceType = ConfigurationManager.AppSettings["API_GET_ALL_DEVICE_TYPE"];
        /// <summary>
        /// 获取所有设备列表
        /// </summary>
        public static readonly string GetAllDeviceList = ConfigurationManager.AppSettings["API_GET_ALL_DEVICE_LIST"];
        /// <summary>
        /// 获取所有设备类型树
        /// </summary>
        public static readonly string GetAllDeviceTypeForTree = ConfigurationManager.AppSettings["API_GET_ALL_DEVICE_TYPE_TREE"];
        /// <summary>
        /// 获取所有校区
        /// </summary>
        public static readonly string GetMap = ConfigurationManager.AppSettings["API_GET_MAP"];
        /// <summary>
        /// 获取地图elements
        /// </summary>
        public static readonly string GetMapElements = ConfigurationManager.AppSettings["API_GET_MAP_ELEMENTS"];
        /// <summary>
        /// 获取layers
        /// </summary>
        public static readonly string GetMapLayers = ConfigurationManager.AppSettings["API_GET_MAP_LAYERS"];
        /// <summary>
        /// 获取未处理报警数量
        /// </summary>
        public static readonly string GetAlarmCount = ConfigurationManager.AppSettings["API_GET_ALARM_COUNT"];
        /// <summary>
        /// 根据报警类型获取报警信息
        /// </summary>
        public static readonly string GetAlarmInfo = ConfigurationManager.AppSettings["API_GER_UNTREATED_ALARM"];
        /// <summary>
        /// 获取所有报警信息
        /// </summary>
        public static readonly string GetAllAlarmInfo = ConfigurationManager.AppSettings["API_GRT_ALL_ALARM"];
        /// <summary>
        /// 获取异常摄像机
        /// </summary>
        public static readonly string GetErrorCamera = ConfigurationManager.AppSettings["API_GET_ALL_ERRORCAMERA"];
        /// <summary>
        /// 获取所有报警信号类型
        /// </summary>
        public static readonly string GetAllSignal = ConfigurationManager.AppSettings["API_GET_ALL_SIGNAL"];
        /// <summary>
        /// 获取菜单配置
        /// </summary>
        public static readonly string GetClientMenu = ConfigurationManager.AppSettings["API_GET_CLIENT_MENU"];
        /// <summary>
        /// 获取所有客户端状态
        /// </summary>
        public static readonly string GetAllClientInfo = ConfigurationManager.AppSettings["API_GET_ALL_CLIENT_INFO"];
        /// <summary>
        /// 获取所有客户端配置
        /// </summary>
        public static readonly string GetAllClientConfig = ConfigurationManager.AppSettings["API_GET_ALL_CLIENT_CONFIG"];
        /// <summary>
        /// 获取所有报警类型
        /// </summary>
        public static readonly string GetAlarmType = ConfigurationManager.AppSettings["API_GET_ALARM_TYPE"];
        /// <summary>
        /// 获取所有报警级别
        /// </summary>
        public static readonly string GetAlarmLevel = ConfigurationManager.AppSettings["API_GET_ALARM_LEVEL"];
        /// <summary>
        /// 获取所有报警组件
        /// </summary>
        public static readonly string GetAllModule = ConfigurationManager.AppSettings["API_GET_ALL_MODULE"];
        /// <summary>
        /// 获取所有报警组件配置
        /// </summary>
        public static readonly string GetAllModuleConfig = ConfigurationManager.AppSettings["API_GET_ALL_MODULECONFIG"];
        /// <summary>
        /// 获取处置预案
        /// </summary>
        public static readonly string GetTextPlan = ConfigurationManager.AppSettings["API_GET_TEXTPLAN"];
        /// <summary>
        /// 添加活动车辆
        /// </summary>
        public static readonly string InsertTracker = ConfigurationManager.AppSettings["API_INSERT_TRACKER"];
        /// <summary>
        /// 删除活动车辆
        /// </summary>
        public static readonly string DeleteTracker = ConfigurationManager.AppSettings["API_DELETE_TRACKER"];

        /// <summary>
        /// 修改活动车辆
        /// </summary>
        public static readonly string UpdateTracker = ConfigurationManager.AppSettings["API_UPDATE_TRACKER"];

        /// <summary>
        /// 获取活动车辆
        /// </summary>
        public static readonly string GetTracker = ConfigurationManager.AppSettings["API_GET_TRACKER"];
        /// <summary>
        /// 获取活动车辆进入信息
        /// </summary>
        public static readonly string GetTrackerCar = ConfigurationManager.AppSettings["API_GET_TRACKER_CAR"];
        /// <summary>
        /// 根据摄像机编号获取附近摄像机
        /// </summary>
        public static readonly string GetCamera = ConfigurationManager.AppSettings["API_GET_CAMERA"];
        /// <summary>
        /// 通过客户端Id获取该客户端已部署所有设备
        /// </summary>
        public static readonly string GetAllElements = ConfigurationManager.AppSettings["API_GET_ELEMENT_BYID"];
        /// <summary>
        /// 获取拼控摄像机列表
        /// </summary>
        public static readonly string GetCollageCamera = ConfigurationManager.AppSettings["API_GET_COLLAGE_CAMERA"];
        /// <summary>
        /// 获取所有设备统计
        /// </summary>
        public static readonly string GetAllDeviceCount = ConfigurationManager.AppSettings["API_GET_ALL_DEVICE_COUNT"];
        /// <summary>
        /// 获取气体设备
        /// </summary>
        public static readonly string GetGasDevice = ConfigurationManager.AppSettings["API_GET_GAS_DEVICE"];
        /// <summary>
        /// 获取水压设备
        /// </summary>
        public static readonly string GetWaterDevice = ConfigurationManager.AppSettings["API_GET_WATER_DEVICE"];
        /// <summary>
        /// 获取电力设备
        /// </summary>
        public static readonly string GetElectricDevice = ConfigurationManager.AppSettings["API_GET_ELECTRIC_DEVICE"];
        /// <summary>
        /// 获取消防通道摄像机
        /// </summary>
        public static readonly string GetEngineModule = ConfigurationManager.AppSettings["API_GET_FIRE_ENGINE"];
        /// <summary>
        /// 获取电梯摄像机
        /// </summary>
        public static readonly string GetElevatorModule = ConfigurationManager.AppSettings["API_GET_ELEVATOR"];
        /// <summary>
        /// 获取逃生通道摄像机
        /// </summary>
        public static readonly string GetEscapeCamera = ConfigurationManager.AppSettings["API_GET_ESCAPE_CAMERA"];
        /// <summary>
        /// 获取系统设置参数
        /// </summary>
        public static readonly string GetSystemParameter = ConfigurationManager.AppSettings["API_GET_SYSTEM_PARAMETER"];
        /// <summary>
        /// 获取摄像机位置属性
        /// </summary>
        public static readonly string GetCameraParameter= ConfigurationManager.AppSettings["API_GET_CAMERA_PARAMETER"];
        /// <summary>
        /// 根据摄像机位置获取摄像机列表
        /// </summary>
        public static readonly string GetCameraByParameter = ConfigurationManager.AppSettings["API_GET_CAMERA_BY_PARAMETER"];


        public static readonly string GetWaterValue= ConfigurationManager.AppSettings["API_GET_WATER_VALUE"];

        public static readonly string GetWaterDeviceById = ConfigurationManager.AppSettings["API_DEVICE"];

        public static readonly string GetRoomAccessByCode = ConfigurationManager.AppSettings["API_GET_ROOMACCESS_BY_CODE"];
        #endregion APIUrl

        /// <summary>
        /// 语音播报语速
        /// </summary>
        public static readonly string SpeechRate = ConfigurationManager.AppSettings["SpeechRate"];
        /// <summary>
        /// 语音播报声量
        /// </summary>
        public static readonly string SpeechVol = ConfigurationManager.AppSettings["SpeechVol"];

        #region 拼控

        public static readonly string InsertSceneList = ConfigurationManager.AppSettings["API_INSERT_SCENE_LIST"];
        public static readonly string QuerySceneList = ConfigurationManager.AppSettings["API_Query_SCENE_LIST"];
        public static readonly string DeleteSceneList = ConfigurationManager.AppSettings["API_DELETE_SCENE_LIST"];

        public static readonly string InsertPlanRotation = ConfigurationManager.AppSettings["API_INSERT_PlAN_ROTATION"];
        public static readonly string QueryPlanRotation = ConfigurationManager.AppSettings["API_QUERY_PlAN_ROTATION"];
        public static readonly string DeletePlanRotation = ConfigurationManager.AppSettings["API_DELETE_PlAN_ROTATION"];
        public static readonly string UpdatePlanRotation = ConfigurationManager.AppSettings["API_UPDATE_PlAN_ROTATION"];

        public static readonly string GetAllEncoder = ConfigurationManager.AppSettings["API_QUERY_ALL_ENCODERS"];

        #endregion

    }
}
