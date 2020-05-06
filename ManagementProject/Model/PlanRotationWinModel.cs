using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Model
{
    public class PlanRotationWinModel : INotifyPropertyChangedClass
    {
        private ObservableCollection<PlanRotation> planRotation;

        public ObservableCollection<PlanRotation> PlanRotation
        {
            get { return planRotation; }
            set
            {
                planRotation = value;
                NotifyPropertyChanged("PlanRotation");
            }
        }

        private List<Scenes> scenesList;

        public List<Scenes> ScenesList
        {
            get { return scenesList; }
            set
            {
                scenesList = value;
                NotifyPropertyChanged("ScenesList");
            }
        }

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

        private double duration;

        /// <summary>
        /// 持续时间    
        /// </summary>
        public double Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        private DateTime time;

        /// <summary>
        /// 时间    
        /// </summary>
        public DateTime Time
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

    public class Scenes
    {
        /// <summary>
        /// 场景Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 场景名称
        /// </summary>
        public string Name { get; set; }
    }
}
