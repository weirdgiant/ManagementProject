using System.Collections.ObjectModel;
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
    /// TrackCameraList.xaml 的交互逻辑
    /// </summary>
    public partial class TrackCameraList : UserControl
    {
        public TrackCameraList()
        {
            InitializeComponent();
            DataContext = new TrackCameraListViewModel();
        }
    }
    /// <summary>
    /// 相机信息
    /// </summary>
    class CameraInfo
    {
        /// <summary>
        /// 相机名称
        /// </summary>
        public string Name { get; set; }
    }

    class TrackCameraListViewModel : TrackCameraListModel
    {
        public TrackCameraListViewModel()
        {
            CameraLists = new ObservableCollection<CameraInfo>();

            for (int i = 1; i <= 5; i++)
            {
                CameraLists.Add(new CameraInfo
                {
                    Name = $"教学楼摄像机{i}"
                });
            }
        }
    }
    class TrackCameraListModel : INotifyPropertyChangedClass
    {
        private ObservableCollection<CameraInfo> cameraLists;

        public ObservableCollection<CameraInfo> CameraLists
        {
            get { return cameraLists; }
            set
            {
                cameraLists = value;
                NotifyPropertyChanged("CameraLists");
            }
        }
    }
}
