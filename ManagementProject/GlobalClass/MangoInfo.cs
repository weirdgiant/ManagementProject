using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject
{
    public class MangoInfo
    {
        public static MangoInfo instance = new MangoInfo();
        public List<AlarmLevel> AlarmLevels { get; set; }
        public List <AlarmTypeInfo> AlarmTypeInfos { get; set; }
        public List <Device> CameraList { get; set; }
        public List <SystemParameter> SystemParameterList { get; set; }
        public List <ClientConfig> ClientConfigs { get; set; }
        public List<EncoderConfig> EncoderConfigs { get; set; }

        public MangoInfo()
        {
            LoadAlarmLevel();
            LoadAlarmType();
            LoadCameraList();
            LoadSystemParameterList();
            LoadClientConfigs();

            LoadAllEncoder();
        }

        private void LoadAllEncoder()
        {
            EncoderConfigs = HttpAPi.QueryAllEncoder();

            if (EncoderConfigs != null && EncoderConfigs.Count > 0)
            {
               for (int i = 0; i < EncoderConfigs.Count; i++)
               {
                   var encoder = EncoderConfigs[i];
                   encoder.id = 255 - i;
               }
            }
        }

        private void LoadClientConfigs()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllClientInfo;
            ClientConfig[] clientConfigs = HttpAPi.GetAllClientInfo(url);
            ClientConfigs = new List<ClientConfig>(clientConfigs);
        }
        /// <summary>
        /// 加载所有报警级别
        /// </summary>
        private void LoadAlarmLevel()
        {
            AlarmLevel[] alarmLevels = HttpAPi.GetAlarmLevel(AppConfig.ServerBaseUri + AppConfig.GetAlarmLevel);
            AlarmLevels = new List<AlarmLevel>(alarmLevels);
        }
        /// <summary>
        /// 加载所有报警类型
        /// </summary>
        private void LoadAlarmType()
        {
            AlarmTypeInfo[] alarmTypeInfo = HttpAPi.GetAlarmType(AppConfig.ServerBaseUri + AppConfig.GetAlarmType);
            AlarmTypeInfos = new List<AlarmTypeInfo>(alarmTypeInfo);
        }

        private void LoadCameraList()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllDeviceList;
            Device[] devices = HttpAPi.GetDeviceList(url);
            CameraList = new List<Device>(devices);
        }

        private void LoadSystemParameterList()
        {
            try
            {
                SystemParameter[] systemParameters = HttpAPi.GetSystemParameter();
                SystemParameterList = new List<SystemParameter>(systemParameters);
                SystemParameter[] ret = SystemParameterList.Where(x => x.parameter_code == "SYS000003").ToArray();
                if (ret != null)
                {
                    GlobalVariable.MaxCameraCount = int.Parse(ret[0].parameter_value.Trim());
                }
                SystemParameter[] result = SystemParameterList.Where(x => x.parameter_code == "SYS000004").ToArray();
                if (result != null)
                {
                    if (int.Parse(result[0].status) == 1)
                    {
                        GlobalVariable.IsFake = true;
                    }
                    else
                    {
                        GlobalVariable.IsFake = false;
                    }

                }
            }
            catch(Exception ex)
            {
                Logger.Error("LoadSystemParameterList:" + ex.Message);
            }
        }
    }
}
