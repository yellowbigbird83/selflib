using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebGet
{
    public class HtmlParser
    {
        //public HashSet<string> HashImage { get; set; }
        //public Dictionary<string, ImageData> DictImage { get; set; }
        public string StrHtml { get; set; }
        //public string StrNextPageUrl { get; set; }
        public int PageMax { get; set; }
        public bool StopFlag { get; set; }
        public string StrUrl { get;set;}
        public bool UseProxy { get; set; }
        public int ThreadCnt { get; set; }
        
        private HtmlParser()
        {
            //HashImage = new HashSet<string>();
            //DictImage = new Dictionary<string, ImageData>();
            PageMax = 3;
            ThreadCnt = 1;
        }

        private static HtmlParser _instance = null;
        public static HtmlParser Get()
        {
            if (_instance == null)
            {
                _instance = new HtmlParser();
            }
            return _instance;
        }

        public void StartParse(string strUrl
            , bool useProxy
            ,int pageMax
            )
        {
            if (pageMax < 1)
                pageMax = 1;
            PageMax = pageMax;

            StopFlag = false;
            StrUrl = strUrl;
            UseProxy = useProxy;

            UtilWeb.Get().ProxyUrl = "localhost";
            UtilWeb.Get().ProxyPort = 3128;

            //start thread
            ManualResetEvent handle = new ManualResetEvent(false);

            for (int i = 0; i < ThreadCnt; i++)
            {
                Thread thread = new Thread(ThreadDownload);
                thread.Start(handle);
            }
        }

        public void ThreadDownload(object obj)
        {
            ManualResetEvent handle = obj as ManualResetEvent;

            string curUrl = StrUrl;
            string nextPageUrl = "";
            string strResp;
            int pageIdx = 0;

            while (!StopFlag
                && pageIdx < PageMax)
            {
                strResp = UtilWeb.Get().GetResponse(curUrl, UseProxy);
                if (string.IsNullOrWhiteSpace(strResp))
                {
                    strResp = UtilWeb.Get().GetResponse(curUrl, UseProxy);
                }
                if (string.IsNullOrWhiteSpace(strResp))
                {
                    break;
                }

                ParseHtml(strResp, ref nextPageUrl);
                curUrl = nextPageUrl;
                pageIdx++;
            } //while
        }

        public void ParseHtml(string strHtml
            , ref string strNextPageUrl)
        {
            StrHtml = strHtml;
            int lastPos = 0;
            int endPos =0 ;
            while(true)
            {
                string strUrl = FindImgUrl(strHtml
                    , lastPos
                    , ref endPos);
                lastPos = endPos;

                if(endPos >= strHtml.Length)
                    break;
                if (string.IsNullOrWhiteSpace(strUrl))
                    continue;
                //if (!HashImage.Contains(strUrl))
                //    HashImage.Add(strUrl);

                ImageData img = new ImageData();
                bool fok = img.SetUrl(strUrl);
                if (!fok)
                    continue;

                //if (!DictImage.ContainsKey(img.FileName))
                //{
                //    DictImage.Add(img.FileName, img);
                //}
                ImageBuffer.Get().Add(img);
            }

            strNextPageUrl = FindNextPageUrl(strHtml);

            //DownloadAllImage(DictImage);
        }

        public string FindImgUrl(string str
            , int startPos
            , ref int endPos
            )
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            //find start
            string strPart0 = "src=\"";
            int pos0 = str.IndexOf(strPart0, startPos);
            if (pos0 < 0
                || pos0 >= str.Length)
            {
                endPos = str.Length + 1;
                return "";
            }
            pos0+= strPart0.Length;
            endPos = pos0 + 1;

            //find end
            int pos1 = str.IndexOf("\"", pos0 );
            if (pos1 < 0
                || pos1 >= str.Length)
            {
                endPos = str.Length + 1;
                return "";
            }

            string strUrl=str.Substring(pos0 , pos1-pos0);
            endPos = pos1 + 1;

            int pos2 = strUrl.LastIndexOf(".");
            if (pos2 < 0
                || pos2 >= strUrl.Length)
                return string.Empty;

            string strbrix = strUrl.Substring(pos2+1);
            if (strbrix.Equals("gif", StringComparison.OrdinalIgnoreCase)
                || strbrix.Equals("jpg", StringComparison.OrdinalIgnoreCase)
                || strbrix.Equals("gif", StringComparison.OrdinalIgnoreCase)
                || strbrix.Equals("mp4", StringComparison.OrdinalIgnoreCase)
                )
                return strUrl;

            return string.Empty;
        }

        public static string StrDomain = "https://www.tumblr.com";
        protected string FindNextPageUrl(string strHtml)
        {
            if (string.IsNullOrWhiteSpace(strHtml))
                return "";

            string strUrl = "";

            string strNextPage = "next_page_link";
            int pos0 = strHtml.IndexOf(strNextPage, 0);
            if (pos0 < 0
                || pos0 >= strHtml.Length)
                return "";

            string strHref = "href=\"";
            int pos1 = strHtml.IndexOf(strHref, pos0 + strNextPage.Length);
            if (pos1 < 0
                || pos1 >= strHtml.Length)
                return "";

            pos1+= strHref.Length;
            int pos2 = strHtml.IndexOf("\"", pos1 +1);
            if (pos2 < 0
                || pos2 >= strHtml.Length)
                return "";
            string strUrlPart = strHtml.Substring(pos1, pos2-pos1);
            strUrl = StrDomain + strUrlPart;

            return strUrl;
        }

        
    }
}
