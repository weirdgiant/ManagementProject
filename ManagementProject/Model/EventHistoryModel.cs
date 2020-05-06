using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ManagementProject.Model
{
    public class EventHistoryModel : INotifyPropertyChangedClass
    {
        private int currentPageNumber = 1;
        private int pageDataCount = 20;
        private int totalDataCount;

        /// <summary>
        /// 选择页
        /// </summary>
        public int CurrentPageNumber
        {
            get { return currentPageNumber; }
            set
            {
                currentPageNumber = value;
                UpdatePageData();
                NotifyPropertyChanged("CurrentPageNumber");
            }
        }

        /// <summary>
        /// 每页可显示的数据
        /// </summary>
        public int PageDataCount
        {
            get { return pageDataCount; }
            set
            {
                pageDataCount = value;
                UpdatePageData();
                NotifyPropertyChanged("PageDataCount");
            }
        }



        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalDataCount
        {
            get { return totalDataCount; }
            set
            {
                totalDataCount = value;
                NotifyPropertyChanged("TotalDataCount");
            }
        }

        public void UpdatePageData()
        {
            if (AlarmHistory == null)
                return;
            TotalDataCount = AlarmHistory.Count;

            //根据选择页和每页可显示的数据计算当前应该添加的数据量
            int addDataCount = 0;
            if (CurrentPageNumber * PageDataCount <= TotalDataCount)
            {
                addDataCount = PageDataCount;
            }
            else
            {
                addDataCount = TotalDataCount - (CurrentPageNumber - 1) * PageDataCount;
            }
            GetData((CurrentPageNumber - 1) * PageDataCount, addDataCount);

        }

        private void GetData(int CountBegin,int CountGet)
        {
            AlarmHistoryData = new ObservableCollection<History>(AlarmHistory.Skip(CountBegin).Take(CountGet).ToArray());
        }


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


        private ObservableCollection<History> _alarmHistoryData;
        public ObservableCollection<History> AlarmHistoryData
        {
            get
            {
                return _alarmHistoryData;
            }
            set
            {
                _alarmHistoryData = value;
                NotifyPropertyChanged("AlarmHistoryData");
            }
        }

        private ObservableCollection<History> _alarmHistory;
        public ObservableCollection<History> AlarmHistory
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

        #region 下载控件属性

        private string labValue;

        public string LabValue
        {
            get { return labValue; }
            set
            {
                labValue = value;
                NotifyPropertyChanged("LabValue");
            }
        }

        private double pbValue;

        public double PbValue
        {
            get { return pbValue; }
            set
            {
                pbValue = value;
                NotifyPropertyChanged("PbValue");
                IsShowFolder = (PbValue == pbMax);
            }
        }

        private double pbMax = 100;

        public double PbMax
        {
            get { return pbMax; }
            set
            {
                pbMax = value;
                NotifyPropertyChanged("PbMax");
            }
        }

        private bool isShowFolder;

        public bool IsShowFolder
        {
            get { return isShowFolder; }
            set
            {
                isShowFolder = value;
                NotifyPropertyChanged("IsShowFolder");
                if (IsShowFolder)
                {
                    IsShowProgressBar = IsShowDownloadButton = !IsShowFolder;
                }
            }
        }

        private bool isShowProgressBar;

        public bool IsShowProgressBar
        {
            get { return isShowProgressBar; }
            set
            {
                isShowProgressBar = value;
                NotifyPropertyChanged("IsShowProgressBar");
                if (IsShowProgressBar)
                {
                    IsShowFolder = IsShowDownloadButton = !IsShowProgressBar;
                }
            }
        }

        private bool isShowDownloadButton;

        public bool IsShowDownloadButton
        {
            get { return isShowDownloadButton; }
            set
            {
                isShowDownloadButton = value;
                NotifyPropertyChanged("IsShowDownloadButton");
                if (IsShowDownloadButton)
                {
                    IsShowProgressBar = IsShowFolder = !IsShowDownloadButton;
                }
            }
        }
        #endregion

    }

    public class History
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string AlarmSignal { get; set; }
        public string ConfirmTime { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Level { get; set; }
        public string Device { get; set; }
        public string State { get; set; }     
        public string ConfirmNote { get; set; }
        public string MapId { get; set; }
    }
}
