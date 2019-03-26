using ManagementProject.FunctionalWindows;

using ManagementProject.PageView;
using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;


namespace ManagementProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 自定义属性
        private bool _isLoadMainMenu;
        public bool IsLoadMainMenu
        {
            get
            {
                return _isLoadMainMenu;
            }
            set
            {
                _isLoadMainMenu = value;
                if (IsLoadMainMenu)
                {
                   // UnloadMainMenu();
                    //MenuInit();
                }
            }
        }

        private bool _isTrackPage;
        public bool IsTrackPage
        {
            get
            {
                return _isTrackPage;
            }
            set
            {
                _isTrackPage = value;
                if (value)
                {
                    TrackMenuMargin();
                }else
                {
                    MenuMargin();
                }
            }
        }

        private bool _isAlarmPage;
        public bool IsAlarmPage
        {
            get
            {
                return _isAlarmPage;
            }
            set
            {
                _isAlarmPage = value;
                if(value)
                {
                   // UnloadMainMenu();
                }
            }
        }
        #endregion
        private MainWindowViewModel MainWindowViewModel { get; set; }       
        public MainWindow()
        {
          
            InitializeComponent();
            SetMainWindow();
            MainWindowViewModel  = new MainWindowViewModel();
            DataContext =MainWindowViewModel;
        }

        /// <summary>
        /// 将当前窗口设置为主窗口
        /// </summary>
        private void SetMainWindow()
        {
            Application.Current.MainWindow = this;
        }
        #region 设置菜单控件位置
        private void TrackMenuMargin()
        {
            //mainMenuGrid.Margin = new Thickness(0, 0, 350, 50);
            MainWindowViewModel.SetTrackerMainMenuMargin();
        }

        private void MenuMargin()
        {
            //mainMenuGrid.Margin = new Thickness(0, 0, 50, 50);
            MainWindowViewModel.SetMainMenuMargin();
        }

        #endregion

    }
}
