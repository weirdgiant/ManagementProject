using ManagementProject.UserControls;
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

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// AlarmDeviceWin.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmDeviceWin : Window
    {
        public AlarmDeviceWin()
        {
            InitializeComponent();
           
        }

    }
    public class AlarmDeviceWinViewModel : AlarmDeviceWinModel
    {
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand CloseWinCommand { get; set; }
        private AlarmPageViewModel alarmPageViewModel { get; set; }
        public Dictionary<int, RadioButton> AlarmTabButtonDic = new Dictionary<int, RadioButton>();
        public AlarmDeviceWinViewModel(AlarmPageViewModel _alarmPageViewModel)
        {
            alarmPageViewModel = _alarmPageViewModel;
            LoadedCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(WindowLoaded) };
            CloseWinCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(CloseWin) };
        }

        

        private void CloseWin(object obj)
        {
            if (obj == null)
                return;

            AlarmDeviceWin deviceWin = (AlarmDeviceWin)obj;
            deviceWin.Close();
        }

        private void WindowLoaded(object obj)
        {
            if (obj == null)
                return;

            WrapPanel wrapPanel = (WrapPanel)obj;
            RadioButton radioButton;
            int i = 0;
            foreach (var item in alarmPageViewModel.AlarmInfo)
            {
                if (i >= 4)
                {
                    radioButton = new RadioButton()
                    {
                        Height = 30,
                        Width = 131,
                        Content = item.sersorname ,
                        ToolTip = item.sersorname,
                        Tag =item,
                        TabIndex =i+1,
                        Margin = new Thickness(5),
                        Style = Application.Current.FindResource("AlarmDeviceRadioButtonStyle") as Style,
                    };

                    radioButton.Click += RadioButton_Click;
                    radioButton.SetBinding(RadioButton.CommandProperty, new Binding { Path = new PropertyPath("CloseWinCommand") });
                    radioButton.SetBinding(RadioButton.CommandParameterProperty, new Binding() { RelativeSource = new RelativeSource { AncestorType = typeof(Window), Mode = RelativeSourceMode.FindAncestor } });

                    wrapPanel.Children.Add(radioButton);
                    AlarmTabButtonDic.Add(item.id, radioButton);
                }
                i++;
            }
            if (AlarmTabButtonDic.ContainsKey (alarmPageViewModel.SelectedId))
            {
                RadioButton radio = AlarmTabButtonDic[alarmPageViewModel.SelectedId];
                radio.IsChecked = true;
            }

        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Alarm alarm = (Alarm)radio.Tag;
            //alarmPageViewModel.AlarmInfo.RemoveAt(radio.TabIndex);
            //alarmPageViewModel.AlarmInfo.Insert(0,alarm);           
            alarmPageViewModel.SelectedId = alarm.id;
            //alarmPageViewModel.LoadTabControl();
            //alarmPageViewModel.disposalPlanViewModel.AlarmID = alarm.id;
            //alarmPageViewModel.disposalPlanViewModel.alarmPageViewModel = alarmPageViewModel;
        }
    }
    public class AlarmDeviceWinModel : INotifyPropertyChangedClass
    {

    }
}
