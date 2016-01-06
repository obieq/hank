// ---------------------------------------------------------------------------------------------------
// <copyright file="TblDbLogDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-20</date>
// <summary>
//     The TblDbLogDto class
// </summary>
// --------------------------------------------------------------------------------------------------- 

namespace Elephant.Hank.Resources.Dto
{
    /// <summary>
    /// The TblDbLogDto class
    /// </summary>
    public class TblDbLogDto : BaseTableDto
    {
        /// <summary>
        /// Gets or sets the PreviosValue
        /// </summary>
        public string PreviousValue { get; set; }

        /// <summary>
        /// Gets or sets the NewValue
        /// </summary>
        public string NewValue { get; set; }

        /// <summary>
        /// Gets or sets the ValueId
        /// </summary>
        public long ValueId { get; set; }

        /// <summary>
        /// Gets or sets the TableType
        /// </summary>
        public string TableType { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        public int OperationType { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdatedBy
        /// </summary>
        public string LastUpdatedBy { get; set; }
    }
}
