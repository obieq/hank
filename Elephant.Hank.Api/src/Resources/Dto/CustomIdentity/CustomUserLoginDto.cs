// ---------------------------------------------------------------------------------------------------
// <copyright file="CustomUserLoginDto.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-09-23</date>
// <summary>
//     The CustomUserLoginDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Dto.CustomIdentity
{
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// The custom user login.
    /// </summary>
    public class CustomUserLoginDto : IdentityUserLogin<long>
    {
    }
}
