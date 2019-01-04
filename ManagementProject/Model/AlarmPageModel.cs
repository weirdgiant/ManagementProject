using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class AlarmPageModel : INotifyPropertyChangedClass
    {
        private AlarmType _pageType;
        public AlarmType PageType
        {
            get
            {
                return _pageType;
            }
            set
            {
                _pageType = value;
                NotifyPropertyChanged("PageType");
            }
        }


    }
}
