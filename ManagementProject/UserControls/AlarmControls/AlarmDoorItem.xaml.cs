using com.mango.protocol;
using com.mango.protocol.msg;
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

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// AlarmDoorItem.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDoorItem : UserControl
    {
        public string Code { get; set; }
        public string DeviceName { get; set; }
        public bool IsOpen { get; set; } = false;
        public AlarmDoorItem()
        {
            InitializeComponent();
            Loaded += AlarmDoorItem_Loaded;
            openbt.Click += Openbt_Click;
        }

        private void AlarmDoorItem_Loaded(object sender, RoutedEventArgs e)
        {
            SetStatus(IsOpen);
            state.Text = "正在解锁中…";
            deviceName.Text = DeviceName;
        }

        public void SetStatus(bool sataus)
        {
            try
            {
                if (sataus)
                {
                    openbt.Visibility = Visibility.Collapsed;
                    state.Foreground = Brushes.CornflowerBlue;
                    state.Text = "解锁成功";
                    stateImg.Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/AlarmIcon/开锁.png", UriKind.Relative));
                }
                else
                {
                    openbt.Visibility = Visibility.Visible;
                    state.Foreground = Brushes.Red;
                    state.Text = "解锁失败";
                    stateImg.Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/AlarmIcon/关锁.png", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
           
        }

        private void Openbt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenDoor(Code);
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }
           
        }

        /// <summary>
        /// 打开所有门禁并返回门禁状态列表
        /// </summary>
        private void OpenDoor(string code)
        {
            CS_ACCESSCONTROl login = new CS_ACCESSCONTROl
            {
                code = code,
                type = 3
            };
            OutStream message = App.mango.Async(login, (short)login.protocol);
            state.Foreground = Brushes.LightYellow;
            state.Text = "正在解锁中…";
        }
    }
}
