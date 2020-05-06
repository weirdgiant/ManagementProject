using ManagementProject.Helper;
using ManagementProject.Model;
using ManagementProject.UserControls;
using MangoApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementProject.ViewModel
{
    public class AlarmPageViewModel : AlarmPageModel
    {
        public StackPanel RightTabPanel { get; set; }
        public Grid FrameGrid { get; set; }
        public Frame MainFrame { get; set; }
        public MainWindow _mainWin;
        public TabControlWin _tabControlWin;
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public DisposalPlanViewModel disposalPlanViewModel { get; set; }
        public DelegateCommand MainPageReturnCommand { get; set; }
        public DelegateCommand AlarmMainCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand UnLoadCommand { get; set; }
        public AlarmPageViewModel(MainWindowViewModel _mainWindowViewModel )
        {
            mainWindowViewModel = _mainWindowViewModel;
            InitCommand();
            InitControl();
           // Reloaded();
        }


        public void Reloaded()
        {           
            LoadAlarmMain();
        }

        private int _reloadId;
        public int ReloadId
        {
            get
            {
                return _reloadId;
            }
            set
            {
                _reloadId = value;
                NotifyPropertyChanged("ReloadId");
                SelectedId = value;
                LoadTabControl();
            }
        }


        private int _selectedId;
        public int SelectedId
        {
            get
            {
                return _selectedId;
            }
            set
            {
                _selectedId = value;
                NotifyPropertyChanged("SelectedId");
                if (SelectedId!=0)
                {                 
                    GetSelectedAlarm();
                    ReloadedAlarm();
                    Reloaded();
                    LoadRightTab();
                }
               
            }
        }

        private void GetSelectedAlarm()
        {
            try
            {
                SelectedAlarm = AlarmInfo.Where(x => x.id == SelectedId).ToArray()[0];
                GlobalVariable.CurrentAlarmLevel = SelectedAlarm.level;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        private void ReloadedAlarm()
        {
            disposalPlanViewModel.AlarmID = SelectedId;
        }

        private void InitCommand()
        {
            MainPageReturnCommand = new DelegateCommand();
            MainPageReturnCommand.ExecuteCommand = new Action<object>(MainPageReturn);
            AlarmMainCommand = new DelegateCommand();
            AlarmMainCommand.ExecuteCommand = new Action<object>(AlarmMain);
            LoadCommand = new DelegateCommand();
            LoadCommand.ExecuteCommand = new Action<object>(Load);
            UnLoadCommand = new DelegateCommand();
            UnLoadCommand.ExecuteCommand = new Action<object>(UnLoad);
        }



        private void InitControl()
        {
            disposalPlanViewModel = new DisposalPlanViewModel(this);
        }

        private void Load(object obj)
        {
            PageView.AlarmPage page = (PageView.AlarmPage)obj;
            mainWindowViewModel.InitAllSubWin();
            RightTabPanel = page.MyPanel;
            FrameGrid = page.mianGrid;
            MainFrame = page.mainFrame;
            GlobalVariable.IsAlarmPage = true;
           // Reloaded();
            LoadTabControl();
           
            //  LoadRightTab();
        }

        /// <summary>
        /// 加载右侧导航栏
        /// </summary>
        public void LoadRightTab()
        {
            try
            {
                RightTabPanel.Children.Clear();
                SetMainRedioBt();
                foreach (var item in ScenesList)
                {
                    if (item.position !=1)
                    {
                        InitSubAlarm(item.position,item);
                        continue;
                    }
                    if (item.status == 1)
                    {
                        continue;

                    }
                    RadioButton TabButto = TabButton(item.name);
                    TabButto.DataContext = item;
                    TabButto.Click += TabButto_Click;
                    RightTabPanel.Children.Add(TabButto);
                   

                }
            }catch (Exception ex)
            {
                Logger.Error(typeof (AlarmPageViewModel),"LoadRightTab:"+ex.Message);
            }
        }

        private void SetMainRedioBt()
        {
            try
            {
                ScenesList[] sceneslist = ScenesList.Where(x => x.status == 1).ToArray();
                if (sceneslist.Length == 0)
                {
                    return;
                }
                ScenesList scenes = sceneslist[0];
                RadioButton TabButto = TabButton(scenes.name);
                TabButto.DataContext = scenes;
                TabButto.Click += TabButto_Click;
                TabButto.IsChecked = true;
                RightTabPanel.Children.Add(TabButto);
            }catch (Exception ex)
            {
                Logger.Error(typeof(AlarmPageViewModel), "SetMainRedioBt:" + ex.Message);
            }
        }


        private void TabButto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radio = (RadioButton)sender;
                ScenesList scenes = (ScenesList)radio.DataContext;
                CurrentScenes = scenes;
                if (scenes.status != 1)
                {
                    //LoadAlarmMain();
                    LoadAlarmTracker(scenes);
                    HiddenTab();
                }
                else
                {
                    LoadAlarmMain();
                    ShowTab();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(AlarmPageViewModel), "TabButto_Click:" + ex.Message);
            }

        }

        private RadioButton TabButton(string name)
        {
            string GetName = "\n";
            char[] nameAr = name.ToArray();
            foreach (var item in nameAr)
            {
                GetName += item + "\n";
            }
            Label label = new Label
            {
                FontSize=16,
                Content = GetName,
                Foreground =Brushes.White,
                VerticalContentAlignment =VerticalAlignment.Center ,
                Width = 40,
                HorizontalContentAlignment = HorizontalAlignment.Center ,
            };
            RadioButton radio = new RadioButton()
            {
                /*Height = 75,*/
                Width=50,
                VerticalAlignment =VerticalAlignment.Top ,
                Content = label,
                ToolTip = name,
                Style = Application.Current.FindResource("AlarmRadioButtonStyle2") as Style,
            };

            return radio;
        }

        public void LoadTabControl()
        {
            GetAlarmInfo(PageType);
            if (_tabControlWin!=null)
            {
                _tabControlWin.Close();
            }
            _mainWin = (MainWindow)Application.Current.MainWindow;
            _tabControlWin = new TabControlWin()
            {
                Owner = _mainWin,
                Topmost = true,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = 0,
                Top = 53,
                DataContext =this
            };
            _tabControlWin.Show();
            
        }

        private void ShowTab()
        {
            if (_tabControlWin != null)
            {
                _tabControlWin.Visibility = Visibility.Visible;
            }
        }
        private void HiddenTab()
        {
            if (_tabControlWin != null)
            {
                _tabControlWin.Visibility = Visibility.Collapsed;
            }

        }

        private void UnLoad(object obj)
        {
            mainWindowViewModel.InitSubWin();
            GlobalVariable.IsAlarmPage = false;
            _tabControlWin.Close();
            LoadAlarmMain();
            MapDic.Clear();
            MainFrame.Content =null;

            //客户端下墙
            PinkongHelper.ClientDownWall();
        }

        /// <summary>
        /// 主场景
        /// </summary>
        /// <param name="obj"></param>
        private void AlarmMain(object obj)
        {
            LoadAlarmMain();
        }

        public AlarmMainPage alarmMainPage { get; set; }
        private void LoadAlarmMain()
        {
            alarmMainPage = null;
            alarmMainPage = new AlarmMainPage();
            MainFrame.NavigationService.Navigate(alarmMainPage);
            MainFrame.NavigationService.RemoveBackEntry();
        }
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="obj"></param>
        private void AlarmTracker(object obj)
        {
           // LoadAlarmTracker();
        }

        AlarmTrackPage alarmTrack;
        private void LoadAlarmTracker(ScenesList scenes)
        {
            alarmTrack = new AlarmTrackPage();
            MainFrame.NavigationService.Navigate(alarmTrack);
            MainFrame.NavigationService.RemoveBackEntry();
            alarmTrack.LoadContant(scenes);
        }


        


        private void InitSubAlarm(int posion, ScenesList scenes)
        {
            try
            {

                List<AuxiliaryWindow> auxiliaries = mainWindowViewModel.SubWindows;
                AuxiliaryWindow auxiliary = auxiliaries[posion - 2];
                auxiliary.panel.Children.Clear();
                Frame frame = new Frame();
                auxiliary.panel.Children.Add(frame);
                AlarmTrackPage alarmpage = new AlarmTrackPage();
                frame.NavigationService.Navigate(alarmpage);
                alarmpage.SubWinIndex = posion - 2;
                alarmpage.LoadContant(scenes);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 返回主页
        /// </summary>
        /// <param name="obj"></param>
        private void MainPageReturn(object obj)
        {
            ReturnMainPage();
        }

        public void ReturnMainPage()
        {
            mainWindowViewModel.LoadMainPage();
        }
    }
}
