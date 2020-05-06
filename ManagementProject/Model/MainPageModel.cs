using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class MainPageModel:INotifyPropertyChangedClass 
    {
        private int _sid;
        public int Sid
        {
            get
            {
                return _sid;
            }
            set
            {
                _sid = value;
                NotifyPropertyChanged("Sid");
            }
        }

        private List<string> _deviceTypeList;
        public List <string> DeviceTypeList
        {
            get
            {
                return _deviceTypeList;
            }
            set
            {
                _deviceTypeList = value;
                NotifyPropertyChanged("DeviceTypeList");
            }
        }

        private bool _isTracker;
        public bool IsTracker
        {
            get
            {
                return _isTracker;
            }
            set
            {
                _isTracker = value;
                NotifyPropertyChanged("IsTracker");
            }
        }

        private List<string> _codeList;
        public List<string> CodeList
        {
            get
            {
                return _codeList;
            }
            set
            {
                _codeList = value;
                NotifyPropertyChanged("CodeList");
            }
        }

        private string _selectedDeviceCode;
        public string SelectedDeviceCode
        {
            get
            {
                return _selectedDeviceCode;
            }
            set
            {
                _selectedDeviceCode = value;
                NotifyPropertyChanged("SelectedDeviceCode");
            }
        }
        private string _selectedElementCode;
        public string SelectedElementCode
        {
            get
            {
                return _selectedElementCode;
            }
            set
            {
                _selectedElementCode = value;
                NotifyPropertyChanged("SelectedElementCode");
            }
        }


        private bool _isOpenTag=true;
        public bool IsOpenTag
        {
            get
            {
                return _isOpenTag;
            }
            set
            {
                _isOpenTag = value;
                NotifyPropertyChanged("IsOpenTag");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
    }
}
