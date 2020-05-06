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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.PageView
{
    /// <summary>
    /// AlarmPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmPage : Page
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        public AlarmPageViewModel AlarmPageViewModel { get; set; }
        public AlarmPage()
        {          
            InitializeComponent();
            MainWindow main = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            MainWindowViewModel.alarmPageViewModel.PageType = GlobalVariable.AlarmPageType;
            AlarmPageViewModel= MainWindowViewModel.alarmPageViewModel;
            DataContext = AlarmPageViewModel;
        }
    }
}
