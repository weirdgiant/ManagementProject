using ManagementProject.Model;
using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class MainPageViewModel : MainPageModel
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public MainWindowStatisticsViewModel CameraStatisticsViewModel { get; set; }
        public MainWindowStatisticsViewModel WaterStatisticsViewModel { get; set; }
        public CameraInfoViewModel CameraInfoViewModel { get; set; }
        public ClientInfoViewModel ClientInfoViewModel { get; set; }
        public AlarmButtonViewModel carAlarmViewModel { get; set; }
        public AlarmButtonViewModel waterAlarmViewModel { get; set;}
        public AlarmButtonViewModel fireAlarmViewModel { get; set; }
        public MainPageViewModel( MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            ControlViewModelInit();
        }

        private void ControlViewModelInit()
        {
            CameraStatisticsViewModel = new MainWindowStatisticsViewModel();
            WaterStatisticsViewModel = new MainWindowStatisticsViewModel();
            CameraInfoViewModel = new CameraInfoViewModel();
            ClientInfoViewModel = new ClientInfoViewModel();
            carAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel);
            waterAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel);
            fireAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel);
            ///初始化车辆报警
            carAlarmViewModel.AlarmType = AlarmType.CarAlarm;
            carAlarmViewModel.AlarmCount = "5";
            ///初始化水压报警
            waterAlarmViewModel.AlarmType = AlarmType.WaterAlarm;
            waterAlarmViewModel.AlarmCount = "6";
            ///初始化火灾报警
            fireAlarmViewModel.AlarmType = AlarmType.FireAlarm;
            fireAlarmViewModel.AlarmCount = "7";

            CameraStatisticsViewModel.Icon = "/ManagementProject;component/ImageSource/Icon/mainwindowicon/摄像机.png";
            CameraStatisticsViewModel.Number = "8";
            WaterStatisticsViewModel.Icon = "/ManagementProject;component/ImageSource/Icon/mainwindowicon/水压设备.png";
            WaterStatisticsViewModel.Number = "5";
        }
        
    }
}
