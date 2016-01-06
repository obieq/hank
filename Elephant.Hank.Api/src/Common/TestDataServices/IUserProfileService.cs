// ---------------------------------------------------------------------------------------------------
// <copyright file="IUserProfileService.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The IUserProfileService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Common.TestDataServices
{
    using Elephant.Hank.Common.DataService;
    using Elephant.Hank.Resources.Dto;
    using Elephant.Hank.Resources.Messages;

    /// <summary>
    /// The UserProfileService class
    /// </summary>
    public interface IUserProfileService : IBaseService<TblUserProfileDto>
    {
        /// <summary>
        /// Gets the by userid.
        /// </summary>
        /// <param name="userid">The user identifier.</param>
        /// <returns>TblUserProfileDto object</returns>
        ResultMessage<TblUserProfileDto> GetByUserId(long userid);

        /// <summary>
        /// Save and update the custom user profile along with user object
        /// </summary>
        /// <param name="userProfileDto">user's profile object</param>
        /// <param name="userid">id of the logged in user</param>
        /// <returns>the updated user profile object</returns>
        ResultMessage<TblUserProfileDto> SaveUpdateCustom(TblUserProfileDto userProfileDto, long userid);
    }
}
