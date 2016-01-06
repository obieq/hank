// ---------------------------------------------------------------------------------------------------
// <copyright file="TblLogger.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TblLogger class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService.DBSchema
{
    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The TblLogger class
    /// </summary>
    [EfIgnoreDbLog]
    public class TblLogger : BaseTable
    {
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the name of the machine.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        public int Linenumber { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        public string SessionId { get; set; }
    }
}
