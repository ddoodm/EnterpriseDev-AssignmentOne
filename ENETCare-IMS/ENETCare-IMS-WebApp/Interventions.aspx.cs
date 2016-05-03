using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;
using ENETCare.IMS.WebApp.Controls;

namespace ENETCare.IMS.WebApp
{
    public partial class InterventionsWebUI : System.Web.UI.Page
    {
        private ENETCareDAO application;
        private Interventions.Interventions interventions;

        protected new ILocalizedUser User;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain interventions from application context
            application = ENETCareDAO.Context;
            interventions = application.Interventions;
            User = (ILocalizedUser)UserSession.Current.User;
        }

        protected void Button_CreateNewIntervention_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateInterventionPage.aspx");
        }

        protected void Button_Clients_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clients.aspx");
        }

        protected void InterventionsDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = interventions.FilterByDistrict(User.District);
        }

        protected void Table_Interventions_Load(object sender, EventArgs e)
        {
            // Set GridView to render its header in a HTML 'thead'
            Table_Interventions.HeaderRow.TableSection
                = TableRowSection.TableHeader;
        }
    }
}