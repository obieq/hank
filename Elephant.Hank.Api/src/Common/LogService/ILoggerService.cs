// ---------------------------------------------------------------------------------------------------
// <copyright file="ILoggerService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The ILoggerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.LogService
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The LoggerService interface.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogException(string message);

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void LogException(Exception ex);

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="frame">The frame.</param>
        void LogException(Exception ex, int frame);

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        void LogException(Exception ex, EventLogEntryType level);

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        void LogException(Exception ex, EventLogEntryType level, int frame);

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        /// <param name="frame">The frame.</param>
        void LogException(string message, EventLogEntryType level, int frame);

        /// <summary>
        /// The log failure audit.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogFailureAudit(string message);

        /// <summary>
        /// The log information.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogInformation(string message);

        /// <summary>
        /// The log success audit.
        /// </summary>
        /// <param name="message">The message.</param>
        void LogSuccessAudit(string message);

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
        void LogToDB(int level, string machineName, string filename, string method, int linenumber, string exceptionMessage, string stackTrace, Guid sessionId);
    }
}