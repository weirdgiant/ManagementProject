﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class MainWindowModel : INotifyPropertyChanged
    {       
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }


    public class MainWindowProperty:MainWindowModel
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
    }
}