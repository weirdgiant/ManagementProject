using LiveCharts;
using LiveCharts.Wpf;
using MangoApi;
using System;
using System.Collections.Generic;
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
    /// BuildingDistribution.xaml 的交互逻辑
    /// </summary>
    public partial class BuildingDistribution : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public BuildingDistribution()
        {
            InitializeComponent();
        }

        public void LoadSource(ErrorDeviceType[] list)
        {
            DataContext = null;
            List<string> strList = new List<string>();
            ChartValues<int> deviceCount = new ChartValues<int>();
            ChartValues<int> deviceErrorCount = new ChartValues<int>();
            for (int i = 0; i < list.Length; i++)
            {
                deviceCount.Add(list[i].deviceCount);
                deviceErrorCount.Add(list[i].deviceErrorCount);
                strList.Add(list[i].mapName);
            }

            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title ="正常",
                    Values = deviceCount,
                    StackMode = StackMode.Values, 
                    MaxColumnWidth=20,
                },
                new StackedColumnSeries
                {
                     Title ="异常",
                     Values = deviceErrorCount,
                     StackMode = StackMode.Values,
                     DataLabels = true,
                     MaxColumnWidth=20,
                }
            };

            Labels = strList.ToArray();
            Formatter = value => value.ToString();
            //Formatter = value => value + " 个";
            DataContext = this;
        }
       
    }
}
