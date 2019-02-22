using ManagementProject.ViewModel;
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
    /// AlarmMainPage.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmMainPage : UserControl
    {
        public AlarmPageViewModel alarmPageViewModel { get; set; }
        public AlarmMainPage()
        {
            InitializeComponent();
            DataContext = alarmPageViewModel;
        }



        public class AlarmPageLayout
        {
            /// <summary>
            /// 枚举，五种布局方式
            /// </summary>
            public string LayoutType;
            /// <summary>
            /// 布局中的组件，如没有为空
            /// </summary>
            public AlarmControl LayoutPanel1;
            public AlarmControl LayoutPanel2;
            public AlarmControl LayoutPanel3;
            public AlarmControl LayoutPanel4;
        }

        public class AlarmControl
        {
            public string ControlName;
            public string ControlSize;
            public string ControlType;
        }

        /// <summary>
        /// 布局方式枚举，命名简单粗暴
        /// </summary>
        public enum LayoutType
        {
            Layout1,
            layout2,
            layout3,
            layout4,
            layout5
        }
    }
}
