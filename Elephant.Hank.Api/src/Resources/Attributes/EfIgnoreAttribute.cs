// ---------------------------------------------------------------------------------------------------
// <copyright file="EfIgnoreAttribute.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-08</date>
// <summary>
//     The EfIgnoreAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Attributes
{
    using System;

    /// <summary>
    /// The DisplayTextAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class EfIgnoreAttribute : Attribute
    {
    }
}