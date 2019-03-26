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

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// PlanRotation.xaml 的交互逻辑
    /// </summary>
    public partial class PlanRotationControl : UserControl
    {
        public PlanRotationControl()
        {
            InitializeComponent();
            DataContext = new PlanRotationViewModel();

        }
    }

    class PlanRotationViewModel : PlanRotationModel
    {
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }

        public PlanRotationViewModel()
        {
            RotationName = "计划轮训1";
            DeleteCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Delete) };
            EditCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Edit) };
        }

        private void Edit(object obj)
        {
            TextBlock txtName = (TextBlock)obj;

            ModifyRotationWin modifyRotation = new ModifyRotationWin(txtName.Text) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            modifyRotation.ShowDialog();
        }

        private void Delete(object obj)
        {
            MessageBox.Show("删除失败");
        }
    }

    class PlanRotationModel : INotifyPropertyChangedClass
    {
        private string rotationName;

        /// <summary>
        /// 计划轮训名称
        /// </summary>
        public string RotationName
        {
            get { return rotationName; }
            set
            {
                rotationName = value;
                NotifyPropertyChanged("RotationName");
            }
        }

        private bool isOpen;

        /// <summary>
        /// 是否打开轮训
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                NotifyPropertyChanged("IsOpen");
            }
        }

    }
}
