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
        private string nowtime;
        public string NowTime
        {
            get
            {
                return nowtime;
            }
            set
            {
                nowtime = value;
                NotifyPropertyChanged("NowTime");
            }
        }

        private string titlename;
        public string TitleName
        {
            get
            {
                return titlename;
            }
            set
            {
                titlename = value;
                NotifyPropertyChanged("TitleName");
            }
        }

        
    }
}
