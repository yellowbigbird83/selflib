using ICSharpCode.SharpZipLib.Zip;
//using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebGet
{
    public class HttpDownloader
    {
        private string FilePath { get; set; }

        protected string ProxyUrl { get; set; }
        protected int ProxyPort { get; set; }
        protected string ProxyUserName { get; set; }
        protected string ProxyUserPwd { get; set; }
        
        public HttpDownloader(string filePath
            , string proxyUrl = ""
            , int proxyPort = 0
            , string userName = ""
            , string userPwd = ""
            )
        {
            //ConfigName = configName;
            FilePath = filePath;

            ProxyUrl = proxyUrl;
            ProxyPort = proxyPort;
            ProxyUserName = userName;
            ProxyUserPwd = userPwd;
        }


        public bool DownloadFile(string urlFileName
            , bool isReWriteFile = false)
        {
            if (string.IsNullOrWhiteSpace(urlFileName))
                return false ;

            bool fok = false;
            try
            {
                string saveFileName = Path.GetFileName(urlFileName) ;
                string filePathName = FilePath + saveFileName;
                //LogVeiwerHelper.WriteInfo("KNT房态房价更新", filePathName);

                UtilFile.CreateFolder(FilePath);
                                
                if (File.Exists(filePathName))
                {
                    if (!isReWriteFile)
                        return false;
                }
                else{
                    using (StreamWriter sw = File.CreateText(filePathName))
                    {
                    }
                }

                HttpWebRequest request = RequestGetService(urlFileName
                    , ProxyUrl
                    , ProxyPort
                    , ProxyUserName
                    , ProxyUserPwd
                    , 600000);// (HttpWebRequest)HttpWebRequest.Create(filename);
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    using (FileStream fs = File.Create(filePathName))
                    {
                        byte[] bytes = new byte[102400];
                        int n = 1;
                        while (n > 0)
                        {
                            n = stream.Read(bytes, 0, 10240);
                            fs.Write(bytes, 0, n);
                        }
                    }
                }
                fok = true;
            }
            catch (Exception ex)
            {
                fok = false;
                //LogVeiwerHelper.WriteException("KNT房态房价更新", "下载错误:" + ex.ToString());
            }
            return fok;
        }

        /// <summary>
        /// Get 方式请求, 
        /// can't support https now
        /// </summary>
        /// <param name="serviceUrl">链接地址</param>
        /// <returns>WebRequest</returns>
        public static HttpWebRequest RequestGetService(string serviceUrl
            , string proxyUrl
            , int proxyPort
            , string userName
            , string userPwd
            , int timeout = 30000)
        {
            HttpWebRequest myWebRequest = null;

            //如果是发送HTTPS请求  
            //if (serviceUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            //{
            //    ServicePointManager.ServerCertificateValidationCallback =
            //        new RemoteCertificateValidationCallback(delegate { return true; });
            //    myWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //    myWebRequest.ProtocolVersion = HttpVersion.Version10;
            //}
            //else
            {
                myWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            }
            myWebRequest.Method = "Get";
            myWebRequest.ContentType = "text/xml;charset=UTF-8";
            
            //代理分支
#if !DEBUG
            WebProxy proxy = new WebProxy(string.Format("{0}:{1}", proxyUrl, proxyPort), true);
            if (!string.IsNullOrWhiteSpace(userName))
            {
                proxy.Credentials = new NetworkCredential(userName, userPwd, "cn1.global.ctrip.com");
            }
            myWebRequest.Proxy = proxy;            
#endif
            return myWebRequest;
        }

        public bool CanDecompress(string filePath)
        {
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(filePath)))
            {
                return s.CanDecompressEntry;
            }
        }

        public List<string> Decompress(string filePath, bool justReadFile)
        {
            List<string> outputFileNameList = new List<string>();
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(filePath)))
            {
                ZipEntry theEntry;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (!string.IsNullOrEmpty(pathToZip))
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);
                    if (!Directory.Exists(FilePath + directoryName))
                    {
                        Directory.CreateDirectory(FilePath + directoryName);
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if ((File.Exists(FilePath + directoryName + fileName)) || (!File.Exists(FilePath + directoryName + fileName)))
                        {
                            if (!justReadFile)
                            {
                                using (FileStream streamWriter = File.Create(FilePath + directoryName + fileName))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while ((size = s.Read(data, 0, data.Length)) > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                }
                            }
                            outputFileNameList.Add(FilePath + directoryName + fileName);
                        }
                    }
                }
            }
            return outputFileNameList;
        }
    }
}
