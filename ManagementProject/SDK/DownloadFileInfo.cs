using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace ManagementProject
{
    /************************************************************************/
    /*  录像下载类
     *  保存录像下载相关信息*/
    /************************************************************************/
    public class DownloadFileInfo:INotifyPropertyChanged
    {
        private int downloadPercent;
        /*录像名称*/
        public string vodName { set; get; }
        /*录像开始时间*/
        public DateTime startTime { set; get; }
        /*录像结束时间*/
        public DateTime endTime { set; get; }
        /*录像下载进度*/
        public int DownloadPercent {
            get
            {
                return downloadPercent;

            }
            set
            {
                if(value!= downloadPercent)
                {
                    this.downloadPercent = value;
                    NotifyPrpertyChanged();
                }
            }
        }
        /*录像是否在下载*/
        public bool isDownloading = false;
        /*下载通道号（每个下载唯一指定ID）*/
        public byte[] downloadID;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPrpertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
