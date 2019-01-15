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
    /// AlarmCarInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmCarInfo : UserControl
    {
        public AlarmCarInfo()
        {
            InitializeComponent();
        }
    }

    public class AlarmCarInfoModel:INotifyPropertyChangedClass
    {
        private string _alarmCarNumber;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string AlarmCarNumber
        {
            get
            {
                return _alarmCarNumber;
            }
            set
            {
                _alarmCarNumber = value;
                NotifyPropertyChanged("AlarmCarNumber");
            }
        }
        private string _alarmCarOwner;
        /// <summary>
        /// 车主
        /// </summary>
        public string AlarmCarOwner
        {
            get
            {
                return _alarmCarOwner;
            }
            set
            {
                _alarmCarOwner = value;
                NotifyPropertyChanged("AlarmCarOwner");
            }
        }

        private string _phoneNymber;
        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return _phoneNymber;
            }
            set
            {
                _phoneNymber = value;
                NotifyPropertyChanged("PhoneNumber");
            }
        }

        private string _department;
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
                NotifyPropertyChanged("Department");
            }
        }

        
    }

    public class AlarmCarInfoViewModel:AlarmCarInfoModel
    {
        public AlarmCarInfoViewModel()
        {

        }

        public void SetAlarmCarInfo()
        {
            AlarmCarNumber = "京A02019";
            AlarmCarOwner = "MySelf";
            PhoneNumber = "88888888";
            Department = "58";
        }
    }
}
