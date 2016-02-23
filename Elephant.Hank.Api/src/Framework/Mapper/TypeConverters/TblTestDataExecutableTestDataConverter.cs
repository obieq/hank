// ---------------------------------------------------------------------------------------------------
// <copyright file="TblTestDataExecutableTestDataConverter.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-27</date>
// <summary>
//     The TblTestDataExecutableTestDataConverter class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Framework.Mapper.TypeConverters
{
    using System.Linq;

    using AutoMapper;
    using Elephant.Hank.DataService.DBSchema;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Models;

    /// <summary>
    /// The TblTestDataExecutableTestDataConverter class
    /// </summary>
    public class TblTestDataExecutableTestDataConverter : ITypeConverter<TblTestDataDto, ExecutableTestData>
    {
        /// <summary>
        /// Performs conversion from source to destination type
        /// </summary>
        /// <param name="context">Resolution context</param>
        /// <returns>
        /// Destination object
        /// </returns>
        public ExecutableTestData Convert(ResolutionContext context)
        {
            var src = context.SourceValue as TblTestDataDto;

            ExecutableTestData result = null;

            if (src != null)
            {
                result = new ExecutableTestData
                             {
                                 Id = src.Id,
                                 Action = src.ActionValue,
                                 Value = src.Value,
                                 ExecutionSequence = src.ExecutionSequence,
                                 VariableName = src.VariableName != null ? src.VariableName : string.Empty,
                                 IsOptional = src.IsOptional,
                                 StepType = src.LinkTestType,
                                 CategoryId = src.DataBaseCategoryId,
                                 SharedTestDataId = src.SharedTestDataId
                             };
                result.DisplayName = src.DisplayNameValue;
                result.Locator = src.LocatorValue + string.Empty;
                result.LocatorIdentifier = src.LocatorIdentifierValue + string.Empty;
            }

            return result;
        }
    }
}