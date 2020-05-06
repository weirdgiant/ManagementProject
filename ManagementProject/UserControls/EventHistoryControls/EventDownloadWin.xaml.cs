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
using System.Windows.Shapes;

namespace ManagementProject.UserControls.EventHistoryControls
{
    /// <summary>
    /// EventDownloadWin.xaml 的交互逻辑
    /// </summary>
    public partial class EventDownloadWin : Window
    {
        public EventDownloadWin()
        {
            InitializeComponent();
        }
    }

    public class EventDownloadWinModel:INotifyPropertyChangedClass
    {
        private string _tipText;
        public string TipText
        {
            get
            {
                return _tipText;
            }
            set
            {
                _tipText = value;
                NotifyPropertyChanged("TipText");
            }
        }

        private Visibility _isStart=Visibility.Visible;
        public Visibility IsStart
        {
            get
            {
                return _isStart;
            }
            set
            {
                _isStart = value;
                NotifyPropertyChanged("IsStart");
            }
        }
    }

    public class EventDownloadWinViewModel: EventDownloadWinModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ChannelList { get; set; }
        private StackPanel DownloadControlPanel { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand StartDownloadCommand { get; set; }
        public EventDownloadWinViewModel()
        {
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            StartDownloadCommand = new DelegateCommand();
            StartDownloadCommand.ExecuteCommand = new Action<object>(StartDownload);
            TipText = LoadText;
        }
        private string LoadText = "在下载视频起见，请不要离开本页面否则视为放弃下载。\n确定是否下载该事件的视频吗？";
        private string StartText = "正在下载视频！\n离开本页面视为放弃下载！";
        private string EndText = "下载完成!";
        private void Loaded(object obj)
        {
            EventDownloadWin downloadWin = (EventDownloadWin)obj;
            DownloadControlPanel = downloadWin.panel;
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="obj"></param>
        private void CloseWin(object obj)
        {
            EventDownloadWin downloadWin = (EventDownloadWin)obj;
            downloadWin.Close();
        }
        /// <summary>
        /// 确定下载
        /// </summary>
        /// <param name="obj"></param>
        private void StartDownload(object obj)
        {
            try
            {
                EventDownloadWin downloadWin = (EventDownloadWin)obj;
                DownloadControlPanel = downloadWin.panel;
                string[] channellist = ChannelList.Split(',');
                IsStart = Visibility.Collapsed;
                TipText = StartText;
                for (int i = 0; i < channellist.Length; i++)
                {
                    EventDownloadControlViewModel downloadControlViewModel = new EventDownloadControlViewModel();
                    downloadControlViewModel.StartTime = StartTime;
                    downloadControlViewModel.EndTime = EndTime;
                    downloadControlViewModel.Chanel = int.Parse(channellist[i]);
                    downloadControlViewModel.FileName = "文件" + (i + 1);
                    EventDownloadControl downloadControl = new EventDownloadControl();
                    downloadControl.DataContext = downloadControlViewModel;
                    DownloadControlPanel.Children.Add(downloadControl);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
           
           
        }
    }
}
