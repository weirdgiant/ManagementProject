using ManagementProject.Helper;
using ManagementProject.UserControls.ChartControls;
using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ManagementProject.PageView
{
    /// <summary>
    /// ChartPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChartPage : Page
    {
        private bool IsStartUp { get; set; } = false;
        ChartPageViewModel chartPageViewModel { get; set; }
        public ChartPage()
        {
            InitializeComponent();
            chartPageViewModel = new ChartPageViewModel();
            DataContext = chartPageViewModel;
            Loaded += ChartPage_Loaded;
            Unloaded += ChartPage_Unloaded;
        }

        private void ChartPage_Unloaded(object sender, RoutedEventArgs e)
        {
            IsStartUp=false;
            chartPageViewModel = new ChartPageViewModel();
            chartPageViewModel.InitHistory();
            DataContext = chartPageViewModel;
        }

        private async void RefreshDateTimeAsync()
        {
            IsStartUp = true;
            while (IsStartUp)
            {
                chartPageViewModel.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
                chartPageViewModel.CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                RefreshData();
                await Task.Delay(1000);
            }
        }

        private void RefreshData()
        {
            InitPieCount(GlobalVariable.CurrentSid);
            InitEventRate(GlobalVariable.CurrentSid);
            InitErrorRate(GlobalVariable.CurrentSid);
        }

        private void ChartPage_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDateTimeAsync();
            
        }
        #region 楼宇分布
        private void InitDeviceBuilingTypeCount(int sid, string type)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetBuildingCount;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceTypeCount(sid, type, url);
                deviceBuiling.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        private void InitDeviceBuilingClassCount(int sid, string type)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetBuildingCountByClass;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceClassCount(sid, type, url);
                deviceBuiling.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }
        #endregion
        #region 过去12月设备故障
        private void InitDeviceRateType(int sid, string type)
        {
            try
            {
                DateTime time = DateTime.Now;
                string start = time.AddMonths(-12).ToString("yyyy-MM");
                string end = time.ToString("yyyy-MM");
                string url = AppConfig.ServerBaseUri + AppConfig.GetEventRateByType;
                ErrorDeviceType[] ret = HttpAPi.GetErrorRateType(sid, start, end, type, url);
                lineRate.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        private void InitDeviceRateClass(int sid, string type)
        {
            try
            {
                DateTime time = DateTime.Now;
                string start = time.AddMonths(-12).ToString("yyyy-MM");
                string end = time.ToString("yyyy-MM");
                string url = AppConfig.ServerBaseUri + AppConfig.GetEventRateByClass;
                ErrorDeviceType[] ret = HttpAPi.GetErrorRateClass(sid, start, end, type, url);
                lineRate.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        #endregion
        #region 报警事件统计
        private void InitPieCount(int sid)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetEventByType;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceCount(sid, url);
                pieCount.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }

        }
        #endregion
        #region 故障设备列表
        private void InitErrorDeviceTypeList(int sid, string type,ref int deviceCount)
        {
            try
            {
                //chartPageViewModel.ErrorDeviceList.Clear();
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorDeviceByType;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceTypeCount(sid, type, url);
                chartPageViewModel.ErrorDeviceList = new ObservableCollection<ErrorDeviceType>(ret);
                deviceCount = ret.Length;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        private void InitErrorDeviceClassList(int sid, string type,ref string count)
        {
            try
            {
                //chartPageViewModel.ErrorDeviceList.Clear();
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorDeviceByClass;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceClassCount(sid, type, url);
                chartPageViewModel.ErrorDeviceList = new ObservableCollection<ErrorDeviceType>(ret);
                count = ret.Length.ToString();
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }
        #endregion

        private void InitEventRate(int sid)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetEventRate;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceCount(sid, url);
                columnCount.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        private void InitErrorRate(int sid)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorRate;
                ErrorDeviceType ret = HttpAPi.GetErrorDeviceRate(sid, url);
                EquipNormalRateViewModel vm = new EquipNormalRateViewModel();
                double rate =1.0- (double)ret.errorCount / (double)ret.count;
                vm.ProgressValue = rate * 100;
                vm.Count = ret.count.ToString();
                errorRate.DataContext = vm;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        /// <summary>
        /// / 根据设备小类获取故障设备发生时间分布
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="type"></param>
        private void IntErrorTimeByType(int sid, string type)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorTimeByType;
                string[] ret = HttpAPi.GetErrorTimeByType(sid, type, url);
                errorTime.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage),ex.Message);
            }
        }

        /// <summary>
        /// 根据设备大类获取设备故障时间
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="type"></param>
        private void IntErrorTimeByClass(int sid, string type)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorTimeByClass;
                string[] ret = HttpAPi.GetErrorTimeByClass(sid, type, url);
                errorTime.LoadSource(ret);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), ex.Message);
            }
        }

        private void deviceClass_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            string errorDeviceCount = "";
            string type = radio.Tag.ToString();        
            InitDeviceBuilingClassCount(GlobalVariable.CurrentSid, type);
            InitDeviceRateClass(GlobalVariable.CurrentSid, type);
            InitErrorDeviceClassList(GlobalVariable.CurrentSid, type,ref errorDeviceCount);
            IntErrorTimeByClass(GlobalVariable.CurrentSid, type);
            chartPageViewModel.ErrorCount = errorDeviceCount;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            int errorDeviceCount = 0;
            string type = radio.Tag.ToString();
            
            InitDeviceBuilingTypeCount(GlobalVariable.CurrentSid, type);
            InitDeviceRateType(GlobalVariable.CurrentSid, type);
            InitErrorDeviceTypeList(GlobalVariable.CurrentSid, type,ref errorDeviceCount);
            IntErrorTimeByType(GlobalVariable.CurrentSid, type);
            chartPageViewModel.ErrorCount = errorDeviceCount.ToString();
        }

        private void CheckBoxOpenMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            chartPageViewModel.IsShowMap = !chartPageViewModel.IsShowMap;

            foreach (var item in keyValuePairs)
            {
                item.Key.Foreground = item.Value;
            }
        }

        Dictionary<TextBlock, Brush> keyValuePairs = new Dictionary<TextBlock, Brush>();

        private void CheckBoxOpenMenu_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox check))
                return;

            //var errorCountTextBlock = FramEleHelper.GetChildObject<TextBlock>(check, "TbErrorCount");
            ////if (int.Parse(errorCountTextBlock.Text) <= 0)
            ////    return;

            //check.IsEnabled = int.Parse(errorCountTextBlock.Text) > 0;

            try
            {
                if (check.Tag==null)
                {
                    return;
                }
                string type = check.Tag.ToString();
                string count = "";
                foreach (var img in FramEleHelper.GetChildObjects<Image>(check, ""))
                {
                    if (img != null && img.Source != null)
                        ImgRadio.Source = img.Source;
                }

                chartPageViewModel.IsShowMap = !chartPageViewModel.IsShowMap;
                InitDeviceBuilingClassCount(GlobalVariable.CurrentSid, type);
                InitDeviceRateType(GlobalVariable.CurrentSid, type);
                InitErrorDeviceClassList(GlobalVariable.CurrentSid, type, ref count);
                IntErrorTimeByClass(GlobalVariable.CurrentSid, type);
                InitDeviceRateClass(GlobalVariable.CurrentSid, type);

                keyValuePairs.Clear();
                foreach (var textBlock in FramEleHelper.GetChildObjects<TextBlock>(check, ""))
                {
                    keyValuePairs.Add(textBlock, textBlock.Foreground);
                    textBlock.Foreground = Brushes.White;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(ChartPage), "CheckBoxOpenMenu_Checked" + ex.Message);
            }
        }

        #region Event

        private void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            if (e.Delta > 0)
                scrollViewer.LineLeft();
            else
                scrollViewer.LineRight();

            e.Handled = true;
        } 

        #endregion
    }

    #region Converter
    class HomeClassIconConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            return $"/ManagementProject;component/ImageSource/Icon/Home/HomeClassIcon/{value.ToString()}.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => "";
    } 
    #endregion
}
