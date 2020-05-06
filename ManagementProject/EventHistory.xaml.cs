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

namespace ManagementProject
{
    /// <summary>
    /// EventHistory.xaml 的交互逻辑
    /// </summary>
    public partial class EventHistory : Window
    {
        private MainWindow main = (MainWindow)Application.Current.MainWindow;
        private MainWindowViewModel MainWindowViewModel { get; set; }
        public EventHistoryViewModel EventHistoryViewModel { get; set; }
        public EventHistory()
        {
            InitializeComponent();

            MainWindowViewModel = (MainWindowViewModel)main.DataContext;
            EventHistoryViewModel = new EventHistoryViewModel();
            DataContext = EventHistoryViewModel;
            start.textBlock1.TextChanged += TextBlock1_TextChanged;
            end.textBlock1.TextChanged += TextBlock1_TextChanged1;
            Closing += EventHistory_Closing;
        }

        private void EventHistory_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowViewModel.mainWindowMenuViewModel.SetSelection(MainWindowViewModel.mainWindowMenuViewModel.SelectionIndex);
        }

        private void TextBlock1_TextChanged1(object sender, TextChangedEventArgs e)
        {
            EventHistoryViewModel.EventEndTime = end.textBlock1.Text;
        }

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            EventHistoryViewModel.EventStartTime = start.textBlock1.Text;
        }
    }
}
