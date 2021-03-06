﻿// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestDataDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-17</date>
// <summary>
//     The TblTestDataDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Elephant.Hank.Resources.Dto.Linking;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TblTestDataDto class
    /// </summary>
    public class TblTestDataDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the test identifier.
        /// </summary>
        [Required]
        public long TestId { get; set; }

        /// <summary>
        /// Gets or sets the execution sequence.
        /// </summary>
        [Required]
        public long ExecutionSequence { get; set; }

        /// <summary>
        /// Gets or sets the locator identifier identifier.
        /// </summary>
        [Required]
        public long? LocatorIdentifierId { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        public long PageId { get; set; }

        /// <summary>
        /// Gets or sets the action identifier.
        /// </summary>
        public long? ActionId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the test.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayNameValue { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string ActionValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shared test.
        /// </summary>
        public int LinkTestType { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestId
        /// </summary>
        public long? SharedTestId { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestSteps
        /// </summary>
        public List<TblLnkTestDataSharedTestDataDto> SharedTestSteps { get; set; }

        /// <summary>
        /// Gets or sets the SharedTestData
        /// </summary>
        public TblSharedTestDto SharedTest { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is step belongs to shared component.
        /// </summary>
        public bool IsStepBelongsToSharedComponent { get; set; }

        /// <summary>
        /// Gets or sets the locator value.
        /// </summary>
        public string LocatorValue { get; set; }

        /// <summary>
        /// Gets or sets the LocatorIdentifier Value
        /// </summary>
        public string LocatorIdentifierValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the variable id
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteTestId
        /// </summary>
        public long? SharedStepWebsiteTestId { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteId
        /// </summary>
        public long? SharedStepWebsiteId { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteName
        /// </summary>
        public string SharedStepWebsiteName { get; set; }

        /// <summary>
        /// Gets or sets the SharedStepWebsiteTestName
        /// </summary>
        public string SharedStepWebsiteTestName { get; set; }

        /// <summary>
        /// Gets or sets the CategoryId
        /// </summary>
        public long? DataBaseCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the shared test data identifier.
        /// can only be set and used by testqueue executable service while mapping sharedtestdata in to testdata 
        /// </summary>
        /// <value>
        /// The shared test data identifier.
        /// </value>
        public long SharedTestDataId { get; set; }

        /// <summary>
        /// Gets or sets the API test data identifier.
        /// </summary>
        /// <value>
        /// The API test data identifier.
        /// </value>
        public long? ApiTestDataId { get; set; }

        /// <summary>
        /// Gets or sets the day till past.
        /// </summary>
        /// <value>
        /// The day till past.
        /// </value>
        public long? DayTillPast { get; set; }

        /// <summary>
        /// Gets or sets the day till past by date.
        /// </summary>
        /// <value>
        /// The day till past by date.
        /// </value>
        public DateTime? DayTillPastByDate { get; set; }

        /// <summary>
        /// Gets or sets the test category identifier.
        /// </summary>
        /// <value>
        /// The test category identifier.
        /// </value>
        public long? SharedStepWebsiteTestCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the API test data.
        /// </summary>
        /// <value>
        /// The API test data.
        /// </value>
        public TblApiTestDataDto ApiTestData { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public List<NameValuePair> Headers { get; set; }

        /// <summary>
        /// Gets or sets the ignore headers.
        /// </summary>
        /// <value>
        /// The ignore headers.
        /// </value>
        public List<NameValuePair> IgnoreHeaders { get; set; }

        /// <summary>
        /// Gets or sets the type of the request.
        /// </summary>
        /// <value>
        /// The type of the request.
        /// </value>
        public string RequestType { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string RequestBody { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string ApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the modified by user.
        /// </summary>
        /// <value>
        /// The name of the modified by user.
        /// </value>
        public string ModifiedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the browser identifier.
        /// </summary>
        /// <value>
        /// The browser identifier.
        /// </value>
        public long? BrowserId { get; set; }
    }
}