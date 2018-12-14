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
    /// AlarmButton.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmButton : UserControl
    {
        public AlarmButton()
        {
            InitializeComponent();
        }
    }

    public class AlarmButtonModel : INotifyPropertyChangedClass
    {
        private string _alarmCount;
        public string AlarmCount
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
        private string _alarmIcon;
        public string AlarmIcon
        {
            get
            {
                return _alarmIcon;
            }
            set
            {
                _alarmIcon = value;
                NotifyPropertyChanged("AlarmIcon");
            }
        }
    }
    public class AlarmButtonViewModel : AlarmButtonModel
    {
        public AlarmButtonViewModel()
        {

        }
    }

}
