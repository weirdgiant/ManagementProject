using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// ParameterWin.xaml 的交互逻辑
    /// </summary>
    public partial class ParameterWin : Window
    {
        public MapControl mapControl { get; set; }
        public Parameter[] List { get; set; }
        public ParameterWin()
        {
            InitializeComponent();
            Loaded += ParameterWin_Loaded;
            Unloaded += ParameterWin_Unloaded;
        }

        private void ParameterWin_Loaded(object sender, RoutedEventArgs e)
        {
            LoadControl(List , mapControl);
        }

        private void ParameterWin_Unloaded(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
        }

        public void LoadControl(Parameter[] list, MapControl map)
        {
            //mapControl = map;
            panel.Children.Clear();
            foreach (var item in list)
            {
                if (item==null)
                {
                    continue;
                }
                ToggleButton toggle = NewButton(item);
                toggle.Click += Toggle_Click;
                panel.Children.Add(toggle);
                Grid grid = new Grid
                {
                    Height = 10
                };
                panel.Children.Add(grid);
            }
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = (ToggleButton)sender;
            string parameter = toggle.DataContext.ToString();
            if (toggle.IsChecked == true)
            {

                mapControl.ParameterList.Add(parameter);
                List<string> codeList = new List<string>();
                foreach (var child in mapControl.ParameterList)
                {
                    Element[] elements = HttpAPi.GetCameraByParameter(mapControl.CurrentMapId, child);
                   
                    foreach (var item in elements)
                    {
                        codeList.Add(item.code);
                    }
                }
               
                if (GlobalVariable.IsAlarmPage)
                {
                    mapControl.view.SetCodeList(codeList); ;
                    mapControl.view.Refresh();
                }
                else
                {
                    mapControl.CodeVisibleList = codeList;
                }

            }
            else
            {
                mapControl.ParameterList.Remove(parameter);
                List<string> codeList = new List<string>();
                foreach (var child in mapControl.ParameterList)
                {
                    Element[] elements = HttpAPi.GetCameraByParameter(mapControl.CurrentMapId, child);

                    foreach (var item in elements)
                    {
                        codeList.Add(item.code);
                    }
                }
                if (GlobalVariable.IsAlarmPage)
                {
                    mapControl.view.SetCodeList(codeList);
                    mapControl.view.Refresh();
                }
                else
                {
                    mapControl.CodeVisibleList = codeList;
                }


            }
        }

        private ToggleButton NewButton(Parameter active)
        {
            //Style btn_style = (Style)FindResource("TrackeTab");
            Style btn_style = (Style)FindResource("myAttrToggleButton");
            ToggleButton bt = new ToggleButton()
            {
                Width = 128,
                Height = 30,
                Style = btn_style,
                BorderBrush = null,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White")),
                // Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF363636"))
            };
            bt.Content = active.listName;
            bt.ToolTip = active.listName;
            bt.DataContext = active.listValue;
            return bt;
        }
    }
}
