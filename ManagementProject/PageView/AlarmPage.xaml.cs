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
            //InitControl(AlarmPageViewModel.PageType);
        }

        /// <summary>
        /// 根据报警类型加载控件
        /// </summary>
        /// <param name="type">报警类型</param>
        private void InitControl(AlarmType type)
        {
            switch (type)
            {
                case AlarmType.FireAlarm:
                    InitFireAlarmControls();
                    break;
                case AlarmType.WaterAlarm:
                    InitWaterAlarmControls();
                    break;
                case AlarmType.CarAlarm:
                    InitCarAlarmControls();
                    break;
            }
        }
        /// <summary>
        /// 初始化火灾报警控件
        /// </summary>
        private void InitFireAlarmControls()
        {
            AlarmMainPage alarmMainPage = new AlarmMainPage
            {
                DataContext = AlarmPageViewModel
            };
            mianGrid.Children.Add(alarmMainPage);
        }
        /// <summary>
        /// 初始化水压报警控件
        /// </summary>
        private void InitWaterAlarmControls()
        {
            AlarmMainPage alarmMainPage = new AlarmMainPage();
            alarmMainPage.DataContext = AlarmPageViewModel;
            WaterMessage waterMessage = new WaterMessage();
            waterMessage.DataContext = AlarmPageViewModel.waterMessageViewModel;
            alarmMainPage.panel2.Children.Add(waterMessage);
            mianGrid.Children.Add(alarmMainPage);
        }
        /// <summary>
        /// 初始化车辆报警控件
        /// </summary>
        private void InitCarAlarmControls()
        {
            AlarmMainPage alarmMainPage = new AlarmMainPage();
            alarmMainPage.DataContext = AlarmPageViewModel;

            AlarmCarInfo alarmCarInfo = new AlarmCarInfo();
            alarmCarInfo.DataContext = AlarmPageViewModel.alarmCarInfoViewModel;
            alarmMainPage.panel2.Children.Add(alarmCarInfo);
            mianGrid.Children.Add(alarmMainPage);
        }
    }
}
