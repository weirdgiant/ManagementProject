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
        public AlarmButtonViewModel carAlarmViewModel { get; set; }
        public MainPageViewModel( MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            ControlViewModelInit();
        }

        private void ControlViewModelInit()
        {
            CameraStatisticsViewModel = new MainWindowStatisticsViewModel();
            WaterStatisticsViewModel = new MainWindowStatisticsViewModel();
            carAlarmViewModel = new AlarmButtonViewModel(MainWindowViewModel);
            carAlarmViewModel.AlarmType = AlarmType.CarAlarm;

            CameraStatisticsViewModel.Icon = "/ManagementProject;component/ImageSource/Icon/mainwindowicon/摄像机.png";
            CameraStatisticsViewModel.Number = "8";
            WaterStatisticsViewModel.Icon = "/ManagementProject;component/ImageSource/Icon/mainwindowicon/水压设备.png";
            WaterStatisticsViewModel.Number = "5";
        }
        
    }
}
