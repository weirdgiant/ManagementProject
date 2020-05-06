using ManagementProject.Model;
using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// TrackTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrackTabControl : Window
    {
        public TrackPageViewModel _trackPageViewModel { get; set; }
        public TrackManagementViewModel _trackManagementViewModel { get; set; }
        private Dictionary<int, RadioButton> TabButtonDic = new Dictionary<int, RadioButton>();
        public TrackTabControl()
        {
            InitializeComponent();
            Loaded += TrackTabControl_Loaded;
        }

        private void TrackTabControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReloadTab();
        }

        public void ReloadTab()
        {
            try
            {
                TabButtonDic.Clear();
                panel.Children.Clear();
                InitTab();
                RadioButton radio = TabButtonDic[TabButtonDic.First().Key];
                ActivityManage active = (ActivityManage)radio.DataContext;
                _trackManagementViewModel.CurrentId = TabButtonDic.First().Key;
                int mapId = int.Parse(radio.Tag.ToString());
                _trackPageViewModel.Sid = mapId;
                GetCameraList(active);
                radio.IsChecked = true;
            }
            catch (Exception ex)
            {

                Logger .Error (ex);
            }
           
            
        }


        private void InitTab()
        {
            _trackManagementViewModel =(TrackManagementViewModel) DataContext;
            _trackManagementViewModel.UpdatePageData();
            ActivityManage[] activities = _trackManagementViewModel.Datas.Where (x=>DateTime.Parse ( x.StartTime )<DateTime .Now).ToArray();
            foreach (var item in activities)
            {
                RadioButton radio = TabButton(item);
                radio.Click += Radio_Click;
                TabButtonDic.Add(item.ActivityId ,radio);
                Grid grid = new Grid() { Height =10,};
                panel.Children.Add(grid);
                panel.Children.Add(radio);
            }
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radio = (RadioButton)sender;
                ActivityManage active = (ActivityManage)radio.DataContext;
                int mapId = int.Parse(radio.Tag.ToString());
                _trackPageViewModel.Sid = mapId;
                _trackManagementViewModel.CurrentId = active.ActivityId;
                GetCameraList(active);
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
            
        }

        private string[] IdList;
        private void GetCameraList(ActivityManage active)
        {
            IdList = null;
            _trackPageViewModel.mapControl.CloseAllMapPlayer();
            _trackPageViewModel.mapControl.UnSelectedAll();
            if (string.IsNullOrEmpty (active.DeviceIdList))
            {
                return;
            }
            
            IdList = active.DeviceIdList.Replace(" ", "").Split(',');
            if (_trackPageViewModel.mapControl.ActualWidth !=0)
            {
                foreach (var item in IdList)
                {
                    int cameraid = int.Parse(item);
                    List<Device> list = MangoInfo.instance.CameraList;
                    Device device = list.Where(x => x.id == cameraid).ToArray()[0];
                    _trackPageViewModel.mapControl.FlickerFireEscapeCamera(device.code);
                }
                _trackPageViewModel.mapControl.view.Refresh();
            }
            
            var descriptor = System.ComponentModel.DependencyPropertyDescriptor.FromProperty(ActualWidthProperty, typeof(MapControl));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(_trackPageViewModel.mapControl, ActualWidth_ValueChanged);
            }
        }

        private void ActualWidth_ValueChanged(object sender, EventArgs e)
        {
            MapControl map = (MapControl)sender;
            foreach (var item in IdList)
            {
                int cameraid = int.Parse(item);
                List<Device> list = MangoInfo.instance.CameraList;
                Device device = list.Where(x => x.id == cameraid).ToArray()[0];
                _trackPageViewModel.mapControl.FlickerFireEscapeCamera(device.code);
            }
            map.view.Refresh();
        }


        private RadioButton TabButton(ActivityManage active)
        {
            
            Style btn_style = (Style)FindResource("TrackeTabRadioButtonStyle");
            RadioButton radioButton = new RadioButton()
            {
                Width = 128,
                Height = 30,
                Style = btn_style,
            };
            radioButton.Tag = active.MapId;
            radioButton.Content = active.ActivityName;
            radioButton.ToolTip = active.ActivityName;
            radioButton.DataContext = active;
            return radioButton;
        }
    }
}
