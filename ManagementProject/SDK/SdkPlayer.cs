using System;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using SFAI.Utility;
using System.Windows;

namespace ManagementProject
{
    public enum PLAY_TYPE_E
    {
        PLAY_TYPE_BLANK = 0,
        PLAY_TYPE_LIVE,
        PLAY_TYPE_SWITCH,
        PLAY_TYPE_VOD,
        PLAY_TYPE_LOCAL,

    }

    public enum XP_PLAY_STATUS_E
    {
        XP_PLAY_STATUS_16_BACKWARD = 0,     /**< 16倍速后退播放 */
        XP_PLAY_STATUS_8_BACKWARD = 1,      /**< 8倍速后退播放 */
        XP_PLAY_STATUS_4_BACKWARD = 2,      /**< 4倍速后退播放 */
        XP_PLAY_STATUS_2_BACKWARD = 3,      /**< 2倍速后退播放 */
        XP_PLAY_STATUS_1_BACKWARD = 4,      /**< 正常速度后退播放 */
        XP_PLAY_STATUS_HALF_BACKWARD = 5,   /**< 1/2倍速后退播放 */
        XP_PLAY_STATUS_QUARTER_BACKWARD = 6,/**< 1/4倍速后退播放 */
        XP_PLAY_STATUS_QUARTER_FORWARD = 7, /**< 1/4倍速播放 */
        XP_PLAY_STATUS_HALF_FORWARD = 8,    /**< 1/2倍速播放 */
        XP_PLAY_STATUS_1_FORWARD = 9,       /**< 正常速度前进播放 */
        XP_PLAY_STATUS_2_FORWARD = 10,      /**< 2倍速前进播放 */
        XP_PLAY_STATUS_4_FORWARD = 11,      /**< 4倍速前进播放 */
        XP_PLAY_STATUS_8_FORWARD = 12,      /**< 8倍速前进播放 */
        XP_PLAY_STATUS_16_FORWARD = 13      /**< 16倍速前进播放 */
    }

    public enum SetPlayStatus
    {
        Quick_Play,
        Slow_Play
    }
    public class CameraCodeEventArgs : EventArgs
    {
        public String CameraCode;
        public bool isPtz;
    }
    public class UnvOnePlayer
    {
        public bool IsDigitalZoom;
        public IntPtr WindowsHandle { get; set; }

        public static int MAX_PLAY_NUMBER = 256;
        public static int CurrentPlayerNumber = 0;
        public static bool IsInitialized = false;
        public static PLAY_WND_INFO_S[] PlayWndInfo = new PLAY_WND_INFO_S[MAX_PLAY_NUMBER];
        public string ChannelCode { get; set; }
        public PLAY_TYPE_E PlayType=PLAY_TYPE_E.PLAY_TYPE_BLANK;
        public int ID;
        public bool IsRecording;
        public bool IsPlayed;
        public bool IsPause;
        public bool IsMute;
        public bool IsStartedVod;
        public string CameraCode;

        public bool IsPtz { get; set; }
        public string SnapPicturePath { get; set; }
        public string RecodVideoPath { get; set; }
        public System.Timers.Timer TimersTimer { set; get; }
        public static UnvOnePlayer CreateOnePlayer()
        {
            if (CurrentPlayerNumber < MAX_PLAY_NUMBER)
            {
                var player = new UnvOnePlayer();
                player.ID = CurrentPlayerNumber++;
                return player;
            }
            else
            {
                Logger.Error(typeof(UnvOnePlayer), "超出最大播放器数量");
                throw new Exception("Exceed max player number!");
            }


        }

        public UnvOnePlayer()
        {
            RecodVideoPath = VideoLocalConfig.VodDownloadLoc;
            SnapPicturePath = VideoLocalConfig.PicSnatchLoc;
            PlayType = PLAY_TYPE_E.PLAY_TYPE_BLANK;

        }

