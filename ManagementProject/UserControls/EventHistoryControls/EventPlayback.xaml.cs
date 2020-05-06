using ManagementProject.Model;
using NetSDKCS;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.EventHistoryControls
{
    /// <summary>
    /// EventPlayback.xaml 的交互逻辑
    /// </summary>
    public partial class EventPlayback : UserControl
    {

        private System.Windows.Forms.PictureBox PlayWin;
        public IntPtr mplayHandle;
        public IntPtr windowhandle { get; set; }
        public int Channel { get; set; }
        public int Index;
        public EventPlayback()
        {
            InitializeComponent();
            PlayWin = playWindow;
            windowhandle = PlayWin.Handle;
        }
    } 
}
