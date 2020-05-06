using MangoApi;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// FightPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class FightPanel : UserControl
    {
        //private List<Player> players = new List<Player>();

        public FightPanel()
        {
            InitializeComponent();

            DrawPlayGrid(1, GridFight);
            Unloaded += FightPanel_Unloaded;
        }

        public static void InitPlayerPanel(int columnNumber, int rowNumber, Grid grid)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitPlayer(columnNumber, rowNumber, grid);
        }

        public static void InitSixPlayerPanel(int columnNumber, int rowNumber, Grid grid)
        {
            InitPlayGrid(columnNumber, rowNumber, grid);
            InitSixPlayer(columnNumber, rowNumber, grid);
        }

        public static void DrawPlayGrid(int columnNumber, int rowNumber, Grid grid)
        {
            InitPlayGrid(columnNumber, rowNumber, grid);
            InitPlayer(columnNumber, rowNumber, grid);
        }

        public static void DrawPlayGrid(int count, Grid grid)
        {
            switch (count)
            {
                case 1:
                    DrawPlayGrid(1, 1, grid);
                    break;
                case 4:
                    DrawPlayGrid(2, 2, grid);
                    break;
                case 6:
                    InitSixPlayerPanel(3, 3, grid);
                    break;
                case 9:
                    DrawPlayGrid(3, 3, grid);
                    break;
                case 16:
                    DrawPlayGrid(4, 4, grid);
                    break;
                default:
                    break;
            }
        }

        private static void InitPlayGrid(int columnNumber, int rowNumber, Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < columnNumber; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }
            for (int i = 0; i < rowNumber; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }
        }

        public static void InitPlayer(int columnNumber, int rowNumber, Grid grid)
        {
            //var controls = new List<Player>();
            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    var Player = new Player();
                    //在 Grid 中动态添加控件
                    grid.Children.Add(Player);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(Player, j);
                    Grid.SetColumn(Player, i);
                    //将控件添加到集合中，方便下一步的使用
                    //controls.Add(Player);
                }
            }
        }

        public static void InitSixPlayer(int columnNumber, int rowNumber, Grid grid)
        {
            //var controls = new List<Player>();
            var player_ = new Player();
            player_.SetValue(Grid.RowSpanProperty, 2);
            player_.SetValue(Grid.ColumnSpanProperty, 2);
            grid.Children.Add(player_);

            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == 0 && j == 1) continue;
                    if (i == 1 && j == 0) continue;
                    if (i == 1 && j == 1) continue;
                    var player = new Player();
                    //在 Grid 中动态添加控件
                    grid.Children.Add(player);
                    //设定控件在 Grid 中的位置
                    Grid.SetRow(player, j);
                    Grid.SetColumn(player, i);
                    //将控件添加到集合中，方便下一步的使用
                    //controls.Add(player);
                }
            }
        }

        private void FightPanel_Unloaded(object sender, RoutedEventArgs e) => StopPlayer();

        public void StopPlayer()
        {
            //players.Clear();
            GridFight.Children.Clear();
            ((Panel)Parent).Children.Remove(this);
        }
    }    
}
