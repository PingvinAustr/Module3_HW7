using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_HW7
{
    public class BackupHandler
    {
        public static async Task CreateBackup(string logs)
        {
            string fileName = DateTime.Now.ToString(
                "HH.mm.ss.ffffff dd.MM.yyyy")
                + ".txt";
            ConfigController.ValidateConfig();
            Directory.CreateDirectory(@"..\\..\\..\\" + "Backup");
            FileInfo fileInf = new FileInfo(
                @"..\\..\\..\\" + "Backup" + "\\"
                + fileName);

            FileStream fs = fileInf.Create();
            fs.Close();
            File.AppendAllText(
               fileInf.FullName, logs);
        }
    }
}
