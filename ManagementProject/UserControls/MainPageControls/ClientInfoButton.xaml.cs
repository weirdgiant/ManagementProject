using ManagementProject.Converters;
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
           // LostFocus += ClientInfoButton_LostFocus;
        }

        private void ClientInfoButton_LostFocus(object sender, RoutedEventArgs e)
        {
            pop.StaysOpen = false;
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

        private ObservableCollection<ClientInfo> _clientList;
        public ObservableCollection<ClientInfo> ClientList
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
            ClientList = new ObservableCollection<ClientInfo>();
            ClientConfig[] clientConfig= HttpAPi.GetAllClientInfo(AppConfig.ServerBaseUri + AppConfig.GetAllClientInfo);
            ClientConfig[] ret = clientConfig.Where(x => x.status == 0).ToArray();
            if (ret.Length !=0)
            {
                int index = 1;
                foreach (var item in ret)
                {
                    string time = "";
                    string date = "";
                    if (item.offlineTime != null)
                    {
                        DateTime dateTime =TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.offlineTime));
                        time = dateTime.ToString("HH:mm:ss");
                        date = dateTime.Date.ToString("yyyy-MM-dd");
                    }
                    ClientInfo info = new ClientInfo()
                    {
                        id = item.id,
                        ip = item.ipAddress,
                        name = item.clientName,
                        index = index,
                        outdate= date,
                        outtime = time,
                    };
                    index++;
                    ClientList.Add(info);
                }
            }
           //ClientList = new ObservableCollection<ClientConfig>(clientConfig);
        }
    }

    public class ClientInfo
    {
        public int id { get; set; }
        public int index { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public string outdate { get; set; }
        public string outtime { get; set; }
    }
}
