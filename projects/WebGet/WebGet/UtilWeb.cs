using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebGet
{
    class UtilWeb
    {
        private static UtilWeb g_instance = null;

        public string ProxyUrl { get; set; }
        public int ProxyPort { get; set; }

        public bool IsUseProxy { get; set; }
        public static UtilWeb Get()
        {
            if (g_instance == null)
            {
                g_instance = new UtilWeb();
            }
            return g_instance;
        }
        
        public string GetResponse(string strUrl, bool isUseProxy)
        {
            HttpWebRequest webreq = GetWebRequestor(strUrl, isUseProxy);
            string response = Download(webreq);
            return response;
        }

        public static HttpWebRequest GetWebRequestor(string apiUrl, bool isUseProxy)
        {
            HttpWebRequest requestor = null;
            requestor = (HttpWebRequest)WebRequest.Create(apiUrl);
            //Https分支
            if (apiUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                requestor.ProtocolVersion = HttpVersion.Version11;
            }

            CookieContainer cookieContainer = new CookieContainer();
            requestor.CookieContainer = cookieContainer;
            SetCookie(cookieContainer);

            requestor.Method = "Get";
            requestor.KeepAlive = true;
            //if (apiConfig.ReqType == RequestTypeEnum.UseJson)
            //{
            //    requestor.ContentType = "text/json;charset=UTF-8";
            //}
            //else if (apiConfig.ReqType == RequestTypeEnum.UseXml)
            {
                //soap 1.1 could use this
                requestor.ContentType = "text/xml;charset=UTF-8";
            }
            //else if (apiConfig.ReqType == RequestTypeEnum.UseSoap12)
            //{
            //    requestor.ContentType = "text/soap+xml;charset=UTF-8";
            //}
            //else
            //{
            //}

            //basic认证分支
            //if (apiConfig.UseBasic)
            //{
            //    string basicCode = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", apiConfig.ApiUserName, apiConfig.ApiUserPw)));
            //    requestor.Headers.Add("Authorization", "Basic " + basicCode);
            //}

            requestor.Headers.Add("Accept-Encoding", "gzip, deflate");

            //是否需要SOAPAction，SOAPAction可能对于不同的请求不一样
            //if (!string.IsNullOrWhiteSpace(apiConfig.SoapAction))
            //{
            //    requestor.Headers.Add("SOAPAction", apiConfig.SoapAction);
            //}

            //代理分支
            if (isUseProxy)
            {
                //WebProxy proxy = new WebProxy(string.Format("{0}:{1}", apiConfig.ProxyUrl, apiConfig.ProxyPort), true);
                //if (!string.IsNullOrWhiteSpace(apiConfig.ProxyUserName))
                //{
                //    proxy.Credentials = new NetworkCredential(apiConfig.ProxyUserName, apiConfig.ProxyUserPw, "cn1.global.ctrip.com");
                //}
                //requestor.Proxy = proxy;
            }

            //Timeout
            requestor.Timeout = 20 * 1000;
            return requestor;
        }
        //protected HttpWebRequest CreateRequest(string url
        //    , bool isUseProxy
        //    )
        //{

        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        //    if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
        //        request.ProtocolVersion = HttpVersion.Version11;
        //    }

        //    CookieContainer cookieContainer = new CookieContainer();
        //    request.CookieContainer = cookieContainer;
        //    //SetCookie(domain, path, cookieContainer);
        //    request.Headers.Add("Accept-Encoding", "gzip, deflate");
        //    if (request.RequestUri.AbsoluteUri.StartsWith("https"))
        //    {
        //        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //    }
        //    request.Method = "GET";
        //    request.KeepAlive = true;
        //    //request.Host = "dot.ops.ctripcorp.com";
        //    request.ProtocolVersion = HttpVersion.Version11;
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:43.0) Gecko/20100101 Firefox/43.0";
        //    request.Accept = "application/json, text/javascript, */*; q=0.01";
        //    request.ContentType = "text/html; charset=utf-8";
        //    //request.contentend //gzip

        //    //request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
        //    //request.Headers.Add("X-Requested-With", "XMLHttpRequest");
        //    //request.Referer = "http://dbquery.ops.ctripcorp.com/Home/Index";
        //    //request.MediaType = "application/x-www-form-urlencoded";

        //    if (isUseProxy)
        //    {
        //        WebProxy proxy = new WebProxy(string.Format("{0}:{1}", ProxyUrl, ProxyPort), true);
        //        //if (!string.IsNullOrWhiteSpace(userName))
        //        //{
        //        //    proxy.Credentials = new NetworkCredential(userName, userPwd, "cn1.global.ctrip.com");
        //        //}
        //        request.Proxy = proxy;
        //    }
            
        //    return request;
        //}

        /// <summary>
        /// return a string of file
        /// </summary>
        /// <returns></returns>
        public bool DownloadFile(string url
            , string strPath)
        {
            string strContent = GetResponse(url, IsUseProxy);
            if (string.IsNullOrWhiteSpace(strContent))
                return false;

            //save file
            try
            {
                string saveFileName = Path.GetFileName(url);
                string filePathName = strPath + saveFileName;
                if (File.Exists(filePathName))
                    return false;

                byte[] array = Encoding.Default.GetBytes(strContent);
                MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
                //StreamReader reader = new StreamReader(stream);
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
            }
            catch(Exception ex)
            {
            }
            return true;
        }

        private string Download(HttpWebRequest request)
        {
            string responseString = string.Empty;
            try
            {
                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = httpWebResponse.GetResponseStream())
                    {
                        if (httpWebResponse.ContentEncoding.Equals("gzip", StringComparison.OrdinalIgnoreCase))
                        {
                            using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress))
                            {
                                using (StreamReader streamReader = new StreamReader(gzipStream))
                                {
                                    responseString = streamReader.ReadToEnd();
                                }
                            }
                        }
                        else
                        {
                            using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                            {
                                responseString = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                responseString = ex.ToString();
            }

            return responseString;
        }


        private static void SetCookie(CookieContainer cookieContainer)
        {
            //bird
            //string cookieStr = string.Format(@"_ga={0}; _bfa={1}; .dot={2}; ASP.NET_SessionSvc={3}; ASP.NET_SessionId={4}; _bfi="
            //    , "GA1.2.1344985777.1435659110"//ga
            //    , "1.1435906151521.5ygd2.1.1440662135256.1450230682837.12.51"//bfa
            //    , "E8865263A36748A8D231A8673A43988F365E80F35759421C9626985D370FB3F9EE52DF0B10179C4F3BA9F13B288AFD9742F6DD4EFA72483DC98AC1E9DBD36D96DDFED2329CE758BC9F013025B5B7FDE3D773B304D530BCBBFBF3CD1EA936DB87812917D43ED4B8E34711A6EF91697E9F0CBD7838DA8C19350816ED8B69821DCB1C276115E2C7B88782C0E87C5898DBB56DD223052E20FC5F2C9C0CC3CB77885349EBB0D802B4936E25EAC973EFD5E933DBB9A90B" //.dot
            //    , "MTAuMTUuMTM2LjMyfDkwOTB8b3V5YW5nfGRlZmF1bHR8MTQ0ODk2NTQyNDgzNQ" //ASP.NET_SessionSvc
            //    , "jkxzunuq435tfarj55w3x1af"  //ASP.NET_SessionId
            //    , "ffffffff09076f3645525d5f4f58455e445a4a423660"  //bfi
            //    );
            //bird //"_ga=GA1.2.1344985777.1435659110; _bfa=1.1435906151521.5ygd2.1.1440662135256.1450230682837.12.51; .dot=58010C8A6E22212B8EE1029B222627294DB166E1C7BA17BB86BE753D05533346C10AF4CEF01FD64A9233D01C9B95B86770C4B1769B72FF6C4B2A2A6D1A01FFD3F7E63FB8961FF3F11CDC15D25740C149229533D160E16782ECC98B77FC62B478B0ED5615395635CD2D8D8BC3C2BAAAB8E7F381CACA52E28728151F467D584BA97C4DD197CC3AFDBE6CE165541CA88F0B701DDF9C1FFDFC7F9245B33ACC4C8293BBA70512CF41DCDBA39F747DB5F910176C672E90; ASP.NET_SessionSvc=MTAuMTUuMTM2LjMyfDkwOTB8b3V5YW5nfGRlZmF1bHR8MTQ0ODk2NTQyNDgzNQ; ASP.NET_SessionId=jkxzunuq435tfarj55w3x1af; _bfi=p1%3D102104%26p2%3D102102%26v1%3D51%26v2%3D49";
            //karl //"_ga=GA1.2.1711721784.1438048817; _bfa=1.1440986308339.3m0vlm.1.1450361443662.1452151083924.3.4; .dot=FE34DCA67563345A62933432D8A0EAE8CB4F6931BF264BA92A6DD98367AB1E86206F327B5CC3724A1DE25A0B53D5C831DAEBF12F011BA175686C9856D472B5E6E4F39D6881CC0B2E91A87F630BE2748DB42CE4E4B1A96B43E3CA888F4B26F99E23079A3A386CF189D2A6F66E0E438DC43AE6BF2556D17B1B6405438DB03462CE04C271E29BB95B5AE21D20AC797FF92B0BFAD9374C5AD36BA9EE127385799B119A78CC81; ASP.NET_SessionSvc=MTAuMTUuMTM2LjI1fDkwOTB8b3V5YW5nfGRlZmF1bHR8MTQ0OTA1MTEzNzk2NQ; ASP.NET_SessionId=3wzmg00jjv5zd40ghdq1wkga; _bfi=p1%3D88890106%26p2%3D0%26v1%3D4%26v2%3D0";
            //cookieContainer.Add(new Uri(url), new Cookie("ASP.NET_SessionId", "3wzmg00jjv5zd40ghdq1wkga"));
            //cookieContainer.Add(new Uri(url), new Cookie("ASP.NET_SessionSvc", "MTAuMTUuMTM2LjI1fDkwOTB8b3V5YW5nfGRlZmF1bHR8MTQ0OTA1MTEzNzk2NQ"));

            string cookieStr = "tmgioct=57029c4db882250634098550; rxx=1n1ebmnwywv.9ty0awm&v=1; logged_in=1; last_toast=1463666152; language=%2Czh_CN; anon_id=IDXNOXXCYHTSXSXBMSRGSXUYWVEPKYRR; pfp=iFHUXvbj45f2nnVck3bAH1dVf81Ke97HFrU8P5jz; pfs=u4RjuQT69WAkoYZaYAG83cCK1k; pfe=1475497061; pfu=135655774; devicePixelRatio=1; documentWidth=1399; capture=C4Z9kgKzi6q0BqH6SFw3YneZs4A; _ga=GA1.2.484574220.1459788882; __utma=189990958.484574220.1459788882.1470132396.1470141162.142; __utmc=189990958; __utmz=189990958.1465013795.73.5.utmcsr=fetishhand.tumblr.com|utmccn=(referral)|utmcmd=referral|utmcct=/; yx=4iqz5egot572v%26o%3D3%26f%3Df3";

            if (!string.IsNullOrEmpty(cookieStr))
            {
                string[] cookies = cookieStr.Split(new char[] { ';' });
                if (cookies.Length > 0)
                {
                    foreach (string cookieItem in cookies)
                    {
                        string[] pair = cookieItem.Trim().Split(new char[] { '=' });
                        //cookieContainer.Add(uri, new Cookie(pair[0].Trim(), pair[1].Trim())); 
                        try
                        {
                            string strName = pair[0].Trim();
                            string strValue = pair[1].Trim();
                            if (string.IsNullOrWhiteSpace(strName)
                                || string.IsNullOrWhiteSpace(strValue))
                                continue;
                            Cookie cookie = new Cookie(strName, strValue);
                            cookie.Domain = "www.tumblr.com";
                            cookieContainer.Add(cookie);
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
            }
        }

        private static string GetResponseText(HttpWebResponse httpWebResponse)
        {
            string textRsp = string.Empty;
            if (httpWebResponse.ContentEncoding.ToLower().Contains("gzip"))
            {
                var gZipStream = httpWebResponse.GetResponseStream();
                var gZipBytes = Decompress(gZipStream);

                textRsp = System.Text.Encoding.UTF8.GetString(gZipBytes);
            }
            else
            {
                StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
                textRsp = reader.ReadToEnd();
            }

            return textRsp;
        }

        private static byte[] Decompress(Stream stream)
        {
            GZipStream compressedStream = new GZipStream(stream, CompressionMode.Decompress, true);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedStream.Read(block, 0, block.Length);
                if (bytesRead <= 0) 
                    break;
                outBuffer.Write(block, 0, bytesRead);
            }
            compressedStream.Close();

            return outBuffer.ToArray();
        }
    }
}
