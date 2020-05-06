using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject
{
    public class FileHelper
    {
        public static bool StringArrayCompare(string[] one, string[] anothor)
        {
            if (one == null && anothor == null)
            {
                return true;
            }
            else if (one != null && anothor != null)
            {

                if (one.Count() != anothor.Count())
                    return false;
                for (int i = 0; i < one.Count(); i++)
                {
                    if (one[i] != anothor[i])
                    {
                        return false;
                    }

                }

            }
            else
            {
                return false;
            }
            return true;

        }

        public static string[] GetFile(string directoryPath)
        {
            //如果目录不存在，则抛出异常  
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }


            //获取文件列表  
            return Directory.GetFiles(directoryPath);
        }

        public static IEnumerable<string> GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常  
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表  
            return Directory.EnumerateFiles(directoryPath);
        }
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录  
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

    }
}
