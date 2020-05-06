using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using ManagementProject.Model;
using MangoApi;
using Newtonsoft.Json;

namespace ManagementProject
{
    public static class HttpAPi
    {       
        public static string Post(string url, Dictionary<string, string> param)
        {
            HttpClient client = new HttpClient();
            try
            {
                Task<HttpResponseMessage> ret = client.PostAsync(url, new FormUrlEncodedContent(param));
                ret.Wait();
                if (!ret.IsFaulted)
                {
                    return ret.Result.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), e.Message);
            }
            finally
            {
                client.Dispose();
            }

            return null;
        }

        public static string Post(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                Task<HttpResponseMessage> ret = client.PostAsync(url, null);
                ret.Wait();
                if (!ret.IsFaulted)
                {
                    return ret.Result.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi),e.Message);
            }
            finally
            {
                client.Dispose();
            }

            return null;
        }

        #region 首页图表类接口

        public static ErrorDeviceType[] GetErrorDeviceCount(int sid, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType[] ret = JsonConvert.DeserializeObject<ErrorDeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceCount:" + e.Message);
            }
            return null;
        }

        public static ErrorDeviceType GetErrorDeviceRate(int sid, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType ret = JsonConvert.DeserializeObject<ErrorDeviceType>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceCount:" + e.Message);
            }
            return null;
        }

        public static string[] GetErrorTimeByType(int sid, string device_type_code, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"map_id", sid.ToString()},
               {"device_type_code", device_type_code.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                string[] ret = JsonConvert.DeserializeObject<string[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceCount:" + e.Message);
            }
            return null;
        }
        public static string[] GetErrorTimeByClass(int map_id, string device_class, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"map_id", map_id.ToString()},
               {"device_class", device_class.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                string[] ret = JsonConvert.DeserializeObject<string[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceCount:" + e.Message);
            }
            return null;
        }

        
        public static ErrorDeviceType[] GetErrorDeviceTypeCount(int sid,string deviceType, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
               {"deviceType", deviceType.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType[] ret = JsonConvert.DeserializeObject<ErrorDeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceTypeCount:" + e.Message);
            }
            return null;
        }

        public static ErrorDeviceType[] GetErrorDeviceClassCount(int sid, string deviceClass, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
               {"deviceClass", deviceClass.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType[] ret = JsonConvert.DeserializeObject<ErrorDeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceClassCount:" + e.Message);
            }
            return null;
        }

        public static ErrorDeviceType[] GetErrorRateType(int sid, string startMonth, string endMonth, string deviceType, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
                {"startMonth", startMonth.ToString()},
                 {"endMonth", endMonth.ToString()},
               {"deviceType", deviceType.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType[] ret = JsonConvert.DeserializeObject<ErrorDeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceTypeCount:" + e.Message);
            }
            return null;
        }

        public static ErrorDeviceType[] GetErrorRateClass(int sid, string startMonth, string endMonth, string deviceClass, string url)
        {
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
               {"startMonth", startMonth.ToString()},
               {"endMonth", endMonth.ToString()},
               {"deviceClass", deviceClass.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorDeviceType[] ret = JsonConvert.DeserializeObject<ErrorDeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorDeviceClassCount:" + e.Message);
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 获取地图中摄像机位置列表
        /// </summary>
        /// <param name="mapId"></param>
        /// <returns></returns>
        public static Parameter[] ParameterList(int mapId)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetCameraParameter;
            var param = new Dictionary<string, string>
            {
                  {"mapId", mapId.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Parameter[] ret = JsonConvert.DeserializeObject<Parameter[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "ParameterList:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 根据摄像机位置获取摄像机列表
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="localAttr"></param>
        /// <returns></returns>
        public static Element[] GetCameraByParameter(int mapId, string localAttr)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetCameraByParameter;
            var param = new Dictionary<string, string>
            {
                  {"mapId", mapId.ToString()},
                  {"localAttrs", localAttr.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetCameraByParameter:" + e.Message);
            }
            return null;
        }

        public static SystemParameter[] GetSystemParameter()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetSystemParameter;
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                SystemParameter[] ret = JsonConvert.DeserializeObject<SystemParameter[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetSystemParameter:" + e.Message);
            }
            return null;
        }


        public static WaterValue[] GetWaterValue(string code, string altime)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetWaterValue;
            var param = new Dictionary<string, string>
            {
                  {"sersorid", code.ToString()},
               {"altime", altime.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                WaterValue[] ret = JsonConvert.DeserializeObject<WaterValue[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetWaterDGetWaterValueevice:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取水泵房水压设备
        /// </summary>
        /// <returns></returns>
        public static ElementDevice[] GetWaterDevice()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetWaterDevice;
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                ElementDevice[] ret = JsonConvert.DeserializeObject<ElementDevice[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetWaterDevice:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取房间设备列表
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public static ElementDevice[] GetRoomDevice(int mapid,string url)
        {            
            var param = new Dictionary<string, string>
            {
               {"mapId", mapid.ToString()},
            };
            string content = Post(url,param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ElementDevice[] ret = JsonConvert.DeserializeObject<ElementDevice[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetGasDevice:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取消防通道摄像机
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        public static ElementDevice[] GetEngineModule(int sid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetEngineModule;
            var param = new Dictionary<string, string>
            {
               {"sId", sid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ElementDevice[] ret = JsonConvert.DeserializeObject<ElementDevice[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetEngineModule:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取电梯摄像机
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public static ElementDevice[] GetElevatorModule(int mapid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetElevatorModule;
            var param = new Dictionary<string, string>
            {
               {"mapId", mapid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                ElementDevice[] ret = JsonConvert.DeserializeObject<ElementDevice[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetElevatorModule:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据地图Id获取所有设备统计
        /// </summary>
        /// <param name="mapid"></param>
        /// <returns></returns>
        public static DeviceCount[] GetAllDeviceCount(int mapid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllDeviceCount;
            var param = new Dictionary<string, string>
            {
               {"mapId", mapid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                DeviceCount[] ret = JsonConvert.DeserializeObject<DeviceCount[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAllDeviceCount:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取拼控摄像机
        /// </summary>
        /// <returns></returns>
        public static CollageCameraList[] GetCollageCamera()
        {
            var url = AppConfig.ServerBaseUri + AppConfig.GetCollageCamera;
            var param = new Dictionary<string, string> { { "clientId", App.mango.getClientInfo().userId.ToString() } };
            var content = Post(url,param);

            if (content == null)
            {
                return null;
            }
            try
            {
                CollageCameraList[] ret = JsonConvert.DeserializeObject<CollageCameraList[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetCollageCamera:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 通过客户端Id获取该客户端已部署所有设备
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Element[] GetAllElements(int id)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllElements;
            var param = new Dictionary<string, string>
            {
               {"id", id.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAllElements:" + e.Message);
            }
            return null;
        }

        public static Element [] GetMapElements(int mapId,string url)
        {
           
            var param = new Dictionary<string, string>
            {
               {"mapId", mapId.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetMapElements:" + e.Message);
            }
            return null;
        }


        public static Element[] GetRoomAccess(string code)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetRoomAccessByCode;
            var param = new Dictionary<string, string>
            {
               {"code", code},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetMapElements:" + e.Message);
            }
            return null;
        }


        public static Element[] GetWaterElements(int sid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetWaterDeviceById;
            var param = new Dictionary<string, string>
            {
               {"sid", sid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetMapElements:" + e.Message);
            }
            return null;
        }

        public static Element[] GetEscapeCamera(string code)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetEscapeCamera;
            var param = new Dictionary<string, string>
            {
               {"code", code.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Element[] ret = JsonConvert.DeserializeObject<Element[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetEscapeCamera:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据摄像机编号获取周围摄像机
        /// </summary>
        /// <param name="code"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Device[] GetCamera(string code,int num,string localAttrs=null)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetCamera;
            var param = new Dictionary<string, string>
            {
               {"code", code.ToString()},
               {"num", num.ToString()},
               {"localAttrs", localAttrs},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Device[] ret = JsonConvert.DeserializeObject<Device[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetCamera:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Device[] GetDeviceList(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                Device[] ret = JsonConvert.DeserializeObject<Device[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetDeviceList:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DeviceType[] GetDeviceTypeList()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllDeviceType;
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                DeviceType[] ret = JsonConvert.DeserializeObject<DeviceType[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetDeviceTypeList:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取设备类型树列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DeviceTypeOfTree[] GetDeviceTypeListOfTree(string url,int mapid)
        {
            var param = new Dictionary<string, string> {
               {"mapId", mapid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                DeviceTypeOfTree[] ret = JsonConvert.DeserializeObject<DeviceTypeOfTree[]>(content);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), "GetDeviceTypeListOfTree:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取地图列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MangoMap[] GetMapList(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                MangoMap[] ret = JsonConvert.DeserializeObject<MangoMap[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetMapList:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取报警信号列表
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Signal[] GetAllSignal(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                Signal[] ret = JsonConvert.DeserializeObject<Signal[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAllSignal:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取所有报警级别
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AlarmLevel [] GetAlarmLevel(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                AlarmLevel[] ret = JsonConvert.DeserializeObject<AlarmLevel[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarmLevel:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取所有报警类型
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AlarmTypeInfo[] GetAlarmType(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                AlarmTypeInfo[] ret = JsonConvert.DeserializeObject<AlarmTypeInfo[]>(content);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarmType:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取报警
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Alarm[] GetAlarm(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                Alarm[] ret = JsonConvert.DeserializeObject<Alarm[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarm:" + e.Message);
            }
            return null;
        }


        public static Alarm[] GetUndealAlarm(string url, string clientId)
        {
            var param = new Dictionary<string, string> {
               {"clientId",clientId},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Alarm[] ret = JsonConvert.DeserializeObject<Alarm[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetUndealAlarm" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 根据报警类型获取报警信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static Alarm[] GetAlarmInfo(string url,string flag)
        {
            var param = new Dictionary<string, string> {
               {"flag",flag},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Alarm[] ret = JsonConvert.DeserializeObject<Alarm[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarmInfo:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取所有报警
        /// </summary>
        /// <param name="url"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Alarm[] GetAllAlarmInfo(string url, AlarmFilter filter)
        {
            var param = new Dictionary<string, string> {
               {"sid", filter.sid.ToString()},
               {"altimeStart",filter.altimeStart.ToString()/*"2017-01-01 8:00:00" /*filter.altimeStart.ToString()*/},
               {"altimeStop",filter.altimeStop.ToString()/*"2019-12-12 8:00:00" /*filter.altimeStop.ToString()*/},
               {"flag", filter.flag},
               {"level", filter.level},
               {"state", filter.state},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                Alarm[] ret = JsonConvert.DeserializeObject<Alarm[]>(content);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), "GetAllAlarmInfo:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取处置预案
        /// </summary>
        /// <param name="url"></param>
        /// <param name="alarmid"></param>
        /// <returns></returns>
        public static TextPlan [] GetPlan(string url ,string alarmid,string flag)
        {
            var param = new Dictionary<string, string> {
               {"id",alarmid},
                {"flag",flag},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                TextPlan[] ret = JsonConvert.DeserializeObject<TextPlan[]>(content);
                return ret;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), "GetPlan:" + ex.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取异常摄像机
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ErrorCamera[] GetErrorCamera(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                ErrorCamera[] ret = JsonConvert.DeserializeObject<ErrorCamera[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetErrorCamera:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取去所有客户端状态
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ClientConfig[] GetAllClientInfo(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                ClientConfig[] ret = JsonConvert.DeserializeObject<ClientConfig[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAllClientInfo:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取所有报警组件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AlarmModule [] GetAlarmModule(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                AlarmModule[] ret = JsonConvert.DeserializeObject<AlarmModule[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarmModule:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取报警组件配置
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AlarmModuleConfig[] GetAlarmModuleConfig(string url)
        {
            string content = Post(url);
            if (content == null)
            {
                return null;
            }
            try
            {
                AlarmModuleConfig[] ret = JsonConvert.DeserializeObject<AlarmModuleConfig[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetAlarmModuleConfig:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 添加活动车辆
        /// </summary>
        /// <param name="url"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static TrackEvent[] InsertTrackEvent(string url, ActivityManage activity)
        {
            var param = new Dictionary<string, string> {
               //{"id",activity.ActivityId.ToString()},
               {"name",activity.ActivityName.ToString()},
               {"mapId",activity.MapId.ToString()},
               {"startTime",activity.StartTime.ToString()},
               {"stopTime",activity.EndTime.ToString()},
               {"status",activity.State.ToString()},
               {"deviceCameraIds",activity.DeviceIdList.ToString()},
               {"carJson",activity.CarNumber.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                TrackEvent[] ret = JsonConvert.DeserializeObject<TrackEvent[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "InsertTrackEvent:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 删除活动车辆
        /// </summary>
        /// <param name="url"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static TrackEvent[] DeleteTrackEvent(string url, ActivityManage activity)
        {
            var param = new Dictionary<string, string> {
               {"id",activity.ActivityId.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                TrackEvent[] ret = JsonConvert.DeserializeObject<TrackEvent[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "DeleteTrackEvent:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 更新活动车辆
        /// </summary>
        /// <param name="url"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static bool UpdateTrackEvent(string url, ActivityManage activity)
        {
            var param = new Dictionary<string, string> {
               {"id",activity.ActivityId.ToString()},
               {"name",activity.ActivityName.ToString()},
               {"mapId",activity.MapId.ToString()},
               {"startTime",activity.StartTime},
               {"stopTime",activity.EndTime},
               {"status",activity.State.ToString()},
               {"deviceCameraIds",activity.DeviceIdList.ToString()},
               {"carJson",activity.CarNumber.ToString()},//null
            };
            string content = Post(url, param);//For input string: \"\"
            if (content == null)
            {
                return false;
            }
            try
            {
                //var result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content); 
                //return result.success;
                return content.Equals("success");
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "UpdateTrackEvent:" + e.Message);
            }
            return false;
        }

        /// <summary>
        /// 获取活动车辆信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static TrackEvent[] GetTrackEvent(string url,int clientid)
        {
            var param = new Dictionary<string, string> {
               {"clientId",clientid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                TrackEvent[] ret = JsonConvert.DeserializeObject<TrackEvent[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetTrackEvent:" + e.Message);
            }
            return null;
        }
        /// <summary>
        /// 获取活动车辆进入信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="activityid"></param>
        /// <returns></returns>
        public static TracerCarInfo[] GetTrackerCarInfo(string url,int activityid)
        {
            var param = new Dictionary<string, string> {
               {"activityId",activityid.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                TracerCarInfo[] ret = JsonConvert.DeserializeObject<TracerCarInfo[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Logger.Error(typeof(HttpAPi), "GetTrackerCarInfo:" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<BitmapImage> LoadImage(string url)
        {
            if (string.IsNullOrEmpty (url))
            {
                return null;
            }
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest request = webRequest as HttpWebRequest;
            WebResponse response =await request.GetResponseAsync();
            Stream stream = response.GetResponseStream();
            BitmapImage ret = new BitmapImage();
            ret.BeginInit();
            ret.StreamSource = stream;
            ret.CacheOption = BitmapCacheOption.OnLoad;
            ret.EndInit();
            return ret;
        }
        /// <summary>
        /// Bitmap转BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        #region 拼控API

        public static bool SaveSceneList(SceneList sceneList,out string resultMsg)
        {
            resultMsg = "";
            string url = AppConfig.ServerBaseUri + AppConfig.InsertSceneList;            
            var json= JsonConvert.SerializeObject(sceneList);
            var param = new Dictionary<string, string> { { "sceneList", json } };
            try
            {
                var content = Post(url, param);
                var result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content);
                resultMsg = result.msg;
                return result.success;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "SaveSceneList()");
                return false;
            }
        }

        public static bool DeleteSceneList(int sceneId,out string resultMsg)
        {
            resultMsg = "";
            var url = AppConfig.ServerBaseUri + AppConfig.DeleteSceneList;
            var param = new Dictionary<string, string> { { "id", sceneId.ToString() } };
            try
            {
                var content = Post(url, param);//"" null
                if (string.IsNullOrEmpty(content))
                    return false;

                var result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content);
                resultMsg = result.msg;
                return result.success;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "DeleteSceneList()");
                return false;
            }
        }

        public static List<SceneList> QuerySceneList()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.QuerySceneList;
            try
            {
                var sceneList = new List<SceneList>();
                var content = Post(url);
                sceneList = JsonConvert.DeserializeObject<List<SceneList>>(content);
                return sceneList;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi),ex.Message+ "QuerySceneList()");
                return null;
            }
        }

        public static ObservableCollection<Model.PlanRotation> QueryPlanRotation()
        {
            var url = AppConfig.ServerBaseUri + AppConfig.QueryPlanRotation;
            try
            {
                var rotation = new ObservableCollection<Model.PlanRotation>();
                var content = Post(url);
                rotation = JsonConvert.DeserializeObject<ObservableCollection<Model.PlanRotation >>(content);
                return rotation;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "QueryPlanRotation()");
                return null;
            }
        }

        public static bool SavePlanRotation(Model.PlanRotation rotation, out string resultMsg)
        {
            resultMsg = "";
            var url = AppConfig.ServerBaseUri + AppConfig.InsertPlanRotation;
            try
            {
                var param = new Dictionary<string, string>
                {
                    { "sceneId", rotation.sceneId },
                    { "name", rotation.name },
                    { "duration", rotation.duration.ToString() },
                    { "time", rotation.time},
                    { "isOpen", rotation.isOpen.ToString()},
                    { "clientId", App.mango.getClientInfo().userId.ToString() },
                };
                var content = Post(url, param);
                if (string.IsNullOrEmpty(content))
                    return false;

                var result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content);
                resultMsg = result.msg;
                return result.success;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "SavePlanRotation()");
                return false;
            }
        }

        public static bool UpdatePlanRotation(Model.PlanRotation rotation)
        {    
            var url = AppConfig.ServerBaseUri + AppConfig.UpdatePlanRotation;
            try
            {
                var param = new Dictionary<string, string>
                {
                    { "id", rotation.id.ToString() },
                    { "sceneId", rotation.sceneId },
                    { "name", rotation.name },
                    { "duration", rotation.duration.ToString() },
                    { "time", rotation.time},
                    { "isOpen", rotation.isOpen.ToString()},
                };

                var content = Post(url, param);
                if (string.IsNullOrEmpty(content))
                    return false;

                var result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content);
                return result.success;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "UpdatePlanRotation()");
                return false;
            }
        }

        public static bool DeletePlanRotation(int rotationId)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.DeletePlanRotation;
                var param = new Dictionary<string, string>
                {
                    { "id", rotationId.ToString() },
                    { "clientId", App.mango.getClientInfo().userId.ToString() }
                };
                var content = Post(url, param);
                if (string.IsNullOrEmpty(content))
                    return false;

                MangoMapLibrary.Api.ApiResult result = JsonConvert.DeserializeObject<MangoMapLibrary.Api.ApiResult>(content);
                return result.success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static List<EncoderConfig> QueryAllEncoder()
        {
            try
            {
                var url = AppConfig.ServerBaseUri + AppConfig.GetAllEncoder;
                var param = new Dictionary<string, string> { { "id", App.mango.getClientInfo().userId.ToString() } };
                var content = Post(url, param);
                var encoders = JsonConvert.DeserializeObject<List<EncoderConfig>>(content);
                return encoders;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(HttpAPi), ex.Message + "QueryAllEncoder()");
                return null;
            }
        }

        #endregion

    }
}
