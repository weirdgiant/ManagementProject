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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// WindowsFormsHostButton.xaml 的交互逻辑
    /// </summary>
    public partial class WindowsFormsHostButton : UserControl
    {
        private BitmapImage _btImage;
        public BitmapImage BtImage
        {
            get
            {
                return _btImage;
            }
            set
            {
                _btImage = value;
                btimage.Source = BtImage;
            }
        }
        public WindowsFormsHostButton()
        {
            InitializeComponent();
        }
    }
}
