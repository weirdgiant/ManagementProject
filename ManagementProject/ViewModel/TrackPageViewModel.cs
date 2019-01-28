using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class TrackPageViewModel : TrackPageModel
    {
        public TrackManagementViewModel TrackManagementViewModel { get; set; }
        public DelegateCommand OpenTrackerCommand { get; set; }
        public TrackPageViewModel()
        {
            TrackManagementViewModel = new TrackManagementViewModel();
            OpenTrackerCommand = new DelegateCommand();
            OpenTrackerCommand.ExecuteCommand = new Action<object>(OpenTracker);
        }
        /// <summary>
        /// 打开活动管理
        /// </summary>
        /// <param name="obj"></param>
        private void OpenTracker(object obj)
        {
            TrackManagement trWin = new TrackManagement();
            trWin.DataContext = TrackManagementViewModel;
            trWin.Left = 23;
            trWin.Top = 165;
            trWin.ShowDialog();
        }
    }
}
