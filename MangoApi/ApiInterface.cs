using com.mango.protocol.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoApi
{
    #region 首页图表类接口
    public class ErrorDeviceType
    {
        public int id { get; set; }
        public string deviceType { get; set; }
        public string deviceTypeName { get; set; }
        public string deviceClass { get; set; }
        public string deviceName { get; set; }
        public string dateName { get; set; }
        public string alarmClass { get; set; }
        public string alarmName { get; set; }
        public string month { get; set; }
        public int count { get; set; }
        public string imgurl { get; set; }
        public int errorCount { get; set; }
        public int mapId { get; set; }
        public string mapName { get; set; }
        public int deviceCount { get; set; }
        public int deviceErrorCount { get; set; }
        public string locationAttribute { get; set; }
        public string errorAlarmTime { get; set; }
    }

    #endregion

    public class SystemParameter
    {
        public int id { get; set; }
        public string name { get; set; }
        public string parameter_code { get; set; }
        public string status { get; set; }
        public string parameter_value { get; set; }
        public string parameter_description { get; set; }
        public string parameter_type { get; set; }
    }
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
        public string description { get; set; }
        public string securityPerson { get; set; }       
        public string securityPhone { get; set; }
        public string buildingPerson { get; set; }
        public string buildingPhone { get; set; }
        public string tenementPerson { get; set; }
        public string tenementPhone { get; set; }
        public string buildType { get; set; }
        public string waterHouse { get; set; }
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
        public int mapId { get; set; }
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
        public string errorAlarmTime { get; set; }
        public int errorDeviceId { get; set; }
        public int errorAlarmStatus { get; set; }
        public string errorDeviceName { get; set; }
        public string errorDeviceIp { get; set; }
        public string errorDeviceMacaddr { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string errorDeviceCode { get; set; }
        public int errorNo { get; set; }

        public int sdkId { get; set; }
        public int deviceId { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string deviceTypeCode { get; set; }
        public int iconAngle { get;set; }

        public string iconExt { get; set; }
        public int mapId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string roomNum { get; set; }
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
        public string offlineTime { get; set; }
        public string channel_number { get; set; }
        public string signalLists { get; set; }

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
        public string alarmType { get; set; }
        public string alarmTypeVal { get; set; }
        public int type { get; set; }
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
        public int mapid { get; set; }
        public bool fake { get; set; }
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
        public string startTime { get; set; }
        public string startTimeStr { get; set; }
        public string stopTime { get; set; }
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

    public class WaterValue
    {
        public string id { get; set; }
        public string altime { get; set; }
        public string value { get; set; }
        public string dowm { get; set; }
        public string up { get; set; }
        public string sersorid { get; set; }
        public string type { get; set; }
        public string dbtime { get; set; }
        public string note { get; set; }
        public string state { get; set; }
        public string loc { get; set; }
    }

    public class TracerCarInfo
    {
        public int id { get; set; }
        public string carNumber { get; set; }
        public string carOwner { get; set; }
        public int activityId { get; set; }
        public string carPhone { get; set; }
        public string insertTime { get; set; }
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
        /// <summary>
        /// 查看范围
        /// </summary>
        public string viewRange { get; set; }
        /// <summary>
        /// 默认校区
        /// </summary>
        public int campus { get; set; }
        public string camera { get; set; }
        public string coding { get; set; }
        public int displaycount { get; set; }
        public string coordinate1 { get; set; }
        public string coordinate2 { get; set; }
        public int status { get; set; }//0--离线 1--在线
        public string channel_number { get; set; }
        public string offlineTime { get; set; }
        public SignalList[] signalLists { get; set; }
    }

    public class SignalList
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string signal_name { get; set; }
        public ScenesList[] scenesList { get; set; }
    }
    public class ScenesList
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string name { get; set; }
        public int status { get; set; }//1-主场景
        public int position { get; set; }//1-主屏，2-辅屏1，。。。
        public PageLayout[] pageLayoutList { get; set; }
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

    public class AlarmModule
    {
        public int id;
        public string module_name;
        public string module_id;
        public string module_size;
        public string description;
        public string img_url;
        public string module_classify;
        public string module_content;
    }

    public class AlarmModuleConfig
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

    public class TextPlan
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string papers { get; set; }
        public string steps { get; set; }
        public string template { get; set; }
        public NoteTemplate[] templateListValue { get; set; }
        public string content { get; set; }
        public string employeeIds { get; set; }
        public Employee[] employeeList { get; set; }
    }

    public class CarInfo
    {
        public int id { get; set; }
        public string carid { get; set; }
        public string owner { get; set; }
        public string phone { get; set; }
        public string unit { get; set; }
        public string brand { get; set; }
        public string remarks { get; set; }
    }
    //{\"contactId\":\"10\",\"contactName\":\"张队长\",\"phone\":\"12312312312\",\"templateId\":\"6\",\"templateName\":\"火灾报警通知\"}
    public class PlanContent
    {
        public string contactId { get; set; }
        public string contactName { get; set; }
        public string phone { get; set; }
        public string templateId { get; set; }
        public string templateName { get; set; }

    }

    public class Employee
    {
        public string id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string department { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
    }

    public class NoteTemplate
    {
        public int id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string number { get; set; }
        public string description { get; set; }
    }

    public class Element
    {
        public int id { get; set; }
        public int deviceId { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string deviceTypeCode { get; set; }
        public string deviceTypeName { get; set; }
        public int iconAngle { get; set; }
        public string iconExt { get; set; }
        public int mapId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string roomNum { get; set; }
        public string locationAttribute { get; set; }
        public string description { get; set; }
        public string deviceClassValue { get; set; }
        public string deviceClassName { get; set; }
        public string imgUrl { get; set; }
        public string deviceClass { get; set; }
        public int status { get; set; }
        public string deviceStatus { get; set; }
        public Element[] childElements { get; set; }
        public string distance { get; set; }
    }

    public class IconExt
    {
        public string width { get; set; }
        public string linkMap { get; set; }
        public string filename { get; set; }
        public string zoomLevel { get; set; }

    }


    public class CollageCameraList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
        public CollageCameraList[] children { get; set; }
        public CollageCamera[] devices { get; set; }
        //public Element[] devices { get; set; }
        public bool parent { get; set; }
    }

    public class CollageCamera : Element
    {
        public string ip { get; set; }
        public string port { get; set; }
        //public string vendor { get; set; }
        public string brand { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class DeviceCount
    {
        public string deviceClass { get; set; }
        public string deviceType { get; set; }
        public string deviceCount { get; set; }
        public string deviceIcon { get; set; }
    }

    public class ElementDevice
    {
        public int id { get; set; }
        public string name { get; set; }
        public string deviceLocation { get; set; }
        public string deviceCategory { get; set; }
        public string code { get; set; }
        public string deviceExtProperties { get; set; }
        public string ip { get; set; }
        public string devicePort { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string brand { get; set; }
        public string deviceDesc { get; set; }
        public string deviceNode { get; set; }
        public string deviceMacaddr { get; set; }
        public string ptz { get; set; }
        public string status { get; set; }
        public string alertObjId { get; set; }
        public string deviceTypeId { get; set; }
        public string deviceStatus { get; set; }
        public ElementDevice[] childern { get; set; }
        public Element[] childElements { get; set; }
        public string count { get; set; }
        public string deviceTypeName { get; set; }
        public string deviceimg { get; set; }
        public string deviceClass { get; set; }
        public string deviceTypeCode { get; set; }
        public string showType { get; set; }
        public string checkedimg { get; set; }
        public string alarmimg { get; set; }
        public string deviceClassValue { get; set; }
        public string deviceClassName { get; set; }
        public string imgUrl { get; set; }
        public int mapId { get; set; }
        public string deviceId { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string iconExt { get; set; }
        public string iconAngle { get; set; }
        public string roomNum { get; set; }
        public string locationAttribute { get; set; }
        public string description { get; set; }
        public string distance { get; set; }
        public string deviceIds { get; set; }

    }


    public class Parameter
    {
        public int id { get; set; }
        public string listValue { get; set; }
        public string listCode { get; set; }
        public string listName { get; set; }
        public string showIndex { get; set; }
        public string listDescription { get; set; }
        public string isImg { get; set; }
        public string imgUrl { get; set; }
        public int parentId { get; set; }
    }


    #region Alarm peculiarnote
    /// <summary>
    /// 电力
    /// </summary>
    public class AlarmElectric
    {
        public string name { get; set; }
        public string code { get; set; }
        public string type { get; set; }// # 0-设备初始化 1-设备正常 2-设备故障 3-设备超时 4-超载 5-漏电
        public ElectricValue[] currentValues { get; set; }
    }

    public class ElectricValue
    {
        public float c { get; set; }
        public float cp { get; set; }
        public int s { get; set; }
        public string unit { get; set; }
    }
    /// <summary>
    /// 门禁
    /// </summary>
    public class AlarmDoor
    {
        public string host { get; set; }
        public int eType { get; set; }//# 0 非法刷卡开门 1 正常刷卡开门 2 按钮事件 3 延时报警 4 非法开门
        public string card { get; set; }
        public int offset { get; set; }
        public string time { get; set; }
        public int busNo { get; set; }
        public int doorNo { get; set; }
        public DoorUser[] user { get; set; }
    }

    public class DoorUser
    {
        public string name { get; set; }
        public string sex { get; set; }
        public string dep { get; set; }//部门
        public string seno { get; set; }//学号
        public string phone { get; set; }
        public string topic { get; set; }
    }
    /// <summary>
    /// 围栏
    /// </summary>
    public class AlarmFence
    {
        public string strMac { get; set; }
        public string SubSystemID { get; set; }
        public string strCode { get; set; }
        public string lPlayback { get; set; }// # 整数，是否是回放事件
        public string Acct { get; set; }
        public string IsNewEvent { get; set; }// # 是整数，1为新事件， 0为从报警中恢复
        public string CID { get; set; }
        public string IsZone { get; set; }//# strCode 是否为防区号； 1 为防区号， 0 为用户号；
        public string DateTime { get; set; }
    }
    /// <summary>
    /// 水压
    /// </summary>
    public class AlarmWater
    {
        public string time { get; set; }
        public string sourcetype { get; set; }
        public string type { get; set; }
        public string sourceid { get; set; }
        public string sourcename { get; set; }
        public string sourcebuilding { get; set; }
        public string title { get; set; }
        public string level { get; set; }
        public string value { get; set; }
        public string unit { get; set; }
    }

    /*state：
	0：正常
	1：传感器离线
	2：采集板离线
	3：终端离线
	4：过低
	5：过高

sensortype：
	1：压力
	2：液压
	3：电压
	4：电流
	5：温度
	6：湿度
	7：开关量输入
	8：开关量输出

eventType：
	1：传感器离线
	2：传感器上线
	3：安卓屏离线
	4：安卓屏上线
	5：采集板离线
	6：采集板上线
	7：传感器数值低于下限
	8：下限恢复
	9：传感器数值高于上限
	10：上限恢复*/

        /// <summary>
        /// 气体
        /// </summary>
    public class AlarmGas
    {
        public string gas { get; set; }
        public string host { get; set; }
        public string type { get; set; }
        public string unit { get; set; }
        public string warn { get; set; }
        public string range { get; set; }
        public string ratio { get; set; }
        public string upLim { get; set; }
        public string value { get; set; }
        public string downLim { get; set; }


    }
    /*
 unit:

      VV("%V/V",1),
      LEL("%LEL",2),
 	  PPM("PPM",3),
 	  KPPM("KPPM",4);
      
        
 type:

    LPG("液化石油气",0),
 	LNG("液化天然气",1),
 	GAS_CH4("甲烷",2),
 	GAS_CO("一氧化碳",3),
 	GAS_CO2("二氧化碳",4),
 	GAS_NO("一氧化氮",5),
 	GAS_NO2("二氧化氮",6),
 	GAS_H2S("硫化氢",7),
 	GAS_H2("氢气",8),
 	GAS_O2("氧气",9),
 	EX10("[异常值 10]",10),
 	EX11("[异常值 11]",11),
 	EX12("[异常值 12]",12),
 	COMBUS("可燃气体",13),
 	GAS("毒气",14),
 	C6H6("苯",15),
 	GAS_NH3("氨气",16),
 	GAS_CL2("氯气",17),
 	GAS_HCL("氯化氢",18),
 	C2H3CL19("氯乙烯",19),
 	C3H3N("丙烯腈",20),
 	C4H8S("四氢塞酚",21),
 	CH("乙炔",22),
 	CH2O("甲醛",23),
 	CH3OH("甲醇",24),
 	ETO("ETO",25),
 	F2("氟气",26),
 	HCN("氰化氢",27),
 	HF("氟化氢",28),
 	O3("臭氧",29),
 	PH3("磷化氢",30),
 	Si2H6("六氢化二硅",31),
 	SiH4("四氢化硅",32),
 	SO2("二氧化硫",33),
 	VOC("VOC",34),
 	OLINE("OLINE",35),
 	DIESEL("DIESEL",36),
 	UNKOWN("UNKOWN",128),
 
 warn:

    0-未连接, 1-正常, 2-探测器故障, 3-通讯故障, 4-低警, 5-高警,6-丢失, 7-屏蔽,10-动作,11-未动作    */
    /// <summary>
    /// 红外
    /// </summary>
    public class AlarmInfrared
    {
        public string CID { get; set; }
        public string Acct { get; set; }
        public string IsZone { get; set; }
        public string strMac { get; set; }
        public string strCode { get; set; }
        public string lPlayback { get; set; }
        public string IsNewEvent { get; set; }
        public string SubSystemID { get; set; }

    }
    /*由于平台返回数据问题外包一层Data*/
    public class FaceBlackDate
    {
        public FaceBlackList data { get; set; }
    }
    /// <summary>
    /// 人脸黑名单
    /// </summary>
    public class FaceBlackList
    {
        public string Id { get; set; }
        public string FaceId { get; set; }
        public int FaceAge { get; set; }
        public string FaceCity { get; set; }
        public string FaceName { get; set; }
        public string Latitude { get; set; }
        public string SnapTime { get; set; }
        public string AlarmTime { get; set; }
        public string AlarmType { get; set; }
        public string FacelibId { get; set; }
        public string GuardType { get; set; }
        public string Longitude { get; set; }
        public string SnappicId { get; set; }
        public string FaceCardid { get; set; }
        public int FaceGender { get; set; }
        public int FaceIdtype { get; set; }
        public string FacePicurl { get; set; }
        public string SnapPicurl { get; set; }
        public string FaceCountry { get; set; }
        public string FacecamCode { get; set; }
        public string GuardReason { get; set; }
        public string GuardtaskId { get; set; }
        public string FaceBirthday { get; set; }
        public string FacePlaction { get; set; }
        public string FaceProvince { get; set; }
        public int FaceSamevalue { get; set; }
        public string SnapfacePicurl { get; set; }
    }
    /// <summary>
    /// 车辆
    /// </summary>
    public class AlarmCar
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Plate { get; set; }
        public string Speed { get; set; }
        public string CarUrl { get; set; }
        public string Illtime { get; set; }
        public string SceneUrl { get; set; }
        public string Department { get; set; }
    }
    #endregion


    #region 拼控接口

    /// <summary>
    /// 场景列表
    /// </summary>
    public class SceneList
    {
        /// <summary>
        /// 场景id
        /// </summary>
        public int id;

        /// <summary>
        /// 场景名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 场景配置集合
        /// </summary>
        public List<ScenesConfig> scenesList;
    }

    /// <summary>
    /// 大窗口（现在9个）
    /// </summary>
    public class ScenesConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id;

        /// <summary>
        /// 新窗口Id
        /// </summary>
        public int winId;

        /// <summary>
        /// 场景id
        /// </summary>
        public int sceneId;

        public double x;
        public double y;

        /// <summary>
        /// 宽度
        /// </summary>
        public double width;

        /// <summary>
        /// 高度
        /// </summary>
        public double height;

        /// <summary>
        /// 边距（左,上,右,下）如:(0,4,-5,3)中间用逗号隔开
        /// </summary>
        public string margin;

        /// <summary>
        /// 窗口层叠值
        /// </summary>
        public int zIndex;

        /// <summary>
        /// 行属性
        /// </summary>
        public int rowProperty;

        /// <summary>
        /// 列属性
        /// </summary>
        public int columnProperty;

        /// <summary>
        /// 子窗口配置
        /// </summary>
        public List<ChildFight> childFights;
    }

    /// <summary>
    /// 子窗口配置
    /// </summary>
    public class ChildFight
    {
        public int id;
        public int pid;
        public string cameraCode;
        public string cameraName;
        public string vendor;
        public string ip;
        public string port;
        public string user;
        public string pass;
    }


    /// <summary>
    /// 计划轮询
    /// </summary>
    public class PlanRotation
    {
        /// <summary>
        /// 轮询id
        /// </summary>
        public int id;

        /// <summary>
        /// 轮训开关 1开 0关
        /// </summary>
        public int isOpen { get; set; }

        /// <summary>
        /// 场景id ,隔开
        /// </summary>
        public string sceneId;

        /// <summary>
        /// 时间 ,隔开
        /// </summary>
        public string time;

        /// <summary>
        /// 轮询名称    
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 持续时间
        /// </summary>
        public double duration;
        public int clientId;
    }

    public class WinParams
    {
        public int id;
        public int x;
        public int y;
        public int w;
        public int h;
        public int z;
        public int split;
        public Camera[] cam;
    }

    public class SceneSet
    {
        public int id;
        public int x;
        public int y;
        public int w;
        public int h;
        public int z;
        public int split;
        public CameraScene[] cam;
    }

    public class CameraScene 
    {
        public string Code;
        public string Name;
        public int vendor;
        public string ip;
        public string user;
        public string pass;
    }

    public class Camera
    {
        public string code;
        public string name;
        public int vendor;
        public string ip;
        public string user;
        public string pass;
    }

    class PinkongResult
    {
        public string Code;
        public string Msg;
        public string sn;
        public string cmd;
    }

    public class WinList
    {
        public int Code;
        public int Sn;
        public int cmd;
        public WinProp[] M;
    }

    public class WinProp
    {
        public int Enable;
        public int WndOperateMode;
        public int WindowNo;
        public int LayerIndex;
        public int XCoordinate;
        public int YCoordinate;
        public int Height;
        public int Width;
        public int Split;
        public int DecResource;
        public WinCam[] cam;
    }

    public class WinCam
    {
        public string Status;
        public string Stream;
        public string Packet;
        public string Mode;
        public int FpsDecV;
        public int FpsDecA;
        public int DecodedV;
        public int DecodedA;
        public int wImgW;
        public int wImgH;
        public string url;
        public string ip;
    }
    /// <summary>
    /// 编码器配置信息
    /// </summary>
    public class EncoderConfig : WinParams { }
    #endregion
}
