using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace MangoMapLibrary
{
    public class GraphicsHelper
    {

        private static readonly double PI = Math.PI; //3.14159265;  //π
        private static readonly double EARTH_RADIUS = 6378137;    //地球半径
        private static readonly double RAD = Math.PI / 180.0;   //   π/180

        private static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        public static void DrawRoundRect(Rectangle rectangle, Graphics g, int _radius)
        {
            Pen shadowPen = new Pen(Color.Blue);
            g.DrawPath(shadowPen, DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 1, _radius));
        }

        /// <summary>
        /// 根据两点间经纬度坐标（double值），计算两点间距离，单位为米
        /// </summary>
        /// <param name="lng1">经度1</param>
        /// <param name="lat1">纬度1</param>
        /// <param name="lng2">经度2</param>
        /// <param name="lat2">纬度2</param>
        /// <returns>返回距离（米）</returns>
        public double GetDistance(double lng1, double lat1, double lng2, double lat2)
        {
            double radLat1 = lat1 * RAD;  // // RAD=π/180
            double radLat2 = lat2 * RAD;
            double a = radLat1 - radLat2;
            double b = (lng1 - lng2) * RAD;
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s;
        }


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

    }
}
