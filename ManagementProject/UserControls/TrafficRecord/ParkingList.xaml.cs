using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
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

namespace ManagementProject.UserControls.TrafficRecord
{
    /// <summary>
    /// ParkingList.xaml 的交互逻辑
    /// </summary>
    public partial class ParkingList : UserControl
    {
        public ParkingList()
        {
            InitializeComponent();
            Loaded += ParkingList_Loaded;
            start.textBlock1.TextChanged += TextBlock1_TextChanged;
            end.textBlock1.TextChanged += TextBlock1_TextChanged1;
        }

        private void ParkingList_Loaded(object sender, RoutedEventArgs e)
        {
            start.textBlock1.Text = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            end.textBlock1.Text = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void TextBlock1_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            TrafficRecordViewModel traffic = (TrafficRecordViewModel)DataContext;
            traffic.ParkingEndTime = end.textBlock1.Text;
        }

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            TrafficRecordViewModel traffic = (TrafficRecordViewModel)DataContext;
            traffic.ParkingStartTime = start.textBlock1.Text;
        }
    }
}
