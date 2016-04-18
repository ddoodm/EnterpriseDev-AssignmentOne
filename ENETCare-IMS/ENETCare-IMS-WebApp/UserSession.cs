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
    public class UserSession<UserType> where UserType: User
    {
        private const string USER_SESSION_KEY =
            "ENETCare.IMS.WebApp.UserSession";

        private UserSession()
        {
            Application = new ENETCareDAO();

            // TODO: Initialize the User as an actual user
            User testUser = new SiteEngineer
                ("Joe Blogs", "joeblogs", "abc123!", 0,
                Application.Districts.GetDistrictByID(0), 12, 1500);
            User = (UserType)testUser;
        }

        /// <summary>
        /// The current session
        /// </summary>
        public static UserSession<UserType> Current
        {
            get
            {
                UserSession<UserType> cSession =
                    (UserSession<UserType>)HttpContext.Current.Session[USER_SESSION_KEY];

                // If the session is not initialized, initialize it
                if(cSession == null)
                {
                    cSession = new UserSession<UserType>();
                    HttpContext.Current.Session[USER_SESSION_KEY] = cSession;
                }

                return cSession;
            }
        }

        public UserType User { get; private set; }
        public ENETCareDAO Application { get; private set; }
    }
}