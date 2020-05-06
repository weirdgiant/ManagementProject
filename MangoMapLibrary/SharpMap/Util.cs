using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoMapLibrary
{
    class ParseUtil
    {
        public static Color ParseColor(string color) => string.IsNullOrEmpty(color) ? Color.Black : Color.FromArgb(int.Parse(color));

        public static Font ParseFont(string familyname,int size)
        {
            if(string.IsNullOrEmpty(familyname) || size == 0)
            {
                return new Font("宋体", 10);
            }

            return new Font(familyname, size);
        }
    }
}
