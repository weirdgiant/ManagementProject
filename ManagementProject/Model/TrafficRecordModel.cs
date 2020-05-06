using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class TrafficRecordModel : INotifyPropertyChangedClass
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
            if (TrafficRecordList == null)
                return;
            TotalDataCount = TrafficRecordList.Count;

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

        private void GetData(int CountBegin, int CountGet)
        {
            TrafficRecordListData = new ObservableCollection<AlarmTraffic>(TrafficRecordList.Skip(CountBegin).Take(CountGet).ToArray());
        }


        private string _startTime;
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }
        private string _endTime;
        public string EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }

        private string _overSpeedStartTime;
        public string OverSpeedStartTime
        {
            get
            {
                return _overSpeedStartTime;
            }
            set
            {
                _overSpeedStartTime = value;
                NotifyPropertyChanged("OverSpeedStartTime");
            }
        }
        private string _overSpeedEndTime;
        public string OverSpeedEndTime
        {
            get
            {
                return _overSpeedEndTime;
            }
            set
            {
                _overSpeedEndTime = value;
                NotifyPropertyChanged("OverSpeedEndTime");
            }
        }

        private string _parkingStartTime;
        public string ParkingStartTime
        {
            get
            {
                return _parkingStartTime;
            }
            set
            {
                _parkingStartTime = value;
                NotifyPropertyChanged("ParkingStartTime");
            }
        }
        private string _parkingEndTime;
        public string ParkingEndTime
        {
            get
            {
                return _parkingEndTime;
            }
            set
            {
                _parkingEndTime = value;
                NotifyPropertyChanged("ParkingEndTime");
            }
        }


        private string _alarmDevice;
        public string AlarmDevice
        {
            get
            {
                return _alarmDevice;
            }
            set
            {
                _alarmDevice = value;
                NotifyPropertyChanged("AlarmDevice");
            }
        }
        private string _alarmSignal;
        public string AlarmSignal
        {
            get
            {
                return _alarmSignal;
            }
            set
            {
                _alarmSignal = value;
                NotifyPropertyChanged("AlarmSignal");
            }
        }
        private string _carNumber;
        public string CarNumber
        {
            get
            {
                return _carNumber;
            }
            set
            {
                _carNumber = value;
                NotifyPropertyChanged("CarNumber");
            }
        }

        private string _carOwner;
        public string CarOwner
        {
            get
            {
                return _carOwner;
            }
            set
            {
                _carOwner = value;
                NotifyPropertyChanged("CarOwner");
            }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged("PhoneNumber");

            }
        }

        private string _department;
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                NotifyPropertyChanged("Department");
            }
        }

        private ObservableCollection<string> _alarmSignalList;
        public ObservableCollection <string> AlarmSignalList
        {
            get
            {
                return _alarmSignalList;
            }
            set
            {
                _alarmSignalList = value;
                NotifyPropertyChanged("AlarmSignalList");
            }
        }

        private ObservableCollection<AlarmTraffic> _trafficRecordList;
        public ObservableCollection<AlarmTraffic> TrafficRecordList
        {
            get
            {
                return _trafficRecordList;
            }
            set
            {
                _trafficRecordList = value;
                NotifyPropertyChanged("TrafficRecordList");
            }
        }

        private ObservableCollection<AlarmTraffic> _trafficRecordListData;
        public ObservableCollection<AlarmTraffic> TrafficRecordListData
        {
            get
            {
                return _trafficRecordListData;
            }
            set
            {
                _trafficRecordListData = value;
                NotifyPropertyChanged("TrafficRecordListData");
            }
        }

        private ObservableCollection<AlarmCarInfo> _overSpeedDevice;
        public ObservableCollection<AlarmCarInfo> OverSpeedDevice
        {
            get
            {
                return _overSpeedDevice;
            }
            set
            {
                _overSpeedDevice = value;
                NotifyPropertyChanged("OverSpeedDevice");
            }
        }

        private ObservableCollection<AlarmCarInfo> _overSpeedCar;
        public ObservableCollection<AlarmCarInfo> OverSpeedCar
        {
            get
            {
                return _overSpeedCar;
            }
            set
            {
                _overSpeedCar = value;
                NotifyPropertyChanged("OverSpeedCar");
            }
        }
        private ObservableCollection<AlarmCarInfo> _parkingDevice;
        public ObservableCollection<AlarmCarInfo> ParkingDevice
        {
            get
            {
                return _parkingDevice;
            }
            set
            {
                _parkingDevice = value;
                NotifyPropertyChanged("ParkingDevice");
            }
        }

        private ObservableCollection<AlarmCarInfo> _parkingCar;
        public ObservableCollection<AlarmCarInfo> ParkingCar
        {
            get
            {
                return _parkingCar;
            }
            set
            {
                _parkingCar = value;
                NotifyPropertyChanged("ParkingCar");
            }
        }


        public class AlarmTraffic
        {
            public string AlarmTime { get; set; }
            public string Loacation { get; set; }
            public string AlarmDevice { get; set; }
            public string CarNumber { get; set; }
            public string AlarmSignal { get; set; }
            public string CarOwner { get; set; }
            public string Phone { get; set; }
            public string Department { get; set; }
            public string CarNumberPic{ get; set; }
            public string LocationPic { get; set; }
        }
        public class AlarmCarInfo
        {
            public string CarNumber { get; set; }
            public string AlarmCount { get; set; }
            public string CarOwner { get; set; }
            public string Phone { get; set;}
            public string Department { get; set; }
            public string AlarmDevice { get; set; }
            public string Location { get; set; }
            public string DeviceCode { get; set; }
            public string DeviceType { get; set; }
        }
    }
}
