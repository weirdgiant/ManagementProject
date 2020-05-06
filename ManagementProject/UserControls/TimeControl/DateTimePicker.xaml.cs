using ManagementProject.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ManagementProject.UserControls.TimeControl
{
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        #region Construction Method

        public DateTimePicker()
        {
            InitializeComponent();
            textBlock1.TextChanged += TextBlock1_TextChanged;
        }

        public DateTimePicker(string txt)
            : this()
        {
        }

        #endregion

        #region Property

        /// <summary>
        /// 时间附件属性
        /// </summary>
        public static readonly DependencyProperty TimeTextProperty =
        DependencyProperty.Register("TimeText", typeof(string), typeof(DateTimePicker), new PropertyMetadata(defaultValue: null,
                    propertyChangedCallback: OnPropertyChanged,coerceValueCallback));
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DateTimePicker dpk)
                dpk.textBlock1.Text = dpk.TimeText;
        }

        private static object coerceValueCallback(DependencyObject d, object baseValue)
        {
            if (baseValue != null)
            {
                var control = d as DateTimePicker;
                try
                {
                    string time = (string)baseValue;
                    control.textBlock1.Text = time;
                }
                catch (Exception ex)
                {
                    Logger.Error(typeof(MapControl), "coerceValueCallback:" + ex.Message);
                }
            }
            return baseValue;
        }

        public string TimeText
        {
            get { return (string)GetValue(TimeTextProperty); }
            set { SetValue(TimeTextProperty, value); }
        }


        public double ButtonFontSize
        {
            get { return (double)GetValue(ButtonFontSizeProperty); }
            set { SetValue(ButtonFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonFontSizeProperty =
            DependencyProperty.Register("ButtonFontSize", typeof(double), typeof(DateTimePicker), new PropertyMetadata(12.0,ChangeFontSize));

        private static void ChangeFontSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker timePicker = (DateTimePicker)d;
            Button button = timePicker.ButtonPicker;
            button.FontSize = timePicker.ButtonFontSize;
        }

        public Brush ButtonForeground
        {
            get { return (Brush)GetValue(ButtonForegroundProperty); }
            set { SetValue(ButtonForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonForegroundProperty =
            DependencyProperty.Register("ButtonForeground", typeof(Brush), typeof(DateTimePicker), new UIPropertyMetadata(Brushes.Red, ChangeForeground));

        private static void ChangeForeground(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker timePicker = (DateTimePicker)d;
            Button button = timePicker.ButtonPicker;
            button.Foreground = timePicker.ButtonForeground;
            //Path path = timePicker.ButtonPath;
            //path.Fill = timePicker.ButtonForeground;
        }

        public Brush TextBackground
        {
            get { return (Brush)GetValue(TextBackgroundProperty); }
            set { SetValue(TextBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBackgroundProperty =
            DependencyProperty.Register("TextBackground", typeof(Brush), typeof(DateTimePicker), new UIPropertyMetadata(Brushes.Red, ChangeBackground));

        private static void ChangeBackground(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker timePicker = (DateTimePicker)d;
            TextBox textBox = timePicker.textBlock1;
            textBox.Background = timePicker.TextBackground;
        }

        #endregion

        #region Event

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void IconButton_Click(object sender, RoutedEventArgs e)
        {
            var dtView = new TDateTimeView(TimeText);
            dtView.DateTimeOK += (dataTimeStr) =>
             {
                 textBlock1.Text = dataTimeStr;
                 popChioce.IsOpen = false;
             };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;

            //await DialogHost.Show(dtView, "RootDialog");
            //DialogHost.IsOpen = true;
        }

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimeText = textBlock1.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            //if (Equals(button.CommandParameter, "1"))//确定
            //{
            //    var data = CombinedCalendar.DisplayDate;
            //    if (CombinedCalendar.SelectedDate != null)
            //        data = (DateTime)CombinedCalendar.SelectedDate;

            //    var time = CombinedClock.Time;
            //    var combineTime = data.Add(new TimeSpan(time.Hour, time.Minute, time.Second));
            //    TimeText = combineTime.ToString("yyyy-MM-dd HH:mm:ss");
            //    DialogHost.IsOpen = false;
            //}
            //else
            //{
            //    DialogHost.IsOpen = false;
            //}
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        #endregion
    }
}
