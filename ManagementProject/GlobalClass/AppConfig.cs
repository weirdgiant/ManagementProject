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
        #region SystemInfo
        /// <summary>
        /// 获取产品名称
        /// </summary>
        public static readonly string ProductName = ConfigurationManager.AppSettings["ProductName"];
        /// <summary>
        /// 获取标题栏名称
        /// </summary>
        public static readonly string TitleText = ConfigurationManager.AppSettings["TitleText"];
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
        /// 获取elements
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
        #endregion APIUrl


    }
}
