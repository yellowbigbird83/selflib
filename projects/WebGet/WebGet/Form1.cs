using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region button
        private void butGet_Click(object sender, EventArgs e)
        {
            UtilWeb.Get().ProxyUrl = "localhost";
            UtilWeb.Get().ProxyPort = 3128;

            string url = textBoxUrl.Text;
            bool fUseProxy = checkBoxUseProxy.Checked;
            string strresp = UtilWeb.Get().GetResponse(url, fUseProxy);
            textBoxResponse.Text = strresp;
        }

        private void butParse_Click(object sender, EventArgs e)
        {
            string strResp = textBoxResponse.Text;
            HtmlParser parser = HtmlParser.Get();
            string strUrl = "";

            for (int idx = 0; idx < 3; idx++)
            {
                if (string.IsNullOrWhiteSpace(strResp))
                    break;

                parser.ParseHtml(strResp, ref strUrl);
                //strUrl = parser.StrNextPageUrl;
                strResp = UtilWeb.Get().GetResponse(strUrl, false);

            }
        }
        #endregion

        private void butTestDownloadFile_Click(object sender, EventArgs e)
        {
            string strPath = System.Windows.Forms.Application.StartupPath + "\\img\\";
            string strUrl = "https://65.media.tumblr.com/61c4f9ed2274b7650c28465efae69224/tumblr_oajys4a9CL1vuu8cto1_540.jpg";
            HttpDownloader http = new HttpDownloader(strPath);
            http.DownloadFile(strUrl);
        }

        private void butPreview_Click(object sender, EventArgs e)
        {
            FormPreview form1 = new FormPreview();
            form1.ShowDialog();
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            int pageMax = 0;
            int.TryParse(textBoxPageMax.Text, out pageMax);

            HtmlParser.Get().StartParse(textBoxUrl.Text
                , false
                , pageMax
                );
            ImageBuffer.Get().StartDownload();
        }
    }
}
