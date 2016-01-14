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

    /// <summary>
    /// The ActionConstants class
    /// </summary>
    public class ActionConstants
    {
        /// <summary>
        /// Gets the SetVariable Action's Identifier
        /// </summary>
        public int SetVariableActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["SetVariableActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the SetVariable Action's Identifier
        /// </summary>
        public int SetVariableManuallyActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["SetVariableManuallyActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the DeclareVariable Action's Identifier
        /// </summary>
        public int DeclareVariableActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["DeclareVariableActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the TakeScreenShot Action's Identifier
        /// </summary>
        public int TakeScreenShotActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["TakeScreenShotActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the LoadNewUr Action's Identifier
        /// </summary>
        public int LoadNewUrlActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["LoadNewUrlActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the SwitchWebsiteType Action's Identifier
        /// </summary>
        public int SwitchWebsiteTypeActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["SwitchWebsiteTypeActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the AssertUrlToContain Action's Identifier
        /// </summary>
        public int AssertUrlToContainActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["AssertUrlToContainActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the HandleBrowserAlertPopup Action's Identifier
        /// </summary>
        public int HandleBrowserAlertPopupActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["HandleBrowserAlertPopupActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Wait Action's Identifier
        /// </summary>
        public int WaitActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["WaitActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Load Partial Url Action's Identifier
        /// </summary>
        public int LoadPartialUrlActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["LoadPartialUrlActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Log Text Action's Identifier
        /// </summary>
        public int LogTextActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["LogTextActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Log Text Action's Identifier
        /// </summary>
        public int AssertToEqualActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["AssertToEqualActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Switch window Action's Identifier
        /// </summary>
        public int SwitchWindowActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["SwitchWindowActionId"].ToString());
            }
        }

        /// <summary>
        /// Gets the Switch window Action's Identifier
        /// </summary>
        public int IgnoreLoadNeUrlActionId
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IgnoreLoadNeUrlActionId"].ToString());
            }
        }
    }
}
