using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using NetSDKCS;

// ReSharper disable ConvertToAutoProperty
namespace ManagementProject
{
    public class NVRSDKManager
    {
        public IntPtr m_LoginHandle = IntPtr.Zero;
        private int queryTime = 0;
        public DateTime m_PlayBack_StartTime { get; set; }
        public DateTime m_PlayBack_EndTime { get; set; }
        private NET_DEVICEINFO_Ex m_DeviceInfo;
        private readonly fDisConnectCallBack m_DisConnectHandle;
        public bool IsLogin { get;set;}

        private NVRSDKManager()
        {
            m_DisConnectHandle = DisConnectCallBack;

        }


        private void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {

            MessageBox.Show("NVR连接断开！", Marshal.PtrToStringAnsi(pchDVRIP));
        }

        public bool PlayBackControl(IntPtr mplayHandle, PlayBackType playType)
        {
            try
            {

                var result = NETClient.PlayBackControl(mplayHandle, playType);
                return result;

            }

            catch (Exception ex)
            {
                Logger.Error(typeof(NVRSDKManager),ex.Message);
            }
            return false;
        }

        public bool Initialize()
        {
           return NETClient.Init(m_DisConnectHandle, IntPtr.Zero, null);
        }

        public void CleanUp()
        {
            NETClient.Cleanup();
          
        }

        public bool Pause(IntPtr playHandle)
        {
            if (playHandle != IntPtr.Zero)
            {
                var result = NETClient.PlayBackControl(playHandle, PlayBackType.Pause);
                return result;
            }
            return false;

        }
        public bool Resume(IntPtr playHandle)
        {
            if (playHandle != IntPtr.Zero)
            {
                var result = NETClient.PlayBackControl(playHandle, PlayBackType.Play);
                return result;
            }
            return false;
        }

        public void SetReconnectDev()
        {
            NETClient.SetAutoReconnect((a, b, c, d) => { MessageBox.Show("NVR 设备已经重新连接"); }, IntPtr.Zero);
        }

