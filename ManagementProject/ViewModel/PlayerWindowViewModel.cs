using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using ManagementProject.UserControls;
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
        PlayerPanel playerPanel { get; set; }
        private PlayerWindowType playerWindowType;
        public DelegateCommand SixGridCommand { get; set; }
        public DelegateCommand NineGridCommand { get; set; }
        public DelegateCommand TwelveGridCommand { get; set; }
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MinWinCommand { get; set; }
        public DelegateCommand MaxWinCommand { get; set; }
        public PlayerWindowViewModel(PlayerPanel playerPanel, PlayerWindowType type)
        {
            playerWindowType = type;
            this.playerPanel = playerPanel;
            SetWindowsType(type);

            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MinWinCommand = new DelegateCommand();
            MinWinCommand.ExecuteCommand = new Action<object>(MinWin);
            MaxWinCommand = new DelegateCommand();
            MaxWinCommand.ExecuteCommand = new Action<object>(MaxWin);
            SixGridCommand = new DelegateCommand();
            SixGridCommand.ExecuteCommand = new Action<object>(SixGrid);
            NineGridCommand = new DelegateCommand();
            NineGridCommand.ExecuteCommand = new Action<object>(NineGrid);
            TwelveGridCommand = new DelegateCommand();
            TwelveGridCommand.ExecuteCommand = new Action<object>(TwelveGrid);
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
            PlayerPanel.InitSixPlayerPanel(3,3,playerPanel .playgrid, playerWindowType);
        }
        private void NineGrid(object obj)
        {
            PlayerPanel.InitPlayerPanel(3, 3, playerPanel.playgrid, playerWindowType);
        }
        private void TwelveGrid(object obj)
        {
            PlayerPanel.InitPlayerPanel(4, 4, playerPanel.playgrid, playerWindowType);
        }
    }
}
