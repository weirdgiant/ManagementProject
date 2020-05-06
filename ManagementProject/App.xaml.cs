using com.mango.protocol;
using Lierda.WPFHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static bool UseNvr=true;
        public static MangoApp mango;
        public App()
        {
            Startup += App_Startup;
            Exit += App_Exit;
        }



        //LierdaCracker cracker = new LierdaCracker();
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    cracker.Cracker(1);//垃圾回收间隔时间
        //    base.OnStartup(e);
        //}

        private void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                ErrorCode.Initialize();
                Logger.Initialize();
                Logger.Info(GetType(), "应用程序启动");
                mango = MangoApp.getInstance();
                mango.start();

                if (UseNvr)
                {
                    var ret = NVRSDKManager.Instance.Initialize();
                    if (!ret)
                    {
                        Logger.Error("NVR 初始化失败！");
                        UseNvr = false;
                        return;

                    }
                    NVRSDKManager.Instance.SetReconnectDev();

                    ret = NVRSDKManager.Instance.Login(AppConfig .NvrIP, ushort.Parse(AppConfig.NvrPort), AppConfig.NvrUser, AppConfig.NvrPassword);
                    if (!ret)
                    {
                        Logger.Error("NVR 初始化失败！");
                        UseNvr = false;
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            mango.exit();
            var sdkManager = SdkManager.GetInstance();
            if (sdkManager.userLoginStatus)
            {
                UnvOnePlayer.ClosePlayer();
                sdkManager.LogOut();
                sdkManager.CleanUp();
            }

            if (UseNvr)
            {
                NVRSDKManager.Instance.Logout();
                NVRSDKManager.Instance.CleanUp();
            }

            Logger.Info(GetType(), "应用程序退出");
            Environment.Exit(0);
        }


    }

   


}
