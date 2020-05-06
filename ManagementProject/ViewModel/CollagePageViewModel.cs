using ManagementProject.Helper;
using ManagementProject.Model;
using ManagementProject.UserControls.FightControl;
using MangoApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PlanRotation = ManagementProject.Model.PlanRotation;
using Player = ManagementProject.UserControls.FightControl.Player;

namespace ManagementProject.ViewModel
{
    public class CollagePageViewModel : CollagePageModel
    {
        #region Property     

        public Grid FightGrid;
        public bool IsSwitchScene { get; set; }
        //private bool IsStart = true;
        private int playControlMargin;

        public List<int> winIdList = new List<int>();

        public MainWindowViewModel MainWindowViewModel { get; set; }

        public DelegateCommand PageLoadedCommand { get; set; }
        public DelegateCommand PageUnloadedCommand { get; set; }
        public DelegateCommand CreatNewWinCommand { get; set; }
        public DelegateCommand CloseAllWinCommand { get; set; }
        public DelegateCommand NewScenesCommand { get; set; }
        public DelegateCommand SaveScenesCommand { get; set; }
        public DelegateCommand SwitchSceneCommand { get; set; }
        public DelegateCommand DeleteSceneCommand { get; set; }
        public DelegateCommand MouseMoveCommand { get; set; }
        public DelegateCommand NewPlanRotationCommand { get; set; }

        //SceneWindow Command
        public DelegateCommand SaveSceneWinCommand { get; set; }
        public DelegateCommand CloseSceneWinCommand { get; set; }
        public DelegateCommand CancelSceneWinCommand { get; set; }

        //PlanRotationWin Command
        public DelegateCommand CloseRotationWinCommand { get; set; }
        public DelegateCommand DeleteRotationCommand { get; set; }
        public DelegateCommand EditRotationCommand { get; set; }
        public DelegateCommand NextRotationCommand { get; set; }
        public DelegateCommand SaveRotationCommand { get; set; }
        public DelegateCommand CancelRotationCommand { get; set; }
        public DelegateCommand OpenPlanRotationCommand { get; set; }
        public DelegateCommand DeleteRotationWinCommand { get; set; }
        public DelegateCommand ClosePlanRotationCommand { get; set; }

        #endregion

        #region Construction Method
        public CollagePageViewModel(MainWindowViewModel _mainWindowViewModel)
        {
            MainWindowViewModel = _mainWindowViewModel;
            InitCommand();
        }
        #endregion

        #region Private Method

        #region Initialization Command

