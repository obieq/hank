namespace Elephant.Hank.WindowsApplication
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tmrSchedular = new System.Windows.Forms.Timer(this.components);
            this.mnuCtxRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sysTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.tmrHideMe = new System.Windows.Forms.Timer(this.components);
            this.tmrCleaner = new System.Windows.Forms.Timer(this.components);
            this.grpCleaner = new System.Windows.Forms.GroupBox();
            this.lblCleanerHours = new System.Windows.Forms.Label();
            this.txtCleanerRunAtHour = new System.Windows.Forms.TextBox();
            this.lblClearReportHours = new System.Windows.Forms.Label();
            this.lblClearLogHours = new System.Windows.Forms.Label();
            this.txtClearReportHours = new System.Windows.Forms.TextBox();
            this.txtClearLogHours = new System.Windows.Forms.TextBox();
            this.grpBase = new System.Windows.Forms.GroupBox();
            this.lblBaseWebUrl = new System.Windows.Forms.Label();
            this.txtBaseWebUrl = new System.Windows.Forms.TextBox();
            this.lblFrameworkBasePath = new System.Windows.Forms.Label();
            this.txtFrameworkBasePath = new System.Windows.Forms.TextBox();
            this.lblBaseApiUrl = new System.Windows.Forms.Label();
            this.txtBaseApiUrl = new System.Windows.Forms.TextBox();
            this.lblExecutionTimeLapseInMilli = new System.Windows.Forms.Label();
            this.txtExecutionTimeLapseInMilli = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCompressPng = new System.Windows.Forms.Button();
            this.grpRunning = new System.Windows.Forms.GroupBox();
            this.dgRunning = new System.Windows.Forms.DataGridView();
            this.grpInQueue = new System.Windows.Forms.GroupBox();
            this.dgInQueue = new System.Windows.Forms.DataGridView();
            this.btnClearHub = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.mnuCtxRightClick.SuspendLayout();
            this.grpCleaner.SuspendLayout();
            this.grpBase.SuspendLayout();
            this.grpRunning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRunning)).BeginInit();
            this.grpInQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrSchedular
            // 
            this.tmrSchedular.Enabled = true;
            this.tmrSchedular.Interval = 10000;
            this.tmrSchedular.Tick += new System.EventHandler(this.tmrSchedular_Tick);
            // 
            // mnuCtxRightClick
            // 
            this.mnuCtxRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem1});
            this.mnuCtxRightClick.Name = "mnuCtxRightCLick";
            this.mnuCtxRightClick.Size = new System.Drawing.Size(117, 54);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sysTray
            // 
            this.sysTray.ContextMenuStrip = this.mnuCtxRightClick;
            this.sysTray.Icon = ((System.Drawing.Icon)(resources.GetObject("sysTray.Icon")));
            this.sysTray.Text = "Protractor controller";
            this.sysTray.Visible = true;
            this.sysTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.sysTray_MouseDoubleClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(519, 605);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "S&ave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tmrHideMe
            // 
            this.tmrHideMe.Enabled = true;
            this.tmrHideMe.Interval = 1000;
            this.tmrHideMe.Tick += new System.EventHandler(this.tmrHideMe_Tick);
            // 
            // tmrCleaner
            // 
            this.tmrCleaner.Enabled = true;
            this.tmrCleaner.Interval = 10000;
            this.tmrCleaner.Tick += new System.EventHandler(this.tmrCleaner_Tick);
            // 
            // grpCleaner
            // 
            this.grpCleaner.Controls.Add(this.lblCleanerHours);
            this.grpCleaner.Controls.Add(this.txtCleanerRunAtHour);
            this.grpCleaner.Controls.Add(this.lblClearReportHours);
            this.grpCleaner.Controls.Add(this.lblClearLogHours);
            this.grpCleaner.Controls.Add(this.txtClearReportHours);
            this.grpCleaner.Controls.Add(this.txtClearLogHours);
            this.grpCleaner.Location = new System.Drawing.Point(12, 170);
            this.grpCleaner.Name = "grpCleaner";
            this.grpCleaner.Size = new System.Drawing.Size(663, 122);
            this.grpCleaner.TabIndex = 22;
            this.grpCleaner.TabStop = false;
            this.grpCleaner.Text = "I/O Cleaner Setup";
            // 
            // lblCleanerHours
            // 
            this.lblCleanerHours.AutoSize = true;
            this.lblCleanerHours.Location = new System.Drawing.Point(36, 30);
            this.lblCleanerHours.Name = "lblCleanerHours";
            this.lblCleanerHours.Size = new System.Drawing.Size(108, 13);
            this.lblCleanerHours.TabIndex = 27;
            this.lblCleanerHours.Text = "Cleaner run at (hours)";
            // 
            // txtCleanerRunAtHour
            // 
            this.txtCleanerRunAtHour.Location = new System.Drawing.Point(187, 30);
            this.txtCleanerRunAtHour.Name = "txtCleanerRunAtHour";
            this.txtCleanerRunAtHour.Size = new System.Drawing.Size(435, 20);
            this.txtCleanerRunAtHour.TabIndex = 26;
            // 
            // lblClearReportHours
            // 
            this.lblClearReportHours.AutoSize = true;
            this.lblClearReportHours.Location = new System.Drawing.Point(36, 82);
            this.lblClearReportHours.Name = "lblClearReportHours";
            this.lblClearReportHours.Size = new System.Drawing.Size(136, 13);
            this.lblClearReportHours.TabIndex = 25;
            this.lblClearReportHours.Text = "Keep report data for (hours)";
            // 
            // lblClearLogHours
            // 
            this.lblClearLogHours.AutoSize = true;
            this.lblClearLogHours.Location = new System.Drawing.Point(36, 56);
            this.lblClearLogHours.Name = "lblClearLogHours";
            this.lblClearLogHours.Size = new System.Drawing.Size(128, 13);
            this.lblClearLogHours.TabIndex = 24;
            this.lblClearLogHours.Text = "Keep logs data for (hours)";
            // 
            // txtClearReportHours
            // 
            this.txtClearReportHours.Location = new System.Drawing.Point(187, 82);
            this.txtClearReportHours.Name = "txtClearReportHours";
            this.txtClearReportHours.Size = new System.Drawing.Size(435, 20);
            this.txtClearReportHours.TabIndex = 23;
            // 
            // txtClearLogHours
            // 
            this.txtClearLogHours.Location = new System.Drawing.Point(187, 56);
            this.txtClearLogHours.Name = "txtClearLogHours";
            this.txtClearLogHours.Size = new System.Drawing.Size(435, 20);
            this.txtClearLogHours.TabIndex = 22;
            // 
            // grpBase
            // 
            this.grpBase.Controls.Add(this.lblBaseWebUrl);
            this.grpBase.Controls.Add(this.txtBaseWebUrl);
            this.grpBase.Controls.Add(this.lblFrameworkBasePath);
            this.grpBase.Controls.Add(this.txtFrameworkBasePath);
            this.grpBase.Controls.Add(this.lblBaseApiUrl);
            this.grpBase.Controls.Add(this.txtBaseApiUrl);
            this.grpBase.Controls.Add(this.lblExecutionTimeLapseInMilli);
            this.grpBase.Controls.Add(this.txtExecutionTimeLapseInMilli);
            this.grpBase.Location = new System.Drawing.Point(12, 12);
            this.grpBase.Name = "grpBase";
            this.grpBase.Size = new System.Drawing.Size(663, 152);
            this.grpBase.TabIndex = 23;
            this.grpBase.TabStop = false;
            this.grpBase.Text = "Base Setup";
            // 
            // lblBaseWebUrl
            // 
            this.lblBaseWebUrl.AutoSize = true;
            this.lblBaseWebUrl.Location = new System.Drawing.Point(36, 86);
            this.lblBaseWebUrl.Name = "lblBaseWebUrl";
            this.lblBaseWebUrl.Size = new System.Drawing.Size(71, 13);
            this.lblBaseWebUrl.TabIndex = 23;
            this.lblBaseWebUrl.Text = "Base Web url";
            // 
            // txtBaseWebUrl
            // 
            this.txtBaseWebUrl.Location = new System.Drawing.Point(187, 83);
            this.txtBaseWebUrl.Name = "txtBaseWebUrl";
            this.txtBaseWebUrl.Size = new System.Drawing.Size(435, 20);
            this.txtBaseWebUrl.TabIndex = 18;
            // 
            // lblFrameworkBasePath
            // 
            this.lblFrameworkBasePath.AutoSize = true;
            this.lblFrameworkBasePath.Location = new System.Drawing.Point(37, 113);
            this.lblFrameworkBasePath.Name = "lblFrameworkBasePath";
            this.lblFrameworkBasePath.Size = new System.Drawing.Size(109, 13);
            this.lblFrameworkBasePath.TabIndex = 22;
            this.lblFrameworkBasePath.Text = "Framework base path";
            // 
            // txtFrameworkBasePath
            // 
            this.txtFrameworkBasePath.Location = new System.Drawing.Point(187, 110);
            this.txtFrameworkBasePath.Name = "txtFrameworkBasePath";
            this.txtFrameworkBasePath.Size = new System.Drawing.Size(435, 20);
            this.txtFrameworkBasePath.TabIndex = 20;
            // 
            // lblBaseApiUrl
            // 
            this.lblBaseApiUrl.AutoSize = true;
            this.lblBaseApiUrl.Location = new System.Drawing.Point(37, 60);
            this.lblBaseApiUrl.Name = "lblBaseApiUrl";
            this.lblBaseApiUrl.Size = new System.Drawing.Size(65, 13);
            this.lblBaseApiUrl.TabIndex = 21;
            this.lblBaseApiUrl.Text = "Base Api Url";
            // 
            // txtBaseApiUrl
            // 
            this.txtBaseApiUrl.Location = new System.Drawing.Point(187, 57);
            this.txtBaseApiUrl.Name = "txtBaseApiUrl";
            this.txtBaseApiUrl.Size = new System.Drawing.Size(435, 20);
            this.txtBaseApiUrl.TabIndex = 17;
            // 
            // lblExecutionTimeLapseInMilli
            // 
            this.lblExecutionTimeLapseInMilli.AutoSize = true;
            this.lblExecutionTimeLapseInMilli.Location = new System.Drawing.Point(37, 34);
            this.lblExecutionTimeLapseInMilli.Name = "lblExecutionTimeLapseInMilli";
            this.lblExecutionTimeLapseInMilli.Size = new System.Drawing.Size(67, 13);
            this.lblExecutionTimeLapseInMilli.TabIndex = 19;
            this.lblExecutionTimeLapseInMilli.Text = "Time interval";
            // 
            // txtExecutionTimeLapseInMilli
            // 
            this.txtExecutionTimeLapseInMilli.Location = new System.Drawing.Point(187, 31);
            this.txtExecutionTimeLapseInMilli.Name = "txtExecutionTimeLapseInMilli";
            this.txtExecutionTimeLapseInMilli.Size = new System.Drawing.Size(435, 20);
            this.txtExecutionTimeLapseInMilli.TabIndex = 16;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(600, 605);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Cl&ose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCompressPng
            // 
            this.btnCompressPng.Location = new System.Drawing.Point(393, 605);
            this.btnCompressPng.Name = "btnCompressPng";
            this.btnCompressPng.Size = new System.Drawing.Size(120, 23);
            this.btnCompressPng.TabIndex = 25;
            this.btnCompressPng.Text = "Compress Png to Jpg";
            this.btnCompressPng.UseVisualStyleBackColor = true;
            this.btnCompressPng.Click += new System.EventHandler(this.btnCompressPng_Click);
            // 
            // grpRunning
            // 
            this.grpRunning.Controls.Add(this.dgRunning);
            this.grpRunning.Location = new System.Drawing.Point(12, 298);
            this.grpRunning.Name = "grpRunning";
            this.grpRunning.Size = new System.Drawing.Size(663, 140);
            this.grpRunning.TabIndex = 26;
            this.grpRunning.TabStop = false;
            this.grpRunning.Text = "Running";
            // 
            // dgRunning
            // 
            this.dgRunning.AllowUserToAddRows = false;
            this.dgRunning.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgRunning.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRunning.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgRunning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRunning.Location = new System.Drawing.Point(10, 22);
            this.dgRunning.Name = "dgRunning";
            this.dgRunning.ReadOnly = true;
            this.dgRunning.Size = new System.Drawing.Size(640, 112);
            this.dgRunning.TabIndex = 0;
            // 
            // grpInQueue
            // 
            this.grpInQueue.Controls.Add(this.dgInQueue);
            this.grpInQueue.Location = new System.Drawing.Point(12, 444);
            this.grpInQueue.Name = "grpInQueue";
            this.grpInQueue.Size = new System.Drawing.Size(663, 140);
            this.grpInQueue.TabIndex = 27;
            this.grpInQueue.TabStop = false;
            this.grpInQueue.Text = "In Queue";
            // 
            // dgInQueue
            // 
            this.dgInQueue.AllowUserToAddRows = false;
            this.dgInQueue.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgInQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgInQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInQueue.Location = new System.Drawing.Point(10, 19);
            this.dgInQueue.Name = "dgInQueue";
            this.dgInQueue.ReadOnly = true;
            this.dgInQueue.Size = new System.Drawing.Size(640, 115);
            this.dgInQueue.TabIndex = 1;
            // 
            // btnClearHub
            // 
            this.btnClearHub.Location = new System.Drawing.Point(267, 605);
            this.btnClearHub.Name = "btnClearHub";
            this.btnClearHub.Size = new System.Drawing.Size(120, 23);
            this.btnClearHub.TabIndex = 28;
            this.btnClearHub.Text = "Clear Hub";
            this.btnClearHub.UseVisualStyleBackColor = true;
            this.btnClearHub.Click += new System.EventHandler(this.btnClearHub_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(141, 605);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(120, 23);
            this.btnPause.TabIndex = 29;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 640);
            this.ContextMenuStrip = this.mnuCtxRightClick;
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnClearHub);
            this.Controls.Add(this.grpInQueue);
            this.Controls.Add(this.grpRunning);
            this.Controls.Add(this.btnCompressPng);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpBase);
            this.Controls.Add(this.grpCleaner);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.mnuCtxRightClick.ResumeLayout(false);
            this.grpCleaner.ResumeLayout(false);
            this.grpCleaner.PerformLayout();
            this.grpBase.ResumeLayout(false);
            this.grpBase.PerformLayout();
            this.grpRunning.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRunning)).EndInit();
            this.grpInQueue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInQueue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrSchedular;
        private System.Windows.Forms.ContextMenuStrip mnuCtxRightClick;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon sysTray;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer tmrHideMe;
        private System.Windows.Forms.Timer tmrCleaner;
        private System.Windows.Forms.GroupBox grpCleaner;
        private System.Windows.Forms.Label lblClearReportHours;
        private System.Windows.Forms.Label lblClearLogHours;
        private System.Windows.Forms.TextBox txtClearReportHours;
        private System.Windows.Forms.TextBox txtClearLogHours;
        private System.Windows.Forms.GroupBox grpBase;
        private System.Windows.Forms.Label lblBaseWebUrl;
        private System.Windows.Forms.TextBox txtBaseWebUrl;
        private System.Windows.Forms.Label lblFrameworkBasePath;
        private System.Windows.Forms.TextBox txtFrameworkBasePath;
        private System.Windows.Forms.Label lblBaseApiUrl;
        private System.Windows.Forms.TextBox txtBaseApiUrl;
        private System.Windows.Forms.Label lblExecutionTimeLapseInMilli;
        private System.Windows.Forms.TextBox txtExecutionTimeLapseInMilli;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCompressPng;
        private System.Windows.Forms.Label lblCleanerHours;
        private System.Windows.Forms.TextBox txtCleanerRunAtHour;
        private System.Windows.Forms.GroupBox grpRunning;
        private System.Windows.Forms.GroupBox grpInQueue;
        private System.Windows.Forms.DataGridView dgRunning;
        private System.Windows.Forms.DataGridView dgInQueue;
        private System.Windows.Forms.Button btnClearHub;
        private System.Windows.Forms.Button btnPause;
    }
}