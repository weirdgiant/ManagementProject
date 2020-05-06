using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ManagementProject.UserControls
{
    /// <summary>
    /// 自定义Popup,结合ToggleButton使用
    /// </summary>
    public class MyPopup : Popup
    {
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var isOpen = IsOpen;
            base.OnPreviewMouseLeftButtonDown(e);

            if (isOpen && !IsOpen)
                e.Handled = true;
        }
    }
}
