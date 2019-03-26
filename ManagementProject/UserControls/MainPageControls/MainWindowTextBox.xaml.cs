﻿
using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private MainWindowTextBoxViewModel mainWindowTextBoxViewModel;
        public MainWindowTextBox()
        {
            InitializeComponent();
            mainWindowTextBoxViewModel = new MainWindowTextBoxViewModel();
            DataContext = mainWindowTextBoxViewModel;
        }
    }

    public class MainWindowTextBoxModel:INotifyPropertyChangedClass
    {
        private bool _isOpened;
        public bool IsOpened
        {
            get
            {
                return _isOpened;
            }
            set
            {
                _isOpened = value;
                NotifyPropertyChanged("IsOpened");
                if (IsOpened)
                {

                    Icon = "/ImageSource/Icon/mainwindowicon/raiseup.png";
                }
                else
                {
                    Icon = "/ImageSource/Icon/mainwindowicon/dropdown.png";
                }
            }
        }

        private string _icon;
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon =value;
                NotifyPropertyChanged("Icon");
            }
        }


        private string _schoolName;
        public string SchoolName
        {
            get
            {
                return _schoolName;
            }
            set
            {
                _schoolName = value;
                NotifyPropertyChanged("SchoolName");
            }
        }

        private int _selectedIndex;
        public int SelectionIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged("SelectionIndex");
            }
        }

        private ObservableCollection<School> _schoolList;
        public ObservableCollection<School> SchoolList
        {
            get
            {
                return _schoolList;
            }
            set
            {
                _schoolList = value;
                NotifyPropertyChanged("SchoolList");
            }
        }

        public class School
        {
            public int SchoolId { get; set; }
            public string SchoolName { get; set; }
        }
    }
    public class MainWindowTextBoxViewModel:MainWindowTextBoxModel
    {
        public DelegateCommand DrapClickCommand { get; set; }
        public DelegateCommand ItemSelectedCommand { get; set; }
        public DelegateCommand ShowMesCommand { get; set; }
        public MainWindowTextBoxViewModel()
        {
            Icon = "/ImageSource/Icon/mainwindowicon/dropdown.png";
            ItemSelectedCommand = new DelegateCommand();
            ItemSelectedCommand.ExecuteCommand = new Action<object>(ItemSelected);
            DrapClickCommand = new DelegateCommand();
            DrapClickCommand.ExecuteCommand = new Action<object>(DrapClick);
            ShowMesCommand = new DelegateCommand();
            ShowMesCommand.ExecuteCommand = new Action<object>(ShowMes);
            AddList();
        }

        private void AddList()
        {
            SchoolList = new ObservableCollection<School>();
            string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
            MangoMap[] map = HttpAPi.GetMapList(url);
            MangoMap[] results = map.Where(x => x.pid == 0).ToArray();
            foreach (var item in results)
            {
                School school = new School();
                school.SchoolName = item.name;
                school.SchoolId = item.id;
                SchoolList.Add(school);
            }
            SchoolName = SchoolList[0].SchoolName;
            GlobalVariable.SelectedSchoolId = SchoolList[0].SchoolId;
            GlobalVariable.CurrentMapId = SchoolList[0].SchoolId;
        }

        public void ItemSelected(object obj)
        {
            School tb =(School)obj;
            SchoolName = tb.SchoolName;
            IsOpened = false;
            SelectionIndex = -1;
            GlobalVariable.SelectedSchoolId = tb.SchoolId;
        }

        private void ShowMes(object obj)
        {
            SchoolMessage scm = new SchoolMessage();
            SchoolMesViewModel schoolMesViewModel = new SchoolMesViewModel();
            scm.DataContext = schoolMesViewModel;
            scm.Topmost = true;
            scm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scm.ShowDialog();
        }

        private void DrapClick(object obj)
        {
            

        }
    }
}
