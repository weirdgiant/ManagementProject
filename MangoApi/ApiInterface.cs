using com.mango.protocol.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoApi
{

    public class MangoMap
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int pid { get; set; }
        public int idx { get; set; }
        public double defaultLongitude { get; set; }
        public double defaultLatitude { get; set; }
        public int defaultZoomLevel { get; set; }
        public int backgroundColor { get; set; }
        public string securityPerson { get; set; }
        public string description { get; set; }
        public string person_phone { get; set; }
    }
    public class MangoLayer
    {
        public int id { get; set; }
        public string name { get; set; }
        public int mapId { get; set; }
        public string color { get; set; }
        public int labelEnabled { get; set; }
        public string labelColor { get; set; }
        public string labelFont { get; set; }
        public int labelFontSize { get; set; }
        public string filenamePrefix { get; set; }
        public int renderMode { get; set; }
        public string textureFilename { get; set; }
        public int type { get; set; }
    }
    /// <summary>
    /// 设备信息
    /// </summary>
    public class Device
    {
        public int id { get; set; }
        public string name { get; set; }
        public int deviceLocation { get; set; }
        public int deviceCategory { get; set; }
        public string code { get; set; }
        public string deviceExtProperties { get; set; }
        public string ip { get; set; }
        public int devicePort { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string brand { get; set; }
        public string deviceDesc { get; set; }
        public int deviceNode { get; set; }
        public string deviceMacaddr { get; set; }
        public int ptz { get; set; }
        public int status { get; set; }
        public int alertObjId { get; set; }
        public string unitType { get; set; }
        public string deviceTypeId { get; set; }
        public string deviceStatus { get; set; }
    }
    /// <summary>
    /// 设备类型
    /// </summary>
    public class DeviceType
    {
        public int id { get; set; }
        public string deviceTypeName { get; set; }
        public string deviceImg { get; set; }
        public string checkedImg { get; set; }
        public string alarmImg { get; set; }
        public string deviceClass { get; set; }
        public string deviceTypeCode { get; set; }
        public int showType { get; set; }
    }
    /// <summary>
    /// 设备类型树
    /// </summary>
    public class DeviceTypeOfTree
    {
        public int id;
        public string name;
        public int deviceLocation;
        public int deviceCategory;
        public string code;
        public string deviceExtProperties;
        public string ip;
        public int devicePort;
        public string username;
        public string password;
        public string brand;
        public string deviceDesc;
        public int deviceNode;
        public string deviceMacaddr;
        public int ptz;
        public int status;
        public int alertObjId;
        public string deviceTypeId;
        public int deviceStatus;
        public DeviceTypeOfTree[] childern;
        public DeviceElement[] childElements;
        public int count;
        public string deviceTypeName;
        public string deviceimg;
        public string deviceClass;
        public string deviceTypeCode;
        public int showType;
        public string checkedimg;
        public string alarmimg;
        public string deviceClassValue;
        public string deviceClassName;
        public string imgUrl;
        public int mapid;
    }

    public class DeviceElement
    {
        public int id;
        public int deviceId;
        public double longitude;
        public double latitude;
        public string deviceTypeCode;
        public int iconAngle;
        public string iconExt;
        public int mapId;
        public string code;
        public string name;
    }

        /// <summary>
        /// 异常摄像机
        /// </summary>
        public class ErrorCamera
    {
        public int id { get; set; }
        public string errorAlarmTime { get; set; }// "2019-03-12T02:17:39.639Z",
        public int errorDeviceId { get; set; }
        public int errorAlarmStatus { get; set; }
        public string errorDeviceName { get; set; }
        public string errorDeviceIp { get; set; }
        public string errorDeviceMacaddr { get; set; }
        public string errorDeviceCode { get; set; }
        public int errorNo { get; set; }
        public int sdkId { get; set; }
        public int deviceId { get; set; }
        public long longitude { get; set; }
        public long latitude { get; set; }
        public string deviceTypeCode { get; set; }
        public int iconAngle { get; set; }
        public string iconExt { get; set; }
        public int mapId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int roomNum { get; set; }
        public string locationAttribute { get; set; }
        public string description { get; set; }
    }
    /// <summary>
    /// 客户端状态
    /// </summary>
    public class ClientState
    {
        public int id { get; set; }
        public int clientProperty { get; set; }
        public string clientName { get; set; }
        public string ipAddress { get; set; }
        public string clientNumber { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string menu { get; set; }
        public int pinkong { get; set; }
        public string alarm { get; set; }
        public string jurisdiction { get; set; }
        public string viewRange { get; set; }
        public int campus { get; set; }
        public string camera { get; set; }
        public string coding { get; set; }
        /// <summary>
        /// 大屏幕数量
        /// </summary>
        public int displaycount { get; set; }
        public string coordinate1 { get; set; }
        public string coordinate2 { get; set; }
        public int status { get; set; }
    }
    /// <summary>
    /// 报警信号
    /// </summary>
    public class Signal
    {
        public int id { get; set; }
        public string signal_name { get; set; }
        public string signal_id { get; set; }
        public int signal_level { get; set; }
        public int display { get; set; }
        public string alert_type { get; set; }
        public int plan_id { get; set; }
        public string img_url { get; set; }
        public string device_type_status { get; set; }
    }
    /// <summary>
    /// 报警
    /// </summary>
    public class Alarm
    {       
        public int id { get; set; }
        public string flag { get; set; }
        public string flagVal { get; set; }
        public int type { get; set; }
        public string alarmType { get; set; }
        public string alarmTypeVal { get; set; }
        public string sersor { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public string altime { get; set; }
        public int level { get; set; }
        public string peculiarnote { get; set; }//"{\"message\": \"模拟报警\", \"unit_id\": \"aaaaa\", \"alarm_time\": \"2017/11/20 23:11:40\", \"alarm_type\": \"火警\", \"is_success\": 0, \"device_type\": \"\", \"alarm_source_desc\": \"05号主机0002回路0134节点\"}",
        /// <summary>
        /// 存入数据库时间
        /// </summary>
        public string dbtime { get; set; }
        public ALARM_STATE state { get; set; }
        public string processuser { get; set; }
        public string processnote { get; set; }
        public string confirmnote { get; set; }
        public string processtime { get; set; }
        public string confirmuser { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string confirmtime { get; set; }
        public int record { get; set; }
        public string path { get; set; }
        public int needrec { get; set; }
        public string tick { get; set; }
        public string chat { get; set; }
        public string extnote { get; set; }
        public string location { get; set; }
        public string sersorname { get; set; }
        public int alarmCount { get; set; }
        public int sid { get; set; }
    }
    /// <summary>
    /// 活动
    /// </summary>
    public class TrackEvent
    {
        public int id { get; set; }
        public int activityId { get; set; }
        public string name { get; set; }
        public int mapId { get; set; }
        public long startTime { get; set; }
        public string startTimeStr { get; set; }
        public long stopTime { get; set; }
        public string stopTimeStr { get; set; }
        public int status { get; set; }
        public int carNum { get; set; }
        public string carNames { get; set; }
        public string carIds { get; set; }
        public int deviceCameraNum { get; set; }
        public string deviceCameranames { get; set; }
        public string deviceCameraIds { get; set; }
        public int deviceCameraId { get; set; }
        public string carJson { get; set; }
    }
    /// <summary>
    /// 报警级别
    /// </summary>
    public class AlarmLevel
    {
        public int id { get; set; }
        public string listValue { get; set; }
        public string listCode { get; set; }
        public string listName { get; set; }
        public int showIndex { get; set; }
        public string listDescription { get; set; }
        public int isImg { get; set; }
        public string imgUrl { get; set; }
        public int parentId { get; set; }
    }
    /// <summary>
    /// 报警类型
    /// </summary>
    public class AlarmTypeInfo
    {
        public int id;
        public string listValue;
        public string listCode;
        public string listName;
        public int showIndex;
        public string listDescription;
        public int isImg;
        public string imgUrl;
        public int parentId;
    }

    /// <summary>
    /// 报警界面布置
    /// </summary>
    public class PageLayout
    {
        public int id;
        public string windowId;
        public string componentId;
        public string size;
        public string preview;
        public int module;
        public int pid;
    }
    /// <summary>
    /// 客户端配置
    /// </summary>
    public class ClientConfig
    {
        public int id;
        public int clientProperty;
        public string clientName;
        public string ipAddress;
        public string clientNumber;
        public string username;
        public string password;
        public string menu;
        public int pinkong;
        public string alarm;
        public string jurisdiction;
        public string viewRange;
        public int campus;
        public string camera;
        public string coding;
        public int displaycount;
        public string coordinate1;
        public string coordinate2;
        public int status;
        public SignalList[] signalLists;
    }

    public class SignalList
    {
        public int id;
        public int pid;
        public string signal_name;
        public ScenesList[] scenesList;
    }
    public class ScenesList
    {
        public int id;
        public int pid;
        public string name;
        public PageLayout[] pageLayoutList;
    }

    public class AlarmFilter
    {
        public int sid;
        public string altimeStart;
        public string altimeStop;
        public string flag;
        public string level;
        public string state;
    }

}
