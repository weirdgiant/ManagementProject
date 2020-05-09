using ManagementProject.UserControls;
using ManagementProject.UserControls.AlarmControls;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ManagementProject.Model
{
    /// <summary>
    /// 监控设备
    /// </summary>
   public class MonitorEquipmentModel : INotifyPropertyChangedClass
    {

        private ObservableCollection<DeviceItemsInfo> devices;

        public ObservableCollection<DeviceItemsInfo> DeviceItems
        {
            get { return devices; }
            set
            {
                devices = value;
                NotifyPropertyChanged("DeviceItems");
            }
        }

        private ObservableCollection<ContentItems> listBoxItems;

        public ObservableCollection<ContentItems> ListBoxItems
        {
            get { return listBoxItems; }
            set
            {
                listBoxItems = value;
                NotifyPropertyChanged("ListBoxItems");
            }
        }
    }


    public class DeviceItemsInfo
    {
        /// <summary>
        /// 左侧垂直线的长度
        /// </summary>
        public double LineLength { get; set; } = 100;

        /// <summary>
        /// 泵房名称或房间名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 是否显示房名称
        /// </summary>
        public bool IsShowRoomName { get; set; }
        public bool IsShowContant { get; set; } = true;

        /// <summary>
        /// 图片URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 设备类型标记
        /// </summary>
        public string DeviceTypeRemark { get; set; }

        /// <summary>
        /// 设备状态
        /// </summary>
        //public Brush DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? Brushes.Green : Brushes.Red;
        public Brush DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF353535")) : Brushes.Red;

        /// <summary>
        /// 根据设备类型得到设备类型标记和图片URL
        /// </summary>
        /// <param name="type">水压类型</param>
        private void GetInfoByType(string type)
        {
           // string text = RemarkExtend.GetRemark(type);
            ImageUrl = $@"/ManagementProject;component/ImageSource/Icon/AlarmIcon/{type}.png";
        }

        private string deviceType;

        /// <summary>
        /// 水压类型
        /// </summary>
        public string DeviceType
        {
            get { return deviceType; }
            set
            {
                deviceType = value;
                GetInfoByType(DeviceType);
            }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public AbnormalInfo AbnormalInfo { get; set; }
    }

    public class ContentItems
    {
        public ObservableCollection<DeviceItemsInfo> DeviceItems { get; set; }
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    [Remark("设备类型")]
    public enum ElementDeviceType
    {
        #region 水压设备类型
        /// <summary>
        /// 水压
        /// </summary>
        [Remark("水压")]
        WaterPressureSensor,
        /// <summary>
        /// 液位
        /// </summary>
        [Remark("液位")]
        LiquidLevelSensor,
        #endregion

        #region 气体设备类型
        /// <summary>
        /// 氢气
        /// </summary>
        [Remark("氢气")]
        H2GasDetector,

        /// <summary>
        /// 氧气
        /// </summary>
        [Remark("氧气")]
        O2GasDetector,

        /// <summary>
        /// 一氧化碳
        /// </summary>
        [Remark("一氧化碳")]
        COGasDetector,

        /// <summary>
        /// 氮气
        /// </summary>
        [Remark("氮气")]
        N3GasDetector,

        /// <summary>
        /// 可燃气体
        /// </summary>
        [Remark("可燃气体")]
        CombustibleGasDetector,
        /// <summary>
        /// 氩气
        /// </summary>
        [Remark("氩气")]
        ArGasDetector,
        #endregion

        #region 电力设备类型
        /// <summary>
        /// 漏电
        /// </summary>
        [Remark("漏电")]
        LeakageMonitoring,

        /// <summary>
        /// 单相电
        /// </summary>
        [Remark("单相电")]
        SinglePhaseMonitoring,
        /// <summary>
        /// 三相电
        /// </summary>
        [Remark("三相电")]
        ThreePhaseMonitoring
        #endregion
    }
}
