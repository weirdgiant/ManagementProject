using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class EventHistoryModel : INotifyPropertyChangedClass
    {
        private string _eventStartTime;
        /// <summary>
        /// 事件开始时间
        /// </summary>
        public string EventStartTime
        {
            get
            {
                return _eventStartTime;
            }
            set
            {
                _eventStartTime = value;
                NotifyPropertyChanged("EventStartTime");
            }
        }

        private string _eventEndTime;
        /// <summary>
        /// 事件结束时间
        /// </summary>
        public string EventEndTime
        {
            get
            {
                return _eventEndTime;
            }
            set
            {
                _eventEndTime = value;
                NotifyPropertyChanged("EventEndTime");
            }
        }

        private string _alarmType;
        /// <summary>
        /// 报警类型
        /// </summary>
        public string AlarmType
        {
            get
            {
                return _alarmType;
            }
            set
            {
                _alarmType = value;
                NotifyPropertyChanged("AlarmType");
            }
        }
        private string _alarmLevel;
        /// <summary>
        /// 报警级别
        /// </summary>
        public string AlarmLevel
        {
            get
            {
                return _alarmLevel;
            }
            set
            {
                _alarmLevel = value;
                NotifyPropertyChanged("AlarmLevel");
            }
        }

        private string _alarmState;
        /// <summary>
        /// 处理状态
        /// </summary>
        public string AlarmState
        {
            get
            {
                return _alarmState;
            }
            set
            {
                _alarmState = value;
                NotifyPropertyChanged("AlarmState");
            }
        }

        private ObservableCollection<Alarm> _alarmHistory;
        public ObservableCollection<Alarm> AlarmHistory
        {
            get
            {
                return _alarmHistory;
            }
            set
            {
                _alarmHistory = value;
                NotifyPropertyChanged("AlarmHistory");
            }
        }

        private ObservableCollection<string> _alarmLevelList;
        public ObservableCollection<string> AlarmLevelList
        {
            get
            {
                return _alarmLevelList;
            }
            set
            {
                _alarmLevelList = value;
                NotifyPropertyChanged("AlarmLevelList");
            }
        }
        private ObservableCollection<string> _alarmTypeInfoList;
        public ObservableCollection<string> AlarmTypeInfoList
        {
            get
            {
                return _alarmTypeInfoList;
            }
            set
            {
                _alarmTypeInfoList = value;
                NotifyPropertyChanged("AlarmTypeInfoList");
            }
        }

        private ObservableCollection<string> _alarmStateList;
        public ObservableCollection <string > AlarmStateList
        {
            get
            {
                return _alarmStateList;
            }
            set
            {
                _alarmStateList = value;
                NotifyPropertyChanged ("AlarmStateList");
            }
        }

    }
}
