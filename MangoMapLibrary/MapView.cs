using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpMap.Layers;
using SharpMap.Data.Providers;
using SharpMap.Forms;
using SharpMap;
using GeoAPI.Geometries;
using MangoMapLibrary.Api;
using System.Threading;
using System.IO;
using Newtonsoft.Json;

namespace MangoMapLibrary
{
    #region Delegate Type
    public delegate void OnElementDelete(AbstractLayerElement element);
    public delegate void OnElementRemove(AbstractLayerElement element);
    public delegate void OnElementAdd(AbstractLayerElement element);
    public delegate void OnElementClick(object sender,Point point);
    public delegate void OnElementMoveIn(AbstractLayerElement element);
    public delegate void OnElementChanged(AbstractLayerElement element);
    public delegate void OnLayerChanged(MangoLayer layer);
    public delegate void OnLayerCreate(MangoLayer layer);
    public delegate void OnLayerDelete(MangoLayer layer);

    public delegate void OnElementMove(MangoLayer layer);

    #endregion

    public partial class MapView : UserControl,IDisposable 
    {
        public enum RunningMode
        {
            VIEW,
            EDITOR
        }

        public DeviceLayer deviceLayer;
        //private Thread alarmThread;
        private bool running = true;
        private string MAP_PATH;
        private RunningMode Mode { get; }
        private Dictionary<string, DeviceType> deviceTypeList;

        public event OnElementDelete OnElementDelete;
        public event OnElementDelete OnElementRemove;
        public event OnElementAdd OnElementAdd;
        public event OnElementClick OnElementClick;
        public event OnElementMoveIn OnElementMoveIn;
        public event OnLayerChanged OnLayerChanged;
        public event OnLayerCreate OnLayerCreate;
        public event OnLayerDelete OnLayerDelete;
        public event OnElementChanged OnElementChanged;

        
        public MapView(string MAP_PATH, RunningMode mode)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.MAP_PATH = MAP_PATH;
            Mode = mode;
            DoubleBuffered = true;
            mapBox1.ActiveTool = MapBox.Tools.Pan;
            //alarmThread = new Thread(AlarmThreadRunning);
            //alarmThread.Start();
            RefreshDateTimeAsync();
        }

        public new void Dispose()
        {
            mapBox1.Map.Dispose();
            mapBox1.Dispose();
            mapZoomToolStrip1.Dispose();
        }


        private async void RefreshDateTimeAsync()
        {
            bool drawFlag = false;
            while (running)
            {
                if (deviceLayer != null)
                {
                    try
                    {
                        deviceLayer.RenderAlarm(this.mapBox1.CreateGraphics(), this.mapBox1.Map, drawFlag);
                    }
                    catch (Exception e) { }
                }
                drawFlag = !drawFlag;
                await Task.Delay(1000);
            }
        }

        private void AlarmThreadRunning()
        {

            bool drawFlag = false;
            while (running)
            {
                if (deviceLayer != null)
                {
                    try
                    {
                        deviceLayer.RenderAlarm(this.mapBox1.CreateGraphics(), this.mapBox1.Map, drawFlag);
                    }
                    catch (Exception e) { }
                }
                drawFlag = !drawFlag;
                Thread.Sleep(500);
            }
        }

        public DeviceLayer AddDeviceLayer(string name)
        {
            deviceLayer = new DeviceLayer();
            mapBox1.Map.Layers.Add(deviceLayer);
            return deviceLayer;
        }

        public void RemoveLayer(string name)
        {
            ILayer layer = mapBox1.Map.GetLayerByName(name);
            if (layer != null)
            {
                mapBox1.Map.Layers.Remove(layer);
                if (OnLayerDelete != null)
                {
                    OnLayerDelete.Invoke((layer as AbstractMangoLayer).MangoLayer());
                }
            }
        }

        public Map Map()
        {
            return mapBox1.Map;
        }

