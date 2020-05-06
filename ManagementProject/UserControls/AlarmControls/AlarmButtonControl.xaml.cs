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
        public async void InitAlarmButton()
        {
            try
            {
                alarmControl.Children.Clear();
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)main.DataContext;
                string url = AppConfig.ServerBaseUri + AppConfig.GetAlarmCount;
                string clientId = App.mango.getClientInfo().userId.ToString ();// string.Join(",", GlobalVariable.AlarmMapList);
                Alarm[] UnDealedAlarm = HttpAPi.GetUndealAlarm(url, clientId);
                foreach (var item in UnDealedAlarm)
                {
                    AlarmButton alarmButton = new AlarmButton();
                    AlarmButtonViewModel alarmButtonViewModel = new AlarmButtonViewModel(mainWindowViewModel);
                    alarmButtonViewModel.AlarmCount = item.alarmCount.ToString();
                    alarmButtonViewModel.Type = item.flag;
                    AlarmTypeInfo alarmTypeInfo = MangoInfo.instance.AlarmTypeInfos.Where(x => x.listValue.Trim() == item.flag.Trim()).ToArray()[0];
                    alarmButtonViewModel.AlarmIcon = await HttpAPi.LoadImage(AppConfig.ImageBaseUri + alarmTypeInfo.imgUrl);
                    alarmButton.DataContext = alarmButtonViewModel;
                    Grid grid = new Grid
                    {
                        Width = 15
                    };
                    alarmControl.Children.Add(alarmButton);
                    alarmControl.Children.Add(grid);
                    GlobalVariable.AlarmDic.Clear();
                    GetAlarmInfo(item.flag);
                }
            }catch (Exception ex)
            {
                Logger.Error(typeof(AlarmButtonControl),ex.Message);
            }

        }

        public void GetAlarmInfo(string type)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAlarmInfo;
            Alarm[] alarm = HttpAPi.GetAlarmInfo(url, type);
            foreach (var item in alarm)
            {
                GlobalVariable.AlarmDic.Add(item .id,item);
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
