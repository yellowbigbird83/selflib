namespace WebGet
{
    partial class FormPreview
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
            this.butNextPage = new System.Windows.Forms.Button();
            this.butPreviousPage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxPageIdx = new System.Windows.Forms.TextBox();
            this.butRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butNextPage
            // 
            this.butNextPage.Location = new System.Drawing.Point(853, 12);
            this.butNextPage.Name = "butNextPage";
            this.butNextPage.Size = new System.Drawing.Size(75, 23);
            this.butNextPage.TabIndex = 10;
            this.butNextPage.Text = "next page";
            this.butNextPage.UseVisualStyleBackColor = true;
            this.butNextPage.Click += new System.EventHandler(this.butNextPage_Click);
            // 
            // butPreviousPage
            // 
            this.butPreviousPage.Location = new System.Drawing.Point(853, 41);
            this.butPreviousPage.Name = "butPreviousPage";
            this.butPreviousPage.Size = new System.Drawing.Size(75, 23);
            this.butPreviousPage.TabIndex = 11;
            this.butPreviousPage.Text = "Previous page";
            this.butPreviousPage.UseVisualStyleBackColor = true;
            this.butPreviousPage.Click += new System.EventHandler(this.butPreviousPage_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 516);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // textBoxPageIdx
            // 
            this.textBoxPageIdx.Location = new System.Drawing.Point(854, 128);
            this.textBoxPageIdx.Name = "textBoxPageIdx";
            this.textBoxPageIdx.ReadOnly = true;
            this.textBoxPageIdx.Size = new System.Drawing.Size(100, 21);
            this.textBoxPageIdx.TabIndex = 14;
            this.textBoxPageIdx.Text = "1";
            // 
            // butRefresh
            // 
            this.butRefresh.Location = new System.Drawing.Point(854, 70);
            this.butRefresh.Name = "butRefresh";
            this.butRefresh.Size = new System.Drawing.Size(75, 23);
            this.butRefresh.TabIndex = 15;
            this.butRefresh.Text = "Refresh";
            this.butRefresh.UseVisualStyleBackColor = true;
            // 
            // FormPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 525);
            this.Controls.Add(this.butRefresh);
            this.Controls.Add(this.textBoxPageIdx);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.butPreviousPage);
            this.Controls.Add(this.butNextPage);
            this.Name = "FormPreview";
            this.Text = "Preview";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butNextPage;
        private System.Windows.Forms.Button butPreviousPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxPageIdx;
        private System.Windows.Forms.Button butRefresh;
    }
}