using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ManagementProject.Model
{
    public class WaterPreMonitorModel : INotifyPropertyChangedClass
    {

        private string[] labels;
        /// <summary>
        /// X轴数据源
        /// </summary>
        public string[] Labels 
        {
            get { return labels; }
            set
            {
                labels = value;
                NotifyPropertyChanged("Labels");
            }
        }

        private ChartValues<double> values;

        /// <summary>
        /// Y轴数据源
        /// </summary>
        public ChartValues<double> Values
        {
            get { return values; }
            set
            {
                values = value;
                NotifyPropertyChanged("Values");
            }
        }

        private string title;

        /// <summary>
        /// 折线图标题信息
        /// </summary>
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }


        private string xTitle;

        /// <summary>
        /// 折线图X轴名称
        /// </summary>
        public string XTitle
        {
            get { return xTitle; }
            set
            {
                xTitle = value;
                NotifyPropertyChanged("XTitle");
            }
        }

        private string yTitle;

        /// <summary>
        /// 折线图Y轴名称
        /// </summary>
        public string YTitle
        {
            get { return yTitle; }
            set
            {
                yTitle = value;
                NotifyPropertyChanged("YTitle");
            }
        }


        private ObservableCollection<WaterDeviceInfo> waterPress;

        /// <summary>
        /// 水压信息
        /// </summary>
        public ObservableCollection<WaterDeviceInfo> WaterPress
        {
            get { return waterPress; }
            set
            {
                waterPress = value;
                NotifyPropertyChanged("WaterPress");
            }
        }


        public class WaterDeviceInfo:INotifyPropertyChangedClass
        {
            public string DeviceType { get; set; }
            public string DeviceCode { get; set; }
            public string ImageUrl { get; set; }
            public string DeviceName { get; set; }
            public string DeviceState { get; set; }

            public string StatusText { get; set; }


            private Brush stateBackground;

            public Brush StateBackground
            {
                get { return stateBackground; }
                set
                {
                    stateBackground = value;
                    NotifyPropertyChanged("StateBackground");
                }
            }

            private Brush stateForeground;

            public Brush StateForeground
            {
                get { return stateForeground; }
                set
                {
                    stateForeground = value;
                    NotifyPropertyChanged("StateForeground");
                }
            } 
        }
    }
}
