using ManagementProject.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ManagementProject.UserControls.TimeControl
{
    /// <summary>
    /// TimePickers.xaml 的交互逻辑
    /// </summary>
    public partial class TimePickers : UserControl
    {
        private string formerDateTimeStr = "";

        public TimePickers() => InitializeComponent();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public TimePickers(string txt)
            : this()
        {
            formerDateTimeStr = txt;

            Loaded += TimePickers_Loaded;
            Unloaded += TimePickers_Unloaded;
        }

        private void TimePickers_Unloaded(object sender, RoutedEventArgs e)
        {
            OnDateTimeContent(formerDateTimeStr);
        }

        private void TimePickers_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(formerDateTimeStr))
                formerDateTimeStr = DateTime.Now.ToString();
            else
            {
                CombinedCalendar.SelectedDate = Convert.ToDateTime(Convert.ToDateTime(formerDateTimeStr).ToShortDateString());
                CombinedClock.Time= Convert.ToDateTime(formerDateTimeStr);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (Equals(button.CommandParameter, "1"))//确定
            {
                var data = CombinedCalendar.DisplayDate;
                if (CombinedCalendar.SelectedDate != null)
                    data = (DateTime)CombinedCalendar.SelectedDate;

                var time = CombinedClock.Time;
                var combineTime = data.Add(new TimeSpan(time.Hour, time.Minute, time.Second));
                formerDateTimeStr = combineTime.ToString("yyyy-MM-dd HH:mm:ss");
                OnDateTimeContent(formerDateTimeStr);
            }
            else
            {
                OnDateTimeContent(formerDateTimeStr);
            }
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar||Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        /// <summary>
        /// 时间确定后的传递事件
        /// </summary>
        public Action<string> DateTimeOK;

        /// <summary>
        /// 时间确定后传递的时间内容
        /// </summary>
        /// <param name="dateTimeStr"></param>
        protected void OnDateTimeContent(string dateTimeStr) => DateTimeOK?.Invoke(dateTimeStr);
    }
}
