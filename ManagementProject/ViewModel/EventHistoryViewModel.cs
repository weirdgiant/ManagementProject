using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class EventHistoryViewModel : EventHistoryModel
    {
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public EventHistoryViewModel()
        {
            CloseCommand = new DelegateCommand();
            CloseCommand.ExecuteCommand = new Action<object>(Close);
            SearchCommand = new DelegateCommand();
            SearchCommand.ExecuteCommand = new Action<object>(Search);
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="obj"></param>
        private void Search(object obj)
        {

        }

        private void Close(object obj)
        {
            if (obj != null)
            {
                EventHistory _eventHistory = (EventHistory)obj;
                _eventHistory.Close();
            }
        }
    }
}
