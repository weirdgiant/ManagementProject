using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using ManagementProject.PageView;
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
        public TrackTabControl _trackTabControl;
        public MapControl mapControl;
        private MainWindow _mainWin = (MainWindow)Application.Current.MainWindow;
        public TrackManagementViewModel TrackManagementViewModel { get; set; }
        public DelegateCommand OpenTrackerCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public TrackPageViewModel()
        {
            TrackManagementViewModel = new TrackManagementViewModel(this);
            OpenTrackerCommand = new DelegateCommand();
            OpenTrackerCommand.ExecuteCommand = new Action<object>(OpenTracker);
            LoadCommand = new DelegateCommand();
            LoadCommand.ExecuteCommand = new Action<object>(Load);
            UnLoadCommand = new DelegateCommand();
            UnLoadCommand.ExecuteCommand = new Action<object>(UnLoad);
        }

       
        private void Load(object obj)
        {
            TrackPage trackPage =(TrackPage)obj;
            mapControl = trackPage.map;
            GlobalVariable.IsTrackPage = true;

            LoadTrackInfoControl();
            LoadTrackBt();
            LoadTrackTabControl();
            //Sid = GlobalVariable.CurrentSid;
        }

        private void LoadTrackInfoControl()
        {
            _trackInfo = new TrackInfo
            {
                DataContext = TrackManagementViewModel,
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 1597,//1920-323
                Top = 50
            };
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

        private void LoadTrackTabControl()
        {
            _trackTabControl = new TrackTabControl();
            _trackTabControl.DataContext = TrackManagementViewModel;
            _trackTabControl._trackPageViewModel = this;
            _trackTabControl.Owner = _mainWin;
            _trackTabControl.Topmost = true;
            _trackTabControl.WindowStartupLocation = WindowStartupLocation.Manual;
            _trackTabControl.Left = 20;
            _trackTabControl.Top = 70;
            _trackTabControl.Show();

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
            _trackTabControl.Close();
        }

        /// <summary>
        /// 打开活动管理
        /// </summary>
        /// <param name="obj"></param>
        private void OpenTracker(object obj)
        {
            try
            {
                TrackManagement trWin = new TrackManagement();
                trWin.Owner = _trackTabControl;
                //trWin.Topmost = true;
                trWin.DataContext = TrackManagementViewModel;
                trWin.Left = 23;
                trWin.Top = 165;
                trWin.ShowDialog();
            }catch(Exception ex)
            {
                Logger.Error(typeof (TrackPageViewModel), "OpenTracker:" + ex.Message );
            }
        }
    }
}
