using ManagementProject.Converters;
using ManagementProject.Model;
using ManagementProject.UserControls.TrafficRecord;
using MangoApi;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ManagementProject.ViewModel
{
    public class TrafficRecordViewModel:TrafficRecordModel 
    {
        private Grid MyPanel { get; set; }
        private TrafficList _trafficList { get; set; } = new TrafficList();
        private ParkingList _parkingList { get; set; } = new ParkingList();
        private OverSpeedList _overSpeedList { get; set; } = new OverSpeedList();
        private Alarm[] CaraAlarms { get; set; }
        public DelegateCommand LoadedCommand { get; set; }
        public DelegateCommand CloseWinCommand { get; set; }
        public DelegateCommand MaxWinCommand { get; set; }
        public DelegateCommand SearchRecordCommand { get; set; }
        public DelegateCommand TrafficListCommand { get; set; }
        public DelegateCommand ParkingListCommand { get; set; }
        public DelegateCommand OverSpeedListCommand { get; set; }
        public DelegateCommand SearchParkingCommand { get; set; }
        public DelegateCommand SearchOverSpeedCommand { get; set; }
        public DelegateCommand ExportTrafficListCommand { get; set; }
        public DelegateCommand ExportParkingListCommand { get; set; }
        public DelegateCommand ExportOverSpeedListCommand { get; set; }
        public DelegateCommand ShowPictureCommand { get; set; }
        public TrafficRecordViewModel()
        {
            CommandInit();
            TrafficRecordList = new ObservableCollection<AlarmTraffic>();
            OverSpeedCar = new ObservableCollection<AlarmCarInfo>();
            OverSpeedDevice = new ObservableCollection<AlarmCarInfo>();
            ParkingDevice = new ObservableCollection<AlarmCarInfo>();
            ParkingCar = new ObservableCollection<AlarmCarInfo>();
            string[] signalList = {"所有的", "车辆违停", "车辆超速" };
            AlarmSignalList = new ObservableCollection<string>(signalList);
            AlarmSignal = "所有的";
        }
        private void CommandInit()
        {
            LoadedCommand = new DelegateCommand();
            LoadedCommand.ExecuteCommand = new Action<object>(Loaded);
            CloseWinCommand = new DelegateCommand();
            CloseWinCommand.ExecuteCommand = new Action<object>(CloseWin);
            MaxWinCommand = new DelegateCommand();
            MaxWinCommand.ExecuteCommand = new Action<object>(MaxWin);
            SearchRecordCommand = new DelegateCommand();
            SearchRecordCommand.ExecuteCommand = new Action<object>(SearchRecord);
            TrafficListCommand = new DelegateCommand();
            TrafficListCommand.ExecuteCommand = new Action<object>(TrafficList);
            ParkingListCommand = new DelegateCommand();
            ParkingListCommand.ExecuteCommand = new Action<object>(ParkingList);
            OverSpeedListCommand = new DelegateCommand();
            OverSpeedListCommand.ExecuteCommand = new Action<object>(OverSpeedList);
            SearchParkingCommand = new DelegateCommand();
            SearchParkingCommand.ExecuteCommand = new Action<object>(SearchParking);
            SearchOverSpeedCommand = new DelegateCommand();
            SearchOverSpeedCommand.ExecuteCommand = new Action<object>(SearchOverSpeed);
            ExportTrafficListCommand = new DelegateCommand();
            ExportTrafficListCommand.ExecuteCommand = new Action<object>(ExportTrafficList);
            ExportParkingListCommand = new DelegateCommand();
            ExportParkingListCommand.ExecuteCommand = new Action<object>(ExportParkingList);
            ExportOverSpeedListCommand = new DelegateCommand();
            ExportOverSpeedListCommand.ExecuteCommand = new Action<object>(ExportOverSpeedList);
            ShowPictureCommand = new DelegateCommand();
            ShowPictureCommand.ExecuteCommand = new Action<object>(ShowPicture);

        }
        private async void ShowPicture(object obj)
        {
            TextBlock bt=(TextBlock)obj;
            string url = bt.Tag.ToString();
            if (url == "") return;
            BitmapImage image = await HttpAPi.LoadImage(url);
            Window imageWin = new Window();
            imageWin.Topmost = true;
            Image img = new Image();
            img.Source = image;
            imageWin.Content = img;
            imageWin.ShowDialog();
        }
        private void Loaded(object obj)
        {
            TrafficWin _win = (TrafficWin)obj;
            MyPanel = _win.panel;
            _trafficList.DataContext = this;
            MyPanel.Children.Add(_trafficList);
            InitHistory();
        }


        private void InitHistory()
        {
            
            StartTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            EndTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            ParkingStartTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            ParkingEndTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            OverSpeedStartTime = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            OverSpeedEndTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
            AlarmFilter alarmFilter = new AlarmFilter();
            alarmFilter.altimeStart ="";
            alarmFilter.level = "";
            alarmFilter.state = "";
            alarmFilter.altimeStop = "";
            alarmFilter.flag ="CAR";
            alarmFilter.sid = 0 ;

            string url = AppConfig.ServerBaseUri + AppConfig.GetAllAlarmInfo;
            CaraAlarms = HttpAPi.GetAllAlarmInfo(url, alarmFilter);
            CaraAlarms = CaraAlarms.Where(x=> GlobalVariable.AlarmMapList.Contains(x.mapid.ToString())).ToArray();

        }

        private void SearchOverSpeed()
        {
            OverSpeedCar.Clear();
            OverSpeedDevice.Clear();
            if (DateTime.Parse(OverSpeedStartTime) > DateTime.Parse(OverSpeedEndTime))
            {
                return;
            }
            Alarm[] alarms = SelectedOverSpeedList();
            GetDeviceList(alarms,OverSpeedDevice);
            GetCarList(alarms, OverSpeedCar);
        }

        private Alarm[] SelectedOverSpeedList()
        {
            //1-违停;2-超速;
            long start = TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(OverSpeedStartTime));
            long end = TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(OverSpeedEndTime));
            Alarm[] alarms;
            alarms = CaraAlarms.Where(x => x.type == 2).ToArray();
            alarms = alarms.Where(x=>long.Parse(x.altime)<= end).ToArray ();
            alarms = alarms.Where(x => long.Parse(x.altime) >= start).ToArray();

            return alarms;
        }

        private void SearchParking()
        {
            ParkingCar.Clear();
            ParkingDevice.Clear();
            if (DateTime.Parse(ParkingStartTime) > DateTime.Parse(ParkingEndTime))
            {
                return;
            }
            Alarm[] alarms = SelectedParkingList();
            GetDeviceList(alarms,ParkingDevice);
            GetCarList(alarms,ParkingCar);
        }
        /// <summary>
        /// 报警设备TOP10
        /// </summary>
        /// <param name="alarms"></param>
        /// <param name="list"></param>
        private void GetDeviceList(Alarm[] alarms, ObservableCollection<AlarmCarInfo> list)
        {
            var qry = from s in alarms
                      group s by s.sersor into ws
                      orderby ws.Count() descending
                      select new
                      {
                          Num = ws.Count(),
                          Code = ws.Key,
                          alarmList = ws.ToArray(),
                          
                     };
            int count = 0;
            foreach (var item in qry)
            {
                if (count==10)
                {
                    break;
                }
                AlarmCarInfo carInfo = new AlarmCarInfo();
                carInfo.DeviceCode = item.Code;
                carInfo.AlarmDevice = item.alarmList[0].sersorname;
                carInfo.AlarmCount = item.Num.ToString();
                carInfo.Location = item.alarmList[0].location;
                carInfo.DeviceType = "-";
                list.Add(carInfo);
                count++;
                
;            }
        }
        /// <summary>
        /// 报警车牌TOP10
        /// </summary>
        /// <param name="alarms"></param>
        /// <param name="list"></param>
        private void GetCarList(Alarm[] alarms, ObservableCollection<AlarmCarInfo> list)
        {
            var qry = from s in alarms
                      group s by  JsonConvert.DeserializeObject<AlarmCar>(s.peculiarnote).Plate into ws
                      orderby ws.Count() descending
                      select new
                      {
                          Num = ws.Count(),
                          CarNumber = ws.Key,
                          alarmList = ws.ToArray(),

                      };
            int count = 0;
            foreach (var item in qry)
            {
                if (count == 10)
                {
                    break;
                }
                AlarmCarInfo carInfo = new AlarmCarInfo();
                carInfo.CarNumber = item.CarNumber;
                carInfo.CarOwner = JsonConvert.DeserializeObject < AlarmCar > (item.alarmList[0].peculiarnote).Name;
                carInfo.AlarmCount = item.Num.ToString();
                carInfo.Phone = JsonConvert.DeserializeObject<AlarmCar>(item.alarmList[0].peculiarnote).Phone;
                carInfo .Department = JsonConvert.DeserializeObject<AlarmCar>(item.alarmList[0].peculiarnote).Department;
                list.Add(carInfo);
                count++;

                ;
            }
        }

        private Alarm[] SelectedParkingList()
        {
            //1-违停;2-超速;
            long start = TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(ParkingStartTime));
            long end = TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(ParkingEndTime));
            Alarm[] alarms;
            alarms = CaraAlarms.Where(x => x.type == 1).ToArray();
            alarms = alarms.Where(x => long.Parse(x.altime) <= end).ToArray();
            alarms = alarms.Where(x => long.Parse(x.altime) >= start).ToArray();

            return alarms;
        }

        private void SearchHistory()
        {
            TrafficRecordList.Clear();
            if (DateTime.Parse(StartTime) > DateTime.Parse(EndTime))
            {
                return;
            }
            Alarm[] alarms = SelectedTrafficList();
            if (alarms != null)
            {
                foreach (var item in alarms)
                {
                    AlarmCar ret = JsonConvert.DeserializeObject<AlarmCar>(item.peculiarnote);
                    AlarmTraffic history = new AlarmTraffic();
                    history.Loacation = item.location;
                    history.AlarmTime = TimerConvert.ConvertTimeStampToDateTime(long.Parse(item.altime)).ToString("yyyy-MM-dd HH:mm:ss");
                    history.AlarmSignal = GetSignalName(item.type);// item.flag.Trim()+"-"+item.type;
                    history.AlarmDevice = item.sersorname;
                    history.CarNumber = ret.Plate;
                    history.CarOwner = ret.Name;
                    history.Department = ret.Department;
                    history.CarNumberPic = ret.CarUrl;
                    history.LocationPic = ret.SceneUrl;
                    history.Phone = ret.Phone;
                    TrafficRecordList.Add(history);
                }
                UpdatePageData();
            }
        }

        private string GetSignalName(int type)
        {
            if (type==1)
            {
                return "车辆违停";
            }
            else
            {
                return "车辆超速";
            }
        }

        private Alarm[] SelectedTrafficList()
        {
            //1-违停;2-超速;
            long start = TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(StartTime));
            long end= TimerConvert.ConvertDateTimeToTimeStamp(DateTime.Parse(EndTime));
            int type = AlarmType(AlarmSignal);
            Alarm[] alarms;
            if (type!=0)
            {
                alarms = CaraAlarms.Where(x => x.type == type).ToArray();
            }else
            {
                alarms = CaraAlarms;
            }
            alarms = alarms.Where(x => long.Parse(x.altime) <= end).ToArray();
            alarms = alarms.Where(x => long.Parse(x.altime) >= start).ToArray();
            if (alarms.Length  !=0&&!string.IsNullOrEmpty( AlarmDevice))
            {
                alarms = alarms.Where(x => x.sersorname.Contains(AlarmDevice)).ToArray();
            }
            if (alarms.Length != 0 && !string.IsNullOrEmpty(CarNumber))
            {
                alarms = alarms.Where(x => JsonConvert.DeserializeObject<AlarmCar>(x.peculiarnote).Plate == CarNumber).ToArray();
            }
            if (alarms.Length != 0 && !string.IsNullOrEmpty(CarOwner))
            {
                alarms = alarms.Where(x => JsonConvert.DeserializeObject<AlarmCar>(x.peculiarnote).Name == CarOwner).ToArray();
            }
            if (alarms.Length != 0 && !string.IsNullOrEmpty(PhoneNumber))
            {
                alarms = alarms.Where(x => JsonConvert.DeserializeObject<AlarmCar>(x.peculiarnote).Phone == PhoneNumber).ToArray();
            }
            if (alarms.Length != 0 && !string.IsNullOrEmpty(Department))
            {
                alarms = alarms.Where(x => JsonConvert.DeserializeObject<AlarmCar>(x.peculiarnote).Department == Department).ToArray();
            }



            return alarms;
        }

        private int AlarmType(string alarmSignal)
        {
            int type = 0;
            if (alarmSignal == "车辆违停")
            {
                type = 1;
            }
            else if (alarmSignal == "车辆超速")
            {
                type = 2;
            }
            else
            {
                type = 0;
            }
            return type;
        }


        private void CloseWin(object obj)
        {
            if (obj != null)
            {
                TrafficWin _win = (TrafficWin)obj;
                _win.Close();
            }
        }

        private void MaxWin(object obj)
        {
            if (obj != null)
            {
                TrafficWin _win = (TrafficWin)obj;
                if (_win.WindowState == WindowState.Maximized)
                {
                    _win.WindowState = WindowState.Normal;
                    _win.maxbt.Content = "\xE922";
                }
                else
                {
                    _win.WindowState = WindowState.Maximized;
                    _win.maxbt.Content = "\xE923";
                }

            }
        }
        /// <summary>
        /// 违规记录列表导出
        /// </summary>
        /// <param name="obj"></param>
        private async void ExportTrafficList(object obj)
        {
            try
            {
                if (TrafficRecordList.Count == 0)
                    return;

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx;*.xls",
                    RestoreDirectory = true,
                    FileName = $"车辆报警清单{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                };

                if (sfd.ShowDialog() == true)
                {
                    var result = await ExportExcel(TrafficRecordList, sfd.FileName);
                    MessageBox.Show($@"导出成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 违停记录导出
        /// </summary>
        /// <param name="obj"></param>
        private async void ExportParkingList(object obj)
        {
            try
            {
                if (ParkingCar.Count == 0&& ParkingDevice.Count == 0)
                    return;

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx;*.xls",
                    RestoreDirectory = true,
                    FileName = $"车辆违停{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                };

                if (sfd.ShowDialog() == true)
                {
                    var result = await ExportTopCountExcel(ParkingCar,ParkingDevice, sfd.FileName);
                    MessageBox.Show($@"导出成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 超速记录导出
        /// </summary>
        /// <param name="obj"></param>
        private async void ExportOverSpeedList(object obj)
        {
            try
            {
                if (OverSpeedCar.Count == 0&& OverSpeedDevice.Count ==0)
                    return;

                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx;*.xls",
                    RestoreDirectory = true,
                    FileName = $"车辆超速{DateTime.Now.ToString("yyyyMMddHHmmss")}"
                };

                if (sfd.ShowDialog() == true)
                {
                    var result = await ExportTopCountExcel(OverSpeedCar, OverSpeedDevice, sfd.FileName);
                    MessageBox.Show($@"导出成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 交通违规记录查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchRecord(object obj)
        {
            SearchHistory();
        }
        /// <summary>
        /// 违停TOP10查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchParking(object obj)
        {
            SearchParking();
        }
        /// <summary>
        /// 超速TOP10查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchOverSpeed(object obj)
        {
            SearchOverSpeed();
        }
        /// <summary>
        /// 违规记录列表
        /// </summary>
        /// <param name="obj"></param>
        private void TrafficList(object obj)
        {
            MyPanel.Children.Clear();
            _trafficList.DataContext = this;
            MyPanel.Children.Add(_trafficList);
        }
        /// <summary>
        /// 违停TOP10
        /// </summary>
        /// <param name="obj"></param>
        private void ParkingList(object obj)
        {
            MyPanel.Children.Clear();
            _parkingList.DataContext = this;
            MyPanel.Children.Add(_parkingList);
        }
        /// <summary>
        /// 超速TOP10
        /// </summary>
        /// <param name="obj"></param>
        private void OverSpeedList(object obj)
        {
            MyPanel.Children.Clear();
            _overSpeedList.DataContext = this;
            MyPanel.Children.Add(_overSpeedList);
        }



        private async Task<bool> ExportExcel(ObservableCollection<AlarmTraffic> trafficRecordList, string filePath)
        {
            return await Task.Run(() =>
            {

                string localFilePath = filePath; //获得文件路径 
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWB = excelApp.Workbooks.Add(System.Type.Missing);    //创建工作簿（WorkBook：即Excel文件主体本身）
                Microsoft.Office.Interop.Excel.Worksheet excelWS = (Microsoft.Office.Interop.Excel.Worksheet)excelWB.Worksheets[1];   //创建工作表（即Excel里的子表sheet） 1表示在子表sheet1里进行数据导出


                #region 表格属性设置
                var range = excelWS.get_Range("A1", "H1"); //获取Excel多个单元格区域：本例做为Excel表头  
                range.Font.Size = 15;
                range.Font.Name = "宋体"; //设置字体的种类 
                #endregion

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("报警时间", typeof(string));
                dt.Columns.Add("报警地点", typeof(string));
                dt.Columns.Add("报警设备", typeof(string));
                dt.Columns.Add("车牌", typeof(string));
                dt.Columns.Add("报警信号", typeof(string));
                dt.Columns.Add("车主", typeof(string));
                dt.Columns.Add("手机号", typeof(string));
                dt.Columns.Add("部门", typeof(string));

                foreach (var item in trafficRecordList)
                {
                    System.Data.DataRow row = dt.NewRow();
                    row["报警时间"] = item.AlarmTime;
                    row["报警地点"] = item.Loacation;
                    row["报警设备"] = item.AlarmDevice ;
                    row["车牌"] = item.CarNumber;
                    row["报警信号"] = item.AlarmSignal ;
                    row["车主"] = item.CarOwner;
                    row["手机号"] = item.Phone ;
                    row["部门"] = item.Department;
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

        private async Task<bool> ExportTopCountExcel(ObservableCollection<AlarmCarInfo> carList, ObservableCollection<AlarmCarInfo> deviceList, string filePath)
        {
            return await Task.Run(() =>
            {

                string localFilePath = filePath; //获得文件路径 
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //设置工作表的个数
                excelApp.SheetsInNewWorkbook = 2;
                //创建Wprkbook
                Microsoft.Office.Interop.Excel.Workbook excelWB= excelApp.Workbooks.Add();//创建工作簿（WorkBook：即Excel文件主体本身）
                //取出第一个工作表
                Microsoft.Office.Interop.Excel.Worksheet excelWS1 = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveWorkbook.Worksheets[1];//创建工作表（即Excel里的子表sheet） 1表示在子表sheet1里进行数据导出
                Microsoft.Office.Interop.Excel.Worksheet excelWS2 = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveWorkbook.Worksheets[2]; //sheet2
                #region 表格属性设置
                var range1 = excelWS1.get_Range("A1", "E1"); //获取Excel多个单元格区域：本例做为Excel表头  
                range1.Font.Size = 15;
                range1.Font.Name = "宋体"; //设置字体的种类 

                var range2 = excelWS2.get_Range("A1", "E1"); //获取Excel多个单元格区域：本例做为Excel表头  
                range2.Font.Size = 15;
                range2.Font.Name = "宋体"; //设置字体的种类 
                #endregion
                excelWS1.Name = "车辆报警车牌TOP10";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("车牌", typeof(string));
                dt.Columns.Add("次数", typeof(string));
                dt.Columns.Add("车主", typeof(string));
                dt.Columns.Add("电话", typeof(string));
                dt.Columns.Add("部门", typeof(string));

                foreach (var item in carList)
                {
                    System.Data.DataRow row = dt.NewRow();
                    row["车牌"] = item.CarNumber ;
                    row["次数"] = item.AlarmCount;
                    row["车主"] = item.CarOwner;
                    row["电话"] = item.Phone;
                    row["部门"] = item.Department;
                    dt.Rows.Add(row);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        excelWS1.Cells[1, j + 1] = dt.Columns[j].ColumnName;
                        excelWS1.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();   //Excel单元格第一个从索引1开始
                    }
                }

                excelWS2.Name = "车辆报警设备TOP10";
                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2.Columns.Add("设备名称", typeof(string));
                dt2.Columns.Add("报警次数", typeof(string));
                dt2.Columns.Add("唯一标识", typeof(string));
                dt2.Columns.Add("位置", typeof(string));
                dt2.Columns.Add("型号", typeof(string));

                foreach (var item in deviceList)
                {
                    System.Data.DataRow row = dt2.NewRow();
                    row["设备名称"] = item.AlarmDevice;
                    row["报警次数"] = item.AlarmCount;
                    row["唯一标识"] = item.DeviceCode;
                    row["位置"] = item.Location;
                    row["型号"] = item.DeviceType;
                    dt2.Rows.Add(row);
                }

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        excelWS2.Cells[1, j + 1] = dt2.Columns[j].ColumnName;
                        excelWS2.Cells[i + 2, j + 1] = dt2.Rows[i][j].ToString();   //Excel单元格第一个从索引1开始
                    }
                }

                excelWB.SaveAs(localFilePath);  //将其进行保存到指定的路径
                excelWB.Close();
                excelApp.Quit();  //KillAllExcel(excelApp); 释放可能还没释放的进程

                return true;
            });
        }
    }
}
