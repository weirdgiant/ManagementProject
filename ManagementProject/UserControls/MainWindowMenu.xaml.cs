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
    /// MainWindowMenu.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowMenu : UserControl
    {
        public MainWindowMenu()
        {
            InitializeComponent();
            Loaded += MainWindowMenu_Loaded;
            //menubt.MouseEnter += Menubt_MouseEnter;
            //menubt.MouseLeave += Menubt_MouseLeave;
            menubt.Click += Menubt_Click;
            
        }

        private void MainWindowMenu_Loaded(object sender, RoutedEventArgs e)
        {
             showpanel.Visibility = Visibility.Collapsed;
        }

        public bool IsOpened = false;
        private void Menubt_Click(object sender, RoutedEventArgs e)
        {
            if (IsOpened==false)
            {
                menubtimage.Source = new BitmapImage(new Uri(@"/ImageSource/Icon/menuicon/menuopen.png", UriKind.Relative));
                showpanel.Visibility = Visibility.Visible;
                IsOpened = true;
            }
            else
            {
                menubtimage.Source = new BitmapImage(new Uri(@"/ImageSource/Icon/menuicon/menuclose.png", UriKind.Relative)); ;
                showpanel.Visibility = Visibility.Collapsed;
                IsOpened = false;
            }
        }

        private void Menubt_MouseLeave(object sender, MouseEventArgs e)
        {
         
            menubtimage.Source = new BitmapImage(new Uri(@"/ImageSource/Icon/menuicon/menuclose.png", UriKind.Relative));;
            showpanel.Visibility = Visibility.Collapsed;
        }

        private void Menubt_MouseEnter(object sender, MouseEventArgs e)
        {
            
            menubtimage.Source = new BitmapImage(new Uri(@"/ImageSource/Icon/menuicon/menuopen.png", UriKind.Relative));
            showpanel.Visibility = Visibility.Visible;
        }
    }
}
