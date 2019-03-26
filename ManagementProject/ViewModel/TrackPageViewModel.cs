using ManagementProject.FunctionalWindows;
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
    public class TrackPageViewModel : TrackPageModel
    {
        private TrackInfo _trackInfo;
        private TrackManageButton _trackManageButton;
        private MainWindow _mainWin = (MainWindow)Application.Current.MainWindow;
        public TrackManagementViewModel TrackManagementViewModel { get; set; }
        public DelegateCommand OpenTrackerCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public TrackPageViewModel()
        {
            TrackManagementViewModel = new TrackManagementViewModel();
            OpenTrackerCommand = new DelegateCommand();
            OpenTrackerCommand.ExecuteCommand = new Action<object>(OpenTracker);
            LoadCommand = new DelegateCommand();
            LoadCommand.ExecuteCommand = new Action<object>(Load);
            UnLoadCommand = new DelegateCommand();
            UnLoadCommand.ExecuteCommand = new Action<object>(UnLoad);
        }

       
        private void Load(object obj)
        {
            GlobalVariable.IsTrackPage = true;

            LoadTrackInfoControl();
            LoadTrackBt();
            Sid = GlobalVariable.SelectedSchoolId;
        }

        private void LoadTrackInfoControl()
        {
            _trackInfo = new TrackInfo();
            _trackInfo.Owner = _mainWin;
            _trackInfo.Topmost = true;
            _trackInfo.WindowStartupLocation = WindowStartupLocation.Manual;
            _trackInfo.Left = 1620;
            _trackInfo.Top = 45;
            _trackInfo.Show();
        }

        private void LoadTrackBt()
        {
            _trackManageButton = new TrackManageButton();
            _trackManageButton.Owner = _mainWin;
            _trackManageButton.DataContext = this;
            _trackManageButton.Topmost = true;
            _trackManageButton.WindowStartupLocation = WindowStartupLocation.Manual;
            _trackManageButton.Left = 50;
            _trackManageButton.Top = 985;
            _trackManageButton.Show();

        }

        private void UnLoad(object obj)
        {
            GlobalVariable.IsTrackPage = false;
            UnLoadControl();
        }

        private void UnLoadControl()
        {
            _trackInfo.Close();
            _trackManageButton.Close();
        }

        /// <summary>
        /// 打开活动管理
        /// </summary>
        /// <param name="obj"></param>
        private void OpenTracker(object obj)
        {
            TrackManagement trWin = new TrackManagement();
            trWin.Owner = _mainWin;
            trWin.DataContext = TrackManagementViewModel;
            trWin.Left = 23;
            trWin.Top = 165;
            trWin.ShowDialog();
        }
    }
}
