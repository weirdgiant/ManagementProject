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
using System.Windows.Shapes;

namespace ManagementProject.FunctionalWindows
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        private DialogWindow()
        {
            InitializeComponent();
        }

        public DialogWindow(string content)
        {
            InitializeComponent();
            DataContext = new DialogWindowViewModel(content);
        }

        #endregion
    }

    public class DialogWindowViewModel : DialogWindowModel
    {
        public DelegateCommand OkCommand { get; set; }

        public DialogWindowViewModel(string content)
        {
            Title = "消息发送成功";
            Content = content;
            OkCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(Ok) };
        }

        private void Ok(object obj)
        {
            if (obj != null)
            {
                Window window = (Window)obj;
                window.Close();
            }
        }
    }
    public class DialogWindowModel: INotifyPropertyChangedClass
    {
        #region  Properties
        private string title;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                NotifyPropertyChanged("Content");
            }
        }
        #endregion
    }
}
