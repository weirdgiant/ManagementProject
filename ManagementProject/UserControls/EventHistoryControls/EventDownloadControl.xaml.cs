using NetSDKCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManagementProject.UserControls.EventHistoryControls
{
    /// <summary>
    /// EventDownloadControl.xaml 的交互逻辑
    /// </summary>
    public partial class EventDownloadControl : UserControl
    {
        public EventDownloadControl()
        {
            InitializeComponent();
        }
    }
    public class EventDownloadControlModel:INotifyPropertyChangedClass
    {
        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                NotifyPropertyChanged("FileName");
            }
        }

        private double pbValue;

        public double PbValue
        {
            get { return pbValue; }
            set
            {
                pbValue = value;
                NotifyPropertyChanged("PbValue");
                //IsShowFolder = (PbValue == pbMax);
            }
        }

        private double pbMax = 100;

        public double PbMax
        {
            get { return pbMax; }
            set
            {
                pbMax = value;
                NotifyPropertyChanged("PbMax");
            }
        }

        private bool isShowFolder;

        public bool IsShowFolder
        {
            get { return isShowFolder; }
            set
            {
                isShowFolder = value;
                NotifyPropertyChanged("IsShowFolder");
                if (IsShowFolder)
                {
                    IsShowProgressBar = !IsShowFolder;
                }
            }
        }

        private bool isShowProgressBar;

        public bool IsShowProgressBar
        {
            get { return isShowProgressBar; }
            set
            {
                isShowProgressBar = value;
                NotifyPropertyChanged("IsShowProgressBar");
                if (IsShowProgressBar)
                {
                    IsShowFolder = !IsShowProgressBar;
                }
            }
        }
    }
    public class EventDownloadControlViewModel: EventDownloadControlModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Chanel { get; set; }
        private fTimeDownLoadPosCallBack m_DownloadPosCallBack { get; set; }
        private const int DOWNLOAD_END = -1;
        private const int DOWNLOAD_FAILED = -2;
        private IntPtr m_DownloadID { get; set; } = IntPtr.Zero;
        public DelegateCommand OpenFileCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public EventDownloadControlViewModel()
        {
            OpenFileCommand = new DelegateCommand();
            OpenFileCommand.ExecuteCommand = new Action<object>(OpenFile);
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            IsShowProgressBar = false ;
            IsShowFolder = true;
            m_DownloadPosCallBack = new fTimeDownLoadPosCallBack(DownLoadPosCallBack);
        }
        private void OpenFile(object obj)
        {
            try
            {
                string path = ".\\EventDownload\\".Trim('.').Trim('\\'); ;
                System.Diagnostics.Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory + path);
            }catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Loaded(object obj)
        {
            string fileName = $"{Chanel}_{StartTime:yyyyMMddHHmmssffff}_{EndTime:yyyyMMddHHmmssffff}";
            string path = ".\\EventDownload\\".Trim('.').Trim('\\');
            string FileName = AppDomain.CurrentDomain.BaseDirectory + path + "\\" + fileName + ".dav";

            if (!System.IO.File.Exists(FileName))
            {
                IsShowProgressBar = true  ;
                IsShowFolder = false ;

                NVRSDKManager.Instance.SetDeviceModel(1, EM_RECORD_TYPE.ALL);
                try
                {
                    m_DownloadID = NETClient.DownloadByTime(NVRSDKManager.Instance.m_LoginHandle, Chanel, EM_QUERY_RECORD_TYPE.ALL, StartTime, EndTime, FileName, m_DownloadPosCallBack, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero);
                    if (IntPtr.Zero == m_DownloadID)
                    {
                        MessageBox.Show(NETClient.GetLastError());
                        return;
                    }
                }
                catch (NETClientExcetion netEx)
                {
                    MessageBox.Show(netEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public void SetProgressBar(double value)
        {
            if (m_DownloadID != IntPtr.Zero)
            {
                if (DOWNLOAD_END == value)
                {
                    PbValue = 100;
                    NETClient.StopDownload(m_DownloadID);
                    IsShowProgressBar = false;
                    IsShowFolder = true;
                }
                //显示进度条
                PbValue = value;
                if (PbValue == 100)
                {
                    IsShowProgressBar = false;
                    IsShowFolder = true;
                }
                //显示百分比
                string LabValue = Convert.ToInt64((value / PbMax) * 100) + "%";
            }
        }


        private void DownLoadPosCallBack(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize, int index, NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser)
        {
            if (lPlayHandle == m_DownloadID)
            {
                int value = 0;
                if (DOWNLOAD_END == (int)dwDownLoadSize)
                {
                    value = DOWNLOAD_END;
                }
                else if (DOWNLOAD_FAILED == (int)dwDownLoadSize)
                {
                    value = DOWNLOAD_FAILED;
                }
                else
                {
                    value = (int)(dwDownLoadSize * 100 / dwTotalSize);
                }
                SetProgressBar(value);
            }
        }
    }
}
