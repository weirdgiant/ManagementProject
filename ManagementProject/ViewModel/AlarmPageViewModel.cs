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
        public AlarmPageViewModel(MainWindowViewModel _mainWindowViewModel )
        {
            mainWindowViewModel = _mainWindowViewModel;
            MainPageReturnCommand = new DelegateCommand();
            MainPageReturnCommand.ExecuteCommand = new Action<object>(MainPageReturn);
        }

        private void InitControl()
        {
            alarmCarInfoViewModel = new AlarmCarInfoViewModel();
            disposalPlanViewModel = new DisposalPlanViewModel();
            waterMessageViewModel = new WaterMessageViewModel();
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
