using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using ManagementProject.UserControls.MainPageControls;
using ManagementProject.UserControls.PlayerControls;
using MangoMapLibrary;
using MangoMapLibrary.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : System.Windows.Controls.UserControl,IDisposable 
    {
        public List<string> ParameterList = new List<string>();
        public bool IsShowDeviceInfo { get; set; } = true;
        public bool IsTrackPage { get; set; } = false;
        public bool IsAlarm { get; set; } = false;
        public int AlarmId { get; set; }
        public int CurrentMapId { get; set; }
        System.Windows.Forms.TreeView treeView1 = new System.Windows.Forms.TreeView();
        System.Windows.Forms.TreeView treeView2 = new System.Windows.Forms.TreeView();
        System.Windows.Forms.TreeView treeView3 = new System.Windows.Forms.TreeView();
        public MapView view;
        private Dictionary<string, SelectedPlayer> TrackPlayerDictionary = new Dictionary<string, SelectedPlayer>();
        private Dictionary<string, MapPlayer> MapPlayerDictionary = new Dictionary<string, MapPlayer>();
        private List<string> MapPlayerCodeList = new List<string>();
        private Dictionary<string, DeviceType> deviceTypeList = new Dictionary<string, DeviceType>();
       
        private Dictionary<int, Device> deviceList = new Dictionary<int, Device>();
        private MainWindow _mainWin = (MainWindow)System.Windows.Application.Current.MainWindow;
        private void dispose()
        {
            _mainWin = null;
            treeView1.Dispose();
            treeView2.Dispose();
            treeView3.Dispose();
            view.Close();
            view.Dispose();
        }
        public MapControl()
        {
            InitializeComponent();
            MapViewInit();


            Loaded += MapControl_Loaded;

            Unloaded += MapControl_Unloaded;
            deviceinfobt.bt.Click += Bt_Click;
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            IsOpenTag = !IsOpenTag;
        }

        private void MapControl_Unloaded(object sender, RoutedEventArgs e)
        {
            IsTracker = false;
            view.SetFireRoad(false);
            CloseAllMapPlayer();
            UnLoadControl();
            Dispose();
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpenTag = true;
            SetDeviceInfoMargin();
            SetDeviceInfoVisbility();
            deviceinfobt.BtImage = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/设备信息.png", UriKind.Relative));
            SetMapName();
            //载入建筑资源
            LoadResource();
        }

        private void SetMapName()
        {
            text.message.Visibility = Visibility.Collapsed;
            text.drapbt.Visibility = Visibility.Collapsed;
            text.homebt.Visibility = Visibility.Collapsed;
            textpanel.Visibility = Visibility.Hidden;
        }

        private void SetDeviceInfoMargin()
        {
            if (IsTrackPage)
            {
                deviceinfobt.Margin = new Thickness(0,32,373,0);
            }
           

        }

        private void SetDeviceInfoVisbility()
        {
            if (!IsShowDeviceInfo)
            {
                deviceinfobt.Visibility  = Visibility.Collapsed  ;
            }

        }

        private void MapViewInit()
        {
            InitMapList();
            InitDeviceType(AppConfig.GetAllDeviceType);
            InitDeviceList(AppConfig.GetAllDeviceList);

            //初始化地图
            view = new MapView(AppConfig.MapPath, MapView.RunningMode.VIEW);            //以视图模式运行

            view.InitDeviceTypeList(this.deviceTypeList);
            view.Dock = DockStyle.Fill;
            view.OnElementClick += View_OnElementClick;
            //view.OnElementMoveIn += View_OnElementMoveIn;
            //view.OnElementChanged += View_OnElementChanged;
            //view.OnLayerChanged += View_OnLayerChanged;                     //MangoLayer属性变化
            panel.Controls.Add(view);
            view.Map().MapViewOnChange += MapControl_MapViewOnChange;
        }

        private void MapControl_MapViewOnChange()
        {
            try
            {
                if (IsTrackPage )
                {
                    foreach (var item in TrackPlayerDictionary)
                    {
                        RefreshTrackPlayer(item.Key, item.Value);
                    }
                }
                else
                {
                    foreach (var item in MapPlayerDictionary)
                    {
                        RefreshMapPlayer(item.Key, item.Value);
                    }
                }
                
            }catch (Exception ex)
            {
                Logger.Error("MapControl_MapViewOnChange:" + ex.Message);
            }
        }

        private void RefreshMapPlayer(string code,MapPlayer player)
        {
            List<AbstractLayerElement> list = view.GetDeviceList();
            List<AbstractLayerElement> elements = list.Where(x => x.Code == code).ToList();
            AbstractLayerElement element = elements[0];
            Window parentWindow = Window.GetWindow(this);
            System.Drawing.PointF Elementp = new System.Drawing.PointF();
            view.RenderToScreen(element.p, out Elementp);
            player.Top = Elementp.Y + 20;
            player.Left = parentWindow.Left + Elementp.X + 20;
        }

        private void RefreshTrackPlayer(string code, SelectedPlayer player)
        {
            List<AbstractLayerElement> list = view.GetDeviceList();
            List<AbstractLayerElement> elements = list.Where(x => x.Code == code).ToList();
            AbstractLayerElement element = elements[0];
            System.Drawing.PointF Elementp = new System.Drawing.PointF();
            view.RenderToScreen(element.p, out Elementp);
            player.Top = Elementp.Y + 20;
            player.Left = Elementp.X + 20;
        }





        #region 初始化楼层按钮
        private BuildingControl control;
        private void SelectedBuilding(int id)
        {
            try
            {
                if (IsAlarm )
                {
                    return;
                }
                if (control!=null)
                {
                    return;
                }

                MangoApi.MangoMap[] ret = GlobalVariable.MapList.Where(x => x.id == id).ToArray();
                if (ret.Length == 0) return;
                MangoApi.MangoMap[] map = GlobalVariable.MapList.Where(x => x.pid == ret[0].pid).ToArray();
                if (map.Length == 0) return;
                control = new BuildingControl();
                control.SetTextBox();
                control.InitTab(map);
                control.SetCurrentId(id);
                control.Owner = _mainWin;
                control.Topmost = true;
                control.WindowStartupLocation = WindowStartupLocation.Manual;
                control.Left = 1750;
                control.Top = 225;
                control.Show();
                
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl),ex.Message);
            }

        }
        private void UnLoadControl()
        {
            UnloadAlarmInfo();
            UnLoadBuildingControl();
            UnLoadedBuildingInfo();
        }
        #endregion

        private void UnLoadedBuildingInfo()
        {
            if (buildingMsg!=null)
            {
                buildingMsg.Close();
            }
        }

        private SchoolBuildingMsg buildingMsg;
        public void LoadBuildingInfo(int buildingmapid, System.Drawing.PointF point)
        {
            buildingMsg = new SchoolBuildingMsg();
            SchoolBuildMsgViewModel buildMsgViewModel = new SchoolBuildMsgViewModel();
            buildMsgViewModel.InitMes(buildingmapid);
            buildingMsg.DataContext = buildMsgViewModel;
            buildingMsg.Owner = _mainWin;
            buildingMsg.Topmost = true;
            buildingMsg.WindowStartupLocation = WindowStartupLocation.Manual;
            buildingMsg.Left = point.X+20;
            buildingMsg.Top = point.Y+20;
            buildingMsg.LoadedItem(buildingmapid);
            buildingMsg.ShowDialog();
           
        }

        public void AlarmLoadBuildingInfo(int buildingmapid, System.Drawing.PointF point)
        {
            buildingMsg = new SchoolBuildingMsg();
            SchoolBuildMsgViewModel buildMsgViewModel = new SchoolBuildMsgViewModel();
            buildMsgViewModel.InitMes(buildingmapid);
            buildingMsg.DataContext = buildMsgViewModel;
            buildingMsg.Owner = _mainWin;
            buildingMsg.Topmost = true;
            buildingMsg.WindowStartupLocation = WindowStartupLocation.Manual;
            buildingMsg.Left = point.X + 20;
            buildingMsg.Top = point.Y + 20;
            buildingMsg.Show();
            buildingMsg.LoadedItem(buildingmapid);
        }

        bool isop = false;
        private void View_OnElementClick(object sender, System.Drawing.Point point)
        {
            if (IsAlarm)
            {
                return;
            }
            AbstractLayerElement element=(AbstractLayerElement)sender;
            System.Drawing.PointF Elementp=new System.Drawing.PointF ();
            view.RenderToScreen(element.p,out Elementp);
            if (element.Type=="@Building")
            {
                LayerElementBuilding layer =(LayerElementBuilding) sender;
                int buildingmapid = layer.Ext.linkMap;
                if (buildingmapid == 0) return;
                GlobalVariable.CurrentMapKind = MapKind.Building;
                GlobalVariable.SelectedSchoolId = buildingmapid;
            }
            else if (element .Type == "@BuildingIcon")
            {
                LayerElementBuilding layer = (LayerElementBuilding)sender;
                int buildingmapid = layer.Ext.linkMap;
                 if (buildingmapid == 0) return;
                LoadBuildingInfo(buildingmapid,point);
            }
            
            if (element.Type.Length <6)
            {
                return;
            }
            string type =element.Type.Substring(element.Type.Length - 6);
            if (type!="Camera")
            {
                return;
            }
            if (IsTrackPage)
            {
                if (TrackPlayerDictionary.ContainsKey(element.Code))
                {
                    SelectedPlayer playerWin = TrackPlayerDictionary[element.Code];
                    playerWin.Close();
                    TrackPlayerDictionary.Remove(element.Code);
                    MapPlayerCodeList.Remove(element.Code);

                    //取消摄像机选中状态
                    var cameraElement = GetElementByCode(element.Code);
                    view.UnSelectOne(cameraElement);
                }
                else
                {
                    InitTrackPlayer(element.Code, Elementp);
                }
            }
            else
            {
                if (MapPlayerDictionary.ContainsKey(element.Code))
                {
                    MapPlayer playerWin = MapPlayerDictionary[element.Code];
                    playerWin.Close();
                    MapPlayerDictionary.Remove(element.Code);
                    MapPlayerCodeList.Remove(element.Code);

                    //取消摄像机选中状态
                    var cameraElement = GetElementByCode(element.Code);
                    view.UnSelectOne(cameraElement);
                }
                else
                {
                    InitMapPlayer(element.Code, Elementp);
                }
            }
            
           
        }

        private void InitTrackPlayer(string code, System.Drawing.PointF point)
        {
            if (TrackPlayerDictionary.Count >= GlobalVariable.MaxCameraCount)
            {
                string keyCode = MapPlayerCodeList.First();
                SelectedPlayer firstPlayer = TrackPlayerDictionary[keyCode];
                firstPlayer.Close();
                TrackPlayerDictionary.Remove(keyCode);
                MapPlayerCodeList.Remove(keyCode);
            }
            Window parentWindow = Window.GetWindow(this);
            SelectedPlayer playerWin = new SelectedPlayer();
            playerWin.Code = code;
            playerWin.DataContext = DataContext;
            //playerWin.Topmost = true;
            //playerWin.WindowStyle = WindowStyle.None;
            playerWin.WindowStartupLocation = WindowStartupLocation.Manual;
            playerWin.Owner = parentWindow;
            playerWin.Top = point.Y + 20;
            playerWin.Left = point.X + 20;
            var player = new Player(PlayerWindowType.Normal);
            player.panel.MouseClick -= player.PnlPlayer_MouseClick;
            playerWin.panel.Children.Add(player);
            playerWin.Show();
            player.CameraCode = code;
            player.StartLive();
            TrackPlayerDictionary.Add(code, playerWin);
            MapPlayerCodeList.Add(code);
        }

        private void InitMapPlayer(string code, System.Drawing.PointF point)
        {
            if (MapPlayerDictionary.Count >= GlobalVariable.MaxCameraCount)
            {
                string keyCode = MapPlayerCodeList.First();
                MapPlayer firstPlayer = MapPlayerDictionary[keyCode];
                firstPlayer.Close();
                MapPlayerDictionary.Remove(keyCode);
                MapPlayerCodeList.Remove(keyCode);


            }
            Window parentWindow = Window.GetWindow(this);

            MapPlayer playerWin = new MapPlayer();
            playerWin.MapPlayerCode = code;
            //playerWin.Topmost = true;
            //playerWin.WindowStyle = WindowStyle.None;
            playerWin.WindowStartupLocation = WindowStartupLocation.Manual;
            playerWin.Owner = _mainWin;
            playerWin.Top = point.Y + 20;
            playerWin.Left = parentWindow.Left + point.X + 20;
            var player = new Player(PlayerWindowType.Normal);
            playerWin.panel.Children.Add(player);
            playerWin.Show();
            player.mapControl = this;
            player.CameraCode = code;
            player.StartLive();
            MapPlayerDictionary.Add(code, playerWin);
            MapPlayerCodeList.Add(code);
            playerWin.MouseLeftButtonDown += PlayerWin_MouseDown;
        }

        /// <summary>
        /// 显示消防通道摄像机
        /// </summary>
        public void FireEscapeCamera(int sid)
        {
            MangoApi.ElementDevice[] camera = HttpAPi.GetEngineModule(sid);
            view.SetFireRoad(true);
            foreach (var item in camera)
            {
                FlickerFireEscapeCamera(item .code);
            }
        }

        public void MoveToTwoThirds(string code)
        {
            var element = GetElementByCode(code);
            view.MoveToTwoThirds(element);
        }

        /// <summary>
        /// 闪烁消防通道摄像机并且打开摄像机画面
        /// </summary>
        /// <param name="code">摄像机编码</param>
        public void FlickerFireEscapeCamera(string code)
        {
            try
            {
                var element = GetElementByCode(code);
                System.Drawing.PointF Elementp = new System.Drawing.PointF();
                view.RenderToScreen(element.p, out Elementp);
                view.FlickerOne(element);
               
                InitMapPlayer(element.Code , Elementp);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl), ex.Message);
            }
        }


        private void PlayerWin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MapPlayer win = (MapPlayer)sender;
            win.DragMove();
        }
        /// <summary>
        /// 关闭所有已打开MapPlayer
        /// </summary>
        public void CloseAllMapPlayer()
        {
            foreach (var item in MapPlayerDictionary)
            {
                item.Value.Close();
                
            }
            MapPlayerDictionary.Clear ();
        }
        #region 依赖属性
        #region 标签控制
        /// <summary>
        /// 标签控制
        /// </summary>
        public bool IsOpenTag
        {
            get
            {
                return (bool)GetValue(IsOpenTagProperty);
            }
            set
            {
                SetValue(IsOpenTagProperty, value);
            }
        }
        //声明依赖属性变量
        public static readonly DependencyProperty IsOpenTagProperty = DependencyProperty.Register("IsOpenTag", typeof(bool), typeof(MapControl),
                new PropertyMetadata(false, (s, e) =>
                {
                    var mdp = s as MapControl;
                    if (mdp != null)
                    {
                        try
                        {
                            bool _isOpened = (bool)e.NewValue;
                            mdp.view.SwitchDeviceTag(!_isOpened);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(typeof(MapControl), "IsOpenTag" + ex.Message);
                        }
                    }

                }));
        #endregion
        #region 地图加载控制
        //使用.NET属性包装依赖属性:属性名称与注册时候的名称必须一致，
        //即属性名SelectedSchoolId对应注册时的SelectedSchoolId
        public int SelectedSchoolId
        {
            get
            {
                return (int)GetValue(SelectedSchoolIdProperty);
            }
            set
            {
                SetValue(SelectedSchoolIdProperty, value);
            }
        }
        //声明依赖属性变量
        public static readonly DependencyProperty SelectedSchoolIdProperty = DependencyProperty.Register("SelectedSchoolId", typeof(int), typeof(MapControl),
                new PropertyMetadata(defaultValue: 0,
                    propertyChangedCallback: OnPropertyChanged,
                    coerceValueCallback: coerceValueCallback));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mdp = d as MapControl;
            if (mdp != null)
            {
                try
                {
                    int sid = (int)e.NewValue;
                    mdp.CloseAllMapPlayer();
                    mdp.LoadSelectedSchoolMap(sid);

                }
                catch (Exception ex)
                {
                    Logger.Error(typeof(MapControl), "SelectedSchoolId" + ex.Message);
                }
            }

        }
       
        private static object coerceValueCallback(DependencyObject d, object baseValue)
        {
            if (baseValue != null)
            {
                var control = d as MapControl;
                //try
                //{
                //    int sid = (int)baseValue;
                //    control.CloseAllMapPlayer();
                //    control.LoadSelectedSchoolMap(sid);
                //}
                //catch (Exception ex)
                //{
                //    Logger.Error(typeof(MapControl), "SelectedSchoolId" + ex.Message);
                //}
            }
            return baseValue;
        }


        /// <summary>
        /// 根据sid加载地图
        /// </summary>
        /// <param name="sid">校区</param>
        public void LoadSelectedSchoolMap(int sid)
        {
            try
            {
                CurrentMapId = sid;
                string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
                MangoMap[] map = MangoMapLibrary.Api.Util.GetMapList(url);
                MangoMap[] results = map.Where(x => x.id == sid).ToArray();
                if (results.Length>0)
                {
                    MangoMap ret = results[0];
                    if (ret.pid != 0 && !GlobalVariable.SidList.Contains(ret.pid.ToString()))
                    {
                        System.Windows.MessageBox.Show("没有当前建筑的查看权限！");
                        return;
                    }
                    text.schoolNameBox.Text = ret.name;
                    if (ret.pid == 0)
                    {
                        UnLoadBuildingControl();
                    }
                    else
                    {
                        UnLoadBuildingControl();
                        SelectedBuilding(sid);
                    }
                    if (parameterWin !=null)
                    {
                        CloseParameterWin();
                        LoadParameterWin(sid);
                    }
                  
                    LoadMap(ret);
                }
            }
            catch (Exception ex)
            {
                view.Refresh();
                Logger.Error(typeof(MapControl), "LoadSelectedSchoolMap:"+ex.Message);
            }
        }

        private void UnLoadBuildingControl()
        {
            if (control != null)
            {
                control.Close();
                control = null;
            }
        }

        #endregion

        public List<string> DeviceVisibleList
        {
            get { return (List<string>)GetValue(DeviceVisibleListProperty); }
            set { SetValue(DeviceVisibleListProperty, value); }
        }

        public static readonly DependencyProperty DeviceVisibleListProperty =
            DependencyProperty.Register("DeviceVisibleList", typeof(List<string>), typeof(MapControl), new PropertyMetadata(null, (s, e) =>
            {
                var mdp = s as MapControl;
                if (mdp != null)
                {
                    try
                    {
                        List<string> list = (List<string>)e.NewValue;
                        mdp.SetDevice(list);
                    }
                    catch(Exception ex)
                    {
                        Logger.Error(typeof(MapControl), "DeviceVisibleList"+ ex.Message);
                    }
                }

            }));


        public List<string> CodeVisibleList
        {
            get { return (List<string>)GetValue(CodeVisibleListProperty); }
            set { SetValue(CodeVisibleListProperty, value); }
        }

        public static readonly DependencyProperty CodeVisibleListProperty =
            DependencyProperty.Register("CodeVisibleList", typeof(List<string>), typeof(MapControl), new PropertyMetadata(null, (s, e) =>
            {
                var mdp = s as MapControl;
                if (mdp != null)
                {
                    try
                    {
                        List<string> list = (List<string>)e.NewValue;
                        mdp.SetCodeList(list);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(typeof(MapControl), "CodeVisibleList" + ex.Message);
                    }
                }

            }));


        public bool IsTracker
        {
            get { return (bool)GetValue(IsTrackerProperty); }
            set { SetValue(IsTrackerProperty, value); }
        }

        public static readonly DependencyProperty IsTrackerProperty =
            DependencyProperty.Register("IsTracker", typeof(bool), typeof(MapControl), new PropertyMetadata(false, (s, e) =>
            {
                var mdp = s as MapControl;
                if (mdp != null)
                {
                    try
                    {
                        bool istracker = (bool)e.NewValue;
                        if (istracker)
                        {
                            mdp.LoadParameterWin(mdp.CurrentMapId);
                        }else
                        {
                            mdp.CloseParameterWin();
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(typeof(MapControl), "IsTracker" + ex.Message);
                    }
                }

            }));
        private ParameterWin parameterWin;
        public void LoadParameterWin(int mapid)
        {
            try
            {
                Window parentWindow = Window.GetWindow(this);
                MangoApi.Parameter[] list = HttpAPi.ParameterList(mapid);
                int count = list.Count();
                parameterWin = new ParameterWin() { List = list, mapControl = this };
                //parameterWin.LoadControl(list,this);
                parameterWin.Topmost = true;
                parameterWin.WindowStartupLocation = WindowStartupLocation.Manual;
                parameterWin.Owner = _mainWin;
                parameterWin.Top = 980 - 40 * count;
                parameterWin.Left = parentWindow.Left + 1700;
                parameterWin.Show();

            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
           
        }

        public void CloseParameterWin()
        {
            if (parameterWin!=null)
            {
                parameterWin.Close();
                parameterWin = null;
            }
        }



        public void SetCodeList(List<string> codeList)
        {
            view.SetCodeList(codeList);
        }

        /// <summary>
        /// 根据列表显示地图设备图标
        /// </summary>
        /// <param name="devicecode"></param>
        public void SetDevice(List<string> devicecode)
        {
            view.SelectedDeviceType(devicecode);
        }


        public bool SelectedCancel
        {
            get
            {
                return (bool)GetValue(SelectedCancelProperty);
            }
            set
            {
                SetValue(SelectedCancelProperty, value);
            }
        }
        //声明依赖属性变量
        public static readonly DependencyProperty SelectedCancelProperty = DependencyProperty.Register("SelectedCancel", typeof(bool), typeof(MapControl),
                new PropertyMetadata(false, (s, e) =>
                {
                    var mdp = s as MapControl;
                    if (mdp != null)
                    {
                        try
                        {
                            mdp.UnSelectedAll();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(typeof(MapControl), "SelectedCancel:" + ex.Message);
                        }
                    }

                }));


        public string SelectedElement
        {
            get
            {
                return (string)GetValue(SelectedElementProperty);
            }
            set
            {
                SetValue(SelectedElementProperty, value);
            }
        }
        //声明依赖属性变量
        public static readonly DependencyProperty SelectedElementProperty = DependencyProperty.Register("SelectedElement", typeof(string), typeof(MapControl),
                new PropertyMetadata(null, (s, e) =>
                {
                    var mdp = s as MapControl;
                    if (mdp != null)
                    {
                        try
                        {
                            string code = (string)e.NewValue;
                            mdp.CloseAllMapPlayer();
                            mdp.StopFickerByCode(code);
                            mdp.FlickerOneByCode(code);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(typeof(MapControl), "SelectedElement:" + ex.Message);
                        }
                    }

                }));
        public string SelectedDevice
        {
            get
            {
                return (string)GetValue(SelectedDeviceProperty);
            }
            set
            {
                SetValue(SelectedDeviceProperty, value);
            }
        }
        //声明依赖属性变量
        public static readonly DependencyProperty SelectedDeviceProperty = DependencyProperty.Register("SelectedDevice", typeof(string), typeof(MapControl),
                new PropertyMetadata(null, (s, e) =>
                {
                    var mdp = s as MapControl;
                    if (mdp != null)
                    {
                        try
                        {
                            string code = (string)e.NewValue;
                            mdp.CloseAllMapPlayer();
                            mdp.GetSelectedDevice(code);
                        }
                        catch(Exception ex)
                        {
                            Logger.Error(typeof(MapControl), "SelectedDevice:" + ex.Message);
                        }
                    }

                }));

        public void GetSelectedDevice(string code)
        {
            try
            {
                var element = GetElementByCode(code);
                view.SelectOne(element);
                view.Center(element);
                view.FlickerOne(element);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl),ex.Message );
            }
        }

        private AbstractLayerElement GetElementByCode(string code)
        {
            List<AbstractLayerElement> list = view.GetDeviceList();
            List<AbstractLayerElement> elements = list.Where(x => x.Code == code).ToList();
            if (elements.Count>0)
            {
                return elements[0];
            }
            return null;
        }

        public void UnSelectedAll()
        {
            try
            {
                List<AbstractLayerElement> list = view.GetDeviceList();
                foreach (var item in list)
                {
                    view.StopFlickerOne(item);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl), ex.Message);
            }
        }

        public void FlickerOneByCode(string code)
        {
            try
            {
                var element = GetElementByCode(code);
                view.FlickerOne(element);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl), ex.Message);
            }
        }

        public void StopFickerByCode(string code)
        {
            try
            {
                var element = GetElementByCode(code);
                view.StopFlickerOne(element);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(MapControl), ex.Message);
            }
        }


        AlarmInfoWin alarmInfoWin;
        public void LoadAlarmInfo(MangoApi.Alarm alarm)
        {
            try
            {
                UnloadAlarmInfo();
                Window parentWindow = Window.GetWindow(this);
                AlarmControls.AlarmDialog dialog = new AlarmControls.AlarmDialog(alarm);
                alarmInfoWin = new AlarmInfoWin();
                alarmInfoWin.Content = dialog;
                alarmInfoWin.WindowStartupLocation = WindowStartupLocation.Manual;
                alarmInfoWin.Owner = _mainWin;
                alarmInfoWin.Top = 400;
                alarmInfoWin.Left = 620;
                alarmInfoWin.Topmost = true;
                alarmInfoWin.Show();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void UnloadAlarmInfo()
        {
            if (alarmInfoWin != null)
            {
                alarmInfoWin.Close();
            }
        }

        #endregion
        private void LoadResource()
        {
            treeView3.Nodes.Clear();
            string rootPath = Directory.GetCurrentDirectory() + "\\Resource";
            DirectoryInfo root = new DirectoryInfo(rootPath);
            FileInfo[] files = root.GetFiles();
            foreach (FileInfo info in files)
            {
                TreeNode node = new TreeNode();
                node.Text = info.Name;
                IconExtParamBuilding ext = new IconExtParamBuilding();
                ext.filename = "Resource/" + info.Name;
                node.Tag = ext;
                treeView3.Nodes.Add(node);
            }
        }

        Dictionary<int, TreeNode> mapitems = new Dictionary<int, TreeNode>();
        private void InitMapList()
        {
            this.treeView1.Nodes.Clear();
            MangoMap[] maps = MangoMapLibrary.Api.Util.GetMapList(AppConfig.ServerBaseUri + AppConfig.GetMap);
          
            if (maps != null)
            {
                Dictionary<int, TreeNode> items = new Dictionary<int, TreeNode>();
                foreach (MangoMap map in maps)
                {
                    TreeNode node = new TreeNode();
                    node.Text = map.name;
                    node.Tag = map;
                    items.Add(map.id, node);
                }
                foreach (MangoMap map in maps)
                {
                    if (map.pid > 0 && items.ContainsKey(map.pid))
                    {
                        items[map.pid].Nodes.Add(items[map.id]);
                        continue;
                    }
                    this.treeView1.Nodes.Add(items[map.id]);
                }
                mapitems = items;
            }
            this.treeView1.ExpandAll();
        }

        private void InitDeviceList(string API_GET_ALL_DEVICE_LIST)
        {
            Device[] _deviceList = MangoMapLibrary.Api.Util.GetDeviceList(AppConfig.ServerBaseUri + API_GET_ALL_DEVICE_LIST);
            if (_deviceList != null)
            {
                Dictionary<string, TreeNode> items = new Dictionary<string, TreeNode>();
                foreach (DeviceType type in deviceTypeList.Values)
                {
                    TreeNode node = new TreeNode();
                    node.Text = type.deviceTypeName;
                    node.Tag = type;
                    items.Add(type.deviceTypeCode, node);
                }
                foreach (Device device in _deviceList)
                {
                    if (deviceList.ContainsKey(device.id))
                        continue;

                    deviceList.Add(device.id, device);//展示区_海康
                    if (device.deviceTypeId == null || device.deviceTypeId == "" || !items.ContainsKey(device.deviceTypeId))
                    {
                        continue;
                    }

                    TreeNode node = new TreeNode();
                    node.Text = device.name;
                    node.Tag = device;

                    items[device.deviceTypeId].Nodes.Add(node);
                }

                foreach (TreeNode type in items.Values)
                {
                    if (type.Nodes.Count > 0)
                        this.treeView2.Nodes.Add(type);
                }
            }
            this.treeView2.ExpandAll();
        }

        int imagesize = 0;
        private void InitDeviceType(string API_GET_ALL_DEVICE_TYPE)
        {
            DeviceType[] deviceList = MangoMapLibrary.Api.Util.GetDeviceTypeList(AppConfig .ServerBaseUri + AppConfig .GetAllDeviceType);
            if (deviceList != null)
            {

                foreach (DeviceType type in deviceList)
                {
                    if (type.deviceTypeCode == null || type.deviceTypeCode == "")
                    {
                        continue;
                    }
                    try
                    {
                        type.deviceImage = MangoMapLibrary.Util.LoadImage(AppConfig.ImageBaseUri + type.deviceImg);
                        type.checkedImage = MangoMapLibrary.Util.LoadImage(AppConfig.ImageBaseUri + type.checkedImg);
                        type.alarmImage = MangoMapLibrary.Util.LoadImage(AppConfig.ImageBaseUri + type.alarmImg);
                        imagesize = type.deviceImage.Width * type.deviceImage.Height * 4 + type.checkedImage.Width * type.checkedImage.Height * 4 + type.alarmImage.Width * type.alarmImage.Height * 4;
                    }
                    catch (Exception ex)
                    {
                        //System.Windows.MessageBox.Show("载入图片失败，类型：" + type.deviceTypeName);
                        Logger.Error("载入图片失败，类型：" + type.deviceTypeName);
                    }
                    deviceTypeList.Add(type.deviceTypeCode, type);
                }
               // System.Windows.MessageBox.Show(""+ imagesize);
            }
        }
        private void LoadMap(MangoMap map)
        {
            MangoLayer[] layers = MangoMapLibrary.Api.Util.GetLayerList(AppConfig.ServerBaseUri + AppConfig .GetMapLayers, map.id);
            if (layers == null)
            {
                System.Windows.MessageBox.Show("读取层数据失败!");
                return;
            }

            // 添加设备
            MangoLayerElement[] elements = MangoMapLibrary.Api.Util.GetMapElements(AppConfig.ServerBaseUri + AppConfig.GetMapElements, map.id);

            this.view.LoadMap(map, layers, elements);

            //刷新已部署设备列表
           // InitDeployedDeviceList();
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
        // ~MapControl() {
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
