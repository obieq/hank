// ---------------------------------------------------------------------------------------------------
// <copyright file="EfIgnoreDbLogAttribute.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-21</date>
// <summary>
//     The EfIgnoreDbLogAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Attributes
{
    using System;

    /// <summary>
    /// The EfIgnoreDbLogAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EfIgnoreDbLogAttribute : Attribute
    {
    }
}
