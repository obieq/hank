// ---------------------------------------------------------------------------------------------------
// <copyright file="SampleDirection.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The SampleDirection class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Areas.HelpPage.SampleGeneration
{
    /// <summary>
    /// Indicates whether the sample is used for request or response
    /// </summary>
    public enum SampleDirection
    {
        /// <summary>
        /// The request
        /// </summary>
        Request = 0,

        /// <summary>
        /// The response
        /// </summary>
        Response
    }
}