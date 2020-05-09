using ManagementProject.Converters;
using ManagementProject.Model;
using ManagementProject.UserControls;
using ManagementProject.UserControls.EventHistoryControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class EventHistoryViewModel : EventHistoryModel
    {
        private EventHistory EventHistoryWin { get; set; } 
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }

        public DelegateCommand DownloadCommand { get; set; }
        public DelegateCommand OpenFileCommand { get; set; }
        public DelegateCommand MouseDoubleClickCommand { get; set; }

        public EventHistoryViewModel()
        {
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteCommand = new Action<object>(Close);
            SearchCommand = new DelegateCommand();
            SearchCommand.ExecuteCommand = new Action<object>(Search);
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);

            DownloadCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(DownloadFile) };
            OpenFileCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(OpenFile) };
            MouseDoubleClickCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(MouseDoubleClick) };

            AlarmHistory = new ObservableCollection<History>();
        }


        /// <summary>
        /// 双击某个事件的报警设备名称，播放该事件的录像。
        /// </summary>
        /// <param name="obj">选中对象</param>
        private void MouseDoubleClick(object obj)
        {
            if (obj!=null)
            {
                History history = (History)obj;
                string mapid = history.MapId;
                int flag = MangoApp.getInstance().getClientInfo().flag;
                string channel = GetChannelList(mapid,flag);               

                if (channel=="")
                {
                    MessageBox.Show("获取查询通道失败，请设置查询通道！");
                    return;
                }
                DateTime start = DateTime.Parse(history.Time);
                DateTime end = DateTime.Parse(history.ConfirmTime);

                if (!IsPlayBack(start, end, channel))
                {
                    MessageBox.Show("当前时间点暂无回放！ ！");
                    return;
                }
                string[] channelList = channel.Split(',');
                EventPlayerControl pc = new EventPlayerControl();
                EventPlayerViewModel pcv = new EventPlayerViewModel { AlarmTime = history.Time, ConfirmTime = history.ConfirmTime,Channel = channelList };
                pc.DataContext = pcv;
                Window eventPlayback = new Window();
                eventPlayback.Topmost = true;
                eventPlayback.Content = pc;
                eventPlayback.Show();
            }
        }

        private string GetChannelList(string mapid,int flag)
        {
            string channel = "";
            if (flag == 1)
            {
                List<ClientConfig> ClientConfigs = MangoInfo.instance.ClientConfigs.Where(x => x.clientProperty != 1).ToList();
                if (ClientConfigs.Count != 0)
                {
                    foreach (var item in ClientConfigs)
                    {
                        if (item.jurisdiction == "") continue;
                        string[] range = item.jurisdiction.Split(',');
                        if (range.Contains(mapid))
                        {
                            channel = item.channel_number;
                            break;
                        }
                    }
                    if (channel == "")
                    {
                        channel = GlobalVariable.CurrenClientConfig.channel_number;
                    }
                }
                else
                {
                    channel = GlobalVariable.CurrenClientConfig.channel_number;
                }

            }
            else
            {
                channel = GlobalVariable.CurrenClientConfig.channel_number;
            }
            return channel;

        }

        private bool IsPlayBack(DateTime start, DateTime end,string chanel)
        {
            string[] channelList = chanel.Split(',');
            bool isPlay = false;
            foreach (var item in channelList)
            {
                isPlay= NVRSDKManager.Instance.QueryPlayBack(start, end, int.Parse(item))|| isPlay;
            }
            return isPlay;
        }

        

        private void Loaded(object obj)
        {
            if (obj != null)
            {
                EventHistory historyWin =(EventHistory)obj;
                EventHistoryWin = historyWin;
            }
            string[] alarmStateList = { "所有状态", "真实报警", "确认误报", "设备故障", "模拟报警" };//
            AlarmLevelList = new ObservableCollection<string>();
            AlarmLevelList.Add("所有级别");
            AlarmTypeInfoList = new ObservableCollection<string>();
            AlarmTypeInfoList.Add("所有类型");
            AlarmStateList = new ObservableCollection<string>(alarmStateList);           
            foreach (var item in MangoInfo .instance .AlarmLevels)
            {
                string level = item.listName;
                AlarmLevelList.Add(level);
            }
            foreach (var item in MangoInfo.instance.AlarmTypeInfos)
            {
                string type = item.listName;
                AlarmTypeInfoList.Add(type);
            }
            InitHistory();
           // EventStartTime= DateTime.Now.AddDays(-1).ToShortDateString();
        }

        private void InitHistory()
        {
            EventStartTime= DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
            EventEndTime= DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            AlarmFilter alarmFilter = new AlarmFilter();
            alarmFilter.altimeStart = DateTime.Now.Date.ToString();
            alarmFilter.altimeStop = DateTime.Now.ToString();
            alarmFilter.level = GetSelectedLevel(AlarmLevel);
            alarmFilter.flag = GetSelectAlarmType(AlarmType);
            alarmFilter.state = GetSelectAlarmState(AlarmState);
            alarmFilter.sid = 0;
            SearchHistory(alarmFilter);

            //是否显示下载按钮
            var hasPath = ExistsFile(saveUrl);
            if (hasPath)
                IsShowFolder = true;
            else
                IsShowDownloadButton = true;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="obj"></param>
        private void Search(object obj)
        {
            AlarmFilter alarmFilter = new AlarmFilter();
            alarmFilter.altimeStart = EventStartTime;
            alarmFilter.altimeStop = EventEndTime;
            alarmFilter.level = GetSelectedLevel(AlarmLevel);
            alarmFilter.flag = GetSelectAlarmType(AlarmType);
            alarmFilter.state = GetSelectAlarmState(AlarmState);
            alarmFilter.sid = 0;
            SearchHistory(alarmFilter);
        }

        private void SearchHistory(AlarmFilter alarmFilter)
        {
            AlarmHistory.Clear();
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllAlarmInfo;
            Alarm[] alarms = HttpAPi.GetAllAlarmInfo(url, alarmFilter);
            if (alarms==null)
            {
                return;
            }
            alarms = alarms.Where(x => x.confirmtime !=null).ToArray();

            if (AlarmState== "模拟报警" && GlobalVariable.IsFake)
            {
                alarms = alarms.Where(x=>x.fake).ToArray();
            }else if (AlarmState == "模拟报警" && !GlobalVariable.IsFake)
            {
                alarms = null;
            }else if (AlarmState != "所有状态"&&AlarmState != "模拟报警" && GlobalVariable.IsFake)
            {
                alarms = alarms.Where(x => !x.fake).ToArray();
            }
            if (alarms != null)
            {
                try
                {
                    foreach (var item in alarms)
                    {
                        if (GetAlarmState(item).ToString() == "未处理")
                        {
                            continue;
                        }
                        if (!GlobalVariable.AlarmMapList.Contains(item.mapid.ToString()))
                        {
                            continue;
                        }
                        try
                        {
                            History history = new History();
                            history.Id = item.id;
                            history.AlarmSignal = GetSignalName(item .flag.Trim ()+"-"+item.type);
                            history.Level = item.level.ToString();
                            history.Location = item.location;
                            history.State = GetAlarmState(item).ToString();
                            history.Time = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.altime)).ToString("yyyy-MM-dd HH:mm:ss");
                            history.ConfirmTime = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.confirmtime)).ToString("yyyy-MM-dd HH:mm:ss");
                            history.Type = item.flagVal;
                            history.Device = item.sersorname;
                            history.MapId = item.mapid.ToString();
                            history.ConfirmNote = item.confirmnote;
                            AlarmHistory.Add(history);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                       
                    }
                }
                catch (Exception ex)
                {

                    Logger.Error(ex);
                }
               
                UpdatePageData();
                //AlarmHistory = new ObservableCollection<History>(alarms);
            }else
            {
                UpdatePageData();
            }
        }

        public string GetSignalName(string signal_id)
        {
            string name ="";
            try
            {
                name=GlobalVariable.Signals.Where(x => x.signal_id == signal_id).ToArray()[0].signal_name;
            }
            catch(Exception)
            {

            }
            return name;
        }

        public string GetAlarmState(Alarm alarm)
        {
            com.mango.protocol.Enum.ALARM_STATE STATE = alarm.state;
            if (alarm.fake && GlobalVariable.IsFake)
            {
                return "模拟报警";
            }
            switch (STATE)
            {
                case com.mango.protocol.Enum.ALARM_STATE.NEW:
                    return "未处理";
                case com.mango.protocol.Enum.ALARM_STATE.ADEVICE_ERROR:
                    return "设备故障";
                case com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_MISTAKE:
                    return "确认误报";
                case com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_REAL:
                    return "真实报警";
                case com.mango.protocol.Enum.ALARM_STATE.FIXED:
                    return "自动处理";
                default:
                    return "模拟报警";
            }
        }

        private string GetSelectedLevel(string level)
        {
            if (level!="所有级别")
            {
                List<AlarmLevel> alarmLevel = MangoInfo.instance.AlarmLevels.Where(AlarmLevel => AlarmLevel.listName == level).ToList();
                return alarmLevel[0].listValue;
            }
            return null;           
        }

        private string GetSelectAlarmType(string type)
        {
            if(type!= "所有类型")
            {
                List<AlarmTypeInfo> alarmType = MangoInfo.instance.AlarmTypeInfos.Where(AlarmTypeInfo => AlarmTypeInfo.listName == type).ToList();
                return alarmType[0].listValue;
            }
            return null;
        }
        private string GetSelectAlarmState(string state)
        {
            if (state!= "所有状态")
            {
                switch (state)
                {
                    case "未处理" :
                        return com.mango.protocol.Enum.ALARM_STATE.NEW.GetHashCode().ToString();
                    case "设备故障" :
                        return com.mango.protocol.Enum.ALARM_STATE.ADEVICE_ERROR.GetHashCode().ToString();
                    case "确认误报":
                        return com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_MISTAKE.GetHashCode().ToString();
                    case "真实报警":
                        return com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_REAL.GetHashCode().ToString();
                    case "自动处理":
                        return com.mango.protocol.Enum.ALARM_STATE.FIXED.GetHashCode().ToString();
                    default:
                        return null;
                }
            }
            return null;
        }

        private void Close(object obj)
        {
            if (obj != null)
            {
                EventHistory _eventHistory = (EventHistory)obj;
                _eventHistory.Close();
            }
        }

        #region 下载控件

        private string httpFileUrl = "https://download.microsoft.com/download/2/4/3/24375141-E08D-4803-AB0E-10F2E3A07AAA/AccessDatabaseEngine.exe";

        private static readonly string baseDic = AppDomain.CurrentDomain.BaseDirectory;

        private string saveUrl = $"{baseDic}报警时间_报警类型_报警源名称.mp4";//报警时间_报警类型_报警源名称.mp4

        private bool ExistsFile(string saveUrl)
        {
            return File.Exists(saveUrl);
        }

        private void OpenFile(object obj)
        {
            //var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            //{
            //    Filter = "视频文件 (*.mp4)|*.mp4",
            //};
            //var result = openFileDialog.ShowDialog();
            //if (result == true)
            //{
            //    var fileName = openFileDialog.FileName;
            //}
            DateTime start = DateTime.Now.AddDays(-2);
            DateTime end = DateTime.Now.AddDays(-1);
            if (!NVRSDKManager.Instance.QueryPlayBack(start, end, 1))
            {
                MessageBox.Show("当前时间点暂无回放！请稍后查看！");
                return;
            }
            NVRSDKManager.Instance.Download(1, start, end);

        }

        private void DownloadFile(object obj)
        {
            if (obj != null)
            {
                History history = (History)obj;
                string mapid = history.MapId;
                int flag = MangoApp.getInstance().getClientInfo().flag;
                string channel = GetChannelList(mapid, flag);
                DateTime start = DateTime.Parse(history.Time);
                DateTime end = DateTime.Parse(history.ConfirmTime);
                EventDownloadWinViewModel downloadWinViewModel = new EventDownloadWinViewModel() { StartTime =start,EndTime =end ,ChannelList =channel};

                EventDownloadWin downloadWin = new EventDownloadWin();
                downloadWin.Owner = EventHistoryWin;               
                downloadWin.DataContext = downloadWinViewModel;
                downloadWin.ShowDialog();
            }
        }

        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        public void DownloadHttpFile(String http_url, String save_url)
        {
            WebResponse response = null;
            //获取远程文件
            WebRequest request = WebRequest.Create(http_url);
            response = request.GetResponse();
            if (response == null) return;
            //读远程文件的大小
            PbMax = response.ContentLength;
            //下载远程文件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                using (Stream netStream = response.GetResponseStream())
                {
                    try
                    {
                        using (Stream fileStream = new FileStream(save_url, FileMode.Create))
                        {
                            byte[] read = new byte[1024];
                            long progressBarValue = 0;
                            int realReadLen = netStream.Read(read, 0, read.Length);
                            while (realReadLen > 0)
                            {
                                fileStream.Write(read, 0, realReadLen);
                                progressBarValue += realReadLen;
                                SetProgressBar(progressBarValue);
                                realReadLen = netStream.Read(read, 0, read.Length);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(typeof(EventHistoryViewModel), ex.Message);
                    }
                }
            }, null);
        }

        public void SetProgressBar(double value)
        {
            //显示进度条
            PbValue = value;
            //显示百分比
            LabValue = Convert.ToInt64((value / PbMax) * 100) + "%";
        }
        #endregion
    }
}
