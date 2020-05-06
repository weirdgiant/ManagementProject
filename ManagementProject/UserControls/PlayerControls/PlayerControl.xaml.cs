using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        public PlayerControl()
        {
            InitializeComponent();

            start.Loaded += (s, e) => { start.TimeText = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss"); };
            end.Loaded += (s, e) => { end.TimeText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); };
            start.textBlock1.TextChanged += TextBlock1_TextChanged;
            end.textBlock1.TextChanged += TextBlock1_TextChanged1;
        }

        private void TextBlock1_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            PlayerControlViewModel controlViewModel = DataContext as PlayerControlViewModel;
            controlViewModel.EndTime = end.textBlock1.Text;
        }

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext == null) return;
            PlayerControlViewModel controlViewModel = DataContext as PlayerControlViewModel;
            controlViewModel.StartTime = start.textBlock1.Text;
        }
    }

    public class PlayerControlModel : INotifyPropertyChangedClass
    {
        #region 变量定义

        private string playbacktime;
        /// <summary>
        /// 播放时间
        /// </summary>
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

        private string _startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }

        private string _endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }

        private double _maxiMum;
        public double MaxiMum
        {
            get
            {
                return _maxiMum;
            }
            set
            {
                _maxiMum = value;
                NotifyPropertyChanged("MaxiMum");
            }
        }

        private double _miniMum;
        public double MiniMum
        {
            get
            {
                return _miniMum;
            }
            set
            {
                _miniMum = value;
                NotifyPropertyChanged("MiniMum");
            }
        }


        private double _sliderValue;
        public double SliderValue
        {
            get
            {
                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
                NotifyPropertyChanged("SliderValue");
            }
        }

        private PlayerControlType playerControlStatus;
        public PlayerControlType PlayerControlStatus
        {
            get
            {
                return playerControlStatus;
            }
            set
            {
                playerControlStatus = value;
                NotifyPropertyChanged("PlayerControlStatus");
            }
        }

        private string startBtToolTip;
        public string StartBtToolTip
        {
            get
            {
                return startBtToolTip;
            }
            set
            {
                startBtToolTip = value;
                NotifyPropertyChanged("StartBtToolTip");
            }
        }
        private string startIcon;
        public string StartIcon
        {
            get
            {
                return startIcon;
            }
            set
            {
                startIcon = value;
                NotifyPropertyChanged("StartIcon");
            }
        }

        private string endIcon;
        public string EndIcon
        {
            get
            {
                return endIcon;
            }
            set
            {
                endIcon = value;
                NotifyPropertyChanged("EndIcon");
            }
        }

        private bool endBtEnabled;
        public bool EndBtEnabled
        {
            get
            {
                return endBtEnabled;
            }
            set
            {
                endBtEnabled = value;
                NotifyPropertyChanged("EndBtEnabled");
            }
        }

        private bool isOpen;
        public bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
                NotifyPropertyChanged("IsOpen");
            }
        }

        private string _selectedRate;
        public string SelectedRate
        {
            get
            {
                return _selectedRate;
            }
            set
            {
                _selectedRate = value;
                NotifyPropertyChanged("SelectedRate");
            }
        }

        private string _rateColor;
        public string RateColor
        {
            get
            {
                return _rateColor;
            }
            set
            {
                _rateColor = value;
                NotifyPropertyChanged("SelectedRate");
            }
        }

        private ObservableCollection<SelectionItem> selectionItemCollection;
        public ObservableCollection<SelectionItem> SelectionItemCollection
        {
            get
            {
                return selectionItemCollection;
            }
            set
            {
                selectionItemCollection = value;
                NotifyPropertyChanged("SelectionItemCollection");
            }
        }

        public class SelectionItem
        {
            public string Header { get; set; }     
            public uint Speed { get; set; }
        }
        #endregion
    }

    public class PlayerControlViewModel : PlayerControlModel
    {
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand EndCommand { get; set; }
        public DelegateCommand OpenContextMenuCommand { get; set; }
        public DelegateCommand RateSelectedCommand { get; set; }
        public DelegateCommand SliderMoveCommand { get; set; }
        public DelegateCommand SliderDownCommand { get; set; }
        public PlayerControlViewModel()
        {
            StartTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InitSelectionItem();
            PlayerControlInit();
            DelegateCommandInit();
        }

        private void InitSelectionItem()
        {
            RateColor = "#FF818181";
            SelectedRate = "1.0X";
            SelectionItemCollection = new ObservableCollection<SelectionItem>();
            string[] NameList = { "4.0X" , "2.0X", "1.0X","-1.0X", "-2.0X", "-4.0X" };
            uint[] SpeedList = { 11,10,9,4,3,2};
            for (int i=0;i< NameList.Length; i++)
            {
                SelectionItem st = new SelectionItem();
                st.Header = NameList[i];
                st.Speed = SpeedList[i];
                SelectionItemCollection.Add(st);
            }
           
        }

        private void PlayerControlInit()
        {
            EndType();
            StartBtToolTip = "播放";
            PlayerControlStatus = PlayerControlType.End;
        }

        private void DelegateCommandInit()
        {
            StartCommand = new DelegateCommand();
            StartCommand.ExecuteCommand = new Action<object>(Start);

            EndCommand = new DelegateCommand();
            EndCommand.ExecuteCommand = new Action<object>(End);

            OpenContextMenuCommand = new DelegateCommand();
            OpenContextMenuCommand.ExecuteCommand = new Action<object>(OpenContextMenu);

            RateSelectedCommand = new DelegateCommand();
            RateSelectedCommand.ExecuteCommand = new Action<object>(RateSelected);

            SliderMoveCommand = new DelegateCommand();
            SliderMoveCommand.ExecuteCommand = new Action<object>(SliderMove);

            SliderDownCommand = new DelegateCommand();
            SliderDownCommand.ExecuteCommand = new Action<object>(SliderDown);

        }
        private void SliderDown(object obj)
        {
            if (VodTimer.IsEnabled)
                VodTimer.Stop();
        }

        /// <summary>
        /// 打开倍率选择
        /// </summary>
        /// <param name="obj"></param>
        private void OpenContextMenu(object obj)
        {
            if (IsOpen)
            {
                IsOpen = false ;
                RateColor = "#FF818181";
            }
            else
            {
                IsOpen = true;
                RateColor = "#FF1A8BDA";
            }
           
        }

        /// <summary>
        /// 选择倍率
        /// </summary>
        /// <param name="obj"></param>
        private void RateSelected(object obj)
        {
            SelectionItem item = (SelectionItem)obj;
            SelectedRate = item.Header;
            IsOpen = false;
            Task.Run(() =>
            {
                if (IsAllVODPlay)
                {
                    var players = SelectedPlayerPanel.Players.Where(p => p.CameraCode != null);

                    foreach (var player in players)
                    {
                        if (player.OnePlayer.PlayType == PLAY_TYPE_E.PLAY_TYPE_VOD)
                            player.PlayOnSpeed(item.Speed);
                    }
                }
                else
                {
                    if (SelectedPlayer.OnePlayer.PlayType == PLAY_TYPE_E.PLAY_TYPE_VOD)
                        SelectedPlayer.PlayOnSpeed(item.Speed);
                }
            });
        }

        /// <summary>
        /// 开始&暂停
        /// </summary>
        /// <param name="obj"></param>
        private void Start(object obj)
        {
            try
            {
                if (PlayerControlStatus == PlayerControlType.End)
                {
                    PlayerControlStatus = PlayerControlType.Playback;
                    StartBtToolTip = "暂停";
                    PauseType();
                    PlayVOD();
                }
                else if (PlayerControlStatus == PlayerControlType.Playback)
                {
                    PlayerControlStatus = PlayerControlType.Pause;
                    StartBtToolTip = "播放";
                    PlayBackType();
                    PauseAllVOD();
                }
                else if (PlayerControlStatus == PlayerControlType.Pause)
                {
                    PlayerControlStatus = PlayerControlType.Playback;
                    StartBtToolTip = "暂停";
                    PauseType();
                    ResumeAllVOD();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(PlayerControlViewModel),"Start:" + ex.Message);
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="obj"></param>
        private void End(object obj)
        {
            EndType();
            PlayerControlStatus = PlayerControlType.End;
            StartBtToolTip = "播放";
            StopPlay();
            //Task.Delay(1000);
            AllPlayLive();
        }

        private void PlayBackType()
        {
            StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (2).png";
            EndIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/结束回放-点击后.png";
            EndBtEnabled = true;
        }

        private void PauseType()
        {
            StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (1).png";
            EndIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/结束回放-点击后.png";
            EndBtEnabled = true;
        }

        private void EndType()
        {
            StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (2).png";
            EndIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/结束回放.png";
            EndBtEnabled = false;
            PlaybackTime = "0000-00-00 00:00:00";
        }

        private void InitVod()
        {
            dtBegin = DateTime.Parse(StartTime);
            dtEnd = DateTime.Parse(EndTime);
            dtSpan = dtEnd - dtBegin;
            VodTimer = new System.Windows.Threading.DispatcherTimer();
            VodTimer.Interval = new TimeSpan(0, 0, 0, 1);
            VodTimer.Tick += VodTimer_Tick;
            sdkManager = SdkManager.GetInstance();
        }

        private void VodTimer_Tick(object sender, EventArgs e)
        {
            if (SelectedPlayer == null)
                return;

            if (SliderValue < MaxiMum && SelectedPlayer.OnePlayer.ChannelCode != null)
            {
                double abcurrentSec = 0;
                // slider.Value = VodTimer.Interval.TotalSeconds + slider.Value;
                PlaybackTime = (dtBegin + new TimeSpan(0, 0, (int)SliderValue)).ToString();

                var ptr = Marshal.AllocHGlobal(1 * Marshal.SizeOf(typeof(int)));

                var ulRet = IMOSSDK.IMOS_GetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(SelectedPlayer.OnePlayer.ChannelCode), ptr);
                if (ulRet == 0)
                {

                    abcurrentSec = Marshal.ReadInt32(ptr) + TimeSpan.FromHours(8).TotalSeconds;

                }

                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var span = dtBegin - dt;
                double startSec = span.TotalSeconds;
                //Todo:UTC转化为当前时区时间
                double currentSec = abcurrentSec - startSec;
                if (currentSec > 0)
                {
                    SliderValue = currentSec;
                    PlaybackTime = (dtBegin + new TimeSpan(0, 0, (int)currentSec)).ToString();
                }

            }
            else
            {
                VodTimer.Stop();
                bool ret=VodTimer.IsEnabled;
            }
        }

        private void AllPlayLive()
        {
            try
            {
                var players = SelectedPlayerPanel.Players.Where(p => p.CameraCode != null);
                Task.Run(() =>
                {
                    foreach (var player in players)
                    {
                        player.PlayLive();
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }


        public static IMOSSDK.XP_RUN_INFO_CALLBACK_EX_PF callBackMsg;
        public SdkManager sdkManager { set; get; }
        public Player SelectedPlayer { set; get; }
        public PlayerPanel SelectedPlayerPanel { set; get; }
        public System.Windows.Threading.DispatcherTimer VodTimer { set; get; }
        public bool IsAllVODPlay { get; set; }



        private DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
        private DateTime dtBegin;
        private DateTime dtEnd;
        private TimeSpan dtSpan;
        private void PlayVOD()
        {
            //IsAllVODPlay = true;
            InitVod();
            //dtBegin = DateTime .Parse(StartTime);
            //dtEnd = DateTime .Parse (EndTime);

            dtSpan = dtEnd - dtBegin;
            MaxiMum = dtSpan.TotalSeconds;
            MiniMum = 0;

            TimeSpan currentPos = new TimeSpan(0, 0, (int)SliderValue);
            var vodBeginTime = dtBegin.ToString(SdkManager.m_strDateFormat);
            var vodEndTime = dtEnd.ToString(SdkManager.m_strDateFormat);

            double pos = SliderValue;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            var span = dtBegin - dt;

            //TODO: 转换为UTC
            double currentSec = span.TotalSeconds + pos - TimeSpan.FromHours(8).TotalSeconds;
            //Todo:UTC转化为当前时区时间
            Task.Run(() =>
            {
                if (IsAllVODPlay)
                {
                    var players = SelectedPlayerPanel.Players.Where(p => p.CameraCode != null);

                    foreach (var player in players)
                    {
                        player.StopPlay();
                        string strCameCode = player.CameraCode.PadRight(48, '\0');
                        List<URL_INFO_S> urlList = new List<URL_INFO_S>();
                        if (dtBegin < dtEnd)
                        {
                            List<RECORD_FILE_INFO_S> list = sdkManager.QueryRecord(vodBeginTime, vodEndTime, strCameCode);
                            if (list.Count > 0)
                            {
                                urlList = sdkManager.GetRecordURL(list, strCameCode);
                            }
                            else
                            {
                                MessageBox.Show("该摄像机下无录像信息！");
                            }

                        }
                        int i = urlList.Count - 1;
                        for (; i >= 0; i--)
                        {
                            var url = urlList[i];
                            player.PlayVOD(url);
                            if (player.OnePlayer.ChannelCode == null)
                                Thread.Sleep(1000);
                            if (player.OnePlayer.ChannelCode != null)
                                IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(player.OnePlayer.ChannelCode), (uint)currentSec);

                        }

                        //player.PlayVOD(dtBegin.ToString(SdkManager.m_strDateFormat), dtEnd.ToString(SdkManager.m_strDateFormat), imos.CameraCode);

                    }
                }
                else
                {

                    var players = SelectedPlayerPanel.Players.Where(p => p.CameraCode != null).ToList();
                    SelectedPlayer = players[0];
                    if (SelectedPlayer?.CameraCode != null)
                    {
                        SelectedPlayer.StopPlay();
                        string strCameCode = SelectedPlayer.CameraCode.PadRight(48, '\0');
                        List<URL_INFO_S> urlList = new List<URL_INFO_S>();
                        if (dtBegin < dtEnd)
                        {
                            List<RECORD_FILE_INFO_S> list = sdkManager.QueryRecord(vodBeginTime, vodEndTime, strCameCode);
                            if (list.Count > 0)
                            {
                                urlList = sdkManager.GetRecordURL(list, strCameCode);
                            }
                            else
                            {
                                MessageBox.Show("该摄像机下无录像信息！");
                            }
                              


                        }
                        int i = urlList.Count-1;
                        for (; i >0; i--)
                        {

                            var url = urlList[i];
                            SelectedPlayer.PlayVOD(url);
                            if (SelectedPlayer.OnePlayer.ChannelCode == null)
                                Thread.Sleep(1000);
                            if (SelectedPlayer.OnePlayer.ChannelCode != null)
                                IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(SelectedPlayer.OnePlayer.ChannelCode), (uint)currentSec);

                        }

                    }
                    else
                    {

                        MessageBox.Show("请先点选择播放窗口！");

                    }
                }

                VodTimer.Start();
            });

        }

        private void ResumeAllVOD()
        {
            if (IsAllVODPlay)
            {
                var players = SelectedPlayerPanel.Players.Where(p => p.OnePlayer.IsPlayed);

                foreach (var player in players)
                {
                    Task.Run(() =>
                    {
                        player.ResumeVOD();

                    });
                }
            }
            else
            {
                if (SelectedPlayer != null)
                {
                    SelectedPlayer.ResumeVOD();
                }
            }
        }

        private void PauseAllVOD()
        {
            if (IsAllVODPlay)
            {
                var players = SelectedPlayerPanel.Players.Where(p => p.OnePlayer.IsPlayed);

                foreach (var player in players)
                {
                    Task.Run(() =>
                    {
                        player.PauseVOD();

                    });
                }
            }
            else
            {
                if (SelectedPlayer != null)
                {
                    SelectedPlayer.PauseVOD();
                }
            }
        }
        private void StopPlay()
        {
            if (IsAllVODPlay)
            {
                var players = SelectedPlayerPanel.Players.Where(p => p.OnePlayer.IsPlayed);

                foreach (var player in players)
                {
                    Task.Run(() =>
                    {
                        player.StopPlay();
                        player.NeedRestorePlay = false;

                    });
                }
            }
            else
            {
                if (SelectedPlayer != null)
                {
                    SelectedPlayer.NeedRestorePlay = false;
                    SelectedPlayer.StopPlay();

                }
            }

            if (VodTimer.IsEnabled == true)
                VodTimer.Stop();

        }


        private void SliderMove(object obj)
        {

           // dtBegin = startDatePicker.SelectedDate.Value + startTimePicker.Value.Value.TimeOfDay;
            //  dtEnd = endDatePicker.SelectedDate.Value + endTimePicker.Value.Value.TimeOfDay;
            dtSpan = dtEnd - dtBegin;
            MaxiMum = dtSpan.TotalSeconds;

            TimeSpan currentPos = new TimeSpan(0, 0, (int)SliderValue);
            var beginTime = dtBegin.ToString(SdkManager.m_strDateFormat);
            var endTime = dtEnd.ToString(SdkManager.m_strDateFormat);
            PlaybackTime = (dtBegin + currentPos).ToLongTimeString();

            double pos = SliderValue;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            var span = dtBegin - dt;

            //TODO: 转换为UTC
            double currentSec = span.TotalSeconds + pos - TimeSpan.FromHours(8).TotalSeconds;
            //Todo:UTC转化为当前时区时间
            uint ulRet = 0;

            Task.Run(() =>
            {
                if (IsAllVODPlay)
                {
                    var players = SelectedPlayerPanel.Players.Where(p => p.OnePlayer.IsPlayed && p.OnePlayer.PlayType == PLAY_TYPE_E.PLAY_TYPE_VOD);

                    foreach (var player in players)
                    {

                        if (currentSec > 0)
                        {
                            ulRet = IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(player.OnePlayer.ChannelCode), (uint)currentSec);

                            if (ulRet != 0)
                            {
                                Logger.Error(GetType(), player.CameraCode + ":设置播放时间失败" + ErrorCode.GetErrorMsg(ulRet));
                            }


                        }
                        else
                        {
                            List<URL_INFO_S> urlList = new List<URL_INFO_S>();
                            string strCameCode = player.CameraCode.PadRight(48, '\0');
                            if (dtBegin < dtEnd)
                            {

                                List<RECORD_FILE_INFO_S> list = sdkManager.QueryRecord(beginTime, endTime, strCameCode);
                                if (list.Count > 0)
                                    urlList = sdkManager.GetRecordURL(list, strCameCode);

                            }

                            foreach (URL_INFO_S url in urlList)
                            {
                                player.PlayVOD(url);

                                int n = 0;
                                while (player.OnePlayer.ChannelCode == null)
                                {
                                    Thread.Sleep(100);
                                    if (n > 20)
                                        break;
                                    n++;
                                }

                                if (player.OnePlayer.ChannelCode != null)
                                    ulRet = IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(player.OnePlayer.ChannelCode), (uint)currentSec);
                                if (ulRet != 0)
                                {
                                    Logger.Error(GetType(), player.CameraCode + ":设置播放时间失败" + ErrorCode.GetErrorMsg(ulRet));
                                }


                            }


                        }

                    }
                }

                else
                {
                    if (SelectedPlayer != null)
                    {

                        if (SelectedPlayer.OnePlayer.IsPlayed && SelectedPlayer.OnePlayer.PlayType == PLAY_TYPE_E.PLAY_TYPE_VOD)
                        {

                            if (currentSec >= 0)
                            {
                                ulRet = IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(SelectedPlayer.OnePlayer.ChannelCode), (uint)currentSec);


                                if (ulRet != 0)
                                {
                                    Logger.Error(GetType(), SelectedPlayer.CameraCode + ":设置播放时间失败" + ErrorCode.GetErrorMsg(ulRet));
                                }

                            }

                        }
                        else
                        {
                            if (SelectedPlayer?.CameraCode == null)
                            {
                                return;
                            }
                            string strCameCode = SelectedPlayer.CameraCode.PadRight(48, '\0');
                            List<URL_INFO_S> urlList = new List<URL_INFO_S>();
                            if (dtBegin < dtEnd)
                            {
                                List<RECORD_FILE_INFO_S> list = sdkManager.QueryRecord(beginTime, endTime, strCameCode);
                                if (list.Count > 0)
                                    urlList = sdkManager.GetRecordURL(list, strCameCode);
                            }


                            foreach (URL_INFO_S url in urlList)
                            {
                                SelectedPlayer.PlayVOD(url);

                                int n = 0;
                                while (SelectedPlayer.OnePlayer.ChannelCode == null)
                                {
                                    Thread.Sleep(100);
                                    if (n > 20)
                                        break;
                                    n++;
                                }

                                if (SelectedPlayer.OnePlayer.ChannelCode != null)
                                    ulRet = IMOSSDK.IMOS_SetPlayedTimeEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(SelectedPlayer.OnePlayer.ChannelCode), (uint)currentSec);

                                if (ulRet != 0)
                                {
                                    Logger.Error(GetType(), SelectedPlayer.CameraCode + ":设置播放时间失败" + ErrorCode.GetErrorMsg(ulRet));
                                }
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("未选中播放窗口！或者没有对应的摄像机！");
                    }

                }
                if (!VodTimer.IsEnabled)
                {
                    Thread.Sleep(200);
                    VodTimer.Start();
                }

            });

        }
    }

   
}
