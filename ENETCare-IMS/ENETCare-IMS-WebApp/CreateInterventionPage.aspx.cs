using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class CreateInterventionPage : System.Web.UI.Page
    {
        private ENETCareDAO application;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = UserSession<SiteEngineer>.Current.Application;
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            // TODO: Do not use test data to create an Intervention
            Client client = application.Clients.GetClientByID(0);
            InterventionTypes types = new InterventionTypes();

            SiteEngineer engineer =
                UserSession<SiteEngineer>.Current.User;

            // TODO: Fix the Intervention ID here
            Intervention newIntervention = Intervention.Factory.CreateIntervention
                (0, types[1], client, engineer);

            newIntervention.UpdateNotes(engineer, TextBox_Notes.Text);

            application.Interventions.Add(newIntervention);

            // Redirect to Interventions table
            Response.Redirect("Interventions.aspx");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Interventions.aspx");
        }
    }
}