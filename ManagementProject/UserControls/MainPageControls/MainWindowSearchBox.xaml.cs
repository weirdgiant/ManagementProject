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
    /// MainWindowSearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowSearchBox : UserControl
    {
        public MainWindowSearchBox()
        {
            InitializeComponent();
            searchcontent.TextChanged += Searchcontent_TextChanged;
            searchcontent.LostFocus += Searchcontent_LostFocus;
        }

        private void Searchcontent_TextChanged(object sender, TextChangedEventArgs e)
        {
            pop.IsOpen = true;
            pop.StaysOpen = true;
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
    }
    public class MainWindowSearchViewModel:MainWindowSearchModel
    {
        public MainWindowSearchViewModel()
        {

        }
    }
}
