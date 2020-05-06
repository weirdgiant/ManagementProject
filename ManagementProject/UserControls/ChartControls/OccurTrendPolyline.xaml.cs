using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementProject.UserControls.ChartControls
{
    /// <summary>
    /// OccurTrendPolyline.xaml 的交互逻辑
    /// </summary>
    public partial class OccurTrendPolyline : UserControl
    {
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<int> Values { get; set; }
        public Brush DangerBrush { get; set; }
        //public CartesianMapper<ObservableValue> Mapper { get; set; }

        public OccurTrendPolyline()
        {
           InitializeComponent();      
        }

        public void LoadSource(ErrorDeviceType[] list)
        {
            DataContext = null;
            Values = new ChartValues<int>();
            Labels = new List<string>();
            for (int i = 0; i <list.Length ; i++)
            {
                Values.Add(list[i].errorCount);
                Labels.Add(list[i].month);
            }

            YFormatter = value => value.ToString();

            DataContext = this;
        }
    }
}
