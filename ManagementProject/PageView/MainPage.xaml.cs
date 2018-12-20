using ManagementProject.UserControls;
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
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            choosetb.message.MouseLeftButtonDown += Message_MouseLeftButtonDown;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage img1 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/摄像机.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/水压设备.png", UriKind.Relative));
            camerastatistics.image.Source = img1;
            camerastatistics.number.Text = "7";
            sensorstatistics.image.Source = img2;
            sensorstatistics.number.Text = "7";


            BitmapImage img3 = new BitmapImage(new Uri(@"/ImageSource/Icon/AlarmIcon/车辆违停.png", UriKind.Relative));
            alarmbt.alarmicon.Source = img3;
            alarmbt.alarmcount.Text = "2";
        }

        private void Message_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SchoolMessage scm = new SchoolMessage();
            scm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scm.ShowDialog();
        }
    }
}
