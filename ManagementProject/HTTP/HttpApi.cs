using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
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
            catch (Exception e) { }

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
            catch (Exception e) { }

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

            }
            return null;
        }
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static DeviceType[] GetDeviceTypeList(string url)
        {
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

            }
            return null;
        }
        /// <summary>
        /// 获取所有报警
        /// </summary>
        /// <param name="url"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static Alarm[] GetAlarmInfo(string url, AlarmFilter filter)
        {
            var param = new Dictionary<string, string> {
               {"sid", filter.sid.ToString()},
               {"altimeStart", filter.altimeStart.ToString()},
               {"altimeStop", filter.altimeStop.ToString()},
               {"flag", filter.flag.ToString()},
               {"level", filter.level.ToString()},
               {"state", filter.state.ToString()},
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

            }
            return null;
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static BitmapImage LoadImage(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest request = webRequest as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            BitmapImage ret = new BitmapImage();
            ret.BeginInit();
            ret.StreamSource = stream;
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
    }
}
