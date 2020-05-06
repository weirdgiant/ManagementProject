﻿using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// AddTracker.xaml 的交互逻辑
    /// </summary>
    public partial class AddTracker : Window
    {
        public AddTracker()
        {
            InitializeComponent();
            MouseDown += AddTracker_MouseDown;
        }

        private void AddTracker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
