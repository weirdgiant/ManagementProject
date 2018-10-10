using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
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
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
            this.Loaded += MapControl_Loaded;
            try
            {
                System.Net.IPHostEntry e = System.Net.Dns.GetHostEntry("ditu.google.cn");
            }
            catch
            {
                map.Manager.Mode = AccessMode.CacheOnly;
                MessageBox.Show("No internet connection avaible, going to CacheOnly mode.", "GMap.NET Demo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            map.MapProvider = GMapProviders.GoogleChinaTerrainMap   ; //google china 地图
            map.MinZoom = 1;  //最小缩放
            map.MaxZoom = 17; //最大缩放
            map.Zoom = 5;     //当前缩放
            map.ShowCenter = false; //不显示中心十字点
            map.DragButton = MouseButton.Left; //左键拖拽地图
            map.Position = new PointLatLng(32.064, 118.704); //地图中心位置：南京

            map.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(map);
            PointLatLng point = map.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
            GMapMarker marker = new GMapMarker(point);
            map.Markers.Add(marker);
        }

        private void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            
        }
    }
}
