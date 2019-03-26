using ManagementProject.Model;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class EventHistoryViewModel : EventHistoryModel
    {
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public EventHistoryViewModel()
        {
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteCommand = new Action<object>(Close);
            SearchCommand = new DelegateCommand();
            SearchCommand.ExecuteCommand = new Action<object>(Search);
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
        }

        private void Loaded(object obj)
        {
            string[] alarmStateList = { "所有状态", "真实报警", "确认误报", "设备故障", "确认误报" };
            AlarmLevelList = new ObservableCollection<string>();
            AlarmLevelList.Add("所有级别");
            AlarmTypeInfoList = new ObservableCollection<string>();
            AlarmTypeInfoList.Add("所有类型");
            AlarmStateList = new ObservableCollection<string>(alarmStateList);           
            foreach (var item in MangoInfo .instance .AlarmLevels)
            {
                string level = item.listName;
                AlarmLevelList.Add(level);
            }
            foreach (var item in MangoInfo.instance.AlarmTypeInfos)
            {
                string type = item.listName;
                AlarmTypeInfoList.Add(type);
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="obj"></param>
        private void Search(object obj)
        {
            AlarmHistory = new ObservableCollection<Alarm>();
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllAlarmInfo;
            AlarmFilter alarmFilter = new AlarmFilter
            {
                altimeStart = EventStartTime,
                altimeStop = EventEndTime,
                level = GetSelectedLevel(AlarmLevel),
                flag = GetSelectAlarmType( AlarmType),
                state = GetSelectAlarmState(AlarmState),
                sid = 1,
            };

            Alarm[] alarms = HttpAPi.GetAlarmInfo(url, alarmFilter);
            AlarmHistory = new ObservableCollection<Alarm>(alarms);
        }

        private string GetSelectedLevel(string level)
        {
            if (level!="所有级别")
            {
                List<AlarmLevel> alarmLevel = MangoInfo.instance.AlarmLevels.Where(AlarmLevel => AlarmLevel.listName == level).ToList();
                return alarmLevel[0].listValue;
            }
            return null;           
        }

        private string GetSelectAlarmType(string type)
        {
            if(type!= "所有类型")
            {
                List<AlarmTypeInfo> alarmType = MangoInfo.instance.AlarmTypeInfos.Where(AlarmTypeInfo => AlarmTypeInfo.listName == type).ToList();
                return alarmType[0].listValue;
            }
            return null;
        }
        private string GetSelectAlarmState(string state)
        {
            if (state!= "所有状态")
            {

            }
            return null;
        }

        private void Close(object obj)
        {
            if (obj != null)
            {
                EventHistory _eventHistory = (EventHistory)obj;
                _eventHistory.Close();
            }
        }
    }
}
