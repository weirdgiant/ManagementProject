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

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// ModifyRotationWin.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyRotationWin : Window
    {
        public ModifyRotationWin()
        {
            InitializeComponent();
        }

        public ModifyRotationWin(string name):this()
        {
            DataContext = new ModifyRotationWinViewModel(name);
        }
    }

    class ModifyRotationWinViewModel: ModifyRotationWinModel
    {

        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand NextCommand { get; set; }

        public ModifyRotationWinViewModel(string name)
        {
            RotationName= name;

            CloseWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseWin) };
            SubmitCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Submit) };
            DeleteCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Delete) };
            NextCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Next) };
        }

        private void Next(object obj)
        {
            
        }

        private void Delete(object obj)
        {
           
        }

        private void Submit(object obj)
        {
            
        }

        private void CloseWin(object obj)
        {
            Window window = (Window)obj;
            window.Close();
        }
    }

    class ModifyRotationWinModel : INotifyPropertyChangedClass
    {
        private string rotationName;

        /// <summary>
        /// 轮询名称
        /// </summary>
        public string RotationName
        {
            get { return rotationName; }
            set
            {
                rotationName = value;
                NotifyPropertyChanged("RotationName");
            }
        }

        private string duration;

        /// <summary>
        /// 持续时间    
        /// </summary>
        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        private string time;

        /// <summary>
        /// 时间    
        /// </summary>
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }
        private string scenes;

        /// <summary>
        /// 场景    
        /// </summary>
        public string Scenes
        {
            get { return scenes; }
            set
            {
                scenes = value;
                NotifyPropertyChanged("Scenes");
            }
        }
    }
}
