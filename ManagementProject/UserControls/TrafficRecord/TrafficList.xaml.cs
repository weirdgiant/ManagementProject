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
    /// TrafficList.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficList : UserControl
    {
        public TrafficList()
        {
            InitializeComponent();
            start.textBlock1.TextChanged += TextBlock1_TextChanged;
            end.textBlock1.TextChanged += TextBlock1_TextChanged1;

        }

        private void TextBlock1_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            TrafficRecordViewModel traffic = (TrafficRecordViewModel)DataContext;
            traffic.EndTime = end.textBlock1.Text;
        }

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            TrafficRecordViewModel traffic = (TrafficRecordViewModel)DataContext;
            traffic.StartTime = start.textBlock1.Text;
        }
    }
}
