using ManagementProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Thumb = System.Windows.Controls.Primitives.Thumb;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// PlayControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayControl : UserControl
    {

        private PlayerPanel playerPanel;

        /// <summary>
        /// 拼控实体
        /// </summary>
        private Grid uniformGrid;

        /// <summary>
        /// 拼控的实际高度
        /// </summary>
        private double fightHeight;

        /// <summary>
        /// 拼控的实际宽度
        /// </summary>
        private double fightWidth;

        public PlayControl(Grid grid)
        {
            InitializeComponent();
            uniformGrid = grid;
            InitPanel();
            DataContext = new PlayControlViewModel(playerPanel,grid);
        }

        private void InitPanel()
        {
            fightHeight = uniformGrid.ActualHeight;
            fightWidth = uniformGrid.ActualWidth;

            playerPanel = new PlayerPanel();
            grid.Children.Add(playerPanel);
            Grid.SetRow(playerPanel, 1);

            GridDrag.PreviewMouseMove += PlayControl_PreviewMouseMove;
            GridDrag.PreviewMouseLeftButtonDown += PlayControl_PreviewMouseLeftButtonDown;
            GridDrag.PreviewMouseLeftButtonUp += PlayControl_PreviewMouseLeftButtonUp;
        }


        private void PlayControl_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Grid rec = (Grid)sender;
            //rec.SetValue(Grid.ZIndexProperty, 0);
            //Grid.SetZIndex(rec, 0);
            rec.ReleaseMouseCapture();
        }

        private void PlayControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //zIndex++;
            Grid rec = (Grid)sender;

            //rec.SetValue(Grid.ZIndexProperty, 1);
            //UniformGrid.SetZIndex(uniformGrid, zIndex);
            //Grid.SetZIndex(this, zIndex);

            mouseOffset = Mouse.GetPosition(rec);
            rec.CaptureMouse();
        }

        Point mouseOffset = new Point();

        //int zIndex = 0;

        private void PlayControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //zIndex++;
            Grid rec = (Grid)sender;

            if (rec.IsMouseCaptured)
            {
                Point mouseDelta = Mouse.GetPosition(rec);
                mouseDelta.Offset(-mouseOffset.X, -mouseOffset.Y);

                var x = mouseDelta.X;
                var y = mouseDelta.Y;

                //Console.WriteLine(x + "," + y); 

                Margin = new Thickness(
                Margin.Left + mouseDelta.X,
                Margin.Top + mouseDelta.Y,
                Margin.Right - mouseDelta.X,
                Margin.Bottom - mouseDelta.Y);

                //rec.Margin = new Thickness(
                //rec.Margin.Left + mouseDelta.X,
                //rec.Margin.Top + mouseDelta.Y,
                //rec.Margin.Right - mouseDelta.X,
                //rec.Margin.Bottom - mouseDelta.Y);
            }

            SetControlToTop(uniformGrid,this);
        }

        /// <summary>
        /// Set the current control to the top
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="userControl">父控件</param>
        private void SetControlToTop(Panel panel, Control userControl)
        {
            //Button bt = ui as Button;
            //Canvas parent = bt.Parent as Canvas;

            if (panel != null)
            {
                IEnumerable<UIElement> uiE = panel.Children.OfType<UIElement>().Where(x => x != userControl);//枚举类型定义

                if (uiE.Count() > 0)//判断 除去用户选择的控件，是否还有其他控件。
                {
                    var maxZ = uiE.Select(x => Panel.GetZIndex(x)).Max();
                    Grid.SetZIndex(userControl, maxZ + 1);//置于最顶层
                }
            }
        }

        /// <summary>
        /// Set the current control to the bottom
        /// </summary>
        private void SetControlToBottom(Panel panel)
        {
            //Button bt = ui as Button;

            //if (uni.GetZIndex() == 0)
            //{
            //    Panel.SetZIndex(bt, bt.GetZIndex());
            //}

            //Panel.SetZIndex(bt, 0);//置于最底层
        }

        #region 自定义事件
        ///// <summary>
        ///// 光标离边框的距离
        ///// </summary>
        //private const double DISTANCE = 4d;

        ///// <summary>
        ///// 边框的实际宽度
        ///// </summary>
        //private double width = 0d;

        ///// <summary>
        ///// 边框的实际高度
        ///// </summary>
        //private double height = 0d;

        //private Point ChangeCursor(object sender, MouseEventArgs e)
        //{
        //    width = BorderTitle.ActualWidth;
        //    height = BorderTitle.ActualHeight;

        //    Point point = e.GetPosition((IInputElement)sender);

        //    var x = point.X;
        //    var y = point.Y;

        //    //MyTb.Text = $"x:{point.X} y:{point.Y}";

        //    if (x < DISTANCE || width - x < DISTANCE)
        //    {
        //        BorderTitle.Cursor = Cursors.SizeWE;
        //    }

        //    if (y < DISTANCE || height - y < DISTANCE)
        //    {
        //        BorderTitle.Cursor = Cursors.SizeNS;
        //    }

        //    if (x < DISTANCE && y < DISTANCE || width - x < DISTANCE && height - y < DISTANCE)
        //    {
        //        BorderTitle.Cursor = Cursors.SizeNWSE;
        //    }

        //    if (width - x < DISTANCE && y < DISTANCE || height - y < DISTANCE && x < DISTANCE)
        //    {
        //        BorderTitle.Cursor = Cursors.SizeNESW;
        //    }
        //    return point;
        //}

        //double d = 0;
        //private void BorderTitle_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var point = ChangeCursor(sender, e);

        //    var x = point.X;
        //    var y = point.Y;

        //    if (Mouse.LeftButton == MouseButtonState.Pressed)
        //    {
        //        if (width - x < DISTANCE)
        //        {
        //            d++;
        //            //Width += DISTANCE;
        //            //Width += DISTANCE;
        //            Margin = new Thickness(0, 0, -d, 0);
        //        }

        //        if (height - y < DISTANCE)
        //        {
        //            //Height += DISTANCE;
        //            Margin = new Thickness(0, 0, 0, -DISTANCE);
        //        }
        //    }
        //}

        //private void BorderTitle_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    ChangeCursor(sender, e);
        //}
        #endregion

        private Cursor _cursor;

        private void OnResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;

            _cursor = Cursor;
            if (thumb.HorizontalAlignment == HorizontalAlignment.Right)
                Cursor = Cursors.SizeNWSE;
            else
                Cursor = Cursors.SizeNESW;
        }

        private void OnResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            Cursor = _cursor;
        }

        private void OnResizeThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;

            SetControlToTop(uniformGrid, this);

            double yAdjust = Height + e.VerticalChange;
            double xAdjust = Width + e.HorizontalChange;

            if (thumb.HorizontalAlignment==HorizontalAlignment.Right)
            {
                //确保不要调整到负宽度或高度           
                xAdjust = (ActualWidth + xAdjust) > MinWidth ? xAdjust : MinWidth;
                yAdjust = (ActualHeight + yAdjust) > MinHeight ? yAdjust : MinHeight;

                if (xAdjust < 0 || yAdjust < 0)
                    return;

                Margin = new Thickness(
                Margin.Left,
                Margin.Top,
                Margin.Right - e.HorizontalChange,
                Margin.Bottom - e.VerticalChange);
            }
            else
            {
                //确保不要调整到负宽度或高度           
                //xAdjust = (ActualWidth + xAdjust) > MinWidth ? xAdjust : MinWidth;
                //yAdjust = (ActualHeight + yAdjust) > MinHeight ? yAdjust : MinHeight;

                //if (xAdjust < 0 || yAdjust < 0)
                //    return;

                // Margin = new Thickness(
                // Margin.Left - e.HorizontalChange,
                // Margin.Top,
                // Margin.Right,
                // Margin.Bottom - e.VerticalChange);
            }
            //Width = xAdjust;
            //Height = yAdjust;
        }
    }
}
