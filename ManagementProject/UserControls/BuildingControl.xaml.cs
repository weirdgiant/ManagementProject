using ManagementProject.ViewModel;
using MangoApi;
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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// BulidingControl.xaml 的交互逻辑
    /// </summary>
    public partial class BuildingControl : Window
    {
        private MainWindow _mainWin { get; set; }
        private MainWindowViewModel MainWindowViewModel;
        private MainWindowTextBoxViewModel  TextBoxViewModel { get; set; }
        private Dictionary<int, RadioButton> TabButtonDic = new Dictionary<int, RadioButton>();
        public BuildingControl()
        {
            InitializeComponent();
            Loaded += BulidingControl_Loaded;
        }

        private void BulidingControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            // InitTab();
        }

        public void SetTextBox()
        {
            _mainWin = (MainWindow)Application.Current.MainWindow;
            MainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            TextBoxViewModel = MainWindowViewModel.mainPageViewModel.mainWindowTextBoxViewModel;
            TextBoxViewModel.InBuildingMap();
        }

        public void SetCurrentId(int id)
        {
            RadioButton radio = TabButtonDic[id];
            TextBoxViewModel.SchoolName = radio.ToolTip.ToString();
            radio.IsChecked = true;
        }

        public void InitTab(MangoMap[] mangoMaps)
        {
            foreach (var item in mangoMaps.Reverse())
            {
                RadioButton radio = TabButton(item);
                radio.Click += Radio_Click;
                TabButtonDic.Add(item.id, radio);
                Grid grid = new Grid() { Height = 10, };
                panel.Children.Add(grid);
                panel.Children.Add(radio);
            }
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            int id = (int)radio.DataContext;
            GlobalVariable.SelectedSchoolId = id;
            TextBoxViewModel.SchoolName = radio.ToolTip.ToString();
        }

        private RadioButton TabButton(MangoMap map)
        {
            RadioButton radioButton = new RadioButton()
            {
                Width = 45,
                Height = 35,
                Style = (Style)FindResource("TrackeTab"),
                //Content = map.idx + "F",
                //ToolTip = map.name,
                //DataContext = map.id,
                //VerticalAlignment = VerticalAlignment.Center,
                //HorizontalAlignment=HorizontalAlignment.Center,
                //VerticalContentAlignment = VerticalAlignment.Center,
                //HorizontalContentAlignment = HorizontalAlignment.Center,

            };
            Label label = new Label()
            {
                Width = 45,
                Height = 35,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("white")),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = map.idx + "F",
            };
            Grid grid = new Grid()
            {
                Width = 45,
                Height = 35,
            };
            grid.Children.Add(label);
            radioButton.Content = grid;
            radioButton.ToolTip = map.name;
            radioButton.DataContext = map.id;
            return radioButton;
        }
    }
}
