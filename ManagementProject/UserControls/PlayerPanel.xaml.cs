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
        private PlayerWindowType playerWindowType;
        public PlayerPanel(PlayerWindowType type)
        {
            playerWindowType = type;
            InitializeComponent();
            Loaded += PlayerPanel_Loaded;
        }

        private void PlayerPanel_Loaded(object sender, RoutedEventArgs e)
        {
            InitSixPlayerPanel(3,3,playgrid, playerWindowType);         
        }

        public static void InitPlayerPanel(int columnNumber, int rowNumber, Grid grid,PlayerWindowType type)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitPlayer(columnNumber, rowNumber, grid, type);

        }
        public static void InitSixPlayerPanel(int columnNumber, int rowNumber, Grid grid, PlayerWindowType type)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitSixPlayer(columnNumber, rowNumber, grid, type);

        }




        private static void DrawPlayGrid(int columnNumber, int rowNumber,Grid grid)
        {
            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < columnNumber; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);

                grid.ColumnDefinitions.Add(cd);

            }
            grid.RowDefinitions.Clear();
            for (int i = 0; i < rowNumber; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(rd);

            }

            grid.Children.Clear();

        }


        private static void InitPlayer(int columnNumber, int rowNumber, Grid grid,PlayerWindowType type)
        {
            var controls = new List<Player>();
            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    var Player = new Player(type);
                    //在 Grid 中动态添加控件
                    grid.Children.Add(Player);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(Player, j);
                    Grid.SetColumn(Player, i);
                    //将控件添加到集合中，方便下一步的使用
                    controls.Add(Player);
                }
            }
        }

        private static void InitSixPlayer(int columnNumber, int rowNumber, Grid grid, PlayerWindowType type)
        {
            var controls = new List<Player>();
            var Player = new Player(type);
            Player.SetValue(Grid.RowSpanProperty, 2);
            Player.SetValue(Grid.ColumnSpanProperty, 2);
            grid.Children.Add(Player);
            Grid.SetRow(Player, 0);
            Grid.SetColumn(Player, 0);
            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == 0 && j == 1) continue;
                    if (i == 1 && j == 0) continue;
                    if (i == 1 && j == 1) continue;
                    var player = new Player(type);
                    //在 Grid 中动态添加控件
                    grid.Children.Add(player);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(player, j);
                    Grid.SetColumn(player, i);
                    //将控件添加到集合中，方便下一步的使用
                    controls.Add(player);
                }
            }
        }

    }
}
