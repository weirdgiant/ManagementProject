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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// DisposalPlan.xaml 的交互逻辑
    /// </summary>
    public partial class DisposalPlan : UserControl
    {
        public DisposalPlan()
        {
            InitializeComponent();
        }
    }
    public class DisposalPlanModel:INotifyPropertyChangedClass
    {

    }
    public class DisposalPlanViewModel:DisposalPlanModel
    {
        public DelegateCommand OpenDisposalPlanCommand { get; set; }
        public DelegateCommand HandleAlarmCommand { get; set; }
        public DelegateCommand PressHandlingCommand { get; set; }
        public DelegateCommand ShowMessageCommand { get; set; }
        public DisposalPlanViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            OpenDisposalPlanCommand = new DelegateCommand();
            OpenDisposalPlanCommand.ExecuteCommand = new Action<object>(OpenDisposalPlan);
            HandleAlarmCommand = new DelegateCommand();
            HandleAlarmCommand.ExecuteCommand = new Action<object>(HandleAlarm);
            PressHandlingCommand = new DelegateCommand();
            PressHandlingCommand.ExecuteCommand = new Action<object>(PressHandling);
            ShowMessageCommand = new DelegateCommand();
            ShowMessageCommand.ExecuteCommand = new Action<object>(ShowMessage);

        }

        /// <summary>
        /// 打开预案文件
        /// </summary>
        /// <param name="obj"></param>
        private void OpenDisposalPlan(object obj)
        {

        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="obj"></param>
        private void HandleAlarm(object obj)
        {

        }
        /// <summary>
        /// 跟催
        /// </summary>
        /// <param name="obj"></param>
        private void PressHandling(object obj)
        {

        }
        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="obj"></param>
        private void ShowMessage(object obj)
        {

        }
    }
}
