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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// ErrorDeviceControl.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorDeviceControl : Window
    {
        public ErrorDeviceControl()
        {
            InitializeComponent();
            Loaded += ErrorDeviceControl_Loaded;
        }

        private void ErrorDeviceControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (GlobalVariable . CurrentClientProperty==0)
            {
                client.Visibility = Visibility.Hidden;
            }
        }
    }
}