        internal void InvokeOnLayerChanged(AbstractMangoLayer layer)
        {
            layer.RenderPrepare();
            if (this.OnLayerChanged != null)
            {
                this.OnLayerChanged.Invoke(layer.MangoLayer());
            }
            this.Refresh();
        }

        public ILayer LoadLayer(MangoLayer layer)
        {
            LayerType type = (LayerType)layer.type;
            switch (type)
            {
                case LayerType.SHAPE_FILE:
                    return LoadShapeFileLayer(layer);
                case LayerType.GDI_IMAGE:
                    return LoadGDILayer(layer);
            }
            return null;
        }

        private ILayer LoadGDILayer(MangoLayer layer)
        {
            if (layer.filenamePrefix == null || layer.filenamePrefix == "")
            {
                return null;
            }
            string filename = MAP_PATH + layer.filenamePrefix;
            if (!System.IO.File.Exists(filename))
            {
                return null;
            }

            MangoGDILayer gdiImageLayer = new MangoGDILayer(layer, filename);
            this.mapBox1.Map.Layers.Add(gdiImageLayer);
            return gdiImageLayer;
        }

        public ILayer AddLabelLayer(string name, MangoVectorLayer layer)
        {
            MangoLabelLayer lLayer = new MangoLabelLayer(layer.Layer, name);
            lLayer.DataSource = (layer).DataSource;
            lLayer.Enabled = true;
            lLayer.LabelColumn = "Name";

            lLayer.RenderPrepare();
            mapBox1.Map.Layers.Add(lLayer);
            return lLayer;
        }

        public void InitDeviceTypeList(Dictionary<string, DeviceType> deviceTypeList)
        {
            this.deviceTypeList = deviceTypeList;
            //初始化添加设备菜单
          //  menuItemAddDevice.DropDownItems.Clear();
            foreach (DeviceType type in deviceTypeList.Values)
            {
                if (type.deviceClass.Equals("Camera"))
                {
                    continue;
                }
                ToolStripMenuItem item = new ToolStripMenuItem() { Text = type.deviceTypeName, Tag = type };
                item.Click += Item_Click;
               // menuItemAddDevice.DropDownItems.Add(item);
            }

            //初始化设备过滤菜单
            deviceTypeListComboBox.Items.Clear();
            deviceTypeListComboBox.Items.Add("全部");
            foreach (DeviceType type in deviceTypeList.Values)
            {
                deviceTypeListComboBox.Items.Add(type.deviceTypeName);
            }

        }

        private void Item_Click(object sender, EventArgs e)
        {
            var t = mapZoomToolStrip1.Text;
            var T = mapBox1.Text;
            var IT = mapZoomToolStrip1.Items[10].Text;

            DeviceType type = (sender as ToolStripMenuItem).Tag as DeviceType;
            if (type.deviceClass == "ElectronicFence")
            {
                drawFence = new DrawFence();
                drawFence.type = type.deviceTypeCode;
                return;
            }

            Coordinate p = mapBox1.Map.ImageToWorld(addMemuPoint);
            MangoLayerElement ele = new MangoLayerElement(currentMap.id, p, type.deviceTypeCode);
            LayerDeviceElement element = new LayerDeviceElement(ele, type.ToIconSet());
            deviceLayer.AddLayerElement(element);
            Refresh();

            if (OnElementAdd != null)
            {
                OnElementAdd.Invoke(element);
            }
        }

        public ILayer LoadShapeFileLayer(MangoLayer layer)
        {
            if (string.IsNullOrEmpty(layer.filenamePrefix))
                return null;

            string filename = MAP_PATH + layer.filenamePrefix; // 默认载入文件名, .shp 为层文件名，每个图层包括前缀名+ (.shp,.dbf,.sbx 等若干文件)
            if (!File.Exists(filename))
                return null;

            ShapeFile shapeFileData = new ShapeFile(filename);
            MangoVectorLayer shapeFileLayer = new MangoVectorLayer(layer, shapeFileData);
            shapeFileLayer.RenderPrepare();
            this.mapBox1.Map.Layers.Add(shapeFileLayer);
            return shapeFileLayer;
        }

