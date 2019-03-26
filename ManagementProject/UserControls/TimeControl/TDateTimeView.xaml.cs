using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls.TimeControl
{
    /// <summary>
    /// TDateTimeView.xaml 的交互逻辑
    /// </summary>
    public partial class TDateTimeView : UserControl
    {
        #region 构造函数
        public TDateTimeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public TDateTimeView(string txt)
            : this()
        {
            this.formerDateTimeStr = txt;
        }
        #endregion

        #region 全局变量

        /// <summary>
        /// 从 DateTimePicker 传入的日期时间字符串
        /// </summary>
        private string formerDateTimeStr = string.Empty;

        // private string selectDate = string.Empty;    

        #endregion   

        #region 事件

        private void UserControl_Unloaded(object sender, RoutedEventArgs e) => btnOK_Click(this, null);

        /// <summary>
        /// TDateTimeView 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            #region 时间控件赋值
            for (int i = 0; i <= 59; i++)
            {
                if (i < 24)
                    CboHour.Items.Add(i);

                CboMin.Items.Add(i);
                CboSec.Items.Add(i);
            }

            var now = DateTime.Now;

            CboHour.Text = now.Hour.ToString();
            CboMin.Text = now.Minute.ToString();
            CboSec.Text = now.Second.ToString();
            #endregion
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBtnCloseView_Click(object sender, RoutedEventArgs e)
        {
            OnDateTimeContent(formerDateTimeStr);
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dt = new DateTime?();

            if (calDate.SelectedDate == null)
            {
                dt = DateTime.Now.Date;
            }
            else
            {
                dt = calDate.SelectedDate;
            }

            DateTime dtCal = Convert.ToDateTime(dt);

            string timeStr = "00:00:00";
            
            timeStr = CboHour.Text + ":" + CboMin.Text + ":" + CboSec.Text;

            string dateStr;
            dateStr = dtCal.ToString("yyyy-MM-dd");

            string dateTimeStr;
            dateTimeStr = dateStr + " " + timeStr;

            string str1 = string.Empty;
            str1 = dateTimeStr;
            OnDateTimeContent(str1);
        }

        /// <summary>
        /// 解除calendar点击后 影响其他按钮首次点击无效的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calDate_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        #endregion

        #region Action交互

        /// <summary>
        /// 时间确定后的传递事件
        /// </summary>
        public Action<string> DateTimeOK;

        /// <summary>
        /// 时间确定后传递的时间内容
        /// </summary>
        /// <param name="dateTimeStr"></param>
        protected void OnDateTimeContent(string dateTimeStr)
        {
            if (DateTimeOK != null)
            {
                DateTimeOK(dateTimeStr);
            }
        }

        #endregion
    }
}
