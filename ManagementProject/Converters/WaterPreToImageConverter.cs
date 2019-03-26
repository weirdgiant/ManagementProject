using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ManagementProject.Converters
{
    /// <summary>
    /// 根据水压信息，转换为相应的图片
    /// </summary>
    public class WaterPreToImageConverter : IValueConverter
    {
        public static WaterPreToImageConverter Instance = new WaterPreToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new BitmapImage(new Uri($"pack://application:,,,/ImageSource/Icon/MainWinControlIcon/{value}.png")); ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
