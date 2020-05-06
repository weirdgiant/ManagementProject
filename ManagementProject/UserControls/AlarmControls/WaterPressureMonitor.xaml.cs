using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ManagementProject.Model;
using MangoApi;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// WaterPressureMonitor.xaml 的交互逻辑
    /// </summary>
    public partial class WaterPressureMonitor : UserControl
    {
        public WaterPressureMonitor()
        {
            InitializeComponent();
        }
    }

    public class WaterPreMonitorViewModel : WaterPreMonitorModel
    {
        public Func<double, string> YFormatter { get; set; }
        public Brush DangerBrush { get; set; }//异常画刷
        public CartesianMapper<ObservableValue> Mapper { get; set; }


        public DelegateCommand SelectionChangedCommand { get; set; }
        public DelegateCommand RadioButtonUncheckedCommand { get; set; }

        public WaterPreMonitorViewModel()
        {
            Title = "实时水压变化曲线图";

            SelectionChangedCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(SelectionChanged) };
            RadioButtonUncheckedCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(RadioButtonUnchecked) };
            WaterPress = new ObservableCollection<WaterDeviceInfo>();
            Values = new ChartValues<double>();
        }       

        public void LoadWaterPressure(int mapid)
        {
            WaterPress.Clear();
            Element[] ret = HttpAPi.GetWaterElements(mapid);
            if (ret.Length == 0) return;
            foreach (var item in ret)
            {
                WaterDeviceInfo info = new WaterDeviceInfo
                {
                    DeviceType = item.deviceTypeCode,
                    DeviceCode = item.code,
                    DeviceName = item.name,
                    DeviceState= item.deviceStatus,
                    StatusText="正常",
                    StateBackground= new SolidColorBrush(Color.FromRgb(0, 159, 255)),
                    StateForeground= Brushes.White,
                    ImageUrl = "/ManagementProject;component/ImageSource/Icon/AlarmIcon/38室外消防栓.png" 
                };
                WaterPress.Add(info);
            }
        }

        public void LoadLineChart(string code,string altime)
        {
            DangerBrush = new SolidColorBrush(Color.FromRgb(238, 83, 80));

            string url = AppConfig.ServerBaseUri + AppConfig.GetWaterValue;
            WaterValue[] waterValue = HttpAPi.GetWaterValue(code, altime);
          
            Mapper = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill(item => item.Value > 0.3 ? DangerBrush : null)
                .Stroke(item => item.Value > 0.3 ? DangerBrush : null);

            if (waterValue==null|| waterValue.Length==0)
                return;

            foreach (var item in waterValue)
            {
                if (Values.Count >30)
                {
                    for (int i=0;i<10;i++)
                    {
                        Values.RemoveAt(i);
                    }
                }
                double value = double.Parse(item.value);
                Values.Add(value);
            }

            YFormatter = y => Math.Round(y, 2, MidpointRounding.AwayFromZero).ToString();
        }

        public string Code { get; set; }
        public bool IsStartUp { get; set; } = false;
        public async void RefreshAsync()
        {
            IsStartUp = true;
            while (IsStartUp)
            {
                LoadLineChart(Code, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                await Task.Delay(10000);
            }
        }

        private void SelectionChanged(object obj)
        {
            if (obj != null)
            {
                WaterDeviceInfo waterPressInfo = (WaterDeviceInfo)obj;
                waterPressInfo.StateForeground = new SolidColorBrush(Color.FromRgb(0, 159, 255));
                waterPressInfo.StateBackground = Brushes.White ;

                Values.Clear();
                Code = waterPressInfo.DeviceCode;
               // LoadLineChart(waterPressInfo.DeviceCode, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }
        }

        private void RadioButtonUnchecked(object obj)
        {
            if (obj != null)
            {
                WaterDeviceInfo waterPressInfo = (WaterDeviceInfo)obj;
                waterPressInfo.StateForeground = Brushes.White;
                waterPressInfo.StateBackground = new SolidColorBrush(Color.FromRgb(0, 159, 255));
            }
        }
    }
}
