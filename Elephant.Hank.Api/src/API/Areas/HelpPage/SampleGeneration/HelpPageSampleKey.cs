// ---------------------------------------------------------------------------------------------------
// <copyright file="HelpPageSampleKey.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gurpreet Singh</author>
// <date>2015-04-10</date>
// <summary>
//     The HelpPageSampleKey class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Areas.HelpPage.SampleGeneration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net.Http.Headers;

    /// <summary>
    /// This is used to identify the place where the sample should be applied.
    /// </summary>
    public class HelpPageSampleKey
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageSampleKey" /> class.
        /// Creates a new <see cref="HelpPageSampleKey" /> based on media type and CLR type.
        /// </summary>
        /// <param name="mediaType">The media type.</param>
        /// <param name="type">The CLR type.</param>
        /// <exception cref="System.ArgumentNullException">
        /// mediaType
        /// or
        /// type
        /// </exception>
        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, Type type)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            this.ControllerName = string.Empty;
            this.ActionName = string.Empty;
            this.ParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            this.ParameterType = type;
            this.MediaType = mediaType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageSampleKey" /> class.
        /// Creates a new <see cref="HelpPageSampleKey" /> based on <see cref="SampleDirection" />, controller name, action name and parameter names.
        /// </summary>
        /// <param name="sampleDirection">The <see cref="SampleDirection" />.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="parameterNames">The parameter names.</param>
        public HelpPageSampleKey(SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }

            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }

            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            this.SampleDirection = sampleDirection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpPageSampleKey" /> class.
        /// Creates a new <see cref="HelpPageSampleKey" /> based on media type, <see cref="SampleDirection" />, controller name, action name and parameter names.
        /// </summary>
        /// <param name="mediaType">The media type.</param>
        /// <param name="sampleDirection">The <see cref="SampleDirection" />.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="parameterNames">The parameter names.</param>
        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }

            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int)sampleDirection, typeof(SampleDirection));
            }

            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }

            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }

            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }

            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.MediaType = mediaType;
            this.ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            this.SampleDirection = sampleDirection;
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string ControllerName { get; private set; }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string ActionName { get; private set; }

        /// <summary>
        /// Gets the media type.
        /// </summary>
        /// <value>
        /// The media type.
        /// </value>
        public MediaTypeHeaderValue MediaType { get; private set; }

        /// <summary>
        /// Gets the parameter names.
        /// </summary>
        public HashSet<string> ParameterNames { get; private set; }

        /// <summary>
        /// Gets the parameter type.
        /// </summary>
        public Type ParameterType { get; private set; }

        /// <summary>
        /// Gets the <see cref="SampleDirection"/>.
        /// </summary>
        public SampleDirection? SampleDirection { get; private set; }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// The <see cref="bool" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            HelpPageSampleKey otherKey = obj as HelpPageSampleKey;
            if (otherKey == null)
            {
                return false;
            }

            return string.Equals(this.ControllerName, otherKey.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(this.ActionName, otherKey.ActionName, StringComparison.OrdinalIgnoreCase) &&
                (this.MediaType == otherKey.MediaType || (this.MediaType != null && this.MediaType.Equals(otherKey.MediaType))) &&
                this.ParameterType == otherKey.ParameterType &&
                this.SampleDirection == otherKey.SampleDirection &&
                this.ParameterNames.SetEquals(otherKey.ParameterNames);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode = this.ControllerName.ToUpperInvariant().GetHashCode() ^ this.ActionName.ToUpperInvariant().GetHashCode();
            if (this.MediaType != null)
            {
                hashCode ^= this.MediaType.GetHashCode();
            }

            if (this.SampleDirection != null)
            {
                hashCode ^= this.SampleDirection.GetHashCode();
            }

            if (this.ParameterType != null)
            {
                hashCode ^= this.ParameterType.GetHashCode();
            }

            foreach (string parameterName in this.ParameterNames)
            {
                hashCode ^= parameterName.ToUpperInvariant().GetHashCode();
            }

            return hashCode;
        }
    }
}
