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
        public TrackManagementViewModel()
        {
            OpenAddTrackerCommand = new DelegateCommand();
            OpenAddTrackerCommand.ExecuteCommand = new Action<object>(OpenAddTracker);
        }

        private void OpenAddTracker(object obj)
        {
            AddTracker addTracker = new AddTracker();
            addTracker.ShowDialog();
        }
    }
}
