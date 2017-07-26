using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGet
{
    public class ImageData
    {
        public string Url { get; set; }
        /// <summary>
        /// file name without number
        /// </summary>
        public string FileName { get; set; }
        public string FileExt { get; set; }
        public string FileName400 { get; set; }
        public string FileName500 { get; set; }
        public string FileName540 { get; set; }
        public string FileName1280 { get; set; }

        //todo, int32
        public int FileSize { get; set; }
        public int Width {get;set;}
        public int Height {get;set;}

        public bool SetUrl(string strUrl)
        {
            //update url
            Url = strUrl;
            string fileName = Path.GetFileName(Url);
            FileExt = Path.GetExtension(fileName);

            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            int posDot = fileName.LastIndexOf(".");
            if (posDot < 0 || posDot > fileName.Length)
                return false;

            int pos0 = fileName.LastIndexOf("_", posDot -1);
            if (pos0 < 0 || pos0 > fileName.Length)
                return false; 

            string strNumber = fileName.Substring(pos0+1 , posDot - pos0 - 1);
            int oldNumber = 0;
            if (string.IsNullOrWhiteSpace(strNumber)
                || !int.TryParse(strNumber, out oldNumber)
                )
                return false;

            //remove number
            FileName = fileName.Replace("_"+strNumber, "");
            FileName400 = fileName.Replace(strNumber, "400");
            FileName500 = fileName.Replace(strNumber, "500");
            FileName540 = fileName.Replace(strNumber, "540");
            FileName1280 = fileName.Replace(strNumber, "1280");
            return true;
        }

        public void Download(
            //string strPath
            bool isPreview
            , HttpDownloader http
            )
        {
            if (http == null)
                return;

            //string strPath = System.Windows.Forms.Application.StartupPath + "\\image\\";

            //HttpDownloader http = new HttpDownloader(strPath);

            string strUrl = isPreview ? Url: Url; 
            http.DownloadFile(strUrl);            
        }

        public static string GetSavePath()
        {
            string strPath = System.Windows.Forms.Application.StartupPath + "\\image\\" + DateTime.Now.ToString("yyyy_MM_dd") + "\\";
            return strPath;
        }

        public string GetFileNamePreview()
        {
            return FileName400;
        }

        public string GetFilePathName(bool isPreview)
        {
            string strPath = GetSavePath();
            string fileName = isPreview ? GetFileNamePreview() : FileName1280;
            string strPathName = strPath + fileName;
            return strPathName;
        }
    }
}
