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
        public CollagePageViewModel collagePageViewModel { get; set; }
        public AlarmPageViewModel alarmPageViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }

        public DelegateCommand ShowCommand { get; set; }
        public DelegateCommand ShutDownCommand { get; set; }
        public DelegateCommand MinSizeCommand { get; set; }

        private void InitCommand()
        {
            ShowCommand = new DelegateCommand();
            ShowCommand.ExecuteCommand = new Action<object>(LoadedCommand);
            ShutDownCommand = new DelegateCommand ();
            ShutDownCommand.ExecuteCommand = new Action<object>(ShutDown);
            MinSizeCommand = new DelegateCommand();
            MinSizeCommand.ExecuteCommand = new Action<object>(MinSize);
        }

        private void InitViewModel()
        {
            ///初始化菜单VM
            mainWindowMenuViewModel = new MainWindowMenuViewModel(this);
            ///初始化报警VM
            alarmPageViewModel = new AlarmPageViewModel(this);
            ///初始化电子地图VM
            mainPageViewModel = new MainPageViewModel(this);
            ///初始化拼控VM
            collagePageViewModel = new CollagePageViewModel(this);
        }

        public MainWindowViewModel()
        {
            InitViewModel();
            InitCommand();
        }
        
        #region LoadedCommand
        private void LoadedCommand(object obj)
        {
            LoadTitle();
            RefreshDateTimeAsync();
            LoadMainPage();
        }

        /// <summary>
        /// 加载电子地图
        /// </summary>
        public void LoadMainPage()
        {
            PageUrl = "/ManagementProject;component/PageView/MainPage.xaml";
            mainWindowMenuViewModel.ShowMenu();
        }

        /// <summary>
        /// 加载标题栏名称
        /// </summary>
        private void LoadTitle()
        {
            string titletext = ConfigurationManager.AppSettings["TitleText"];
            TitleName= titletext;            
        }
        /// <summary>
        /// 异步刷新当前时间
        /// </summary>
        private async void RefreshDateTimeAsync()
        {
            while (true)
            {              
                CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
                await Task.Delay(1000);
            }
        }
        #endregion
        /// <summary>
        /// 标题栏用户按键
        /// </summary>
        /// <param name="obj"></param>
        private void UserInfo(object obj)
        {

        }
        /// <summary>
        /// 拼控按钮事件
        /// </summary>
        /// <param name="obj"></param>
        private void CollageControl(object obj)
        {

        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="obj"></param>
        private void ShutDown(object obj)
        {
            MainWindow main = (MainWindow)obj;
            Application.Current.Shutdown();
            main.Close();
        }
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="obj"></param>
        private void MinSize(object obj)
        {
            MainWindow main=(MainWindow)obj;
            main.WindowState = WindowState.Minimized;
        }
    }

}
