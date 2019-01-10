using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class CollagePageViewModel : CollagePageModel
    {
        public MainWindowViewModel MainWindowViewModel { get; set; }
        /// <summary>
        /// 生成窗口
        /// </summary>
        public DelegateCommand CreatNewWinCommand { get; set; }
        /// <summary>
        /// 关闭平台窗口
        /// </summary>
        public DelegateCommand CloseAllWinCommand { get; set; }
        public CollagePageViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            InitCommand();
        }

        public void InitCommand()
        {
            CreatNewWinCommand = new DelegateCommand();
            CreatNewWinCommand.ExecuteCommand = new Action<object>(CreatNewWin);
            CloseAllWinCommand = new DelegateCommand();
            CloseAllWinCommand.ExecuteCommand = new Action<object>(CloseAllWin);
        }

        /// <summary>
        /// 生成窗口
        /// </summary>
        /// <param name="obj"></param>
        private void CreatNewWin(object obj)
        {

        }

        /// <summary>
        /// 关闭平台窗口
        /// </summary>
        /// <param name="obj"></param>
        private void CloseAllWin(object obj)
        {

        }
    }
}
