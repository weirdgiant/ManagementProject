using ManagementProject.PageView;
using ManagementProject.UserControls;
using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
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
        private WindowControlButton  ControlButton;
        public int windowstate = 0;//0-最大化 ,1正常
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            closebt.MouseEnter += Closebt_MouseEnter;
            closebt.MouseLeave += Closebt_MouseLeave;

            choosetb.message.MouseLeftButtonDown += Message_MouseLeftButtonDown;

            this.Loaded += MainWindow_Loaded;
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            titletime.Content  = DateTime.Now.ToString("yyyy-M-d HH:mm:ss");

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
    }
}
