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
    /// MainWindowStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowStatistics : UserControl
    {

        public MainWindowStatistics()
        {
            InitializeComponent();
            drapbt.Click += Drapbt_Click;
        }

        private void Drapbt_Click(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = true;
        }
    }
    public class MainWindowStatisticsModel:INotifyPropertyChangedClass
    {
        private bool _isOpened;
        public bool IsOpened
        {
            get
            {
                return _isOpened;
            }
            set
            {
                _isOpened = value;
                NotifyPropertyChanged("IsOpened");
            }
        }

        private BitmapImage _icon;
        public BitmapImage Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                NotifyPropertyChanged("Icon");
            }
        }

        private string _number;
        public  string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }

        private ObservableCollection<DeviceItem> _deviceItemList;
        public ObservableCollection<DeviceItem> DeviceItemList
        {
            get
            {
                return _deviceItemList;
            }
            set
            {
                _deviceItemList = value;
                NotifyPropertyChanged("DeviceItemList");
            }
        }

        public class DeviceItem
        {
            public int id { get; set; }
            public string type { get; set; }
            public BitmapImage icon { get; set; }
            public string name { get; set; }
            public string count { get; set; }
        }
    }
    public class MainWindowStatisticsViewModel:MainWindowStatisticsModel
    {
        public MainWindowStatisticsViewModel()
        {

        }
    }
}
