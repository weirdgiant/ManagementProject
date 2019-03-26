using System.Windows.Media;

namespace ManagementProject.Model
{
    /// <summary>
    /// 水压信息
    /// </summary>
    public class WaterPressInfo : INotifyPropertyChangedClass
    {
        private WaterPressureType type;

        public WaterPressureType Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
                GetImgByType();
            }
        }

        private SolidColorBrush background;

        public SolidColorBrush Background
        {
            get { return background; }
            set
            {
                background = value;
                NotifyPropertyChanged("Background");
            }
        }

        private string imageName;

        public string ImageName
        {
            get { return imageName; }
            set
            {
                imageName = value;
                NotifyPropertyChanged("ImageName");
            }
        }

        private void GetImgByType()
        {
            switch (Type)
            {
                case WaterPressureType.喷淋水压:
                    ImageName = "/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/喷淋泵.png";
                    break;
                case WaterPressureType.泵房水压:
                    ImageName = "/ManagementProject;component/ImageSource/Icon/MainWinControlIcon/泵房水压.png";
                    break;
                case WaterPressureType.消防水压:
                    break;
                case WaterPressureType.液位:
                    break;
                default:
                    break;
            }
        }

        private AbnormalInfo status;

        /// <summary>
        /// 异常信息
        /// </summary>
        public AbnormalInfo Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
                if (Status == AbnormalInfo.正常)
                    Background = new SolidColorBrush(Colors.LightSkyBlue);
                else
                    Background = new SolidColorBrush(Colors.Red);
            }
        }
    }

    public enum AbnormalInfo
    {
        /// <summary>
        /// 正常
        /// </summary>
        正常,
        //Normal,

        /// <summary>
        /// 异常
        /// </summary>
        异常,
        //Abnormal,
    }

    /// <summary>
    /// 水压类型
    /// </summary>
    public enum WaterPressureType
    {
        /// <summary>
        /// 喷淋水压
        /// </summary>
        //Spray,
        喷淋水压,

        /// <summary>
        /// 泵房水压
        /// </summary>
        //PumpRoom,
        泵房水压,

        /// <summary>
        /// 消防水压
        /// </summary>
        //Fire,
        消防水压,

        /// <summary>
        /// 液位
        /// </summary>
        //LiquidLevel,
        液位,
    }
}
