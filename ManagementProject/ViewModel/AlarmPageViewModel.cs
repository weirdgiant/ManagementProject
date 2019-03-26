using ManagementProject.Model;
using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class AlarmPageViewModel : AlarmPageModel
    {
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public AlarmCarInfoViewModel alarmCarInfoViewModel { get; set; }
        public DisposalPlanViewModel disposalPlanViewModel { get; set; }
        public WaterMessageViewModel waterMessageViewModel { get; set; }
        public DelegateCommand MainPageReturnCommand { get; set; }
        public DelegateCommand AlarmMainCommand { get; set; }
        public DelegateCommand AlarmTrackerCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public AlarmPageViewModel(MainWindowViewModel _mainWindowViewModel )
        {
            mainWindowViewModel = _mainWindowViewModel;
            InitCommand();
            InitControl();
            LoadAlarmMain();
        }

        private void InitCommand()
        {
            MainPageReturnCommand = new DelegateCommand();
            MainPageReturnCommand.ExecuteCommand = new Action<object>(MainPageReturn);
            AlarmMainCommand = new DelegateCommand();
            AlarmMainCommand.ExecuteCommand = new Action<object>(AlarmMain);
            AlarmTrackerCommand = new DelegateCommand();
            AlarmTrackerCommand.ExecuteCommand = new Action<object>(AlarmTracker);
            LoadCommand = new DelegateCommand();
            LoadCommand.ExecuteCommand = new Action<object>(Load);
            UnLoadCommand = new DelegateCommand();
            UnLoadCommand.ExecuteCommand = new Action<object>(UnLoad);
        }

        private void InitControl()
        {
            alarmCarInfoViewModel = new AlarmCarInfoViewModel();
            disposalPlanViewModel = new DisposalPlanViewModel();
            waterMessageViewModel = new WaterMessageViewModel();
        }

        private void Load(object obj)
        {
            GlobalVariable.IsAlarmPage = true;
        }

        private void UnLoad(object obj)
        {
            GlobalVariable.IsAlarmPage = false;
        }

        /// <summary>
        /// 主场景
        /// </summary>
        /// <param name="obj"></param>
        private void AlarmMain(object obj)
        {
            LoadAlarmMain();
        }
        private void LoadAlarmMain()
        {
            PageUrl = "/ManagementProject;component/UserControls/AlarmControls/AlarmMainPage.xaml";
        }
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="obj"></param>
        private void AlarmTracker(object obj)
        {
            PageUrl = "/ManagementProject;component/UserControls/AlarmControls/AlarmTrackPage.xaml";
        }

        /// <summary>
        /// 返回主页
        /// </summary>
        /// <param name="obj"></param>
        private void MainPageReturn(object obj)
        {
            mainWindowViewModel.LoadMainPage();
        }
    }
}
