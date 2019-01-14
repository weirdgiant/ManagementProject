using ManagementProject.FunctionalWindows;
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

        private PlayerState _state;
        public PlayerState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                NotifyPropertyChanged("State");
            }
        }
    }

    public class PlayerViewModel:PlayerModel
    {
        public DelegateCommand ShowPopupCommand { get; set; }
        public DelegateCommand FullScreenCommand { get; set; }
        public DelegateCommand PhotographCommand { get; set; }
        public DelegateCommand ZoomCommand { get; set; }
        public DelegateCommand OperationCommand { get; set; }
        public DelegateCommand MessageCommand { get; set; }
        public DelegateCommand PlaybackCommand { get; set; }
        public DelegateCommand TrackCommand { get; set; }
        private void InitCommand()
        {
            ShowPopupCommand = new DelegateCommand();
            ShowPopupCommand.ExecuteCommand = new Action<object>(ShowPopup);
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

        PlayerWindowType Ptype;
        public PlayerViewModel(PlayerWindowType type)
        {
            Ptype = type;
            IsVisbility(type);
            InitCommand();

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


        private void ShowPopup(object obj)
        {
            if (IsOpened == true)
            {
                IsOpened = false;
            }
            else
            {
                IsOpened = true;
            }
        }


        /// <summary>
        /// 全屏
        /// </summary>
        /// <param name="obj"></param>
        private void FullScreen(object obj)
        {
            if (State == PlayerState.Max )
            {
                State = PlayerState.Normal;
                Window w = (Window)obj;
                w.Close();
            }
            else
            {
                State = PlayerState.Max;
                Window w = new Window();
                w.WindowState = WindowState.Maximized;
                w.WindowStyle = WindowStyle.None;
                Player p = new Player(Ptype);
                
                p.DataContext = this;
                Grid grid = new Grid();
                grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5C5C5C"));
                grid.Children.Add(p);
                p.Margin = new Thickness(0);
                w.Content = grid;
                w.Show();
            }
           
        }
        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="obj"></param>
        private void Photograph(object obj)
        {
            MessageBox.Show("这是拍照！");
        }
        /// <summary>
        /// 数字放大
        /// </summary>
        /// <param name="obj"></param>
        private void Zoom(object obj)
        {
            MessageBox.Show("这是数字放大！");
        }
        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="obj"></param>
        private void Operation(object obj)
        {
            MessageBox.Show("这是操作！");
        }
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="obj"></param>
        private void Message(object obj)
        {
            MessageBox.Show("这是信息！");
        }
        /// <summary>
        /// 回放
        /// </summary>
        /// <param name="obj"></param>
        private void Playback(object obj)
        {
            if (GlobalVariable.PlayerWindowIsOpened == false)
            {
                PlayerWindow newWin = new PlayerWindow(PlayerWindowType.Playerback);
                newWin.Topmost = true;
                newWin.WindowStartupLocation = WindowStartupLocation.Manual;
                newWin.Left = 23;
                newWin.Top = 165;
                newWin.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
            else
            {
                PlayerWindow w = (PlayerWindow)obj;
                w.Close();
                PlayerWindow newWin = new PlayerWindow(PlayerWindowType.Playerback);
                newWin.Topmost = true;
                newWin.WindowStartupLocation = WindowStartupLocation.Manual;
                newWin.Left = 23;
                newWin.Top = 165;
                newWin.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
        }
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="obj"></param>
        private void Track(object obj)
        {
            if (GlobalVariable.PlayerWindowIsOpened == false)
            {
                PlayerWindow newWin = new PlayerWindow(PlayerWindowType.Track);
                newWin.Topmost = true;
                newWin.WindowStartupLocation = WindowStartupLocation.Manual;
                newWin.Left = 23;
                newWin.Top = 165;
                newWin.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
            else
            {
                PlayerWindow w = (PlayerWindow)obj;
                w.Close();
                PlayerWindow newWin = new PlayerWindow(PlayerWindowType.Track);
                newWin.Topmost = true;
                newWin.WindowStartupLocation = WindowStartupLocation.Manual;
                newWin.Left = 23;
                newWin.Top = 165;
                newWin.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
        }
    }
}
