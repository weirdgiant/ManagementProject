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
    /// SchoolMessage.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolMessage : Window 
    {
        public SchoolMessage()
        {
            InitializeComponent();
        }

        private void closebt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }

    public class SchoolMesModel:INotifyPropertyChangedClass
    {

    }

    public class SchoolMesViewModel:SchoolMesModel
    {
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MoveWinCommand { get; set; }
        public SchoolMesViewModel()
        {
            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MoveWinCommand = new DelegateCommand();
            MoveWinCommand.ExecuteCommand = new Action<object>(MoveWin);
        }
        private void CloseWin (object obj)
        {

        }
        private void MoveWin(object obj)
        {

        }
    }
}
