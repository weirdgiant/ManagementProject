using ManagementProject.FunctionalWindows;
using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class Player : System.Windows.Controls.UserControl, IDisposable
    {
        public MapControl mapControl { get; set; }
        private MainWindow _mainWin = (MainWindow)System.Windows.Application.Current.MainWindow;
        private MainWindowViewModel mainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }

        public int Index { get; set; }
        public bool Selected { get; set; }

        private bool isFull = false;
        public System.Windows.Forms.Panel PnlPlayer { get; set; }
        private readonly System.Windows.Forms.Panel _pnlDigital;
        public string CameraCode { get; set; }
        public bool IsZoom { get; set; }
        public IntPtr WindowHandle { get; set; }
        public bool NeedRestorePlay { get; set; }
        private DigitalRec _direc { get; set; }
        private SpinLock spinLock { get; set; } = new SpinLock(true);
        public UnvOnePlayer OnePlayer { get; set; }
        private PlayerWindowType _playerWinType;

        private void dispose()
        {
            PnlPlayer.Dispose();
            _pnlDigital.Dispose();
            WindowHandle = IntPtr.Zero;
        }
        public Player(PlayerWindowType type)
        {
            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = mainWindowViewModel.mainPageViewModel;
            InitializeComponent();
            _playerWinType = type;
            IsVisbility(type);
            OnePlayer = UnvOnePlayer.CreateOnePlayer();      
            SizeChanged += Player_SizeChanged;
            PnlPlayer = panel;
            if (panel != null)
            {
                WindowHandle = panel.Handle;
                panel.Tag = this;
                this.DataContext = this;
                panel.AllowDrop = true;
                //PnlPlayer.DragEnter += DropList_DragEnter;
                //PnlPlayer.DragDrop += DropList_Drop;
                panel.MouseClick += PnlPlayer_MouseClick;
                panel.MouseDoubleClick += Panel_MouseDoubleClick;

                _pnlDigital = new System.Windows.Forms.Panel();
                _pnlDigital.Visible = false;
                _pnlDigital.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left);
                _pnlDigital.MouseDown += PnlDigital_MouseDown;
                _pnlDigital.MouseMove += PnlDigital_MouseMove;
                _pnlDigital.MouseWheel += PnlDigital_MouseWheel;
                panel.Controls.Add(_pnlDigital);
                WindowsFormHostPlayer.SnapsToDevicePixels = true;

            }

            // Loaded += Player_Loaded;
            Unloaded += Player_Unloaded;
            ZoomButton.Click += ZoomButton_Click;
            snatch.Click += Snatch_Click;
            maxbt.Click += Maxbt_Click;
            infobt.Click += Infobt_Click;
            infopop.MouseLeftButtonDown += Infopop_MouseLeftButtonDown;
            playBack.Click += PlayBack_Click;
            playTrack.Click += PlayTrack_Click;
            operate.Click += Operate_Click;

        }

        private void Operate_Click(object sender, RoutedEventArgs e)
        {
            ptzpop.IsOpen = false ;
            ptzpop.IsOpen =true;
            Popup.IsOpen = false;
            if (ptzpop.IsOpen)
            {
                if (ActualHeight==1080)
                {
                    OnClick(ptzpop);
                }
               
                ptzpop.Height = ActualHeight;
                ptzGrid.Width = ActualWidth;
                ptzGrid.Height = ActualHeight;
                ptzpop.HorizontalOffset = -5;
                ptzpop.VerticalOffset = ptzGrid.Height - 5;
                ptz.CamCode = CameraCode;
                var ulRet = ptz.Ptz_Connect(CameraCode, SdkManager.GetInstance().stLoginInfo);
                if (ulRet == 0)
                {
                    OnePlayer.IsPtz = true;

                }
            }         
        }

        /// <summary>
        /// 解决Popup控件在全屏时只显示0.75问题
        /// </summary>
        private void OnClick(CusPopup pop)
        {
            DependencyObject parent = pop.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;

                    var mainWin = Application.Current.MainWindow;

                    element.Height = mainWin.Height;
                    element.Width = mainWin.Width;

                    break;
                }
            }
            while (parent != null);
        }

        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayTrack_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.PlayerWindow != null)
            {
                mainWindowViewModel.PlayerWindow.Close();
            }
            mainWindowViewModel.PlayerWindow = new PlayerWindow(PlayerWindowType.Track, CameraCode);
            mainWindowViewModel.PlayerWindow.mapControl = mapControl;
            mainWindowViewModel.PlayerWindow.Topmost = true;
            mainWindowViewModel.PlayerWindow.Owner = _mainWin;
            mainWindowViewModel.PlayerWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            mainWindowViewModel.PlayerWindow.Left = 23;
            mainWindowViewModel.PlayerWindow.Top = 165;
            mainWindowViewModel.PlayerWindow.Show();
        }
        /// <summary>
        /// 回放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBack_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.PlayerWindow!=null)
            {
                mainWindowViewModel.PlayerWindow.Close();
            }
            mainWindowViewModel.PlayerWindow = new PlayerWindow(PlayerWindowType.Playerback, CameraCode);
            mainWindowViewModel.PlayerWindow.mapControl = mapControl;
            mainWindowViewModel.PlayerWindow.Topmost = true;
            mainWindowViewModel.PlayerWindow.Owner = _mainWin;
            mainWindowViewModel.PlayerWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            mainWindowViewModel.PlayerWindow.Left = 23;
            mainWindowViewModel.PlayerWindow.Top = 165;
            mainWindowViewModel.PlayerWindow.Show();
        }

        private void IsVisbility(PlayerWindowType type)
        {
            if (type == PlayerWindowType.Playerback)
            {
                playBack.Visibility = Visibility.Collapsed;
                playTrack.Visibility = Visibility.Visible;
            }
            else if (type == PlayerWindowType.Track)
            {
                playBack.Visibility = Visibility.Visible;
                playTrack.Visibility = Visibility.Collapsed;
            }
            else if (type == PlayerWindowType.Normal)
            {
                playBack.Visibility = Visibility.Visible;
                playTrack.Visibility = Visibility.Visible;
            }
            else if (type == PlayerWindowType.Module)
            {
                playBack.Visibility = Visibility.Collapsed;
                playTrack.Visibility = Visibility.Collapsed;
            }
        }

        private void Panel_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            Player player = PnlPlayer.Tag as Player;
            PlayerPanel playPanel = VisualHelper.FindAnchestor<PlayerPanel>(player);
            if (player != null && player.OnePlayer.PlayType == PLAY_TYPE_E.PLAY_TYPE_LIVE)
            {
                PlayLiveAround(playPanel);
            }
        }

        public void PlayLiveAround(PlayerPanel playPanel)
        {
            if (playPanel == null || CameraCode == null) return;
            int playerCount = playPanel.Players.Count;

            int mapid = MangoInfo.instance.CameraList.Where(x => x.code == CameraCode).ToArray()[0].mapId;
            if (GlobalVariable.IsAlarmPage)
            {

                mapControl.LoadSelectedSchoolMap(mapid);// = mapid;

                DeviceType[] deviceTypes = HttpAPi.GetDeviceTypeList();
                deviceTypes = deviceTypes.Where(x => x.deviceClass == "Camera").ToArray();
                List<string> typeCodeList = new List<string>();
                foreach (var item in deviceTypes)
                {
                    if (!typeCodeList.Contains(item.deviceTypeCode))
                    {
                        typeCodeList.Add(item.deviceTypeCode);
                    }

                }

                mapControl.SetDevice(typeCodeList);
            }
            else
            {
                mainPageViewModel.Sid = mapid;
            }

            mapControl.MoveToTwoThirds(CameraCode);
            string localAttrs = string.Join(",", mapControl.ParameterList);
            if (playerCount == 6)
            {
                List<string> codeList = new List<string>();
                playPanel.InitSixPlayerPanel(3, 3, CameraCode, playPanel.playgrid, ref codeList, PlayerWindowType.Track, localAttrs);
            }
            else if (playerCount == 9)
            {
                playPanel.InitPlayerPanel(3, 3, CameraCode, playPanel.playgrid, PlayerWindowType.Track, localAttrs);
            }
            else if (playerCount == 16)
            {
                playPanel.InitPlayerPanel(4, 4, CameraCode, playPanel.playgrid, PlayerWindowType.Track, localAttrs);
            }
        }

        #region 显示摄像机信息

        /// <summary>
        /// 显摄像机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Infobt_Click(object sender, RoutedEventArgs e)
        {
            List<Device> list = MangoInfo.instance.CameraList.Where(x => x.code == CameraCode).ToList();
            if (list .Count !=0)
            {
                Popup.IsOpen = false;
                grid.Height = ActualHeight - 10;
                pcode.Content = CameraCode;
                pcode.ToolTip = CameraCode;
                pname.Content = list[0].name;
                pname.ToolTip = list[0].name;
                pip.Content = list[0].ip;
                pip.ToolTip = list[0].ip;
                infopop.IsOpen = true;
            }

        }
        private void Infopop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            infopop.IsOpen = false;
        }
        #endregion

        private void Player_Unloaded(object sender, RoutedEventArgs e)
        {
            UnvOnePlayer.CurrentPlayerNumber--;
            StopPlay();
            OnePlayer.StopAll();
            Dispose();
        }

        /// <summary>
        /// 播放实时
        /// </summary>
        public void StartLive()
        {
            //string codee = "RenLian_HouMen";
            if (CameraCode != null && OnePlayer.IsPlayed != true)
            {
                PlayLive(CameraCode);
            }
        }

        #region 全屏功能
        public Window MaxWin { get; set; }
        public bool IsMaxWin = false;
        private void Maxbt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsMaxWin)
                {
                    MaxWin.Close();
                }
                else
                {
                    MaxWin = new Window
                    {
                        WindowState = WindowState.Maximized,
                        WindowStyle = WindowStyle.None
                    };
                    Player p = new Player(_playerWinType);
                    p.IsMaxWin = true;
                    p.playBack.Visibility = Visibility.Collapsed;
                    p.playTrack.Visibility = Visibility.Collapsed;
                    p.MaxWin = MaxWin;
                    Grid grid = new Grid
                    {
                        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5C5C5C"))
                    };
                    grid.Children.Add(p);
                    p.CameraCode = CameraCode;
                    p.StartLive();

                    MaxWin.Content = grid;
                    MaxWin.Show();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(Player), ex.Message);
            }
        }
        #endregion

        #region 拍照
        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snatch_Click(object sender, RoutedEventArgs e)
        {
            string fileName = string.Empty;
            if (CameraCode != null)
            {
                fileName = OnePlayer.SnapPicture(CameraCode, 1) + ".jpg";
                System.Diagnostics.Process.Start("explorer.exe", fileName);
            }
        }
        #endregion

        #region 数字放大
        /// <summary>
        /// 数字放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomButton_Click(object sender, RoutedEventArgs e)
        {

            if (IsZoom)
            {
                var result = StopDigitalZoom();
                if (result)
                {
                    //ZoomButton.Foreground = _defaultForeBrush;
                }

            }
            else
            {
                var result = StartDigitalZoom();
                if (result)
                    ZoomButton.Foreground = new SolidColorBrush(Colors.LawnGreen);
            }


        }
        private bool StartDigitalZoom()
        {
            _pnlDigital.Top = 0;
            _pnlDigital.Left = 0;
            _pnlDigital.Width = panel.Width / 3;
            _pnlDigital.Height = panel.Height / 3;
            _direc = new DigitalRec();
            XP_RECT_S rec = _direc.GetRec(_pnlDigital.Width, _pnlDigital.Height, _pnlDigital.Location);
            bool flag = OnePlayer.DigitalZoom(_pnlDigital.Handle, rec);
            if (flag)
            {
                _pnlDigital.Visible = true;
                IsZoom = true;
                //this.pbDigitalZoom.Image = PlayWindow._stopDigitalZoomBitmap1;
            }
            return flag;
        }

        private bool StopDigitalZoom()
        {
            if (OnePlayer.CloseDigitalZoom())
            {
                _pnlDigital.Visible = false;
                IsZoom = false;
                return true;
            }
            return false;
        }
        private void PnlDigital_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (System.Windows.Forms.MouseButtons.Left == e.Button)
            {
                XP_RECT_S rec = _direc.GetRec(_pnlDigital.Width, _pnlDigital.Height, e.Location);
                OnePlayer.DigitalZoom(_pnlDigital.Handle, rec);
            }
            _pnlDigital.Focus();
        }

        private void PnlDigital_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            XP_RECT_S rec = _direc.GetRec(_pnlDigital.Width, _pnlDigital.Height, e.Location);
            OnePlayer.DigitalZoom(_pnlDigital.Handle, rec);
        }

        private void PnlDigital_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (IsZoom)
            {
                XP_RECT_S xp_rec;
                if (e.Delta > 0)
                {
                    _direc.Zoom = _direc.Zoom + 1;
                    xp_rec = _direc.GetRec(_pnlDigital.Width, _pnlDigital.Height, e.Location);
                }
                else
                {
                    _direc.Zoom = _direc.Zoom - 1;
                    xp_rec = _direc.GetRec(_pnlDigital.Width, _pnlDigital.Height, e.Location);
                }
                OnePlayer.DigitalZoom(_pnlDigital.Handle, xp_rec);
            }
        }
        public void PnlPlayer_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Popup.IsOpen = !Popup.IsOpen;

                var ulRet = ptz.Ptz_Connect(CameraCode, SdkManager.GetInstance().stLoginInfo);
                if (ulRet==0)
                {
                    operate.Visibility = Visibility.Visible;
                }
                else
                {
                    operate.Visibility = Visibility.Collapsed;
                }
                
            }
        }

        #endregion

        #region 实时播放
        public void PlayLive()
        {
            if (!string.IsNullOrEmpty(CameraCode))
            {
                PlayLive(CameraCode);

            }

        }

        public void PlayLive(string cameraCode)
        {
            if (string.IsNullOrEmpty(cameraCode))
                return;

            Task.Run(() =>
            {
                bool gotLock = false;

                try
                {
                    spinLock.Enter(ref gotLock);
                    StopPlay();
                    if (!SdkManager.GetInstance().IsCameraOnline(cameraCode))
                    {
                        Logger.Debug(GetType(), "摄像机下线：" + cameraCode + " ");
                        return;
                    }
                    uint result = OnePlayer.PlayLive(cameraCode, WindowHandle);


                    if (result == 0)
                    {
                        CameraCode = cameraCode;
                        NeedRestorePlay = true;
                    }
                    else
                    {
                        Logger.Debug(GetType(), "开始播放实况失败" + ErrorCode.GetErrorMsg(result));
                    }

                }
                finally
                {
                    // Only give up the lock if you actually acquired it
                    if (gotLock) spinLock.Exit();
                }

            });

        }

        #endregion

        #region 播放录像
        public void PlayVOD(URL_INFO_S urlInfo, uint time = 0)
        {
            Task.Run(() =>
            {
                bool gotLock = false;

                try
                {

                    spinLock.Enter(ref gotLock);
                    StopPlay();
                    uint result = OnePlayer.StartVod(urlInfo, WindowHandle);
                    if (result != 0)
                    {
                        Logger.Error(GetType(), "开始播放VOD失败" + ErrorCode.GetErrorMsg(result));
                    }
                    else
                    {
                        if (time != 0)
                        {
                            SpinWait.SpinUntil(() => OnePlayer.ChannelCode != null, 800);
                            SetPlayTime(time);
                        }

                    }
                }
                finally
                {
                    // Only give up the lock if you actually acquired it
                    if (gotLock) spinLock.Exit();
                }
            });


        }

        public void PlayVOD(string strBegin, string strEnd, string cameraCode, uint time = 0)
        {
            Task.Run(() =>
            {
                string cameraCodeStr = cameraCode.PadRight(48, '\0');
                var filelist = SdkManager.GetInstance().QueryRecord(strBegin, strEnd, cameraCodeStr);
                List<URL_INFO_S> urllist = new List<URL_INFO_S>();
                if (filelist.Count > 0)
                {
                    urllist = SdkManager.GetInstance().GetRecordURL(filelist, cameraCodeStr);
                }
                if (urllist.Count > 0)
                {
                    foreach (URL_INFO_S url in urllist)
                    {

                        PlayVOD(url, time);
                        CameraCode = cameraCode;

                    }

                }
            });

        }
        public void SetPlayTime(uint time)
        {
            if (OnePlayer.ChannelCode != null)
            {

                IMOSSDK.IMOS_SetPlayedTimeEx(
                ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo,
                Encoding.Default.GetBytes(OnePlayer.ChannelCode), time);


            }
        }
        #endregion
        public void StopVOD()
        {

            uint result = OnePlayer.StopVod();
            if (result != 0)
            {
                Logger.Error(GetType(), CameraCode + "停止VOD失败");
            }
            else
            {
                OnePlayer.ChannelCode = null;
            }

            var convert = new System.Drawing.ColorConverter();

            var convertFromString = convert.ConvertFromString("#071427");
            if (convertFromString != null)
                panel.BackColor = (System.Drawing.Color)convertFromString;
            panel.Refresh();

        }

        public void StopPlay()
        {

            uint ulRet = 0;
            OnePlayer.StopAll();

            if (SdkManager.GetInstance().userLoginStatus && this.OnePlayer.IsPtz)
            {
                ulRet = IMOSSDK.IMOS_ReleasePtzCtrl(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(this.CameraCode), 1);

            }
            if (ulRet != 0)
            {
                Logger.Error(GetType(), "云台链接释放失败!");
            }


            Dispatcher.BeginInvoke(new Action(() =>
            {
                panel.Refresh();
            }));
        }

        public void PauseVOD()
        {
            OnePlayer.PauseVod();

        }

        public void ResumeVOD()
        {
            OnePlayer.ResumeVod();

        }

        public void PlayOnSpeed(uint playSpeed)
        {
            OnePlayer.PlayOnSpeed(playSpeed);

        }

        public void PlayByFrame()
        {
            OnePlayer.PlayByOneFrame();

        }





        /// <summary>
        /// 根据实际窗口大小改变全屏按钮位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth;
            fullgrid.Margin = new Thickness(ActualWidth - 50, 0, 0, 0);
            //PlayerViewModel.IsOpened = false;
            Popup.IsOpen = false;
            if (OnePlayer != null && this.OnePlayer.IsDigitalZoom)
            {
                _pnlDigital.Width = panel.Width / 3;
                _pnlDigital.Height = panel.Height / 3;
                _pnlDigital.BringToFront();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~Player() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion


        private void Panel_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.All;
            DrawDragDropEffect(e.Data);
        }

        private void DrawDragDropEffect(System.Windows.Forms.IDataObject dragEventData)
        {
            //object data= dragEventData.GetData(typeof(Model.CameraList));
            //CameraCode = "31011000111320000004";
            //PlayLive(CameraCode);
        }
    }
}
