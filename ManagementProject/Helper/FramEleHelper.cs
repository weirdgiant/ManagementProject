using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementProject.Helper
{
    public class FramEleHelper
    {
        /// <summary>
        /// 获取父控件  调用方法：Grid layoutGrid = VTHelper.GetParentObject<Grid>(this.spDemoPanel, "LayoutRoot");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetParentObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                if (parent is T && (((T)parent).Name == name || string.IsNullOrEmpty(name)))
                {
                    return (T)parent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }


        /// <summary>
        /// 获取子控件  调用方法：StackPanel sp = VTHelper.GetChildObject<StackPanel>(this.LayoutRoot, "spDemoPanel");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取所有子控件
        /// 调用方法：List<TextBlock> textblock = VTHelper.GetChildObjects<TextBlock>(this.LayoutRoot, "");
        /// 在表格中的控件：List<TextBox> listTextBox = GetChildObjects<TextBox>(radGridView_File.ItemContainerGenerator.ContainerFromItem(radGridView_File.SelectedItems[0]), "");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();
            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (((T)child).Name == name || string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, ""));
            }
            return childList;
        }


        /// <summary>
        /// Set the current control to the top
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="userControl">父控件</param>
        public static void SetControlToTop(Panel panel, Control userControl)
        {
            //Button bt = ui as Button;
            //Canvas parent = bt.Parent as Canvas;

            if (panel != null)
            {
                IEnumerable<UIElement> uiE = panel.Children.OfType<UIElement>().Where(x => x != userControl);//枚举类型定义

                if (uiE.Count() > 0)//判断 除去用户选择的控件，是否还有其他控件。
                {
                    var maxZ = uiE.Select(x => Panel.GetZIndex(x)).Max();
                    Panel.SetZIndex(userControl, maxZ + 1);//置于最顶层
                }
            }
        }
    }
}
