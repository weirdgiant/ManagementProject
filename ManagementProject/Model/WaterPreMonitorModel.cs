using System.Collections.ObjectModel;

namespace ManagementProject.Model
{
    public class WaterPreMonitorModel : INotifyPropertyChangedClass
    {

        private string legendName;

        /// <summary>
        /// 折线图名称
        /// </summary>
        public string LegendName
        {
            get { return legendName; }
            set
            {
                legendName = value;
                NotifyPropertyChanged("LegendName");
            }
        }


        private ObservableCollection<WaterPressInfo> waterPress;

        /// <summary>
        /// 水压信息
        /// </summary>
        public ObservableCollection<WaterPressInfo> WaterPress
        {
            get { return waterPress; }
            set
            {
                waterPress = value;
                NotifyPropertyChanged("WaterPress");
            }
        }
    }
}
