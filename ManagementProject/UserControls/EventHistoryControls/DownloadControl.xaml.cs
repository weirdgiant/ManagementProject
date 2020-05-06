using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace ManagementProject.UserControls.EventHistoryControls
{
    /// <summary>
    /// DownloadControl.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadControl : UserControl
    {
        public DownloadControl()
        {
            InitializeComponent();
            DataContext = new DownloadControlViewModel();
        }
    }
    public class DownloadControlViewModel : DownloadControlModel
    {
        private string httpFileUrl = "https://download.microsoft.com/download/2/4/3/24375141-E08D-4803-AB0E-10F2E3A07AAA/AccessDatabaseEngine.exe";

        private static readonly string baseDic = AppDomain.CurrentDomain.BaseDirectory;

        private string saveUrl = $"{baseDic}报警时间_报警类型_报警源名称.mp4";//报警时间_报警类型_报警源名称.mp4

        public DelegateCommand DownloadCommand { get; set; }
        public DelegateCommand OpenFileCommand { get; set; }

        public DownloadControlViewModel()
        {
            var hasPath =  ExistsFile(saveUrl);
            if (hasPath)
                IsShowFolder = true;
            else
                IsShowDownloadButton = true;

            DownloadCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(DownloadFile) };
            OpenFileCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(OpenFile) };
        }

        private bool ExistsFile(string saveUrl)
        {
            return File.Exists(saveUrl);
        }

        private void OpenFile(object obj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "视频文件 (*.mp4)|*.mp4",
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
               var fileName = openFileDialog.FileName;
            }
        
        }

        private void DownloadFile(object obj)
        {
            if (HttpFileExist(httpFileUrl))
            {
                IsShowProgressBar=true;
                DateTime start = DateTime.Now.AddDays(-2);
                DateTime end = DateTime.Now.AddDays(-1);
                if (!NVRSDKManager.Instance.QueryPlayBack(start, end,2))
                {
                    MessageBox.Show("当前时间点暂无回放！请稍后查看！");
                    return;
                }
                NVRSDKManager.Instance.Download(2, start, end);
                // DownloadHttpFile(httpFileUrl, saveUrl);
            }
        }

        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        public void DownloadHttpFile(String http_url, String save_url)
        {
            WebResponse response = null;
            //获取远程文件
            WebRequest request = WebRequest.Create(http_url);
            response = request.GetResponse();
            if (response == null) return;
            //读远程文件的大小
            PbMax = response.ContentLength;
            //下载远程文件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                using (Stream netStream = response.GetResponseStream())
                {
                    try
                    {
                        using (Stream fileStream = new FileStream(save_url, FileMode.Create))
                        {
                            byte[] read = new byte[1024];
                            long progressBarValue = 0;
                            int realReadLen = netStream.Read(read, 0, read.Length);
                            while (realReadLen > 0)
                            {
                                fileStream.Write(read, 0, realReadLen);
                                progressBarValue += realReadLen;
                                SetProgressBar(progressBarValue);
                                realReadLen = netStream.Read(read, 0, read.Length);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(typeof(DownloadControlViewModel), ex.Message);
                    }
                }
            }, null);
        }

        public void SetProgressBar(double value)
        {
            //显示进度条
            PbValue = value;
            //显示百分比
            LabValue = Convert.ToInt64((value / PbMax) * 100) + "%";
        }
    }

    public class DownloadControlModel : INotifyPropertyChangedClass
    {
        private string labValue;

        public string LabValue
        {
            get { return labValue; }
            set
            {
                labValue = value;
                NotifyPropertyChanged("LabValue");
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
                IsShowFolder = (PbValue == pbMax);
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
                    IsShowProgressBar = IsShowDownloadButton = !IsShowFolder;
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
                    IsShowFolder = IsShowDownloadButton = !IsShowProgressBar;
                }
            }
        }

        private bool isShowDownloadButton;

        public bool IsShowDownloadButton
        {
            get { return isShowDownloadButton; }
            set
            {
                isShowDownloadButton = value;
                NotifyPropertyChanged("IsShowDownloadButton");
                if (IsShowDownloadButton)
                {
                    IsShowProgressBar = IsShowFolder = !IsShowDownloadButton;
                }
            }
        }

    }
}
