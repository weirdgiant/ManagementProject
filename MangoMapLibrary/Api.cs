using GeoAPI.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MangoMapLibrary.Api
{
    public class Sector
    {
        public double angle;
        public double r;

        public String toString()
        {
            return "Sector [angle=" + angle + ", r=" + r + "]";
        }

        public Sector()
        {

        }

        public Sector(double angle, double r)
        {
            this.angle = angle;
            this.r = r;
        }


    }


    public class MathUtil
    {
        public static PointF GetLocation(double r, double angle, PointF loc, double v)
        {
            PointF target = new PointF();
            double ang = (angle + v) / 180 * Math.PI;
            target.X = (float)(loc.X + r * Math.Sin(ang));
            target.Y = (float)(loc.Y - r * Math.Cos(ang));
            return target;
        }

        public static Sector GetAngle(PointF a, PointF o)
        {
            double hypotenuse = Math.Sqrt(Math.Pow(Math.Abs(o.X - a.X), 2) + Math.Pow(Math.Abs(o.Y - a.Y), 2));
            double shortedge = o.Y - a.Y;
            double angle = Math.Acos(shortedge / hypotenuse);
            angle = angle / Math.PI * 180;
            if (a.X < o.X)
            {
                angle = 0 - angle;
            }
            return new Sector(angle, hypotenuse);
        }

        public static PointF GetPointByAngle(PointF a, PointF o, double rotation)
        {
            int _rotation;
            Math.DivRem((int)rotation, 360, out _rotation);
            if (_rotation < 0)
            {
                _rotation = 360 + _rotation;
            }
            if (_rotation == 0)
            {
                return a;
            }

            Sector sec = GetAngle(a, o);
            PointF target = GetLocation(sec.r, _rotation, o, sec.angle);
            return target;
        }
    }

    public class Util
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
                Task<HttpResponseMessage> ret = client.PostAsync(url,null);
                ret.Wait();
                if (!ret.IsFaulted)
                {
                    return ret.Result.Content.ReadAsStringAsync().Result;

                }
            }
            catch (Exception e) { }

            return null;
        }

        public static bool UpdateLayerElement(string url, AbstractLayerElement element)
        {
            Dictionary<string, string> param = element.Ele.ToDict();
            param["iconExt"] = element.ToIconExt();
            
            string content = Post(url, param);
            Console.WriteLine(content);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            
            return result.success;
        }

        public static bool DeleteMap(string url, MangoMap map)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["id"] = map.id.ToString();

            string content = Post(url, param);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            return result.success;
        }

        public static bool InsertMap(string url, MangoMap map)
        {
            var param = new Dictionary<string, string> {
               {"name", map.name},
               {"type", map.type.ToString()},
               {"pid", map.pid.ToString()},
               {"idx", map.idx.ToString()},
               {"defaultLongitude", map.defaultLongitude.ToString()},
               {"defaultLatitude", map.defaultLatitude.ToString()},
               {"defaultZoomLevel", map.defaultZoomLevel.ToString()},
            };

            string content = Post(url, param);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            if (result.success)
            {
                map.id = int.Parse(result.msg);
            }

            return result.success;
        }

        public static bool DeleteLayer(string url, MangoLayer layer)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["id"] = layer.id.ToString();

            string content = Post(url, param);
            Console.WriteLine(content);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);

            return result.success;
        }

        public static bool InsertMapLayers(string url, MangoLayer layer)
        {
            var param = new Dictionary<string, string> {
               {"name", layer.name},
               {"mapId", layer.mapId.ToString()},
               {"color", layer.color},
               {"labelEnabled", layer.labelEnabled.ToString()},
               {"labelColor", layer.labelColor},
               {"labelFont", layer.labelFont},
               {"labelFontSize", layer.labelFontSize.ToString()},
               {"filenamePrefix", layer.filenamePrefix},
               {"renderMode", layer.renderMode.ToString()},
               {"textureFilename", layer.textureFilename},
               {"type", layer.type.ToString()},
            };
            string content = Post(url, param);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            if (result.success)
            {
                layer.id = int.Parse(result.msg);
            }
            return result.success;
        }

        public static bool UpdateMapLayers(string url, MangoLayer layer)
        {
            var param = new Dictionary<string, string> {
               {"id", layer.id.ToString()},
               {"name", layer.name},
               {"mapId", layer.mapId.ToString()},
               {"color", layer.color},
               {"labelEnabled", layer.labelEnabled.ToString()},
               {"labelColor", layer.labelColor},
               {"labelFont", layer.labelFont},
               {"labelFontSize", layer.labelFontSize.ToString()},
               {"filenamePrefix", layer.filenamePrefix},
               {"renderMode", layer.renderMode.ToString()},
               {"textureFilename", layer.textureFilename},
               {"type", layer.type.ToString()},
            };
            string content = Post(url, param);
            Console.WriteLine(content);

            return true;
        }

        public static bool InsertLayerElement(string url, AbstractLayerElement element)
        {
            Dictionary<string, string> param = element.Ele.ToDict();
            param["iconExt"] = element.ToIconExt();
            
            string content = Post(url, param);
            Console.WriteLine(content);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            if (result.success)
            {
                element.Id = int.Parse(result.msg);
            }
            return result.success;
        }

        public static void DeleteLayerElement(string url,int eleId)
        {
            var param = new Dictionary<string, string> {
               {"id", eleId.ToString()},
            };
            Post(url, param);
        }

        public static MangoLayer[] GetLayerList(string url,int mapId)
        {
            var param = new Dictionary<string, string> {
               {"mapId", mapId.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                MangoLayer[] ret = JsonConvert.DeserializeObject<MangoLayer[]>(content);
                return ret;
            }catch(Exception e)
            {
                
            }
            return null;
        }

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

        public static MangoMap[] GetMapList(string url)
        {
            string content = Post(url);
            if(content == null)
            {
                return null;
            }
            try
            {
                MangoMap[] ret = JsonConvert.DeserializeObject<MangoMap[]>(content);
                return ret;
            }
            catch(Exception e)
            {

            }
            return null;
        }

        public static MangoLayerElement[] GetMapElements(string url, int mapId)
        {
            var param = new Dictionary<string, string> {
               {"mapId", mapId.ToString()},
            };
            string content = Post(url, param);
            if (content == null)
            {
                return null;
            }
            try
            {
                MangoLayerElement[] ret = JsonConvert.DeserializeObject<MangoLayerElement[]>(content);
                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public static bool UpdateMap(string url, MangoMap map)
        {
            var param = new Dictionary<string, string> {
               {"id", map.id.ToString()},
               {"name", map.name},
               {"type", map.type.ToString()},
               {"pid", map.pid.ToString()},
               {"idx", map.idx.ToString()},
               {"defaultLongitude", map.defaultLongitude.ToString()},
               {"defaultLatitude", map.defaultLatitude.ToString()},
               {"defaultZoomLevel", map.defaultZoomLevel.ToString()},
            };

            string content = Post(url, param);
            Console.WriteLine(content);

            return true;
        }

        public static bool SortIndexForMap(string url, int pid, string data)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param["pid"] = pid.ToString();
            param["ids"] = data;

            string content = Post(url, param);
            Console.WriteLine(content);
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
            
            return result.success;
        }
    }

    public class ApiResult
    {
        public bool success;
        public string msg;
    }

    public class MangoLayerElement
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


        public Dictionary<string,string> ToDict()
        {
            var param = new Dictionary<string, string> {
               {"id", id.ToString()},
               {"deviceId", deviceId.ToString()},
               {"longitude", longitude.ToString()},
               {"latitude", latitude.ToString()},
               {"deviceTypeCode", deviceTypeCode},
               {"iconAngle", iconAngle.ToString()},
               {"iconExt", iconExt},
               {"mapId", mapId.ToString()},
               {"code", code},
               {"name", name},
            };
            return param;
        }

        /**
         * JSON 使用
         * **/
        [JsonConstructor]
        public MangoLayerElement()
        {

        }

        public MangoLayerElement(string name,string deviceTypeCode)
        {
            this.code = Guid.NewGuid().ToString();
            this.name = name;
            this.deviceTypeCode = deviceTypeCode;
        }
               
        public MangoLayerElement(Device device)
        {
            this.deviceTypeCode = device.deviceTypeId;
            this.code = device.code;
            this.name = device.name;
            this.deviceId = device.id;
        }

        public MangoLayerElement(int mapId,Coordinate p, string deviceTypeCode)
        {
            this.mapId = mapId;
            this.code = Guid.NewGuid().ToString();
            this.deviceTypeCode = deviceTypeCode;
            this.latitude = p.X;
            this.longitude = p.Y;
        }
    }

    public class IconExtParamFence
    {
        public PointF[] points;
        public string type;
    }

    public class IconExtParamBuilding
    {
        public string filename;
        public double zoomLevel;
        public double width;
        public int linkMap;

        public IconExtParamBuilding Clone()
        {
            IconExtParamBuilding instance = new IconExtParamBuilding();
            instance.filename = filename;
            instance.zoomLevel = zoomLevel;
            instance.linkMap = linkMap;
            return instance;
        }
    }

    public class MangoLayer
    {
        public int id;
        public string name;
        public int mapId;
        public string color;
        public int labelEnabled;
        public string labelColor;
        public string labelFont;
        public int labelFontSize;
        public string filenamePrefix;
        public int renderMode;
        public string textureFilename;
        public int type;
    }

    public class MangoMap
    {
        public int id;
        public string name;
        public int type;
        public int pid;
        public int idx;
        public double defaultLongitude;
        public double defaultLatitude;
        public int defaultZoomLevel;
        public int backgroundColor;
    }

    public class Device
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
        public int brand;
        public string deviceDesc;
        public int deviceNode;
        public string deviceMacaddr;
        public int ptz;
        public int status;
        public int alertObjId;
        public string unitType;
        public string deviceTypeId;
        public string deviceStatus;
    }

    public class DeviceType
    {
        public int id;
        public string deviceTypeName;
        public string deviceImg;
        public string checkedImg;
        public string alarmImg;
        public string deviceClass;
        public string deviceTypeCode;
        public int showType;

        public Bitmap deviceImage;
        public Bitmap checkedImage;
        public Bitmap alarmImage;


        public ElementIconSet ToIconSet()
        {
            ElementIconSet set = new ElementIconSet(
                deviceImage.Clone() as Bitmap,
                checkedImage.Clone() as Bitmap,
                alarmImage.Clone() as Bitmap);
            return set;
        }
    }
}
