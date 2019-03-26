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
        private MapView view;
        private Dictionary<string, DeviceType> deviceTypeList = new Dictionary<string, DeviceType>();
        private Dictionary<int, Device> deviceList = new Dictionary<int, Device>();
        public MapControl()
        {
            InitializeComponent();
            this.Loaded += MapControl_Loaded;
        }
        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitMapList();
            InitDeviceType(AppConfig .GetAllDeviceType);
            InitDeviceList(AppConfig .GetAllDeviceList);

            //初始化地图
            view = new MapView(AppConfig.MapPath, MapView.RunningMode.VIEW);            //以视图模式运行

            view.InitDeviceTypeList(this.deviceTypeList);
            view.Dock = DockStyle.Fill;
            //view.OnElementMoveIn += View_OnElementMoveIn;
            //view.OnElementChanged += View_OnElementChanged;
            //view.OnLayerChanged += View_OnLayerChanged;                     //MangoLayer属性变化
            panel.Controls.Add(view);

            //载入建筑资源
            this.LoadResource();
            GlobalVariable.IsMapLoaded = true;
        }


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
                new PropertyMetadata(0, (s, e) =>
                {
                    var mdp = s as MapControl;
                    if (mdp != null)
                    {
                        try
                        {
                            int sid = (int)e.NewValue;
                            mdp.LoadSelectedSchoolMap(sid);
                        }
                        catch
                        {

                        }
                    }

                }));


        /// <summary>
        /// 根据sid加载地图
        /// </summary>
        /// <param name="sid">校区</param>
        public void LoadSelectedSchoolMap(int sid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
            MangoMap[] map = MangoMapLibrary.Api.Util.GetMapList(url);
            MangoMap[] results = map.Where(x => x.id == sid).ToArray();
            LoadMap(results[0]);
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
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("载入图片失败，类型：" + type.deviceTypeName);
                    }
                    deviceTypeList.Add(type.deviceTypeCode, type);
                }
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
    }
}
