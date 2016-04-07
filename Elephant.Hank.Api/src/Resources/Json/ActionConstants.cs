// ---------------------------------------------------------------------------------------------------
// <copyright file="ActionConstants.cs" company="Elephant Insurance Services, LLC">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Vyom Sharma</author>
// <date>2015-05-08</date>
// <summary>
//     The ActionConstants class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Elephant.Hank.Resources.Json
{
    using System.Configuration;

    using Elephant.Hank.Resources.Extensions;

    /// <summary>
    /// The ActionConstants class
    /// </summary>
    public class ActionConstants
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static ActionConstants instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ActionConstants"/> class from being created.
        /// </summary>
        private ActionConstants()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static ActionConstants Instance
        {
            get
            {
                return instance ?? (instance = new ActionConstants());
            }
        }

        /// <summary>
        /// Gets the SetVariable Action's Identifier
        /// </summary>
        public long SetVariableActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SetVariableActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the SetVariable Action's Identifier
        /// </summary>
        public long SetVariableManuallyActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SetVariableManuallyActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the DeclareVariable Action's Identifier
        /// </summary>
        public long DeclareVariableActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["DeclareVariableActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the TakeScreenShot Action's Identifier
        /// </summary>
        public long TakeScreenShotActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["TakeScreenShotActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the LoadNewUr Action's Identifier
        /// </summary>
        public long LoadNewUrlActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["LoadNewUrlActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the SwitchWebsiteType Action's Identifier
        /// </summary>
        public long SwitchWebsiteTypeActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SwitchWebsiteTypeActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the AssertUrlToContain Action's Identifier
        /// </summary>
        public long AssertUrlToContainActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["AssertUrlToContainActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the HandleBrowserAlertPopup Action's Identifier
        /// </summary>
        public long HandleBrowserAlertPopupActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["HandleBrowserAlertPopupActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Wait Action's Identifier
        /// </summary>
        public long WaitActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["WaitActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Load Partial Url Action's Identifier
        /// </summary>
        public long LoadPartialUrlActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["LoadPartialUrlActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Log Text Action's Identifier
        /// </summary>
        public long LogTextActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["LogTextActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Log Text Action's Identifier
        /// </summary>
        public long AssertToEqualActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["AssertToEqualActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Switch window Action's Identifier
        /// </summary>
        public long SwitchWindowActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SwitchWindowActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the Switch window Action's Identifier
        /// </summary>
        public long IgnoreLoadNeUrlActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["IgnoreLoadNeUrlActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the sen key action identifier.
        /// </summary>
        /// <value>
        /// The sen key action identifier.
        /// </value>
        public long SendKeyActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SendKeyActionId"].ToInt64();
            }
        }

        /// <summary>
        /// Gets the terminate test action id
        /// </summary>
        public long TerminateTestActionId
        {
            get
            {
                return ConfigurationManager.AppSettings["TerminateTestActionId"].ToInt64();
            }
        }
    }
}
