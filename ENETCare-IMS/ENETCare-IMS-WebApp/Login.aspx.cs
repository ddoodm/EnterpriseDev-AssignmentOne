using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    /*
     * Temporary login stub until database stuff is sorted out. Login details
     * are checked against local credential lists
     */
    public partial class Login : System.Web.UI.Page
    {
        ENETCareDAO application;
        Users.Users users;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = UserSession.Current.Application;
            users = new Users.Users(application);
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            User selectedUser = LoginUser(textName.Text, textPass.Text);
            UserSession.Current.Login(selectedUser);

            if(selectedUser == null)
                labelError.Text = "Invalid login";
            else
            {
                if (selectedUser is SiteEngineer)
                {
                    Response.Redirect("Interventions.aspx");
                }
                else if (selectedUser is Manager)
                {
                    Response.Redirect("Clients.aspx");
                }
                else if (selectedUser is Accountant)
                {
                    Response.Redirect("Accountants.aspx");
                }
            }
        }

        private User LoginUser(string username, string password)
        {
            return users.Login(username, password);
        }
    }
}