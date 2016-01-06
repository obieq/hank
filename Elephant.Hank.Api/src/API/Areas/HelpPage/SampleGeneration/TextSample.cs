// ---------------------------------------------------------------------------------------------------
// <copyright file="TextSample.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The TextSample class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Areas.HelpPage.SampleGeneration
{
    using System;

    /// <summary>
    /// This represents a preformatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSample" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }

            this.Text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            TextSample other = obj as TextSample;
            return other != null && this.Text == other.Text;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}