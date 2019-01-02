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
        public CollagePageViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
        }
    }
}
