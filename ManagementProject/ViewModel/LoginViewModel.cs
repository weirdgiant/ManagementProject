using com.mango.protocol;
using com.mango.protocol.msg;
using ManagementProject.Model;
using MangoApi;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class LoginViewModel:LoginModel 
    {
        public DelegateCommand LoginOKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand MoveWinCommand { get; set; }
        public LoginViewModel()
        {
            Init();
            LoginOKCommand = new DelegateCommand();
            LoginOKCommand.ExecuteCommand = new Action<object>(LoginOKAsync);

            CancelCommand = new DelegateCommand();
            CancelCommand.ExecuteCommand = new Action<object>(Cancel);
            MoveWinCommand = new DelegateCommand();
            MoveWinCommand.ExecuteCommand = new Action<object>(MoveWin);
        }
        /// <summary>
        /// 初始化登录界面信息
        /// </summary>
        private void Init()
        {
            try
            {
                IsLoginEnabled = true;
                PhoneNumber = "021-65691583";
                MailAdress = "inquir@mangocosmos.com";
                Version = "V " + AssemblyVersion;
                Copyright = AssemblyCopyright + " " + AssemblyCompany;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 登录确定
        /// </summary>
        /// <param name="obj"></param>
        private async void LoginOKAsync(object obj)
        {
            try
            {

            if (obj != null)
            {
                if (UserName == null || PassWord == null)
                {
                    MessageBox.Show("请输入账号和密码！", "错误");
                    return;
                }
                CS_LOGIN login = new CS_LOGIN
                {
                    username = UserName,
                    password = PassWord
                };
                OutStream message = App.mango.Async(login, (short)login.protocol);
                    
                    if (message != null)
                    {
                        SC_LOGIN ret = (SC_LOGIN)Package.SCStruct<SC_LOGIN>(message);
                        if (ret.ret == 0)
                        {
                            App.mango.SetClientInfo(ret.userId, login.username, login.password, ret.config, ret.module, ret.sid, ret.flag);
                            App.mango.Init();
                            var result = await LoginUnvAsync(AppConfig.SDKUserName, AppConfig.SDKPassword, AppConfig.SDKServerIp);
                            if (result)
                            {
                                BuildCameraList();
                                var isApi = await LoadApi();
                                if (isApi)
                                {
                                    Login loginwin = (Login)obj;
                                    MainWindow main = new MainWindow();
                                    main.Top = 0;
                                    main.Left = 0;
                                    main.Hide();
                                    main.Show();
                                    loginwin.Close();
                                }
                                else
                                {
                                    MessageBox.Show("获取Api失败!", "错误");
                                    Logger.Error(typeof(LoginViewModel), "API获取数据失败！");
                                }
                            }

                        }
                        else if (ret.ret == -2)
                        {
                            MessageBox.Show("账号已登录，请检查!", "错误");
                            Logger.Error(typeof(LoginViewModel), "账号已登录，请检查！");
                        }
                        else if (ret.ret == -3)
                        {
                            MessageBox.Show("客户端IP与账户绑定IP不匹配!", "错误");
                            Logger.Error(typeof(LoginViewModel), "客户端IP与账户绑定IP不匹配！");
                        }
                        else
                        {

                            MessageBox.Show("用户名或密码错!", "错误");
                            Logger.Error(typeof(LoginViewModel), "用户名或密码错！");
                        }

                    }
                    else
                    {
                        MessageBox.Show("请求超时，与服务器通讯故障!", "错误");
                    }
                    //var result = await LoginUnvAsync("loadmin", "Admin123", AppConfig.SDKServerIp);
                    //if (result)
                    //{
                    //    var isApi = await LoadApi();
                    //    if (isApi)
                    //    {
                    //        BuildCameraList();
                    //        Login loginwin = (Login)obj;
                    //        MainWindow main = new MainWindow();
                    //        main.Hide();
                    //        main.Show();
                    //        loginwin.Close();
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("API获取数据失败");
                    //        Logger.Error(typeof(LoginViewModel), "API获取数据失败！");
                    //    }
                    //}

                }
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }
        }

        

        /// <summary>
        /// 登录视频服务器
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<bool> LoginUnvAsync(string username, string password, string ip)
        {
            IsLoginEnabled = false;
            return await Task.Run(() =>
            {

                var sdkManager = SdkManager.GetInstance();
                var ulRet = sdkManager.LoginMethod(username, password, AppConfig.SDKServerIp, "N/A");               
                if (!ulRet)
                {
                    IsLoginEnabled = true;
                    return false;
                    
                }
                IsLoginEnabled = true;
                sdkManager.userLoginStatus = true;
                UnvOnePlayer.InitializePlayer();
                Logger.Info("登录视频服务器成功！");
                return true;
            });

        }

        public  void BuildCameraList()
        {
            Task.Run(() =>
            {
                var sdkManager = SdkManager.GetInstance();
                while (true)
                {

                    if (sdkManager.userLoginStatus)
                    {
                        sdkManager.TreeViewRoot = new System.Windows.Forms.TreeView();
                        sdkManager.SetTreeRoot(sdkManager.TreeViewRoot);
                        sdkManager.BuildTree(sdkManager.TreeViewRoot.Nodes[0], sdkManager.TreeViewRoot);
                        sdkManager.isGotCamListResetEvent.Set();
                        //sdkManager.TreeViewRoot.Sort();
                    }
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(30));
                }
            });

        }
        /// <summary>
        /// 检测Api是否连接
        /// </summary>
        /// <returns></returns>
        private async Task<bool> LoadApi()
        {
            IsLoginEnabled = false;
            return await Task.Run(() =>
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
                MangoMap[] map = HttpAPi.GetMapList(url);
                if (map!=null)
                {
                    IsLoginEnabled = true;
                    GlobalVariable.MapList = map;
                    return true;
                }
                IsLoginEnabled = true;
                return false;
            });
        }


        /// <summary>
        /// 取消退出
        /// </summary>
        /// <param name="obj"></param>
        private void Cancel(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                login.Close();
            }
        }

        /// <summary>
        /// 移动窗口
        /// </summary>
        /// <param name="obj"></param>
        private void MoveWin(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                login.DragMove();
            }
        }


        #region 程序集属性访问器

        /// <summary>
        /// 获取标题
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            }
        }

        /// <summary>
        /// 获取产品描述
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// 获取产品
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// 获取版权标志
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// 获取公司
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        
    }
}
