using com.mango.protocol;
using com.mango.protocol.Enum;
using com.mango.protocol.msg;
using ManagementProject.Helper;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject
{

    public class SpeechManager
    {

        /// using System.Speech.Synthesis;
        private SpeechSynthesizer ss = new SpeechSynthesizer();


        public void StopAll()
        {
            ss.SpeakAsyncCancelAll();
        }

        public void Add(string content)
        {
            content = filterIP(content);
            ss.Speak(content);
        }

        public static string filterIP(string content)
        {
            Regex reg = new Regex(@"\d{1,3}(\.\d{1,3}){3}");
            return reg.Replace(content, "", 1);
        }

        internal void Init()
        {
            //this.ss.SelectVoice(conf.speechVoice);
            this.ss.Volume = int.Parse(AppConfig.SpeechVol);
            this.ss.Rate = int.Parse(AppConfig.SpeechRate);

        }
    }
    public class MangoApp
    {
        private static MangoApp instance = new MangoApp();

        public static MangoApp getInstance()
        {
            return instance;
        }
        public SpeechManager speechManager;
        private MangoSocketClient client;
        private AppMessageHandler handler;
        private MangoAlarmManager alarmManager;
        private ClientInfo info;
        public bool Running = true;

        public MangoApp()
        {
           // AppLogger.init();
        }

        public ClientInfo getClientInfo()
        {
            return info;
        }


        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <returns></returns>
        public MangoAlarmManager getAlarmManager()
        {
            return alarmManager;
        }

        /// <summary>
        /// 消息监听线程
        /// </summary>
        public void start()
        {
            Thread thread = new Thread(run);
            thread.Start();
            Logger.Info("App start Thread Start:" + thread.ManagedThreadId);
        }


        /// <summary>
        /// 发送结构体获取返回值
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public OutStream Async(object message, short protocol)
        {
            if (!this.client.isConnected())
                return null;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " Async:" + protocol.ToString());
            return handler.Await(client, message, protocol);
        }

        /// <summary>
        /// 运行消息监听线程
        /// </summary>
        public void run()
        {
            speechManager = new SpeechManager();
            speechManager.Init();
            handler = new AppMessageHandler();
            ///接受报警消息
            alarmManager = new MangoAlarmManager();

            client = new MangoSocketClient(AppConfig.ServerIP,int.Parse(AppConfig.ServerPort), handler);
            try
            {
                client.Start();     //启动连接服务器
                Logger.Info(typeof(MangoApp),"连接服务器！");
            }
            catch (Exception ex)
            {
                Logger.Error("连接服务器错误：" + ex.ToString());
            }
            //       HKSDK.Get();
            Logger.Info("App start Thread Stop:" + Thread.CurrentThread.ManagedThreadId);
        }

        public void exit()
        {
            this.Running = false;
            client.exit();
        }

        public bool isConnected()
        {
            return client.isConnected();
        }

        public MangoSocketClient GetClient()
        {
            return client;
        }

        internal void SetClientInfo(int userId, string username, string password, ClientConfig config, string module, string sid, int flag)
        {
            info = new ClientInfo(userId, username, password, config, module, sid, flag);
        }


        internal bool isLogin()
        {
            return info != null;
        }

        internal void Write(object message, short protocol)
        {
            if (!this.client.isConnected())
                return;
            this.client.Write(message, protocol);
        }

        /// <summary>
        /// 登录初始化
        /// </summary>
        internal void Init()
        {

        }

    }

    class AppMessageHandler : MessageHandler
    {
        public override void onConnectFailed(MangoSocketClient client)
        {
            try
            {
               // AppLogger.log.Error("onConnectFailed");
                SetStatus("无法连接至服务器");
            }
            catch (Exception)
            {

            }

        }

        public override void onDisconnected(MangoSocketClient client)
        {
            try
            {
               // AppLogger.log.Error("onDisconnected");
                SetStatus("与服务器通讯中断");
                if (MangoApp.getInstance().Running)
                {
                    MangoApp.getInstance().GetClient().Start();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AppMessageHandler), "onDisconnected:"+ex.Message);

            }

        }

        private void SetStatus(string status)
        {
            try
            {
                //Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                //    if (Application.Current == null || Application.Current.MainWindow == null)
                //        return;
                //    MainWindow main = (MainWindow)Application.Current.MainWindow;
                //    main.SetServerStatus(status);

                //}));
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }

        }

        public override void onReconnected(MangoSocketClient client)
        {

        }

        /// <summary>
        /// 门禁状态
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        public override void onSC_ACCESSCONTROL(object session, SC_ACCESSCONTROL message)
        {
            //AppLogger.log.Info("onSC_HastenHandle:" + message.alarmId + "," + message.userId);
            MangoApp.getInstance().getAlarmManager().OnAccessControl(message);

        }


        /// <summary>
        /// 监听跟催消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        public override void onSC_HastenHandle(object session, SC_HastenHandle message)
        {
            //AppLogger.log.Info("onSC_HastenHandle:" + message.alarmId + "," + message.userId);
            MangoApp.getInstance().getAlarmManager().OnHastenHandle(message.alarmId, message.userId);

        }

        /// <summary>
        /// 监听报警处理信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        public override void onSC_ConfirmAlarm(object session, SC_ConfirmAlarm message)
        {
            //  AppLogger.log.Info("onSC_ConfirmAlarm:" + message.alarmId);

            MangoApp.getInstance().getAlarmManager().OnAlarmConfirmed(message.alarmId.ToString());
        }

        /// <summary>
        /// 监听报警信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        public override void onSC_Alarm(object session, SC_Alarm message)
        {
          //  AppLogger.log.Info("onSC_Alarm:" + message.alarm.id);
            MangoApp.getInstance().getAlarmManager().OnAlarm(message.alarm);
        }

        public override void onConnected(MangoSocketClient client)
        {
           // AppLogger.log.Info("onConnected");
            SetStatus("通讯正常");
            //AppLogger.log.Error("onReconnected");
            ClientInfo info = MangoApp.getInstance().getClientInfo();
            if (info != null)
            {
                CS_LOGIN login = new CS_LOGIN();
                login.username = info.username;
                login.password = info.password;

                OutStream os = MangoApp.getInstance().Async(login, (short)login.protocol);
                if (os != null)
                {
                    SC_LOGIN ret = (SC_LOGIN)Package.SCStruct<SC_LOGIN>(os);

                    //AppLogger.log.Info("Login:" + ret.ret);
                    if (ret.ret != 0)
                    {

                    }
                }
            }
        }

        public override void onConnectStart(MangoSocketClient client)
        {
           // AppLogger.log.Info("onConnectStart");
            SetStatus("正在连接服务器");
        }
    }

    public class MangoAlarmManager : AlarmManager
    {
        /// <summary>
        /// 发生报警
        /// </summary>
        /// <param name="alarm"></param>
        public void OnAlarm(Alarm alarm)
        {
            if (alarm.flag =="CAR")
            {
                if (alarm.type ==2)
                {
                    return;
                }
            }
            //会出现卡顿，需要优化
            this.Add(alarm);
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                InitAlarmPage(alarm);

            }));

            //上墙
            //if (GlobalVariable.IsPushAlarmWindow && GlobalVariable.IsAlarmPage && GlobalVariable.CurrentAlarmLevel > alarm.level)
            //{
            //    PinkongHelper.ClientUpWall();
            //}

            ///上墙
            //if (AppConfig.instance.TVWallEnable == 1 && GlobalVariable.IsAlarmPage && GlobalVariable.CurrentAlarmLevel > alarm.level)
            //{
            //    CS_PushClient push = new CS_PushClient();
            //    push.type = PushWindowType.WINDOW_1x1;
            //    push.screenId = AppConfig.instance.screenId;
            //    MangoApp.getInstance().Write(push, (short)push.protocol);
            //}


            // ...报警发生时需要语音提示，语音格式为：报警类型，报警设备所在地图名称+报警设备名称，等级。
            //例：火灾报警，地点，第一食堂第一层，一楼充值处，等级，X级。
            Thread thread = new Thread(new ParameterizedThreadStart(OnAlarmSpeech));
            thread.Start(alarm);
            Logger.Info("OnAlarmSpeech Thread Start:" + thread.ManagedThreadId);
        }

        private void InitAlarmPage(Alarm alarm )
        {
            try
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.WindowState = WindowState.Maximized;
                MainWindowViewModel mainview = (MainWindowViewModel)main.DataContext;
                mainview.CloseFunWin();
                main.WindowState = WindowState.Maximized;
                if (GlobalVariable.IsAlarmPage && GlobalVariable.CurrentAlarmLevel > alarm.level)
                {
                    mainview.AlarmPageInit(alarm.flag);
                    mainview.alarmPageViewModel.ReloadId = alarm.id;
                    if (GlobalVariable.IsPushAlarmWindow)
                    {
                        PinkongHelper.ClientUpWall();
                    }
                    //mainview.alarmPageViewModel.LoadTabControl();
                }
                else if(GlobalVariable.IsAlarmPage && GlobalVariable.CurrentAlarmLevel <= alarm.level)
                {
                    mainview.alarmPageViewModel.LoadTabControl();
                }
                else 
                {
                    
                    mainview.AlarmPageInit(alarm.flag);
                    mainview.alarmPageViewModel.SelectedId = alarm.id;
                    if (GlobalVariable.IsPushAlarmWindow)
                    {
                        PinkongHelper.ClientUpWall();
                    }
                    //mainview.alarmPageViewModel.SelectedId = alarm.id;
                    // mainview.alarmPageViewModel.LoadTabControl();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MangoAlarmManager), ex.Message);
            }
        }

        /// <summary>
        /// 启动语音播报
        /// </summary>
        /// <param name="obj"></param>
        private void OnAlarmSpeech(object obj)
        {
            try
            {
                Alarm alarm = obj as Alarm;
                string signalId = alarm.flag + "-" + alarm.type;
                MangoApi.Signal signal = GlobalVariable.Signals.Where(x => x.signal_id == signalId).ToArray()[0];
                if (signal != null)
                {
                    string format = "{0} 地点 {1} {2} 等级{3}级";
                    string position = alarm.location != null ? alarm.location : "";
                    try
                    {
                        string ret = string.Format(format, signal.signal_name, position, alarm.sersorname, alarm.level);
                        MangoApp.getInstance().speechManager.Add(ret);
                        MangoApp.getInstance().speechManager.Add(ret);
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e.Message);
                    }
                }
                Logger.Info("OnAlarmSpeech Thread Stop:" + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 移除已处理报警
        /// </summary>
        /// <param name="alarmId"></param>
        public void OnAlarmConfirmed(string alarmId)
        {
            try
            {
                int[] id = Array.ConvertAll(alarmId.Split(','), int.Parse);
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    AlarmConfirmed(id);

                }));

            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }


            //foreach (var item in alarmId)
            //{
            //    if (GlobalVariable.AlarmDic.Keys.Contains(item))
            //    {
            //        MangoApi.Alarm alarm = GlobalVariable.AlarmDic[item];
            //        string Text = alarm.location + "发生" + alarm.flagVal + "报警" + "，该报警已经处理完毕！\n";
            //        MessageBox.Show(Text);
            //    }
            //}

            //if (mainview.mainPageViewModel.alarmControl != null)
            //{
            //    mainview.mainPageViewModel.alarmControl.InitAlarmButton();
            //}

        }

        private void AlarmConfirmed(int[] alarmId)
        {
            try
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                MainWindowViewModel mainview = (MainWindowViewModel)main.DataContext;
                if (GlobalVariable.IsAlarmPage)
                {
                    AlarmPageViewModel alarmPageViewModel = mainview.alarmPageViewModel;
                    int count = alarmPageViewModel.AlarmList.Where(x => alarmId.Contains(x.id)).ToArray().Length;
                    if (count != 0)
                    {
                        alarmPageViewModel.SelectedId = 0;
                        alarmPageViewModel.GetAlarmInfo(alarmPageViewModel.PageType);
                        alarmPageViewModel._tabControlWin.ReLoaded();
                    }

                }
                else
                {
                    if (mainview.mainPageViewModel.alarmControl != null)
                    {
                        mainview.mainPageViewModel.alarmControl.InitAlarmButton();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void OnAccessControl(SC_ACCESSCONTROL mes)
        {


            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                onAccessControl(mes);

            }));


           

        }

        private void onAccessControl(SC_ACCESSCONTROL mes)
        {
            try
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                MainWindowViewModel mainview = (MainWindowViewModel)main.DataContext;
                if (GlobalVariable.IsAlarmPage)
                {
                    AlarmPageViewModel alarmPageViewModel = mainview.alarmPageViewModel;
                    if (alarmPageViewModel.alarmMainPage != null)
                    {
                        alarmPageViewModel.alarmMainPage.ChangeAccessState(mes.status, mes.code);
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
        }


        public void OnHastenHandle(int alarmId, int userid)
        {
            int clientflag = MangoApp.getInstance().getClientInfo().flag;
            ///判断主分控
            if (clientflag == 0)
            {
                ///移除已处理报警
                //Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                //    MainWindow main = (MainWindow)Application.Current.MainWindow;
                //    main.RemoveAlarm(alarmId);
                //}));
            }
            else
            {
                if (userid != MangoApp.getInstance().getClientInfo().userId)
                {
                    //AppConfig.instance.alarmid.Add(alarmId);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        MainWindow main = (MainWindow)Application.Current.MainWindow;
                       // main.ShowHandelMessage(alarmId);
                    }));
                }
            }
        }


    }

    public class ClientInfo
    {
        public int userId;
        public string username;
        public string password;
        public ClientConfig config;
        public string module;
        public string sid;
        public int flag;
        public ClientInfo(int userId, string username, string password, ClientConfig config, string module, string sid, int flag)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.config = config;
            this.module = module;
            this.sid = sid;
            this.flag = flag;
        }
    }
}
