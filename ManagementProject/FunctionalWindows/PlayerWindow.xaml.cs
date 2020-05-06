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
        public MapControl mapControl { get; set; }
        public List<string> CodeList = new List<string>();
        private PlayerPanel playerPanel;
        public PlayerWindowViewModel PlayerWindowViewModel { get; set; }
        public PlayerControlViewModel PlayerControlViewModel;

        public MainWindowViewModel mainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }
        public MainWindow _mainWin;
        private string _code;
        private PlayerWindowType _type;
        public PlayerWindow(PlayerWindowType type,string code)
        {

            InitializeComponent();
            Loaded += PlayerWindow_Loaded;
            _code = code;
            _type = type;
             PlayerWindowViewModel = new PlayerWindowViewModel(type,code);
            if (GlobalVariable .IsAlarmPage)
            {
                PlayerWindowViewModel.IsAlarmTracker = Visibility.Collapsed;
            }           
            DataContext = PlayerWindowViewModel;
            Closing += PlayerWindow_Closing;
            _mainWin = (MainWindow)Application.Current.MainWindow;
            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = mainWindowViewModel.mainPageViewModel;
            PlayerControlViewModel = new PlayerControlViewModel();
            if (type == PlayerWindowType.Playerback)
            {
                PlayerControlViewModel.IsAllVODPlay = false ;
            }
            else
            {
                PlayerControlViewModel.IsAllVODPlay = true;
                mainPageViewModel.IsTracker = true;
                mainPageViewModel.TrackStart();
            }
           
            PlayerControlViewModel.SelectedPlayerPanel = playerPanel;
            
            control.DataContext = PlayerControlViewModel;
            // MouseLeftButtonDown += PlayerWindow_MouseLeftButtonDown;          
        }

        private void PlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitPanel(_type, _code);
            PlayerWindowViewModel.SetPanel(playerPanel);
            PlayerControlViewModel.SelectedPlayerPanel = playerPanel;
        }

        private void InitPanel(PlayerWindowType type,string code)
        {
            CodeList.Clear();
            playerPanel = new PlayerPanel();
            playerPanel.mapControl = mapControl;
            mapControl.MoveToTwoThirds(code);
            grid.Children.Add(playerPanel);
            if (type== PlayerWindowType.Playerback )
            {
                playerPanel.InitPlayVod(code);
            }else 
            {
                playerPanel.InitSixPlayerPanel(3, 3, code, playerPanel.playgrid,ref CodeList, type);
            }
            Grid.SetRow(playerPanel, 1);
        }

        private void PlayerWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PlayerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (GlobalVariable.IsAlarmPage ==false)
            {
                mainPageViewModel.IsSelected = !mainPageViewModel.IsSelected;
                mainPageViewModel.IsTracker = false;
                mainPageViewModel.TrackeEnd();
            }
            playerPanel.Clear();
            GlobalVariable.PlayerWindowIsOpened = false;
        }
    }
}
