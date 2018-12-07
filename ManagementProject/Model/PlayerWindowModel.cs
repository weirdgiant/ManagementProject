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
        private Visibility windowType;
        public Visibility WindowType
        {
            get
            {
                return windowType;
            }
            set
            {
                windowType = value;
                NotifyPropertyChanged("WindowType");
            }
        }

        private string windowLogo;
        public string WindowLogo
        {
            get
            {
                return windowLogo;
            }
            set
            {
                windowLogo = value;
                NotifyPropertyChanged("WindowLogo");
            }
        }
    }
}
