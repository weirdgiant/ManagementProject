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
using System.Windows.Shapes;

namespace ManagementProject.UserControls.TrackPageControls
{
    /// <summary>
    /// TrackCameraWin.xaml 的交互逻辑
    /// </summary>
    public partial class TrackCameraWin : Window
    {
        public TrackCameraWin()
        {
            InitializeComponent();
            Loaded += TrackCameraWin_Loaded;
        }

        private void TrackCameraWin_Loaded(object sender, RoutedEventArgs e)
        {
            tracker.DataContext = DataContext;           
        }
    }
}
