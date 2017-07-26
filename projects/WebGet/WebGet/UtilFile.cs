using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGet
{
    public class UtilFile
    {
        public static void CreateFolder(string strPath)
        {
            if (string.IsNullOrWhiteSpace(strPath))
                return;
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
        }
    }
}
