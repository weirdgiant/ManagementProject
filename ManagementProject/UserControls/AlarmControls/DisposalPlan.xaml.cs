using Aspose.Words;
using com.mango.protocol;
using com.mango.protocol.Enum;
using com.mango.protocol.msg;
using ManagementProject.Converters;
using ManagementProject.FunctionalWindows;
using ManagementProject.UserControls.AlarmControls;
using ManagementProject.ViewModel;
using MangoApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ManagementProject.UserControls
{
    /// <summary>
    /// DisposalPlan.xaml 的交互逻辑
    /// </summary>
    public partial class DisposalPlan : UserControl
    {
        public DisposalPlan()
        {
            InitializeComponent();
        }
    }
    #region Model
    public class DisposalPlanModel:INotifyPropertyChangedClass
    {
        private Visibility _isOpenTextVisibility;
        public Visibility IsOpenTextVisibility
        {
            get
            {
                return _isOpenTextVisibility;
            }
            set
            {
                _isOpenTextVisibility = value;
                NotifyPropertyChanged("_isOpenTextVisibility");
            }
        }

        private string _disposalName;
        public string DisposalName
        {
            get
            {
                return _disposalName;
            }
            set
            {
                _disposalName = value;
                NotifyPropertyChanged("DisposalName");
            }
        }

        private string _alarmInfo;
        public string AlarmInfo
        {
            get
            {
                return _alarmInfo;
            }
            set
            {
                _alarmInfo = value;
                NotifyPropertyChanged("AlarmInfo");
            }
        }
        private string _alarmTime;
        public string AlarmTime
        {
            get
            {
                return _alarmTime;
            }
            set
            {
                _alarmTime = value;
                NotifyPropertyChanged("AlarmTime");
            }
        }

        private string _alarmLocation;
        public string AlarmLocation
        {
            get
            {
                return _alarmLocation;
            }
            set
            {
                _alarmLocation = value;
                NotifyPropertyChanged("AlarmLocation");
            }
        }
        private string _alarmDevice;
        public string AlarmDevice
        {
            get
            {
                return _alarmDevice;
            }
            set
            {
                _alarmDevice = value;
                NotifyPropertyChanged("AlarmDevice");
            }
        }

        private string _alarmTip;
        public string AlarmTip
        {
            get
            {
                return _alarmTip;
            }
            set
            {
                _alarmTip = value;
                NotifyPropertyChanged("AlarmTip");
            }
        }


        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
            }
        }
        private string _carNumber;
        public string CarNumber
        {
            get
            {
                return _carNumber;
            }
            set
            {
                _carNumber = value;
                NotifyPropertyChanged("CarNumber");
            }
        }
        private string _carOwner;
        public string CarOwner
        {
            get
            {
                return _carOwner;
            }
            set
            {
                _carOwner = value;
                NotifyPropertyChanged("CarOwner");
            }
        }
        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                NotifyPropertyChanged("Phone");
            }
        }

        private string _depart;
        public string Depart
        {
            get
            {
                return _depart;
            }
            set
            {
                _depart = value;
                NotifyPropertyChanged("Depart");
            }
        }

        private bool _isConfirmbyCode=false;
        public bool IsConfirmbyCode
        {
            get
            {
                return _isConfirmbyCode;
            }
            set
            {
                _isConfirmbyCode = value;
                NotifyPropertyChanged("IsConfirmbyCode");
            }
        }

        private ObservableCollection<PlanText> _plan;
        public ObservableCollection<PlanText> Plan
        {
            get
            {
                return _plan;
            }
            set
            {
                _plan = value;
                NotifyPropertyChanged("Plan");
            }
        }

        public TextPlan CurrentPlan { get; set; }
    }
    #endregion
    public class DisposalPlanViewModel:DisposalPlanModel
    {
        private int _alarmID;
        public int AlarmID
        {
            get
            {
                return _alarmID;
            }
            set
            {
                _alarmID = value;
                SetPlan(_alarmID.ToString());
            }
        }
        public AlarmPageViewModel alarmPageViewModel { get; set; }
        public MainWindowViewModel mainWindowViewModel { get; set; }
        public DelegateCommand OpenDisposalPlanCommand { get; set; }
        public DelegateCommand HandleAlarmCommand { get; set; }
        public DelegateCommand PressHandlingCommand { get; set; }
        public DelegateCommand ShowMessageCommand { get; set; }
        public DelegateCommand RealAlarmCommand { get; set; }
        public DelegateCommand ErrorAlarmCommand { get; set; }
        public DelegateCommand DeviceErrorCommand { get; set; }
        public DelegateCommand CloseConfirmWinCommand { get; set; }
        public DelegateCommand SendMesCommand { get; set; }
        public DisposalPlanViewModel(AlarmPageViewModel _alarmPageViewModel)
        {
            alarmPageViewModel = _alarmPageViewModel;
            InitCommand();
        }

        private void InitCommand()
        {
            OpenDisposalPlanCommand = new DelegateCommand();
            OpenDisposalPlanCommand.ExecuteCommand = new Action<object>(OpenDisposalPlan);
            HandleAlarmCommand = new DelegateCommand();
            HandleAlarmCommand.ExecuteCommand = new Action<object>(HandleAlarm);
            PressHandlingCommand = new DelegateCommand();
            PressHandlingCommand.ExecuteCommand = new Action<object>(PressHandling);
            ShowMessageCommand = new DelegateCommand();
            ShowMessageCommand.ExecuteCommand = new Action<object>(ShowMessage);
            RealAlarmCommand = new DelegateCommand();
            RealAlarmCommand.ExecuteCommand = new Action<object>(RealAlarm);
            ErrorAlarmCommand = new DelegateCommand();
            ErrorAlarmCommand.ExecuteCommand = new Action<object>(ErrorAlarm);
            DeviceErrorCommand = new DelegateCommand();
            DeviceErrorCommand.ExecuteCommand = new Action<object>(DeviceError);
            CloseConfirmWinCommand = new DelegateCommand();
            CloseConfirmWinCommand.ExecuteCommand = new Action<object>(CloseConfirmWin);
            SendMesCommand = new DelegateCommand();
            SendMesCommand.ExecuteCommand = new Action<object>(SendMes);

        }

        #region 发送短信
        private long LastNoticeTime;
        private string LastPhone;
        private void SendMes(object obj)
        {
            try
            {
                PlanText plan = (PlanText)obj;


                if (LastPhone == plan.Phone && LastNoticeTime != 0 && DateTime.Now.Ticks - LastNoticeTime < 242668523)
                {
                    //MessageBox.Show("10秒内不可再次发送!");
                    DialogWindow dialogWindow = new DialogWindow("如需再次发送，请60秒之后再操作") { Topmost = true };
                    dialogWindow.ShowDialog();
                    return;
                }
                string mapName = "";
                MangoMap[] ret = GlobalVariable.MapList.Where(x => x.id == alarmPageViewModel.SelectedAlarm.mapid).ToArray();
                if (ret==null)
                {
                    MangoMap[] map = GlobalVariable.MapList.Where(x => x.id == alarmPageViewModel.SelectedAlarm.sid).ToArray();
                    mapName = map[0].name;
                }
                else
                {
                    mapName = ret[0].name;
                }
               
                CS_SendSMS send = new CS_SendSMS();
                string signalType = alarmPageViewModel.SelectedAlarm.flag.Trim() + "-" + alarmPageViewModel.SelectedAlarm.type;
                string content = plan.MesModel;
                content = content.Replace("$MapName$", mapName);
                content = content.Replace("$DeviceName$", alarmPageViewModel.SelectedAlarm.sersorname);
                if (content.Contains("$Owner$"))
                {
                    content = content.Replace("$Owner$", "");
                }
                if (content.Contains("$CarNumber$"))
                {
                    content = content.Replace("$CarNumber$", "");
                }
                send.content = content;
                send.phone = plan.Phone;
                send.alarmtype = signalType;
                send.push = plan .PushId;
                OutStream message = App.mango.Async(send, (short)send.protocol);
                LastNoticeTime = DateTime.Now.Ticks;
                LastPhone = plan.Phone;
                //MessageBox.Show("短信通知成功!");
                DialogWindow dialog = new DialogWindow("") { Topmost=true};
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlanViewModel), "SendMes:" + ex.Message);
            }
        }
        #endregion
        #region 设置处置预案
        /// <summary>
        /// 设置处置预案
        /// </summary>
        /// <param name="alarmid"></param>
        private void SetPlan(string alarmid)
        {
            try
            {
                Plan = new ObservableCollection<PlanText>();
                string flag = alarmPageViewModel.SelectedAlarm.flag.Trim();
                string url = AppConfig.ServerBaseUri + AppConfig.GetTextPlan;
                TextPlan[] textPlan = HttpAPi.GetPlan(url, alarmid, flag);
                if (textPlan.Length == 0)
                {
                    return;
                }
                CurrentPlan = textPlan[0];
                string step = CurrentPlan.steps;
                string content = CurrentPlan.content;
                DisposalName = CurrentPlan.name;
                if (string.IsNullOrEmpty (CurrentPlan.papers))
                {
                    IsOpenTextVisibility = Visibility.Collapsed;
                }else
                {
                    IsOpenTextVisibility = Visibility.Visible;
                }
               
                if (step == null) return;
                string[] plan = step.Split(new[] { "#$%@" }, StringSplitOptions.None);
                if (CurrentPlan.employeeList != null)
                {
                    for (int i = 0; i < plan.Length; i++)
                    {
                        PlanText planText = new PlanText();
                        int index = i + 1;
                        planText.PlanStep = index + "、" + plan[i];
                        if (CurrentPlan.employeeList[i] == null)
                        {
                            planText.IsVisibility = Visibility.Collapsed;
                        }
                        else
                        {
                            planText.Contacts = CurrentPlan.employeeList[i].name;
                            planText.Phone = CurrentPlan.employeeList[i].mobile;
                            planText.MesModel = CurrentPlan.templateListValue[i].content;
                            planText.IsVisibility = Visibility.Visible;
                            planText.PushId = CurrentPlan.employeeList[i].id;
                        }
                        Plan.Add(planText);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlanViewModel), ex.Message);
            }
        }
        #endregion
        #region 打开预案文件
        /// <summary>
        /// 打开预案文件
        /// </summary>
        /// <param name="obj"></param>
        private void OpenDisposalPlan(object obj)
        {
            try
            {
                string url = AppConfig.ImageBaseUri + CurrentPlan.papers;
                string[] list = url.Split(new[] { "/" }, StringSplitOptions.None);
                WebClient dc = new WebClient();
                string path = list[list.Length - 1];
                string[] fileFull = path.Split(new[] { "." }, StringSplitOptions.None);
                string fileName = fileFull[0];
                string fileType = fileFull[1];

                string savePath = fileName + ".pdf";
                dc.DownloadFile(url, path);
                if (fileType != "pdf")
                {
                    Document doc = new Document(path);
                    doc.Save(savePath, SaveFormat.Pdf);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                // System.Diagnostics.Process.Start(path);
                PdfReader reader = new PdfReader(savePath);
                reader.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                reader.Topmost = true;
                reader.ShowDialog();
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlan),ex.Message);
            }
        }
        #endregion

        #region 报警处理

        /// <summary>
        /// 关闭报警处理界面
        /// </summary>
        /// <param name="obj"></param>
        private void CloseConfirmWin(object obj)
        {
            if (obj != null)
            {
                Window alarmConfirm = (Window)obj;
                alarmConfirm.Close();
            }
        }
        /// <summary>
        /// 真实报警
        /// </summary>
        /// <param name="obj"></param>
        private void RealAlarm(object obj)
        {
            try
            {
                if (!IsConfirmbyCode)
                {
                    confirmById(ALARM_STATE.CONFIRMED_AS_REAL);
                }
                else
                {
                    confirmByCode(ALARM_STATE.CONFIRMED_AS_REAL);
                }
                //confirmByCode(ALARM_STATE.CONFIRMED_AS_REAL);
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlanViewModel), "RealAlarm"+ex.Message );
            }
            
        }
        /// <summary>
        /// 误报
        /// </summary>
        /// <param name="obj"></param>
        private void ErrorAlarm(object obj)
        {
            try
            {
                if (!IsConfirmbyCode)
                {
                    confirmById(ALARM_STATE.CONFIRMED_AS_MISTAKE);
                }
                else
                {
                    confirmByCode(ALARM_STATE.CONFIRMED_AS_MISTAKE);
                }
                //confirmByCode(ALARM_STATE.CONFIRMED_AS_MISTAKE );
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlanViewModel), "ErrorAlarm" + ex.Message);
            }
        }
        /// <summary>
        /// 设备故障
        /// </summary>
        /// <param name="obj"></param>
        private void DeviceError(object obj)
        {
            try
            {
                if (!IsConfirmbyCode)
                {
                    confirmById(ALARM_STATE.ADEVICE_ERROR);
                }
                else
                {
                    confirmByCode(ALARM_STATE.ADEVICE_ERROR);
                }
               
            }
            catch (Exception ex)
            {
                Logger.Error(typeof(DisposalPlanViewModel), "DeviceError" + ex.Message);
            }
        }

        private void confirmByCode(ALARM_STATE state)
        {
            int id = AlarmID;
            MangoApi.Alarm alarm = alarmPageViewModel.SelectedAlarm;
            string alarmcode = alarm.sersor;
            try
            {
                CS_ConfirmAlarmByCode confirm = new CS_ConfirmAlarmByCode();
                confirm.code = alarmcode;
                confirm.note = AlarmTip;
                confirm.state = state;

                OutStream buffer = App.mango.Async(confirm, (short)confirm.protocol);
                if (buffer != null)
                {
                    SC_ConfirmAlarm ret = (SC_ConfirmAlarm)Package.SCStruct<SC_ConfirmAlarm>(buffer);
                    if (ret.ret != 0)
                    {
                        MessageBox.Show("系统错误，确认失败!");
                        return;
                    }
                    else
                    {
                        alarmPageViewModel.SelectedId = 0;
                        alarmPageViewModel.GetAlarmInfo(alarmPageViewModel.PageType);
                        alarmPageViewModel._tabControlWin.ReLoaded();
                        string flag = alarmPageViewModel.SelectedAlarm.flag.Trim();
                        if (flag == "CAR")
                        {
                            alarmCarConfirm.Close();
                        }
                        else
                        {
                            alarmConfirm.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }
            finally
            {
                if (alarm.flag.Trim() == "GAS")
                {
                    CloseAllDoor(alarm.sersor);
                }
            }

            
        }

        private void confirmById(ALARM_STATE state)
        {
            int id = AlarmID;
            MangoApi.Alarm alarm = alarmPageViewModel.SelectedAlarm;
            try
            {
                CS_ConfirmAlarm confirm = new CS_ConfirmAlarm();
                confirm.alarmId = AlarmID.ToString();
                confirm.state = state;
                confirm.note = AlarmTip;
                OutStream buffer = App.mango.Async(confirm, (short)confirm.protocol);
                if (buffer != null)
                {
                    SC_ConfirmAlarm ret = (SC_ConfirmAlarm)Package.SCStruct<SC_ConfirmAlarm>(buffer);
                    if (ret.ret != 0)
                    {
                        MessageBox.Show("系统错误，确认失败!");
                        return;
                    }
                    else
                    {
                        alarmPageViewModel.SelectedId = 0;
                        alarmPageViewModel.GetAlarmInfo(alarmPageViewModel.PageType);
                        alarmPageViewModel._tabControlWin.ReLoaded();
                        string flag = alarmPageViewModel.SelectedAlarm.flag.Trim();
                        if (flag == "CAR")
                        {
                            alarmCarConfirm.Close();
                        }
                        else
                        {
                            alarmConfirm.Close();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                Logger .Error(ex);
            }
            finally
            {
                if (alarm.flag.Trim() == "GAS")
                {
                    CloseAllDoor(alarm.sersor);
                }
            }

            
           

        }

        private void InitAlarmConfirm()
        {
            int id = AlarmID;
            MangoApi.Alarm alarm = alarmPageViewModel.AlarmInfo.Where(x => x.id == id).ToArray()[0];
            AlarmLocation = alarm.location;
            AlarmInfo = alarm.flagVal + "报警" + "   " + alarm.level + "级";
            User = App.mango.getClientInfo().username;
            AlarmDevice = alarm.sersorname;
            AlarmTime = TimerConvert.ConvertTimeStampToDateTime(long.Parse(alarm.altime)).ToString(); //alarm.altime;
            if (alarm.flag .Trim ()=="CAR")
            {
                AlarmCar alarmCar = (AlarmCar)GetAlarmInfo<AlarmCar>(alarm.peculiarnote);
                CarNumber = alarmCar.Plate;
                CarOwner = alarmCar.Name;
                Phone = alarmCar.Phone;
                Depart = alarmCar.Department;
            }
        }

        private object GetAlarmInfo<T>(string info)
        {
            if (info == null) return null;
            var ret = JsonConvert.DeserializeObject<T>(info);
            return ret;
        }


        /// <summary>
        /// 关闭所有门禁
        /// </summary>
        private void CloseAllDoor(string code)
        {
            try
            {
                Element[] elements = HttpAPi.GetRoomAccess(code);
                if (elements != null && elements.Length != 0)
                {
                    foreach (var item in elements)
                    {
                        CS_ACCESSCONTROl login = new CS_ACCESSCONTROl
                        {
                            code = item.code,
                            type = 2
                        };
                        OutStream message = App.mango.Async(login, (short)login.protocol);
                    }
                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
           
           
        }




        #endregion

        private AlarmConfirm alarmConfirm { get; set; }
        private AlarmCarConfirm alarmCarConfirm;
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="obj"></param>
        private void HandleAlarm(object obj)
        {
            string flag = alarmPageViewModel.SelectedAlarm.flag.Trim();
            InitAlarmConfirm();
            if (flag=="CAR")
            {
                alarmCarConfirm = new AlarmCarConfirm();
                alarmCarConfirm.Topmost = true;
                alarmCarConfirm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                alarmCarConfirm.DataContext = this;
                PlanText[] plan = Plan.Where(x => x.IsChecked == false).ToArray();
                if (plan.Length != 0)
                {
                    alarmCarConfirm.realbt.IsEnabled = false;
                }
                else
                {
                    alarmCarConfirm.realbt.IsEnabled = true;
                }
                alarmCarConfirm.ShowDialog();
            }
            else
            {
                alarmConfirm = new AlarmConfirm();
                alarmConfirm.Topmost = true;
                alarmConfirm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                alarmConfirm.DataContext = this;
                PlanText[] plan = Plan.Where(x => x.IsChecked == false).ToArray();
                if (plan.Length != 0)
                {
                    alarmConfirm.realbt.IsEnabled = false;
                }
                else
                {
                    alarmConfirm.realbt.IsEnabled = true;
                }
                alarmConfirm.ShowDialog();
            }
            
            
        }
        /// <summary>
        /// 跟催
        /// </summary>
        /// <param name="obj"></param>
        private void PressHandling(object obj)
        {

        }
        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="obj"></param>
        private void ShowMessage(object obj)
        {

        }
    }

    public class PlanText
    {
        public string PushId { get; set; }
        public string PlanStep { get; set; }
        public string Phone { get; set; }
        public string Contacts { get; set; }
        public bool IsChecked { get; set; }
        public Visibility IsVisibility { get; set; }
        public string MesModel { get; set; }
    }
}
