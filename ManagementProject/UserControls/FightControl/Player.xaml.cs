using ManagementProject.Helper;
using ManagementProject.Model;
using MangoApi;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class Player : UserControl
    {
        #region Property

        private string cameraName;

        public string CameraName
        {
            get { return cameraName; }
            set { cameraName = value; InitCamera(); }
        }

        public string Ip { get; set; }
        public int Vendor { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string CameraCode { get; set; }

        public WinParams WinPara { get; set; }

        #endregion

        #region Construction Method

        public Player() => InitializeComponent();

        #endregion

        #region Private Method

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CameraList)))
            {
                var camera = e.Data.GetData(typeof(CameraList)) as CameraList;
                SetCamera(camera);
            }
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(CameraList)))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void SetCamera(CameraList camera)
        {
            CameraCode = camera.Code;
            CameraName = camera.Name;
            Vendor = camera.Vendor;
            Password = camera.Password;
            User = camera.User;
            Ip = camera.Ip;
            Port = camera.Port;

            InitCamera();

            var pc = FramEleHelper.GetParentObject<PlayControl>(this, "PlayCon");
            pc.winParams.cam = new Camera[]
            {
               new Camera
               {
                   code=camera.Code,
                   name=camera.Name,
                   vendor=camera.Vendor,
                   ip=camera.Ip,
                   user=camera.User,
                   pass=camera.Password
               }
            };

            PinkongHelper.SetCamera(pc.winParams);
        }

        private void InitCamera()
        {
            TbCameraName.Text = CameraName;
        }

        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
