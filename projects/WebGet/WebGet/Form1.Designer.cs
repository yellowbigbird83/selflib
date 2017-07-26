namespace WebGet
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.textBoxResponse = new System.Windows.Forms.TextBox();
            this.butGet = new System.Windows.Forms.Button();
            this.checkBoxUseProxy = new System.Windows.Forms.CheckBox();
            this.butParse = new System.Windows.Forms.Button();
            this.butTestDownloadFile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butPreview = new System.Windows.Forms.Button();
            this.butStart = new System.Windows.Forms.Button();
            this.textBoxPageMax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(872, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(13, 29);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(742, 21);
            this.textBoxUrl.TabIndex = 1;
            this.textBoxUrl.Text = "https://www.tumblr.com/dashboard";
            // 
            // textBoxResponse
            // 
            this.textBoxResponse.Location = new System.Drawing.Point(12, 56);
            this.textBoxResponse.MaxLength = 3276700;
            this.textBoxResponse.Multiline = true;
            this.textBoxResponse.Name = "textBoxResponse";
            this.textBoxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResponse.Size = new System.Drawing.Size(234, 125);
            this.textBoxResponse.TabIndex = 2;
            // 
            // butGet
            // 
            this.butGet.Location = new System.Drawing.Point(762, 26);
            this.butGet.Name = "butGet";
            this.butGet.Size = new System.Drawing.Size(98, 23);
            this.butGet.TabIndex = 3;
            this.butGet.Text = "get response";
            this.butGet.UseVisualStyleBackColor = true;
            this.butGet.Click += new System.EventHandler(this.butGet_Click);
            // 
            // checkBoxUseProxy
            // 
            this.checkBoxUseProxy.AutoSize = true;
            this.checkBoxUseProxy.Location = new System.Drawing.Point(762, 55);
            this.checkBoxUseProxy.Name = "checkBoxUseProxy";
            this.checkBoxUseProxy.Size = new System.Drawing.Size(78, 16);
            this.checkBoxUseProxy.TabIndex = 4;
            this.checkBoxUseProxy.Text = "Use proxy";
            this.checkBoxUseProxy.UseVisualStyleBackColor = true;
            // 
            // butParse
            // 
            this.butParse.Location = new System.Drawing.Point(761, 82);
            this.butParse.Name = "butParse";
            this.butParse.Size = new System.Drawing.Size(98, 23);
            this.butParse.TabIndex = 5;
            this.butParse.Text = "parse";
            this.butParse.UseVisualStyleBackColor = true;
            this.butParse.Click += new System.EventHandler(this.butParse_Click);
            // 
            // butTestDownloadFile
            // 
            this.butTestDownloadFile.Location = new System.Drawing.Point(761, 112);
            this.butTestDownloadFile.Name = "butTestDownloadFile";
            this.butTestDownloadFile.Size = new System.Drawing.Size(75, 23);
            this.butTestDownloadFile.TabIndex = 6;
            this.butTestDownloadFile.Text = "test download file";
            this.butTestDownloadFile.UseVisualStyleBackColor = true;
            this.butTestDownloadFile.Click += new System.EventHandler(this.butTestDownloadFile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 197);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // butPreview
            // 
            this.butPreview.Location = new System.Drawing.Point(762, 142);
            this.butPreview.Name = "butPreview";
            this.butPreview.Size = new System.Drawing.Size(75, 23);
            this.butPreview.TabIndex = 8;
            this.butPreview.Text = "Preview";
            this.butPreview.UseVisualStyleBackColor = true;
            this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
            // 
            // butStart
            // 
            this.butStart.Location = new System.Drawing.Point(762, 172);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 23);
            this.butStart.TabIndex = 9;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // textBoxPageMax
            // 
            this.textBoxPageMax.Location = new System.Drawing.Point(495, 55);
            this.textBoxPageMax.Name = "textBoxPageMax";
            this.textBoxPageMax.Size = new System.Drawing.Size(100, 21);
            this.textBoxPageMax.TabIndex = 10;
            this.textBoxPageMax.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "PageMax";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 426);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPageMax);
            this.Controls.Add(this.butStart);
            this.Controls.Add(this.butPreview);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.butTestDownloadFile);
            this.Controls.Add(this.butParse);
            this.Controls.Add(this.checkBoxUseProxy);
            this.Controls.Add(this.butGet);
            this.Controls.Add(this.textBoxResponse);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.TextBox textBoxResponse;
        private System.Windows.Forms.Button butGet;
        private System.Windows.Forms.CheckBox checkBoxUseProxy;
        private System.Windows.Forms.Button butParse;
        private System.Windows.Forms.Button butTestDownloadFile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butPreview;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.TextBox textBoxPageMax;
        private System.Windows.Forms.Label label1;
    }
}

