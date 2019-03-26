using ManagementProject.Model;
using ManagementProject.UserControls;
using ManagementProject.UserControls.FightControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ManagementProject.ViewModel
{
    public class PlayControlViewModel : PlayControlModel
    {
        public PlayerPanel PlayerPanel { get; set; }

        /// <summary>
        /// 拼控
        /// </summary>
        private Grid fightGrid;

        public DelegateCommand OneGridCommand { get; set; }
        public DelegateCommand FourGridCommand { get; set; }
        public DelegateCommand SixGridCommand { get; set; }
        public DelegateCommand SevenGridCommand { get; set; }
        public DelegateCommand NineGridCommand { get; set; }
        public DelegateCommand TwelveGridCommand { get; set; }
        public DelegateCommand CloseControlCommand { get; set; }

        public PlayControlViewModel(PlayerPanel playerPanel,Grid grid)
        {
            PlayerPanel = playerPanel;
            fightGrid = grid;

            OneGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(OneGrid) };
            FourGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(FourGrid) };
            SixGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SixGrid) };
            SevenGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SevenGrid) };
            NineGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(NineGrid) };
            TwelveGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(TwelveGrid) };

            CloseControlCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseControl) };
        }

        private void OneGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(1, 1, PlayerPanel.playgrid);
            PlayerPanel.InitPlayer(1, 1, PlayerPanel.playgrid);
        }

        private void FourGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(2,2, PlayerPanel.playgrid);
            PlayerPanel.InitPlayer(2,2, PlayerPanel.playgrid);
        }

        private void SixGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(3, 3, PlayerPanel.playgrid);
            PlayerPanel.InitSixPlayer(3, 3, PlayerPanel.playgrid);
        }
        private void SevenGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(4, 3, PlayerPanel.playgrid);
            PlayerPanel.InitSevenPlayer(4, 3, PlayerPanel.playgrid);
        }
        private void NineGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(3, 3, PlayerPanel.playgrid);
            PlayerPanel.InitPlayer(3, 3, PlayerPanel.playgrid);
        }
        private void TwelveGrid(object obj)
        {
            PlayerPanel.DrawPlayGrid(4, 3, PlayerPanel.playgrid);
            PlayerPanel.InitPlayer(4, 3, PlayerPanel.playgrid);
        }

        private void CloseControl(object obj)
        {
            PlayControl playControl = (PlayControl)obj;

            fightGrid.Children.Remove(playControl);
        }
    }
}
