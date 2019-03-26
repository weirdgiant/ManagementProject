using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class PlayControlModel:INotifyPropertyChangedClass
    {

        //private double pcWidth;

        //public double PcWidth
        //{
        //    get { return pcWidth; }
        //    set
        //    {
        //        pcWidth = value;
        //        NotifyPropertyChanged("PcWidth");
        //    }
        //}

        private int controlLogo;

        public int ControlLogo
        {
            get { return controlLogo; }
            set
            {
                controlLogo = value;
                NotifyPropertyChanged("ControlLogo");
            }
        }

    }
}
