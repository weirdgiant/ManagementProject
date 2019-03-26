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
        public MangoInfo()
        {
            LoadAlarmLevel();
            LoadAlarmType();
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
    }
}
