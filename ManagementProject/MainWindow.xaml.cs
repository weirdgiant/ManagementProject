using ManagementProject.FunctionalWindows;

using ManagementProject.PageView;
using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;


namespace ManagementProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel MainWindowViewModel { get; set; }
        private WindowControlButton  ControlButton;
        public int windowstate = 0;//0-最大化 ,1正常
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel  = new MainWindowViewModel();

            DataContext =MainWindowViewModel;

            closebt.MouseEnter += Closebt_MouseEnter;
            closebt.MouseLeave += Closebt_MouseLeave;

            choosetb.message.MouseLeftButtonDown += Message_MouseLeftButtonDown;
            choosetb.drapbt.Click += Drapbt_Click;

            this.Loaded += MainWindow_Loaded;
        }

        private void Drapbt_Click(object sender, RoutedEventArgs e)
        {
            if(GlobalVariable.MainWindowTextBoxIsDraped == false)
            {

                drappanel.Visibility = Visibility.Collapsed;

               // GlobalVariable.MainWindowTextBoxIsDraped = true;

            }
            else
            {
                drappanel.Visibility = Visibility.Visible;

               // GlobalVariable.MainWindowTextBoxIsDraped = false;
            }
            

           
        }

        private void Message_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            SchoolMessage scm = new SchoolMessage();
            scm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scm.ShowDialog();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPage();

            BitmapImage img1 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/摄像机.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/水压设备.png", UriKind.Relative));
            camerastatistics.image.Source = img1;
            camerastatistics.number.Text = "7";
            sensorstatistics.image.Source = img2;
            sensorstatistics.number.Text = "7";

        }


        MainPage mp;
        private void LoadPage()
        {
            mp = new MainPage();
            mainframe.Navigate(mp);
        }

        private void Closebt_MouseLeave(object sender, MouseEventArgs e)
        {
            closebt.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Closebt_MouseEnter(object sender, MouseEventArgs e)
        {
            closebt.Background = new SolidColorBrush(Colors.Red);
        }

        private void SetEvent()
        {

            ControlButton = new WindowControlButton();
            ControlButton.Click += ControlButton_Click;
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }

        private void closebt_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
            
        }

        private void recoverbt_Click(object sender, RoutedEventArgs e)
        {
            if (windowstate==0)
            {
                recoverbt.Content = "Max";
                WindowState = WindowState.Normal;
                windowstate = 1;
            }
            else if (windowstate == 1)
            {
                recoverbt.Content = "Normal";
                WindowState = WindowState.Maximized;
                windowstate = 0;
            }

        }

        private void minbt_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        
        private void button3_Click(object sender, RoutedEventArgs e)
        {
           
            if (GlobalVariable.PlayerWindowIsOpened==false)
            {
                PlayerWindow newwindow = new PlayerWindow(PlayerWindowType.Track );
                newwindow.Topmost = true;
                newwindow.Owner = this;
                newwindow.WindowStartupLocation = WindowStartupLocation.Manual;
                newwindow.Left = 23;
                newwindow.Top = 165;
                newwindow.Show();
                GlobalVariable.PlayerWindowIsOpened = true;
            }
           
        }
    }
}
