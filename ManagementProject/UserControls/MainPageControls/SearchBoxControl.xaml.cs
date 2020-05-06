using ManagementProject.ViewModel;
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
    /// SearchBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBoxControl : Window
    {
        private MainPageViewModel mainPageViewModel { get; set; }
        public SearchBoxControl()
        {
            InitializeComponent();
           
            Loaded += SearchBoxControl_Loaded;
        }

        private void SearchBoxControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainPageViewModel = (MainPageViewModel)DataContext;
            choosetb.DataContext = mainPageViewModel.mainWindowTextBoxViewModel;
            mainPageViewModel.mainWindowTextBoxViewModel.AddList();
            mainPageViewModel.mainWindowTextBoxViewModel.InHomeMap();
        }
    }
}
