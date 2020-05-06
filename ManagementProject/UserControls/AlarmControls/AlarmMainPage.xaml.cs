using ManagementProject.FunctionalWindows;
using ManagementProject.UserControls.AlarmControls;
using ManagementProject.ViewModel;
using MangoApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// AlarmMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmMainPage : UserControl, IDisposable
    {
        public int SubWinIndex { get; set; } = -1;
        private List<Player> Players = new List<Player>();
        private ClientConfig CurrenClientConfig { get; set; }
        private AlarmModule[] alarmModules { get; set; }
        private AlarmModuleConfig[] alarmModuleConfigs { get; set; }
        private ScenesList[] ScenesList { get; set; }
        private Signal CurrentSignal { get; set; }
        private MainWindow _main = (MainWindow)Application.Current.MainWindow;
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public AlarmPageViewModel AlarmPageViewModel { get; set; }
        private Dictionary<int, Grid> GridDic = new Dictionary<int, Grid>();
        private MapControl CurrentMap { get; set; }
        private void dispose()
        {
            //CurrentMap.Dispose();
            //CurrentMap = null;
            CurrenClientConfig = null;
            alarmModules = null;
            alarmModuleConfigs = null;
            ScenesList = null;
            _main = null;
            MainWindowViewModel = null;
            AlarmPageViewModel = null;
            panel.Children.Clear();
            GridDic.Clear();
        }
        public AlarmMainPage()
        {
            InitializeComponent();
            MainWindowViewModel = (MainWindowViewModel)_main.DataContext;
            AlarmPageViewModel = MainWindowViewModel.alarmPageViewModel;
            //DataContext = AlarmPageViewModel;
            //CurrenClientConfig = GlobalVariable.CurrenClientConfig;
            LoadConfig();
            alarmModules = GlobalVariable.AlarmModules;
            alarmModuleConfigs = GlobalVariable.AlarmModuleConfigs;
            GetConfig(AlarmPageViewModel);
            AlarmPageViewModel.ScenesList = ScenesList;
            Loaded += AlarmMainPage_Loaded;
            Unloaded += AlarmMainPage_Unloaded;
        }

        private void LoadConfig()
        {
            int clientid = App.mango.getClientInfo().userId;
            string url = AppConfig.ServerBaseUri + AppConfig.GetAllClientConfig;
            ClientConfig[] clientConfigs = HttpAPi.GetAllClientInfo(url);
            if (clientConfigs == null) return;
            ClientConfig[] clientConfig = clientConfigs.Where(x => x.id == clientid).ToArray();
            if (clientConfig.Length == 0)
            {
                return;
            }
            CurrenClientConfig = clientConfig[0];
        }

        private void AlarmMainPage_Unloaded(object sender, RoutedEventArgs e)
        {

            if (Players.Count != 0)
            {
                foreach (var item in Players)
                {
                    item.StopPlay();
                }
            }
            Players.Clear();

            if (MainWindowViewModel.PlayerWindow != null)
            {
                MainWindowViewModel.PlayerWindow.Close();
            }
            if (playerWindow != null)
            {
                playerWindow.Close();
            }
            Dispose();
        }

        private void AlarmMainPage_Loaded(object sender, RoutedEventArgs e)
        {
            SetControl();
        }

        public void SetControl()
        {
            try
            {
                if (ScenesList.Where(x => x.status == 1).ToArray().Length > 0)
                {
                    ScenesList scenes = ScenesList.Where(x => x.status == 1).ToArray()[0];
                    if (scenes.pageLayoutList.Length > 0)
                    {
                        int layoutid = int.Parse(scenes.pageLayoutList[0].windowId.Last().ToString());
                        GetLayout(layoutid);
                        foreach (var item in scenes.pageLayoutList)
                        {
                            try
                            {
                                AlarmModule module = alarmModules.Where(x => x.id == item.module).ToArray()[0];
                                int cid = int.Parse(item.componentId.Last().ToString());
                                if (cid != 0)
                                {
                                    Grid grid = GridDic[cid];
                                    InitAlarmControl(module.module_content, grid);
                                }
                            }catch (Exception ex)
                            {
                                Logger.Error(typeof(AlarmMainPage), "SetControl:" + ex.Message);
                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AlarmMainPage), "SetControl:" + ex.Message);
            }
        }

        private void InitAlarmControl(string name, Grid grid)
        {
            try
            {
                int sid = AlarmPageViewModel.SelectedAlarm.sid;
                int mapid = AlarmPageViewModel.SelectedAlarm.mapid;
                switch (name)
                {
                    case "Map":
                        if (mapid == 0)
                        {
                            break;
                        }
                        Map(AlarmPageViewModel.SelectedAlarm, grid);
                        break;
                    case "ArchiMap":
                        if (sid == 0)
                        {
                            break;
                        }
                        SchoolMap(AlarmPageViewModel.SelectedAlarm, grid);
                        break;
                    case "Plan":
                        //InitDoorControl(grid);
                        DisposalPlan plan = disposalPlan();
                        grid.Children.Add(plan);
                        break;
                    case "WaterChart":
                        WaterPressureMonitor waterPressure = new WaterPressureMonitor();
                        WaterPreMonitorViewModel waterPreMonitorViewModel = new WaterPreMonitorViewModel();
                        waterPreMonitorViewModel.LoadWaterPressure(mapid);
                        waterPreMonitorViewModel.LoadLineChart(waterPreMonitorViewModel.WaterPress[0].DeviceCode, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        waterPressure.DataContext = waterPreMonitorViewModel;
                        grid.Children.Add(waterPressure);
                        break;
                    case "WaterList":
                        MonitorEquipment monitorWater = new MonitorEquipment();
                        MonitorEquipmentViewModel monitorWaterVm = new MonitorEquipmentViewModel();
                        monitorWaterVm.GetWaterDevice(sid, AlarmPageViewModel.AlarmList);
                        monitorWater.DataContext = monitorWaterVm;
                        grid.Children.Add(monitorWater);
                        break;
                    case "GasList":
                        MonitorEquipment monitorGas = new MonitorEquipment();
                        MonitorEquipmentViewModel monitorGasVm = new MonitorEquipmentViewModel();
                        monitorGasVm.GetGasDevice(AlarmPageViewModel.SelectedAlarm.mapid, AlarmPageViewModel.SelectedAlarm.sid, AlarmPageViewModel.AlarmList);
                        monitorGas.DataContext = monitorGasVm;
                        grid.Children.Add(monitorGas);
                        break;
                    case "CarMap":
                        if (mapid == 0)
                        {
                            break;
                        }
                        Map(AlarmPageViewModel.SelectedAlarm, grid);
                        // Map(AlarmPageViewModel.SelectedAlarm,grid);
                        // AlarmPageViewModel.MapDic.Add("CarMap", Map(AlarmPageViewModel.SelectedAlarm));
                        break;
                    case "CarInfo":
                        AlarmCarInfo carInfo = new AlarmCarInfo();
                        AlarmCarInfoViewModel alarmCarInfoViewModel = new AlarmCarInfoViewModel(AlarmPageViewModel);
                        carInfo.DataContext = alarmCarInfoViewModel;
                        grid.Children.Add(carInfo);
                        break;
                    case "CamCorder":
                        InitCamCorder(grid);
                        break;
                    case "EscapeCamera":
                        InitEscapeCamCorder(grid);
                        break;
                    case "ElevatorCamera":
                        InitElevatorCamera(grid);
                        break;
                    case "FireEscape":
                        EscapeMap(AlarmPageViewModel.SelectedAlarm, grid);
                        break;
                    case "OutDoorTracker":
                        TrackerMap(AlarmPageViewModel.SelectedAlarm, grid);
                        break;
                    case "InDoorTracker":
                        TrackerMap(AlarmPageViewModel.SelectedAlarm, grid);
                        break;
                    case "ElectricDeviceList":
                        InitElectricDeviceList(grid);
                        break;
                    case "DoorControl":
                        InitDoorControl(grid);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AlarmMainPage), "InitAlarmControl:" + ex.Message);
            }
        }

        #region 门禁联动组件

        private AlarmDoorControl alarmDoorControl;
        /// <summary>
        /// 门禁联动组件
        /// </summary>
        /// <param name="grid"></param>
        private void InitDoorControl(Grid grid)
        {
            alarmDoorControl = new AlarmDoorControl();
            alarmDoorControl.Code = AlarmPageViewModel.SelectedAlarm.sersor;
            grid.Children.Add(alarmDoorControl);
        }

        public void ChangeAccessState(bool state,string code)
        {
            try
            {
                if (alarmDoorControl != null)
                {
                    foreach (var item in alarmDoorControl.panel.Children)
                    {
                        if (item.GetType() == typeof(AlarmDoorItem))
                        {
                            AlarmDoorItem child = (AlarmDoorItem)item;
                            if (child.Code == code)
                            {
                                child.SetStatus(state);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }

        }

        #endregion


        #region 组件初始化

        private void TrackerMap(Alarm alarm, Grid grid)
        {
            int sid = AlarmPageViewModel.SelectedAlarm.mapid;
            MapControl map = new MapControl();
            map.IsAlarm = true;
            map.view.SwitchDeviceTag(false);

            map.LoadSelectedSchoolMap(sid);
            map.MoveToTwoThirds(alarm.sersor);
            //map.GetSelectedDevice(alarm.sersor);
            grid.Children.Add(map);
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(MapControl));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(map, TrackerMapChanged);
            }
        }
        private void TrackerMapChanged(object sender, EventArgs e)
        {
            MapControl map = sender as MapControl;
            map.textpanel.Visibility = Visibility.Visible;
            InitTrack(AlarmPageViewModel.SelectedAlarm.sersor, map);

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
        }


        PlayerWindow playerWindow { get; set; }
        private void InitTrack(string code, MapControl map)
        {
            map.IsTracker = true;
            if (playerWindow != null && !playerWindow.IsActive)
            {
                playerWindow.Close();
            }
            playerWindow = new PlayerWindow(PlayerWindowType.Track, code);
            playerWindow.mapControl = map;
            playerWindow.Topmost = true;
            playerWindow.Owner = _main;
            playerWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            if (SubWinIndex != -1)
            {
                playerWindow.Left = MainWindowViewModel.SubWindows[SubWinIndex].Left + 23;
            }
            else
            {
                playerWindow.Left = 23;
            }

            playerWindow.Top = 165;
            playerWindow.Show();
            List<string> list = playerWindow.CodeList;
            foreach (var item in list)
            {
                map.StopFickerByCode(item);
                map.FlickerOneByCode(item);
            }
        }

        /// <summary>
        /// 设置电力设备列表
        /// </summary>
        /// <param name="grid"></param>
        private void InitElectricDeviceList(Grid grid)
        {
            MonitorEquipment ElectricDevice = new MonitorEquipment();
            MonitorEquipmentViewModel ElectricDeviceVm = new MonitorEquipmentViewModel();
            ElectricDeviceVm.GetElectricDevice(AlarmPageViewModel.SelectedAlarm.mapid, AlarmPageViewModel.SelectedAlarm.sid, AlarmPageViewModel.AlarmList);
            ElectricDevice.DataContext = ElectricDeviceVm;
            grid.Children.Add(ElectricDevice);
        }


        /// <summary>
        /// 逃生通道
        /// </summary>
        /// <param name="grid"></param>
        private void InitEscapeCamCorder(Grid grid)
        {
            string[] str = grid.Tag.ToString().Split(',');
            int row = int.Parse(str[0]);
            int coloum = int.Parse(str[1]);
            int count = row * coloum;
            PlayerPanel panel = new PlayerPanel();
            panel.InitEscapePlayer(coloum, row, AlarmPageViewModel.SelectedAlarm.sersor, grid);
        }
        /// <summary>
        /// 设置附近摄像机
        /// </summary>
        /// <param name="grid"></param>
        private void InitCamCorder(Grid grid)
        {
            string[] str = grid.Tag.ToString().Split(',');
            int row = int.Parse(str[0]);
            int coloum = int.Parse(str[1]);
            int count = row * coloum;
            PlayerPanel panel = new PlayerPanel();
            panel.InitAlarmPlayerPanel(coloum, row, AlarmPageViewModel.SelectedAlarm.sersor, grid, ref Players);


        }
        /// <summary>
        /// 设置电梯摄像机
        /// </summary>
        /// <param name="grid"></param>
        private void InitElevatorCamera(Grid grid)
        {
            string[] str = grid.Tag.ToString().Split(',');
            int row = int.Parse(str[0]);
            int coloum = int.Parse(str[1]);
            int count = row * coloum;
            PlayerPanel CamCorderPanel = new PlayerPanel();
            CamCorderPanel.InitElevatorPlayer(coloum, row, AlarmPageViewModel.SelectedAlarm.mapid, grid, PlayerWindowType.Module);
        }

        /// <summary>
        /// 添加消防通道组件
        /// </summary>
        /// <param name="alarm"></param>
        /// <param name="grid"></param>
        private void EscapeMap(Alarm alarm, Grid grid)
        {
            int sid = AlarmPageViewModel.SelectedAlarm.sid;

            MapControl map = new MapControl();
            map.IsAlarm = true;
            grid.Children.Add(map);
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(MapControl));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(map, ActualWidth_ValueChanged);
            }
            map.LoadSelectedSchoolMap(sid);

        }

        private void ActualWidth_ValueChanged(object sender, EventArgs e)
        {
            MapControl map = (MapControl)sender;
            map.FireEscapeCamera(AlarmPageViewModel.SelectedAlarm.sid);
        }

        private void Map(Alarm alarm, Grid grid)
        {

            int mapid = AlarmPageViewModel.SelectedAlarm.mapid;
            List<string> typeCodeList = new List<string>(CurrentSignal.device_type_status.Split(','));
            CurrentMap = new MapControl();
            CurrentMap.IsShowDeviceInfo = false;
            CurrentMap.IsAlarm = true;
            CurrentMap.view.SwitchDeviceTag(false);
            CurrentMap.LoadSelectedSchoolMap(mapid);
            //CurrentMap.SetDevice(typeCodeList);
            CurrentMap.GetSelectedDevice(alarm.sersor);
            CurrentMap.LoadAlarmInfo(alarm);
            
            CurrentMap.view.ClickMap();
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(MapControl));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(CurrentMap, mapActualWidthChanged);
            }
            grid.Children.Add(CurrentMap);
        }
        private void mapActualWidthChanged(object sender, EventArgs e)
        {
            try
            {
                //string[] aaa = CurrentSignal.device_type_status.Split(',');
                string codelist = CurrentSignal.device_type_status.Replace(" ","");
                List<string> typeCodeList = new List<string>(codelist.Split(','));
                CurrentMap.SetDevice(typeCodeList);

                foreach (var item in Players)
                {
                    CurrentMap.StopFickerByCode(item.CameraCode);
                    CurrentMap.FlickerOneByCode(item.CameraCode);
                }
            }
            catch
            {

            }
        }

        private void SchoolMap(Alarm alarm, Grid grid)
        {
            int mapid = AlarmPageViewModel.SelectedAlarm.mapid;
            int sid = AlarmPageViewModel.SelectedAlarm.sid;
            MapControl map = new MapControl();
            map.IsShowDeviceInfo = false;
            map.IsAlarm = true;
            map.view.SwitchDeviceTag(false);
            map.LoadSelectedSchoolMap(sid);
            if (mapid != sid)
            {
                var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(MapControl));
                if (descriptor != null)
                {
                    descriptor.AddValueChanged(map, ActualWidthChanged);
                }
                grid.Children.Add(map);
            }
            else
            {
                map.LoadSelectedSchoolMap(AlarmPageViewModel.SelectedAlarm.mapid);
                // map.GetSelectedDevice(alarm.sersor);
                grid.Children.Add(map);
            }

            //map.GetSelectedDevice(alarm.sersor);

        }


        private void ActualWidthChanged(object sender, EventArgs e)
        {
            try
            {
                MapControl map = (MapControl)sender;
                MangoMap[] mapList = GlobalVariable.MapList;
                MangoMap[] results = mapList.Where(x => x.id == AlarmPageViewModel.SelectedAlarm.mapid).ToArray();
                if (results != null)
                {
                    int buildId = results[0].pid;
                    string url = AppConfig.ServerBaseUri + AppConfig.GetMapElements;
                    Element[] list = HttpAPi.GetMapElements(AlarmPageViewModel.SelectedAlarm.sid, url);
                    Element[] ret = list.Where(x => x.deviceTypeCode == "@BuildingIcon").ToArray();
                    if (ret.Length == 0)
                    {
                        return;
                    }
                    Element element = ret.Where(x => int.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<IconExt>(x.iconExt).linkMap) == buildId).ToArray()[0];
                    System.Drawing.PointF Elementp = new System.Drawing.PointF();
                    GeoAPI.Geometries.Coordinate point = new GeoAPI.Geometries.Coordinate();
                    point.X = element.latitude;
                    point.Y = element.longitude;
                    map.view.RenderToScreen(point, out Elementp);
                    map.view.Center(point);
                    Elementp.X = 600;
                    Elementp.Y = 730;
                    map.AlarmLoadBuildingInfo(buildId, Elementp);
                    map.view.Refresh();
                }
            }
            catch (Exception ex)
            {

            }

        }

        private AlarmCarInfo CarInfo()
        {
            AlarmCarInfo carInfo = new AlarmCarInfo();
            return carInfo;
        }

        private DisposalPlan disposalPlan()
        {
            DisposalPlan disposal = new DisposalPlan();
            AlarmPageViewModel.disposalPlanViewModel.AlarmID = AlarmPageViewModel.SelectedId;
            AlarmPageViewModel.disposalPlanViewModel.alarmPageViewModel = AlarmPageViewModel;
            disposal.DataContext = AlarmPageViewModel.disposalPlanViewModel;
            return disposal;
        }

        #endregion
        #region 设置报警页面布局
        private void GetLayout(int id)
        {
            try
            {
                switch (id)
                {
                    case 1:
                        Layout1();
                        break;
                    case 2:
                        Layout2();
                        break;
                    case 3:
                        Layout3();
                        break;
                    case 4:
                        Layout4();
                        break;
                    case 5:
                        Layout5();
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AlarmMainPage), "GetLayout" + ex.Message);
            }
        }

        private void SetGrid(Grid grid, int row, int colum)
        {
            Grid.SetRow(grid, row);
            Grid.SetColumn(grid, colum);
        }

        private Grid MyGrid(int row, int column)
        {
            Grid grid = new Grid();
            grid.SetValue(Grid.RowSpanProperty, row);
            grid.SetValue(Grid.ColumnSpanProperty, column);
            return grid;
        }

        private void Layout1()
        {
            GridDic.Clear();
            Grid grid1 = MyGrid(3, 3);
            grid1.Tag = "3,3";
            GridDic.Add(1, grid1);
            panel.Children.Add(grid1);
            SetGrid(grid1, 0, 0);
        }
        private void Layout2()
        {
            GridDic.Clear();
            Grid grid1 = MyGrid(2, 2);
            grid1.Tag = "2,2";
            GridDic.Add(1, grid1);
            panel.Children.Add(grid1);
            SetGrid(grid1, 0, 0);
            Grid grid2 = MyGrid(2, 1);
            grid2.Tag = "2,1";
            GridDic.Add(2, grid2);
            panel.Children.Add(grid2);
            SetGrid(grid2, 0, 2);
            Grid grid3 = MyGrid(1, 2);
            grid3.Tag = "1,2";
            GridDic.Add(3, grid3);
            panel.Children.Add(grid3);
            SetGrid(grid3, 2, 0);
            Grid grid4 = MyGrid(1, 1);
            grid4.Tag = "1,1";
            GridDic.Add(4, grid4);
            panel.Children.Add(grid4);
            SetGrid(grid4, 2, 2);

        }
        private void Layout3()
        {
            GridDic.Clear();
            Grid grid1 = MyGrid(2, 3);
            grid1.Tag = "2,3";
            GridDic.Add(1, grid1);
            panel.Children.Add(grid1);
            SetGrid(grid1, 0, 0);
            Grid grid2 = MyGrid(1, 1);
            grid2.Tag = "1,1";
            GridDic.Add(2, grid2);
            panel.Children.Add(grid2);
            SetGrid(grid2, 2, 0);
            Grid grid3 = MyGrid(1, 2);
            grid3.Tag = "1,2";
            GridDic.Add(3, grid3);
            panel.Children.Add(grid3);
            SetGrid(grid3, 2, 1);

        }
        private void Layout4()
        {
            GridDic.Clear();
            Grid grid1 = MyGrid(3, 2);
            grid1.Tag = "3,2";
            GridDic.Add(1, grid1);
            panel.Children.Add(grid1);
            SetGrid(grid1, 0, 0);
            Grid grid2 = MyGrid(1, 1);
            grid2.Tag = "1,1";
            GridDic.Add(2, grid2);
            panel.Children.Add(grid2);
            SetGrid(grid2, 0, 2);
            Grid grid3 = MyGrid(1, 1);
            grid3.Tag = "1,1";
            GridDic.Add(3, grid3);
            panel.Children.Add(grid3);
            SetGrid(grid3, 1, 2);
            Grid grid4 = MyGrid(1, 1);
            grid4.Tag = "1,1";
            GridDic.Add(4, grid4);
            panel.Children.Add(grid4);
            SetGrid(grid4, 2, 2);

        }
        private void Layout5()
        {
            GridDic.Clear();
            Grid grid1 = MyGrid(3, 2);
            grid1.Tag = "3,2";
            GridDic.Add(1, grid1);
            panel.Children.Add(grid1);
            SetGrid(grid1, 0, 0);
            Grid grid2 = MyGrid(2, 1);
            grid2.Tag = "2,1";
            GridDic.Add(2, grid2);
            panel.Children.Add(grid2);
            SetGrid(grid2, 0, 2);
            Grid grid3 = MyGrid(1, 1);
            grid3.Tag = "1,1";
            GridDic.Add(3, grid3);
            panel.Children.Add(grid3);
            SetGrid(grid3, 2, 2);
        }
        #endregion
        /// <summary>
        /// 获取当前报警页面布局配置
        /// </summary>
        /// <param name="type"></param>
        private void GetConfig(AlarmPageViewModel viewModel)
        {
            try
            {
                string signalType = viewModel.SelectedAlarm.flag.Trim() + "-" + viewModel.SelectedAlarm.type;
                if (CurrenClientConfig.signalLists.Length != 0)
                {
                    foreach (var item in CurrenClientConfig.signalLists)
                    {
                        Signal[] signal = GlobalVariable.Signals.Where(x => x.signal_name == item.signal_name).ToArray();
                        if (signal.Length == 0) continue;
                        if (signal[0].signal_id == signalType)
                        {
                            ScenesList = item.scenesList;
                            CurrentSignal = signal[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
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
        // ~AlarmMainPage() {
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


    }
}
