using ManagementProject.Model;
using ManagementProject.UserControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class MainPageViewModel : MainPageModel
    {
        private MainWindow _mainWin = (MainWindow)Application.Current.MainWindow;
        private AlarmButtonControl alarmControl;
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public MainWindowStatisticsViewModel CameraStatisticsViewModel { get; set; }
        public MainWindowStatisticsViewModel WaterStatisticsViewModel { get; set; }
        public CameraInfoViewModel CameraInfoViewModel { get; set; }
        public ClientInfoViewModel ClientInfoViewModel { get; set; }
        public AlarmButtonViewModel carAlarmViewModel { get; set; }
        public AlarmButtonViewModel waterAlarmViewModel { get; set;}
        public AlarmButtonViewModel fireAlarmViewModel { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public MainPageViewModel( MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            ControlViewModelInit();
            CommandInit();
        }

        private void CommandInit()
        {
            LoadCommand = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(Loaded)
            };
            UnLoadCommand = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(UnLoaded)
            };
        }
        private void Loaded(object obj)
        {
            //AlarmControlInit();
        }
        private void AlarmControlInit()
        {
            alarmControl = new AlarmButtonControl();
            alarmControl.Owner = _mainWin;
            alarmControl.Topmost = true;
            alarmControl.WindowStartupLocation = WindowStartupLocation.Manual;
            alarmControl.Left = 295;
            alarmControl.Top = 75;
            alarmControl.Show();
        }
        private void UnLoaded(object obj)
        {
            //if (alarmControl!=null)
            //{
            //    alarmControl.Close();
            //}           
        }
        private void ControlViewModelInit()
        {
            CameraInfoViewModel = new CameraInfoViewModel();
            ClientInfoViewModel = new ClientInfoViewModel();          
        }

        private void InitAlarmButton()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAlarmCount;
            Alarm[] UnDealedAlarm = HttpAPi.GetAlarm(url);
            ///初始化车辆报警
            carAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel)
            {
                AlarmType = AlarmType.CarAlarm,
                AlarmCount = "5"
            };
            ///初始化水压报警
            waterAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel)
            {
                AlarmType = AlarmType.WaterAlarm,
                AlarmCount = "6"
            };
            ///初始化火灾报警
            fireAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel)
            {
                AlarmType = AlarmType.FireAlarm,
                AlarmCount = "7"
            };
        }
        
    }
}
