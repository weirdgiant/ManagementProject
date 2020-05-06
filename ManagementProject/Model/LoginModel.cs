using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class LoginModel : INotifyPropertyChangedClass
    {
        private bool _isLoginEnabled;
        public bool IsLoginEnabled
        {
            get
            {
                return _isLoginEnabled;
            }
            set
            {
                _isLoginEnabled = value;
                NotifyPropertyChanged("IsLoginEnabled");
            }
        }
        private string _username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                NotifyPropertyChanged("UserName");
            }
        }
    
        private string _password;
        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyPropertyChanged("PassWord");
            }
        }

        private string _phoneNumber;
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        private string _mailAdress;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string MailAdress
        {
            get
            {
                return _mailAdress;
            }
            set
            {
                _mailAdress = value;
                NotifyPropertyChanged("MailAdress");
            }
        }

        private string _company;
        /// <summary>
        /// 公司
        /// </summary>
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                NotifyPropertyChanged("Company");
            }
        }
        private string _copyright;
        /// <summary>
        /// 版权
        /// </summary>
        public string Copyright
        {
            get
            {
                return _copyright;
            }
            set
            {
                _copyright = value;
                NotifyPropertyChanged("Copyright");
            }
        }

        private string _version;
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                NotifyPropertyChanged("Version");
            }
        }
    }
}
