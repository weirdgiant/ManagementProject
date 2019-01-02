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
    /// TrackerInfoControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrackerInfoControl : UserControl
    {
        public TrackerInfoControl()
        {
            InitializeComponent();
        }
    }

    public class TrackerInfoModel:INotifyPropertyChangedClass
    {

    }
    public class TrackerInfoViewModel:TrackerInfoModel
    {
        public TrackerInfoViewModel()
        {

        }
    }
}
