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
    /// SchoolMessage.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolMessage : Window 
    {
        public SchoolMessage()
        {
            InitializeComponent();
        }

        private void closebt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }

    public class SchoolMesModel:INotifyPropertyChangedClass
    {
        private string _creationDate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationDate
        {
            get
            {
                return _creationDate;
            }
            set
            {
                _creationDate = value;
                NotifyPropertyChanged("CreationDate");
            }
        }
        private string _buildingNumber;
        /// <summary>
        /// 建筑数量
        /// </summary>
        public string BuildingNumber
        {
            get
            {
                return _buildingNumber;
            }
            set
            {
                _buildingNumber = value;
                NotifyPropertyChanged("BuildingNumber");
            }
        }
        private string _head;
        /// <summary>
        /// 保卫处负责人
        /// </summary>
        public string Head
        {
            get
            {
                return _head;
            }
            set
            {
                _head = value;
                NotifyPropertyChanged("Head");
            }
        }

        private string _phone;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                NotifyPropertyChanged("Phone");
            }
        }

        private string _remarks;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return _remarks;
            }
            set
            {
                _remarks = value;
                NotifyPropertyChanged("Remarks");
            }
        }

        private string _camera;
        public string Camera
        {
            get
            {
                return _camera;
            }
            set
            {
                _camera = value;
                NotifyPropertyChanged("Camera");
            }
        }

    }

    public class SchoolMesViewModel:SchoolMesModel
    {
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MoveWinCommand { get; set; }
        public SchoolMesViewModel()
        {
            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MoveWinCommand = new DelegateCommand();
            MoveWinCommand.ExecuteCommand = new Action<object>(MoveWin);
            InitMes();
        }


        private void InitMes()
        {
            CreationDate = "";
            BuildingNumber = "";
            Head = "";
            Phone = "";
            Remarks = "";
        }

        private void CloseWin (object obj)
        {
            SchoolMessage scm = (SchoolMessage)obj;
            scm.Close();
        }
        private void MoveWin(object obj)
        {
            SchoolMessage scm = (SchoolMessage)obj;
            scm.DragMove();
        }
    }
}
