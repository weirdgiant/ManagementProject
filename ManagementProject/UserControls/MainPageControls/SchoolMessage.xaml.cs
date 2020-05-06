using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Loaded += SchoolMessage_Loaded;
        }

        private async void SchoolMessage_Loaded(object sender, RoutedEventArgs e)
        {
            DeviceCount[] counts = HttpAPi.GetAllDeviceCount(GlobalVariable.CurrentMapId);
            if (counts!=null)
            {
                foreach (var item in counts)
                {
                    SchoolMesItem mesItem = new SchoolMesItem();
                    mesItem.count.Text = item.deviceCount;
                    mesItem.type.Text = item.deviceType;
                    mesItem.image.Source = await HttpAPi.LoadImage(AppConfig.ImageBaseUri + item.deviceIcon);
                    devicePanel.Children.Add(mesItem);
                }
            }
            int count = devicePanel.Children.Count;
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


        private string _schoolName;
        public string SchoolName
        {
            get
            {
                return _schoolName;
            }
            set
            {
                _schoolName = value;
                NotifyPropertyChanged("SchoolName");
            }
        }


        private School _school;
        public School SchoolMes
        {
            get
            {
                return _school;
            }
            set
            {
                _school = value;
                NotifyPropertyChanged("SchoolMes");
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
            //InitMes();
        }


        public void InitMes()
        {
            MangoMap[] map = GlobalVariable.MapList;
            MangoMap[] results = map.Where(x => x.pid == GlobalVariable .CurrentMapId).ToArray();
            SchoolName =SchoolMes.SchoolName;
            CreationDate = "";
            BuildingNumber = results.Length.ToString();
            Head = SchoolMes.Contact;
            Phone = SchoolMes.Phone;
            Remarks = SchoolMes.Discription;
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
