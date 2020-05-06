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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls.AlarmControls
{
    /// <summary>
    /// FireEscape.xaml 的交互逻辑
    /// </summary>
    public partial class FireEscape : UserControl
    {
        public List<Player> Players = new List<Player>();
        private Element[] EscapeCamera { get; set; }
        private int TotalDataCount { get; set; }
        private int PageNum { get; set; } = 1;
        private int CurrentPage { get; set; } = 1;
        public FireEscape()
        {
            InitializeComponent();
        }

        public void GetEscapeCamera(string code)
        {
            EscapeCamera = HttpAPi.GetEscapeCamera(code);
            TotalDataCount = EscapeCamera.Length;
            PageNum = EscapeCamera.Length / 6+1;
        }

        public void InitPanel()
        {
            DrawPlayGrid(3, 2, panel);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        private void LastPage()
        {
            if (CurrentPage>1)
            {
                CurrentPage--;
            }
        }
        /// <summary>
        /// 下一页
        /// </summary>
        private void NextPage()
        {
            if (CurrentPage< PageNum)
            {
                CurrentPage++;
            }
        }

        private Element[] CurrentCameraList()
        {
            return EscapeCamera;
        }

        /// <summary>
        /// 2*3布局
        /// </summary>
        private void SetLayout()
        {
            int count = EscapeCamera.Length;
            if (count <= 3)
            {
                InitThreePlayer(EscapeCamera,panel);
            }
            else if(count <= 6)
            {
                InitPlayer(3,2, EscapeCamera,panel);
            }else
            {
                PageNum =1+ count / 6;
                InitPlayer(3, 2, EscapeCamera, panel);
            }
           
        }
        
        
        

        public void DrawPlayGrid(int columnNumber, int rowNumber, Grid grid)
        {
            grid.Children.Clear();

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

        public void InitPlayer(int columnNumber, int rowNumber, Element[] cameraList,Grid grid, PlayerWindowType type = PlayerWindowType.Module)
        {
            try
            {
                Players.Clear();
                int num = columnNumber * rowNumber;
                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {
                        var Player = new Player(type);
                        if (count < cameraList.Length)
                        {
                            Player.CameraCode = cameraList[count].code;

                        }
                        else
                        {
                           // Player.CameraCode = camera[0].code;
                        }
                        Player.StartLive();
                        //在 Grid 中动态添加控件
                        grid.Children.Add(Player);
                        //设定控件在 Grid 中的位置
                        Grid.SetRow(Player, j);
                        Grid.SetColumn(Player, i);
                        //将控件添加到集合中，方便下一步的使用
                        Players.Add(Player);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(PlayerPanel), ex.Message);
            }
        }

        public void InitThreePlayer(Element[] cameraList, Grid grid, PlayerWindowType type = PlayerWindowType.Module)
        {
            try
            {

                Players.Clear();
                var Player = new Player(type);
                Player.SetValue(Grid.RowSpanProperty, 2);
                Player.SetValue(Grid.ColumnSpanProperty, 2);
                grid.Children.Add(Player);
                Grid.SetRow(Player, 0);
                Grid.SetColumn(Player, 0);
                Player.CameraCode = cameraList[0].code;

                Player.StartLive();
                Players.Add(Player);
                int count = 0;
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 2; j++)
                    {

                        if (i == 0 && j == 0) continue;
                        if (i == 0 && j == 1) continue;
                        if (i == 1 && j == 0) continue;
                        if (i == 1 && j == 1) continue;
                        count++;
                        var player = new Player(type);

                        //在 Grid 中动态添加控件
                        grid.Children.Add(player);
                        //设定控件在 Grid 中的位置
                        Grid.SetRow(player, j);
                        Grid.SetColumn(player, i);
                        if (count < cameraList.Length)
                        {
                            player.CameraCode = cameraList[count].code;

                        }
                        else
                        {
                           // player.CameraCode = cameraList[0].code;

                        }
                        player.StartLive();
                        Players.Add(player);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}
