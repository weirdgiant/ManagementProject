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

        IsPlayerVisbility IsPlayerVisbility;
        public Player()
        {
            InitializeComponent();
            IsPlayerVisbility = new IsPlayerVisbility();
            DataContext = IsPlayerVisbility;
           
            bottomgrid.MouseLeftButtonDown += Player_MouseLeftButtonDown;
        }

     
        private void Player_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPlayerVisbility.IsOpened== true)
            {
                IsPlayerVisbility.IsOpened = false;
            }else
            {
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