        public override void Refresh()
        {
            base.Refresh();
            mapBox1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LayersForm form = new LayersForm(this);
            form.ShowDialog();
        }

        private Point GetLocation(double r, double angle, Point loc)
        {
            Point target = new Point(0, 0);
            double ang = (angle - 45) / 180 * Math.PI;
            target.X = (int)(loc.X - r * Math.Cos(ang));
            target.Y = (int)(loc.Y + r * Math.Sin(ang));
            return target;
        }

        private void MenuItemCamera_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap("D:\\a\\a.bmp");
            Graphics g = mapBox1.CreateGraphics();
            // Create parallelogram for drawing image.
            PointF ulCorner = new PointF(addMemuPoint.X, addMemuPoint.Y);
            PointF o = new PointF(ulCorner.X + image.Width / 2, ulCorner.Y + image.Width / 2);
            PointF a = MathUtil.GetPointByAngle(ulCorner, o, 60);
            PointF b = MathUtil.GetPointByAngle(ulCorner, o, 60 + 90);
            PointF c = MathUtil.GetPointByAngle(ulCorner, o, 60 + 270);
            // Draw image to screen.+
            g.DrawImage(image, new PointF[] { a, b, c });
        }

        private PointF addMemuPoint;        //添加设备时鼠标坐标
        private DrawFence drawFence;          //围栏划线

        public MangoMap currentMap { get; private set; }

        private void mapMouseMenu_Opened(object sender, EventArgs e)
        {
            if (drawFence != null)
            {
                OnAddFenceFinished();
                drawFence = null;
                mapBox1.Refresh();
                return;
            }

            //mapMouseMenu.Enabled = deviceLayer != null;
            Point p = mapBox1.PointToClient(MousePosition);
            addMemuPoint = new PointF(p.X, p.Y);

            AbstractLayerElement device = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if (device != null)
            {
                //MenuItemDelete.Enabled = (device.Type != "@Building")&& (device.Type != "@BuildingIcon");
               // MenuItemRemove.Enabled=device.Type!= "TensionFence" && device.Type != "VibrantCable " && device.Type != "InfraredRadiationDetector" && device.Type != "LeakyCable";
            }
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device = deviceLayer.Get(addMemuPoint, mapBox1.Map);
            if (device != null)
            {
                DeleteElement(device);
            }
        }

        private void MenuItemRemove_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if (device != null)
                RemoveElement(device);
        }

        public void DeleteElement(AbstractLayerElement element)
        {
            deviceLayer.Remove(element);
            mapBox1.Refresh();
            if (OnElementDelete != null)
                OnElementDelete.Invoke(element);
        }

        public void RemoveElement(AbstractLayerElement element)
        {
            deviceLayer.Remove(element);
            mapBox1.Refresh();
            if (OnElementRemove != null)
            {
                OnElementRemove.Invoke(element);
            }
        }

        public void Zoom(int level)
        {
            mapBox1.Map.Zoom = level;
        }

        public void Center(Coordinate p)
        {
            this.mapBox1.Map.Center = p;
        }


        /// <summary>
        /// Place the device at a height of 1/2 on the screen 2/3
        /// </summary>
        /// <param name="element">The device to be moved</param>
        public void MoveToTwoThirds(AbstractLayerElement element)
        {
            if (element != null)
            {
                mapBox1.Map.Center = new Coordinate(element.p.X - (1d / 6d) * mapBox1.Map.Envelope.Width,
                mapBox1.Map.Envelope.Centre.Y);
                Refresh();
            }
        }


        private void OnAddFenceFinished()
        {
            if (drawFence.lines.Count == 0)
            {
                return;
            }
            IconExtParamFence ext = new IconExtParamFence();
            ext.type = drawFence.type;
            ext.points = new PointF[drawFence.lines.Count];
            for (int i = 0; i < ext.points.Length; i++)
            {
                Coordinate p = (drawFence.lines[i]);
                ext.points[i] = new PointF((float)p.X, (float)p.Y);
            }
            Coordinate p1 = (drawFence.lines[0]);
            LayerElementFence fence = new LayerElementFence(new MangoLayerElement(this.currentMap.id, p1, ElementType.TYPE_FENCE), deviceTypeList[drawFence.type].ToIconSet(), ext);
            this.deviceLayer.AddLayerElement(fence);
            this.mapBox1.Refresh();
            if (this.OnElementAdd != null)
            {
                this.OnElementAdd.Invoke(fence);
            }
        }

        private void DrawAddFence()
        {
            Graphics g = this.mapBox1.CreateGraphics();
            DeviceType type = this.deviceTypeList[drawFence.type];
            foreach(Coordinate c in drawFence.lines)
            {
                PointF p = this.mapBox1.Map.WorldToImage(c);
                g.DrawImage(type.deviceImage, p.X - 16, p.Y - 16,32,32);
            }
        }

        private void AddFence()
        {
            Point p = this.mapBox1.PointToClient(Control.MousePosition);
            PointF c = new PointF(p.X, p.Y);
            this.drawFence.Add(this.mapBox1.Map.ImageToWorld(c));
            this.DrawAddFence();
        }

        private ElementIconSet GetIconSet(string type)
        {
            if (!this.deviceTypeList.ContainsKey(type))
                return null;
            return this.deviceTypeList[type].ToIconSet();
        }

        public void LoadMap(MangoMap map, MangoLayer[] layers, MangoLayerElement[] elements)
        {
            Clear();
            SetBackgroundColor(Color.FromArgb(map.backgroundColor));
            SetMap(map);
            Zoom(map.defaultZoomLevel);
            deviceTypeListComboBox.SelectedIndex = 0;

            List<ILayer> _layers = new List<ILayer>();
            foreach (MangoLayer layer in layers)
            {
                ILayer mapLayer = LoadLayer(layer);
                if (mapLayer == null)
                {
                    continue;
                }
                _layers.Add(mapLayer);
            }

            // 添加设备层
            DeviceLayer deviceLayer = AddDeviceLayer("DeviceLayer");

            AddElement(elements, deviceLayer);

            //添加标签层
            foreach (ILayer _layer in _layers)
            {
                //当前图层是否有标签层
                if (_layer is MangoVectorLayer)
                {
                    MangoLayer layer = (_layer as MangoVectorLayer).Layer;
                    if (layer.labelEnabled == 1)
                    {
                        AddLabelLayer(layer.name + "(标签层)", _layer as MangoVectorLayer);
                    }
                }
            }

            Center(new Coordinate(map.defaultLatitude, map.defaultLongitude));
            Refresh();
        }

        private void AddElement(MangoLayerElement[] elements, DeviceLayer deviceLayer)
        {
            if (elements != null)
            {
                try
                {
                    foreach (MangoLayerElement ele in elements)
                    {
                        switch (ele.deviceTypeCode)
                        {
                            case ElementType.TYPE_BUILDING:
                            case ElementType.TYPE_BUILDING_ICON:
                                {
                                    IconExtParamBuilding ext = JsonConvert.DeserializeObject<IconExtParamBuilding>(ele.iconExt);

                                    var baseDic = AppDomain.CurrentDomain.BaseDirectory;
                                    if (!File.Exists(baseDic + ext.filename))
                                    {
                                        MessageBox.Show($"未找到 {ext.filename} 文件");
                                        continue;
                                    }

                                    Bitmap bitmap = new Bitmap(ext.filename);
                                    ElementIconSet set = new ElementIconSet(bitmap, null, null);
                                    LayerElementBuilding building = new LayerElementBuilding(ele, set, ext);

                                    deviceLayer.AddLayerElement(building);
                                    break;
                                }
                            case ElementType.FENCE_InfraredRadiationDetector:
                            case ElementType.FENCE_LeakyCable:
                            case ElementType.FENCE_TensionFence:
                            case ElementType.FENCE_VibrantCable:
                                {
                                    if (ele.iconExt == null)
                                        continue;

                                    IconExtParamFence ext = JsonConvert.DeserializeObject<IconExtParamFence>(ele.iconExt);
                                    LayerElementFence fence = new LayerElementFence(ele, this.GetIconSet(ext.type), ext);
                                    deviceLayer.AddLayerElement(fence);
                                    break;
                                }
                            case "":
                            case null:
                                continue;
                            default:
                                {
                                    LayerDeviceElement element = new LayerDeviceElement(ele, GetIconSet(ele.deviceTypeCode));
                                    deviceLayer.AddLayerElement(element);
                                    break;
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
        }

        public void mapBox1_Click(object sender, EventArgs e) => ClickMap(false);

        public void ClickMap(bool isClear=true)
        {
            if (drawFence != null)
            {
                AddFence();
                return;
            }
            if (deviceLayer == null)
                return;

            if (isClear)
                deviceLayer.ClearSelected();

            Point p = mapBox1.PointToClient(MousePosition);
            PointF c = new PointF(p.X, p.Y);
            AbstractLayerElement device = this.deviceLayer.Get(c, this.mapBox1.Map);
            if (device != null)
            {
                device.status = ICON_STATUS.CHECKED;
                if (OnElementClick != null)
                {
                    OnElementClick.Invoke(device, p);
                }
            }
            mapBox1.Refresh();
        }

        /// <summary>
        /// 世界坐标转换为屏幕坐标
        /// </summary>
        /// <param name="p"></param>
        /// <param name="point"></param>
        public void RenderToScreen(Coordinate p,out PointF point)
        {
            //世界坐标转换为屏幕坐标
            var _p = mapBox1.Map.WorldToImage(p,true);
            point = _p;
        }


        /// <summary>
        /// 设置消防道路
        /// </summary>
        /// <param name="map">MapView</param>
        /// <param name="isSet">是否设置 true:设置 false:取消设置</param>
        public void SetFireRoad(bool isSet)
        {
          
            foreach (ILayer layer in Map().Layers)
            {
                try
                {
                    if (layer.LayerName.Equals("消防道路") && layer is MangoVectorLayer)
                    {
                        MangoVectorLayer mangoLayer = (MangoVectorLayer)layer;
                        mangoLayer.Layer.color = (isSet == true) ? Color.Red.ToArgb().ToString() : Color.White.ToArgb().ToString();
                        InvokeOnLayerChanged(mangoLayer);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        private void mapBox1_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            Point p = this.mapBox1.PointToClient(Control.MousePosition);
            PointF c = new PointF(p.X, p.Y);
            Coordinate coor = this.mapBox1.Map.ImageToWorld(c);

            var xCoor = Math.Round(coor.X, 3);
            var yCoor = Math.Round(coor.Y, 3);

            label_coor.Text = $"坐标:{xCoor},{yCoor}";

            if (this.Mode == RunningMode.VIEW)
            {
                if (this.deviceLayer != null)
                {
                    AbstractLayerElement device = this.deviceLayer.Get(c, this.mapBox1.Map);
                    if (device != null)
                    {
                        if (OnElementMoveIn != null)
                        {
                            OnElementMoveIn.Invoke(device);
                        }
                    }
                }
            }
        }

        private void DrawDragDropEffect(IDataObject dragEventData)
        {
            Bitmap bitmap = null;
            object data = dragEventData.GetData(typeof(LayerDeviceElement));
            if (data != null)
            {
                bitmap = (data as LayerDeviceElement).iconSet.icon;
                Point p = this.mapBox1.PointToClient(Control.MousePosition);
                using (Graphics g = this.mapBox1.CreateGraphics())
                {
                    g.DrawImage(bitmap, p.X - LayerDeviceElement.ICON_RADIUS, p.Y - LayerDeviceElement.ICON_RADIUS, LayerDeviceElement.ICON_RADIUS * 2, LayerDeviceElement.ICON_RADIUS * 2);
                };
            }
            else
            {
                data = dragEventData.GetData(typeof(LayerElementBuilding));
                if (data != null)
                {
                    bitmap = (data as LayerElementBuilding).iconSet.icon;
                    Point p = this.mapBox1.PointToClient(Control.MousePosition);
                    using (Graphics g = this.mapBox1.CreateGraphics())
                    {
                        g.DrawImage(bitmap, p.X - bitmap.Width / 2, p.Y - bitmap.Height / 2);
                        //g.DrawImage(new Bitmap(), p.X - bitmap.Width / 2, p.Y - bitmap.Height / 2);
                    };
                }
            }

            mapBox1.Refresh();
        }

        public List<AbstractLayerElement> GetDeviceList()
        {
            if (deviceLayer == null)
            {
                return null;
            }
            return deviceLayer.GetAllElements();
        }

        public void SetMap(MangoMap map) => currentMap = map;

        private void MenuItemProperties_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if (device != null)
            {
                ShowElementProperty(device);
            }
        }

        public void SetBackgroundColor(Color color)
        {
            try
            {
                mapBox1.BackColor = color;
            }
            catch { }
        }

        public void Clear()
        {
            mapBox1.Map.Layers.Clear();
            Refresh();
        }

        private void MapView_DragDrop(object sender, DragEventArgs e)
        {
            if (this.deviceLayer == null)
            {
                return;
            }
            
            object data = e.Data.GetData(typeof(LayerDeviceElement));
            if(data != null)
            {
                OnDragDropElement(data as LayerDeviceElement);
            }
            else
            {
                data = e.Data.GetData(typeof(LayerElementBuilding));
                if(data != null)
                {
                    LayerElementBuilding building = data as LayerElementBuilding;
                    building.Ext.zoomLevel = this.mapBox1.Map.Zoom;
                    building.Ext.width = this.mapBox1.Map.Size.Width;
          
                    OnDragDropElement(building);
                }
            }
        }

        /// <summary>
        /// 地图缩放比例
        /// </summary>
        public double MapZoom
        {
            get => mapBox1.Map.Zoom;
            set
            {
                mapBox1.Map.Zoom = value;
                mapBox1.Refresh();
            }
        }

        private void OnDragDropElement(AbstractLayerElement device)
        {
            device.Ele.mapId = this.currentMap.id;

            Point tmp = this.mapBox1.PointToClient(Control.MousePosition);
            PointF addMemuPoint = new PointF(tmp.X, tmp.Y);
            Coordinate p = this.mapBox1.Map.ImageToWorld(addMemuPoint);
            device.p = p;
            this.deviceLayer.AddLayerElement(device);
            this.mapBox1.Refresh();
            if (this.OnElementAdd != null)
            {
                this.OnElementMoveIn.Invoke(device);
            }
        }

        private void MapView_DragOver(object sender, DragEventArgs e)
        {
           // e.Effect = DragDropEffects.All;
           // this.DrawDragDropEffect(e.Data);
        }

        public void Close()
        {
            running = false;
            //if (alarmThread != null)
            //{
            //    alarmThread.Abort();
            //}
        }

        private void MapView_DragEnter(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.Copy;
        }

        public void Center(AbstractLayerElement element)
        {
            if (element!=null)
            {
                mapBox1.Map.Center = element.p;
                Refresh();
            }
        }

        private void 闪烁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var device = deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            FlickerOne(device);
        }



        /// <summary>
        /// 设备闪烁
        /// </summary>
        /// <param name="element">设备</param>
        public void FlickerOne(AbstractLayerElement element)
        {
            if (element != null)
            {
                (element).alarm = !(element).alarm;
            }
        }

        public void StopFlickerOne(AbstractLayerElement element)
        {
            if (element != null)
            {
                (element).alarm = false;
            }
        }

        public void FlickerOne(AbstractLayerElement element,bool state)
        {
            if (element != null)
            {
                (element).alarm = state;
            }
        }




        public void CreateLayer(string filename)
        {
            MangoLayer layer = new MangoLayer();
            layer.type = (int)(filename.Contains(".shp") ? LayerType.SHAPE_FILE : LayerType.GDI_IMAGE);

            
            layer.name = Path.GetFileNameWithoutExtension(filename);
            layer.filenamePrefix = Path.GetFileName(filename);//此处仅记录文件名和扩展名，基于整个应用默认将地图路径作为可配置的原因，切方便不同客户端部署
            layer.mapId = this.currentMap.id;
            this.LoadLayer(layer);
            this.Refresh();
            if(this.OnLayerCreate != null)
            {
                this.OnLayerCreate.Invoke(layer);
            }
            
        }
                
        public void ShowElementProperty(AbstractLayerElement element)
        {
            DeviceProperty p = new DeviceProperty(element);
            if (p.ShowDialog() == DialogResult.OK)
            {
                Refresh();
                if (OnElementChanged != null)
                {
                    OnElementChanged.Invoke(element);
                }
            }
        }
		 /// <summary>
        /// 打开或关闭所有设备标签
        /// </summary>
        /// <param name="isShow">true:开,false:关</param>
        public void SwitchDeviceTag(bool isShow)
        {
            Global.ShowMapTag = isShow;
            mapBox1.Refresh();
        }

        /// <summary>
        /// Lock the viewport
        /// </summary>
        /// <param name="isLock">true is lock, false is unlock</param>
        private void LockMapViewPort(bool isLock)
        {
            var lockButton = mapZoomToolStrip1.Items[14] as ToolStripButton;
            lockButton.Checked = isLock;
        }

        /// <summary>
        /// 鼠标坐标
        /// </summary>
        public Coordinate MouseCoor
        {
            get { return MouseCoor; }
            set
            {
                MouseCoor = value;
            }
        }

        public void SelectOne(AbstractLayerElement element)
        {
            deviceLayer.ClearSelected();
            if (element!=null)
                element.status = ICON_STATUS.CHECKED;
        }

        public void UnSelectOne(AbstractLayerElement element)
        {
            if (element!=null)
                element.status = ICON_STATUS.NORMAL;
        }

        private void mapBox1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("Zoom " + mapBox1.Map.Zoom);
            Console.WriteLine(mapBox1.Map.MapScale);
        }

        private DeviceType GetDeviceTypeByName(string name)
        {
            foreach (DeviceType type in deviceTypeList.Values)
            {
                if (type.deviceTypeName.Equals(name))
                {
                    return type;
                }
            }
            return null;
        }

        private void deviceTypeListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (deviceLayer == null) return;

            if (deviceTypeListComboBox.SelectedIndex <= 0)
            {
                deviceLayer.SetRenderType(null);
            }
            else
            {
                DeviceType type = GetDeviceTypeByName(this.deviceTypeListComboBox.SelectedItem as string);
                if (type != null)
                {
                    List<string> all = new List<string>();
                    all.Add(type.deviceTypeCode);
                    deviceLayer.SetRenderType(all);
                }
                else
                {
                    deviceLayer.SetRenderType(null);
                }
            }
            Refresh();
        }

        public void SelectedDeviceType(List<string> typecode)
        {
            if (this.deviceLayer == null) return;
            this.deviceLayer.SetRenderType(typecode);
            this.Refresh();
        }

        public void SetCodeList(List<string> codeList)
        {
            if (this.deviceLayer == null) return;
            this.deviceLayer.SetCodeList(codeList);
            this.Refresh();
        }
		

      
    }

    #region Draw Fence
    public class DrawFence
    {
        public List<Coordinate> lines = new List<Coordinate>();
        public string type;

        public void Add(Coordinate point) => lines.Add(point);
    }
    #endregion
}
