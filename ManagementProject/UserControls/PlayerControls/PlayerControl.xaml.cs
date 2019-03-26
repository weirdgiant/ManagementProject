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
            PlayerControlViewModel.PlaybackTime = "0000-00-00 00:00:00";
            this.DataContext = PlayerControlViewModel;
                
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
        }
        #endregion
    }

    public class PlayerControlViewModel : PlayerControlModel
    {
        public DelegateCommand StartCommand { get; set; }
        public DelegateCommand EndCommand { get; set; }
        public DelegateCommand OpenContextMenuCommand { get; set; }
        public DelegateCommand RateSelectedCommand { get; set; }
        public PlayerControlViewModel()
        {
            InitSelectionItem();
            PlayerControlInit();
            DelegateCommandInit();
        }

        private void InitSelectionItem()
        {
            RateColor = "#FF818181";
            SelectedRate = "1.0X";
            SelectionItemCollection = new ObservableCollection<SelectionItem>();
            string[] NameList = { "4.0X" , "2.0X", "1.0X", "-2.0X", "-4.0X" };
            foreach (var item in NameList)
            {
                SelectionItem st = new SelectionItem();
                st.Header = item;
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
        }

        /// <summary>
        /// 开始&暂停
        /// </summary>
        /// <param name="obj"></param>
        private void Start(object obj)
        {
            if (PlayerControlStatus == PlayerControlType.End)
            {
                PlayerControlStatus = PlayerControlType.Playback;
                StartBtToolTip = "暂停";
                PauseType();
            }
            else if (PlayerControlStatus == PlayerControlType.Playback )
            {
                PlayerControlStatus = PlayerControlType.Pause;
                StartBtToolTip = "播放";
                PlayBackType();
            }
            else if (PlayerControlStatus == PlayerControlType.Pause)
            {
                PlayerControlStatus = PlayerControlType.Playback;
                StartBtToolTip = "暂停";
                PauseType();
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
    }

   
}
