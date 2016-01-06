// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailProcessor.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The EmailProcessor class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Emailer
{
    using System;
    using System.Linq;

    using Elephant.Hank.WindowsApplication.Framework.FileHelper;
    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;
    using Elephant.Hank.WindowsApplication.Resources.Models;

    /// <summary>
    /// The EmailProcessor class
    /// </summary>
    public class EmailProcessor
    {
        /// <summary>
        /// The email builder
        /// </summary>
        private readonly EmailBuilder emailBuilder;

        /// <summary>
        /// The email sender
        /// </summary>
        private readonly EmailSender emailSender;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProcessor"/> class.
        /// </summary>
        public EmailProcessor()
        {
            this.emailBuilder = new EmailBuilder();
            this.emailSender = new EmailSender();
        }

        /// <summary>
        /// Emails the report.
        /// </summary>
        public SchedulerHistoryEmailStatus EmailReport(ReportResultData reportData)
        {
            string emailHtml;
            string subject;
            string toEmailId = Properties.Settings.Default.FaultedEmailId;

            var result = SchedulerHistoryEmailStatus.Sent;

            try
            {
                if (reportData == null || reportData.ReportData == null || !reportData.ReportData.Any())
                {
                    throw new Exception("Something went wrong!");
                }

                emailHtml = this.emailBuilder.GetReportHtml(reportData);

                subject = "Hank report: " + DateTime.Now.ToShortDateString() + " - " + reportData.PostFixToSubject;

                if (reportData.TotalCount != 0 && reportData.DoSendEmail)
                {
                    toEmailId = reportData.ToEmailIds;
                }
                else
                {
                    subject += " - Default";
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogException(ex);

                emailHtml = ex.Message;
                subject = "Not able to generate! ";
                toEmailId = Properties.Settings.Default.FaultedEmailId;
                result = SchedulerHistoryEmailStatus.SendException;
            }

            var resultVal = this.emailSender.SendEmail((toEmailId + string.Empty).Replace(",", ";").Split(';'), subject, emailHtml);

            if (resultVal == SchedulerHistoryEmailStatus.NoValidRecipientFound
                || resultVal == SchedulerHistoryEmailStatus.NoRecipientFound)
            {
                this.emailSender.SendEmail((Properties.Settings.Default.FaultedEmailId + string.Empty).Replace(",", ";").Split(';'), subject, emailHtml);
            }

            return result == SchedulerHistoryEmailStatus.SendException ? result : resultVal;
        }
    }
}
