using System;
using System.Collections.Generic;
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
        public string EnentEndTime
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
    }
}