        public static uint InitializePlayer()
        {
            IntPtr ptrPlayWndInfo = Marshal.AllocHGlobal(MAX_PLAY_NUMBER * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
            var ulRet = IMOSSDK.IMOS_StartPlayer(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, (uint)MAX_PLAY_NUMBER, ptrPlayWndInfo);
            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_StartPlayer失败：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_StartPlayer 启动播放器失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }

            for (int i = 0; i < MAX_PLAY_NUMBER; i++)
            {
                IntPtr ptrTemp = new IntPtr(ptrPlayWndInfo.ToInt64 () + i * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
                PlayWndInfo[i] = (PLAY_WND_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(PLAY_WND_INFO_S));

            }

            IsInitialized = true;
            return ulRet;

        }

        public static uint ClosePlayer()
        {
            uint ulRet = 0;

            if (SdkManager.GetInstance().userLoginStatus)
            {

                ulRet = IMOSSDK.IMOS_StopPlayer(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo);
                if (0 != ulRet)
                {
#if DEBUG
                    MessageBox.Show("IMOS_StopPlayer失败：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                    Logger.Error(typeof(UnvOnePlayer), "IMOS_StopPlayer 关闭播放器失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));

                }
            }

            return ulRet;
        }

        public uint PlayLive(string cameraCode, IntPtr playWindow)
        {
            uint ulRet;
            var channelCode = Marshal.AllocHGlobal(1 * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
            IMOSSDK.IMOS_GetChannelCode(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, channelCode);
            ChannelCode = Marshal.PtrToStringAnsi(channelCode);
            Marshal.FreeHGlobal(channelCode);

            ulRet = IMOSSDK.IMOS_SetPlayWnd(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), playWindow);
            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_SetPlayWnd:" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_SetPlayWnd 设置播放器句柄失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }

            ulRet = IMOSSDK.IMOS_StartMonitor (ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(cameraCode), Encoding.Default.GetBytes(ChannelCode), 1, 0);
            if (0 != ulRet)
            {

                Logger.Error(typeof(UnvOnePlayer), cameraCode + " IMOS_StartMonitor 开始播放失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));

            }
            else
            {
                CameraCode = cameraCode;
                IsPlayed = true;
                PlayType = PLAY_TYPE_E.PLAY_TYPE_LIVE;
            }
            return ulRet;

        }

        public uint StopLive()
        {
            uint ulRet = 0;
            if (ChannelCode == null)
                return 0;
            ulRet = IMOSSDK.IMOS_StopMonitor(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), 0);
            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_StopMonitor " + CameraCode + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_StopMonitor 停止播放失败,错误码：" + CameraCode + ErrorCode.GetErrorMsg(ulRet));

            }

            ulRet = IMOSSDK.IMOS_FreeChannelCode(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
            if (0 != ulRet)
            {
#if DEBUG
                //MessageBox.Show("IMOS_FreeChannelCode" + CameraCode + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_FreeChannelCode 释放播放通道失败,错误码：" + CameraCode + ErrorCode.GetErrorMsg(ulRet));

            }
            ChannelCode = null;
            if (ulRet == 0)
            {
                IsRecording = false;
                IsPlayed = false;
                PlayType = PLAY_TYPE_E.PLAY_TYPE_BLANK;
                ChannelCode = null;
            }

            return ulRet;

        }

        public string SnapPicture(string cameraCode, uint format)
        {
            DateTime dt = DateTime.Now;
            string fileName = $"{cameraCode}_{dt:yyyyMMddHHmmssffff}";
            string path = SnapPicturePath.Trim('.').Trim('\\');
            fileName = AppDomain.CurrentDomain.BaseDirectory + path + "\\" + fileName;

            if (IsPlayed)
            {
                var ulRet = IMOSSDK.IMOS_SnatchOnceEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), Encoding.Default.GetBytes(fileName), format);

