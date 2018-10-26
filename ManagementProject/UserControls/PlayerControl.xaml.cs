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
    /// PlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControlViewModel PlayerControlViewModel;
        public PlayerControl()
        {
            InitializeComponent();
            PlayerControlViewModel = new PlayerControlViewModel();
            PlayerControlViewModel.PlaybackTime = "这是播放时间";
            this.DataContext = PlayerControlViewModel;
                
        }
    }

    

    public class PlayerControlViewModel : INotifyPropertyChangedClass
    {
        /// <summary>
        /// 播放时间
        /// </summary>
        private string playbacktime;
        public string PlaybackTime
        {
            get
            {
                return playbacktime;
            }
            set
            {
                playbacktime = value;
                NotifyPropertyChanged("PlaybackTime");
            }
        }
    }
}
