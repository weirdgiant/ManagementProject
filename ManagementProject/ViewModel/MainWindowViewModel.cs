using ManagementProject.Model;
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
        public DelegateCommand ShowCommand { get; set; }
        public MainWindowViewModel()
        {
            ShowCommand = new DelegateCommand();
            ShowCommand.ExecuteCommand = new Action<object>(LoadedCommand);
        }

        private void LoadedCommand(object obj)
        {
            LoadTitle();
            TimerStart();
            LoadPage();
        }

        private void LoadPage()
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
    }
   
}
