using ManagementProject.UserControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementProject.Model
{
    public class AlarmPageModel : INotifyPropertyChangedClass
    {
        public ScenesList[] ScenesList { get; set; }
        public ScenesList CurrentScenes { get; set; }

        private int _mapId;
        public int MapId
        {
            get
            {
                return _mapId;
            }
            set
            {
                _mapId = value;
                NotifyPropertyChanged("MapId");
            }
        }

        private string _deviceCode;
        public string DeviceCode
        {
            get
            {
                return _deviceCode;
            }
            set
            {
                _deviceCode = value;
                NotifyPropertyChanged("MapId");
            }
        }



        public Dictionary<string, MapControl> MapDic = new Dictionary<string, MapControl>();
        private string _pageType;
        public string PageType
        {
            get
            {
                return _pageType;
            }
            set
            {
                _pageType = value;
                NotifyPropertyChanged("PageType");
                GetAlarmInfo(PageType);
            }
        }
     //   public string _ty



        private string _pageUrl;
        public string PageUrl
        {
            get
            {
                return _pageUrl;
            }
            set
            {
                _pageUrl = value;
                NotifyPropertyChanged("PageUrl");
            }
        }

        private object _page;
        public object Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value;
                NotifyPropertyChanged("Page");
            }
        }

        private ObservableCollection<Alarm> _alarmInfo;
        public ObservableCollection<Alarm> AlarmInfo
        {
            get
            {
                return _alarmInfo;
            }
            set
            {
                _alarmInfo = value;
                NotifyPropertyChanged("AlarmInfo");
            }
        }
        /// <summary>
        /// 获取报警信息列表（暂时写在model里）
        /// </summary>
        /// <param name="type"></param>
        public void GetAlarmInfo(string type)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetAlarmInfo;
            Alarm[] alarm= HttpAPi.GetAlarmInfo(url, type);
            alarm = alarm.Where(x => GlobalVariable.AlarmMapList.Contains(x.mapid.ToString())).ToArray();
            AlarmList = alarm;
            AlarmInfo = new ObservableCollection<Alarm>(alarm);
        }

        public Alarm SelectedAlarm { get; set; }
        public Alarm[] AlarmList { get; set; }
       
    }
}
