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
            get
            {
                return GlobalVariable.CurrentMapId;
            }
            set
            {
                GlobalVariable.CurrentMapId = value;
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
            InitDeviceButton(1);
        }

        private void InitDeviceButton(int mapid)
        {
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)main.DataContext;
            MainPageViewModel mainPageViewModel = (MainPageViewModel)DataContext;          
            DeviceTypeOfTree[] deviceTypes= HttpAPi.GetDeviceTypeListOfTree(AppConfig .ServerBaseUri +AppConfig .GetAllDeviceTypeForTree, mapid);
            foreach (var item in deviceTypes)
            {
                int deviceCount = 0;
                MainWindowStatisticsViewModel mainWindowStatisticsViewModel = new MainWindowStatisticsViewModel();
                mainWindowStatisticsViewModel.DeviceItemList = new System.Collections.ObjectModel.ObservableCollection<DeviceItem>();
                mainWindowStatisticsViewModel.Icon = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/mainwindowicon/"+ item .deviceClassName+ ".png", UriKind.Relative));//HttpAPi.LoadImage(AppConfig .ImageBaseUri + item.deviceimg);
               
                MainWindowStatistics deviceButton = new MainWindowStatistics();
                deviceButton.Width = 50;
                deviceButton.Height = 50;
                foreach (var child in item.childern)
                {
                    if (child.count>0)
                    {
                        DeviceItem deviceItem = new DeviceItem
                        {
                            icon = HttpAPi.LoadImage(AppConfig.ImageBaseUri + child.deviceimg),
                            id = child.id,
                            name = child.deviceTypeName,
                            count = child.count.ToString()
                        };
                        mainWindowStatisticsViewModel.DeviceItemList.Add(deviceItem);
                        deviceCount = deviceCount + child.count;
                    }
                  
                }
                mainWindowStatisticsViewModel.Number = deviceCount.ToString();
                deviceButton.DataContext = mainWindowStatisticsViewModel;
                if (deviceCount>0)
                {
                    Grid grid = new Grid();
                    grid.Width = 10;
                    device.Children.Add(deviceButton);
                    device.Children.Add(grid);
                }
               
            }
        }

    }
}
