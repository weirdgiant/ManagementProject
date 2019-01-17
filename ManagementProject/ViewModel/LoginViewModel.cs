using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        private void Init()
        {
            PhoneNumber = "021-65691583";
            MailAdress = "inquir@mangocosmos.com";
            Version= AssemblyVersion;
            Copyright = AssemblyCopyright +" "+ AssemblyCompany;
        }


        private void LoginOK(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                MainWindow main = new MainWindow();
                main.Show();
                login.Close();
            }
        }
        private void Cancel(object obj)
        {
            if (obj != null)
            {
                Login login = (Login)obj;
                login.Close();
            }
        }

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
