using ManagementProject.Converters;
using ManagementProject.Model;
using MangoApi;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ManagementProject.ViewModel
{
    public class ChartPageViewModel : ChartPageModel
    {
        private MainWindow _mainWin = (MainWindow)System.Windows.Application.Current.MainWindow;
        private MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand EnterMapCommand { get; set; }
        public DelegateCommand DeviceInfoCommand { get; set; }
        public DelegateCommand ExportExcelCommand { get; set; }

        public ChartPageViewModel()
        {

            mainWindowViewModel = (MainWindowViewModel)_mainWin.DataContext;
            EnterMapCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(EnterMap) };
            DeviceInfoCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(DeviceInfo) };
            ExportExcelCommand = new DelegateCommand() { ExecuteCommand = new Action<object>(ExportToExcel) };

            InitErrorDevice(GlobalVariable.CurrentSid);
            InitEventCount(GlobalVariable.CurrentSid);
            AlarmHistory = new ObservableCollection<History>();
            //ErrorDeviceList = new ObservableCollection<ErrorDeviceType>();
            InitHistory();
        }

        private async void ExportToExcel(object obj)
        {
            if (obj == null)
                return;

            try
            {
                ObservableCollection<ErrorDeviceType> errorDeviceType = (ObservableCollection<ErrorDeviceType>)obj;

                if (errorDeviceType.Count == 0)
                    return;

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx;*.xls",
                    RestoreDirectory = true,
                    FileName = $"故障设备清单{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                };

                if (sfd.ShowDialog() == true)
                {
                    var result = await ExportExcel(errorDeviceType, sfd.FileName);
                    MessageBox.Show($@"导出成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }

        private async Task<bool> ExportExcel(ObservableCollection<ErrorDeviceType> errorDeviceType, string filePath)
        {
            return await Task.Run(() =>
            {

                string localFilePath = filePath; //获得文件路径 
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook excelWB = excelApp.Workbooks.Add(System.Type.Missing);    //创建工作簿（WorkBook：即Excel文件主体本身）
                Worksheet excelWS = (Worksheet)excelWB.Worksheets[1];   //创建工作表（即Excel里的子表sheet） 1表示在子表sheet1里进行数据导出


                #region 表格属性设置
                var range = excelWS.get_Range("A1", "E1"); //获取Excel多个单元格区域：本例做为Excel表头  
                range.Font.Size = 15;
                range.Font.Name = "宋体"; //设置字体的种类 
                #endregion

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("设备分类", typeof(string));
                dt.Columns.Add("设备类型", typeof(string));
                dt.Columns.Add("设备名称", typeof(string));
                dt.Columns.Add("故障时间", typeof(string));
                dt.Columns.Add("地点", typeof(string));

                foreach (var item in errorDeviceType)
                {
                    System.Data.DataRow row = dt.NewRow();
                    row["设备分类"] = item.deviceType;
                    row["设备类型"] = item.deviceClass;
                    row["设备名称"] = item.deviceName;

                    if (!string.IsNullOrEmpty(item.errorAlarmTime))
                        row["故障时间"] = Convert.ToDateTime(item.errorAlarmTime).ToString("yyyy-MM-dd HH:mm:ss");

                    row["地点"] = item.locationAttribute;
                    dt.Rows.Add(row);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        excelWS.Cells[1, j + 1] = dt.Columns[j].ColumnName;
                        excelWS.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();   //Excel单元格第一个从索引1开始
                    }
                }

                excelWB.SaveAs(localFilePath);  //将其进行保存到指定的路径
                excelWB.Close();
                excelApp.Quit();  //KillAllExcel(excelApp); 释放可能还没释放的进程

                return true;
            });
        }

        private void DeviceInfo(object obj)
        {
            try
            {
                ErrorDeviceType tb = (ErrorDeviceType)obj;
                DeviceClass = tb.deviceClass;
                DeviceClassName = tb.deviceName;
                DeviceClassCount = tb.count.ToString();
                ErrorCount = tb.errorCount.ToString();
                InitErrorDeviceType(GlobalVariable.CurrentSid, DeviceClass);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitErrorDeviceType(int sid, string type)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorDeviceTypeCount;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceClassCount(sid, type, url);
                ErrorDeviceType = new ObservableCollection<ErrorDeviceType>(ret);
            }
            catch (Exception)
            {
            }
        }

        private void EnterMap(object obj)
        {
            mainWindowViewModel.LoadMainPage();
        }

        private void InitErrorDevice(int sid)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetErrorDeviceCount;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceCount(sid, url);
                ErrorDevice = new ObservableCollection<DeviceClass>();
               
                foreach (var item in ret)
                {
                    if (string.IsNullOrEmpty(item.deviceClass)&&string.IsNullOrEmpty(item.deviceName))
                    {
                        continue;
                    }

                    ErrorDevice.Add(new DeviceClass
                    {
                        deviceName = item.deviceName.Trim(),//增加去空格处理
                        count = item.count,
                        errorCount = item.errorCount,
                        deviceClass = item.deviceClass,
                        DeviceClassIcon = $"/ManagementProject;component/ImageSource/Icon/Home/HomeClassIcon/{item.deviceName.Trim()}.png",
                        ErrorCountForeground = (item.errorCount == 0) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009fff")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4141")),
                    });

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }



        public void InitHistory()
        {
            string EventStartTime = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string EventEndTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            AlarmFilter alarmFilter = new AlarmFilter();
            alarmFilter.altimeStart = DateTime.Now.Date.ToString();
            alarmFilter.altimeStop = DateTime.Now.ToString();
            alarmFilter.sid = 0;
            SearchHistory(alarmFilter);

        }

        private void InitEventCount(int sid)
        {
            try
            {
                string url = AppConfig.ServerBaseUri + AppConfig.GetEventCount;
                ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceCount(sid, url);

                CurrentMonthCount = ret.Where(x => x.dateName == "month").ToArray()[0].count.ToString();
                CurrentYearCount = ret.Where(x => x.dateName == "year").ToArray()[0].count.ToString();
            }
            catch (Exception ex)
            {

            }
        }

        private void InitPieCount(int sid)
        {
            string url = AppConfig.ServerBaseUri + AppConfig.GetEventByType;
            ErrorDeviceType[] ret = HttpAPi.GetErrorDeviceCount(sid, url);
            //pieCount

        }

        private void SearchHistory(AlarmFilter alarmFilter)
        {
            try
            {

                AlarmHistory.Clear();
                string url = AppConfig.ServerBaseUri + AppConfig.GetAllAlarmInfo;
                Alarm[] alarms = HttpAPi.GetAllAlarmInfo(url, alarmFilter);
                if (alarms != null)
                {
                    foreach (var item in alarms)
                    {
                        if (!GlobalVariable.AlarmMapList.Contains(item.mapid.ToString()))
                        {
                            continue;
                        }
                        ChartHistory history = new ChartHistory();
                        history.Id = item.id;
                        history.Level = item.level.ToString();
                        history.Location = item.location;
                        history.State = GetAlarmState(item.state).ToString();
                        history.Time = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.altime)).ToString("HH:mm:ss");
                        history.Type = item.flagVal;
                        history.Device = item.sersorname;

                        if (history.State.Equals("未处理"))
                        {
                            history.TodayLogFill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dc9052"));
                            history.StateForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff4141"));
                        }
                        else
                        {
                            history.TodayLogFill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#03894c"));
                            history.StateForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5ef47"));//f5ef47 //ff4141 未处理
                        }

                        AlarmHistory.Add(history);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string GetAlarmState(com.mango.protocol.Enum.ALARM_STATE STATE)
        {
            switch (STATE)
            {
                case com.mango.protocol.Enum.ALARM_STATE.NEW:
                    return "未处理";
                case com.mango.protocol.Enum.ALARM_STATE.ADEVICE_ERROR:
                    return "设备故障";
                case com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_MISTAKE:
                    return "确认误报";
                case com.mango.protocol.Enum.ALARM_STATE.CONFIRMED_AS_REAL:
                    return "真实报警";
                case com.mango.protocol.Enum.ALARM_STATE.FIXED:
                    return "自动处理";
                default:
                    return "模拟报警";
            }
        }


       
    }
}
