using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    /// <summary>
    /// Wraps session data for a Site Engineer type User.
    /// 
    /// Reference from StackOverflow answer by M4N:
    /// http://stackoverflow.com/questions/621549/how-to-access-session-variables-from-any-class-in-asp-net
    /// </summary>
    public class SiteEngineerSession
    {
        private const string SITE_ENGINEER_SESSION_KEY =
            "ENETCare.IMS.WebApp.SiteEngineerSession";

        private SiteEngineerSession()
        {
            // TODO: Yiannis' static data classes may not exist for long
            Districts.PopulateDistricts();
            Clients.PopulateClients();

            Interventions = new IMS.Interventions.Interventions();

            // TODO: Initialize the User as an actual user
            User = new SiteEngineer
                ("Joe Blogs", "joeblogs", "abc123!",
                Districts.GetDistrictByID(0), 12, 1500);
        }

        /// <summary>
        /// The current session
        /// </summary>
        public static SiteEngineerSession Current
        {
            get
            {
                SiteEngineerSession cSession =
                    (SiteEngineerSession)HttpContext.Current.Session[SITE_ENGINEER_SESSION_KEY];

                // If the session is not initialized, initialize it
                if(cSession == null)
                {
                    cSession = new SiteEngineerSession();
                    HttpContext.Current.Session[SITE_ENGINEER_SESSION_KEY] = cSession;
                }

                return cSession;
            }
        }

        public SiteEngineer User { get; set; }
        public Interventions.Interventions Interventions { get; set; }
    }
}