        public bool QueryPlayBack(DateTime startTime, DateTime endTime, int m_PlayBack_Channel)
        {
            if(startTime>DateTime.Now|| endTime > DateTime.Now||startTime>=endTime)
            {
              
                return false;

            }

            SetDeviceModel(1, EM_RECORD_TYPE.ALL);//Set stream type, and record type
            Query:
            try
            {

                DateTime nendTime = startTime;
                NET_RECORDFILE_INFO[] array_RecordFile_Info = new NET_RECORDFILE_INFO[2000];

                int fileCount = 0;
                NETClient.QueryRecordFile(m_LoginHandle, m_PlayBack_Channel, EM_QUERY_RECORD_TYPE.ALL, startTime, endTime, null, ref array_RecordFile_Info, ref fileCount, 5000, false);
                if (fileCount <= 0 || array_RecordFile_Info.Length <= 0)
                    return false;
                var nstartTime = array_RecordFile_Info[0].starttime.ToDateTime();
                for (int i = 0; i < Math.Min(fileCount, array_RecordFile_Info.Length); i++)
                {
                    NET_TIME startNetTime = array_RecordFile_Info[i].starttime.ToDateTime() < startTime ? NET_TIME.FromDateTime(startTime) : array_RecordFile_Info[i].starttime;
                    NET_TIME endNetTime = array_RecordFile_Info[i].endtime.ToDateTime() > endTime ? NET_TIME.FromDateTime(endTime) : array_RecordFile_Info[i].endtime;
                    if (endNetTime.ToDateTime() > startNetTime.ToDateTime())
                    {
                        var span = endNetTime.ToDateTime() - startNetTime.ToDateTime();
                        nendTime += span;
                    }

                }
                if (startTime >= nstartTime &&nendTime>=startTime)
                    return true;


            }
            catch (NETClientExcetion nex)
            {
                if (queryTime < 3)
                {
                    Thread.Sleep(50);
                    queryTime++;
                    goto Query;
                }
                MessageBox.Show(nex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;

        }
        public IntPtr StartPlayBack(IntPtr windowHandle, int playBackChannel, DateTime starTime, DateTime endTime)
        {

            var result = IntPtr.Zero;
            if (IntPtr.Zero == m_LoginHandle)
            {
                return result;
            }

            SetDeviceModel(1, EM_RECORD_TYPE.ALL);//Set stream type, and record type

            //start play

            try
            {
                //package playback param
                NET_IN_PLAY_BACK_BY_TIME_INFO stuInfo = new NET_IN_PLAY_BACK_BY_TIME_INFO();
                NET_OUT_PLAY_BACK_BY_TIME_INFO stuOut = new NET_OUT_PLAY_BACK_BY_TIME_INFO();
                stuInfo.stStartTime = NET_TIME.FromDateTime(starTime);
                stuInfo.stStopTime = NET_TIME.FromDateTime(endTime);
                stuInfo.hWnd = windowHandle;
                stuInfo.cbDownLoadPos = null;
                stuInfo.dwPosUser = IntPtr.Zero;
                stuInfo.fDownLoadDataCallBack = null;
                stuInfo.dwDataUser = IntPtr.Zero;
                stuInfo.nPlayDirection = 0;
                stuInfo.nWaittime = 5000;

                result = NETClient.PlayBackByTime(m_LoginHandle, playBackChannel, stuInfo, ref stuOut);

            }
            catch (NETClientExcetion nex)
            {
                MessageBox.Show(nex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;

        }

        public bool StopPlayBack(ref IntPtr playHandle)
        {

            bool bResult = false;
            if (IntPtr.Zero != playHandle)
            {
                try
                {

                    bResult = NETClient.PlayBackControl(playHandle, PlayBackType.Stop);
                    if (bResult)
                    {
                        playHandle = IntPtr.Zero;
                    }
                }
                catch (NETClientExcetion nex)
                {
                    MessageBox.Show(nex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return bResult;
        }
        public void SetDeviceModel(int nStreamType, EM_RECORD_TYPE emRecordType)
        {

            //start play
            IntPtr pStream = IntPtr.Zero;
            IntPtr pRecordType = IntPtr.Zero;
            try
            {
                //set streamType
                pStream = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.StructureToPtr(nStreamType, pStream, true);
                NETClient.SetDeviceMode(m_LoginHandle, EM_USEDEV_MODE.RECORD_STREAM_TYPE, pStream);

                //set recordType
                int nRecordType = (int)emRecordType;
                pRecordType = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
                Marshal.StructureToPtr(nRecordType, pRecordType, true);
                NETClient.SetDeviceMode(m_LoginHandle, EM_USEDEV_MODE.RECORD_TYPE, pRecordType);
            }
            catch (NETClientExcetion nex)
            {
                MessageBox.Show(nex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(pStream);
                Marshal.FreeHGlobal(pRecordType);
            }

        }
        public bool Login(string nvrIp, ushort port, string userName, string password)
        {
            try
            {
                m_LoginHandle = NETClient.Login(nvrIp, port, userName, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero,
                    ref m_DeviceInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            if (IntPtr.Zero == m_LoginHandle)
            {
                return false;
#if Debug
                MessageBox.Show("NVR登录失败！");
#endif
            }
            else
            {
                return true;
            }
        }

        public void Logout()
        {
            try
            {
                if (m_LoginHandle != IntPtr.Zero)
                {
                    bool result = NETClient.Logout(m_LoginHandle);
                }
     
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
#if Debug
                MessageBox.Show("NVR登出失败！");
#endif
            }
        }
        public void Download(int channel, DateTime startTime, DateTime endTime,string filepath=null)
        {
            string path = string.Empty;
            if (filepath == null)
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.Filter = "dav|*.dav";
               // string path = string.Empty;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    path = sfd.FileName;
                }
                else //user close the dialog
                {

                    return;
                }
            }else
            {
                path = filepath;
            }

            SetDeviceModel(1, EM_RECORD_TYPE.ALL);//Set stream type, and record type

            try
            {
                IntPtr m_FileHandle = NETClient.DownloadByTime(m_LoginHandle, channel, EM_QUERY_RECORD_TYPE.ALL, startTime, endTime, path, null, IntPtr.Zero, null, IntPtr.Zero, IntPtr.Zero);
                if (IntPtr.Zero != m_FileHandle)
                {

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

        public void StopDownLoad(IntPtr fileHandle)
        {

            if (IntPtr.Zero != fileHandle)
            {
                bool ret = NETClient.StopDownload(fileHandle);

            }

        }

        public static NVRSDKManager Instance { get; } = new NVRSDKManager();
    }
}
