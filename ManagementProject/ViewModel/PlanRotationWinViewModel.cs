using ManagementProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace ManagementProject.ViewModel
{

    class PlanRotationWinViewModel : PlanRotationWinModel
    {
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand SaveRotationCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand NextCommand { get; set; }

        public PlanRotationWinViewModel()
        {
            //Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            PlanRotation = new ObservableCollection<PlanRotation>();

            PlanRotation.Add(new PlanRotation
            {
                PRDuration = 10,
                PRName = "先材楼及周边",
                PRScenesName = "先材楼1",
                IsShowNext = true,
                CanDelete = false
                //PRTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });

            CloseWinCommand = new DelegateCommand { ExecuteCommand = new Action<object>(CloseWin) };
            SaveRotationCommand = new DelegateCommand { ExecuteCommand = new Action<object>(SaveRotation) };
            CancelCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Cancel) };
            DeleteCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Delete) };
            NextCommand = new DelegateCommand { ExecuteCommand = new Action<object>(Next) };
        }

        private void Delete(object obj)
        {
            #region Button Click Event
            //var button = (Button)obj;
            //button.Click += (s, e) =>
            //{
            //    //设置girid的选中元素为Button所在行的元素
            //    var dataContext = ((Button)s).DataContext;
            //    PlanRotation.Remove((PlanRotation)dataContext);
            //};
            #endregion

            var button = (Button)obj;

            var dataContext = button.DataContext;
            var rotation = (PlanRotation)dataContext;

            //MessageBox.Show(rotation.PRTime);

            button.IsEnabled = !rotation.IsShowNext;

            PlanRotation.Remove(rotation);
        }

        private void Next(object obj)
        {
            var button = (Button)obj;
            var dataContext = button.DataContext;
            var rotation = (PlanRotation)dataContext;

            var duration = Duration;
            var time = rotation.PRTime;

            //rotation.PRTime
            //var count = PlanRotation.Count;

            if (string.IsNullOrEmpty(Time.ToString()) ||
                string.IsNullOrEmpty(Scenes))
            {
                MessageBox.Show("请先将信息填写完整", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //foreach (var item in PlanRotation)
            //{
            //    Console.WriteLine($"{item.PRDuration} ,{item.PRName} ,{item.PRScenesName} ,{item.PRTime}");

            //    Console.WriteLine("------------------");
            //}

            PlanRotation.Insert(0, new PlanRotation
            {
                PRDuration = Duration,
                PRName = RotationName,
                PRScenesName = Scenes,
            });
        }

        private static void AddList(ListView lv)
        {
            StackPanel panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(20, 0, 0, 0) };

            #region 动态创建控件
            TextBlock tbTime = new TextBlock
            {
                Text = "时间：",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            };

            UserControls.TimeControl.DateTimePicker timePicker = new UserControls.TimeControl.DateTimePicker()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 28,
                Width = 152,
                Foreground = Brushes.White,
                Background = Brushes.Black,
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFABADB3")),
                BorderThickness = new Thickness(1)
            };

            timePicker.SetBinding(UserControls.TimeControl.DateTimePicker.TagProperty, new Binding() { Path = new PropertyPath("Time"), Mode = BindingMode.OneWayToSource });

            TextBlock tbScenes = new TextBlock
            {
                Text = "场景：",
                Margin = new Thickness(10, 5, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold
            };

            ComboBox combo = new ComboBox
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 80,
                Height = 21,
            };
            combo.SetBinding(ComboBox.TextProperty, new Binding() { Path = new PropertyPath("Scenes"), Mode = BindingMode.OneWayToSource });//, Mode = BindingMode.OneWayToSource
            combo.Items.Add(1);
            combo.Items.Add(2);

            Button btnDelete = new Button
            {
                Content = new System.Windows.Controls.Image
                {
                    Height = 20,
                    Width = 20,
                    Margin = new Thickness(10, 0, 10, 0),
                    Source = new BitmapImage(new Uri("/ManagementProject;component/ImageSource/Icon/FightIcon/delete_black.png", UriKind.RelativeOrAbsolute))
                },
                Style = Application.Current.FindResource("GelButton") as Style
            };

            //deleteBinding.Source = PlanRotationWinViewModel;  // view model?

            btnDelete.SetBinding(Button.CommandProperty, new Binding { Path = new PropertyPath("DeleteCommand") });
            btnDelete.SetBinding(Button.CommandParameterProperty, new Binding() { RelativeSource = new RelativeSource { AncestorType = typeof(ListView), Mode = RelativeSourceMode.FindAncestor } });

            Button btnNext = new Button
            {
                Width = 55,
                Height = 30,
                Content = "下一个",
                Style = Application.Current.FindResource("FilletButton") as Style,
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF009FFF"))
            };
            btnNext.Click += (s, e) =>
            {
                //btnNext.Visibility = Visibility.Collapsed;
            };
            btnNext.SetBinding(Button.CommandProperty, new Binding { Path = new PropertyPath("NextCommand") });
            btnNext.SetBinding(Button.CommandParameterProperty, new Binding() { RelativeSource = new RelativeSource { AncestorType = typeof(ListView), Mode = RelativeSourceMode.FindAncestor } });

            panel.Children.Add(tbTime);
            panel.Children.Add(timePicker);
            panel.Children.Add(tbScenes);
            panel.Children.Add(combo);
            panel.Children.Add(btnDelete);
            panel.Children.Add(btnNext);

            lv.Items.Add(panel);
            #endregion
        }

        /// <summary>
        /// 保存轮训
        /// </summary>
        /// <param name="obj"></param>
        private void SaveRotation(object obj)
        {
            if (string.IsNullOrEmpty(Duration.ToString())||Duration<=0)
            {
                MessageBox.Show("请检查轮询时间是否合法！");
                return;
            }

            //var lv = (ListView)obj;
            int i = 0;

            Console.WriteLine("---------开始---------");
            foreach (var item in PlanRotation)
            {
                i++;
                Console.WriteLine($"{item.PRDuration} ,{item.PRName} ,{item.PRScenesName} ,{item.PRTime}");
            }

            Console.WriteLine(i);

            Console.WriteLine("--------结束----------");

            //AddList(lv);
            //if (string.IsNullOrEmpty())
            //{
            //    MessageBox.Show($"请先输入计划轮训名称", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //MessageBox.Show($"ScenesName:{RotationName}");
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
}
