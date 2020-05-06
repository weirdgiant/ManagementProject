
using ManagementProject.ViewModel;
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
        public MainWindowTextBox()
        {
            InitializeComponent();
        }
    }

    public class MainWindowTextBoxModel:INotifyPropertyChangedClass
    {
        private Visibility _isDrapbt=Visibility.Visible;
        public Visibility IsDrapbt
        {
            get
            {
                return _isDrapbt;
            }
            set
            {
                _isDrapbt = value;
                NotifyPropertyChanged("IsDrapbt");
            }
        }

        private Visibility _isHomebt = Visibility.Collapsed;
        public Visibility IsHomebt
        {
            get
            {
                return _isHomebt;
            }
            set
            {
                _isHomebt = value;
                NotifyPropertyChanged("IsHomebt");
            }
        }


        private Visibility _isInfobt = Visibility.Visible;
        public Visibility IsInfobt
        {
            get
            {
                return _isInfobt;
            }
            set
            {
                _isInfobt = value;
                NotifyPropertyChanged("IsInfobt");
            }
        }

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

                Icon = (IsOpened == true) ? "\xe673" : "\xe653";//chevron-up:chevron-down
            }
        }

        private string _icon = "\xe653";//chevron-down
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

        public string HomeMapName { get; set; }

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


    }
    public class MainWindowTextBoxViewModel:MainWindowTextBoxModel
    {
        public DelegateCommand DrapClickCommand { get; set; }
        public DelegateCommand ItemSelectedCommand { get; set; }
        public DelegateCommand ShowMesCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand ReturnHomeCommand { get; set; }
        public MainWindowTextBoxViewModel()
        {
            ItemSelectedCommand = new DelegateCommand();
            ItemSelectedCommand.ExecuteCommand = new Action<object>(ItemSelected);
            DrapClickCommand = new DelegateCommand();
            DrapClickCommand.ExecuteCommand = new Action<object>(DrapClick);
            ShowMesCommand = new DelegateCommand();
            ShowMesCommand.ExecuteCommand = new Action<object>(ShowMes);
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            ReturnHomeCommand = new DelegateCommand();
            ReturnHomeCommand.ExecuteCommand = new Action<object>(ReturnHome);
        }

        private void ReturnHome(object obj)
        {
            GlobalVariable.SelectedSchoolId = GlobalVariable.CurrentSid;
            InHomeMap();
            SchoolName = HomeMapName;
        }

        public void InHomeMap()
        {
            if (SchoolList.Count == 1)
            {
                IsDrapbt = Visibility.Hidden;
            }else
            {
                IsDrapbt = Visibility.Visible;
            }
            
            IsInfobt = Visibility.Visible;
            IsHomebt = Visibility.Collapsed;
        }

        public void InBuildingMap()
        {
            IsDrapbt = Visibility.Collapsed;
            IsInfobt = Visibility.Hidden;
            IsHomebt = Visibility.Visible;
        }

        private void Loaded(object obj)
        {

        }

        public void AddList()
        {
            try
            {

                SchoolList = new ObservableCollection<School>();
                string url = AppConfig.ServerBaseUri + AppConfig.GetMap;
                MangoMap[] map = HttpAPi.GetMapList(url);
                MangoMap[] results = map.Where(x => x.pid == 0).ToArray();
                string[] sidlist = GlobalVariable.SidList;
                if (sidlist == null) return;
                foreach (var item in sidlist)
                {
                    MangoMap[] ret = results.Where(x => x.id == int.Parse(item)).ToArray();
                    if (ret.Length > 0)
                    {
                        School school = new School();
                        school.SchoolName = ret[0].name;
                        school.SchoolId = ret[0].id;
                        school.Contact = ret[0].securityPerson;
                        school.Phone = ret[0].securityPhone;
                        school.Discription = ret[0].description;
                        SchoolList.Add(school);
                    }
                }
                if (SchoolList.Count ==1)
                {
                    IsDrapbt = Visibility.Hidden;
                }
                School[] result = SchoolList.Where(x => x.SchoolId == GlobalVariable.CurrentMapId).ToArray();
                if (result.Length > 0)
                {
                    SchoolName = result[0].SchoolName;
                    HomeMapName = SchoolName;
                    GlobalVariable.SelectedSchoolId = GlobalVariable.CurrentMapId;
                }
                else
                {
                    // MessageBox.Show("地图加载失败，未配置地图！");
                }

            }
            catch (Exception ex)
            {

                Logger.Error("AddList:" + ex.Message);
            }
        }

        public void ItemSelected(object obj)
        {
            try
            {
                School tb = (School)obj;
                SchoolName = tb.SchoolName;
                IsOpened = false;
                SelectionIndex = -1;
                HomeMapName = SchoolName;
                GlobalVariable.SelectedSchoolId = tb.SchoolId;
                GlobalVariable.CurrentMapId = tb.SchoolId;
                GlobalVariable .CurrentSid= tb.SchoolId;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void ShowMes(object obj)
        {
            try
            {
                School[] result = SchoolList.Where(x => x.SchoolId == GlobalVariable.CurrentMapId).ToArray();
            if (result.Length == 0) return;
            SchoolMessage scm = new SchoolMessage();
            SchoolMesViewModel schoolMesViewModel = new SchoolMesViewModel();
            schoolMesViewModel.SchoolMes = result[0];
            schoolMesViewModel.InitMes();
            scm.DataContext = schoolMesViewModel;
            scm.Topmost = true;
            scm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            scm.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        private void DrapClick(object obj)
        {
            

        }
    }

    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Phone { get; set; }
        public string Contact{ get; set; }
        public string Discription { get; set; }
    }
}
