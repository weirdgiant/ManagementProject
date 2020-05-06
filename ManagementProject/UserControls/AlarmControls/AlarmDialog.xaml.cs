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

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// AlarmDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDialog : UserControl
    {
        private AlarmDialog()
        {
            InitializeComponent();
        }

        public AlarmDialog(Alarm alarm)
        {
            InitializeComponent();
            DataContext = new AlarmDialogViewModel(alarm);
        }
    }
    public class AlarmDialogViewModel : AlarmDialogModel
    {
        public AlarmDialogViewModel(Alarm alarm)
        {
            InitDialog(alarm);
        }

        private void InitDialog(Alarm alarm)
        {
            if (alarm == null)
                return;

            string alarmType = alarm.flag.Trim();
            string signalType = alarmType + "-" + alarm.type;
            Signal signal = GlobalVariable.Signals.Where(x => x.signal_id == signalType).ToArray()[0];
            AlarmTypeRemark = signal.signal_name + "  " + alarm.level + "级";
            if (alarm.fake && GlobalVariable.IsFake)
            {
                AlarmFake = "模拟报警";
            }  
            DeviceName = alarm.sersorname;
            AlarmLocation = alarm.location;
            AlarmTime = Converters.TimerConvert.ConvertTimeStampToDateTime(long.Parse(alarm.altime)).ToString("yyyy-MM-dd HH:mm:ss");
            string info = alarm.peculiarnote;
            //  GetInfoByType(alarmType);
            switch (signalType)
            {
                case "CAR-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "CAR-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "DOOR-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "DOOR-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "DOOR-3":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;

                case "FIRE-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "FIRE-4":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;

                case "MAGNETIC-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";//门磁
                    break;

                case "FNCE-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;

                case "FENCE-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "FENCE-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;
                case "FENCE-5":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    break;

                case "ELECTRICITY-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    InitElecInfo(info);
                    break;
                case "ELECTRICITY-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    InitElecInfo(info);
                    break;

                case "FACE-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    FaceBlackDate alarmFaceBlack = (FaceBlackDate)GetAlarmInfo<FaceBlackDate>(info);
                    IntFaceBlackAsync(alarmFaceBlack.data);
                    
                    break;
                case "FACE-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                   
                    IsShowFacesUnauthorized = true;
                    break;

                case "GAS-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmGas alarmGas1 = (AlarmGas)GetAlarmInfo<AlarmGas>(info);
                    InitGas(alarmGas1);
                    break;
                case "GAS-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmGas alarmGas2 = (AlarmGas)GetAlarmInfo<AlarmGas>(info);
                    InitGas(alarmGas2);
                    break;

                case "WATER-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmWater alarmWater1 = (AlarmWater)GetAlarmInfo<AlarmWater>(info);
                    break;
                case "WATER-2":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmWater alarmWater2 = (AlarmWater)GetAlarmInfo<AlarmWater>(info);
                    break;
                case "WATER-3":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmWater alarmWater3 = (AlarmWater)GetAlarmInfo<AlarmWater>(info);
                    break;
                case "WATER-4":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";
                    AlarmWater alarmWater4 = (AlarmWater)GetAlarmInfo<AlarmWater>(info);
                    break;

                case "REDSCAN-1":
                    AlarmImageUrl = $"/ManagementProject;component/ImageSource/Icon/AlarmIcon/AlarmSignal/{signalType}.png";//红外入侵
                    break;              

                default:
                    break;
            }
        }

        private async void IntFaceBlackAsync(FaceBlackList faceBlack)
        {
            try
            {
                IsShowFacesUnauthorized = true;
                Gender = GenderStr(faceBlack.FaceGender);
                FaceName = faceBlack.FaceName;
                FaceSamevalue = (faceBlack.FaceSamevalue/100.00).ToString();
                SnapPic = await HttpAPi.LoadImage(faceBlack.SnapfacePicurl.Replace(" ", ""));
                FacePic = await HttpAPi.LoadImage(faceBlack.FacePicurl.Replace(" ", ""));
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
            }

        }

        private string GenderStr(int id)
        {
            if (id==0)
            {
                return "女";
            }else if (id == 1)
            {
                return "男";
            }else
            {
                return "未知";
            }
        }

        private void InitGas(AlarmGas gasinfo)
        {
            IsShowGasAbnormal = true;
            if (int.Parse (gasinfo.warn )!=1)
            {
                NormalValue = "异常";
            }
            Concentration = gasinfo.value+ " "+gasinfo.unit;
            GasType = gasinfo.type;
            UpLimit = gasinfo.upLim;
            DownLimit = gasinfo.downLim;
        }
        private object GetAlarmInfo<T>(string info)
        {
            if (info == null) return null;
            var ret = JsonConvert.DeserializeObject<T>(info);
            return ret;
        }

        private void InitElecInfo(string info)
        {
            IsShowElectricInfo = true;
            ElectricInfos = new ObservableCollection<ElectricInfo>();
            AlarmElectric electricInfo = (AlarmElectric)GetAlarmInfo<AlarmElectric>(info);
            ElectricValue[] electricValues = electricInfo.currentValues;
            for (int i=0;i< electricValues.Length;i++)
            {
                ElectricInfo electric = new ElectricInfo();
                if (electricValues[i].s!=0)
                {

                }
                electric.State = EleState(electricValues[i].s);
                electric.Unit  = electricValues[i].unit.ToString();
                electric.UpLimit = electricValues[i].cp.ToString();
                electric.Value  = electricValues[i].c.ToString();
                electric .Name = Convert.ToChar('A' + i).ToString();
                ElectricInfos.Add(electric);
            }
          

        }

        public string EleState(int state)
        {
            switch (state)
            {
                case 0:
                    return "正常";
                case 1:
                    return "异常";
                case 2:
                    return "报警";
                case 4:
                    return "超载";
                case 5:
                    return "漏电";
                default:
                    return "异常";
            }
        }


    }

    public class ElectricInfo
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string UpLimit { get; set; }
        public string State { get; set; }
        public string Unit { get; set; }
    }
    public class AlarmDialogModel : INotifyPropertyChangedClass
    {
        private ObservableCollection<ElectricInfo> _electricInfos;
        public ObservableCollection<ElectricInfo> ElectricInfos
        {
            get
            {
                return _electricInfos;
            }
            set
            {
                _electricInfos = value;
                NotifyPropertyChanged("ElectricInfos");
            }
        }
        #region 公共属性

        public string AlarmImageUrl { get; set; }

        private string alarmTypeRemark;

        public string AlarmTypeRemark
        {
            get { return alarmTypeRemark; }
            set
            {
                alarmTypeRemark = value;
                NotifyPropertyChanged("AlarmTypeRemark");
            }
        }

        private string alarmfake;

        public string AlarmFake
        {
            get { return alarmfake; }
            set
            {
                alarmfake = value;
                NotifyPropertyChanged("AlarmFake");
            }
        }


        private string deviceName;

        public string DeviceName
        {
            get { return deviceName; }
            set
            {
                deviceName = value;
                NotifyPropertyChanged("DeviceName");
            }
        }

        private string alarmTime;

        public string AlarmTime
        {
            get { return alarmTime; }
            set
            {
                alarmTime = value;
                NotifyPropertyChanged("AlarmTime");
            }
        }

        private string _alarmLocation;
        public string AlarmLocation
        {
            get
            {
                return _alarmLocation;
            }
            set
            {
                _alarmLocation = value;
                NotifyPropertyChanged("AlarmLocation");
            }
        }

        #endregion

        private string facesUnauthorizedUrl;

        public string FacesUnauthorizedUrl
        {
            get { return facesUnauthorizedUrl; }
            set
            {
                facesUnauthorizedUrl = value;
                NotifyPropertyChanged("FacesUnauthorizedUrl");
            }
        }

        private bool isShowGasAbnormal = false;

        public bool IsShowGasAbnormal
        {
            get { return isShowGasAbnormal; }
            set
            {
                isShowGasAbnormal = value;
                NotifyPropertyChanged("IsShowGasAbnormal");
            }
        }

        private bool isShowFacesUnauthorized = false;

        public bool IsShowFacesUnauthorized
        {
            get { return isShowFacesUnauthorized; }
            set
            {
                isShowFacesUnauthorized = value;
                NotifyPropertyChanged("IsShowFacesUnauthorized");
            }
        }

        private bool isShowBlacklistOfFaces=false;

        public bool IsShowBlacklistOfFaces
        {
            get { return isShowBlacklistOfFaces; }
            set
            {
                isShowBlacklistOfFaces = value;
                NotifyPropertyChanged("IsShowBlacklistOfFaces");
            }
        }

        private bool _isShowElectricInfo = false;
        public bool IsShowElectricInfo
        {
            get { return _isShowElectricInfo; }
            set
            {
                _isShowElectricInfo = value;
                NotifyPropertyChanged("IsShowElectricInfo");
            }
        }


        private string _gasType;
        public string GasType
        {
            get
            {
                return _gasType;
            }
            set
            {
                _gasType = value;
                NotifyPropertyChanged("GasType");
            }
        }


        private double similarity;
        /// <summary>
        /// 相似度
        /// </summary>
        public double Similarity
        {
            get { return similarity; }
            set
            {
                similarity = value;
                NotifyPropertyChanged("Similarity");
            }
        }

        private string normalValue="正常";
        /// <summary>
        /// 气体正常值
        /// </summary>
        public string NormalValue
        {
            get { return normalValue; }
            set
            {
                normalValue = value;
                NotifyPropertyChanged("NormalValue");
            }
        }

        private string concentration;

        /// <summary>
        /// 气体浓度值
        /// </summary>
        public string Concentration
        {
            get { return concentration; }
            set
            {
                concentration = value;
                NotifyPropertyChanged("Concentration");
            }
        }

        private string _upLimit;

        /// <summary>
        /// 上限值
        /// </summary>
        public string UpLimit
        {
            get { return _upLimit; }
            set
            {
                _upLimit = value;
                NotifyPropertyChanged("UpLimit");
            }
        }

        private string _downLimit;

        /// <summary>
        /// 下限值
        /// </summary>
        public string DownLimit
        {
            get { return _downLimit; }
            set
            {
                _downLimit = value;
                NotifyPropertyChanged("DownLimit");
            }
        }


        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                NotifyPropertyChanged("Gender");
            }
        }

        private string _faceName;
        public string FaceName
        {
            get
            {
                return _faceName;
            }
            set
            {
                _faceName = value;
                NotifyPropertyChanged("FaceName");
            }
        }

        private string _faceSamevalue;
        public string FaceSamevalue
        {
            get
            {
                return _faceSamevalue;
            }
            set
            {
                _faceSamevalue = value;
                NotifyPropertyChanged("FaceSamevalue");
            }
        }

        private BitmapImage _facePic;
        public BitmapImage FacePic
        {
            get
            {
                return _facePic;

            }
            set
            {
                _facePic = value;
                NotifyPropertyChanged("FacePic");
            }
        }
        private BitmapImage _snapPic;
        public BitmapImage SnapPic
        {
            get
            {
                return _snapPic;

            }
            set
            {
                _snapPic = value;
                NotifyPropertyChanged("SnapPic");
            }
        }
        
    }
}
