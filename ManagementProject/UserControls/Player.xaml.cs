using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class Player : UserControl
    {

        public PlayerViewModel PlayerViewModel;
        public Player(PlayerWindowType type)
        {
            InitializeComponent();
            PlayerViewModel = new PlayerViewModel(type);
            DataContext = PlayerViewModel;
            
            bottomgrid.MouseLeftButtonDown += Player_MouseLeftButtonDown;
            SizeChanged += Player_SizeChanged;

            ///获取控件Handel
            HwndSource hs = (HwndSource)PresentationSource.FromDependencyObject(mediaElement);

        }

      

        /// <summary>
        /// 根据实际窗口大小改变全屏按钮位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth;
            fullgrid.Margin =new Thickness(ActualWidth - 50,0,0,0);
            PlayerViewModel.IsOpened = false;
        }

        private void Player_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PlayerViewModel.IsOpened== true)
            {
                PlayerViewModel.IsOpened = false;
            }else
            {
                PlayerViewModel.IsOpened = false;
                PlayerViewModel.IsOpened = true;
            }
            
           
        }
     
    }
    public class PlayerModel : INotifyPropertyChangedClass
    {
        private bool isopened;
        public bool IsOpened
        {
            get { return isopened; }
            set
            {
                isopened = value;
                NotifyPropertyChanged("IsOpened");
            }
        }

        private Visibility isTrack;
        public Visibility IsTrack
        {
            get
            {
                return isTrack;
            }
            set
            {
                isTrack = value;
                NotifyPropertyChanged("IsTrack");
            }
        }
        private Visibility isPlayback;
        public Visibility IsPlayback
        {
            get
            {
                return isPlayback;
            }
            set
            {
                isPlayback = value;
                NotifyPropertyChanged("IsPlayback");
            }
        }
    }

    public class PlayerViewModel:PlayerModel
    {
        public DelegateCommand FullScreenCommand { get; set; }
        public DelegateCommand PhotographCommand { get; set; }
        public DelegateCommand ZoomCommand { get; set; }
        public DelegateCommand OperationCommand { get; set; }
        public DelegateCommand MessageCommand { get; set; }
        public DelegateCommand PlaybackCommand { get; set; }
        public DelegateCommand TrackCommand { get; set; }
        private void Init()
        {
            FullScreenCommand = new DelegateCommand();
            FullScreenCommand.ExecuteCommand = new Action<object>(FullScreen);

            PhotographCommand = new DelegateCommand();
            PhotographCommand.ExecuteCommand = new Action<object>(Photograph);

            ZoomCommand = new DelegateCommand();
            ZoomCommand.ExecuteCommand = new Action<object>(Zoom);

            OperationCommand = new DelegateCommand();
            OperationCommand.ExecuteCommand = new Action<object>(Operation);

            MessageCommand = new DelegateCommand();
            MessageCommand.ExecuteCommand = new Action<object>(Message);

            PlaybackCommand = new DelegateCommand();
            PlaybackCommand.ExecuteCommand = new Action<object>(Playback);

            TrackCommand = new DelegateCommand();
            TrackCommand.ExecuteCommand = new Action<object>(Track);
        }

        public PlayerViewModel(PlayerWindowType type)
        {
            IsVisbility(type);
            Init();

        }


        private void IsVisbility(PlayerWindowType type)
        {
            if (type ==PlayerWindowType.Playerback )
            {
                IsPlayback = Visibility.Collapsed;
                IsTrack = Visibility.Visible;
            }
            else if (type ==PlayerWindowType.Track )
            {
                IsPlayback = Visibility.Visible;
                IsTrack = Visibility.Collapsed;
            }
            else
            {
                IsPlayback = Visibility.Visible;
                IsTrack = Visibility.Visible;
            }
        }

        


       

        private void FullScreen(object obj)
        {
            MessageBox.Show("这是全屏按键！");
        }
        private void Photograph(object obj)
        {
            MessageBox.Show("这是拍照！");
        }
        private void Zoom(object obj)
        {
            MessageBox.Show("这是数字放大！");
        }
        private void Operation(object obj)
        {
            MessageBox.Show("这是操作！");
        }
        private void Message(object obj)
        {
            MessageBox.Show("message");
        }
        private void Playback(object obj)
        {
            MessageBox.Show("playback");
        }
        private void Track(object obj)
        {
            MessageBox.Show("track");
        }
    }
}
