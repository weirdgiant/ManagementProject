using ManagementProject.FunctionalWindows;
using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.ViewModel
{
    public class TrackPageViewModel : TrackPageModel
    {
        public DelegateCommand OpenTrackerCommand { get; set; }
        public TrackPageViewModel()
        {
            OpenTrackerCommand = new DelegateCommand();
            OpenTrackerCommand.ExecuteCommand = new Action<object>(OpenTracker);
        }
        private void OpenTracker(object obj)
        {
            TrackManagement tr = new TrackManagement();
            tr.ShowDialog();
        }
    }
}
