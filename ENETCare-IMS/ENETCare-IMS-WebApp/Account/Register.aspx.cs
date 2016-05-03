using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using ENETCare.IMS.WebApp.Models;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp.Account
{
    public partial class Register : Page
    {
        private EnetCareUser CreateEnetUser()
        {
            var application = ENETCareDAO.Context;

            string roleName = AccountType.SelectedValue;

            EnetCareUser user;

            switch(roleName)
            {
                case "Site Engineer":
                    user = application.Users.CreateSiteEngineer(
                        Name.Text,
                        application.Districts[1],
                        0, 0);
                    break;
                case "Manager":
                        user = application.Users.CreateManager(
                        Name.Text,
                        application.Districts[1],
                        0, 0);
                    break;
                case "Accountant":
                default:
                    user = application.Users.CreateAccountant(Name.Text);
                    break;
            }

            return user;
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            EnetCareUser enetUser = CreateEnetUser();

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text, EnetCareUserId = enetUser.ID };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);

                // Redirect to the user's default homepage
                Response.Redirect(String.Format("~/{0}", enetUser.HomePage));
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}