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
    /// TrackerInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrackerInfoControl : UserControl
    {
        TrackerInfoViewModel trackInfoViewModel = new TrackerInfoViewModel();
        public TrackerInfoControl()
        {
            InitializeComponent();
            DataContext = trackInfoViewModel;
        }
    }

    public class TrackerInfo
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }

    public class TrackerInfoModel:INotifyPropertyChangedClass
    {
        private ObservableCollection<TrackerInfo> _items ;
        public ObservableCollection<TrackerInfo> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }
    }
    public class TrackerInfoViewModel:TrackerInfoModel
    {
        public TrackerInfoViewModel()
        {
            Items = new ObservableCollection<TrackerInfo>();
            for (int i=0;i<4;i++)
            {
                TrackerInfo trackInfo = new TrackerInfo();
                trackInfo.Name = i + "";
                trackInfo.Time = i + 1 + "";
                Items.Add(trackInfo);
            }
        }
    }
}
