using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// MainWindowSearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowSearchBox : UserControl
    {
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }
        public MainWindow _mainWin;
        public MainWindowSearchBox()
        {
            InitializeComponent();
            _mainWin = (MainWindow)Application.Current.MainWindow;
            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = mainWindowViewModel.mainPageViewModel;
            searchbt.Click += Searchbt_Click;
            searchcontent.LostFocus += Searchcontent_LostFocus;
            //buildlist.SelectionChanged += Buildlist_SelectionChanged;
            //devicelist.SelectionChanged += Devicelist_SelectionChanged;
            clearbt.Click += Clearbt_Click;

        }
        private void buildItemName_Click(object sender, RoutedEventArgs e)
        {
            RadioButton toggle = (RadioButton)sender;
            if (toggle != null)
            {
                if (GlobalVariable.SelectedSchoolId != toggle.TabIndex)
                {
                    GlobalVariable.SelectedSchoolId = toggle.TabIndex;
                }
                mainPageViewModel.SelectedDeviceCode = toggle.Tag .ToString();
            }
            pop.IsOpen = false;
        }

        private void deviceItemName_Click(object sender, RoutedEventArgs e)
        {
            RadioButton toggle = (RadioButton)sender;
            if (toggle != null)
            {
                if (GlobalVariable.SelectedSchoolId != toggle.TabIndex)
                {
                    GlobalVariable.SelectedSchoolId = toggle.TabIndex;
                }
                mainPageViewModel.SelectedDeviceCode = toggle.Tag.ToString();
            }
            pop.IsOpen = false;
        }

        private void Clearbt_Click(object sender, RoutedEventArgs e)
        {
            searchcontent.Text = null;
            pop.IsOpen = false;
            pop.StaysOpen = false;
        }

        private void Devicelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Element element = (Element)devicelist.SelectedItem;
            if (element!=null)
            {
                if (GlobalVariable.SelectedSchoolId != element.mapId)
                {
                    GlobalVariable.SelectedSchoolId = element.mapId;
                }
                mainPageViewModel.SelectedDeviceCode = element.code;
                pop.IsOpen = false;
            }
        }

        private void Buildlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Element element = (Element)buildlist.SelectedItem;
            if (element != null)
            {
                if (GlobalVariable.SelectedSchoolId!= element.mapId)
                {
                    GlobalVariable.SelectedSchoolId = element.mapId;
                }                
                mainPageViewModel.SelectedDeviceCode = element.code;             
            }
            pop.IsOpen = false;
        }

        public List<Element> BuildingList ;
        public List<Element> DeviceList;

        private void Searchbt_Click(object sender, RoutedEventArgs e)
        {
            this.device.Visibility = Visibility.Collapsed;
            this.build.Visibility = Visibility.Collapsed;
            buildlist.ItemsSource = null; 
            devicelist.ItemsSource = null;
            string text = searchcontent.Text.Trim();
            Element[] elements =HttpAPi. GetAllElements(App.mango.getClientInfo().userId);
            if (elements==null)
            {
                return;
            }
            Element[] device = elements.Where(x => x.deviceTypeCode != "@Building"&& x.deviceTypeCode != "@BuildingIcon").ToArray();
            Element[] building= elements.Where(x => x.deviceTypeCode == "@Building").ToArray();


            Element[] SelectedDevice = device.Where(x => x.name.Contains(text)).ToArray();
            Element[] SelectedBuilding = building.Where(x => x.name.Contains(text)).ToArray();
            if (SelectedDevice.Length !=0&& text!="")
            {
                this.device.Visibility = Visibility.Visible;
                //this.device.IsSelected = true;
                this.device.IsExpanded = true;
                DeviceList = new List<Element>(SelectedDevice);
                devicelist.ItemsSource = DeviceList;
            }

            if (SelectedBuilding.Length !=0 && text != "")
            {
                this.build.Visibility = Visibility.Visible;
                this.build.IsExpanded = true;
                BuildingList = new List<Element>(SelectedBuilding);
                buildlist.ItemsSource = BuildingList;
            }



            if ( text != ""&SelectedDevice.Length != 0|| text != "" & SelectedBuilding.Length != 0)
            {
                pop.IsOpen = true;
                pop.StaysOpen = true;
            }

            if (text != "" & SelectedDevice.Length == 0 && text != "" & SelectedBuilding.Length == 0)
            {
                MessageBox.Show("搜索不到任何符合要求的对象!");
            }
            
        }

        private void Searchcontent_LostFocus(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = false;
            pop.StaysOpen = false;
        }


    }

    public class MainWindowSearchModel:INotifyPropertyChangedClass
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


        private string searchContent;

        public string SearchContent
        {
            get { return searchContent; }
            set
            {
                searchContent = value;
                NotifyPropertyChanged("SearchContent");
                OnTextChanged();
            }
        }

        private void OnTextChanged()
        {
            if (string.IsNullOrEmpty(SearchContent))
            {

            }
        }
    }
    public class MainWindowSearchViewModel:MainWindowSearchModel
    {

        public MainWindowSearchViewModel()
        {
        }
    }


    public class BuildingNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return null;
                var building = value.ToString();
                return building.Substring(building.LastIndexOf('/') + 1).Replace(".png", "");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
