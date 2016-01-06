// ---------------------------------------------------------------------------------------------------
// <copyright file="LoggerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-29</date>
// <summary>
//     The LoggerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Framework.Helpers
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// The logger service.
    /// </summary>
    public class LoggerService
    {
        #region Public Methods and Operators

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogException(string message)
        {
            LogException(message, EventLogEntryType.Error, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void LogException(Exception ex)
        {
            LogException(ex, EventLogEntryType.Error, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="frame">The frame.</param>
        public static void LogException(Exception ex, int frame)
        {
            LogException(ex, EventLogEntryType.Error, frame);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        public static void LogException(Exception ex, EventLogEntryType level)
        {
            LogException(ex.InnerException != null ? ex.InnerException.Message : ex.Message, level, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        public static void LogException(Exception ex, EventLogEntryType level, int frame)
        {
            LogException(ex.InnerException != null ? ex.InnerException.Message : ex.Message, level, frame);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        public static void LogException(string message, EventLogEntryType level, int frame)
        {
            try
            {
                var callStack = new StackFrame(frame, true);
                string filename = string.Empty;
                try
                {
                    filename = callStack.GetFileName();
                }
                catch
                {
                }

                string method = callStack.GetMethod().ToString();
                int linenumber = callStack.GetFileLineNumber();
                string stackTrace = string.Empty;

                if (level == EventLogEntryType.Error)
                {
                    stackTrace = "Stack Trace: " + Environment.StackTrace;
                }
                else
                {
                    method = string.Empty;
                    linenumber = 0;
                }

                LogToFile(level, filename, method, linenumber, message, stackTrace);
            }
            catch
            {
                // Do nothing since logging is unavailable
            }
        }

        /// <summary>
        /// The log failure audit.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogFailureAudit(string message)
        {
            LogException(message, EventLogEntryType.FailureAudit, 1);
        }

        /// <summary>
        /// The log information.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogInformation(string message)
        {
            LogException(message, EventLogEntryType.Information, 1);
        }

        /// <summary>
        /// The log success audit.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void LogSuccessAudit(string message)
        {
            LogException(message, EventLogEntryType.SuccessAudit, 1);
        }

        /// <summary>
        /// The log to db.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="machineName">The machine name.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="method">The method.</param>
        /// <param name="linenumber">The line number.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <param name="sessionId">The session id.</param>
        public static void LogToFile(int level, string machineName, string filename, string method, int linenumber, string exceptionMessage, string stackTrace, Guid sessionId)
        {
            try
            {
                string logText = "\r\n Level: " + level
                                +"\r\n MachineName: " + machineName
                                + "\r\n Filename: " + filename
                                + "\r\n Method: " + method
                                + "\r\n Linenumber: " + linenumber
                                + "\r\n ExceptionMessage: " + exceptionMessage
                                + "\r\n StackTrace: " + stackTrace
                                + "\r\n sessionId: " + sessionId;
                LogWrite(logText);
            }
            catch (Exception)
            {
                // Don't log since here since we are in the logger itself
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Logs the write.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        private static void LogWrite(string logMessage)
        {
            var exePath = SettingsHelper.Get().LogsLocation;
            using (StreamWriter w = File.AppendText(exePath + "\\" + DateTime.Now.ToString("MM-dd-yyyy-") + "log.txt"))
            {
                Log(logMessage, w);
            }
        }

        /// <summary>
        /// Logs the specified log message.
        /// </summary>
        /// <param name="logMessage">The log message.</param>
        /// <param name="txtWriter">The text writer.</param>
        private static void Log(string logMessage, TextWriter txtWriter)
        {
            txtWriter.Write("\rLog Entry : ");
            txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            txtWriter.WriteLine(logMessage);
            txtWriter.WriteLine(string.Empty);
        }

        /// <summary>
        /// The log to db.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="method">The method.</param>
        /// <param name="linenumber">The line number.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        private static void LogToFile(EventLogEntryType level, string filename, string method, int linenumber, string exceptionMessage, string stackTrace)
        {
            Guid sessionId = Guid.Empty;

            try
            {
                LogToFile(Convert.ToInt32(level), Environment.MachineName, filename, method, linenumber, exceptionMessage, stackTrace, sessionId);
            }
            catch
            {
                // Don't log since here since we are in the logger itself
            }
        }

        #endregion
    }
}