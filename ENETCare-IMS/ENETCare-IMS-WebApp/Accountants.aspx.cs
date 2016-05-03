using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

using ENETCare.IMS;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class AccountantsPage : Page
    {
        private ENETCareDAO application;
        private List<EnetCareUser> users;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = new ENETCareDAO();
            users = application.Users.GetUsers();
        }
        

        protected void Button_Generate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        protected void Button_Edit_Click(object sender, EventArgs e)
        {

        }
        
        protected void SiteEngineersDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = application.Users.GetSiteEngineers();
        }

        protected void ManagersDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = application.Users.GetManagers();
        }

        protected void Managers_Load(object sender, EventArgs e)
        {
            Table_Managers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void SiteEngineers_Load(object sender, EventArgs e)
        {
            Table_SiteEngineers.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}