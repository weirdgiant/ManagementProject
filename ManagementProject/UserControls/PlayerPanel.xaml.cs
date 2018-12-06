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
            DrawPlayGrid(4,3);
            InitBitkyPoleShow(4,3);
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


        private void InitBitkyPoleShow(int columnNumber, int rowNumber)
        {
            var controls = new List<Player>();
            var id = 0;
            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    var Player = new Player();
                    //在 Grid 中动态添加控件
                    playgrid.Children.Add(Player);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(Player, j);
                    Grid.SetColumn(Player, i);
                    //将控件添加到集合中，方便下一步的使用
                    controls.Add(Player);
                    //对控件使用自定义方法进行初始化
                   // Player.setContent(id);
                   // id++;
                }
            }
        }

    }
}
