using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }
        public MainWindow _mainWin;
        public CameraInfoViewModel cameraInfoViewModel { get; set; }
        public CameraInfoButton()
        {
            InitializeComponent();
            cameraInfoViewModel = new CameraInfoViewModel();
            DataContext = cameraInfoViewModel;
            _mainWin = (MainWindow)Application.Current.MainWindow;
            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = mainWindowViewModel.mainPageViewModel;
            Loaded += CameraInfoButton_Loaded;
            Unloaded += CameraInfoButton_Unloaded;
        }

        private void CameraInfoButton_Unloaded(object sender, RoutedEventArgs e)
        {
            cameraInfoViewModel.IsStartUp = false; 
        }

        private void CameraInfoButton_Loaded(object sender, RoutedEventArgs e)
        {
            cameraInfoViewModel.ErrorCamera();
        }

        private void selectedbt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton toggle = (ToggleButton)sender;
                
                mainPageViewModel.SelectedDeviceCode = null;
                if (!string.IsNullOrEmpty(toggle.Tag.ToString()))
                {
                    int mapid = toggle.TabIndex;
                    if (toggle.IsChecked == true)
                    {
                        GlobalVariable.SelectedSchoolId = mapid;
                        mainPageViewModel.SelectedDeviceCode = toggle.Tag.ToString();
                    }
                    else
                    {
                        GlobalVariable.SelectedSchoolId = GlobalVariable.CurrentMapId;
                    }
                   
                  
                    TextBlock toggleTextBlock = (TextBlock)toggle.Content;//#FFC24444

                    //var d = toggleTextBlock.DataContext;
                    //Console.WriteLine(toggle.IsChecked+"@@@@@@@@@@@@@@");

                    toggleTextBlock.Foreground = (toggle.IsChecked == true) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFC24444")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9B9B9B"));
                }
               

            }
            catch
            {
                Logger.Info("查询不到异常摄像机编号！");
            }

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

        private bool _isChecked;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged("IsChecked");
                
            }
        }
        private string _icon;
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                NotifyPropertyChanged("Icon");
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

        private Visibility _isHidden;
        public Visibility IsHidden
        {
            get
            {
                return _isHidden;
            }
            set
            {
                _isHidden = value;
                NotifyPropertyChanged("IsHidden");
            }
        }

    }
    public class CameraInfoViewModel:CameraInfoModel
    {
        public bool IsStartUp { get; set; } = false;
        public DelegateCommand ShowInfoCommand { get; set; }
        public DelegateCommand ShowErrorCameraInfoCommand { get; set; }
        public DelegateCommand LoadedComand { get; set; }
        public DelegateCommand UnLoadedCommand { get; set; }
        public CameraInfoViewModel()
        {
            IsChecked = false;
            ShowInfoCommand = new DelegateCommand();
            ShowInfoCommand.ExecuteCommand = new Action<object>(ShowInfo);
            ShowErrorCameraInfoCommand = new DelegateCommand();
            ShowErrorCameraInfoCommand.ExecuteCommand = new Action<object>(ShowErrorCameraInfo);
            LoadedComand = new DelegateCommand();
            LoadedComand.ExecuteCommand = new Action<object>(Loaded);
            UnLoadedCommand = new DelegateCommand();
            UnLoadedCommand.ExecuteCommand = new Action<object>(UnLoaded);
            //ErrorCamera();
        }

        private void Loaded(object obj)
        {
            ErrorCamera();
        }

        private void UnLoaded(object obj)
        {
            IsStartUp = false;
        }
        private void ShowInfo(object obj)
        {
            //GetErrorCamera();
        }
        private void ShowErrorCameraInfo(object obj)
        {
            ErrorCamera tb = (ErrorCamera)obj;
            int id = tb.id;
        }

        private void GetErrorCamera()
        {
            ErrorCamera[] errorCamera = HttpAPi.GetErrorCamera(AppConfig.ServerBaseUri + AppConfig.GetErrorCamera);
            if (errorCamera == null)
            {
                IsOpened = false;
                IsHidden = Visibility.Collapsed;
                return;
            }
            if (errorCamera.Length  != 0)
            {
                IsHidden = Visibility.Visible;
                ErrorCameraList = new ObservableCollection<ErrorCamera>(errorCamera);
            }
            else
            {
                IsOpened = false;
                IsHidden = Visibility.Collapsed;
                //MessageBox.Show("未检测到异常摄像机！");
            }
        }

        public async void ErrorCamera()
        {
            IsStartUp = true;
            while (IsStartUp)
            {
                GetErrorCamera();
                await Task.Delay(1000);
            }
        }
    }
}
