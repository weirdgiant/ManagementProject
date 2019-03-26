using com.mango.protocol;
using com.mango.protocol.msg;
using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

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
            LoginOKCommand.ExecuteCommand = new Action<object>(LoginOK);

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
            PhoneNumber = "021-65691583";
            MailAdress = "inquir@mangocosmos.com";
            Version= AssemblyVersion;
            Copyright = AssemblyCopyright +" "+ AssemblyCompany;
        }

        /// <summary>
        /// 登录确定
        /// </summary>
        /// <param name="obj"></param>
        private void LoginOK(object obj)
        {
            //if (obj != null)
            //{
            //    if (UserName==null|| PassWord==null)
            //    {
            //        MessageBox.Show("请输入账号和密码！","错误");
            //        return;
            //    }
            //    CS_LOGIN login = new CS_LOGIN
            //    {
            //        username = UserName,
            //        password = PassWord
            //    };
            //    OutStream message = App.mango.Async(login, (short)login.protocol);
            //    if (message != null)
            //    {
            //        SC_LOGIN ret = (SC_LOGIN)Package.SCStruct<SC_LOGIN>(message);
            //        if (ret.ret == 0)
            //        {
            //            App.mango.SetClientInfo(ret.userId, login.username, login.password, ret.config, ret.module, ret.sid, ret.flag);
            //            App.mango.Init();

            //            Login loginwin = (Login)obj;
            //            MainWindow main = new MainWindow();
            //            main.Show();
            //            loginwin.Close();
            //        }
            //        else if (ret.ret == -2)
            //        {
            //            MessageBox.Show("账号已登录，请检查!","错误");
            //        }
            //        else
            //        {

            //            MessageBox.Show("用户名或密码错!","错误");
            //        }

            //    }
            //    else
            //    {
            //        MessageBox.Show("请求超时，与服务器通讯故障!","错误");
            //    }
            Login loginwin = (Login)obj;
            MainWindow main = new MainWindow();
            main.Hide();
            main.Show();
            loginwin.Close();
            //}
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
                Environment.Exit(0);
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
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
