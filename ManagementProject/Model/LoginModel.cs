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
        /// <summary>
        /// 用户名
        /// </summary>
        private string _username;
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

        /// <summary>
        /// 用户密码
        /// </summary>
        private string _password;
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
    }
}
