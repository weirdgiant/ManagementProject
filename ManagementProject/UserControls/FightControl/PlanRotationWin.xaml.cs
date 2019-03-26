using ManagementProject.UserControls.TimeControl;
using ManagementProject.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// PlanRotationWin.xaml 的交互逻辑
    /// </summary>
    public partial class PlanRotationWin : Window
    {
        public PlanRotationWin()
        {
            InitializeComponent();
            DataContext = new PlanRotationWinViewModel();
        }
    }    
}
