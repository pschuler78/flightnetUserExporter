namespace FlightNetUserExporter
{
    partial class MainForm
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
            this.buttonFileExportBrowser = new System.Windows.Forms.Button();
            this.labelFileExport = new System.Windows.Forms.Label();
            this.saveFileDialogFileExport = new System.Windows.Forms.SaveFileDialog();
            this.textBoxFileExport = new System.Windows.Forms.TextBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonFileExportBrowser
            // 
            this.buttonFileExportBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFileExportBrowser.Location = new System.Drawing.Point(704, 55);
            this.buttonFileExportBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFileExportBrowser.Name = "buttonFileExportBrowser";
            this.buttonFileExportBrowser.Size = new System.Drawing.Size(45, 28);
            this.buttonFileExportBrowser.TabIndex = 0;
            this.buttonFileExportBrowser.Text = "...";
            this.buttonFileExportBrowser.UseVisualStyleBackColor = true;
            this.buttonFileExportBrowser.Click += new System.EventHandler(this.buttonFileExportBrowser_Click);
            // 
            // labelFileExport
            // 
            this.labelFileExport.AutoSize = true;
            this.labelFileExport.Location = new System.Drawing.Point(43, 66);
            this.labelFileExport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFileExport.Name = "labelFileExport";
            this.labelFileExport.Size = new System.Drawing.Size(90, 17);
            this.labelFileExport.TabIndex = 1;
            this.labelFileExport.Text = "Export-Datei:";
            // 
            // textBoxFileExport
            // 
            this.textBoxFileExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileExport.Location = new System.Drawing.Point(141, 59);
            this.textBoxFileExport.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFileExport.Name = "textBoxFileExport";
            this.textBoxFileExport.Size = new System.Drawing.Size(553, 22);
            this.textBoxFileExport.TabIndex = 2;
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(47, 134);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(180, 28);
            this.buttonExport.TabIndex = 3;
            this.buttonExport.Text = "Export von FlightNet";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(47, 201);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(823, 396);
            this.textBoxLog.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 633);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.textBoxFileExport);
            this.Controls.Add(this.labelFileExport);
            this.Controls.Add(this.buttonFileExportBrowser);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "FlightNet User Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFileExportBrowser;
        private System.Windows.Forms.Label labelFileExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialogFileExport;
        private System.Windows.Forms.TextBox textBoxFileExport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}

