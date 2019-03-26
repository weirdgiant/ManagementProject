using System;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.TimeControl
{
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        #region 构造函数
        //
        public DateTimePicker()
        {
            InitializeComponent();
            //DataContext=new PlanRotationWinViewModel(); 
            textBlock1.TextChanged += TextBlock1_TextChanged;
        }

       

        public DateTimePicker(string txt)
            : this()
        {
            // this.textBox1.Text = txt;
        }
        #endregion

        #region 属性

        public static readonly DependencyProperty TimeTextProperty =
        DependencyProperty.Register("TimeText", typeof(string), typeof(DateTimePicker),  new PropertyMetadata(null, (s, e) =>
                {
                    //var dp = s as DateTimePicker;
                    //if (dp != null)
                    //{
                    //    try
                    //    {
                    //        dp.textBlock1.Text = e.NewValue.ToString();
                    //    }
                    //    catch
                    //    {

                    //    }
                    //}

                }));

        private void TextBlock1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimeText= textBlock1.Text;
        }

        private static void ChangeText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dpk = (DateTimePicker)d;
            dpk.TimeText = dpk.textBlock1.Text;
            //dpk.TimeText = dpk.DateTime.ToString();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == TimeTextProperty)
            {
                //TimeText = textBlock1.Text;
            }
        }

        public string TimeText
        {
            get { return (string)GetValue(TimeTextProperty); }
            set { SetValue(TimeTextProperty, value); }
        }

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTime { get; set; }

        #endregion

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            if (popChioce.IsOpen)
            {
                popChioce.IsOpen = false;
            }

            TDateTimeView dtView = new TDateTimeView(textBlock1.Text);// TDateTimeView  构造函数传入日期时间
            
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {
                textBlock1.Text = dateTimeStr;
                
                DateTime = Convert.ToDateTime(dateTimeStr);
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭
            };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }

        /// <summary>
        /// DateTimePicker 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            textBlock1.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime = dt;
        }

        #endregion
    }

}
