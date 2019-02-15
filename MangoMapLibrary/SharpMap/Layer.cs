using GeoAPI.Geometries;
using MangoMapLibrary.Api;
using SharpMap;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoMapLibrary
{
    public enum LayerType
    {
        SHAPE_FILE,
        GDI_IMAGE
    }

    public enum MapType
    {
        MAP,
        MAP_GROUP
    }

    public interface AbstractMangoLayer
    {
        void RenderPrepare();
        MangoLayer MangoLayer();
    }


    public class MangoGDILayer : GdiImageLayer,AbstractMangoLayer
    {
        public MangoLayer Layer { get; }

        public MangoGDILayer(MangoLayer layer, string name) : base(name)
        {
            this.Layer = layer;
        }

        public MangoLayer MangoLayer()
        {
            return Layer;
        }

        public void RenderPrepare()
        {
        }
    }

    public class MangoLabelLayer : LabelLayer, AbstractMangoLayer
    {
        public MangoLayer Layer { get; }
        public MangoLabelLayer(MangoLayer layer,string name) : base(name)
        {
            this.Layer = layer;
        }
                
        public void RenderPrepare()
        {
            this.Style.ForeColor = ParseUtil.ParseColor(Layer.labelColor);
            this.Style.Font = ParseUtil.ParseFont(Layer.labelFont, Layer.labelFontSize);
        }

        public MangoLayer MangoLayer()
        {
            return Layer;
        }
    }

    public class MangoVectorLayer : VectorLayer, AbstractMangoLayer
    {
        public MangoLayer Layer { get; }
        public MangoVectorLayer(MangoLayer layer) : base(layer.name)
        {
            this.Layer = layer;
        }

        public MangoVectorLayer(MangoLayer layer, IBaseProvider dataSource) : base(layer.name,dataSource)
        {
            this.Layer = layer;
        }

        public void RenderPrepare()
        {
            //载入图层渲染模式
            if (Layer.renderMode == 1 && Layer.textureFilename != null)
            {
                if (System.IO.File.Exists(Layer.textureFilename))
                {
                    TextureBrush brush = new TextureBrush(new Bitmap(Layer.textureFilename));
                    this.Style.Fill = brush;
                }
            }
            else
            {
                Brush brush = new SolidBrush(ParseUtil.ParseColor(Layer.color));
                this.Style.Fill = brush;
            }
        }

        public MangoLayer MangoLayer()
        {
            return Layer;
        }
    }

    public class DeviceLayer : ILayer
    {
        /// <summary>
        /// 是否渲染
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName { get; set; }
        /// <summary>
        /// 最大可见
        /// </summary>
        public double MaxVisible { get; set; }
        /// <summary>
        /// 最小可见
        /// </summary>
        public double MinVisible { get; set; }

        public string Proj4Projection { get; set; }

        public int SRID { get; set; }

        public int TargetSRID
        {
            get { return 0; }
        }
        /// <summary>
        /// 图层范围
        /// </summary>
        public Envelope Envelope
        {
            get { return new Envelope(0, 1000, 0, 1000); }
        }

        public VisibilityUnits VisibilityUnits { get; set; }
        public string LayerTitle { get; set; }

        List<AbstractLayerElement> elements;

        public DeviceLayer()
        {
            //初始化
            Enabled = true;
            MaxVisible = double.MaxValue;
            MinVisible = double.MinValue;
            //模拟数据
            elements = new List<AbstractLayerElement>();
        }

        public List<AbstractLayerElement> GetAllElements()
        {
            return new List<AbstractLayerElement>(elements);
        }
        public void Render(Graphics g, Map map)
        {
        }

        public void Render(Graphics g, MapViewport map)
        {
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            
            foreach (var pt in elements)
            {
                pt.Render(g, map);                
            }
        }

        public void Remove(AbstractLayerElement device)
        {
            this.elements.Remove(device);
        }


        

        public AbstractLayerElement Get(PointF c, MapViewport map)
        {
            for(int i = elements.Count-1;i >= 0;i--)
            {
                var pt = elements[i];
                //忽略不在当前视图范围内的点位,围栏的点位有多个所以不在此处判断
                if(!(pt is LayerElementFence))
                {
                    if (pt.p.X > map.Envelope.MaxX || pt.p.X < map.Envelope.MinX || pt.p.Y > map.Envelope.MaxY || pt.p.Y < map.Envelope.MinY)
                    {
                        continue;
                    }
                }
                
                if (pt.IsMouseIn(c, map))
                {
                    return pt;
                }
            }

            return null;
        }
        public void AddLayerElement(AbstractLayerElement device)
        {
            elements.Add(device);
        }

        public void ClearSelected()
        {
            foreach (var pt in elements)
            {
                pt.status = ICON_STATUS.NORMAL;
            }
        }

        
        public void RenderAlarm(Graphics g,MapViewport map,bool flag)
        {
            foreach (AbstractLayerElement pt in this.elements)
            {
                pt.RenderAlarm(g, map, flag);                
            }
        }
    }

}
