using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class TrackPageModel : INotifyPropertyChangedClass
    {
        private int _sid;
        public int Sid
        {
            get
            {
                return _sid;
            }
            set
            {
                _sid = value;
                NotifyPropertyChanged("Sid");
            }
        }
    }
}
