using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class Player : UserControl
    {

        public IsPlayerVisbility IsPlayerVisbility;
        public Player()
        {
            InitializeComponent();
            IsPlayerVisbility = new IsPlayerVisbility();
            DataContext = IsPlayerVisbility;
            
            bottomgrid.MouseLeftButtonDown += Player_MouseLeftButtonDown;
            SizeChanged += Player_SizeChanged;

            ///获取控件Handel
            HwndSource hs = (HwndSource)PresentationSource.FromDependencyObject(mediaElement);

        }

      

        /// <summary>
        /// 根据实际窗口大小改变全屏按钮位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ActualWidth;
            fullgrid.Margin =new Thickness(ActualWidth - 100,0,0,0);
            IsPlayerVisbility.IsOpened = false;
        }

        private void Player_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPlayerVisbility.IsOpened== true)
            {
                IsPlayerVisbility.IsOpened = false;
            }else
            {
                IsPlayerVisbility.IsOpened = false;
                IsPlayerVisbility.IsOpened = true;
            }
            
           
        }
     
    }
    public class IsPlayerVisbility : INotifyPropertyChangedClass
    {
        private bool isopened;

        public bool IsOpened
        {
            get { return isopened; }
            set
            {
                isopened = value;
                NotifyPropertyChanged("IsOpened");
            }
        }
    }
}
