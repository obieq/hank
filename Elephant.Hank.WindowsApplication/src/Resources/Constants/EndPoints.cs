// ---------------------------------------------------------------------------------------------------
// <copyright file="EndPoints.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-05-28</date>
// <summary>
//     The EndPoints class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.WindowsApplication.Resources.Constants
{
    /// <summary>
    /// The EndPoints class
    /// </summary>
    public static class EndPoints
    {
        /// <summary>
        /// Route to get Queued test List
        /// </summary>
        public static string GetTestQueue = "api/testqueue";

        /// <summary>
        /// The bulk update test queue
        /// </summary>
        public static string BulkUpdateTestQueue = "api/testqueue/bulk-update";

        /// <summary>
        /// The scheduler by identifier
        /// </summary>
        public static string SchedulerById = "api/scheduler/{0}";

        /// <summary>
        /// The scheduler history status
        /// </summary>
        public static string SchedulerHistoryStatus = "api/scheduler-history/status/{0}/{1}";

        /// <summary>
        /// The scheduler history email status
        /// </summary>
        public static string SchedulerHistoryEmailStatus = "api/scheduler-history/status/{0}/email/{1}";

        /// <summary>
        /// The report search
        /// </summary>
        public static string ReportSearch = "api/report/SearchReport";

        /// <summary>
        /// Route to get test by id
        /// </summary>
        public static string GetTestByQueueId = "api/test/{0}";

        /// <summary>
        /// the unprocessed report search
        /// </summary>
        public static string GetAllUnprocessedReports = "api/report/get-all-unprocessed-for-group/{0}";
    }
}
