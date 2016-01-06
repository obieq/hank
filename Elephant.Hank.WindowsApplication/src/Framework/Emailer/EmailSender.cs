// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-02-26</date>
// <summary>
//     The EmailSender class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Emailer
{
    using System;
    using System.Linq;
    using System.Net.Mail;

    using Elephant.Hank.WindowsApplication.Framework.Helpers;
    using Elephant.Hank.WindowsApplication.Resources.ApiModels.Enum;

    /// <summary>
    /// The EmailSender class
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="toAddresses">To addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="htmlBody">The HTML body.</param>
        public SchedulerHistoryEmailStatus SendEmail(string[] toAddresses, string subject, string htmlBody)
        {
            var result = SchedulerHistoryEmailStatus.NA;

            if (!toAddresses.Any())
            {
                result = SchedulerHistoryEmailStatus.NoRecipientFound;
            }
            else
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtpServer = new SmtpClient();

                foreach (var toAddress in toAddresses)
                {
                    try
                    {
                        mail.To.Add(toAddress);
                    }
                    catch (Exception ex)
                    {
                        LoggerService.LogException(ex);
                        result = SchedulerHistoryEmailStatus.SentPartially;
                    }
                }

                if (!mail.To.Any())
                {
                    result = SchedulerHistoryEmailStatus.NoValidRecipientFound;
                }
                else if (mail.To.Count() != toAddresses.Count())
                {
                    result = SchedulerHistoryEmailStatus.SentPartially;
                }

                if (result != SchedulerHistoryEmailStatus.NoValidRecipientFound
                    && result != SchedulerHistoryEmailStatus.NoRecipientFound)
                {
                    mail.Subject = subject;
                    mail.Body = htmlBody;
                    mail.IsBodyHtml = true;

                    try
                    {
                        smtpServer.Send(mail);

                        if (result != SchedulerHistoryEmailStatus.SentPartially)
                        {
                            result = SchedulerHistoryEmailStatus.Sent;
                        }
                    }
                    catch (Exception ex)
                    {
                        LoggerService.LogException(ex);
                        result = SchedulerHistoryEmailStatus.SendException;
                    }
                }
            }

            return result;
        }
    }
}