using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebGet
{
    public partial class FormPreview : Form
    {
        public List<Button> ListButton { get; set; }
        public int CurrentPageIdx { get; set; }

        public FormPreview()
        {
            InitializeComponent();
            ListButton = new List<Button>();

            FillPanelWithButton();
            FillButtonWithImage();

            CurrentPageIdx = 0;
        }

        #region button
        private void butAddButton_Click(object sender, EventArgs e)
        {
            FillPanelWithButton();  
        }

        private void butNextPage_Click(object sender, EventArgs e)
        {
            int imgCount1page = ListButton.Count();
            int imgIdx = (CurrentPageIdx + 1) * imgCount1page;
            ImageData img = ImageBuffer.Get().GetImage(imgIdx);
            if (img == null)
                return;

            CurrentPageIdx++;
            FillButtonWithImage();

            textBoxPageIdx.Text = CurrentPageIdx.ToString();
        }

        private void butPreviousPage_Click(object sender, EventArgs e)
        {
            if (CurrentPageIdx < 1)
                return;

            CurrentPageIdx--;
            FillButtonWithImage();
            textBoxPageIdx.Text = CurrentPageIdx.ToString();
        }

        private void butImage_Click(object sender, EventArgs e)
        {
            //check which button
            Button but = sender as Button;
            if (but == null)
                return;
            string strIdx = but.Text;
            int imgIdx = 0;
            int.TryParse(strIdx, out imgIdx);
            
            ImageBuffer.Get().SetImageDownloadHd(imgIdx);
        }
        #endregion

        protected void FillPanelWithButton()
        {
            ListButton.Clear();

            int rowNum = tableLayoutPanel1.RowCount;
            int colNum = tableLayoutPanel1.ColumnCount;
            //Image img = Image.FromFile("X:\\test.jpg");
            int butIdx = 0;

            for (int rowIdx = 0; rowIdx < rowNum; rowIdx++)
            {
                for (int colIdx = 0; colIdx < colNum; colIdx++)
                {
                    Button but = new Button();
                    //but.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
                    //but.BackgroundImage = img;

                    but.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    but.Location = new System.Drawing.Point(3, 3);
                    but.Name = string.Format("but_{0}", butIdx);
                    but.Size = new System.Drawing.Size(273, 165);
                    //but.TabIndex = 9;
                    but.UseVisualStyleBackColor = true;
                    but.Text = butIdx.ToString();
                    but.Click += new System.EventHandler(this.butImage_Click);

                    tableLayoutPanel1.Controls.Add(but, rowIdx, colIdx);
                    
                    ListButton.Add(but);
                    butIdx++;
                }
            }     
        }

        protected void FillButtonWithImage()
        {
            int imgCount1page = ListButton.Count();
            int imgIdx = CurrentPageIdx * imgCount1page-1;

            foreach(Button but in ListButton)
            {
                imgIdx++;
                if (but == null)
                    continue;
                try
                {
                    ImageData imgData = ImageBuffer.Get().GetImage(imgIdx);
                    if (imgData == null)
                        continue;
                        //Image.FromFile("X:\\test.jpg");
                    string strFileName = imgData.GetFilePathName(true);
                    if (string.IsNullOrWhiteSpace(strFileName)
                        || !File.Exists(strFileName)
                        )
                    {
                        continue;
                    }
                    Image img = Image.FromFile(strFileName);
                    but.BackgroundImage = img;
                    but.Text = imgIdx.ToString();
                }
                catch(Exception )
                {
                }
            }
        }

    }
}
