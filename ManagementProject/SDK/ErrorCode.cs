using System;
using System.Collections.Generic;
using System.IO;

namespace ManagementProject
{
    class ErrorCode
    {
        static Dictionary<uint, string> dict = new Dictionary<uint, string>();
        public static void Initialize()
        {
            if(File.Exists(".\\SDK\\ErrorCode.txt"))
            {
                using (var fs = File.OpenText(".\\SDK\\ErrorCode.txt"))
                {
                    while (!fs.EndOfStream)
                    {
                        string str = fs.ReadLine();
                        var strArray = str.Split(',');
                        uint key = uint.Parse(strArray[0]);
                        string valuestr = strArray[1];
                        dict.Add(key, valuestr);

                    }

                }
            }
         
        }
        public static string GetErrorMsg(uint key)
        {
            string value = null;
            try
            {
                value = dict[key];
            }
            catch (KeyNotFoundException ex)
            {
                value = key.ToString();
                Console.WriteLine(ex.ToString());
            }
            return value;

        }
    }
}
