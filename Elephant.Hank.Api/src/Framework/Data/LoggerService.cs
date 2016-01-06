// ---------------------------------------------------------------------------------------------------
// <copyright file="LoggerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The LoggerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Data
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;

    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Common.LogService;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Resources.Constants;

    /// <summary>
    /// The logger service.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// The table
        /// </summary>
        private readonly IRepository<TblLogger> table;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerService"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        public LoggerService(IRepository<TblLogger> table)
        {
            this.table = table;
        }

        #region Public Methods and Operators

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogException(string message)
        {
            this.LogException(message, EventLogEntryType.Error, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void LogException(Exception ex)
        {
            this.LogException(ex, EventLogEntryType.Error, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="frame">The frame.</param>
        public void LogException(Exception ex, int frame)
        {
            this.LogException(ex, EventLogEntryType.Error, frame);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        public void LogException(Exception ex, EventLogEntryType level)
        {
            this.LogException(ex.InnerException != null ? ex.InnerException.Message : ex.Message, level, 1);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        public void LogException(Exception ex, EventLogEntryType level, int frame)
        {
            this.LogException(ex.InnerException != null ? ex.InnerException.Message : ex.Message, level, frame);
        }

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        public void LogException(string message, EventLogEntryType level, int frame)
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

                this.LogToDb(level, filename, method, linenumber, message, stackTrace);
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
        public void LogFailureAudit(string message)
        {
            this.LogException(message, EventLogEntryType.FailureAudit, 1);
        }

        /// <summary>
        /// The log information.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogInformation(string message)
        {
            this.LogException(message, EventLogEntryType.Information, 1);
        }

        /// <summary>
        /// The log success audit.
        /// </summary>
        /// <param name="message">The message.</param>
        public void LogSuccessAudit(string message)
        {
            this.LogException(message, EventLogEntryType.SuccessAudit, 1);
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
        public void LogToDB(int level, string machineName, string filename, string method, int linenumber, string exceptionMessage, string stackTrace, Guid sessionId)
        {
            try
            {
                var newLog = new TblLogger
                             {
                                 Level = level,
                                 MachineName = machineName,
                                 Filename = filename,
                                 Method = method,
                                 Linenumber = linenumber,
                                 ExceptionMessage = exceptionMessage,
                                 StackTrace = stackTrace,
                                 SessionId = sessionId.ToString()
                             };

                this.table.Insert(newLog);
                this.table.Commit();
            }
            catch (Exception)
            {
                // Don't log since here since we are in the logger itself
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The log to db.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="method">The method.</param>
        /// <param name="linenumber">The line number.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        private void LogToDb(EventLogEntryType level, string filename, string method, int linenumber, string exceptionMessage, string stackTrace)
        {
            Guid sessionId = Guid.Empty;

            try
            {
                this.LogToDB(Convert.ToInt32(level),  Environment.MachineName,  filename,  method,  linenumber,  exceptionMessage,  stackTrace,  sessionId);
            }
            catch
            {
                // Don't log since here since we are in the logger itself
            }
        }

        #endregion
    }
}