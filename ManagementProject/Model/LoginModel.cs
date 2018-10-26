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
        private string username;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                NotifyPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        private string password;
        public string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                NotifyPropertyChanged("PassWord");
            }
        }
    }
}
