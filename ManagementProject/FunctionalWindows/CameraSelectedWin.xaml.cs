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

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// CameraSelectedWin.xaml 的交互逻辑
    /// </summary>
    public partial class CameraSelectedWin : Window
    {
        public int Sid { get; set; }
        public CameraSelectedWin()
        {
            InitializeComponent();
            Loaded += CameraSelectedWin_Loaded;
            Unloaded += CameraSelectedWin_Unloaded;
        }

        private void CameraSelectedWin_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Window win in this.OwnedWindows)
                {
                    win.Close();
                }
            }
            catch (Exception)
            {

            }
          
        }

        private void CameraSelectedWin_Loaded(object sender, RoutedEventArgs e)
        {
            map.DataContext = DataContext;
            map.IsTrackPage = true;
            map.LoadSelectedSchoolMap(Sid);

        }
    }
}
