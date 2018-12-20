﻿
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

            GlobalVariable.MainWindowTextBoxIsDraped = false;
        }
        
        private void Drapbt_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage img1 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/dropdown.png", UriKind.Relative));
            BitmapImage img2 = new BitmapImage(new Uri(@"/ImageSource/Icon/mainwindowicon/raiseup.png", UriKind.Relative)); 
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

        private ObservableCollection<School> schoolList;
        public ObservableCollection<School> SchoolList
        {
            get
            {
                return schoolList;
            }
            set
            {
                schoolList = value;
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
        public DelegateCommand ItemSelectedCommand { get; set; }
        public MainWindowTextBoxViewModel()
        {
            SchoolName = "国定路校区";
            ItemSelectedCommand = new DelegateCommand();
            ItemSelectedCommand.ExecuteCommand = new Action<object>(ItemSelected);
            AddList();
        }

        private void AddList()
        {
            SchoolList = new ObservableCollection<School>();
            string[] name = { "国定路校区", "中山北路校区", "武东路校区", "武川路校区", "昆山路校区" };
            foreach (var item in name)
            {
                School school = new School();
                school.SchoolName = item;

                SchoolList.Add(school);
            }
        }

        public void ItemSelected(object obj)
        {

            string aa = obj.ToString();



        }
    }
}
