using MangoApi;
using System;
using System.Linq;
using System.Windows;

namespace ManagementProject.UserControls.MainPageControls
{
    /// <summary>
    /// SchoolBuildingMsg.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolBuildingMsg : Window
    {
        public SchoolBuildingMsg()
        {
            InitializeComponent();
        }

        public async void LoadedItem(int id)
        {
            DeviceCount[] counts = HttpAPi.GetAllDeviceCount(id);
            if (counts != null)
            {
                foreach (var item in counts)
                {
                    SchoolBuildingMesItem mesItem = new SchoolBuildingMesItem();
                    mesItem.count.Text = item.deviceCount;
                    mesItem.type.Text = item.deviceType;
                    mesItem.image.Source = await HttpAPi.LoadImage(AppConfig.ImageBaseUri + item.deviceIcon);
                    devicePanel.Children.Add(mesItem);
                }
            }
        }
    }

    class SchoolBuildMsgModel : INotifyPropertyChangedClass
    {
        private string _waterHouseNumber;
        /// <summary>
        /// 泵房数量
        /// </summary>
        public string WaterHouseNumber
        {
            get
            {
                return _waterHouseNumber;
            }
            set
            {
                _waterHouseNumber = value;
                NotifyPropertyChanged("WaterHouseNumber");
            }
        }
        private string _tenementHead;
        /// <summary>
        /// 物业负责人
        /// </summary>
        public string TenementgHead
        {
            get
            {
                return _tenementHead;
            }
            set
            {
                _tenementHead = value;
                NotifyPropertyChanged("TenementgHead");
            }
        }

        private string _tenementPhone;
        /// <summary>
        /// 物业联系电话
        /// </summary>
        public string TenementPhone
        {
            get
            {
                return _tenementPhone;
            }
            set
            {
                _tenementPhone = value;
                NotifyPropertyChanged("TenementPhone");
            }
        }
        private string _buildingHead;
        /// <summary>
        /// 建筑负责人
        /// </summary>
        public string BuildingHead
        {
            get
            {
                return _buildingHead;
            }
            set
            {
                _buildingHead = value;
                NotifyPropertyChanged("BuildingHead");
            }
        }

        private string _buildingPhone;
        /// <summary>
        /// 建筑联系电话
        /// </summary>
        public string BuildingPhone
        {
            get
            {
                return _buildingPhone;
            }
            set
            {
                _buildingPhone = value;
                NotifyPropertyChanged("BuildingPhone");
            }
        }

        private string _remarks;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
                NotifyPropertyChanged("Remarks");
            }
        }
        private string _buildingName;
        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildingName
        {
            get
            {
                return _buildingName;
            }
            set
            {
                _buildingName = value;
                NotifyPropertyChanged("BuildingName");
            }
        }
    }

    class SchoolBuildMsgViewModel: SchoolBuildMsgModel
    {
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MoveWinCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public SchoolBuildMsgViewModel()
        {
            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MoveWinCommand = new DelegateCommand();
            MoveWinCommand.ExecuteCommand = new Action<object>(MoveWin);
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            //InitMes();
        }

        private void Loaded(object obj)
        {
            SchoolBuildingMsg buildingMsg = (SchoolBuildingMsg)obj;

        }

        public void InitMes(int buildingid)
        {
            MangoMap[] map = GlobalVariable.MapList;
            MangoMap[] results = map.Where(x => x.id == buildingid).ToArray();
            MangoMap[] children = map.Where(x => x.pid == buildingid).ToArray();
            if (results.Length ==0)
            {
                Logger.Error(typeof(SchoolBuildingMsg),"获取楼宇失败！"+"楼宇Id："+ buildingid);
                return;
            }
            BuildingName = results[0].name;
            Remarks= results[0].description;
            BuildingPhone= results[0].buildingPhone ;
            BuildingHead = results[0].buildingPerson ;
            TenementPhone = results[0].tenementPhone ;
            TenementgHead = results[0].tenementPerson;
            WaterHouseNumber = results[0].waterHouse;
        }

        private void CloseWin(object obj)
        {
            SchoolBuildingMsg scm = (SchoolBuildingMsg)obj;
            scm.Close();
        }
        private void MoveWin(object obj)
        {
            SchoolBuildingMsg scm = (SchoolBuildingMsg)obj;
            scm.DragMove();
        }
    }
}
