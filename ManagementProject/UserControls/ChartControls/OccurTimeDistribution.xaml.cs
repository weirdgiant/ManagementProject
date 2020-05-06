using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementProject.UserControls.ChartControls
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class OccurTimeDistribution : UserControl
    {
        #region field

        public List<string> LablesX { get; set; }
        public List<string> LablesY { get; set; }

        public ChartValues<ObservablePoint> ValuesA { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<double, string> XFormatter { get; set; }
        public SeriesCollection SeriesValues { get; set; }
        public ScatterSeries ScatterPoints { get; set; }
        public List<DateTime> dateTimes = new List<DateTime>();

        #endregion

        #region constructor method
        public OccurTimeDistribution()
        {
            InitializeComponent();

            //var text = System.IO.File.ReadAllText(@"D:\ChromeDownloads\response_1568787348432.json");
            //string[] ret = JsonConvert.DeserializeObject<string[]>(text);
            //LoadSource(ret);
        }
        #endregion

        #region some method
        public async void LoadSource(string[] list)
        {
            ClearValue();
            #region Test 结束后删除

            if (list.Length == 0 || list.Length > 1000)// 
            {


                return;
            }

            #endregion


            ValuesA = new ChartValues<ObservablePoint>();

            var minDay = DateTime.MaxValue;
            //var minMonth = DateTime.MaxValue;

            foreach (var item in list)
            {
                DateTime dateTime = DateTime.Parse(item);
                if (dateTime < minDay)
                    minDay = dateTime;
                dateTimes.Add(dateTime);
            }

            XFormatter = x => ConvertXToDate((int)x, minDay);
            YFormatter = y => ConvertYToTime((int)y);

            //GeneracRandomTime(minDay);//TODO 在测试完成后删除 插入模拟数据

            foreach (var dateTime in dateTimes)
            {
                ValuesA.Add(ConvertToPoint(dateTime, minDay));
            }

            //InitLables();

            //UI线程运行这一段
            await Task.Run(() =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    ScatterPoints = new ScatterSeries
                    {
                        Values = ValuesA,
                        MaxPointShapeDiameter = 3,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009fff")),//#FF009DFD
                        Title = "",
                        LabelPoint = point => RevertToDatetimeStr((int)point.X, (int)point.Y, minDay),
                    };
                    SeriesValues = new SeriesCollection { ScatterPoints };
                }));

            });

            DataContext = this;
        }

        private void InitLables()
        {
            LablesX = new List<string>();
            LablesY = new List<string>();

            for (int i = 0; i <= 24; i++)
            {
                if (i % 2 == 0)
                    LablesY.Add($"{i}:00");
            }

            var nowTime = DateTime.Now;
            for (int i = 12; i >= 1; i--)
            {
                var time = nowTime.AddMonths(-i);
                LablesX.Add(time.ToString("yyyy-MM"));
            }
        }

        private void ClearValue()
        {
            DataContext = null;
            dateTimes.Clear();
            if (SeriesValues != null&&SeriesValues.Count>0)
                SeriesValues.Clear();
        }

        private string ConvertXToDate(int x, DateTime minDay)
        {
            TimeSpan timeSpan = new TimeSpan(x - 1, 0, 0, 0);
            DateTime xDateTime = minDay + timeSpan;
            return xDateTime.ToString("yyyy-MM-dd");
        }

        private string ConvertYToTime(int y)
        {
            var timeSpan = TimeSpan.FromSeconds(y);
            return timeSpan.ToString();
        }

        private ObservablePoint ConvertToPoint(DateTime dateTime, DateTime minDate)
        {
            var x = (dateTime - minDate).Days + 1;
            var y = dateTime.Hour * 60 * 60 + dateTime.Minute * 60 + dateTime.Second;
            return new ObservablePoint(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">x轴坐标值 以minDay为1，日期差值为x坐标值</param>
        /// <param name="y">y轴坐标值 范围0~60*60*24</param>
        /// <param name="minDay"></param>
        /// <returns></returns>
        private string RevertToDatetimeStr(int x, int y, DateTime minDay)
        {
            return RevertToDatetime(x, y, minDay).ToString();
        }

        private DateTime RevertToDatetime(int x, int y, DateTime minDay)
        {
            try
            {
                TimeSpan delta = new TimeSpan(x - 1, 0, 0, y);
                return minDay + delta - new TimeSpan(minDay.Hour, minDay.Minute, minDay.Second);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(OccurTimeDistribution), ex.Message);
                return DateTime.MinValue;
            }
        }

        #region TODO 在测试完成后删除 插入模拟数据
        /// <summary>
        /// 生成随机时间
        /// </summary>
        /// <param name="minDay"></param>
        private void GeneracRandomTime(DateTime minDay)
        {
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                var x = random.Next(0, 365);
                var y = random.Next(0, 24 * 60 * 60);
                dateTimes.Add(RevertToDatetime(x, y, minDay));
            }
        }
        #endregion 
        #endregion
    }
}
