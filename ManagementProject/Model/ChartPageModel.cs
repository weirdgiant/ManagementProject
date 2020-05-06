using MangoApi;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ManagementProject.Model
{
    public class ChartPageModel : INotifyPropertyChangedClass
    {
        private bool isShowMap;

        public bool IsShowMap
        {
            get { return isShowMap; }
            set
            {
                isShowMap = value;
                NotifyPropertyChanged("IsShowMap");
            }
        }

        private ObservableCollection<ErrorDeviceType> _errorDeviceList;

        public ObservableCollection<ErrorDeviceType> ErrorDeviceList
        {
            get { return _errorDeviceList; }
            set
            {
                _errorDeviceList = value;
                NotifyPropertyChanged("ErrorDeviceList");
            }
        }

        private ObservableCollection<ErrorDeviceType> _errorDeviceType;

        public ObservableCollection<ErrorDeviceType> ErrorDeviceType
        {
            get { return _errorDeviceType; }
            set
            {
                _errorDeviceType = value;
                NotifyPropertyChanged("ErrorDeviceType");
            }
        }

        private ObservableCollection<DeviceClass> _errorDevice;

        public ObservableCollection<DeviceClass> ErrorDevice
        {
            get { return _errorDevice; }
            set
            {
                _errorDevice = value;
                NotifyPropertyChanged("ErrorDevice");
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

        private string _currentMonthCount;
        public string CurrentMonthCount
        {
            get
            {
                return _currentMonthCount;
            }
            set
            {
                _currentMonthCount = value;
                NotifyPropertyChanged("CurrentMonthCount");
            }
        }

        private string _currentYearCount;
        public string CurrentYearCount
        {
            get
            {
                return _currentYearCount;
            }

            set
            {
                _currentYearCount = value;
                NotifyPropertyChanged("CurrentYearCount");
            }
        }

        private string _deviceClass;
        public string DeviceClass
        {
            get
            {
                return _deviceClass;
            }

            set
            {
                _deviceClass = value;
                NotifyPropertyChanged("DeviceClass");
            }
        }
        private string _deviceClassName;
        public string DeviceClassName
        {
            get
            {
                return _deviceClassName;
            }

            set
            {
                _deviceClassName = value;
                NotifyPropertyChanged("DeviceClassName");
            }
        }
        private string _deviceClassCount;
        public string DeviceClassCount
        {
            get
            {
                return _deviceClassCount;
            }

            set
            {
                _deviceClassCount = value;
                NotifyPropertyChanged("DeviceClassCount");
            }
        }

        private string _errorCount;
        public string ErrorCount
        {
            get
            {
                return _errorCount;
            }

            set
            {
                _errorCount = value;
                NotifyPropertyChanged("ErrorCount");
            }
        }
 private string _currentTime;
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }

        private string _currentDate;
        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }
            set
            {
                _currentDate = value;
                NotifyPropertyChanged("CurrentDate");
            }
        }
    }

    /// <summary>
    /// Equipment classification
    /// </summary>
    public class DeviceClass : ErrorDeviceType
    {
        public string DeviceClassIcon { get; set; }

        public Brush ErrorCountForeground { get; set; }
    }

    public class ChartHistory:History
    {
        public Brush TodayLogFill { get; set; }
        public Brush StateForeground { get; set; }
    }
}
