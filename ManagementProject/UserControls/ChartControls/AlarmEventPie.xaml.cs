using LiveCharts;
using LiveCharts.Wpf;
using MangoApi;
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

namespace ManagementProject.UserControls.ChartControls
{
    /// <summary>
    /// AlarmEventPie.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmEventPie : UserControl
    {
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public AlarmEventPie()
        {
            InitializeComponent();
            
        }
        public void LoadSource(ErrorDeviceType[] list)
        {
            SeriesCollection = new SeriesCollection();
            for (int i=0;i< list.Length; i++)
            {
                string name = list[i].alarmName;
                PieSeries pie = new PieSeries();
                pie.DataLabels = true;
                pie.Title = name;
                //pie.LabelPoint = chartPoint =>string.Format("{0} ({1:P})", name, chartPoint.Participation);
                pie.LabelPoint = chartPoint =>string.Format("{0:P}",chartPoint.Participation);
                pie.Values = new ChartValues<int> { list[i].count };
                SeriesCollection.Add(pie);
            }
           
            DataContext = this;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }

    public class TextSource
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
