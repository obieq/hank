// ---------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-06-03</date>
// <summary>
//     The EnumExtensions class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using Elephant.Hank.Resources.Attributes;

    /// <summary>
    /// The EnumfExtensions class
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the attribute text.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Attribute value</returns>
        public static string GetAttributeText<TEnum>(this TEnum? value) where TEnum : struct
        {
            return (value ?? default(TEnum)).GetAttributeText();
        }

        /// <summary>
        /// Gets the attribute text.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>Attribute value</returns>
        public static string GetAttributeText<TEnum>(this TEnum value) where TEnum : struct
        {
            string retValue;
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var displayAttr = type.GetField(name).GetCustomAttributes(false).OfType<DisplayTextAttribute>().SingleOrDefault();

            if (displayAttr == null)
            {
                var descriptionAttr = type.GetField(name).GetCustomAttributes(false).OfType<DescriptionAttribute>().SingleOrDefault();

                retValue = descriptionAttr == null ? name : descriptionAttr.Description;
            }
            else
            {
                retValue = displayAttr.Text;
            }

            return retValue;
        }
    }
}
