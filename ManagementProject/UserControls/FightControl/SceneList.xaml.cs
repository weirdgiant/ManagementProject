using System;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.FightControl
{
    /// <summary>
    /// Scenes.xaml 的交互逻辑
    /// </summary>
    public partial class SceneList : UserControl
    {
        public SceneList()
        {
            InitializeComponent();
            DataContext = new SceneListViewModel();
        }
    }

    class SceneListViewModel: SceneListModel
    {
        public DelegateCommand DeleteCommand { get; set; }

        public SceneListViewModel()
        {
            Scene = "场景1";
            DeleteCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Delete) };

        }

        private void Delete(object obj)
        {
            var result=  MessageBox.Show("确定要删除此场景吗？","提示",MessageBoxButton.OK,MessageBoxImage.Question);
            if (result==MessageBoxResult.OK)
            {
                //MessageBox.Show("sdsd");
            }
        }
    }

    class SceneListModel:INotifyPropertyChangedClass
    {
        private string scene;

        public string Scene
        {
            get { return scene; }
            set
            {
                scene = value;
                NotifyPropertyChanged("Scene");
            }
        }

    }
}
