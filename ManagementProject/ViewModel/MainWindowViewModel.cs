using ManagementProject.FunctionalWindows;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ManagementProject.ViewModel
{
    public class MainWindowViewModel:MainWindowModel 
    {
        public bool IsStartUp { get; set; } = false;
        public List<Window > OpenWinList { get; set; }
        public PlayerWindow PlayerWindow { get; set; }
        public EventHistory EventHistoryWin { get; set; }
        public TrafficWin TrafficWinow { get; set; }
        public List<AuxiliaryWindow> SubWindows { get; set; }
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
            OpenWinList = new List<Window>();
            SubWindows = new List<AuxiliaryWindow>();
            LoadClientConfig();
            LoadModule();
            LoadModuleConfig();
            LoadSignalList();
            InitViewModel();
            InitCommand();
            LoadApi();
            SetAlignment();
        }


        /// popup会识别左撇子、右撇子
        private  void SetAlignment()
        {
            //获取系统是以Left-handed（true）还是Right-handed（false）   
            var ifLeft = SystemParameters.MenuDropAlignment;
            if (ifLeft)
            {
                // change to false               
                var t = typeof(SystemParameters);
                var field = t.GetField("_menuDropAlignment", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
                field.SetValue(null, false);
            }
        }


        private void LoadClientConfig()
        {
            int clientid = App.mango.getClientInfo().userId;//31;//
            List<ClientConfig> clientConfigs = MangoInfo.instance.ClientConfigs;
            if (clientConfigs == null) return;
            ClientConfig[] clientConfig = clientConfigs.Where(x => x.id == clientid).ToArray();
            if (clientConfig.Length ==0)
            {
                return ;
            }
            GlobalVariable .CurrenClientConfig = clientConfig[0];
            GlobalVariable.SidList = clientConfig[0].viewRange.Replace(" ", "").Split(',');
            GlobalVariable .AlarmMapList = clientConfig[0].jurisdiction.Replace(" ", "").Split(',');
            GlobalVariable.CurrentMapId = clientConfig[0].campus;
            GlobalVariable.CurrentSid= clientConfig[0].campus;
            GlobalVariable.CurrentMapKind = MapKind.School;
            GlobalVariable.CurrentClientProperty = clientConfig[0].clientProperty;

            //InitPinkongConfig(clientConfig[0]);

        }

        private void InitPinkongConfig(ClientConfig clientConfig)
        {
            try
            {
                var xy = clientConfig.coordinate1.Split(',');
                var wh = clientConfig.coordinate2.Split(',');
            }
            catch { }
        }

        private void LoadModule()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllModule;
            AlarmModule[] ret = HttpAPi.GetAlarmModule(url);
            GlobalVariable.AlarmModules = ret;
        }
        private void LoadModuleConfig()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllModuleConfig;
            AlarmModuleConfig[] ret = HttpAPi.GetAlarmModuleConfig(url);
            GlobalVariable.AlarmModuleConfigs = ret;

        }

        private void LoadSignalList()
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllSignal;
            Signal[] signalList = HttpAPi.GetAllSignal(url);
            GlobalVariable.Signals = signalList;
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
            LoadTitleIcon();
            LoadTitle();
            LoadUserName();
            RefreshDateTimeAsync();
            LoadChartPage();
            //LoadMainPage();
            MainMenuLoaded();
            //InitWin();
            //InitSubWin();
            //InitAllSubWin();
        }
       
        public void InitAllSubWin()
        {
            InitWin();
            //InitSubWin();
        }

        private void InitWin()
        {
            int num = GlobalVariable.CurrenClientConfig.displaycount;
            if(num<2)
            {
                return;
            }

            if (SubWindows.Count != 0)
            {
                foreach (var item in SubWindows)
                {
                    item.Close();
                }
                SubWindows.Clear();
            }
            System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            int screensCount = screens.Length;
            //主屏幕显示
            System.Windows.Forms.Screen mainScreen = screens.FirstOrDefault(x => x.Primary == true);
            //其他屏幕显示,这里假设有2个
            var subScreen = (from o in screens where o.Primary == false select o).ToList();
            if (subScreen.Count > 0)
            {
                foreach (var item in subScreen)
                {
                    var subscreen1 = item;
                    AuxiliaryWindow win = new AuxiliaryWindow();
                    win.WindowStartupLocation = WindowStartupLocation.Manual;
                    System.Drawing.Rectangle mswa2 = subscreen1.WorkingArea;
                    win.Left = subscreen1.WorkingArea.Left;
                    win.Top = subscreen1.WorkingArea.Top;
                    win.Width = subscreen1.Bounds.Width;
                    win.Height = subscreen1.Bounds.Height;
                    SubWindows.Add(win);
                    win.WindowState = WindowState.Maximized;
                    win.ResizeMode = ResizeMode.NoResize;
                    win.WindowStyle = WindowStyle.None;
                    win.WindowState = WindowState.Normal;
                    win.Owner = _mainWin;
                    win.Show();
                }
            }
        }

        public void InitSubWin()
        {
            try
            {
                if (SubWindows.Count == 0)
                {
                    return;
                }
                foreach (var item in SubWindows)
                {
                    item.Close();

                }

            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
            finally
            {
                SubWindows.Clear();
            }
        }


        /// <summary>
        /// 关闭所有已打开功能窗口
        /// </summary>
        public void CloseFunWin()
        {
            if (EventHistoryWin !=null)
            {
                EventHistoryWin.Close();
            }
            if (PlayerWindow != null)
            {
                PlayerWindow.Close();
            }
            if (TrafficWinow!=null)
            {
                TrafficWinow.Close();
            }
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

        public void LoadChartPage()
        {
            PageUrl = "/ManagementProject;component/PageView/ChartPage.xaml";
            mainWindowMenuViewModel.HiddenMenu();
        }
        /// <summary>
        /// 加载标题栏名称
        /// </summary>
        private void LoadTitle()
        {
            SystemParameter[] ret = MangoInfo.instance.SystemParameterList.Where(x => x.parameter_code == "SYS000001").ToArray();
            if (ret != null)
            {
                TitleName = ret[0].parameter_value.Trim();
            }
            else
            {
                TitleName = AppConfig.TitleText;
            }
          
        }

        private void LoadTitleIcon()
        {
            TitleIcon = "/ManagementProject;component/ImageSource/Icon/mainwindowicon/logo.png";
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
            IsStartUp = true;
            while (IsStartUp)
            {              
                CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");
                //OnGarbageCollection();
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// 强制回收内存
        /// </summary>
        void OnGarbageCollection()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
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
            IsStartUp = false;
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
            mainWindowMenuViewModel.IsOpened = false;
        }

        public void AlarmPageInit(string type)
        {
            try
            {
                GlobalVariable.AlarmPageType = type.Trim();
                alarmPageViewModel.PageType = type.Trim();
                PageUrl = "/ManagementProject;component/PageView/AlarmPage.xaml";
                mainWindowMenuViewModel.HiddenMenu();
            }
            catch (Exception ex)
            {

                Logger.Error("AlarmPageInit:" + ex.Message);
            }
           
        }
    }

}
