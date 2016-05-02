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
    public class UserSession
    {
        private const string USER_SESSION_KEY =
            "ENETCare.IMS.WebApp.UserSession";

        private UserSession()
        {
        }

        /// <summary>
        /// The current session
        /// </summary>
        public static UserSession Current
        {
            get
            {
                UserSession cSession =
                    (UserSession)HttpContext.Current.Session[USER_SESSION_KEY];

                // If the session is not initialized, initialize it
                if(cSession == null)
                {
                    cSession = new UserSession();
                    HttpContext.Current.Session[USER_SESSION_KEY] = cSession;
                }

                return cSession;
            }
        }

        public void Login(User user)
        {
            this.User = user;
        }

        public User User { get; private set; }
    }
}