using System;
using System.Collections.Generic;
using LiteDB;

namespace ManagementProject
{
    /// <summary>
    /// 本地配置文件
    /// </summary>
    public class VideoLocalConfig
    {
        //回放下载地址
        public static string VodDownloadLoc = ".\\Download\\";
        //截图保存地址
        public static string PicSnatchLoc = ".\\Snatch\\";
        //抓拍格式,0为BMP格式，1为JPG格式
        public static uint PicFormat = 0;
        //录像回放传输协议
        public static XP_PROTOCOL_E VodProtocol = XP_PROTOCOL_E.XP_PROTOCOL_TCP;
        //录像下载速度
        public static XP_DOWN_MEDIA_SPEED_E DownloadSpd = XP_DOWN_MEDIA_SPEED_E.XP_DOWN_MEDIA_SPEED_EIGHT;
        //录像下载格式,0为TS,1为FLV
        public static uint DownloadFormat = 0;
        //场模式，单场为0,双场为1
        public static int FieldMode = 0;
        //启动UDP乱序整理模式
        public static Boolean PktSeq = false;
        //渲染模式,0为D3D，1为DDRAW
        public static uint RenderMode = 0;
        //实时性还是流畅性优先,0为实时性优先，1为流畅性优先
        public static uint Fluency = 0;
        //视频输出格式，0为YUV,1为RGB32
        public static uint PixelFormat = 0;
        //xp流配置

    }
    public enum PlayPanelMode
    {
        One = 1,
        Four = 4,
        Six = 6,
        Eight = 8,
        Nine = 9,
        Sixteen = 16
    }
    public class PlayPanelVideoLayOut
    {
        public PlayPanelMode PlayPanelMode { get; set; }
        public List<VideoWindowConfig> VideoWindows { get; set; }

    }
    public class VideoWindowConfig
    {
        public int WindowId { get; set; }
        public string CameraCode { get; set; }
    }
    public static class VideoWindowConfigDBOperation
    {
        public static PlayPanelVideoLayOut GetVideoLayOut(string connectString)
        {
            using (var db = new LiteDatabase(connectString))
            {
                var col = db.GetCollection<PlayPanelVideoLayOut>();
                var config = col.FindById(0) ?? new PlayPanelVideoLayOut { VideoWindows = new List<VideoWindowConfig>() };
                return config;
            }
        }
        public static void SetVideoLayOut(PlayPanelVideoLayOut layOut, string connectString)
        {
            using (var db = new LiteDatabase(connectString))
            {
                var col = db.GetCollection<PlayPanelVideoLayOut>();
                if (col.Count() == 0)
                    col.Insert(0, layOut);
                else
                {
                    col.Update(0, layOut);
                }

            }
        }
    }

}
