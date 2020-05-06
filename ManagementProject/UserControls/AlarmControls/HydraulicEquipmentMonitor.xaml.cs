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
    /// HydraulicEquipmentMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class HydraulicEquipmentMonitor : UserControl
    {
        public HydraulicEquipmentMonitor()
        {
            InitializeComponent();
            DataContext = new HydraulicMonitorViewModel();
        }
    }

    class HydraulicMonitorViewModel: HydraulicMonitorModel
    {
        public HydraulicMonitorViewModel()
        {
            DeviceItems = new ObservableCollection<HydraulicMonitorModel>()
            {
                new HydraulicMonitorModel()
                {
                    IsShowPumpRoomName=true,
                    PumpRoomName="泵房一",
                    AbnormalInfo=AbnormalInfo.Abnormal,
                    WaterPressureType=WaterPressureType.LiquidLevel
                },

                new HydraulicMonitorModel()
                {
                    AbnormalInfo=AbnormalInfo.Normal,
                    WaterPressureType=WaterPressureType.LiquidLevel
                },
                new HydraulicMonitorModel()
                {
                    AbnormalInfo=AbnormalInfo.Normal,
                    WaterPressureType=WaterPressureType.Spray
                },
                new HydraulicMonitorModel()
                {
                    AbnormalInfo=AbnormalInfo.Normal,
                    WaterPressureType=WaterPressureType.PumpRoom
                },
            };
        }
    }
    class HydraulicMonitorModel : INotifyPropertyChangedClass
    {      
        /// <summary>
        /// 泵房名称
        /// </summary>
        public string PumpRoomName { get; set; }

        /// <summary>
        /// 是否显示泵房名称
        /// </summary>
        public bool IsShowPumpRoomName { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        //public Brush DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? Brushes.Green : Brushes.Red;
        public Brush DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF353535")) : Brushes.Red;

        private void GetInfoByType(WaterPressureType type)
        {
            switch (type)
            {
                case WaterPressureType.Spray:
                    DeviceType = "喷淋水压";
                    ImageUrl = @"Assest\Spray.png";
                    break;
                case WaterPressureType.PumpRoom:
                    DeviceType = "泵房水压";
                    ImageUrl = @"Assest\PumpRoom.png";
                    break;
                case WaterPressureType.Fire:
                    DeviceType = "消防水压";
                    ImageUrl = @"Assest\Fire.png";
                    break;
                case WaterPressureType.LiquidLevel:
                    DeviceType = "液位";
                    ImageUrl = @"Assest\LiquidLevel.png";
                    break;
                default:
                    break;
            }
        }

        private WaterPressureType waterPressureType;

        /// <summary>
        /// 水压类型
        /// </summary>
        public WaterPressureType WaterPressureType
        {
            get { return waterPressureType; }
            set
            {
                waterPressureType = value;
                NotifyPropertyChanged("WaterPressureType");
                GetInfoByType(WaterPressureType);
            }
        }

        private AbnormalInfo abnormalInfo;

        /// <summary>
        /// 异常信息
        /// </summary>
        public AbnormalInfo AbnormalInfo
        {
            get { return abnormalInfo; }
            set
            {
                abnormalInfo = value;
                NotifyPropertyChanged("AbnormalInfo");
            }
        }

        private ObservableCollection<HydraulicMonitorModel> devices;

        public ObservableCollection<HydraulicMonitorModel> DeviceItems
        {
            get { return devices; }
            set
            {
                devices = value;
                NotifyPropertyChanged("DeviceItems");
            }
        }
    }
}
