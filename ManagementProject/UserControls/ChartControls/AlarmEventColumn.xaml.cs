using LiveCharts;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ManagementProject.UserControls.ChartControls
{
    /// <summary>
    /// AlarmEventColumn.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmEventColumn : UserControl
    {

        public List<string> Labels { get; set; }
        public ChartValues<int> Values { get; set; }

        public AlarmEventColumn()
        {
            InitializeComponent();
        }

        public void LoadSource(ErrorDeviceType[] list)
        {
            try
            {
                Values = new ChartValues<int>();
                Labels = new List<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    Values.Add(list[i].count);
                    //Labels.Add(list[i].month.Substring(list[i].month.IndexOf("-") + 1));//2019-01
                    Labels.Add(DateTime.Parse(list[i].month).Month.ToString());//2019-01
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            //YFormatter = value => value.ToString() + " 个";

            DataContext = this;
        }

    }
}
