using ManagementProject.Converters;
using ManagementProject.UserControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class TrackManagementModel:INotifyPropertyChangedClass
    {
        #region Fields & Data Property 

        private int currentPageNumber = 1;
        private int pageDataCount = 20;
        private int totalDataCount;

        /// <summary>
        /// 选择页
        /// </summary>
        public int CurrentPageNumber
        {
            get { return currentPageNumber; }
            set
            {
                currentPageNumber = value;
                UpdatePageData();
                NotifyPropertyChanged("CurrentPageNumber");
            }
        }

        /// <summary>
        /// 每页可显示的数据
        /// </summary>
        public int PageDataCount
        {
            get { return pageDataCount; }
            set
            {
                pageDataCount = value;
                UpdatePageData();
                NotifyPropertyChanged("PageDataCount");
            }
        }

        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalDataCount
        {
            get { return totalDataCount; }
            set
            {
                totalDataCount = value;
                NotifyPropertyChanged("TotalDataCount");
            }
        }

        public ObservableCollection<ActivityManage> Datas { get; set; }


        #region 活动窗体属性
        private string trackerTitle;
        /// <summary>
        /// Addtracker 标题显示内容
        /// </summary>
        public string TrackerTitle
        {
            get { return trackerTitle; }
            set
            {
                trackerTitle = value;
                NotifyPropertyChanged("TrackerTitle");
            }
        }

        private string campus;
        /// <summary>
        /// 所属校区
        /// </summary>
        public string Campus
        {
            get { return campus; }
            set
            {
                campus = value;
                NotifyPropertyChanged("Campus");
            }
        }

        private string activityName;
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName
        {
            get { return activityName; }
            set
            {
                activityName = value;
                NotifyPropertyChanged("ActivityName");
            }
        }

        private string startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }

        private string endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }

        #endregion

        #endregion

        #region TrackerInfoControl
        private string _excelName;
        public string ExcelName
        {
            get
            {
                return _excelName;
            }
            set
            {
                _excelName = value;
                NotifyPropertyChanged("ExcelName");
            }
        }

        private string _trackerName;
        public string TrackerName
        {
            get
            {
                return _trackerName;
            }
            set
            {
                _trackerName = value;
                NotifyPropertyChanged("TrackerName");
            }
        }

        private string _trackerStartDate;
        public string TrackerStartDate
        {
            get
            {
                return _trackerStartDate;
            }
            set
            {
                _trackerStartDate = value;
                NotifyPropertyChanged("TrackerStartDate");
            }
        }

        private string _trackerStartTime;
        public string TrackerStartTime
        {
            get
            {
                return _trackerStartTime;
            }
            set
            {
                _trackerStartTime = value;
                NotifyPropertyChanged("TrackerStartTime");
            }
        }
        private string _trackerEndDate;
        public string TrackerEndDate
        {
            get
            {
                return _trackerEndDate;
            }
            set
            {
                _trackerEndDate = value;
                NotifyPropertyChanged("TrackerEndDate");
            }
        }

        private string _trackerEndTime;
        public string TrackerEndTime
        {
            get
            {
                return _trackerEndTime;
            }
            set
            {
                _trackerEndTime = value;
                NotifyPropertyChanged("TrackerEndTime");
            }
        }

        private ObservableCollection<TrackerInfo> _items;
        public ObservableCollection<TrackerInfo> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }
        #endregion

        #region Constructors


        public TrackManagementModel()
        {
            Datas = new ObservableCollection<ActivityManage>();

        }

        #endregion


        private ObservableCollection<School> _schoolList;
        public ObservableCollection<School> SchoolList
        {
            get
            {
                return _schoolList;
            }
            set
            {
                _schoolList = value;
                NotifyPropertyChanged("SchoolList");
            }
        }

        private School _selectedSchool;
        public School SelectedSchool
        {
            get
            {
                return _selectedSchool;
            }
            set
            {
                _selectedSchool = value;
                NotifyPropertyChanged("SelectedSchool");
            }
        }


        private ObservableCollection<TrackerCameraInfo> cameraLists;

        public ObservableCollection<TrackerCameraInfo> CameraLists
        {
            get { return cameraLists; }
            set
            {
                cameraLists = value;
                NotifyPropertyChanged("CameraLists");
            }
        }

     

        public void UpdatePageData()
        {
            try
            {
                if (Datas == null)
                    return;

                Datas.Clear();

                //EventHistory history;
                InitDate();
                TotalDataCount = Datas.Count;

                //根据选择页和每页可显示的数据计算当前应该添加的数据量
                int addDataCount = 0;
                if (CurrentPageNumber * PageDataCount <= TotalDataCount)
                {
                    addDataCount = PageDataCount;
                }
                else
                {
                    addDataCount = TotalDataCount - (CurrentPageNumber - 1) * PageDataCount;
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
            
            
        }
        /// <summary>
        /// model不做逻辑处理，后面做修改
        /// </summary>
        private void InitDate()
        {
            //Datas = new ObservableCollection<ActivityManage>();
            string url = AppConfig.ServerBaseUri + AppConfig.GetTracker;
            TrackEvent[] trackEvents = HttpAPi.GetTrackEvent(url, App.mango.getClientInfo().userId);
            if (trackEvents==null)
                return;
            foreach (var item in trackEvents)
            {
                ActivityManage act = new ActivityManage();
                act.ActivityId = item.id;
                act.ActivityName = item.name;
                act.Campus = item.mapId.ToString();
                act.MapId = item.mapId.ToString();
                act.DeviceIdList = item.deviceCameraIds;
                act.StartTime = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.startTime)).ToString("yyyy-MM-dd HH:mm:ss");
                act.EndTime = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.stopTime)).ToString("yyyy-MM-dd HH:mm:ss"); 
                Datas.Add(act);
            }
        }


    }

    /// <summary>
    /// 相机信息
    /// </summary>
    public class TrackerCameraInfo
    {
        public int Id { get; set; }
        /// <summary>
        /// 相机名称
        /// </summary>
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class TrackerInfo
    {
        public string Name { get; set; }
        public string Time { get; set; }
    }

    /// <summary>
    /// 活动管理
    /// </summary>
    public class ActivityManage
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 所属校区
        /// </summary>
        public string Campus { get; set; }
        /// <summary>
        /// 校区地图id
        /// </summary>
        public string MapId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 车辆数量
        /// </summary>
        public int VehiclesNumber { get; set; }
        /// <summary>
        /// 车牌号json
        /// </summary>
        public string CarNumber { get; set; }//{’carNumber’:’沪A1234’,’carOwner’:’姓名’,’carPhone’:’1234561231’}
        /// <summary>
        /// 设备id 
        /// </summary>
        public string DeviceIdList { get; set; }//1,2,3,4,5
    }

    /// <summary>
    /// 车辆信息
    /// </summary>
    public class Vehicles
    {
        /// <summary>
        /// 车牌
        /// </summary>
        public string carNumber { get; set; }

        /// <summary>
        /// 车主
        /// </summary>
        public string carOwner { get; set; }
        public string carPhone { get; set; }
    }

    public class CarRecord
    {
        public string Name { get; set; }
        public ObservableCollection<Vehicles> Date { get; set; }
    }

}
