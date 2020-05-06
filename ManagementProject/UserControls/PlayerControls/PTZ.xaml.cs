using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// PTZ.xaml 的交互逻辑
    /// </summary>
    public partial class PTZ : UserControl
    {
        public string CamCode { get; set; }
        public int CurrentSpeed { get; set; } = 6;
        public SdkManager SdkManager { get; set; }
        private DataTable m_dtPreset = new DataTable();
        public Player SelectedPlayer { get; set; }
        public class PtzEventArgs : EventArgs
        {
            public MW_PTZ_CMD_E ptzCmd;
            public int ulPtzSpeed;
            public string szCameraCode;
        }

        public delegate void PtzHandler(object sender, PtzEventArgs e);
        public event PtzHandler DoPtzEvent;
        public PTZ()
        {
            InitializeComponent();
            SdkManager = SdkManager.GetInstance();
            DoPtzEvent += new PtzHandler(DoPtzEventPanel);
            PreviewMouseWheel += PTZ_MouseWheel;
            rightdownbt.PreviewMouseLeftButtonUp += Rightdownbt_PreviewMouseLeftButtonUp;
            rightdownbt.PreviewMouseLeftButtonDown += Rightdownbt_PreviewMouseLeftButtonDown;
            rightupbt.PreviewMouseLeftButtonUp += Rightupbt_PreviewMouseLeftButtonUp;
            rightupbt.PreviewMouseLeftButtonDown += Rightupbt_PreviewMouseLeftButtonDown;
            leftdownbt.PreviewMouseLeftButtonUp += Leftdownbt_PreviewMouseLeftButtonUp;
            leftdownbt.PreviewMouseLeftButtonDown += Leftdownbt_PreviewMouseLeftButtonDown;
            leftupbt.PreviewMouseLeftButtonUp += Leftupbt_PreviewMouseLeftButtonUp;
            leftupbt.PreviewMouseLeftButtonDown += Leftupbt_PreviewMouseLeftButtonDown;
            rightbt.PreviewMouseLeftButtonUp += Rightbt_PreviewMouseLeftButtonUp;
            rightbt.PreviewMouseLeftButtonDown += Rightbt_PreviewMouseLeftButtonDown;
            leftbt.PreviewMouseLeftButtonUp += Leftbt_PreviewMouseLeftButtonUp;
            leftbt.PreviewMouseLeftButtonDown += Leftbt_PreviewMouseLeftButtonDown;
            downbt.PreviewMouseLeftButtonUp += Downbt_PreviewMouseLeftButtonUp;
            downbt.PreviewMouseLeftButtonDown += Downbt_PreviewMouseLeftButtonDown;
            upbt.PreviewMouseLeftButtonDown += Upbt_PreviewMouseLeftButtonDown;
            upbt.PreviewMouseLeftButtonUp += Upbt_PreviewMouseLeftButtonUp;
        } 

        private void PTZ_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
                Thread.Sleep(500);
                StopZoomIn();
            }
            else if (e.Delta < 0)
            {
                ZoomOut();
                Thread.Sleep(500);
                StopZoomOut();
            }
           
        }

        private void ZoomIn()
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_ZOOMTELE;
            OnDoPtzEvent(ptzArgs);
        }
        private void StopZoomIn()
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_ZOOMTELESTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void ZoomOut()
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_ZOOMWIDE;
            OnDoPtzEvent(ptzArgs);
        }
        private void StopZoomOut()
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_ZOOMWIDESTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightdownbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_RIGHTDOWNSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightdownbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_RIGHTDOWN;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightupbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_RIGHTUPSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightupbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_RIGHTUP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftdownbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_LEFTDOWNSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftdownbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_LEFTDOWN;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftupbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_LEFTUPSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftupbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_LEFTUP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_PANRIGHTSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Rightbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_PANRIGHT;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_PANLEFTSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Leftbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_PANLEFT;
            OnDoPtzEvent(ptzArgs);
        }

        private void Downbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_TILTDOWNSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Downbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_TILTDOWN;
            OnDoPtzEvent(ptzArgs);
        }

        private void Upbt_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_TILTUPSTOP;
            OnDoPtzEvent(ptzArgs);
        }

        private void Upbt_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PtzEventArgs ptzArgs = new PtzEventArgs();
            ptzArgs.ptzCmd = MW_PTZ_CMD_E.MW_PTZ_TILTUP;
            OnDoPtzEvent(ptzArgs);
        }



        /// <summary>
        /// 设置事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoPtzEventPanel(object sender, PtzEventArgs e)
        {
            uint ulRet = 0;
            try
            {
                PTZ_CTRL_COMMAND_S stPTZCommand = new PTZ_CTRL_COMMAND_S();
                stPTZCommand.ulPTZCmdID = (uint)e.ptzCmd;
                stPTZCommand.ulPTZCmdPara1 = (uint)e.ulPtzSpeed;
                stPTZCommand.ulPTZCmdPara2 = (uint)e.ulPtzSpeed;

                ulRet = IMOSSDK.IMOS_PtzCtrlCommand(ref SdkManager.stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(e.szCameraCode), ref stPTZCommand);
                if (0 != ulRet)
                {
#if DEBUG
                    MessageBox.Show("云台操作失败:" + ErrorCode.GetErrorMsg(ulRet));
#endif
                    Logger.Error(typeof(PTZ), "云台操作失败:" + ErrorCode.GetErrorMsg(ulRet));

                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(PTZ), ex);
            }

        }

        protected void OnDoPtzEvent(PtzEventArgs e)
        {
            if (null != DoPtzEvent)
            {
                PtzEventArgs PtzEventArgs = e;
                e.szCameraCode = this.CamCode;
                e.ulPtzSpeed = CurrentSpeed;
                DoPtzEvent(this, PtzEventArgs);
            }
        }

        public uint Ptz_Connect(string CameraCode, LOGIN_INFO_S stLoginInfo)
        {
            
            uint ulRet = 1;
            CamCode = CameraCode;
            uint camType = SdkManager.GetInstance().CameraType(CameraCode);
            if (camType == (uint)CAMERA_TYPE_E.CAMERA_TYPE_PTZ || camType == (uint)CAMERA_TYPE_E.CAMERA_HD_TYPE_PTZ)
            {
                if (SdkManager.GetInstance().userLoginStatus)
                    ulRet = IMOSSDK.IMOS_StartPtzCtrl(ref stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(CamCode));
            }
            return ulRet;
        }
    }

  
}
