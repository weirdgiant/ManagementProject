using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
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
    public partial class MapControl : System.Windows.Controls.UserControl
    {
        System.Windows.Forms.TreeView treeView1 = new System.Windows.Forms.TreeView();
        System.Windows.Forms.TreeView treeView2 = new System.Windows.Forms.TreeView();
        System.Windows.Forms.TreeView treeView3 = new System.Windows.Forms.TreeView();
        public MapControl()
        {
            InitializeComponent();
            this.Loaded += MapControl_Loaded;
        }
        private MapView view;
        private string SERVER_URL_BASE;
        private string IMAGE_URL_BASE;
        private string MAP_PATH;

        private Dictionary<string, DeviceType> deviceTypeList = new Dictionary<string, DeviceType>();
        private Dictionary<int, Device> deviceList = new Dictionary<int, Device>();
        private void View_onElementDelete(AbstractLayerElement device)
        {
            if (device.Id == 0)
            {
                return;
            }
            AppSettingsReader reader = new AppSettingsReader();
            var API_DELETE_MAP_ELEMENTS = (string)reader.GetValue("API_DELETE_MAP_ELEMENTS", typeof(string));
            MangoMapLibrary.Api.Util.DeleteLayerElement(SERVER_URL_BASE + API_DELETE_MAP_ELEMENTS, device.Id);
        }
        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettingsReader reader = new AppSettingsReader();
            //reader.GetValue()
            MAP_PATH = (string)reader.GetValue("MAP_PATH", typeof(string));
            SERVER_URL_BASE = (string)reader.GetValue("SERVER_URL_BASE", typeof(string));
            IMAGE_URL_BASE = (string)reader.GetValue("IMAGE_URL_BASE", typeof(string));

            var API_GET_ALL_DEVICE_TYPE = (string)reader.GetValue("API_GET_ALL_DEVICE_TYPE", typeof(string));
            var API_GET_ALL_DEVICE_LIST = (string)reader.GetValue("API_GET_ALL_DEVICE_LIST", typeof(string));

            InitMapList();
            InitDeviceType(API_GET_ALL_DEVICE_TYPE);
            InitDeviceList(API_GET_ALL_DEVICE_LIST);

            //初始化地图
            view = new MapView(MAP_PATH, MapView.RunningMode.VIEW);            //以编辑器模式运行

            view.InitDeviceTypeList(this.deviceTypeList);
            view.Dock = DockStyle.Fill;
            //view.OnElementDelete += View_onElementDelete;                       //当地图上设备被删除时触发事件
            //view.OnElementAdd += View_OnElementAdd;                             //当地图上设备被添加时触发事件
            //view.OnElementMoveIn += View_OnElementMoveIn;
            //view.OnElementChanged += View_OnElementChanged;
            //view.OnLayerChanged += View_OnLayerChanged;                         //MangoLayer属性变化
            //view.OnLayerCreate += View_OnLayerCreate;
            //view.OnLayerDelete += View_OnLayerDelete;
            panel.Controls.Add(view);// = view;


            //载入建筑资源
            this.LoadResource();

            MangoMap mapss = (MangoMap)mapitems[1].Tag;

            LoadMap(mapss);
        }
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
            AppSettingsReader reader = new AppSettingsReader();
            var API_GET_MAP = (string)reader.GetValue("API_GET_MAP", typeof(string));
            MangoMap[] maps = MangoMapLibrary.Api.Util.GetMapList(SERVER_URL_BASE + API_GET_MAP);
          
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
            Device[] _deviceList = MangoMapLibrary.Api.Util.GetDeviceList(SERVER_URL_BASE + API_GET_ALL_DEVICE_LIST);
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
                    deviceList.Add(device.id, device);
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

        private void InitDeviceType(string API_GET_ALL_DEVICE_TYPE)
        {
            DeviceType[] deviceList = MangoMapLibrary.Api.Util.GetDeviceTypeList(SERVER_URL_BASE + API_GET_ALL_DEVICE_TYPE);
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
                        type.deviceImage = MangoMapLibrary.Util.LoadImage(IMAGE_URL_BASE + type.deviceImg);
                        type.checkedImage = MangoMapLibrary.Util.LoadImage(IMAGE_URL_BASE + type.checkedImg);
                        type.alarmImage = MangoMapLibrary.Util.LoadImage(IMAGE_URL_BASE + type.alarmImg);
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show("载入图片失败，类型：" + type.deviceTypeName);
                    }

                    deviceTypeList.Add(type.deviceTypeCode, type);
                }
            }

        }
        private void LoadMap(MangoMap map)
        {
            AppSettingsReader reader = new AppSettingsReader();
            var API_GET_MAP_LAYERS = (string)reader.GetValue("API_GET_MAP_LAYERS", typeof(string));
            MangoLayer[] layers = MangoMapLibrary.Api.Util.GetLayerList(SERVER_URL_BASE + API_GET_MAP_LAYERS, map.id);
            if (layers == null)
            {
                System.Windows.MessageBox.Show("读取层数据失败!");
                return;
            }

            // 添加设备
            var API_GET_MAP_ELEMENTS = (string)reader.GetValue("API_GET_MAP_ELEMENTS", typeof(string));
            MangoLayerElement[] elements = MangoMapLibrary.Api.Util.GetMapElements(SERVER_URL_BASE + API_GET_MAP_ELEMENTS, map.id);

            this.view.LoadMap(map, layers, elements);

            //刷新已部署设备列表
           // InitDeployedDeviceList();
        }
        // GMapControl map = new GMapControl();"‪C:\\Users\\mangyu\\Desktop\\捕获.PNG"
        // void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        // {
        //     Point clickPoint = e.GetPosition(map);
        //     PointLatLng point = map.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
        //     GMapMarker marker = new GMapMarker(point);
        //     map.Markers.Add(marker);
        // }

        // private void MapControl_Loaded(object sender, RoutedEventArgs e)
        // {
        //     myGrid.Children.Add(map);
        //     Thread thread = new Thread(run);
        //     thread.Start();
        // }

        // private void run()
        // {

        //     this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
        //(ThreadStart)delegate ()
        //{
        //    try
        //    {
        //        System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("ditu.google.cn");
        //    }
        //    catch
        //    {
        //        map.Manager.Mode = AccessMode.CacheOnly;
        //        MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }

        //    map.MapProvider = GMapProviders.GoogleChinaTerrainMap; //google china 地图
        //    map.MinZoom = 16;  //最小缩放
        //    map.MaxZoom = 30; //最大缩放
        //    map.Zoom = 5;     //当前缩放
        //    map.ShowCenter = false; //不显示中心十字点
        //    map.DragButton = MouseButton.Left; //左键拖拽地图
        //    map.Position = new PointLatLng(31.306710, 121.493781); //地图中心位置：上海财经

        //    map.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
        //});
        // }
    }
}
