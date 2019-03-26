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
    /// PageControl.xaml 的交互逻辑
    /// </summary>
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
        }
    }

    public class PageControlModel : INotifyPropertyChangedClass
    {
        private int _pageSize;
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
                NotifyPropertyChanged("PageSize");
            }
        }
        private int _totalSize;
        /// <summary>
        /// 记录总条数
        /// </summary>
        public int TotalSize
        {
            get
            {
                return _totalSize;
            }
            set
            {
                _totalSize = value;
                NotifyPropertyChanged("TotalSize");
            }
        }
        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
                NotifyPropertyChanged("PageCount");
            }
        }
        private int _countPage;
        /// <summary>
        /// 当前页
        /// </summary>
        public int CountPage
        {
            get
            {
                return _countPage;
            }
            set
            {
                _countPage = value;
                NotifyPropertyChanged("CountPage");
            }
        }
    }
    public class PageControlViewModel:PageControlModel
    {
        public DelegateCommand FirstPageCommand { get; set; }
        public DelegateCommand LastPageCommand { get; set; }
        public DelegateCommand NextPageCommand { get; set; }
        public DelegateCommand PreviousPageCommand { get; set; }
        public DelegateCommand GoToPageCommand { get; set; }
        private void InitCommand()
        {
            FirstPageCommand = new DelegateCommand();
            FirstPageCommand.ExecuteCommand = new Action<object>(FirstPage);
            LastPageCommand = new DelegateCommand();
            LastPageCommand.ExecuteCommand = new Action<object>(LastPage);
            NextPageCommand = new DelegateCommand();
            NextPageCommand.ExecuteCommand = new Action<object>(NextPage);
            PreviousPageCommand = new DelegateCommand();
            PreviousPageCommand.ExecuteCommand = new Action<object>(PreviousPage);
            GoToPageCommand = new DelegateCommand();
            GoToPageCommand.ExecuteCommand = new Action<object>(GoToPage);
        }
        public PageControlViewModel()
        {
            InitCommand();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="obj"></param>
        private void FirstPage(object obj)
        {

        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="obj"></param>
        private void LastPage(object obj)
        {

        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="obj"></param>
        private void NextPage(object obj)
        {

        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="obj"></param>
        private void PreviousPage(object obj)
        {

        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="obj"></param>
        private void GoToPage(object obj)
        {

        }
    }
}
