using System;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// SaveScenes.xaml 的交互逻辑
    /// </summary>
    public partial class SaveScenesWin : Window
    {
        public SaveScenesWin(object obj)
        {
            InitializeComponent();
            DataContext = new SaveScenesWinViewModel(obj);
        }
    }

    class SaveScenesWinViewModel : SaveScenesWinModel
    {
        /// <summary>
        /// 拼控Grid
        /// </summary>
        private Grid fightGrid;

        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand SaveScenesCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public SaveScenesWinViewModel(object obj)
        {
            try
            {
                fightGrid = (Grid)obj;
                CloseWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseWin) };
                SaveScenesCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveScenes) };
                CancelCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Cancel) };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SaveScenes(object obj)
        {
            if (string.IsNullOrEmpty(ScenesName))
            {
                MessageBox.Show($"请先输入场景名称","提示",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            foreach (var item in fightGrid.Children)
            {
                //Width、Height、Margin、ElementCount、ZIndex

                if (item is PlayControl playControl)
                {
                    var margin= playControl.Margin;
                    var height = playControl.ActualHeight;
                    var width = playControl.ActualWidth;
                    

                }
            }

            MessageBox.Show($"ScenesName:{ScenesName}");
        }

        private void Cancel(object obj)
        {
            CloseWin(obj);
        }

        private void CloseWin(object obj)
        {
            Window window = (Window)obj;
            window.Close();
        }
    }

    class SaveScenesWinModel : INotifyPropertyChangedClass
    {
        private string scenesName;

        public string ScenesName
        {
            get { return scenesName; }
            set
            {
                scenesName = value;
                NotifyPropertyChanged("ScenesName");
            }
        }
    }
}
