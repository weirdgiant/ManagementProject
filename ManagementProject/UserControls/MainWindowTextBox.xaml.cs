﻿using ManagementProject.GlobalClass;
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
    /// MainWindowTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowTextBox : UserControl
    {
        public MainWindowTextBox()
        {
            InitializeComponent();
            GlobalVariable.MainWindowTextBoxIsDraped = false;
        }
        
        private void Drapbt_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img1 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/dropdown.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/raiseup.png", UriKind.Relative)); ;
            img2.Rotation = Rotation.Rotate180;

            if (GlobalVariable.MainWindowTextBoxIsDraped == false )
            {

                drapimage.Source = img2;

                GlobalVariable.MainWindowTextBoxIsDraped = true;
                
            }
            else
            {
                drapimage.Source = img1;

                GlobalVariable.MainWindowTextBoxIsDraped = false;
            }
            
        }
    }
}
