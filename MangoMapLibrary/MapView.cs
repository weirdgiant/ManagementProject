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
    public delegate void OnElementDelete(AbstractLayerElement element);
    public delegate void OnElementAdd(AbstractLayerElement element);
    public delegate void OnElementClick(AbstractLayerElement element);
    public delegate void OnElementMoveIn(AbstractLayerElement element);
    public delegate void OnElementChanged(AbstractLayerElement element);
    public delegate void OnLayerChanged(MangoLayer layer);
    public delegate void OnLayerCreate(MangoLayer layer);
    public delegate void OnLayerDelete(MangoLayer layer);

    public partial class MapView : UserControl
    {
        public enum RunningMode
        {
            VIEW,
            EDITOR
        }

        private DeviceLayer deviceLayer;
        public event OnElementDelete OnElementDelete;
        public event OnElementAdd OnElementAdd;
        public event OnElementClick OnElementClick;
        public event OnElementMoveIn OnElementMoveIn;
        public event OnLayerChanged OnLayerChanged;
        public event OnLayerCreate OnLayerCreate;
        public event OnLayerDelete OnLayerDelete;
        public event OnElementChanged OnElementChanged;

        private Dictionary<string, DeviceType> deviceTypeList;
        private Thread alarmThread;
        private bool running    = true;
        private string MAP_PATH;
        private RunningMode Mode { get; }

        public MapView(string MAP_PATH,RunningMode mode)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.MAP_PATH = MAP_PATH;
            this.Mode = mode;
            this.DoubleBuffered = true;
            this.mapBox1.ActiveTool = MapBox.Tools.Pan;
            this.alarmThread = new Thread(AlarmThreadRunning);
            this.alarmThread.Start();
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
                    }catch(Exception e) { }
                    
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
            ILayer layer    = this.mapBox1.Map.GetLayerByName(name);
            if(layer != null)
            {
                this.mapBox1.Map.Layers.Remove(layer);
                if (this.OnLayerDelete != null)
                {
                    this.OnLayerDelete.Invoke((layer as AbstractMangoLayer).MangoLayer());
                }
            }
        }

        public Map Map()
        {
            return this.mapBox1.Map;
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

            MangoGDILayer gdiImageLayer = new MangoGDILayer(layer,filename);
            this.mapBox1.Map.Layers.Add(gdiImageLayer);
            return gdiImageLayer;
        }

        public ILayer AddLabelLayer(string name,MangoVectorLayer layer)
        {
            MangoLabelLayer lLayer = new MangoLabelLayer(layer.Layer,name);
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
            foreach(DeviceType type in deviceTypeList.Values)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = type.deviceTypeName;
                item.Tag = type;
                item.Click += Item_Click;
                this.menuItemAddDevice.DropDownItems.Add(item);
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            DeviceType type = (sender as ToolStripMenuItem).Tag as DeviceType;
            if(type.deviceClass == "ElectronicFence")
            {
                this.drawFence = new DrawFence();
                this.drawFence.type = type.deviceTypeCode;
                return;
            }


            Coordinate p = this.mapBox1.Map.ImageToWorld(this.addMemuPoint);
            MangoLayerElement ele = new MangoLayerElement(this.currentMap.id,p, type.deviceTypeCode);
            LayerElement element = new LayerElement(ele, type.ToIconSet());
            this.deviceLayer.AddLayerElement(element);
            this.Refresh();

            if(this.OnElementAdd != null)
            {
                this.OnElementAdd.Invoke(element);
            }
        }

        public ILayer LoadShapeFileLayer(MangoLayer layer)
        {
            if (layer.filenamePrefix == null || layer.filenamePrefix == "")
            {
                return null;
            }
            string filename = MAP_PATH + layer.filenamePrefix;     // 默认载入文件名, .shp 为层文件名，每个图层包括前缀名+ (.shp,.dbf,.sbx 等若干文件)
            if (!System.IO.File.Exists(filename))
            {
                return null;
            }
            ShapeFile shapeFileData = new ShapeFile(filename);
            MangoVectorLayer shapeFileLayer = new MangoVectorLayer(layer, shapeFileData);
            shapeFileLayer.RenderPrepare();
            this.mapBox1.Map.Layers.Add(shapeFileLayer);
            return shapeFileLayer;
            
        }

        public override void Refresh()
        {
            base.Refresh();
            
            this.mapBox1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            LayersForm form = new LayersForm(this);
            form.ShowDialog();
        }

        private void itemAddLabel_Click(object sender, EventArgs e)
        {
            
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
            Graphics g  = this.mapBox1.CreateGraphics();
            // Create parallelogram for drawing image.
            PointF ulCorner = new PointF(addMemuPoint.X, addMemuPoint.Y);
            PointF o = new PointF(ulCorner.X + image.Width / 2, ulCorner.Y + image.Width / 2);
            PointF a = MathUtil.GetPointByAngle(ulCorner, o, 60);
            PointF b = MathUtil.GetPointByAngle(ulCorner, o, 60+90);
            PointF c = MathUtil.GetPointByAngle(ulCorner, o, 60+270);
            // Draw image to screen.+
            g.DrawImage(image, new PointF[] { a, b, c });

        }

        private PointF addMemuPoint;        //添加设备时鼠标坐标
        private DrawFence drawFence;          //围栏划线

        public MangoMap currentMap { get; private set; }

        private void mapMouseMenu_Opened(object sender, EventArgs e)
        {
            if(drawFence != null)
            {
                this.OnAddFenceFinished();
                this.drawFence = null;
                this.mapBox1.Refresh();
                return;
            }

            this.mapMouseMenu.Enabled = this.deviceLayer != null;

            Point p      = this.mapBox1.PointToClient(Control.MousePosition);
            addMemuPoint = new PointF(p.X, p.Y);
        }

        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device  = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if(device != null)
            {
                this.DeleteElement(device);
            }
        }

        public void DeleteElement(AbstractLayerElement element)
        {
            this.deviceLayer.Remove(element);
            this.mapBox1.Refresh();
            if (this.OnElementDelete != null)
            {
                this.OnElementDelete.Invoke(element);
            }
        }

        public void Zoom(int level)
        {
            this.mapBox1.Map.Zoom = level;
        }

        public void Center(Coordinate p)
        {
            this.mapBox1.Map.Center = p;
        }

        private void OnAddFenceFinished()
        {
            if(drawFence.lines.Count == 0)
            {
                return;
            }
            IconExtParamFence ext = new IconExtParamFence();
            ext.type = drawFence.type;
            ext.points = new PointF[drawFence.lines.Count];
            for(int i = 0; i < ext.points.Length; i++)
            {
                Coordinate p = (drawFence.lines[i]);
                ext.points[i] = new PointF((float)p.X, (float)p.Y);
            }
            Coordinate p1 = (drawFence.lines[0]);
            LayerElementFence fence = new LayerElementFence(new MangoLayerElement(this.currentMap.id,p1, ElementType.TYPE_FENCE), deviceTypeList[drawFence.type].ToIconSet(), ext);
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

        public void LoadMap(MangoMap map,MangoLayer[] layers, MangoLayerElement[] elements)
        {
            this.Clear();
            this.SetBackgroundColor(Color.FromArgb(map.backgroundColor));
            this.SetMap(map);
            this.Zoom(map.defaultZoomLevel);

            List<ILayer> _layers = new List<ILayer>();
            foreach (MangoLayer layer in layers)
            {
                ILayer mapLayer = this.LoadLayer(layer);
                if (mapLayer == null)
                {
                    continue;
                }
                _layers.Add(mapLayer);
            }

            //添加标签层
            foreach (ILayer _layer in _layers)
            {
                //当前图层是否有标签层
                if (_layer is MangoVectorLayer)
                {
                    MangoLayer layer = (_layer as MangoVectorLayer).Layer;
                    if (layer.labelEnabled == 1)
                    {
                        this.AddLabelLayer(layer.name + "(标签层)", _layer as MangoVectorLayer);
                    }
                }

            }

            // 添加设备层
            DeviceLayer deviceLayer = this.AddDeviceLayer("DeviceLayer");
            
            if (elements != null)
            {
                foreach (MangoLayerElement ele in elements)
                {
                    switch (ele.deviceTypeCode)
                    {
                        case ElementType.TYPE_BUILDING:
                            {
                                IconExtParamBuilding ext = JsonConvert.DeserializeObject<IconExtParamBuilding>(ele.iconExt);
                                Bitmap bitmap = new Bitmap(ext.filename);
                                ElementIconSet set = new ElementIconSet(bitmap, null, null);
                                LayerElementBuilding building = new LayerElementBuilding(ele, set, ext);
                                deviceLayer.AddLayerElement(building);
                                break;
                            }
                        case ElementType.TYPE_FENCE:
                            {
                                IconExtParamFence ext = JsonConvert.DeserializeObject<IconExtParamFence>(ele.iconExt);
                                LayerElementFence fence = new LayerElementFence(ele, this.GetIconSet(ext.type), ext);
                                deviceLayer.AddLayerElement(fence);
                                break;
                            }
                        default:
                            {
                                
                                LayerElement element = new LayerElement(ele, GetIconSet(ele.deviceTypeCode));
                                deviceLayer.AddLayerElement(element);
                                break;
                            }

                    }


                }
            }

            this.Center(new Coordinate(map.defaultLatitude, map.defaultLongitude));
            this.Refresh();
        }

        private void mapBox1_Click(object sender, EventArgs e)
        {
            if(this.drawFence != null)
            {
                
                this.AddFence();
                return;
            }
            if(deviceLayer == null)
            {
                return;
            }

            this.deviceLayer.ClearSelected();

            Point p = this.mapBox1.PointToClient(Control.MousePosition);
            PointF c = new PointF(p.X, p.Y);
            AbstractLayerElement device = this.deviceLayer.Get(c, this.mapBox1.Map);
            if (device != null)
            {
                device.status = ICON_STATUS.CHECKED;
                if(this.OnElementClick != null)
                {
                    this.OnElementClick.Invoke(device);
                }
            }
            this.mapBox1.Refresh();
        }

        

        

        private void mapBox1_MouseMove(Coordinate worldPos, MouseEventArgs imagePos)
        {
            Point p = this.mapBox1.PointToClient(Control.MousePosition);
            PointF c = new PointF(p.X, p.Y);
            Coordinate coor =  this.mapBox1.Map.ImageToWorld(c);
            label_coor.Text = "坐标:" + Math.Round(coor.X,3) + "," + Math.Round(coor.Y,3);

            
            if(this.Mode == RunningMode.VIEW)
            {
                if (this.deviceLayer != null)
                {
                    AbstractLayerElement device = this.deviceLayer.Get(c, this.mapBox1.Map);
                    if (device != null)
                    {
                        if (this.OnElementMoveIn != null)
                        {
                            this.OnElementMoveIn.Invoke(device);
                        }
                    }
                }
            }            
        }

        private void DrawDragDropEffect(IDataObject dragEventData)
        {
            Bitmap bitmap = null;
            object data = dragEventData.GetData(typeof(LayerElement));
            if (data != null)
            {
                bitmap = (data as LayerElement).iconSet.icon;
                Point p = this.mapBox1.PointToClient(Control.MousePosition);
                Graphics g = this.mapBox1.CreateGraphics();

                g.DrawImage(bitmap, p.X - LayerElement.ICON_RADIUS, p.Y - LayerElement.ICON_RADIUS, LayerElement.ICON_RADIUS*2, LayerElement.ICON_RADIUS*2);
            }
            else
            {
                data = dragEventData.GetData(typeof(LayerElementBuilding));
                if (data != null)
                {
                    bitmap = (data as LayerElementBuilding).iconSet.icon;
                    Point p = this.mapBox1.PointToClient(Control.MousePosition);
                    Graphics g = this.mapBox1.CreateGraphics();
                    
                    g.DrawImage(bitmap, p.X - bitmap.Width / 2, p.Y - bitmap.Height / 2);
                }
            }

            this.mapBox1.Refresh();
        }

        public List<AbstractLayerElement> GetDeviceList()
        {
            if(this.deviceLayer == null)
            {
                return null;
            }
            return this.deviceLayer.GetAllElements();
        }

        public void SetMap(MangoMap map)
        {
            this.currentMap = map;
        }

        private void MenuItemProperties_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if (device != null)
            {
                this.ShowElementProperty(device);
            }
        }

        public void SetBackgroundColor(Color color)
        {
            try
            {
                this.mapBox1.BackColor = color;
            }
            catch(Exception e)
            {

            }         
        }

        public void Clear()
        {
            this.mapBox1.Map.Layers.Clear();
            this.Refresh();
        }

        private void MapView_DragDrop(object sender, DragEventArgs e)
        {
            if (this.deviceLayer == null)
            {
                return;
            }
            
            object data = e.Data.GetData(typeof(LayerElement));
            if(data != null)
            {
                OnDragDropElement(data as LayerElement);
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

        private void OnDragDropElement(AbstractLayerElement device)
        {
            //mapId
            device.Ele.mapId = this.currentMap.id;

            Point tmp = this.mapBox1.PointToClient(Control.MousePosition);
            PointF addMemuPoint = new PointF(tmp.X, tmp.Y);
            Coordinate p = this.mapBox1.Map.ImageToWorld(addMemuPoint);
            device.p = p;
            this.deviceLayer.AddLayerElement(device);
            this.mapBox1.Refresh();
            if (this.OnElementAdd != null)
            {
                this.OnElementAdd.Invoke(device);
            }
        }

        private void MapView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            this.DrawDragDropEffect(e.Data);
        }

        public void Close()
        {
            this.running = false;
        }



        private void MapView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }


        public void Center(AbstractLayerElement element)
        {
            this.mapBox1.Map.Center = element.p;
            this.Refresh();
        }

        private void 闪烁ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbstractLayerElement device = this.deviceLayer.Get(addMemuPoint, this.mapBox1.Map);
            if (device != null)
            {
                (device).alarm = !(device).alarm;
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
                this.Refresh();
                if (this.OnElementChanged != null)
                {
                    this.OnElementChanged.Invoke(element);
                }
            }
        }

        public void SelectOne(AbstractLayerElement element)
        {
            this.deviceLayer.ClearSelected();
            element.status = ICON_STATUS.CHECKED;
        }

        private void mapBox1_Resize(object sender, EventArgs e)
        {
            Console.WriteLine("Zoom " + this.mapBox1.Map.Zoom);
            Console.WriteLine(this.mapBox1.Map.MapScale);
        }
    }



    public class DrawFence
    {
        public List<Coordinate> lines = new List<Coordinate>();
        public string type;

        public void Add(Coordinate point)
        {
            lines.Add(point);
        }
    }
}
