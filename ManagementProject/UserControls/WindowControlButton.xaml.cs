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

namespace ManagementProject
{
    /// <summary>
    /// WindowControlButton.xaml 的交互逻辑
    /// </summary>
    public partial class WindowControlButton : UserControl
    {

        public event EventHandler Click;
        public WindowControlButton()
        {
            InitializeComponent();
            closebt.MouseEnter += Closebt_MouseEnter;
            closebt.MouseLeave += Closebt_MouseLeave;
            minbt.MouseEnter += Minbt_MouseEnter;
            minbt.MouseLeave += Minbt_MouseLeave;
        }

        private void Minbt_MouseLeave(object sender, MouseEventArgs e)
        {
            minbt.Background = new SolidColorBrush(Colors.Transparent);
            minbt.Width = 40;
            minbt.Height = 40;
        }

        private void Minbt_MouseEnter(object sender, MouseEventArgs e)
        {
            minbt.Background = new SolidColorBrush(Colors.Transparent);
            minbt.Width = 50;
            minbt.Height = 50;
        }

        private void Closebt_MouseLeave(object sender, MouseEventArgs e)
        {
            closebt.Background = new SolidColorBrush(Colors.Transparent);
            closebt.Width = 40;
            closebt.Height = 40;
        }

        private void Closebt_MouseEnter(object sender, MouseEventArgs e)
        {
            closebt.Background = new SolidColorBrush(Colors.Transparent);
            closebt.Width = 50;
            closebt.Height = 50;
        }

        private void minbt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void closebt_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(sender, e);
        }

        private void recoverbt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
