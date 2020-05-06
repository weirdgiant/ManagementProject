using ManagementProject.Converters;
using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using ManagementProject.UserControls;
using ManagementProject.UserControls.PlayerControls;
using ManagementProject.UserControls.TrackPageControls;
using MangoApi;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.ViewModel
{
    public class TrackManagementViewModel : TrackManagementModel
    {
        private int _currentId;
        public int CurrentId
        {
            get
            {
                return _currentId;
            }
            set
            {
                _currentId = value;
                SetCurentActivity(_currentId);
            }
        }
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand OpenAddTrackerCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand CreatNewTrackerCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand CloseCameraSelectedWinCommand { get; set; }
        public DelegateCommand GetSelectedCameraCommand { get; set; }
        public DelegateCommand SelectedCommand { get; set; }
        public DelegateCommand UnSelectedCommand { get; set; }

        #region AddTrack Window Command
        public DelegateCommand AddWinCloseCommand { get; set; }
        public DelegateCommand SelectCameraCommand { get; set; }
        public DelegateCommand AddWinImportCommand { get; set; }
        public DelegateCommand AddWinConfirmCommand { get; set; }

        #endregion

        private string CameraListStr { get; set; } = "";
        private string CarNumberJson { get; set; } = "";
        public TrackPageViewModel trackPageViewModel;
        private ActivityManage _activity;
        private WrapPanel CameraPanel;
        private CameraSelectedWin cameraSelected;
        private TrackCameraWin trackCameraWin;
        private TrackManagement _trackManagement;
        public TrackManagementViewModel(TrackPageViewModel _trackPageViewModel )
        {
            trackPageViewModel = _trackPageViewModel;
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            OpenAddTrackerCommand = new DelegateCommand();
            OpenAddTrackerCommand.ExecuteCommand = new Action<object>(OpenAddTracker);
            AddWinCloseCommand = new DelegateCommand();
            AddWinCloseCommand.ExecuteCommand = new Action<object>(AddWinClose);
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteCommand = new Action<object>(Close);
            SelectCameraCommand = new DelegateCommand();
            SelectCameraCommand.ExecuteCommand = new Action<object>(SelectCamera);
            CreatNewTrackerCommand = new DelegateCommand();
            CreatNewTrackerCommand.ExecuteCommand = new Action<object>(CreatNewTracker);

            EditCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(Edit) };
            DeleteCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(Delete) };

            AddWinImportCommand = new DelegateCommand { ExecuteCommand = new Action<object>(ImportExcel) };


            CloseCameraSelectedWinCommand = new DelegateCommand();
            CloseCameraSelectedWinCommand.ExecuteCommand = new Action<object>(CloseCameraSelectedWin);
            GetSelectedCameraCommand = new DelegateCommand();
            GetSelectedCameraCommand.ExecuteCommand = new Action<object>(GetSelectedCamera);
            SelectedCommand = new DelegateCommand();
            SelectedCommand.ExecuteCommand = new Action<object>(Selected);
            UnSelectedCommand = new DelegateCommand();
            UnSelectedCommand.ExecuteCommand = new Action<object>(UnSelected);
            GetSchoolList();
            CameraLists = new ObservableCollection<TrackerCameraInfo>();
            // InitDate();
        }

        private void Loaded(object obj)
        {
            if (obj!=null)
            {
                TrackManagement management = (TrackManagement)obj;
                _trackManagement = management;
            }
            
        }

        private void UnSelected(object obj)
        {
            SelectedPlayer playerWin = (SelectedPlayer)obj;
            TrackerCameraInfo[] info= CameraLists.Where(x=>x.Code == playerWin.Code).ToArray ();
            if (info.Length !=0)
            {
                CameraLists.Remove(info[0]);
            }
          
        }
        private void Selected(object obj)
        {
            SelectedPlayer playerWin=(SelectedPlayer)obj;
            List<Device> list = MangoInfo.instance.CameraList;
            Device device = list.Where(x=>x.code ==playerWin .Code).ToArray ()[0];
            TrackerCameraInfo[] info = CameraLists.Where(x => x.Code == playerWin.Code).ToArray();
            if (info.Length == 0)
            {
                TrackerCameraInfo trackerCamera = new TrackerCameraInfo();
                trackerCamera.Id = device.id;
                trackerCamera.Code = device.code;
                trackerCamera.Name = device.name;
                CameraLists.Add(trackerCamera);
            }
           
        }

        private void GetSelectedCamera(object obj)
        {
            List<string> idList=new List<string> ();
           
            foreach (var item in CameraLists)
            {
                CameraPanel.Children.Add(CameraLabel(item .Name, item.Code));
                idList.Add(item .Id.ToString() );
            }
            CameraListStr = string.Join(",", idList);
            if (cameraSelected != null)
            {
                cameraSelected.Close();
            }
            if (trackCameraWin != null)
            {
                trackCameraWin.Close();
            }
           
        }

        private Label CameraLabel(string name,string code)
        {
            //LableStyle
            Label label = new Label
            {
                Content =name ,
                Tag =code,
                Style = Application.Current.FindResource("LableStyle") as Style,
            };
            return label;
        }

        private void CloseCameraSelectedWin(object obj)
        {
            if (cameraSelected!=null)
            {
                cameraSelected.Close();
            }
            if (trackCameraWin!=null)
            {
                trackCameraWin.Close();
            }
        }

       
        /// <summary>
        /// Addtracker选择摄像机
        /// </summary>
        /// <param name="obj"></param>
        private void SelectCamera(object obj)
        {
            cameraSelected = new CameraSelectedWin { DataContext = this ,Sid = SelectedSchool.SchoolId };
            cameraSelected.WindowStartupLocation = WindowStartupLocation.Manual;
            cameraSelected.Top = 50;
            cameraSelected.Left = 0;
            cameraSelected.Topmost = true;
            cameraSelected.Show();
            trackCameraWin = new TrackCameraWin() { DataContext = this };
            trackCameraWin.WindowStartupLocation = WindowStartupLocation.Manual;
            trackCameraWin.Owner = cameraSelected;
            trackCameraWin.Top = 50;
            trackCameraWin.Left = 1597;
            trackCameraWin.Topmost = true;
            trackCameraWin.Show();
           
        }

        private void SetCurentActivity(int id)
        {
            ActivityManage activity =Datas.Where(x=>x.ActivityId ==id).ToArray ()[0];
            TrackerName = activity.ActivityName;
            TrackerStartDate = DateTime.Parse(activity.StartTime).Date .ToString ("yyyy-MM-dd");
            TrackerStartTime = DateTime.Parse(activity.StartTime).TimeOfDay.ToString();
            TrackerEndDate = DateTime.Parse(activity.EndTime).Date.ToString("yyyy-MM-dd");
            TrackerEndTime = DateTime.Parse(activity.EndTime).TimeOfDay.ToString();
            InitTrackerItems(id);

        }

        private void InitTrackerItems(int id)
        {
            Items = new ObservableCollection<TrackerInfo>();
            string url = AppConfig.ServerBaseUri + AppConfig.GetTrackerCar;
            TracerCarInfo[] infos = HttpAPi.GetTrackerCarInfo(url,id);
            foreach (var item in infos)
            {
                TrackerInfo trackInfo = new TrackerInfo();
                trackInfo.Name = item.carNumber;
                trackInfo.Time = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.insertTime)).ToString(); 
                Items.Add(trackInfo);
            }
        }


        /// <summary>
        /// Addtracker导入Excel表格
        /// </summary>
        /// <param name="obj"></param>
        private void ImportExcel(object obj)
        {
            OpenFileDialog openfile = new OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = ".xlsx|*.xlsx|.xls|*.xls"
            };

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                var excelFilePath = openfile.FileName;

                string excelConnectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={excelFilePath};Extended Properties=Excel 12.0;Persist Security Info=True";

                var vehicles =ReadRecordFromEXCELAsync(excelConnectionString).Result;
                CarRecord record = new CarRecord();
                record.Date = vehicles;
                record.Name = openfile.SafeFileName;
                CarNumberJson = JsonConvert.SerializeObject(record.Date);
                ExcelName = openfile.SafeFileName;
            }
        }

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="obj"></param>
        private void Delete(object obj)
        {
            if (obj == null)
                return;

            ActivityManage activity = (ActivityManage)obj;
            string url = AppConfig.ServerBaseUri + AppConfig.DeleteTracker;
            HttpAPi.DeleteTrackEvent(url, activity);
            //activity.ActivityId 
            UpdatePageData();
            trackPageViewModel._trackTabControl.ReloadTab();
        }



        /// <summary>
        /// 编辑活动
        /// </summary>
        /// <param name="obj"></param>
        private void Edit(object obj)
        {
            try
            {
                TrackerTitle = "编辑活动";

                if (obj == null)
                    return;

                _activity = (ActivityManage)obj;
                ActivityName = _activity.ActivityName;
                StartTime = _activity.StartTime;
                EndTime = _activity.EndTime;
                Campus = _activity.Campus;
                if (_activity.DeviceIdList!=null)
                {
                    string[] IdList = _activity.DeviceIdList.Replace(" ", "").Split(',');
                    GetCameraList(IdList);
                }

                AddTracker addTracker = new AddTracker { DataContext = this };
                addTracker.Loaded += AddTracker_Loaded;
                addTracker.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                CameraPanel = addTracker.wrapPanel;
                addTracker.Owner = _trackManagement;
                addTracker.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        private void GetCameraList(string[] IdList)
        {
            CameraLists.Clear();
            foreach (var item in IdList)
            {
                int cameraid = int.Parse(item);
                List<Device> list = MangoInfo.instance.CameraList;
                Device device = list.Where(x => x.id == cameraid).ToArray()[0];

                TrackerCameraInfo trackerCamera = new TrackerCameraInfo();
                trackerCamera.Id = device.id;
                trackerCamera.Code = device.code;
                trackerCamera.Name = device.name;
                CameraLists.Add(trackerCamera);
            }

        }

        private void AddTracker_Loaded(object sender, RoutedEventArgs e)
        {
            CameraPanel.Children.Clear();
            foreach (var item in CameraLists)
            {
                CameraPanel.Children.Add(CameraLabel(item.Name, item.Code));
            }
        }

        /// <summary>
        /// 打开AddTracker 添加活动
        /// </summary>
        /// <param name="obj"></param>
        private void OpenAddTracker(object obj)
        {
            try
            {
                TrackerTitle = "添加活动";
                ClearActivityInfo();

                AddTracker addTracker = new AddTracker { DataContext = this };
                addTracker.Owner = _trackManagement;
                addTracker.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                CameraPanel = addTracker.wrapPanel;
                addTracker.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger .Error (ex);
            }
        }

        private void ClearActivityInfo()
        {
            ExcelName = "";
            CarNumberJson = "";
            Campus = "";
            ActivityName = "";
            StartTime = "";
            EndTime = "";//ok
        }

        private void GetSchoolList()
        {
            SchoolList = new ObservableCollection<School>();
            string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
            MangoMap[] map = HttpAPi.GetMapList(url);
            MangoMap[] results = map.Where(x => x.pid == 0).ToArray();
            string[] sidlist = GlobalVariable.SidList;
            if (sidlist == null) return;
            foreach (var item in sidlist)
            {
                MangoMap[] ret = results.Where(x => x.id == int.Parse(item)).ToArray();
                if (ret.Length > 0)
                {
                    School school = new School();
                    school.SchoolName = ret[0].name;
                    school.SchoolId = ret[0].id;
                    school.Contact = ret[0].securityPerson;
                    school.Phone = ret[0].securityPhone;
                    school.Discription = ret[0].description;
                    SchoolList.Add(school);
                }
            }
            SelectedSchool = SchoolList[0];
        }


        /// <summary>
        /// 关闭TrackManagement
        /// </summary>
        /// <param name="obj"></param>
        private void Close(object obj)
        {
            TrackManagement tr = (TrackManagement)obj;
            tr.Close();
        }
        /// <summary>
        /// 关闭AddTracker
        /// </summary>
        /// <param name="obj"></param>
        private void AddWinClose(object obj)
        {
            AddTracker win = (AddTracker)obj;
            win.Close();
            CarNumberJson = "";
            CameraListStr = "";
        }
       

        /// <summary>
        /// Addtracker确定
        /// </summary>
        /// <param name="obj"></param>
        private async void CreatNewTracker(object obj)
        {
            try
            {
                AddTracker win = (AddTracker)obj;

                //
                // 处理数据    
                //

                if (TrackerTitle.Equals("添加活动"))
                {
                    var result = await AddNew();
                    if (result)
                    {
                        UpdatePageData();
                    }

                }
                else if (TrackerTitle.Equals("编辑活动"))
                {
                    if (_activity != null)
                    {
                        _activity.ActivityName = ActivityName;
                        _activity.StartTime = StartTime;
                        _activity.EndTime = EndTime;
                        _activity.Campus = Campus;
                        _activity.MapId= SelectedSchool.SchoolId.ToString();
                        _activity.CarNumber = CarNumberJson;
                        _activity.DeviceIdList = CameraListStr;
                        //RefreshDatas(_activity);
                        //Datas;

                        string url = AppConfig.ServerBaseUri + AppConfig.UpdateTracker;
                        var trackResult = HttpAPi.UpdateTrackEvent(url, _activity);
                    }
                }

                win.Close();
                CarNumberJson = "";
                CameraListStr = "";
                CameraLists = new ObservableCollection<TrackerCameraInfo>();
                trackPageViewModel._trackTabControl.ReloadTab();
            }
            catch (Exception ex)
            {

                Logger .Error (ex);
            }
            
        }

        private async Task<bool> AddNew()
        {
            return await Task.Run(() =>
             {
                 string url = AppConfig.ServerBaseUri + AppConfig.InsertTracker;
                 ActivityManage manage = new ActivityManage();
                 manage.MapId = SelectedSchool.SchoolId.ToString();
                 //manage.Campus = Campus;
                 manage.Campus = SelectedSchool.SchoolName;
                 manage.StartTime = Convert.ToDateTime(StartTime).ToString("yyyy-MM-dd HH:mm:ss");
                 manage.EndTime = Convert.ToDateTime(EndTime).ToString("yyyy-MM-dd HH:mm:ss");
                 manage.ActivityName = ActivityName;
                 manage.CarNumber = CarNumberJson;
                 manage.DeviceIdList = CameraListStr;
                 manage.State = 1;

                 if (Convert.ToDateTime(StartTime)> Convert.ToDateTime(EndTime))
                 {
                     MessageBox.Show("开始时间大于结束时间！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                     return false;
                 }
                 TrackEvent[] track = HttpAPi.InsertTrackEvent(url, manage);
                 if (track == null)
                 {
                     return false;
                 }
                 return true;
             });
        }

        /// <summary>  
        /// Method to Get All the Records from Excel  
        /// </summary>  
        /// <returns></returns>  
        public async Task<ObservableCollection<Vehicles>> ReadRecordFromEXCELAsync(string connectionString)
        {
            ObservableCollection<Vehicles> Vehicles = new ObservableCollection<Vehicles>();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand("Select * from [Sheet1$]", connection);
                try
                {
                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        Vehicles.Add(new Vehicles
                        {
                            carNumber = reader["车牌"].ToString(),
                            carOwner = reader["车主"].ToString(),
                        });
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(typeof(TrackManagementViewModel),ex);//未在本地计算机上注册“Microsoft.ACE.OLEDB.12.0”提供程序。
                }
            }

            return Vehicles;
        }
    }
}