        private void InitCommand()
        {
            PageLoadedCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(PageLoaded) };
            PageUnloadedCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(PageUnloaded) };
            NewScenesCommand = new DelegateCommand { ExecuteCommand = new Action<object>(NewScenes) };
            SaveScenesCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveScenes) };
            CreatNewWinCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(CreatNewWin) };
            CloseAllWinCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(CloseAllWin) };
            NewPlanRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CreatePlanRotation) };

            MouseMoveCommand = new DelegateCommand { ExecuteCommand = new Action<object>(MouseMove) };
            SwitchSceneCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SwitchScene) };
            DeleteSceneCommand = new DelegateCommand { ExecuteCommand = new Action<object>(DeleteScene) };

            SaveSceneWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveSceneWin) };
            CloseSceneWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseSceneWin) };
            CancelSceneWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CancelSceneWin) };

            DeleteRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(DeleteRotation) };
            CloseRotationWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseRotationWin) };
            DeleteRotationWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(DeleteRotationWin) };
            EditRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(EditRotation) };
            NextRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(NextRotation) };
            SaveRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveRotation) };
            CancelRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CancelRotation) };
            OpenPlanRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(OpenPlanRotation) };
            ClosePlanRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(ClosePlanRotation) };
        }

        #endregion

        #region Window Operation

        private void PageLoaded(object obj)
        {
            InitCameraDirectory();
            InitSceneList();
            InitRotationList();
            InitFightGrid(obj);
            InitPinKong();
        }

        private void PageUnloaded(object obj) => FightGrid.Children.Clear();

        private void InitPinKong()
        {
            for (int i = 1; i < 250; i++)
                winIdList.Add(i);
        }

        private void InitFightGrid(object obj)
        {
            FightGrid = (Grid)obj;

            for (int i = 0; i < 3; i++)
                FightGrid.RowDefinitions.Add(new RowDefinition());

            for (int j = 0; j < 6; j++)
                FightGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void InitCameraDirectory()
        {
            //List<CameraList> cameraLists = new List<CameraList>();
            //CollageCameraList[] collageCameras = HttpAPi.GetCollageCamera();
            cameraLists = new List<CameraList>();
            collageCameras = HttpAPi.GetCollageCamera();
            InitCameraList(collageCameras, cameraLists);

            _dataSource = new ObservableCollection<CameraList>(cameraLists);
            CameraItems = new ObservableCollection<CameraList>(cameraLists);
        }

        public new void InitCameraList(CollageCameraList[] collageCameraList, List<CameraList> cameraLists)
        {
            if (collageCameraList == null || collageCameraList.Length <= 0)
                return;

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
        }

        private void CreatePlanRotation(object obj)
        {
            RefushSceneNameList();
            NewPlanRotation.Clear();
            NewPlanRotation.Add(NewRotation());
            ShowRotationWin("新建计划轮巡");
        }

        private void ShowRotationWin(string rotationOperName)
        {
            RotationOperateName = rotationOperName;
            var planWin = new PlanRotationWin { DataContext = this, WindowStartupLocation = WindowStartupLocation.CenterScreen };
            planWin.ShowDialog();
        }

        private void CreatNewWin(object obj)
        {
            CheckCount(FightGrid);
        }

        private void CheckCount(Grid grid)
        {
            IsSwitchScene = false;
            var childrenCount = grid.Children.Count;
            var playControl = new PlayControl(this) { Margin = new Thickness(5) };

            //winId = childrenCount;

            if (childrenCount < 6)
            {
                playControlMargin = 0;
                Grid.SetRow(playControl, 0);
                Grid.SetColumn(playControl, childrenCount);
                grid.Children.Add(playControl);
            }
            else if (childrenCount >= 6 && childrenCount < 12)
            {
                Grid.SetRow(playControl, 1);
                Grid.SetColumn(playControl, childrenCount - 6);
                grid.Children.Add(playControl);
            }
            else if (childrenCount >= 12 && childrenCount < 18)
            {
                Grid.SetRow(playControl, 2);
                Grid.SetColumn(playControl, childrenCount - 12);
                grid.Children.Add(playControl);
            }
            else if (childrenCount >= 18 && childrenCount < winIdList.Count)
            {
                Grid.SetColumn(playControl, 0);
                Grid.SetRow(playControl, 0);
                grid.Children.Add(playControl);
                FramEleHelper.SetControlToTop(grid, playControl);
                playControlMargin += 3;
                playControl.Margin = new Thickness(5 + playControlMargin,
                                                   5 + playControlMargin,
                                                   5 - playControlMargin,
                                                   5 - playControlMargin);
            }
        }

        private void CloseAllWin(object obj)
        {
            winId = 0;
            FightGrid.Children.Clear();
            PinkongHelper.CloseAllAsync();
        }

        private void CloseWindow(object obj)
        {
            var window = (Window)obj;
            window.Close();
        }

        int winId;//拼控窗体Id
        public int GetWinId()
        {
            if (winId < winIdList.Count - 1)//1-250，251-255划分到编码器
                return winIdList[winId++];
            else
                winId = 0;
            return 250;
        }

        #endregion

        #region PlanRotationWin Operation 

        private void InitRotationList()
        {
            RotationLists = HttpAPi.QueryPlanRotation();
            SceneNameList = new ObservableCollection<string>();
            NewPlanRotation = new ObservableCollection<PlanRotation> { NewRotation() };
            //RefreshRotationAsync();
        }

        private PlanRotation NewRotation()
        {
            var sceneName = "";
            if (SceneNameList.Count > 0)
                sceneName = SceneNameList[0];

            return new PlanRotation
            {
                name = "计划轮巡1",
                duration = 0,
                SceneName = sceneName,
                TimeText = DateTime.Now.ToString("HH:mm:ss"),
                IsShowNext = true,
                CanDelete = false
            };
        }

        private void SaveRotation(object obj)
        {
            if (NewPlanRotation == null)
                return;

            if (string.IsNullOrEmpty(RotationName))
            {
                MessageBox.Show("请先输入轮巡名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Duration <= 0)
            {
                MessageBox.Show("轮巡时间必须大于0", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var planRotation = new PlanRotation()
            {
                id = NewPlanRotation[0].id,
                isOpen = NewPlanRotation[0].isOpen,
                name = RotationName,
                duration = Duration,
            };

            if (RotationOperateName.Equals("新建计划轮巡"))
                SaveRotation(planRotation, obj);
            else
                ModifyRotation(planRotation, obj);
        }

        

        private void ModifyRotation(PlanRotation rotation, object obj)
        {
            if (!CheckRotation(rotation))
                return;

            var result = HttpAPi.UpdatePlanRotation(rotation);
            if (result)
            {
                InitRotationList();
                //InitSceneNameList();
                CloseWindow(obj);
                PinkongHelper.ModifyLunXunAsync(true, true);
            }
            else
            {
                MessageBox.Show("更改失败", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void InitSceneNameList()
        {
            SceneNameList = new ObservableCollection<string>();
            NewPlanRotation = new ObservableCollection<PlanRotation> { NewRotation() };
        }

        private void SaveRotation(PlanRotation rotation, object obj)
        {
            if (!CheckRotation(rotation))
                return;

            var result = HttpAPi.SavePlanRotation(rotation, out string resultMsg);
            if (result)
            {
                RotationLists.Add(rotation);
                CloseWindow(obj);
                PinkongHelper.ModifyLunXunAsync(true, true);
            }
            else
            {
                MessageBox.Show($"保存失败,{resultMsg}！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool CheckRotation(PlanRotation planRotation)
        {
            if (planRotation == null)
                return false;

            var strTimes = new List<string>();
            var scenesId = new List<string>();
            var dateTimes = new List<DateTime>();

            foreach (var rotation in NewPlanRotation)
            {
                if (string.IsNullOrEmpty(rotation.SceneName) || string.IsNullOrEmpty(rotation.TimeText))
                {
                    MessageBox.Show("请将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
                foreach (var item in SceneLists)
                {
                    if (item.name.Equals(rotation.SceneName))
                        scenesId.Add(item.id.ToString());
                }
                planRotation.id = rotation.id;
                planRotation.isOpen = rotation.isOpen;
                strTimes.Add(rotation.TimeText);
                dateTimes.Add(DateTime.Parse(rotation.TimeText));
            }

            planRotation.time = string.Join(",", strTimes);
            planRotation.sceneId = string.Join(",", scenesId);

            if (string.IsNullOrEmpty(planRotation.time))
            {
                MessageBox.Show("请至少添加一个计划轮巡场景！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            for (int i = 0; i < dateTimes.Count; i++)
            {
                if (i > 0 && (dateTimes[i] - dateTimes[i - 1]) < TimeSpan.FromSeconds(Duration))
                {
                    MessageBox.Show("当前存在多个场景的播放时间点冲突，请重新选择时间点！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            if (RotationLists.Count == 1)
                return true;

            foreach (var rotation in RotationLists)
            {
                var timeSpan = TimeSpan.FromSeconds(rotation.duration);
                var times = rotation.time.Split(',');

                for (int i = 0; i < dateTimes.Count; i++)
                {
                    for (int j = 0; j < times.Length; j++)
                    {
                        var a = Convert.ToDateTime(times[j]);

                        var dateResult = CheckDate(a, a + timeSpan, dateTimes[i], dateTimes[i] + TimeSpan.FromSeconds(Duration));
                        if (dateResult)
                        {
                            MessageBox.Show($"当前存在与{rotation.name}中多个场景的播放时间点冲突、，请重新选择时间点！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void NextRotation(object obj)
        {
            var button = (Button)obj;
            var rotation = (PlanRotation)button.DataContext;

            if (string.IsNullOrEmpty(rotation.TimeText))
            {
                MessageBox.Show("请先将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewPlanRotation.Insert(NewPlanRotation.Count - 1, new PlanRotation
            {
                TimeText = rotation.TimeText,
                SceneName = rotation.SceneName
            });
        }

        private void DeleteRotation(object obj)
        {
            if (MessageBox.Show("您确定要删除吗？", "提示：", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;

            var rotation = (PlanRotation)obj;
            var result = HttpAPi.DeletePlanRotation(rotation.id);

            if (result)
            {
                RotationLists.Remove(rotation);
                PinkongHelper.ModifyLunXunAsync(true, true);
            }
            else
                MessageBox.Show("删除失败", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DeleteRotationWin(object obj)
        {
            var rotation = (PlanRotation)((Button)obj).DataContext;
            NewPlanRotation.Remove(rotation);
        }

        private void EditRotation(object obj)
        {
            RefushSceneNameList();
            var rotation = (MangoApi.PlanRotation)obj;
            var times = rotation.time.Split(',');//15 25 35
            var scenes = rotation.sceneId.Split(',');

            RotationName = rotation.name;
            Duration = rotation.duration;
            NewPlanRotation.Clear();

            var maxTime = GetMaxTimeByTimes(times);
            for (int i = 0; i < times.Length; i++)//3
            {
                if (i == 0)
                {
                    if (times.Length == 1)
                        NewPlanRotation.Add(new PlanRotation { TimeText = maxTime, IsShowNext = true, CanDelete = false });
                    else
                    {
                        NewPlanRotation.Add(new PlanRotation { TimeText = maxTime, IsShowNext = true, CanDelete = false });
                        NewPlanRotation.Insert(NewPlanRotation.Count - 1, new PlanRotation { TimeText = times[i], });
                    }
                }
                else
                {
                    if (!times[i].Equals(maxTime))
                        NewPlanRotation.Insert(NewPlanRotation.Count - 1, new PlanRotation { TimeText = times[i], });
                }
            }

            var sceneLists = HttpAPi.QuerySceneList();

            for (int i = 0; i < NewPlanRotation.Count; i++)
            {
                NewPlanRotation[i].id = rotation.id;
                NewPlanRotation[i].isOpen = rotation.isOpen;

                if (scenes.Length == 1)
                    NewPlanRotation[i].SceneName = GetRotationNameById(scenes[0], sceneLists);
                else if (scenes.Length > 1)
                    NewPlanRotation[i].SceneName = GetRotationNameById(scenes[i], sceneLists);
            }

            ShowRotationWin("修改计划轮巡");
            PinkongHelper.ModifyLunXunAsync(true, true);
        }

        private string GetMaxTimeByTimes(string[] times)
        {
            var maxDataTime = DateTime.MinValue;
            for (int i = 0; i < times.Length; i++)//3
            {
                var time = DateTime.Parse(times[i]);
                if (time > maxDataTime)
                    maxDataTime = time;
            }
            return maxDataTime.ToLongTimeString();
        }

        private void CancelRotation(object obj) => CloseRotationWin(obj);

        private void CloseRotationWin(object obj)
        {
            var window = (Window)obj;
            window.Close();
        }

        private void ClosePlanRotation(object obj)
        {
            var rotation = (PlanRotation)obj;
            if (!HttpAPi.UpdatePlanRotation(rotation))
                MessageBox.Show("修改失败");

            PinkongHelper.ModifyLunXunAsync(true, true);
        }

        private void OpenPlanRotation(object obj)
        {
            var rotation = (PlanRotation)obj;
            if (!HttpAPi.UpdatePlanRotation(rotation))
                MessageBox.Show("修改失败");

            PinkongHelper.ModifyLunXunAsync(true, true);
        }


        private string GetRotationNameById(string rotationId, List<SceneList> sceneLists)
        {
            if (sceneLists != null)
            {
                var scenes = sceneLists.Where(x => x.id.ToString().Equals(rotationId)).ToList();
                if (scenes.Count > 0)
                    return scenes[0].name;
            }
            return null;
        }

        private bool CheckDate(DateTime a, DateTime b, DateTime x, DateTime y)
        {
            //时间无重叠                 
            if ((b < x) || (y < a))
                return false;

            //重叠
            if ((a < x) && (x < b))
                return true;
            if ((a < y) && (y < b))
                return true;
            if ((a < y) && (b < x))// origin----> if ((a < y) && (b == x))
                return true;
            if (a > x && a < y)
                return true;

            return false;
        }

        #endregion

        #region Scene Operation

        private void InitSceneList()
        {
            SceneLists = new ObservableCollection<SceneList>();
            var sceneList = HttpAPi.QuerySceneList();
            if (sceneList != null)
                sceneList.ForEach(scene => SceneLists.Add(scene));
        }

        private void RefushSceneNameList()
        {
            SceneNameList.Clear();
            var sceneList = HttpAPi.QuerySceneList();
            if (sceneList != null)
                sceneList.ForEach(scene => SceneNameList.Add(scene.name));
        }

        private void SaveSceneWin(object obj)
        {
            if (string.IsNullOrEmpty(ScenesName))
            {
                MessageBox.Show($"请先输入场景名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var scenesConfig = new List<ScenesConfig>();
            int i = 0;
            foreach (PlayControl playControl in FightGrid.Children)
            {
                scenesConfig.Add(new ScenesConfig
                {
                    margin = playControl.Margin.ToString(),
                    width = playControl.ActualWidth,
                    height = playControl.ActualHeight,
                    winId = playControl.winParams.id,
                    zIndex = Convert.ToInt32(playControl.GetValue(Panel.ZIndexProperty)),
                    rowProperty = Convert.ToInt32(playControl.GetValue(Grid.RowProperty)),
                    columnProperty = Convert.ToInt32(playControl.GetValue(Grid.ColumnProperty)),
                    childFights = new List<ChildFight>()
                });

                foreach (var playControlGrid in playControl.grid.Children)
                {
                    if (playControlGrid is FightPanel fightPanel && fightPanel != null)
                    {
                        foreach (Player player in fightPanel.GridFight.Children)
                        {
                            scenesConfig[i].childFights.Add(new ChildFight
                            {
                                cameraCode = player.CameraCode ?? "",
                                cameraName = player.CameraName ?? "",
                                vendor = player.Vendor.ToString(),
                                user = player.User ?? "",
                                pass = player.Password ?? "",
                                ip = player.Ip ?? "",
                                port = player.Port ?? "",
                            });
                        }
                        i++;
                    }
                }
            }
            var sceneList = new SceneList() { name = ScenesName, scenesList = scenesConfig };
            var result = HttpAPi.SaveSceneList(sceneList, out string resultMsg);
            if (result)
            {
                InitSceneList();
                ((Window)obj).Close();

                var msg = JsonConvert.DeserializeObject<SceneList>(resultMsg);

                PinkongHelper.SaveSceneAsync(new Scene { id = msg.id, name = ScenesName });
            }
            else
            {
                MessageBox.Show($"保存失败，{resultMsg}！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelSceneWin(object obj) => CloseSceneWin(obj);

        private void CloseSceneWin(object obj)
        {
            var window = (Window)obj;
            window.Close();
        }

        private void SwitchScene(object obj)
        {
            if (obj == null)
                return;
            IsSwitchScene = true;

            var sceneList = (SceneList)obj;
            FightGrid.Children.Clear();

            foreach (var window in sceneList.scenesList)
            {
                var margin = window.margin.Split(',');
                var playControl = new PlayControl(this)
                {
                    Margin = new Thickness(Convert.ToDouble(margin[0]),
                                           Convert.ToDouble(margin[1]),
                                           Convert.ToDouble(margin[2]),
                                           Convert.ToDouble(margin[3])),
                };
                playControl.SetValue(Panel.ZIndexProperty, window.zIndex);
                playControl.SetValue(Grid.ColumnProperty, window.columnProperty);
                playControl.SetValue(Grid.RowProperty, window.rowProperty);
                FightGrid.Children.Add(playControl);
                DrawPleyer(window, playControl);
            }

            PinkongHelper.SwitchSceneAsync(new Scene { id = sceneList.id, name = sceneList.name });
        }

        private void DrawPleyer(ScenesConfig window, PlayControl playControl)
        {
            FightPanel.DrawPlayGrid(window.childFights.Count, playControl.fightPanel.GridFight);
            playControl.winId = window.winId;
            var players = playControl.fightPanel.GridFight.Children;
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] is Player player)
                {
                    player.CameraCode = window.childFights[i].cameraCode;
                    player.CameraName = window.childFights[i].cameraName;
                }
            }
        }

        private void DeleteScene(object obj)
        {
            if (MessageBox.Show("您确定要删除吗？", "提示：", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;

            var sceneList = (SceneList)obj;
            var result = HttpAPi.DeleteSceneList(sceneList.id, out string resultMsg);
            if (result)
                SceneLists.Remove(sceneList);
            else
                MessageBox.Show($"删除失败,{resultMsg}", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void SaveScenes(object obj)
        {
            if (FightGrid.Children.Count > 0)
            {
                var scenesWin = new SaveScenesWin() { DataContext = this, WindowStartupLocation = WindowStartupLocation.CenterScreen };
                scenesWin.ShowDialog();
            }
        }

        private void NewScenes(object obj)
        {
            PinkongHelper.CloseAllAsync();
            var grid = (Grid)obj;
            IsSwitchScene = false;
            grid.Children.Clear();
            var rows = grid.RowDefinitions.Count;
            var columns = grid.ColumnDefinitions.Count;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (grid.Children.Count >= winIdList.Count)
                        return;

                    var playControl = new PlayControl(this) { Margin = new Thickness(5) };
                    grid.Children.Add(playControl);
                    Grid.SetColumn(playControl, j);
                    Grid.SetRow(playControl, i);
                }
            }
        }

        #endregion    

        #region CameraList Drag Effect

        private void MouseMove(object obj)
        {
            try
            {
                var parameter = (ExCommandParameter)obj;
                var treeView = (TreeView)parameter.Sender;
                var e = (MouseEventArgs)parameter.EventArgs;

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var item = (CameraList)treeView.SelectedItem;
                    if (item != null && !string.IsNullOrEmpty(item.Code))
                    {
                        DragDrop.DoDragDrop(treeView, item, DragDropEffects.Copy);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        #endregion

        #endregion
    }
}
