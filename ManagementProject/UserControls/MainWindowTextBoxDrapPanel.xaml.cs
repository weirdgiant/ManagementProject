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
    /// MainWindowTextBoxDrapPanel.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowTextBoxDrapPanel : UserControl
    {
        public MainWindowTextBoxDrapPanelViewModel MainWindowTextBoxDrapPanelViewModel;
        public MainWindowTextBoxDrapPanel()
        {
            InitializeComponent();
            MainWindowTextBoxDrapPanelViewModel = new MainWindowTextBoxDrapPanelViewModel();
            DataContext = MainWindowTextBoxDrapPanelViewModel;
        }
    }
    public class MainWindowTextBoxDrapPanelModel:INotifyPropertyChangedClass
    {
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

    public class MainWindowTextBoxDrapPanelViewModel: MainWindowTextBoxDrapPanelModel
    {
        public DelegateCommand ItemSelectedCommand { get; set; }
        public MainWindowTextBoxDrapPanelViewModel()
        {
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

        public void ItemSelected( object obj)
        {
           
              string aa=  obj.ToString();

            

        }
    }

}
