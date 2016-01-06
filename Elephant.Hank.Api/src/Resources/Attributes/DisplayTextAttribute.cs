// ---------------------------------------------------------------------------------------------------
// <copyright file="DisplayTextAttribute.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The DisplayTextAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Attributes
{
    using System;

    /// <summary>
    /// The DisplayTextAttribute class
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class DisplayTextAttribute : Attribute
    {
        /// <summary>
        /// The text
        /// </summary>
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayTextAttribute"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public DisplayTextAttribute(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text 
        {
            get
            {
                return this.text;
            }
        }
    }
}
