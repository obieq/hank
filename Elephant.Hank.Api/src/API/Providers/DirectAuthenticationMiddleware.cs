// ---------------------------------------------------------------------------------------------------
// <copyright file="DirectAuthenticationMiddleware.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-10-12</date>
// <summary>
//     The DirectAuthenticationMiddleware class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Api.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using Elephant.Hank.Common.DataService;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// The DirectAuthenticationMiddleware
    /// </summary>
    public class DirectAuthenticationMiddleware : OwinMiddleware
    {
        /// <summary>
        /// The authentication repository
        /// </summary>
        private readonly IAuthRepository authRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectAuthenticationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The OwinMiddleware.</param>
        public DirectAuthenticationMiddleware(OwinMiddleware next)
            : base(next)
        {
            this.authRepository = StructuremapMvc.StructureMapDependencyScope.Container.GetInstance<IAuthRepository>();
        }

        /// <summary>
        /// Owin middleware overrided method
        /// </summary>
        /// <param name="context">the owin context</param>
        /// <returns>next invoke task</returns>
        public override async Task Invoke(IOwinContext context)
        {
            var header = context.Request.Headers.FirstOrDefault(m => m.Key.ToLower() == "authorization");

            if (!string.IsNullOrWhiteSpace(header.Key))
            {
                string headerValue = header.Value.FirstOrDefault().Trim();
                if (!headerValue.StartsWith("Bearer"))
                {
                    string[] parts;
                    try
                    {
                        string parameter = Encoding.UTF8.GetString(
                                          Convert.FromBase64String(
                                               header.Value.FirstOrDefault()));
                        parts = parameter.Split(':');
                    }
                    catch (Exception e)
                    {
                        throw new Exception("invalid value");
                    }

                    if (parts.Count() == 2)
                    {
                        var allowedOrigin = context.Get<string>("as:clientAllowedOrigin") ?? "*";

                        context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                        var user = await this.authRepository.FindUser(parts[0], parts[1]);
                        if (user != null)
                        {
                            string userId = user.Id.ToString();
                            string roleName = this.authRepository.GetRoleName(user.Roles.ToList()[0].RoleId);
                            var claims = new[]
                    {
                        new Claim(ClaimTypes.Name,  user.UserName)
                    };
                            var identity = new ClaimsIdentity(claims, "Basic");
                            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
                            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                            identity.AddClaim(new Claim("sub", user.UserName));
                            identity.AddClaim(new Claim("role", roleName));
                            context.Request.User = new ClaimsPrincipal(identity);

                            var props = new AuthenticationProperties(new Dictionary<string, string>
                                        {
                                            { 
                                                "userName", user.UserName
                                            },
                                            {
                                                "userId", userId
                                            },
                                            {
                                                "type", roleName
                                            }
                                        });

                            var ticket = new AuthenticationTicket(identity, props);
                        }
                    }
                }
            }

            await Next.Invoke(context);
        }
    }
}