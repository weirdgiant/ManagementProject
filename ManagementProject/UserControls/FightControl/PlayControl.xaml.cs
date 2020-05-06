using ManagementProject.Helper;
using ManagementProject.ViewModel;
using MangoApi;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// PlayControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayControl : UserControl
    {
        #region Property

        public Grid fightGrid;
        public WinParams winParams;
        public int winId;
        public FightPanel fightPanel;
        private Point mouseOffset = new Point();
        public CollagePageViewModel collagePageView;

        #endregion

        #region  Construction Method

        public PlayControl(CollagePageViewModel collagePage)
        {
            InitializeComponent();

            InitPanel(collagePage);
            DataContext = new PlayControlViewModel(this);
        }

        #endregion

        private void InitPanel(CollagePageViewModel collagePage)
        {
            collagePageView = collagePage;
            fightGrid = collagePage.FightGrid;

            fightPanel = new FightPanel();
            grid.Children.Add(fightPanel);
            Grid.SetRow(fightPanel, 1);

            GridDrag.PreviewMouseMove += PlayControl_PreviewMouseMove;
            GridDrag.PreviewMouseLeftButtonUp += PlayControl_PreviewMouseLeftButtonUp;
            GridDrag.PreviewMouseLeftButtonDown += PlayControl_PreviewMouseLeftButtonDown;

            Loaded += PlayControl_Loaded;
        }

        private void PlayControl_Loaded(object sender, RoutedEventArgs e)
        {
            MaxWidth = fightGrid.ActualWidth - Margin.Left * 2;
            MaxHeight = fightGrid.ActualHeight - Margin.Left * 2;

            var size = GetWinSize(ActualWidth, ActualHeight);

            winParams = new WinParams()
            {
                id = collagePageView.GetWinId(),
                w = size.Width,
                h = size.Height,
                z = Convert.ToInt32(GetValue(Panel.ZIndexProperty)),
                split = fightPanel.GridFight.Children.Count,
                cam = new Camera[] { }
            };

            GetWinPoint(fightGrid);
        }

        private void PlayControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var rec = (Grid)sender;
            rec.ReleaseMouseCapture();

            GetWinPoint(fightGrid);

            var result = PinkongHelper.SetWindow(winParams);

        }

        private void PlayControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rec = (Grid)sender;
            mouseOffset = Mouse.GetPosition(rec);
            rec.CaptureMouse();
        }

        private void PlayControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var rec = (Grid)sender;

            if (rec.IsMouseCaptured)
            {
                var mouseDelta = Mouse.GetPosition(rec);
                mouseDelta.Offset(-mouseOffset.X, -mouseOffset.Y);

                Margin = new Thickness(
                Margin.Left + mouseDelta.X,
                Margin.Top + mouseDelta.Y,
                Margin.Right - mouseDelta.X,
                Margin.Bottom - mouseDelta.Y);
            }
            FramEleHelper.SetControlToTop(fightGrid, this);
        }

        private PointW GetWinPoint(Grid panel)
        {
            try
            {
                var point = new PointW();
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
                {
                    var p = TransformToAncestor(panel).Transform(new Point(0, 0));
                    point = GetPoint(p.X, p.Y);
                    winParams.x = point.X;
                    winParams.y = point.Y;

                    if (!collagePageView.IsSwitchScene)
                    {
                        //PinkongHelper.CloseWindowAsync(winParams.id);
                        //PinkongHelper.SetWindowAsync(winParams);
                        PinkongHelper.SetWindow(winParams);
                    }
                    else
                    {
                        winParams.id = winId;
                    }
                    //TbId.Text = $"{point.X},{point.Y}";
                    //TbId.Text = winParams.id.ToString();

                }));
                return point;
            }
            catch
            {
                return new PointW(0, 0);
            }
        }

        private void OnResizeThumbDragStarted(object sender, DragStartedEventArgs e) => Cursor = Cursors.SizeNWSE;

        private void OnResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var size = GetWinSize(ActualWidth, ActualHeight);
            winParams.w = size.Width;
            winParams.h = size.Height;
            //TbId.Text = $"w:{winParams.w},h:{winParams.h}";

            var result = PinkongHelper.SetWindow(winParams);
            //Console.WriteLine(result);
        }

        private void OnResizeThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            FramEleHelper.SetControlToTop(fightGrid, this);

            //double yAdjust = Height + e.VerticalChange;
            //double xAdjust = Width + e.HorizontalChange;

            ////确保不要调整到负宽度或高度           
            //xAdjust = (ActualWidth + xAdjust) > MinWidth ? xAdjust : MinWidth;
            //yAdjust = (ActualHeight + yAdjust) > MinHeight ? yAdjust : MinHeight;

            //if (xAdjust < 0 || yAdjust < 0)
            //    return;

            if (ActualWidth < 270 || ActualHeight < 253.33333333333326)
            {
                Margin = new Thickness(5);
                //Width = 270;
                //Height = 253.33333333333326;
                return;
            }
            else
            {
                Margin = new Thickness(
                Margin.Left,
                Margin.Top,
                Margin.Right - e.HorizontalChange,
                Margin.Bottom - e.VerticalChange);
            }
        }

        /// <summary>
        /// 获取大屏上窗体的坐标
        /// </summary>
        /// <param name="x">PlayControl控件相对于FightGrid中的x值</param>
        /// <param name="y">PlayControl控件相对于FightGrid中的y值</param>
        /// <returns>大屏窗体的坐标</returns>
        public PointW GetPoint(double x, double y)
        {
            var m = 11520d * (x - 5) / 1680d;
            var n = 5760d * (y - 5) / 790d;//fightGrid.ActualWidth;
            return new PointW(Convert.ToInt32(m), Convert.ToInt32(n));
        }

        /// <summary>
        /// 获取大屏上窗体大小
        /// </summary>
        /// <param name="actualWidth">PC上窗体的实际宽度</param>
        /// <param name="actualHeight">PC上窗体的实际高度</param>
        /// <param name="minWidth">PC上窗体初始化宽度</param>
        /// <param name="minHeight">PC上窗体初始化实际高度</param>
        /// <param name="wallWinWidth">大屏上窗体的宽度</param>
        /// <param name="wallWinHeight">大屏上窗体的高度</param>
        /// <returns>窗体大小</returns>
        public Size GetWinSize(double actualWidth, double actualHeight, double minWidth = 270d, double minHeight = 253.33333333333326d, double wallWinWidth = 1920d, double wallWinHeight = 1920d)
        {
            var width = actualWidth * (wallWinWidth / minWidth);
            var height = actualHeight * (wallWinHeight / minHeight);
            return new Size((int)width, (int)height);
        }
    }

    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
    public class PointW
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointW(int x, int y)
        {
            X = x;
            Y = y;
        }
        public PointW() { }
    }
}
