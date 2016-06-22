// ---------------------------------------------------------------------------------------------------
// <copyright file="frmMain.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The frmMain class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Windows.Forms;

    using Elephant.Hank.WindowsApplication.Framework.Extensions;
    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Framework.Processes;
    using Elephant.Hank.WindowsApplication.Resources;
    using Elephant.Hank.WindowsApplication.Resources.Constants;

    /// <summary>
    /// The frmMain class
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// The is exit event
        /// </summary>
        private bool isExitEvent;

        /// <summary>
        /// The is pause event
        /// </summary>
        private bool isPauseEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            LoadData();
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
            var settings = SettingsHelper.Get();

            txtExecutionTimeLapseInMilli.Text = settings.ExecutionTimeLapseInMilli.ToString();
            txtBaseApiUrl.Text = settings.BaseApiUrl;
            txtFrameworkBasePath.Text = settings.FrameworkBasePath;
            txtBaseWebUrl.Text = settings.BaseWebUrl;

            txtClearLogHours.Text = settings.ClearLogHours.ToString();
            txtClearReportHours.Text = settings.ClearReportHours.ToString();
            txtCleanerRunAtHour.Text = settings.CleanerRunAtHour.ToString();

            if (settings.ExecutionTimeLapseInMilli > 0)
            {
                this.tmrSchedular.Enabled = true;
                this.tmrSchedular.Interval = settings.ExecutionTimeLapseInMilli;

                this.tmrCleaner.Enabled = true;
                this.tmrCleaner.Interval = settings.RunCleanerInMilliSec;
            }
            else
            {
                this.tmrSchedular.Enabled = false;
                this.tmrCleaner.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isExitEvent)
            {
                e.Cancel = true;
                Hide();
            }
        }
        
        /// <summary>
        /// Handles the Tick event of the tmrSchedular control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrSchedular_Tick(object sender, EventArgs e)
        {
            tmrSchedular.Enabled = false;
            Processor.ExecuteService(this.isPauseEvent);

            this.dgRunning.DataSource = Processor.HubInfo.Select(x => new { StartedAt = x.Value.StartedAt, SchedulerId = x.Value.SchedulerId, TestQueueId = x.Value.TestQueueId, SeleniumAddress = x.Value.SeleniumAddress }).OrderBy(x => x.StartedAt).ToList();
            this.dgInQueue.DataSource = Processor.QueuedTest.Select(x => new { CreatedAt = x.Value.CreatedOn, SchedulerId = x.Value.SchedulerId, TestQueueId = x.Value.TestQueueId, SeleniumAddress = x.Value.SeleniumAddress }).OrderBy(x => x.CreatedAt).ToList();
        }

        /// <summary>
        /// Handles the Click event of the settingsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isExitEvent = true;
            Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var settings = new SettingsModel
                           {
                               ExecutionTimeLapseInMilli = txtExecutionTimeLapseInMilli.Text.ToInt(),
                               BaseApiUrl = txtBaseApiUrl.Text,
                               FrameworkBasePath = txtFrameworkBasePath.Text,
                               BaseWebUrl = txtBaseWebUrl.Text,

                               ClearLogHours = txtClearLogHours.Text.ToInt(336),
                               ClearReportHours = txtClearReportHours.Text.ToInt(720),
                               CleanerRunAtHour = txtCleanerRunAtHour.Text.ToInt(24)
                           };

            SettingsHelper.Save(settings);

            this.LoadData();
        }

        /// <summary>
        /// Handles the Resize event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = "Protractor controller - " + ConfigurationManager.AppSettings[ConfigConstants.EnvironmentName];
            this.sysTray.Text = this.Text;
        }

        /// <summary>
        /// Handles the Tick event of the tmrHideMe control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrHideMe_Tick(object sender, EventArgs e)
        {
            this.Hide();
            tmrHideMe.Enabled = false;

            Processor.ExecuteCleaner();
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the sysTray control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void sysTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        /// <summary>
        /// Handles the Tick event of the tmrCleaner control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrCleaner_Tick(object sender, EventArgs e)
        {
            Processor.ExecuteCleaner();
        }

        /// <summary>
        /// Handles the Click event of the btnCompressPng control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCompressPng_Click(object sender, EventArgs e)
        {
            ImageProcessor.ProcessImages("");
        }

        /// <summary>
        /// Handles the Click event of the btnClearHub control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClearHub_Click(object sender, EventArgs e)
        {
            Processor.HubInfo.Clear();
        }

        /// <summary>
        /// Handles the Click event of the btnPause control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            this.isPauseEvent = !this.isPauseEvent;
            this.btnPause.Text = this.isPauseEvent ? "Resume" : "Pause";
        }
    }
}
