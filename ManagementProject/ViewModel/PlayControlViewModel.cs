using ManagementProject.Helper;
using ManagementProject.Model;
using ManagementProject.UserControls.FightControl;
using System;
using System.Windows.Controls;

namespace ManagementProject.ViewModel
{
    public class PlayControlViewModel : PlayControlModel
    {
        #region Property

        private Grid FightGrid { get; set; }
        private FightPanel FightPanel { get; set; }

        public DelegateCommand OneGridCommand { get; set; }
        public DelegateCommand FourGridCommand { get; set; }
        public DelegateCommand SixGridCommand { get; set; }
        public DelegateCommand NineGridCommand { get; set; }
        public DelegateCommand SixteenGridGridCommand { get; set; }
        public DelegateCommand CloseControlCommand { get; set; }

        #endregion

        #region Construction Method

        public PlayControlViewModel(PlayControl playControl)
        {            
            InitControl(playControl);
            InitCommand();
        }

        private void InitControl(PlayControl playControl)
        {
            FightPanel = playControl.fightPanel;
            FightGrid = playControl.fightGrid;
        }

        #endregion

        #region Initialization Command

        private void InitCommand()
        {
            OneGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(OneGrid) };
            FourGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(FourGrid) };
            SixGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SixGrid) };
            NineGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(NineGrid) };
            SixteenGridGridCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SixteenGrid) };
            CloseControlCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseControl) };
        }
    

        #endregion

        #region Window Operate

        private void OneGrid(object obj) => FightPanel.DrawPlayGrid(1, FightPanel.GridFight);

        private void FourGrid(object obj) => FightPanel.DrawPlayGrid(4, FightPanel.GridFight);

        private void SixGrid(object obj) => FightPanel.DrawPlayGrid(6, FightPanel.GridFight);

        private void NineGrid(object obj) => FightPanel.DrawPlayGrid(9, FightPanel.GridFight);

        private void SixteenGrid(object obj) => FightPanel.DrawPlayGrid(16, FightPanel.GridFight);

        private void CloseControl(object obj)
        {
            var playControl = (PlayControl)obj;
            FightGrid.Children.Remove(playControl);

            PinkongHelper.CloseWindowAsync(playControl.winParams.id);
        }
        #endregion
    }
}
