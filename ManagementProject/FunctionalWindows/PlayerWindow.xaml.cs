using ManagementProject.UserControls;
using ManagementProject.ViewModel;
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
using System.Windows.Shapes;

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// PlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerWindow : Window
    {
        private PlayerPanel playerPanel;
        public PlayerWindowViewModel PlayerWindowViewModel { get; set; }
        public PlayerWindow(PlayerWindowType type)
        {

            InitializeComponent();
            InitPanel(type);
            PlayerWindowViewModel = new PlayerWindowViewModel(playerPanel,type);
            DataContext = PlayerWindowViewModel;

            Closing += PlayerWindow_Closing;
           // MouseLeftButtonDown += PlayerWindow_MouseLeftButtonDown;
            
        }


        private void InitPanel(PlayerWindowType type)
        {
            playerPanel = new PlayerPanel(type);
            grid.Children.Add(playerPanel);
            Grid.SetRow(playerPanel,1);
        }

        private void PlayerWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PlayerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GlobalVariable.PlayerWindowIsOpened = false;
        }

        private void closebt_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minbt_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void recoverbt_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}
