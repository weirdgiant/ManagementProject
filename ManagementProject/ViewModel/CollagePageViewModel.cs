using ManagementProject.Model;
using ManagementProject.PageView;
using ManagementProject.UserControls.FightControl;
using ManagementProject.UserControls.FightControl.CamResource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ManagementProject.ViewModel
{
    public class CollagePageViewModel : CollagePageModel
    {
        #region 属性     

        public MainWindowViewModel MainWindowViewModel { get; set; }
        /// <summary>
        /// 生成窗口
        /// </summary>
        public DelegateCommand CreatNewWinCommand { get; set; }
        /// <summary>
        /// 关闭平台窗口
        /// </summary>
        public DelegateCommand CloseAllWinCommand { get; set; }

        /// <summary>
        /// 新建场景
        /// </summary>
        public DelegateCommand NewScenesCommand { get; set; }

        /// <summary>
        /// 保存场景
        /// </summary>
        public DelegateCommand SaveScenesCommand { get; set; }

        /// <summary>
        /// 新建计划轮询
        /// </summary>
        public DelegateCommand NewPlanRotationCommand { get; set; }

        /// <summary>
        /// 重置窗口
        /// </summary>
        public DelegateCommand ResetWinCommand { get; set; }

        /// <summary>
        /// 机器中所有目录的列表
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> CameraItems { get; set; }

        #endregion

        #region 构造方法
        public CollagePageViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            InitCommand();
            InitCameraDirectory();
        }

        public CollagePageViewModel() { }
       
        #endregion

        #region 私有方法
        private void InitCommand()
        {
            CreatNewWinCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(CreatNewWin) };
            CloseAllWinCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(CloseAllWin) };
            NewScenesCommand = new DelegateCommand { ExecuteCommand = new Action<object>(NewScenes) };
            SaveScenesCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveScenes) };
            NewPlanRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(PlanRotation) };
            ResetWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(ResetWin) };
        }

        private void ResetWin(object obj)
        {
            Grid grid = (Grid)obj;
        }

        /// <summary>
        /// 初始化相机目录
        /// </summary>
        private void InitCameraDirectory()
        {
            //获取逻辑分区
            var children = DirectoryStructure.GetLogicalDrives();

            // Create the view models from the data
            CameraItems = new ObservableCollection<DirectoryItemViewModel>(
            children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));
        }

        private void PlanRotation(object obj)
        {
            PlanRotationWin planWin = new PlanRotationWin { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
            planWin.ShowDialog();
        }

        /// <summary>
        /// 保存场景
        /// </summary>
        /// <param name="obj"></param>
        private void SaveScenes(object obj)
        {
            SaveScenesWin scenesWin = new SaveScenesWin(obj) { WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen };
            scenesWin.ShowDialog();
        }

        /// <summary>
        /// 新建场景
        /// </summary>
        /// <param name="obj"></param>
        private void NewScenes(object obj)
        {
            #region UniformGrid
            //UniformGrid grid = (UniformGrid)obj;

            ////var rowCount = grid.RowDefinitions.Count;
            ////var columnCount = grid.ColumnDefinitions.Count;
            //var childrenCount = grid.Children.Count;

            //if (childrenCount > 0)
            //    grid.Children.Clear();

            //for (int i = 0; i < 9; i++)
            //{
            //    var playControl = new PlayControl(grid) { Margin = new Thickness(5) };
            //    //playControl.ThumbRightBottom.DragDelta += ThumbRightBottom_DragDelta;

            //    //playControl.GridDrag.PreviewMouseMove += PlayControl_PreviewMouseMove;
            //    //playControl.GridDrag.PreviewMouseLeftButtonDown += PlayControl_PreviewMouseLeftButtonDown;
            //    //playControl.GridDrag.PreviewMouseLeftButtonUp += PlayControl_PreviewMouseLeftButtonUp;
            //    grid.Children.Add(playControl);
            //}
            #endregion

            #region Grid Create
            Grid grid = (Grid)obj;

            var rowCount = grid.RowDefinitions.Count;
            var columnCount = grid.ColumnDefinitions.Count;

            var childrenCount = grid.Children.Count;

            if (childrenCount > 0)
                grid.Children.Clear();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var playControl = new PlayControl(grid) { Margin = new Thickness(5), };

                    //playControl.PreviewMouseMove += PlayControl_PreviewMouseMove;
                    //playControl.PreviewMouseLeftButtonDown += PlayControl_PreviewMouseLeftButtonDown;
                    //playControl.PreviewMouseLeftButtonUp += PlayControl_PreviewMouseLeftButtonUp;

                    grid.Children.Add(playControl);
                    Grid.SetColumn(playControl, j);
                    Grid.SetRow(playControl, i);
                }
            }
            #endregion
        }


        int i = 0;
        /// <summary>
        /// 生成窗口
        /// </summary>
        /// <param name="obj"></param>
        private void CreatNewWin(object obj)
        {
            Grid grid = (Grid)obj;

            var count = grid.Children.Count;
            var rowCount = grid.RowDefinitions.Count;
            var columnCount = grid.ColumnDefinitions.Count;

            //foreach (UIElement uiElement in grid.Children)
            //{
            //    if (uiElement.GetType() == typeof(UserControl))
            //    {
            //        if (uiElement != null)
            //        {
            //            // Do something with your control here
            //        }
            //    }
            //}

            //for (int i = 0; i < rowCount; i++)
            //{
            //    for (int j = 0; j < columnCount; j++)
            //    {
            //        //var playControl = new PlayControl() { Margin = new Thickness(5), Cursor = Cursors.Hand };
            //        //grid.Children.Add(playControl);
            //        //Grid.SetColumn(playControl, j);
            //        //Grid.SetRow(playControl, i);

            //    }
            //}
            //grid.Children.Add(new PlayControl());

            CheckCount(grid);

        }

        private void CheckCount(Grid grid)
        {
            var rowCount = grid.RowDefinitions.Count;
            var columnCount = grid.ColumnDefinitions.Count;
            var childrenCount = grid.Children.Count;

            PlayControl playControl = new PlayControl(grid) { Margin = new Thickness(5) };
            switch (childrenCount)
            {
                case 0:
                    Grid.SetColumn(playControl, 0);
                    Grid.SetRow(playControl, 0);
                    grid.Children.Add(playControl);
                    break;
                case 1:
                    Grid.SetColumn(playControl, 1);
                    Grid.SetRow(playControl, 0);
                    grid.Children.Add(playControl);
                    break;
                case 2:
                    Grid.SetColumn(playControl, 2);
                    Grid.SetRow(playControl, 0);
                    grid.Children.Add(playControl);
                    break;
                case 3:
                    Grid.SetColumn(playControl, 0);
                    Grid.SetRow(playControl, 1);
                    grid.Children.Add(playControl);
                    break;
                case 4:
                    Grid.SetColumn(playControl, 1);
                    Grid.SetRow(playControl, 1);
                    grid.Children.Add(playControl);
                    break;
                case 5:
                    Grid.SetColumn(playControl, 2);
                    Grid.SetRow(playControl, 1);
                    grid.Children.Add(playControl);
                    break;
                case 6:
                    Grid.SetColumn(playControl, 0);
                    Grid.SetRow(playControl, 2);
                    grid.Children.Add(playControl);
                    break;
                case 7:
                    Grid.SetColumn(playControl, 1);
                    Grid.SetRow(playControl, 2);
                    grid.Children.Add(playControl);
                    break;
                case 8:
                    Grid.SetColumn(playControl, 2);
                    Grid.SetRow(playControl, 2);
                    grid.Children.Add(playControl);
                    break;
                default:
                    break;
            }


            //for (int i = 0; i < rowCount; i++)
            //{
            //    for (int j = 0; j < columnCount; j++)
            //    {
            //        var playControl = new PlayControl();
            //        grid.Children.Add(playControl);
            //        Grid.SetColumn(playControl, j);
            //        Grid.SetRow(playControl, i);
            //    }
            //}

            Console.WriteLine(childrenCount.ToString());
        }

        /// <summary>
        /// 关闭平台窗口
        /// </summary>
        /// <param name="obj"></param>
        private void CloseAllWin(object obj)
        {
            Grid grid = (Grid)obj;

            //UniformGrid grid = (UniformGrid)obj;
            grid.Children.Clear();
        } 
        #endregion
    }
}
