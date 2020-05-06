using ManagementProject.FunctionalWindows;
using ManagementProject.ViewModel;
using MangoApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// TabControlWin.xaml 的交互逻辑
    /// </summary>
    public partial class TabControlWin : Window
    {
       
        private AlarmPageViewModel alarmPageViewModel { get; set; }
        private AlarmTabViewModel alarmTabViewModel { get; set; }
        public ObservableCollection<Alarm> AlarmInfo { get; set; }
        private Dictionary<int, RadioButton> AlarmTabButtonDic = new Dictionary<int, RadioButton>();
        private Dictionary<int, RadioButton> ExpanderTabDic = new Dictionary<int, RadioButton>();
        public TabControlWin()
        {
            InitializeComponent();
            Loaded += TabControlWin_Loaded;
            Unloaded += TabControlWin_Unloaded;
        }

        private void TabControlWin_Unloaded(object sender, RoutedEventArgs e)
        {
            alarmPageViewModel.SelectedId = 0;
        }

        private void TabControlWin_Loaded(object sender, RoutedEventArgs e)
        {
            ReLoaded();
            alarmPageViewModel.SelectedId = alarmPageViewModel.SelectedId;
        }


        public void ReLoaded()
        {
            control.panel.Children.Clear();
            AlarmTabButtonDic.Clear();
            alarmPageViewModel = (AlarmPageViewModel)DataContext;
            alarmTabViewModel = new AlarmTabViewModel();
            alarmTabViewModel.AlarmInfo = alarmPageViewModel.AlarmInfo;
            control.DataContext = alarmTabViewModel;
           
           
            if (alarmPageViewModel.PageType =="CAR")
            {
                InitCarTabControl();
            }else
            {
                InitControl(alarmTabViewModel);
            }
            if (alarmPageViewModel.SelectedId != 0)
            {
                if (AlarmTabButtonDic.ContainsKey(alarmPageViewModel.SelectedId))
                {
                    RadioButton radio = AlarmTabButtonDic[alarmPageViewModel.SelectedId];
                    radio.IsChecked = true;
                }

            }
            else
            {
                if (AlarmTabButtonDic.Count != 0)
                {
                    RadioButton radio = AlarmTabButtonDic[AlarmTabButtonDic.First().Key];
                    alarmPageViewModel.SelectedId = AlarmTabButtonDic.First().Key;
                    radio.IsChecked = true;
                }else
                {
                    alarmPageViewModel.ReturnMainPage();
                }
            }

        }


        private void InitCarTabControl()
        {
            Alarm[] alarm = alarmPageViewModel.AlarmList.Where(x=>x.type==1).ToArray();
            GetDeviceList(alarm);
        }



        private void GetDeviceList(Alarm[] alarms)
        {
            var qry = from s in alarms
                      group s by s.sersor into ws
                      orderby ws.Count() descending
                      select new
                      {
                          Num = ws.Count(),
                          Code = ws.Key,
                          alarmList = ws.ToArray(),

                      };
            foreach (var item in qry)
            {
                RadioButton radioButton = CarTabButton(item.alarmList[0], item.Num.ToString ());
                radioButton.Click += Car_Click;
                control.panel.Children.Add(radioButton);
                AlarmTabButtonDic.Add(item.alarmList[0].id, radioButton);
            }
        }


        private void Car_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Alarm alarm = (Alarm)radio.DataContext;
            alarmPageViewModel.SelectedId = alarm.id;

        }




        private void InitControl(AlarmTabViewModel _alarmTabViewModel )
        {
            int i = 0;
            foreach (var item in _alarmTabViewModel.AlarmInfo)
            {
                if (i <4)
                {
                    RadioButton radioButton = TabButton(item);
                    radioButton.Click += RadioButton_Click;
                    control.panel.Children.Add(radioButton);
                    AlarmTabButtonDic.Add(item.id, radioButton);
                    i++;
                }else
                {
                    RadioButton tabExpander = ExpanderButton();
                    tabExpander.DataContext = _alarmTabViewModel;
                    tabExpander.Click += TabExpander_Click;
                    control.panel.Children.Add(tabExpander);
                    break;
                }
            }
        }

        private void TabExpander_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            AlarmDeviceWin win = new AlarmDeviceWin();
            AlarmDeviceWinViewModel viewModel = new AlarmDeviceWinViewModel(alarmPageViewModel);
            win.DataContext = viewModel;
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.Topmost = true;
            win.ShowDialog();
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            Alarm alarm = (Alarm)radio.DataContext;
            alarmPageViewModel.SelectedId = alarm.id;

        }

        private RadioButton TabButton(Alarm alarm)
        {
            Label label = new Label()
            {
                Width = 95,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = alarm.sersorname,
            };

            RadioButton radioButton = new RadioButton()
            {
                Width =130,
                Style = (Style)FindResource("AlarmRadioButtonStyle1"),
                ToolTip = alarm.sersorname,
                DataContext = alarm,
                Content =label,
            };

            return radioButton;
        }



        private RadioButton CarTabButton(Alarm alarm,string num)
        {
            Label label = new Label()
            {
                Width = 95,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = alarm.sersorname,
                Margin = new Thickness(0, 0, 35, 0),
            };


            Label count = new Label()
            {
                Width = 30,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Content = num, Margin = new Thickness (90,0,0,0),
            };

            Grid grid = new Grid();
            grid.Children.Add(label);
            grid.Children.Add(count);

            RadioButton radioButton = new RadioButton()
            {
                Width = 130,
                Style = (Style)FindResource("AlarmRadioButtonStyle1"),
                ToolTip = alarm.sersorname,
                DataContext = alarm,
                Content = grid,
            };

            return radioButton;
        }


        private RadioButton ExpanderButton()
        {
            Style btn_style = (Style)FindResource("AlarmRadio");
            RadioButton radioButton = new RadioButton()
            {
                Width = 80,
                Height = 30,
                VerticalAlignment=VerticalAlignment.Center,
                HorizontalAlignment=HorizontalAlignment.Center,
                Style = btn_style
            };

            TextBlock textBlock = new TextBlock
            {
                Text = "\xE10C",
                FontSize = 20,
                Foreground = Brushes.White,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Style= (Style)FindResource("SegoeFontStyle"),
            };

            radioButton.Content = textBlock;
            radioButton.ToolTip = "查看更多";
            return radioButton;
        }
    }
}
