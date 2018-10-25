using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ManagementProject.ViewModel
{
    public class MainWindowViewModel
    {
        public DelegateCommand ShowCommand { get; set; }
        public MainWindowModel  MainWindowModel { get; set; }

        public MainWindowViewModel()
        {
            MainWindowModel = new MainWindowModel();
            ShowCommand = new DelegateCommand();
            ShowCommand.ExecuteCommand = new Action<object>(LoadedCommand);
        }

        private void LoadedCommand(object obj)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        public void TimerStart()
        {

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            MainWindowModel.NowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        }
    }
   
}
