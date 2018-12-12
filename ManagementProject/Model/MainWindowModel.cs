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
        private string _nowTime;
        public string NowTime
        {
            get
            {
                return _nowTime;
            }
            set
            {
                _nowTime = value;
                NotifyPropertyChanged("NowTime");
            }
        }

        private string _titleName;
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
    }
}
