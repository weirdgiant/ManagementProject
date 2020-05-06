using ManagementProject.Converters;
using ManagementProject.ViewModel;
using MangoApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// AlarmCarInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmCarInfo : UserControl
    {
        public AlarmCarInfo()
        {
            InitializeComponent();
        }
    }

    #region Model
    public class AlarmCarInfoModel:INotifyPropertyChangedClass
    {
        #region 报警车辆信息
        private string _alarmTime;
        public string AlarmTime
        {
            get
            {
                return _alarmTime;
            }
            set
            {
                _alarmTime = value;
                NotifyPropertyChanged("AlarmTime");
            }
        }
        private string _alarmCarNumber;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string AlarmCarNumber
        {
            get
            {
                return _alarmCarNumber;
            }
            set
            {
                _alarmCarNumber = value;
                NotifyPropertyChanged("AlarmCarNumber");
            }
        }
        private string _alarmCarOwner;
        /// <summary>
        /// 车主
        /// </summary>
        public string AlarmCarOwner
        {
            get
            {
                return _alarmCarOwner;
            }
            set
            {
                _alarmCarOwner = value;
                NotifyPropertyChanged("AlarmCarOwner");
            }
        }

        private string _phoneNymber;
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return _phoneNymber;
            }
            set
            {
                _phoneNymber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        private string _department;
        /// <summary>
        /// 部门
        /// </summary>
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

        private BitmapImage _carLicenseImage;
        /// <summary>
        /// 车牌抓拍
        /// </summary>
        public BitmapImage CarLicenseImage
        {
            get
            {
                return _carLicenseImage;
            }
            set
            {
                _carLicenseImage = value;
                NotifyPropertyChanged("CarLicenseImage");
            }
        }

        private BitmapImage _sceneImage;
        /// <summary>
        /// 现场抓拍
        /// </summary>
        public BitmapImage SceneImage
        {
            get
            {
                return _sceneImage;
            }
            set
            {
                _sceneImage = value;
                NotifyPropertyChanged("SceneImage");
            }
        }

        private ObservableCollection<ParkingInfo> _alarmCarList;
        public ObservableCollection <ParkingInfo> AlarmCarList
        {
            get
            {
                return _alarmCarList;
            }
            set
            {
                _alarmCarList = value;
                NotifyPropertyChanged("AlarmCarList");
            }
        }


        public class ParkingInfo
        {
            public int AlarmId { get; set; }
            public string CarNumber { get; set; }
            public string ParkingTime { get; set; }
            public string PhoneNumber { get; set; }
            public string Owner { get; set; }
            public string Dep { get; set; }
            public string Time { get; set; }
            public BitmapImage SceneImage { get; set; }
            public BitmapImage CarLicenseImage { get; set; }
        }
        #endregion
    }
    #endregion

    #region ViewModel
    public class AlarmCarInfoViewModel:AlarmCarInfoModel
    {
        private AlarmPageViewModel alarmPageViewModel { get; set; }
        public DelegateCommand SelectAlarmItemComand { get; set; }
        public AlarmCarInfoViewModel(AlarmPageViewModel _alarmPageViewModel)
        {
            alarmPageViewModel = _alarmPageViewModel;
            SelectAlarmItemComand = new DelegateCommand();
            SelectAlarmItemComand.ExecuteCommand = new Action<object>(SelectAlarmItem);

            AlarmCarList = new ObservableCollection<ParkingInfo>();
            Init();
        }

        private void SelectAlarmItem(object obj)
        {
            ParkingInfo info = (ParkingInfo)obj;
            AlarmTime = info.Time ;
            AlarmCarNumber = info.CarNumber;
            AlarmCarOwner = info.Owner;
            PhoneNumber = info.PhoneNumber;
            Department = info.Dep;
            alarmPageViewModel.disposalPlanViewModel.AlarmID = info.AlarmId;
        }

        private async void Init()
        {
            try
            {
                Alarm[] selectedAlarm = alarmPageViewModel.AlarmList.Where(x => x.id == alarmPageViewModel.SelectedId).ToArray();
                string code = selectedAlarm[0].sersor;
                Alarm[] alarm = alarmPageViewModel.AlarmList.Where(x => x.sersor == code).ToArray();
                for (int i = 0; i < alarm.Length; i++)
                {
                    AlarmCar carinfo = JsonConvert.DeserializeObject<AlarmCar>(alarm[i].peculiarnote);
                    ParkingInfo info = new ParkingInfo
                    {
                        AlarmId = alarm[i].id,
                        CarNumber = carinfo.Plate,
                        ParkingTime = ((int.Parse(carinfo.Illtime) / 60000) + 3).ToString(),
                        PhoneNumber = carinfo.Phone,
                        Owner = carinfo.Name,
                        Dep = carinfo.Department,
                        Time = TimerConvert.ConvertTimeStampToDateTime(long.Parse(alarm[i].altime)).ToString("yyyy-MM-dd HH:mm:ss"),
                        CarLicenseImage = await HttpAPi.LoadImage(carinfo.CarUrl),
                        SceneImage = await HttpAPi.LoadImage(carinfo.SceneUrl),
                    };
                    AlarmCarList.Add(info);
                }

                #region Test Data

                //for (int i = 0; i < 10; i++)
                //{
                //    AlarmCarList.Add(new ParkingInfo
                //    {
                //        AlarmId = 1889 + i,
                //        CarNumber = $"沪A00{i.ToString()}",
                //        ParkingTime = (230 + i).ToString(),
                //        PhoneNumber = "17835454545",
                //        Owner = $"快呀呀{i.ToString()}",
                //        Dep = "研发部",
                //        Time = "2019-10-10 11:39:49",
                //    });
                //}

                #endregion

                AlarmCarList = new ObservableCollection<ParkingInfo>(AlarmCarList.OrderByDescending(i => Convert.ToInt32(i.ParkingTime)));
                SetAlarmCarInfo(AlarmCarList);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AlarmCarInfoViewModel), "Init()" + ex.Message);
            }
        }

        /// <summary>
        /// 初始化报警车辆信息
        /// </summary>
        public void SetAlarmCarInfo(ObservableCollection<ParkingInfo> list)
        {
            AlarmTime = list[0].Time;
            AlarmCarNumber = list[0].CarNumber;
            AlarmCarOwner = list[0].Owner;
            PhoneNumber = list[0].PhoneNumber;
            Department = list[0].Dep;
            CarLicenseImage = list[0].CarLicenseImage;
            SceneImage = list[0].SceneImage;
        }
    }
    #endregion
}
