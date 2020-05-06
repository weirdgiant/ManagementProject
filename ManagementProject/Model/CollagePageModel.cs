using MangoApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ManagementProject.Model
{
    public class CollagePageModel : INotifyPropertyChangedClass
    {
        private bool isOpen;
        private bool isMatch = true;
        private bool isExpanded;
        private string criteria;
        private string scenesName;
        private double duration;
        private string rotationName;
        private string rotationOperateName;
        public List<CameraList> cameraLists;
        public CollageCameraList[] collageCameras;

        private ObservableCollection<string> sceneNameList;
        private ObservableCollection<SceneList> sceneLists;
        private ObservableCollection<CameraList> cameraItems;
        private ObservableCollection<PlanRotation> newPlanRotation;
        private ObservableCollection<PlanRotation> rotationLists;
        public  ObservableCollection<CameraList> _dataSource;

        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                NotifyPropertyChanged("IsOpen");
            }
        }

        public bool IsMatch
        {
            get { return isMatch; }
            set
            {
                isMatch = value;
                NotifyPropertyChanged("IsMatch");
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                NotifyPropertyChanged("IsExpanded");
            }
        }

        public string ScenesName
        {
            get { return scenesName; }
            set
            {
                scenesName = value;
                NotifyPropertyChanged("ScenesName");
            }
        }

        public string RotationOperateName
        {
            get { return rotationOperateName; }
            set
            {
                rotationOperateName = value;
                NotifyPropertyChanged("RotationOperateName");
            }
        }
        public ObservableCollection<string> SceneNameList
        {
            get { return sceneNameList; }
            set
            {
                sceneNameList = value;
                NotifyPropertyChanged("SceneNameList");
            }
        }

        public ObservableCollection<SceneList> SceneLists
        {
            get { return sceneLists; }
            set
            {
                sceneLists = value;
                NotifyPropertyChanged("SceneLists");
            }
        }

        public ObservableCollection<CameraList> CameraItems
        {
            get { return cameraItems; }
            set
            {
                cameraItems = value;
                NotifyPropertyChanged("CameraItems");
            }
        }

        public ObservableCollection<PlanRotation> RotationLists
        {
            get { return rotationLists; }
            set
            {
                rotationLists = value;
                NotifyPropertyChanged("RotationLists");
            }
        }

        public ObservableCollection<PlanRotation> NewPlanRotation
        {
            get { return newPlanRotation; }
            set
            {
                newPlanRotation = value;
                NotifyPropertyChanged("NewPlanRotation");
            }
        }

        public string RotationName
        {
            get { return rotationName; }
            set
            {
                rotationName = value;
                NotifyPropertyChanged("RotationName");
            }
        }

        public double Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        public string Criteria
        {
            get { return criteria; }
            set
            {
                criteria = value;
                NotifyPropertyChanged("Criteria");
                ApplyFilter();
            }
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(Criteria))
            {
                CameraItems=_dataSource;
                return;
            }
       
            cameraLists.Clear();
            CameraItems.Clear();
            var result= InitCameraList(collageCameras, cameraLists);

            CameraItems = new ObservableCollection<CameraList>(result);
        }

        public List<CameraList>  InitCameraList(CollageCameraList[] collageCameraList, List<CameraList> cameraLists)
        {
            foreach (var item in collageCameraList)
            {
                CameraList map = new CameraList
                {
                    Id = item.id,
                    Pid = item.pid,
                    Name = item.name
                };
                cameraLists.Add(map);

                if (item.devices != null && item.devices.Length > 0)
                {
                    map.Children = new List<CameraList>();

                    foreach (var child in item.devices)
                    {
                        if (!child.name.Contains(Criteria))
                        {
                            //map.Children.Remove(map);
                            continue;
                        }

                        CameraList camera = new CameraList
                        {
                            Code = child.code,
                            Id = child.id,
                            Pid = child.mapId,
                            Name = child.name,
                            Vendor = Convert.ToInt32(child.brand),
                            Ip = child.ip,
                            Port = child.port,
                            User = child.username,
                            Password = child.password,
                        };
                        map.Children.Add(camera);
                        
                    }
                }
                if (item.children != null)
                {
                    if (map.Children == null)
                        map.Children = new List<CameraList>();

                    InitCameraList(item.children, map.Children);
                }
            }

            return cameraLists;
        }
    }


    public class PlanRotation : MangoApi.PlanRotation
    {
        public bool IsShowNext { get; set; }
        public bool CanDelete { get; set; } = true;
        public string SceneName { get; set; }
        public string TimeText { get; set; }
    }

    public class CameraList
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Ip { get; set; }
        public string Port { get; set; }
        public int Vendor { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Icon { get; set; }
        public List<CameraList> Children { get; set; }

        #region Filter
        //public void ApplyCriteria(string criteria, Stack<CameraList> ancestors)
        //{

        //    if (IsCriteriaMatched(criteria))
        //    {
        //        IsMatch = true;
        //        foreach (var ancestor in ancestors)
        //        {
        //            ancestor.IsMatch = true;//显示
        //            ancestor.IsExpanded = !string.IsNullOrEmpty(criteria);
        //            CheckChildren(criteria, ancestor);
        //        }
        //        IsExpanded = false;//展开
        //    }
        //    else
        //        IsMatch = false;

        //    ancestors.Push(this);

        //    foreach (var child in ancestors)
        //        child.ApplyCriteria(criteria, ancestors);

        //    ancestors.Pop();
        //}


        //private void CheckChildren(string criteria, CollagePageModel parent)
        //{
        //    foreach (var child in parent.CameraItems)
        //    {
        //        if (!child.IsCriteriaMatched(criteria))
        //        {
        //            child.IsMatch = false;
        //        }
        //        CheckChildren(criteria, child);
        //    }
        //}

        //private bool IsCriteriaMatched(string criteria) => string.IsNullOrEmpty(criteria) || Name.Contains(criteria); 
        #endregion
    }
}
