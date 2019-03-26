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
    /// ClientInfoButton.xaml 的交互逻辑
    /// </summary>
    public partial class ClientInfoButton : UserControl
    {
        public ClientInfoViewModel clientInfoViewModel { get; set; }
        public ClientInfoButton()
        {
            InitializeComponent();
            clientInfoViewModel = new ClientInfoViewModel();
            DataContext = clientInfoViewModel;
        }
        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            pop.IsOpen = true;
        }
    }

    public class ClientInfoModel:INotifyPropertyChangedClass
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

        private ObservableCollection<ClientState> _clientList;
        public ObservableCollection<ClientState> ClientList
        {
            get
            {
                return _clientList;
            }
            set
            {
                _clientList = value;
                NotifyPropertyChanged("ClientList");
            }
        }
    }
    public class ClientInfoViewModel:ClientInfoModel
    {
        public DelegateCommand ShowClientInfoCommand { get; set; }
        public ClientInfoViewModel()
        {
            ShowClientInfoCommand = new DelegateCommand();
            ShowClientInfoCommand.ExecuteCommand = new Action<object>(ShowClientInfo);
        }
        private void ShowClientInfo(object obj)
        {
            ClientList = new ObservableCollection<ClientState>();
        }
    }
}