                if (0 != ulRet)
                {
#if DEBUG
                    MessageBox.Show("抓图失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                    Logger.Error(typeof(UnvOnePlayer), "抓图失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));
                    //return ulRet;
                }
                else
                {
                    SnapPicturePath = SnapPicturePath.Trim('.');
                    //MessageBox.Show("抓图成功！图片保存在:" + fileName);
                    //return ulRet;
                }
            }
            return fileName;
        }

        public uint RecordVideo(string cameraCode)
        {
            uint ulRet = 0;
            DateTime dt = DateTime.Now;
            string fileName = $"{cameraCode}_{dt:yyyyMMddHHmmssffff}";
            fileName = AppDomain.CurrentDomain.BaseDirectory + RecodVideoPath.TrimStart('.').TrimStart('\\') + "\\" + fileName;
            IntPtr filepoint = new IntPtr();
            if (IsPlayed)
            {
                if (IsRecording == false)
                {
                    ulRet = IMOSSDK.IMOS_StartRecordEx(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), Encoding.Default.GetBytes(fileName), 0, filepoint);
                    if (0 != ulRet)
                    {
#if DEBUG
                        MessageBox.Show("本地录像失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                        //Logger.Error(typeof(UnvOnePlayer), "本地录像失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));
                    }
                    else
                    {
                        IsRecording = true;

                    }
                    return ulRet;

                }
                else
                {
                    ulRet = IMOSSDK.IMOS_StopRecord(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));

                    if (0 != ulRet)
                    {
#if DEBUG
                        MessageBox.Show("停止本地录像失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                        //Logger.Error(typeof(UnvOnePlayer), "停止本地录像失败,错误码：" + ErrorCode.GetErrorMsg(ulRet));
                    }
                    else
                    {
                        RecodVideoPath = RecodVideoPath.Trim('.');
                        //MsgBox.Show("本地录像保存成功！");
                        string pathName = AppDomain.CurrentDomain.BaseDirectory + VideoLocalConfig.VodDownloadLoc.Trim('.').Trim('\\');
                        if (!FileHelper.IsExistDirectory(pathName))
                            FileHelper.CreateDirectory(pathName);
                        System.Diagnostics.Process.Start("explorer.exe", pathName);
                        IsRecording = false;
                    }
                    return ulRet;
                }
            }
            return ulRet;
        }

        public uint StartVod(URL_INFO_S stURLInfo, IntPtr playWindow)
        {


            uint ulRet = 0;
            var channelCode = Marshal.AllocHGlobal(1 * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
            IMOSSDK.IMOS_GetChannelCode(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, channelCode);
            ChannelCode = Marshal.PtrToStringAnsi(channelCode);
            Marshal.FreeHGlobal(channelCode);

            ulRet = IMOSSDK.IMOS_OpenVodStream(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), stURLInfo.szURL, stURLInfo.stVodSeverIP.szServerIp, stURLInfo.stVodSeverIP.usServerPort, 1);
            if (0 != ulRet)
            {
#if DEBUG
                //MessageBox.Show("IMOS_OpenVodStream打开VOD视频流失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_OpenVodStream打开VOD视频流失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }

            ulRet = IMOSSDK.IMOS_SetDecoderTag(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), stURLInfo.szDecoderTag);
            if (0 != ulRet)
            {
#if DEBUG
                //MessageBox.Show("IMOS_SetDecoderTag设置解码类型失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_SetDecoderTag设置解码类型失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }

            ulRet = IMOSSDK.IMOS_SetPlayWnd(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), playWindow);
            if (0 != ulRet)
            {
#if DEBUG
               // MessageBox.Show("IMOS_SetPlayWnd设置播放窗口句柄失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_SetPlayWnd设置播放窗口句柄失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
                return ulRet;
            }
            ulRet = IMOSSDK.IMOS_StartPlay(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
            if (0 == ulRet)
            {

                IsPlayed = true;
                IsStartedVod = true;

            }
            else
            {
#if DEBUG
                //MessageBox.Show("IMOS_StartPlay 播放VOD失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                Logger.Error(typeof(UnvOnePlayer), "IMOS_StartPlay 播放VOD失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));

            }
            PlayType = PLAY_TYPE_E.PLAY_TYPE_VOD;

            return ulRet;
        }
        //private void timersTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    //TODO: 设置文本视频流信息
        //    //SetLabelText setLabelTextDelegate = new SetLabelText(SetText);
        //    // this.Invoke(setLabelTextDelegate);

        //}
        public uint StopVod()
        {
            uint ulRet = 0;

            if (null != ChannelCode)
            {
                ulRet = IMOSSDK.IMOS_StopPlay(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                if (0 != ulRet)
                {
#if DEBUG
                   // MessageBox.Show("IMOS_StopPlay 停止播放VOD失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                    Logger.Error(typeof(UnvOnePlayer), "IMOS_StopPlay 停止播放VOD失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));

                }
                ulRet = IMOSSDK.IMOS_FreeChannelCode(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                if (0 != ulRet)
                {
#if DEBUG
                    //MessageBox.Show("IMOS_FreeChannelCode 释放VOD播放通道失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                    Logger.Error(typeof(UnvOnePlayer), "IMOS_FreeChannelCode 释放VOD播放通道失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));

                }

            }
            ChannelCode = null;
            if (0 == ulRet)
            {
                ChannelCode = null;
                PlayType = PLAY_TYPE_E.PLAY_TYPE_BLANK;
                IsPlayed = false;
                IsStartedVod = false;

            }
            return ulRet;

        }

        public uint PauseVod()
        {
            uint ulRet = 0;
            if (null != ChannelCode)
            {
                ulRet = IMOSSDK.IMOS_PausePlay(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                if (0 == ulRet)
                {
                    IsPause = true;
                }
                else
                {
#if DEBUG
                    MessageBox.Show("IMOS_PausePlay 暂停播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                   // Logger.Error(typeof(UnvOnePlayer), "IMOS_PausePlay 暂停播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));

                }
            }

            return ulRet;
        }

        public uint ResumeVod()
        {
            uint ulRet = 0;
            if (null != ChannelCode)
            {
                ulRet = IMOSSDK.IMOS_ResumePlay(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                if (0 == ulRet)
                {
                    IsPause = false;
                }
                else
                {
#if DEBUG
                    MessageBox.Show("IMOS_ResumePlay 继续播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
                 //   Logger.Error(typeof(UnvOnePlayer), "IMOS_ResumePlay 继续播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));

                }
            }

            return ulRet;
        }

        public void StopAll()
        {
            //if (IsPlayed)
            //{
            switch (PlayType)
            {
                case PLAY_TYPE_E.PLAY_TYPE_LIVE:
                    StopLive();
                    break;
                case PLAY_TYPE_E.PLAY_TYPE_VOD:
                    StopVod();
                    break;
                default:
                 
                    break;
            }
            
            //}

        }

        public void PlayOnSpeed(uint playSpeed)
        {

            if (null != ChannelCode)
            {
                if (null != SdkManager.GetInstance())
                {
                    SdkManager.GetInstance().SpeedPlay(Encoding.Default.GetBytes(ChannelCode), playSpeed);

                }
            }
        }

        /// <summary>
        /// 单帧播放
        /// </summary>
        public void PlayByOneFrame()
        {

            UInt32 ulRet = 0;
            if (null != ChannelCode)
            {
                ulRet = IMOSSDK.IMOS_OneByOne(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
            }

            if (0 != ulRet)
            {
#if DEBUG
                MessageBox.Show("IMOS_OneByOne 单帧播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
#endif
              //  Logger.Error(typeof(UnvOnePlayer), "IMOS_OneByOne 单帧播放失败，错误码为：" + ErrorCode.GetErrorMsg(ulRet));
            }
        }

        public bool DigitalZoom(IntPtr ptrWin, XP_RECT_S xp_rec)
        {
            uint num = IMOSSDK.IMOS_SetDigitalZoom(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, this.ChannelCode, ptrWin, ref xp_rec);
            if (num != 0u)
            {
              //  Logger.Error(GetType(), "打开数字放大失败:" + ErrorCode.GetErrorMsg(num));
            }
            else
            {
                IsDigitalZoom = true;
            }

            return num == 0u;
        }

        public bool CloseDigitalZoom()
        {
            uint num = IMOSSDK.IMOS_SetDigitalZoom(ref SdkManager.GetInstance().stLoginInfo.stUserLoginIDInfo, this.ChannelCode, (IntPtr)null, (IntPtr)null);
            if (num != 0u)
            {
               // Logger.Error(GetType(), "关闭数字放大失败:" + ErrorCode.GetErrorMsg(num));
            }
            else
            {

                IsDigitalZoom = true;
            }

            return num == 0u;
        }

    }

}
