using ManagementProject.Model;
using ManagementProject.UserControls;
using MangoApi;
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
        private MainWindow _mainWin = (MainWindow)Application.Current.MainWindow;
        public MainMenu mainMenu;
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
            ///初始化菜单ViewModel
            mainWindowMenuViewModel = new MainWindowMenuViewModel(this);
            ///初始化报警ViewModel
            alarmPageViewModel = new AlarmPageViewModel(this);
            ///初始化电子地图ViewModel
            mainPageViewModel = new MainPageViewModel(this);
            ///初始化拼控ViewModel
            collagePageViewModel = new CollagePageViewModel(this);
        }

        public MainWindowViewModel()
        {
            LoadClientConfig();
            InitViewModel();
            InitCommand();
            LoadApi();
           
        }


        private void LoadClientConfig()
        {
            int clientid = 31;
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllClientConfig;
            ClientConfig[] clientConfigs = HttpAPi.GetAllClientInfo(url);
            ClientConfig[] clientConfig = clientConfigs.Where(x => x.id == clientid).ToArray();
            GlobalVariable .CurrenClientConfig = clientConfig[0];
        }

        private  void LoadApi()
        {
            string url = AppConfig.ServerBaseUri+AppConfig .GetMap;
            MangoMap[] map = HttpAPi.GetMapList(url);
            MangoMap[] results = map.Where(x => x.pid == 0).ToArray();
        }
        
        #region LoadedCommand
        private void LoadedCommand(object obj)
        {
            LoadTitle();
            LoadUserName();
            RefreshDateTimeAsync();
            LoadMainPage();
            MainMenuLoaded();
        }
        #region 菜单控件窗口初始化以及位置控制
        /// <summary>
        /// 初始化菜单控件窗口
        /// </summary>
        private void MainMenuLoaded()
        {
            mainMenu = new MainMenu
            {
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 1820,
                Top = 980
            };
            mainMenu.Show();
        }
        /// <summary>
        /// 设置主界面菜单控件窗口位置
        /// </summary>
        public void SetMainMenuMargin()
        {
            mainMenu.Left = 1820;
        }
        /// <summary>
        /// 设置追踪界面菜单控件窗口位置
        /// </summary>
        public void SetTrackerMainMenuMargin()
        {
            mainMenu.Left = 1520;
        }
        #endregion
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
            TitleName= AppConfig.TitleText;
        }
        /// <summary>
        /// 加载用户名
        /// </summary>
        private void LoadUserName()
        {
            if (App.mango.getClientInfo()!= null)
            {
                UserName = App.mango.getClientInfo().username;
            }
           
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
            Environment.Exit(0);
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
