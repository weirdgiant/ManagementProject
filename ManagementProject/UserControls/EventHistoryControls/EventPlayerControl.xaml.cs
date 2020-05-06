using ManagementProject.UserControls.EventHistoryControls;
using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// EventPlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class EventPlayerControl : UserControl
    {
        public EventPlayerControl()
        {
            InitializeComponent();
        }
    }

    public class EventPlayerModel:INotifyPropertyChangedClass
    {
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

        private double _smallChange;
        public double SmallChange
        {
            get
            {
                return _smallChange;
            }
            set
            {
                _smallChange = value;
                NotifyPropertyChanged("SmallChange");
            }
        }

        private double _largeChange;
        public double LargeChange
        {
            get
            {
                return _largeChange;
            }
            set
            {
                _largeChange = value;
                NotifyPropertyChanged("LargeChange");
            }
        }

        private int _delay;
        public int Delay
        {
            get
            {
                return _delay;
            }
            set
            {
                _delay = value;
                NotifyPropertyChanged("Delay");
            }
        }

        private double _value;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged("Value");
            }
        }
        private double _maximum;
        public double Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
                NotifyPropertyChanged("Maximum");
            }
        }
        private double _minimum;
        public double Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;
                NotifyPropertyChanged("Minimum");
            }
        }

        private string _alarmTime;
        /// <summary>
        /// 报警时间
        /// </summary>
        public string AlarmTime
        {
            get
            {
                return _alarmTime;
            }
            set
            {
                _alarmTime = value;
                NotifyPropertyChanged("AlarmTime");
            }
        }
        private string _confirmTime;
        /// <summary>
        /// 处理时间
        /// </summary>
        public string ConfirmTime
        {
            get
            {
                return _confirmTime;
            }
            set
            {
                _confirmTime = value;
                NotifyPropertyChanged("ConfirmTime");
            }
        }

        private string _currentPlayTime;
        /// <summary>
        /// 当前播放时间
        /// </summary>
        public string CurrentPlayTime
        {
            get
            {
                return _currentPlayTime;

            }
            set
            {
                _currentPlayTime = value;
                NotifyPropertyChanged("CurrentPlayTime");
            }
        }

        private bool isOpen;
        /// <summary>
        /// 倍率打开状态
        /// </summary>
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
        /// <summary>
        /// 选中倍率
        /// </summary>
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
            public int Speed { get; set; }
        }
    }
    public class EventPlayerViewModel:EventPlayerModel
    {
        private Grid MyPanel { get; set; }
        private Grid MaxPanel { get; set; }
        private List<EventPlayback> EventPlayList { get; set; } = new List<EventPlayback>();
        public bool IsPlaying { get; set; }
        private System.Windows.Forms.PictureBox PlayWin { get; set; }
        private IntPtr mplayHandle;
        private readonly object syncobj = new object();
        private IntPtr windowhandle { get; set; }
        public DateTime StarTime;
        public DateTime EndTime;
        public string[] Channel { get; set; }
        public TimeSpan Span { get; set; }
        private readonly DispatcherTimer m_DispatcherTimer = new DispatcherTimer();
        private NET_TIME m_OsdTime;
        private NET_TIME m_OsdStartTime;
        private NET_TIME m_OsdEndTime;
        private int m_PlaySpeed;
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public DelegateCommand StartPlayCommand { get; set; }
        public DelegateCommand OpenContextMenuCommand { get; set; }
        public DelegateCommand RateSelectedCommand { get; set; }
        public DelegateCommand DownloadCommand { get; set; }
        public DelegateCommand SliderLeftButtonDownCommand { get; set; }
        public DelegateCommand SliderLeftButtonUpCommand { get; set; }
        public EventPlayerViewModel()
        {
            InitCommand();
            InitSelectionItem();
            StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (1).png";
        }

        private void M_DispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {

                NETClient.GetPlayBackOsdTime(EventPlayList[0].mplayHandle, ref m_OsdTime, ref m_OsdStartTime, ref m_OsdEndTime);
                // NETClient.GetPlayBackOsdTime(mplayHandle, ref m_OsdTime, ref m_OsdStartTime, ref m_OsdEndTime);
                var currentTime = m_OsdTime.ToDateTime() + TimeSpan.FromSeconds(1.199);
                CurrentPlayTime = currentTime.ToString();
                if (currentTime.Date == StarTime.Date)
                {
                    if (currentTime >= EndTime)//Current Time > End Time
                    {
                        m_OsdTime = NET_TIME.FromDateTime(EndTime);
                        foreach (var item in EventPlayList)
                        {
                            NVRSDKManager.Instance.StopPlayBack(ref item.mplayHandle);
                        }
                        // NVRSDKManager.Instance.StopPlayBack(ref mplayHandle);
                        m_DispatcherTimer.Stop();
                        //PlayControlButton.Content = play;
                        IsPlaying = false;
                        return;
                    }

                    var span = currentTime - StarTime;
                    Value = span.TotalSeconds;
                }
            }
            catch { }
        }

        private void InitSelectionItem()
        {
            RateColor = "#FF818181";
            SelectedRate = "1.0X";
            SelectionItemCollection = new ObservableCollection<SelectionItem>();
            string[] NameList = { "4.0X", "2.0X", "1.0X" };
            int[] SpeedList = { 6, 5, 4 };
            for (int i = 0; i < 3; i++)
            {
                SelectionItem st = new SelectionItem();
                st.Header = NameList[i];
                st.Speed = SpeedList[i];
                SelectionItemCollection.Add(st);
            }

        }
        private void InitCommand()
        {
            LoadCommand = new DelegateCommand();
            LoadCommand.ExecuteCommand = new Action<object>(Loaded);
            UnLoadCommand = new DelegateCommand();
            UnLoadCommand.ExecuteCommand = new Action<object>(UnLoaded);
            StartPlayCommand = new DelegateCommand();
            StartPlayCommand.ExecuteCommand = new Action<object>(StartPlay);
            OpenContextMenuCommand = new DelegateCommand();
            OpenContextMenuCommand.ExecuteCommand = new Action<object>(OpenContextMenu);
            RateSelectedCommand = new DelegateCommand();
            RateSelectedCommand.ExecuteCommand = new Action<object>(RateSelected);
            DownloadCommand = new DelegateCommand();
            DownloadCommand.ExecuteCommand = new Action<object>(Download);
            SliderLeftButtonDownCommand = new DelegateCommand();
            SliderLeftButtonDownCommand.ExecuteCommand = new Action<object>(SliderLeftButtonDown);
            SliderLeftButtonUpCommand = new DelegateCommand();
            SliderLeftButtonUpCommand.ExecuteCommand = new Action<object>(SliderLeftButtonUp);
        }

        private void StartPlay(object obj)
        {
            if (IsPlaying)
            {
                m_DispatcherTimer.Stop();
                foreach (var item in EventPlayList)
                {
                    NVRSDKManager.Instance.StopPlayBack(ref item.mplayHandle);
                }
                m_DispatcherTimer.Stop();
                IsPlaying = false;
                StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (2).png";

            }
            else
            {

                bool isPlay = false;
                foreach (var item in EventPlayList)
                {
                    NVRSDKManager.Instance.StopPlayBack(ref item.mplayHandle);
                    item.mplayHandle = NVRSDKManager.Instance.StartPlayBack(item.windowhandle, item.Channel, DateTime.Parse(CurrentPlayTime) , EndTime);
                    if (item.mplayHandle != IntPtr.Zero)
                    {
                        isPlay = isPlay || true;
                    }
                }
                if (isPlay)
                {
                    m_DispatcherTimer.Start();
                    IsPlaying = true;
                }
                StartIcon = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/暂停 (1).png";
            }
        }

        private void UnLoaded(object obj)
        {
            if (IsPlaying)
            {
                foreach (var item in EventPlayList)
                {
                    NVRSDKManager.Instance.StopPlayBack(ref item.mplayHandle);
                }
                m_DispatcherTimer.Stop();
                IsPlaying = false;

            }
        }
        private void SliderLeftButtonDown(object obj)
        {
            m_DispatcherTimer.Stop();
        }
        private void SliderLeftButtonUp(object obj)
        {
            var starTime = StarTime + TimeSpan.FromSeconds(Value);
            Task.Run(() =>
            {
                lock (syncobj)
                {
                    bool isPlay = false;
                    foreach (var item in EventPlayList)
                    {
                        NVRSDKManager.Instance.StopPlayBack(ref item.mplayHandle);
                        item.mplayHandle = NVRSDKManager.Instance.StartPlayBack(item.windowhandle, item.Channel, starTime, EndTime);
                        if (item.mplayHandle != IntPtr.Zero)
                        {
                            isPlay = isPlay || true;
                        }
                    }
                    if (isPlay)
                    {
                        m_DispatcherTimer.Start();
                        IsPlaying = true;
                    }

                }

            });
        }


        private void ShowPlaySpeed(ref int CurrentSpeed, int TotalSpeed)
        {
            bool result = false;
            int mode = TotalSpeed - CurrentSpeed;
            if (mode > 0)
            {
                if (CurrentSpeed >= 8)
                {
                    CurrentSpeed = 8;
                    return;
                }
                foreach (var item in EventPlayList)
                {
                     result = NVRSDKManager.Instance.PlayBackControl(item.mplayHandle, PlayBackType.Fast)|| result;

                   
                }
                if (result)
                    CurrentSpeed++;


            }
            else if (mode < 0)
            {

                if (CurrentSpeed <= 0)
                {
                    CurrentSpeed = 0;
                    return;
                }

                foreach (var item in EventPlayList)
                {
                     result = NVRSDKManager.Instance.PlayBackControl(item.mplayHandle, PlayBackType.Slow) || result;
                   
                }
                if (result)
                    CurrentSpeed--;

            }
            else if (mode == 0)
            {
                //if (CurrentSpeed == 4)
                //    return;
                //var result = NVRSDKManager.Instance.PlayBackControl(mplayHandle, PlayBackType.Normal);
                //if (result)
                //    CurrentSpeed = 4;
            }

        }

        private void Loaded(object obj)
        {
            EventPlayerControl eventPlayer = (EventPlayerControl)obj;
            MyPanel = eventPlayer.panel;
            MaxPanel= eventPlayer.maxPanel;

            int channelCount = Channel.Length;
            DrawPlayGrid(channelCount, MyPanel);
            for (int i=0;i< channelCount;i++)
            {
                EventPlayback playback = new EventPlayback();
                playback.playWindow.Tag = i;
                playback.playWindow.MouseDoubleClick += PlayWindow_MouseDoubleClick1; ;
                playback.Channel = int.Parse(Channel[i]);
                playback.Index = i;
                Grid.SetColumn(playback, i);
                MyPanel.Children.Add(playback);
                EventPlayList.Add(playback);
            }
            //PlayWin = eventPlayer.playWindow;
            //windowhandle = PlayWin.Handle;
            NvrPlayerInit();
           
        }

        private void PlayWindow_MouseDoubleClick1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Windows.Forms.PictureBox pic = (System.Windows.Forms.PictureBox)sender;
            int index = (int)pic.Tag;
            EventPlayback playback = EventPlayList[index];
            if (MaxPanel.Children.Count == 0)
            {
                MyPanel.Children.Remove(playback);
                MaxPanel.Children.Add(playback);
            }
            else
            {
                MaxPanel.Children.Remove(playback);
                Grid.SetColumn(playback, playback.Index);
                MyPanel.Children.Add(playback);

            }
        }




        private void DrawPlayGrid(int columnNumber, Grid grid)
        {
            grid.Children.Clear();

            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < columnNumber; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);

                grid.ColumnDefinitions.Add(cd);

            }
        }

        private void NvrPlayerInit()
        {
            StarTime = DateTime.Parse(AlarmTime);
            EndTime = DateTime.Parse(ConfirmTime);
            Span = EndTime - StarTime;          
            m_DispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            m_DispatcherTimer.Tick += M_DispatcherTimer_Tick;

            m_PlaySpeed = 4;
            SmallChange = 1;
            LargeChange = Span.TotalSeconds;
            Delay = 100;

            Maximum = Span.TotalSeconds;
            Minimum = 0;
            Value = 0;
            NVRPlayer_Loaded();
        }

        private void NVRPlayer_Loaded()
        {
            Task.Run(() =>
            {
                lock (syncobj)
                {
                    bool isPlay = false;
                    foreach (var item in EventPlayList)
                    {
                        item.mplayHandle = NVRSDKManager.Instance.StartPlayBack(item.windowhandle, item.Channel, StarTime, EndTime);
                        if (item.mplayHandle != IntPtr.Zero)
                        {
                            isPlay = isPlay || true;
                        }
                    }
                    if (isPlay)
                    {
                        m_DispatcherTimer.Start();
                        IsPlaying = true;
                    }
                }

            });

        }

        /// <summary>
        /// 打开倍率列表
        /// </summary>
        /// <param name="obj"></param>
        private void OpenContextMenu(object obj)
        {
            if (IsOpen)
            {
                IsOpen = false;
                RateColor = "#FF818181";
            }
            else
            {
                IsOpen = true;
                RateColor = "#FF1A8BDA";
            }
        }
        /// <summary>
        /// 倍率选择
        /// </summary>
        /// <param name="obj"></param>
        private void RateSelected(object obj)
        {
            SelectionItem item = (SelectionItem)obj;
            SelectedRate = item.Header;
            while (m_PlaySpeed!= item.Speed)
            {
                ShowPlaySpeed(ref m_PlaySpeed, item.Speed);
            }
            IsOpen = false;
           
        }
        /// <summary>
        /// 下载历史视频
        /// </summary>
        /// <param name="obj"></param>
        private void Download(object obj)
        {
            foreach (var item in EventPlayList)
            {
                NVRSDKManager.Instance.Download(item.Channel, StarTime, EndTime);
            }

               
        }


        enum PlaySpeed
        {
            MIN,                    // min speed             
            DOWN_16 = MIN,            // speed 1/16X
            DOWN_8,
            DOWN_4,
            DOWN_2,
            NORMAL,                 // normal speed
            UP_2,
            UP_4,
            UP_8,
            UP_16,
            MAX = UP_16,            // max speed
        }



    }
}
