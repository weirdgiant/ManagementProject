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
    /// PlayerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerPanel : UserControl
    {
        public PlayerPanel()
        {
            InitializeComponent();
            Loaded += PlayerPanel_Loaded;
        }

        private void PlayerPanel_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPlayGrid(3,4);
        }

        public void DrawPlayGrid(int columnNumber, int rowNumber)
        {
            playgrid.ColumnDefinitions.Clear();
            for (int i = 0; i < columnNumber; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);
                playgrid.ColumnDefinitions.Add(cd);

            }
            playgrid.RowDefinitions.Clear();
            for (int i = 0; i < rowNumber; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                playgrid.RowDefinitions.Add(rd);

            }

            playgrid.Children.Clear();

        }
    }
}
