using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementProject.Helper
{
    public class ExcelHelper
    {
        private static string connectionString;

        public static string GetConnString(string fileExtension, string path)
        {
            switch (fileExtension.ToUpper())
            {
                case ".XLS":
                    connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=
                                        " + path + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
                    break;
                case ".XLSX":
                    connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=
                                        " + path + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    break;
                default:
                    break;
            }
            return connectionString;
        }

        #region 释放Excel进程
        public bool KillAllExcel(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            try
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    //释放COM组件，其实就是将其引用计数减1   
                    //System.Diagnostics.Process theProc;   
                    foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        //先关闭图形窗口。如果关闭失败.有的时候在状态里看不到图形窗口的excel了，   
                        //但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要释放它   
                        if (theProc.CloseMainWindow() == false)
                        {
                            theProc.Kill();
                        }
                    }
                    excelApp = null;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
