﻿using ManagementProject.Model;
using ManagementProject.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementProject.ViewModel
{
    public class AlarmPageViewModel : AlarmPageModel
    {
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand MainPageReturnCommand { get; set; }
        public AlarmPageViewModel(MainWindowViewModel _mainWindowViewModel )
        {
            mainWindowViewModel = _mainWindowViewModel;
            MainPageReturnCommand = new DelegateCommand();
            MainPageReturnCommand.ExecuteCommand = new Action<object>(MainPageReturn);
        }

        private void MainPageReturn(object obj)
        {
            mainWindowViewModel.LoadMainPage();
        }
    }
}
