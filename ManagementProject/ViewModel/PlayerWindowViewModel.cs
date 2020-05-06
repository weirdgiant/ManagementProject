using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using ManagementProject.UserControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class PlayerWindowViewModel : PlayerWindowModel
    {
        public string CameraCode;
        PlayerPanel playerPanel { get; set; }
        private PlayerWindowType playerWindowType;
        Dictionary<string, Device> CameraDic = new Dictionary<string, Device>();
        public DelegateCommand SixGridCommand { get; set; }
        public DelegateCommand NineGridCommand { get; set; }
        public DelegateCommand TwelveGridCommand { get; set; }
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MinWinCommand { get; set; }
        public DelegateCommand MaxWinCommand { get; set; }
        public DelegateCommand DragCommand { get; set; }

        public PlayerWindowViewModel( PlayerWindowType type,string cameracode)
        {
            CameraCode = cameracode;
            playerWindowType = type;
            
            SetWindowsType(type);

            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MinWinCommand = new DelegateCommand();
            MinWinCommand.ExecuteCommand = new Action<object>(MinWin);
            MaxWinCommand = new DelegateCommand();
            MaxWinCommand.ExecuteCommand = new Action<object>(MaxWin);
            DragCommand = new DelegateCommand
            {
                ExecuteCommand = new Action<object>(DragWin)
            };



            SixGridCommand = new DelegateCommand();
            SixGridCommand.ExecuteCommand = new Action<object>(SixGrid);
            NineGridCommand = new DelegateCommand();
            NineGridCommand.ExecuteCommand = new Action<object>(NineGrid);
            TwelveGridCommand = new DelegateCommand();
            TwelveGridCommand.ExecuteCommand = new Action<object>(TwelveGrid);
        }
        public void SetPanel(PlayerPanel playerPanel)
        {
            this.playerPanel = playerPanel;
        }

        private void SetWindowsType(PlayerWindowType type)
        {
            if (type==PlayerWindowType.Track)
            {
                WindowType = Visibility.Visible;
                WindowLogo = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/追踪.png";
            }
            else if(type ==PlayerWindowType.Playerback )
            {
                WindowType = Visibility.Collapsed;
                WindowLogo = "/ManagementProject;component/ImageSource/Icon/PlayerIcon/回放.png";
            }
        }

        private void CloseWin(object obj)
        {
            PlayerWindow w = (PlayerWindow)obj;
            w.Close();
        }
        private void DragWin(object obj)
        {
            PlayerWindow w = (PlayerWindow)obj;
            w.DragMove();
        }
        private void MinWin(object obj)
        {
            PlayerWindow w = (PlayerWindow)obj;
            w.WindowState = WindowState.Minimized;
        }
        private void MaxWin(object obj)
        {
            PlayerWindow w = (PlayerWindow)obj;
            if(w.WindowState == WindowState.Normal)
            {
                w.WindowState = WindowState.Maximized;
            }
            else
            {
                w.WindowState = WindowState.Normal;
            }
        }
        private void SixGrid(object obj)
        {
            playerPanel.Clear();
            List<string> codelist = new List<string>();
            playerPanel.InitSixPlayerPanel(3,3, CameraCode, playerPanel .playgrid,ref codelist, playerWindowType);
        }
        private void NineGrid(object obj)
        {
            playerPanel.Clear();
            playerPanel.InitPlayerPanel(3, 3, CameraCode, playerPanel.playgrid, playerWindowType);
        }
        private void TwelveGrid(object obj)
        {
            playerPanel.Clear();
            playerPanel.InitPlayerPanel(4, 4, CameraCode, playerPanel.playgrid, playerWindowType);
        }

        private void GetCamera(string code,int num)
        {
            Device[] camera = HttpAPi.GetCamera(code,num);

        }
       
    }
}
