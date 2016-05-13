// ---------------------------------------------------------------------------------------------------
// <copyright file="TblApiTestDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2016-05-13</date>
// <summary>
//     The TblApiTestDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System.Collections.Generic;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TblApiTestDataDto class
    /// </summary>
    public class TblApiTestDataDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public string EndPoint { get; set; }
       
        /// <summary>
        /// Gets or sets the API URL.
        /// </summary>
        /// <value>
        /// The API URL.
        /// </value>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public List<NameValuePair> Headers { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public List<NameValuePair> IgnoreHeaders { get; set; }

        /// <summary>
        /// Gets or sets the type of the request.
        /// </summary>
        /// <value>
        /// The type of the request.
        /// </value>
        public long RequestTypeId { get; set; }
      
        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the request.
        /// </summary>
        /// <value>
        /// The name of the request.
        /// </value>
        public string RequestName { get; set; }
    }
}
