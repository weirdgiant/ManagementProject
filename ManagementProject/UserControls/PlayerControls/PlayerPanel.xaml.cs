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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// PlayerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerPanel : UserControl,IDisposable 
    {
        public MapControl mapControl { get; set; }
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public MainPageViewModel mainPageViewModel { get; set; }
        public MainWindow _mainWin;

        public List<Player> Players = new List<Player>();
        private PlayerWindowType playerWindowType;
        public PlayerPanel(PlayerWindowType type)
        {         
            playerWindowType = type;
            InitializeComponent();
            Loaded += PlayerPanel_Loaded;
        }

        public PlayerPanel()
        {
            _mainWin = (MainWindow)Application.Current.MainWindow;
            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            mainPageViewModel = mainWindowViewModel.mainPageViewModel;

            InitializeComponent();

            DrawPlayGrid(1, 1, playgrid);
            Unloaded += PlayerPanel_Unloaded;
            //InitPlayer(1, 1, playgrid);
            //DrawPlayGrid(2,2, playgrid);
            //InitSixPlayerPanel(3, 3, playgrid, playerWindowType);
        }

        private void PlayerPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            StopPlayer();
        }

        public void StopPlayer()
        {
            foreach (var item in Players)
            {
                item.StopPlay();
            }
            Players = null;
            playgrid.Children.Clear();
        }

        public void InitPlayVod(string code)
        {
            InitPlayer(1, 1, code, playgrid);
        }

        private void PlayerPanel_Loaded(object sender, RoutedEventArgs e)
        {
            //DrawPlayGrid(1, 1, playgrid);
            //InitPlayer(1, 1, playgrid);
        }

        public void InitPlayerPanel(int columnNumber, int rowNumber, string code, Grid grid, PlayerWindowType type, string localAttrs = null)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitPlayer(columnNumber, rowNumber,code, grid, type, localAttrs);

        }

        public  void InitSixPlayerPanel(int columnNumber, int rowNumber, string code, Grid grid, ref List<string> codeList, PlayerWindowType type, string localAttrs = null)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitSixPlayer(columnNumber, rowNumber, code,grid,ref codeList, type, localAttrs);

        }
        public void InitAlarmPlayerPanel(int columnNumber, int rowNumber, string code, Grid grid, ref List<Player>list)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitAlarmPlayer(columnNumber, rowNumber, code, grid,ref list);
        }

        public void InitElevatorPlayer(int columnNumber, int rowNumber, int mapid, Grid grid, PlayerWindowType type)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitPlayer(columnNumber, rowNumber, mapid, grid, type);
        }

        public void InitEscapePlayer(int columnNumber, int rowNumber, string code, Grid grid)
        {
            DrawPlayGrid(columnNumber, rowNumber, grid);
            InitEscapeCamera(columnNumber, rowNumber, grid, code);
        }

        public void Clear()
        {
            if (GlobalVariable.IsAlarmPage == false)
            {
                mainPageViewModel.IsSelected = !mainPageViewModel.IsSelected;
            }
        }

        public  void DrawPlayGrid(int columnNumber, int rowNumber,Grid grid)
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

        public void InitEscapeCamera(int columnNumber, int rowNumber, Grid grid,string code, PlayerWindowType type = PlayerWindowType.Module)
        {
            try
            {
                int num = columnNumber * rowNumber;
                Element[] camera = HttpAPi.GetEscapeCamera(code);
                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {
                        var Player = new Player(type);
                        if (count < camera.Length)
                        {
                            Player.CameraCode = camera[count].code;

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
                        //mainPageViewModel.SelectedDeviceCode = Player.CameraCode;
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


        /// <summary>
        /// 获取电梯摄像机
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <param name="rowNumber"></param>
        /// <param name="mapid"></param>
        /// <param name="grid"></param>
        /// <param name="type"></param>
        public void InitPlayer(int columnNumber, int rowNumber, int mapid, Grid grid, PlayerWindowType type = PlayerWindowType.Module)
        {
            try
            {
                int num = columnNumber * rowNumber;
                ElementDevice[] camera = HttpAPi.GetElevatorModule(mapid);
                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {
                        var Player = new Player(type);
                        if (count < camera.Length)
                        {
                            Player.CameraCode = camera[count].code;

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
                        //mainPageViewModel.SelectedDeviceCode = Player.CameraCode;
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

        public void InitAlarmPlayer(int columnNumber, int rowNumber, string cameraCode, Grid grid, ref List<Player > players ,PlayerWindowType type = PlayerWindowType.Module)
        {
            try
            {
                Players.Clear();
                int num = columnNumber * rowNumber;
                Device[] cameralist = HttpAPi.GetCamera(cameraCode, num);
                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {
                        var Player = new Player(type);
                        if (count < cameralist.Length)
                        {
                            Player.CameraCode = cameralist[count].code;

                        }
                        else
                        {
                           // Player.CameraCode = cameralist[0].code;
                        }
                        Player.StartLive();
                        //在 Grid 中动态添加控件
                        grid.Children.Add(Player);
                        //设定控件在 Grid 中的位置
                        Grid.SetRow(Player, j);
                        Grid.SetColumn(Player, i);
                        if (GlobalVariable.IsAlarmPage == false)
                        {
                            mainPageViewModel.SelectedElementCode = Player.CameraCode;
                        }
                        Players.Add(Player);
                        count++;
                    }
                }
                players = Players;
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(PlayerPanel), ex.Message);
            }
        }

        public  void InitPlayer(int columnNumber, int rowNumber,string cameraCode, Grid grid,PlayerWindowType type=PlayerWindowType.Normal, string localAttrs = null)
        {
            try
            {
                Players.Clear();
                int num = columnNumber * rowNumber;
                Device[] cameralist = HttpAPi.GetCamera(cameraCode, num, localAttrs);
                if (GlobalVariable.IsAlarmPage == false)
                {
                    mainPageViewModel.IsSelected = !mainPageViewModel.IsSelected;
                    foreach (var item in cameralist)
                    {
                        mainPageViewModel.SelectedElementCode = item.code;
                    }

                }
                else
                {
                    mapControl.UnSelectedAll();
                    foreach (var item in cameralist)
                    {
                       
                        mapControl.FlickerOneByCode(item.code);
                    }

                }
                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {
                        var Player = new Player(type);
                        Player.mapControl = mapControl;
                        if (count < cameralist.Length)
                        {
                            Player.CameraCode = cameralist[count].code;
                        }
                        else
                        {
                           // Player.CameraCode = cameralist[0].code;
                        }
                        Player.StartLive();
                        //在 Grid 中动态添加控件
                        grid.Children.Add(Player);
                        //设定控件在 Grid 中的位置
                        Grid.SetRow(Player, j);
                        Grid.SetColumn(Player, i);
                                     
                        Players.Add(Player);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(PlayerPanel),ex.Message);
            }
        }

        public void InitSixPlayer(int columnNumber, int rowNumber, string cameraCode, Grid grid, ref List<string> CodeList, PlayerWindowType type = PlayerWindowType.Playerback, string localAttrs = null)
        {
            try
            {

                Players.Clear();
                //int num = columnNumber * rowNumber;
                Device[] cameralist = HttpAPi.GetCamera(cameraCode, 6, localAttrs);                
                if (cameralist.Length == 0)
                {
                    return;
                }
                if (GlobalVariable.IsAlarmPage == false)
                {
                    mainPageViewModel.IsSelected = !mainPageViewModel.IsSelected;
                    Task.Delay(500);
                    foreach (var item in cameralist)
                    {
                        mainPageViewModel.SelectedElementCode = item.code;
                    }

                }
                else
                {
                    mapControl.UnSelectedAll();
                    foreach (var item in cameralist)
                    {
                       
                        mapControl.FlickerOneByCode(item.code);
                    }
                   
                }
                Device[] selectedCode = cameralist.Where(x => x.code == cameraCode).ToArray();
                var Player = new Player(type);
                Player.mapControl = mapControl;
                Player.SetValue(Grid.RowSpanProperty, 2);
                Player.SetValue(Grid.ColumnSpanProperty, 2);
                grid.Children.Add(Player);
                Grid.SetRow(Player, 0);
                Grid.SetColumn(Player, 0);
                if (selectedCode.Length != 0)
                {
                    Player.CameraCode = cameraCode;
                }
                else
                {
                    Player.CameraCode = cameralist[0].code;
                }

                Player.StartLive();
                Players.Add(Player);
                CodeList.Add(Player.CameraCode);

                int count = 0;
                for (var i = 0; i < columnNumber; i++)
                {
                    for (var j = 0; j < rowNumber; j++)
                    {

                        if (i == 0 && j == 0) continue;
                        if (i == 0 && j == 1) continue;
                        if (i == 1 && j == 0) continue;
                        if (i == 1 && j == 1) continue;
                        count++;
                        var player = new Player(type);
                        player.mapControl = mapControl;
                        //在 Grid 中动态添加控件
                        grid.Children.Add(player);
                        //设定控件在 Grid 中的位置
                        Grid.SetRow(player, j);
                        Grid.SetColumn(player, i);
                        if (count < cameralist.Length)
                        {
                            player.CameraCode = cameralist[count].code;

                        }
                        else
                        {
                           // player.CameraCode = cameralist[0].code;

                        }
                        player.StartLive();
                        

                        Players.Add(player);
                        CodeList.Add(player.CameraCode);

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void InitSevenPlayer(int columnNumber, int rowNumber, Grid grid, PlayerWindowType type=PlayerWindowType.Playerback)
        {
            Players.Clear();
            var controls = new List<Player>();
            var Player = new Player(type);
            Player.SetValue(Grid.RowSpanProperty, 2);
            Player.SetValue(Grid.ColumnSpanProperty, 3);
            grid.Children.Add(Player);
            Grid.SetRow(Player, 0);
            Grid.SetColumn(Player, 0);
            for (var i = 0; i < columnNumber; i++)
            {
                for (var j = 0; j < rowNumber; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (i == 1 && j == 0) continue;
                    if (i == 2 && j == 0) continue;
                    if (i == 0 && j == 1) continue;
                    if (i == 1 && j == 1) continue;
                    if (i == 2 && j == 1) continue;
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

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                 
                    Players = null;
                    StopPlayer();
                }
               
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~PlayerPanel() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class PlayerPanelModel:INotifyPropertyChangedClass
    {
        private int rowNumber;
        public int RowNumber
        {
            get
            {
                return rowNumber;
            }
            set
            {
                rowNumber = value;
                NotifyPropertyChanged("RowNumber");
            }
        }

        private int columnNumber;
        public int ColumnNumber
        {
            get
            {
                return columnNumber;
            }
            set
            {
                columnNumber = value;
                NotifyPropertyChanged("ColumnNumber");
            }
        }
    }
    public class PlayerPanelViewModel:PlayerPanelModel
    {
        public PlayerPanelViewModel(int rowCount,int columnCount)
        {
            RowNumber = rowCount;
            ColumnNumber = columnCount;
        }
    }
}
