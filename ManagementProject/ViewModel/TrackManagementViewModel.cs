using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class TrackManagementViewModel:TrackManagementModel 
    {
        public DelegateCommand OpenAddTrackerCommand { get; set; }
        public DelegateCommand AddWinCloseCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public TrackManagementViewModel()
        {
            OpenAddTrackerCommand = new DelegateCommand();
            OpenAddTrackerCommand.ExecuteCommand = new Action<object>(OpenAddTracker);
            AddWinCloseCommand = new DelegateCommand();
            AddWinCloseCommand.ExecuteCommand = new Action<object>(AddWinClose);
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteCommand = new Action<object>(Close);

        }
        /// <summary>
        /// 打开AddTracker
        /// </summary>
        /// <param name="obj"></param>
        private void OpenAddTracker(object obj)
        {
            AddTracker addTracker = new AddTracker();
            addTracker.DataContext = this;
            addTracker.ShowDialog();
        }
        /// <summary>
        /// 关闭TrackManagement
        /// </summary>
        /// <param name="obj"></param>
        private void Close(object obj)
        {
            TrackManagement tr = (TrackManagement)obj;
            tr.Close();
        }
        /// <summary>
        /// 关闭AddTracker
        /// </summary>
        /// <param name="obj"></param>
        private void AddWinClose(object obj)
        {
            AddTracker win = (AddTracker)obj;
            win.Close();
        }

        private void SelectCamera(object obj)
        {

        }

        private void NewTracker(object obj)
        {

        }
    }
}
