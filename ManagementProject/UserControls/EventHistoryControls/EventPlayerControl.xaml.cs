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
        private string _handlingTime;
        /// <summary>
        /// 处理时间
        /// </summary>
        public string HandlingTime
        {
            get
            {
                return _handlingTime;
            }
            set
            {
                _handlingTime = value;
                NotifyPropertyChanged("HandlingTime");
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
        }
    }
    public class EventPlayerViewModel:EventPlayerModel
    {
        public DelegateCommand OpenContextMenuCommand { get; set; }
        public DelegateCommand RateSelectedCommand { get; set; }
        public DelegateCommand DownloadCommand { get; set; }
        public EventPlayerViewModel()
        {
            InitCommand();
            InitSelectionItem();
        }
        private void InitSelectionItem()
        {
            RateColor = "#FF818181";
            SelectedRate = "1.0X";
            SelectionItemCollection = new ObservableCollection<SelectionItem>();
            string[] NameList = { "4.0X", "2.0X", "1.0X", "-2.0X", "-4.0X" };
            foreach (var item in NameList)
            {
                SelectionItem st = new SelectionItem();
                st.Header = item;
                SelectionItemCollection.Add(st);
            }

        }
        private void InitCommand()
        {
            OpenContextMenuCommand = new DelegateCommand();
            OpenContextMenuCommand.ExecuteCommand = new Action<object>(OpenContextMenu);
            RateSelectedCommand = new DelegateCommand();
            RateSelectedCommand.ExecuteCommand = new Action<object>(RateSelected);
            DownloadCommand = new DelegateCommand();
            DownloadCommand.ExecuteCommand = new Action<object>(Download);
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
        }
        /// <summary>
        /// 下载历史视频
        /// </summary>
        /// <param name="obj"></param>
        private void Download(object obj)
        {

        }


    }
}
