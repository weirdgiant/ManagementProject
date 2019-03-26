using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class MainWindowModel : INotifyPropertyChangedClass
    {
        private string _currentTime;
        /// <summary>
        /// 当前时间
        /// </summary>
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
        }

        private string _titleName;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string TitleName
        {
            get
            {
                return _titleName;
            }
            set
            {
                _titleName = value;
                NotifyPropertyChanged("TitleName");
            }
        }

        private string _pageUrl;
        public string PageUrl
        {
            get
            {
                return _pageUrl;
            }
            set
            {
                _pageUrl = value;
                NotifyPropertyChanged("PageUrl");
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
    }
}
