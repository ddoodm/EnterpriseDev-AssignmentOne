using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class UserRedirectPage : System.Web.UI.Page
    {
        /// <summary>
        /// Redirects the response to the logged-in user's home page
        /// </summary>
        private void RedirectToUserPage()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var aspUserId = User.Identity.GetUserId();
            var aspUser = manager.FindById(aspUserId);
            int enetUserId = aspUser.EnetCareUserId;
            EnetCareUser enetUser = ENETCareDAO.Context.Users.GetUserByID(enetUserId);
            Response.Redirect(string.Format("~/{0}", enetUser.HomePage));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RedirectToUserPage();
        }
    }
}