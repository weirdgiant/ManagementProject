using com.mango.protocol;
using com.mango.protocol.msg;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// AlarmDoorControl.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDoorControl : UserControl
    {
        public string Code { get; set; }
        public AlarmDoorControl()
        {
            InitializeComponent();
            Loaded += AlarmDoorControl_Loaded;
            Unloaded += AlarmDoorControl_Unloaded;
        }

        private void AlarmDoorControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void AlarmDoorControl_Loaded(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
            OpenAllDoor(Code);
          
        }
        /// <summary>
        /// 打开所有门禁并返回门禁状态列表
        /// </summary>
        private void OpenAllDoor(string code)
        {
            try
            {
                Element[] elements = HttpAPi.GetRoomAccess(code);
                if (elements != null && elements.Length != 0)
                {
                    foreach (var item in elements)
                    {
                        AlarmDoorItem alarmDoorItem = new AlarmDoorItem();
                        alarmDoorItem.IsOpen = false;
                        alarmDoorItem.Code = item.code;
                        alarmDoorItem.DeviceName = item.name;
                        panel.Children.Add(alarmDoorItem);
                        OpenDoor(item.code);
                    }
                }
                else
                {
                    panel.Visibility = Visibility.Collapsed;
                    nonePanel.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }

        }


        private void OpenDoor(string code)
        {
            CS_ACCESSCONTROl login = new CS_ACCESSCONTROl
            {
                code = code,
                type = 1
            };
            OutStream message = App.mango.Async(login, (short)login.protocol);
        }

        /// <summary>
        /// 关闭所有门禁
        /// </summary>
        private void CloseAllDoor(string code)
        {
            CS_ACCESSCONTROl login = new CS_ACCESSCONTROl
            {
                code = code,
                type = 2
            };
            OutStream message = App.mango.Async(login, (short)login.protocol);
            if (message != null)
            {
                SC_ACCESSCONTROL[] ret = (SC_ACCESSCONTROL[])Package.SCStruct<SC_ACCESSCONTROL[]>(message);

            }
        }
    }
}
