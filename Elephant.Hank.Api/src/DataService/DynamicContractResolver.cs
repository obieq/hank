// ---------------------------------------------------------------------------------------------------
// <copyright file="DynamicContractResolver.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-08-21</date>
// <summary>
//     The DynamicContractResolver class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Elephant.Hank.Resources.Attributes;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The DynamicContractResolver class
    /// </summary>
    public class DynamicContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Creates properties for the given Newtonsoft.Json.Serialization.JsonContract.
        /// </summary>
        /// <param name="type">The type to create properties for.</param>
        /// <param name="memberSerialization">The member serialization mode for the type.</param>
        /// <returns>Properties for the given Newtonsoft.Json.Serialization.JsonContract.</returns>
        protected override IList<JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);
            List<string> ignoreMembers = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(EfIgnoreDbLogAttribute), true).Any()).Select(x => x.Name).ToList();
            ignoreMembers.AddRange(type.GetProperties().Where(p => p.GetGetMethod().IsVirtual).Select(x => x.Name).ToList());
            IList<JsonProperty> prop = properties.Where(p => !ignoreMembers.Contains(p.PropertyName)).ToList();
            return prop;
        }
    }
}
