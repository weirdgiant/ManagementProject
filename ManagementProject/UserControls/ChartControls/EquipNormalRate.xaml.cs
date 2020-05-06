using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ManagementProject.UserControls.ChartControls
{
    /// <summary>
    /// EquipNormalRate.xaml 的交互逻辑
    /// </summary>
    public partial class EquipNormalRate : UserControl
    {
        public EquipNormalRate()
        {
            InitializeComponent();           
        }
    }
    class EquipNormalRateViewModel: INotifyPropertyChangedClass
    {
        public EquipNormalRateViewModel()
        {
            //ProgressValue = 95.6;
        }

        private double _progressValue;
        public double ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                NotifyPropertyChanged("ProgressValue");

                ShowEllipse = (ProgressValue != 100);
            }
        }

        private string _count;
        public string Count
        {
            get { return _count; }
            set
            {
                _count = value;
                NotifyPropertyChanged("Count");
            }
        }

        private bool showEllipse;

        public bool ShowEllipse
        {
            get { return showEllipse; }
            set
            {
                showEllipse = value;
                NotifyPropertyChanged("ShowEllipse");
            }
        }
    }

    /// <summary>
    /// 正确率转换器
    /// </summary>
    public class NormalRateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempValue = value.ToString();
            if (tempValue.Equals("0")||tempValue.Equals("100"))
                return tempValue + "%";

            return System.Convert.ToDouble(value).ToString("f1") + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
