using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.Model
{
    public class PlayerWindowModel : INotifyPropertyChangedClass
    {
        private Visibility _windowType;
        public Visibility WindowType
        {
            get
            {
                return _windowType;
            }
            set
            {
                _windowType = value;
                NotifyPropertyChanged("WindowType");
            }
        }

        private string _windowLogo;
        public string WindowLogo
        {
            get
            {
                return _windowLogo;
            }
            set
            {
                _windowLogo = value;
                NotifyPropertyChanged("WindowLogo");
            }
        }


        private Visibility _isAlarmTracker=Visibility.Visible;
        public Visibility IsAlarmTracker
        {
            get
            {
                return _isAlarmTracker;
            }
            set
            {
                _isAlarmTracker = value;
                NotifyPropertyChanged("IsAlarmTracker");
            }
        }
    }
}
