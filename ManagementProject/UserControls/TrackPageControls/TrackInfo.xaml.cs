using System.Windows;

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// TrackInfo.xaml 的交互逻辑
    /// </summary>
    public partial class TrackInfo : Window
    {
        public TrackInfo()
        {
            InitializeComponent();
            Loaded += TrackInfo_Loaded;
        }

        private void TrackInfo_Loaded(object sender, RoutedEventArgs e)
        {
            tracker.DataContext = DataContext;
        }
    }
}
