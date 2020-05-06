using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject
{

    internal class DigitalRec
    {
        private int _zoom = 3;
        public int Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                if (value <= 10)
                {
                    if (value <= 0)
                    {
                        return;
                    }
                    _zoom = value;
                }
            }
        }

        public DigitalRec()
        {
            _zoom = 3;
        }

        public XP_RECT_S GetRec(int playWidth, int playHeight, Point p)
        {
            int num = (int)(playWidth / 10.0 * Zoom);
            int num2 = (int)(playHeight / 10.0 * Zoom);
            XP_RECT_S result = default(XP_RECT_S);
            if (p.X < num / 2)
            {
                result.lLeft = 0;
                result.lRight = 10 * Zoom;
            }
            else if (p.X + num / 2 > playWidth)
            {
                result.lLeft = 100 - 10 * Zoom;
                result.lRight = 100;
            }
            else
            {
                result.lLeft = 100 * p.X / playWidth - 5 * Zoom;
                result.lRight = 100 * p.X / playWidth + 5 * Zoom;
            }
            if (p.Y < num2 / 2)
            {
                result.lTop = 0;
                result.lBottom = 10 * Zoom;
            }
            else if (p.Y + num2 / 2 > playHeight)
            {
                result.lTop = 100 - Zoom * 10;
                result.lBottom = 100;
            }
            else
            {
                result.lTop = 100 * p.Y / playHeight - 5 * Zoom;
                result.lBottom = 100 * p.Y / playHeight + 5 * Zoom;
            }
            return result;
        }
    }
}
