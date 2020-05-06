using ManagementProject.ViewModel;
using MangoApi;
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
using System.Windows.Shapes;
using static ManagementProject.UserControls.MainWindowStatisticsModel;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// DeviceControl.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceControl : Window
    {

        public int MapId
        {
            set
            {
                //GlobalVariable.CurrentMapId = value;
                InitDeviceButton(value);
            }
        }

        public DeviceControl()
        {
            InitializeComponent();
            Loaded += DeviceControl_Loaded;
        }

        private void DeviceControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitDeviceButton(GlobalVariable.CurrentMapId);
        }

        private async void InitDeviceButton(int mapid)
        {
            try
            {
                device.Children.Clear();
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)main.DataContext;
                MainPageViewModel mainPageViewModel = (MainPageViewModel)DataContext;
                if (mainPageViewModel.DeviceTypeList!=null)
                {
                    mainPageViewModel.DeviceTypeList.Clear();
                }
               
              DeviceTypeOfTree[] deviceTypes = HttpAPi.GetDeviceTypeListOfTree(AppConfig.ServerBaseUri + AppConfig.GetAllDeviceTypeForTree, mapid);
                List<string> devicelist = new List<string>();
                foreach (var item in deviceTypes)
                {
                    int deviceCount = 0;
                    MainWindowStatisticsViewModel mainWindowStatisticsViewModel = new MainWindowStatisticsViewModel();
                    mainWindowStatisticsViewModel.DeviceItemList = new System.Collections.ObjectModel.ObservableCollection<DeviceItem>();
                    mainWindowStatisticsViewModel.Icon = await HttpAPi.LoadImage(AppConfig .ImageBaseUri + item.imgUrl);//new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/mainwindowicon/" + item.deviceClassName + ".png", UriKind.Relative));

                    MainWindowStatistics deviceButton = new MainWindowStatistics();
                    deviceButton.Width = 50;
                    deviceButton.Height = 50;
                    foreach (var child in item.childern)
                    {
                        if (child.count > 0)
                        {
                            DeviceItem deviceItem = new DeviceItem
                            {
                                icon = await HttpAPi.LoadImage(AppConfig.ImageBaseUri + child.deviceimg),
                                id = child.id,
                                name = child.deviceTypeName,
                                count = child.count.ToString(),
                                deviceTypeCode = child.deviceTypeCode
                            };
                            mainWindowStatisticsViewModel.DeviceItemList.Add(deviceItem);
                            deviceCount = deviceCount + child.count;
                            devicelist.Add(child.deviceTypeCode);
                        }

                    }
                    mainWindowStatisticsViewModel.Number = deviceCount.ToString();
                    deviceButton.DataContext = mainWindowStatisticsViewModel;
                    if (deviceCount > 0)
                    {
                        Grid grid = new Grid();
                        grid.Width = 10;
                        device.Children.Add(deviceButton);
                        device.Children.Add(grid);
                    }

                }
                mainPageViewModel.DeviceTypeList = devicelist;

            }catch(Exception ex)
            {
                Logger.Error(typeof(DeviceControl), ex.Message);
            }
        }

    }
}
