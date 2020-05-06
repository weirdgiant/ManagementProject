using GeoAPI.Geometries;
using MangoMapLibrary.Api;
using Newtonsoft.Json;
using SharpMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MangoMapLibrary
{
    public enum ICON_STATUS
    {
        NORMAL,
        CHECKED
    }

    public class ElementType
    {
        public const string TYPE_BUILDING = "@Building";
        public const string TYPE_BUILDING_ICON = "@BuildingIcon";
        public const string TYPE_FENCE = "@Fence";
        #region 围栏的四种类型
        public const string FENCE_TensionFence = "TensionFence";//张力围栏
        public const string FENCE_VibrantCable = "VibrantCable";//充满活力的电缆
        public const string FENCE_InfraredRadiationDetector = "InfraredRadiationDetector";//红外对射
        public const string FENCE_LeakyCable = "LeakyCable"; //泄露电缆
        #endregion
    }

    
    public class ElementIconSet
    {
        public Bitmap icon;
        public Bitmap selected;
        public Bitmap alarm;

        public ElementIconSet(Bitmap icon,Bitmap selected,Bitmap alarm)
        {
            this.icon = icon;
            this.selected = selected;
            this.alarm = alarm;
        }
    }

    public delegate void ElementMove(AbstractLayerElement element);

    public abstract class AbstractLayerElement
    {
        public Coordinate p
        {
            set
            {
                Ele.latitude = value.X;
                Ele.longitude = value.Y;
            }
            get => new Coordinate(Ele.latitude, Ele.longitude);
        }
        public ICON_STATUS status;

        public ElementIconSet iconSet;

        public ElementIconSet iconBuildSet;

        public bool alarm;

        public MangoLayerElement Ele { get; }

        public string Type
        {
            get => Ele.deviceTypeCode;
            set => Ele.deviceTypeCode = value;
        }
        public string Code
        {
            get => Ele.code;
            set => Ele.code = value;
        }
        public int Angle
        {
            get => Ele.iconAngle;
            set => Ele.iconAngle = value;
        }
        public int Id
        {
            get => Ele.id;
            set => Ele.id = value;
        }
        public AbstractLayerElement(MangoLayerElement ele)
        {
            Ele = ele;
            p = new Coordinate(ele.latitude, ele.longitude);
        }

        public abstract bool IsMouseIn(PointF c, MapViewport map);
        public abstract void Render(Graphics g, MapViewport map);
        public abstract void RenderAlarm(Graphics g, MapViewport map, bool flag);

        public virtual string ToIconExt()
        {
            return null;
        }
    }

    public class LayerElementBuilding : AbstractLayerElement
    {
        private int Width;
        private int Height;

        public IconExtParamBuilding Ext { get; }

        private string fileName;

        public LayerElementBuilding(MangoLayerElement ele, ElementIconSet icon, IconExtParamBuilding ext) : base(ele)
        {
            iconSet = icon;
            Width = icon.icon.Width;
            Height = icon.icon.Height;
            Ext = ext;
            fileName = ext.filename;
        }


        private void GetSize(out double _width,out double _height,MapViewport map)
        {
            double scale = Ext.zoomLevel / map.Zoom;
            double pw = Ext.width / map.Size.Width;
            _width = scale * Width/pw;
            _height = scale * Height/pw;
                        
        }
        
        public override bool IsMouseIn(PointF c, MapViewport map)
        {
            PointF target = map.WorldToImage(p);
            GetSize(out double _width, out double _height, map);
            PointF a = MathUtil.GetPointByAngle(c, target, -Angle);
            return a.X >= target.X - _width / 2 && a.X <= target.X + _width / 2 && a.Y >= target.Y - _height / 2 && a.Y <= target.Y + _height / 2;
            //return c.X >= target.X - _width / 2 && c.X <= target.X + _width / 2 && c.Y >= target.Y - _height / 2 && c.Y <= target.Y + _height / 2;
        }

        public override void Render(Graphics g, MapViewport map)
        {
            //世界坐标转换为屏幕坐标
            var _p = map.WorldToImage(p);
            GetSize(out double _width, out double _height, map);            

            //绘制点
            if (Angle == 0)
            {
                g.DrawImage(iconSet.icon, (float)(_p.X - _width / 2), (float)(_p.Y - _height / 2), (float)_width, (float)_height);

                if (status == ICON_STATUS.CHECKED && !fileName.Equals("Resource/building.png"))
                {
                    Pen pen = new Pen(Color.Blue);
                    g.DrawRectangle(pen, (float)(_p.X - _width / 2), (float)(_p.Y - _height / 2), (float)_width, (float)_height);
                }
            }
            else
            {
                PointF ulCorner = new PointF((float)(_p.X - _width / 2), (float)(_p.Y - _height / 2));
                PointF a = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                ulCorner = new PointF((float)(_p.X + _width / 2), (float)(_p.Y - _height / 2));
                PointF b = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                ulCorner = new PointF((float)(_p.X - _width / 2), (float)(_p.Y + _height / 2));
                PointF c = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                g.DrawImage(iconSet.icon, new PointF[] { a, b, c });
                g.DrawImage(iconBuildSet.icon, new PointF[] { a, b, c });

                if (status == ICON_STATUS.CHECKED && !fileName.Equals("Resource/building.png"))
                {
                    ulCorner = new PointF((float)(_p.X + _width / 2), (float)(_p.Y + _height / 2));
                    PointF d = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                    Pen pen = new Pen(Color.Blue);
                    g.DrawLines(pen, new PointF[] { a, b, d, c, a });
                }
            }
        }

        public override void RenderAlarm(Graphics g, MapViewport map, bool flag) { }

        public override string ToIconExt()
        {
            return JsonConvert.SerializeObject(Ext);
        }
    }

    public class LayerDeviceElement : AbstractLayerElement
    {
        public const int ICON_RADIUS = 16;

        public LayerDeviceElement(MangoLayerElement ele, ElementIconSet icon) : base(ele)
        {
            iconSet = icon;
        }

        //根据坐标进行范围匹配，以目标坐标中心点匹配指定半径内所有范围
        public static double IsPoint(PointF c, PointF target)
        {
            double distance = Math.Pow(Math.Abs(target.X - c.X), 2) + Math.Pow(Math.Abs(target.Y - c.Y), 2);
            return Math.Sqrt(distance);
        }

        public override void Render(Graphics g, MapViewport map)
        {
            //忽略不在当前视图范围内的点位
            if (p.X > map.Envelope.MaxX || p.X < map.Envelope.MinX || p.Y > map.Envelope.MaxY || p.Y < map.Envelope.MinY)
            {
                return;
            }

            //世界坐标转换为屏幕坐标
            var _p = map.WorldToImage(p);
            //绘制点
            Bitmap _icon = status == ICON_STATUS.CHECKED ? iconSet.selected : iconSet.icon;
            if (Angle == 0)
            {
                g.DrawImage(_icon, (int)_p.X - ICON_RADIUS, (int)_p.Y - ICON_RADIUS, ICON_RADIUS * 2, ICON_RADIUS * 2);

                ShowDeviceTag(g, _p);
            }
            else
            {
                PointF ulCorner = new PointF(_p.X - ICON_RADIUS, _p.Y - ICON_RADIUS);
                PointF a = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                PointF b = MathUtil.GetPointByAngle(ulCorner, _p, Angle + 90);
                PointF c = MathUtil.GetPointByAngle(ulCorner, _p, Angle + 270);
                g.DrawImage(_icon, new PointF[] { a, b, c });

                ShowDeviceTag(g,_p);
            }
        }

        private void ShowDeviceTag(Graphics g, PointF _p)
        {
            if (Ele != null && Global.ShowMapTag)
            {
                var font1 = new Font(new FontFamily("微软雅黑"), 8);
                var font2 = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Bold);
                var sizeF1 = g.MeasureString(Ele.name, font1);
                var sizeF2 = g.MeasureString(Ele.deviceTypeName, font2);
                var fontWidth = sizeF1.Width > sizeF2.Width ? sizeF1.Width : sizeF2.Width;

                //Rectangle rectangle = new Rectangle((int)_p.X + ICON_RADIUS * 2, (int)_p.Y - ICON_RADIUS - 25, (int)(fontWidth + 5.0), 22);
                //g.DrawRectangle(new Pen(Color.Black, 25), rectangle);
                try
                {
                    g.DrawImage(Image.FromFile(@"Resource/device_details.png"), _p.X + ICON_RADIUS * 2 - 15, _p.Y - ICON_RADIUS - 40, (int)fontWidth + 25, 60);
                    g.DrawString(Ele.name, font1, Brushes.White, _p.X + ICON_RADIUS * 2, _p.Y - ICON_RADIUS - 10);
                    g.DrawString(Ele.deviceTypeName, font2, Brushes.White, _p.X + ICON_RADIUS * 2, _p.Y - ICON_RADIUS - 30);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public override void RenderAlarm(Graphics g, MapViewport map, bool flag)
        {
            if (!alarm)
                return;
            //忽略不在当前视图范围内的点位
            if (p.X > map.Envelope.MaxX || p.X < map.Envelope.MinX || p.Y > map.Envelope.MaxY || p.Y < map.Envelope.MinY)
            {
                return;
            }
            //世界坐标转换为屏幕坐标
            var _p = map.WorldToImage(p);
            if (Angle == 0)
            {
                g.DrawImage(flag ? iconSet.icon : iconSet.alarm, (int)_p.X - LayerDeviceElement.ICON_RADIUS, (int)_p.Y - LayerDeviceElement.ICON_RADIUS, LayerDeviceElement.ICON_RADIUS * 2, LayerDeviceElement.ICON_RADIUS * 2);
            }
            else
            {
                PointF ulCorner = new PointF(_p.X - LayerDeviceElement.ICON_RADIUS, _p.Y - LayerDeviceElement.ICON_RADIUS);
                PointF a = MathUtil.GetPointByAngle(ulCorner, _p, Angle);
                PointF b = MathUtil.GetPointByAngle(ulCorner, _p, Angle + 90);
                PointF c = MathUtil.GetPointByAngle(ulCorner, _p, Angle + 270);
                g.DrawImage(flag ? iconSet.icon : iconSet.alarm, new PointF[] { a, b, c });

            }
        }

        public override bool IsMouseIn(PointF c, MapViewport map)
        {
            PointF target = map.WorldToImage(p);
            return IsPoint(target, c) <= LayerDeviceElement.ICON_RADIUS;
        }

        
    }

    public class LayerElementFence : AbstractLayerElement
    {
        public IconExtParamFence Ext { get; }

        public LayerElementFence(MangoLayerElement ele, ElementIconSet icon, IconExtParamFence Ext):base(ele)
        {
            this.iconSet = icon;
            this.Ext = Ext;
            //this.Type = ElementType.TYPE_FENCE;
            this.Type = Ext.type;
        }

        public string GetFenceType()
        {
            return Ext.type;
        }

        public override bool IsMouseIn(PointF c, MapViewport map)
        {
            foreach(PointF p in Ext.points)
            {
                PointF target = map.WorldToImage(new Coordinate(p.X, p.Y));
                if (LayerDeviceElement.IsPoint(target, c) <= LayerDeviceElement.ICON_RADIUS)
                    return true;
            }
            return false;
        }

        public override void Render(Graphics g, MapViewport map)
        {
            RenderAlarm(g, map, false,false);
        }

        public override void RenderAlarm(Graphics g, MapViewport map, bool flag)
        {
            RenderAlarm(g, map, flag, true);
        }

        public void RenderAlarm(Graphics g, MapViewport map, bool flag,bool alarmMode)
        {
            if (alarmMode && !this.alarm) return;
            bool render = false;
            foreach (PointF p in Ext.points)
            {
                if (p.X <= map.Envelope.MaxX && p.X >= map.Envelope.MinX && p.Y <= map.Envelope.MaxY && p.Y >= map.Envelope.MinY)
                {
                    render = true;
                    break;
                }
            }
            //如果当前没有点位在视野里，则取消渲染
            if (!render)
            {
                return;
            }
            //画线
            PointF[] linePoints = new PointF[Ext.points.Length];
            for (int i = 0; i < linePoints.Length; i++)
            {
                linePoints[i] = map.WorldToImage(new Coordinate(Ext.points[i].X, Ext.points[i].Y));
            }

            Color penColor = (this.alarm && flag) || status == ICON_STATUS.CHECKED ? Color.Red : Color.Green;
            Pen pen = new Pen(penColor, 6);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            pen.DashPattern = new float[] { 1, 1 };
            g.DrawLines(pen, linePoints);

            //画图标
            for (int i = 0; i < linePoints.Length;i++)
            {
                if (i != 0 && i < linePoints.Length - 1)
                    continue;
                PointF _p = linePoints[i];

                if (iconSet==null)
                    continue;

                //绘制点图标,报警图标》是否选中》默认图标
                Bitmap renderImage = this.alarm && flag ? iconSet.alarm : (this.status == ICON_STATUS.CHECKED ? iconSet.selected:iconSet.icon);
                
                if(renderImage != null)
                {
                    g.DrawImage(renderImage, (int)_p.X - LayerDeviceElement.ICON_RADIUS, (int)_p.Y - LayerDeviceElement.ICON_RADIUS, LayerDeviceElement.ICON_RADIUS * 2, LayerDeviceElement.ICON_RADIUS * 2);

                    if (i==linePoints.Length-1)
                    {
                        ShowDeviceTag(g,_p, LayerDeviceElement.ICON_RADIUS);
                    }
                    
                }
                
            }
        }

        private void ShowDeviceTag(Graphics g, PointF _p,int ICON_RADIUS)
        {
            if (Ele != null && Global.ShowMapTag)
            {
                Font font1 = new Font(new FontFamily("微软雅黑"), 8);

                Font font2 = new Font(new FontFamily("微软雅黑"), 10, FontStyle.Bold);

                var sizeF1 = g.MeasureString(Ele.name, font1);
                var sizeF2 = g.MeasureString(Ele.deviceTypeName, font2);
                var fontWidth = sizeF1.Width > sizeF2.Width ? sizeF1.Width : sizeF2.Width;

                //Rectangle rectangle = new Rectangle((int)_p.X + ICON_RADIUS * 2, (int)_p.Y - ICON_RADIUS - 25, (int)(fontWidth + 5.0), 22);
                //g.DrawRectangle(new Pen(Color.Black, 25), rectangle);

                g.DrawImage(Image.FromFile(@"Resource/device_details.png"), _p.X + ICON_RADIUS * 2 - 15, _p.Y - ICON_RADIUS - 40, (int)fontWidth + 25, 60);

                g.DrawString(Ele.name, font1, Brushes.White, _p.X + ICON_RADIUS * 2, _p.Y - ICON_RADIUS - 10);
                g.DrawString(Ele.deviceTypeName, font2, Brushes.White, _p.X + ICON_RADIUS * 2, _p.Y - ICON_RADIUS - 30);
            }
        }

        public override string ToIconExt()
        {
            var stringExt = JsonConvert.SerializeObject(Ext);
            return stringExt;
        }
    }
}
