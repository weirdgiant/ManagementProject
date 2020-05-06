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

        public DeviceControl deviceControl;
        public ErrorDeviceControl errorDeviceControl;

        public AlarmButtonControl alarmControl;
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public MainWindowTextBoxViewModel mainWindowTextBoxViewModel { get; set; }
        public CameraInfoViewModel CameraInfoViewModel { get; set; }
        public ClientInfoViewModel ClientInfoViewModel { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public MainPageViewModel( MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            ControlViewModelInit();
            CommandInit();
          
        }

        public void TrackStart()
        {
            if (deviceControl!=null)
            {
                deviceControl.Visibility = Visibility.Collapsed;
            }
            if (errorDeviceControl != null)
            {
                errorDeviceControl.Visibility = Visibility.Collapsed;
            }
        }
        public void TrackeEnd()
        {
            try
            {
                if (deviceControl != null)
                {
                    deviceControl.Visibility = Visibility.Visible;
                }
                if (errorDeviceControl != null)
                {
                    errorDeviceControl.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
            
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
            mainWindowTextBoxViewModel = new MainWindowTextBoxViewModel();
    }    
    }
}
