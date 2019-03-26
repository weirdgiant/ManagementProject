using ManagementProject.ViewModel;
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
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// AlarmButtonControl.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmButtonControl : Window
    {
        public AlarmButtonControl()
        {
            InitializeComponent();
            Loaded += AlarmButtonControl_Loaded;
        }

        private void AlarmButtonControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitAlarmButton();
        }
        private void InitAlarmButton()
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)main.DataContext;
            string url = AppConfig.ServerBaseUri + AppConfig.GetAlarmCount;
            Alarm[] UnDealedAlarm = HttpAPi.GetAlarm(url);
            foreach (var item in UnDealedAlarm)
            {
                AlarmButton alarmButton = new AlarmButton();
                AlarmButtonViewModel alarmButtonViewModel = new AlarmButtonViewModel(mainWindowViewModel);
                alarmButtonViewModel.AlarmCount= item.alarmCount.ToString();
                switch (item.flagVal)
                {
                    case "门禁":
                        alarmButtonViewModel.AlarmType = AlarmType.CarAlarm;
                        break;
                    case "火灾":
                        alarmButtonViewModel.AlarmType = AlarmType.FireAlarm;
                        break;
                    case "水压":
                        alarmButtonViewModel.AlarmType = AlarmType.WaterAlarm;                      
                        break;                   
                }
                alarmButton.DataContext = alarmButtonViewModel;
                Grid grid = new Grid
                {
                    Width = 15
                };
                alarmControl.Children.Add(alarmButton);
                alarmControl.Children.Add(grid);
            }
           
        }


        private List<AlarmTypes> Alarmlist()
        {
            List<AlarmTypes> list = new List<AlarmTypes>();
            AlarmTypes aa = new AlarmTypes();
            aa.type = AlarmType.CarAlarm;
            list.Add(aa);
            AlarmTypes bb = new AlarmTypes();
            bb.type = AlarmType.FireAlarm;
            list.Add(bb);
            return list;
        }

        public class AlarmTypes
        {
            public AlarmType type { get; set; }
        }
    }
}
