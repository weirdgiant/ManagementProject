using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SFAI.Contents;

namespace SFAI.sdk
{
    public class PlayStatus
    {
        public List<RecUrlInfo> UrlList = new List<RecUrlInfo>();
       // public PLAY_TYPE_E PlayType;
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public int CurUrlIndex = 0;
        public DateTime CurTime;
        public bool IsPlayed = false;
      
    }
    public class PlayBackArg
    {
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime OffSetTime { get; set; }
        public string CameraCode { get; set; }

        public List<RecUrlInfo> UrlList=new List<RecUrlInfo>();

    }

    public class RecUrlInfo
    {
        //public URL_INFO_S UrlInfo;
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
