﻿using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// AlarmTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmTabControl : UserControl
    {
        public AlarmTabControl()
        {
            InitializeComponent();
        }

    }
    public class AlarmTabModel:INotifyPropertyChangedClass
    {
        private int _alarmCount;
        /// <summary>
        /// 报警数量
        /// </summary>
        public int AlarmCount
        {
            get
            {
                return _alarmCount;
            }
            set
            {
                _alarmCount = value;
                NotifyPropertyChanged("AlarmCount");
            }
        }

        private ObservableCollection<Alarm> _alarmInfo;
        public ObservableCollection<Alarm> AlarmInfo
        {
            get
            {
                return _alarmInfo;
            }
            set
            {
                _alarmInfo = value;
                NotifyPropertyChanged("AlarmInfo");
                AlarmCount = AlarmInfo.Count;
            }
        }
    }
    public class AlarmTabViewModel:AlarmTabModel
    {
        public AlarmTabViewModel()
        {
        }
    }
}
