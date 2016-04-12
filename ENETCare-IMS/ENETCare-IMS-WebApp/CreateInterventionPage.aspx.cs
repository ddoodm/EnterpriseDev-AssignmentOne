using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Interventions;

namespace ENETCare.IMS.WebApp
{
    public partial class CreateInterventionPage : System.Web.UI.Page
    {
        Interventions.Interventions interventions;

        protected void Page_Load(object sender, EventArgs e)
        {
            interventions = SiteEngineerSession.Current.Interventions;
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            Districts.PopulateDistricts();
            Clients.PopulateClients();

            // TODO: Do not use test data to create an Intervention
            Client client = Clients.GetClientByID(0);
            InterventionTypes types = new InterventionTypes();

            Users.SiteEngineer engineer =
                SiteEngineerSession.Current.User;

            // TODO: Fix the Intervention ID here
            Intervention newIntervention = Intervention.Factory.CreateIntervention
                (0, types[1], client, engineer);

            newIntervention.UpdateNotes(engineer, TextBox_Notes.Text);

            interventions.Add(newIntervention);

            // Redirect to Interventions table
            Response.Redirect("Interventions.aspx");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Interventions.aspx");
        }
    }
}