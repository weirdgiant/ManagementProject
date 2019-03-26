using LiveCharts;
using LiveCharts.Wpf;
using ManagementProject.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
            DataContext = new WaterPreMonitorViewModel();
        }
    }

    class WaterPreMonitorViewModel : WaterPreMonitorModel
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public DelegateCommand MouseDownCommand { get; set; }
        public DelegateCommand ShowDetailCommand { get; set; }

        public WaterPreMonitorViewModel()
        {
            MouseDownCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(MouseDown) };
            ShowDetailCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(Down) };

            LoadWaterPressure();
        }

        private void Down(object obj)
        {

        }

        private void MouseDown(object obj)
        {
            //MessageBox.Show(obj.ToString());
            if (obj != null)
            {
                WaterPressInfo waterPressInfo = (WaterPressInfo)obj;

                LegendName = waterPressInfo.Type.ToString();

                var status = waterPressInfo.Status;

                var cv =new ChartValues<double>();

                Random random = new Random();

                for (int i = 0; i < 10; i++)
                {
                    var values = random.NextDouble();
                    cv.Add(values);
                }

                var ls = new LineSeries() { Values=cv,Title= status.ToString() };

                //if (status==AbnormalInfo.正常)
                //{
                //    ls.PointForeground = Brushes.LightSkyBlue;
                //}
                //else
                //{
                //    ls.PointForeground = Brushes.Red;
                //}

                SeriesCollection.Clear();

                SeriesCollection.Add(ls);
            }
        }

        private void LoadWaterPressure()
        {
            #region 折线图
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "正常",
                    Values = new ChartValues<double> { 0, 0.2, 0.4, 0.6, 1.0, 0.8 }
                },
                // new LineSeries
                //{
                //    Title = "异常",
                //    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 },
                //},
            };

            Labels = new[] { "1min", "2min", "3min", "4min", "5min", "6min", "7min", "8min", "9min", "10min" };

            YFormatter = value => value.ToString();
            #endregion

            WaterPress = new ObservableCollection<WaterPressInfo>
            {
                new WaterPressInfo
                {
                    Type=WaterPressureType.喷淋水压,
                    Status=AbnormalInfo.正常
                },
                 new WaterPressInfo
                {
                    Status=AbnormalInfo.异常,
                    Type=WaterPressureType.泵房水压
                },
                 new WaterPressInfo
                {
                    Status=AbnormalInfo.异常,
                    Type=WaterPressureType.喷淋水压
                },
                 new WaterPressInfo
                {
                    Status=AbnormalInfo.正常,
                    Type=WaterPressureType.喷淋水压
                }
                 ,
                 new WaterPressInfo
                {
                    Status=AbnormalInfo.异常,
                    Type=WaterPressureType.泵房水压
                }
            };
        }
    }
}
