using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace TubeWriter3_WPF.Interfaces
{
    public static class FileHandler
    {
        private static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static string excelPath = System.IO.Path.Combine(currentDirectory, @"..\..\..\");

        public static bool Save(IWorkbook workbook)
        {
            Directory.SetCurrentDirectory(excelPath);

            try
            {
                FileInfo fi = new FileInfo("Tubes.xlsx");
                if (fi.Exists)
                {
                    File.Delete("Tubes.xlsx");
                }

                using (FileStream stream = new FileStream("Tubes.xlsx", FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                }

                return true;

                //_ = System.Diagnostics.Process.Start("OpenFile.bat");
            }
            catch(System.IO.IOException)
            {
                return false;
            }
        }

        public static bool Open()
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C start excel {excelPath}\\Tubes.xlsx";
                process.StartInfo = startInfo;
                process.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
