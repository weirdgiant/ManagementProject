using ManagementProject.Model;
using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ManagementProject.ViewModel
{
    public class MainWindowViewModel:MainWindowModel 
    {
        public MainWindowMenuViewModel mainWindowMenuViewModel { get; set; }
        public AlarmPageViewModel alarmPageViewModel { get; set; }
        public DelegateCommand ShowCommand { get; set; }
        public DelegateCommand ShutDownCommand { get; set; }
        public DelegateCommand MinSizeCommand { get; set; }

        private void CommandInit()
        {
            ShowCommand = new DelegateCommand();
            ShowCommand.ExecuteCommand = new Action<object>(LoadedCommand);
            ShutDownCommand = new DelegateCommand ();
            ShutDownCommand.ExecuteCommand = new Action<object>(ShutDown);
            MinSizeCommand = new DelegateCommand();
            MinSizeCommand.ExecuteCommand = new Action<object>(MinSize);
        }

        public MainWindowViewModel()
        {
            mainWindowMenuViewModel = new MainWindowMenuViewModel(this);
            alarmPageViewModel = new AlarmPageViewModel(this);
            CommandInit();
        }
        
        #region LoadedCommand
        private void LoadedCommand(object obj)
        {
            LoadTitle();
            TimerStart();
            LoadMainPage();
        }

        public void LoadMainPage()
        {
            PageUrl = "/ManagementProject;component/PageView/MainPage.xaml";
        }

        private void LoadTitle()
        {
            string titletext = ConfigurationManager.AppSettings["TitleText"];
            TitleName= titletext;            
        }

        public void TimerStart()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            NowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        private void ShutDown(object obj)
        {
            MainWindow main = (MainWindow)obj;
            Application.Current.Shutdown();
            main.Close();
        }
        private void MinSize(object obj)
        {
            MainWindow main=(MainWindow)obj;
            main. WindowState = WindowState.Minimized;
        }
    }

}
