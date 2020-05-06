using LiveCharts;
using LiveCharts.Defaults;
using ManagementProject.UserControls.AlarmControls;
using System.Windows.Media;

namespace ManagementProject.Model
{
    /// <summary>
    /// 水压信息
    /// </summary>
    public class WaterPressInfo : INotifyPropertyChangedClass
    {
        private string[] xData;

        /// <summary>
        /// X轴数据源
        /// </summary>
        public string[] XData
        {
            get { return xData; }
            set
            {
                xData = value;
                NotifyPropertyChanged("XData");
            }
        }

        private ChartValues<ObservableValue> yData;

        /// <summary>
        /// Y轴数据源
        /// </summary>
        public ChartValues<ObservableValue> YData
        {
            get { return yData; }
            set
            {
                yData = value;
                //CheckStatus(YData);
                NotifyPropertyChanged("YData");
            }
        }


        /// <summary>
        /// 图片URL
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 根据设备状态显示对应的颜色
        /// </summary>
        //public Brush DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? Brushes.Green : Brushes.Red;
        public Brush Background => AbnormalInfo == AbnormalInfo.Normal ? new SolidColorBrush(Color.FromRgb(33, 149, 242)) : new SolidColorBrush(Color.FromRgb(238, 83, 80));

        /// <summary>
        /// 设备状态
        /// </summary>
        public string DeviceStatus => AbnormalInfo == AbnormalInfo.Normal ? "正常" : "异常";

        private void GetInfoByType(WaterPressureType type)
        {
            switch (type)
            {
                case WaterPressureType.Spray:
                    DeviceType = "喷淋水压";
                    ImageUrl = @"/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/喷淋泵.png";
                    break;
                case WaterPressureType.PumpRoom:
                    DeviceType = "泵房水压";
                    ImageUrl = @"/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/泵房水压.png";
                    break;
                case WaterPressureType.Fire:
                    DeviceType = "消防水压";
                    ImageUrl = @"/ManagementProject;component/ImageSource/Icon/AlarmIcon/38室外消防栓.png";
                    break;
                case WaterPressureType.LiquidLevel:
                    DeviceType = "液位";
                    ImageUrl = @"/ManagementProject;component/ImageSource/Icon/AlarmIcon/液位.png";
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


        //private string imageName;

        //public string ImageName
        //{
        //    get { return imageName; }
        //    set
        //    {
        //        imageName = value;
        //        NotifyPropertyChanged("ImageName");
        //    }
        //}

        //private void GetImgByType()
        //{
        //    switch (Type)
        //    {
        //        case WaterPressureType.喷淋水压:
        //            ImageName = "/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/喷淋泵.png";
        //            break;
        //        case WaterPressureType.泵房水压:
        //            ImageName = "/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/泵房水压.png";
        //            break;
        //        case WaterPressureType.消防水压:
        //            ImageName = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/38室外消防栓.png";
        //            break;
        //        case WaterPressureType.液位:
        //            ImageName = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/液位.png";
        //            break;
        //        default:
        //            break;
        //    }
        //}

        ////private void CheckStatus(ChartValues<ObservableValue> values)
        ////{
        ////    //检测异常信息

        ////    foreach (var item in values)
        ////    {
        ////        if (item.Value>0.8)
        ////        {
        ////            Status = AbnormalInfo.异常;
        ////        }
        ////        else
        ////        {
        ////            Status = AbnormalInfo.正常;
        ////        }
        ////    }
        ////}

        //private AbnormalInfo status;

        //private WaterPressureType type;

        ///// <summary>
        ///// 水压类型
        ///// </summary>
        //public WaterPressureType Type
        //{
        //    get { return type; }
        //    set
        //    {
        //        type = value;
        //        NotifyPropertyChanged("Type");
        //        GetImgByType();
        //    }
        //}

        //private SolidColorBrush background;

        //public SolidColorBrush Background
        //{
        //    get { return background; }
        //    set
        //    {
        //        background = value;
        //        NotifyPropertyChanged("Background");
        //    }
        //}

        ///// <summary>
        ///// 异常信息
        ///// </summary>
        //public AbnormalInfo Status
        //{
        //    get { return status; }
        //    set
        //    {
        //        status = value;
        //        NotifyPropertyChanged("Status");
        //        if (Status == AbnormalInfo.正常)
        //            Background = new SolidColorBrush(Color.FromRgb(33, 149, 242));//Colors.LightSkyBlue
        //        else
        //            Background = new SolidColorBrush(Color.FromRgb(238, 83, 80));
        //    }
        //}
    }
}
