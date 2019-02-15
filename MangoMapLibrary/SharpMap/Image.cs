using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MangoMapLibrary
{
    public class Util
    {
        public static Bitmap LoadImage(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest request = webRequest as HttpWebRequest;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            Bitmap ret = new Bitmap(reader.BaseStream);
            return ret;
        }
        
        
    }
    
    
}
