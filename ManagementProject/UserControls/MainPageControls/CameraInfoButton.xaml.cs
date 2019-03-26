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
    /// CameraInfoButton.xaml 的交互逻辑
    /// </summary>
    public partial class CameraInfoButton : UserControl
    {
        public CameraInfoViewModel cameraInfoViewModel { get; set; }
        public CameraInfoButton()
        {
            InitializeComponent();
            cameraInfoViewModel = new CameraInfoViewModel();
            DataContext = cameraInfoViewModel;
        }
    }
    public class CameraInfoModel:INotifyPropertyChangedClass
    {
        private bool _isOpened;
        public bool IsOpened
        {
            get
            {
                return _isOpened;
            }
            set
            {
                _isOpened = value;
                NotifyPropertyChanged("IsOpened");
            }
        }
        private ObservableCollection<ErrorCamera> _errorCameraList;
        public ObservableCollection<ErrorCamera> ErrorCameraList
        {
            get
            {
                return _errorCameraList;
            }
            set
            {
                _errorCameraList = value;
                NotifyPropertyChanged("ErrorCameraList");
            }
        }
    }
    public class CameraInfoViewModel:CameraInfoModel
    {
        public DelegateCommand ShowInfoCommand { get; set; }
        public DelegateCommand ShowErrorCameraInfoCommand { get; set; }
        public CameraInfoViewModel()
        {
            ShowInfoCommand = new DelegateCommand();
            ShowInfoCommand.ExecuteCommand = new Action<object>(ShowInfo);
            ShowErrorCameraInfoCommand = new DelegateCommand();
            ShowErrorCameraInfoCommand.ExecuteCommand = new Action<object>(ShowErrorCameraInfo);
        }
        private void ShowInfo(object obj)
        {
            ErrorCamera[] errorCamera= HttpAPi.GetErrorCamera(AppConfig.ServerBaseUri + AppConfig.GetErrorCamera);
            ErrorCameraList = new ObservableCollection<ErrorCamera>(errorCamera);
        }
        private void ShowErrorCameraInfo(object obj)
        {
            ErrorCamera tb = (ErrorCamera)obj;
        }
    }
}
