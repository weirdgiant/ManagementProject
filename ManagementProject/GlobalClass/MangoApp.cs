using com.mango.protocol;
using com.mango.protocol.msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManagementProject
{
    public class MangoApp
    {
        private static MangoApp instance = new MangoApp();

        public static MangoApp getInstance()
        {
            return instance;
        }
        // public SpeechManager speechManager;
        private MangoSocketClient client;
        private AppMessageHandler handler;
      //  private MangoAlarmManager alarmManager;
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
        //public MangoAlarmManager getAlarmManager()
        //{
        //    return alarmManager;
        //}

        /// <summary>
        /// 消息监听线程
        /// </summary>
        public void start()
        {
            Thread thread = new Thread(run);
            thread.Start();
            //  AppLogger.log.Info("App start Thread Start:" + thread.ManagedThreadId);
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
            // AppLogger.log.Info("RUN");
            // speechManager = new SpeechManager();
            // speechManager.Init(config);
            handler = new AppMessageHandler();
            ///接受报警消息
         //   alarmManager = new MangoAlarmManager();

            client = new MangoSocketClient(AppConfig.ServerIP,int.Parse(AppConfig.ServerPort), handler);
            try
            {
                client.Start();     //启动连接服务器
            }
            catch (Exception ex)
            {
               // AppLogger.log.Error(e.ToString());
            }
     //       HKSDK.Get();
           // AppLogger.log.Info("App start Thread Stop:" + Thread.CurrentThread.ManagedThreadId);
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
            catch (Exception)
            {


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
               // AppLogger.log.Error(e.ToString());
            }

        }

        public override void onReconnected(MangoSocketClient client)
        {

        }


        /// <summary>
        /// 监听跟催消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        //public override void onSC_HastenHandle(object session, SC_HastenHandle message)
        //{
        //    AppLogger.log.Info("onSC_HastenHandle:" + message.alarmId + "," + message.userId);
        //    MangoApp.getInstance().getAlarmManager().OnHastenHandle(message.alarmId, message.userId);

        //}

        /// <summary>
        /// 监听报警处理信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        //public override void onSC_ConfirmAlarm(object session, SC_ConfirmAlarm message)
        //{
        //  //  AppLogger.log.Info("onSC_ConfirmAlarm:" + message.alarmId);

        //    MangoApp.getInstance().getAlarmManager().OnAlarmConfirmed(message.alarmId.ToArray());
        //}

        /// <summary>
        /// 监听报警信息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="message"></param>
        public override void onSC_Alarm(object session, SC_Alarm message)
        {
          //  AppLogger.log.Info("onSC_Alarm:" + message.alarm.id);
          //  MangoApp.getInstance().getAlarmManager().OnAlarm(message.alarm);
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
