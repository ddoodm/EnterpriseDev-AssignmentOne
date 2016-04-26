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
        private SiteEngineer engineer;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = UserSession.Current.Application;
            engineer = (SiteEngineer)UserSession.Current.User;
        }

        private InterventionType GetSelectedInterventionType()
        {
            string selectedValue = Dropdown_InterventionType.SelectedValue;
            int selectedId = int.Parse(selectedValue);
            return application.InterventionTypes[selectedId];
        }

        private Client GetSelectedClient()
        {
            string selectedValue = ListBox_Clients.SelectedValue;
            int selectedId = int.Parse(selectedValue);
            return application.Clients.GetClientByID(selectedId);
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            InterventionType interventionType = GetSelectedInterventionType();
            Client client = GetSelectedClient();

            // TODO: Fix the Intervention ID here
            Intervention newIntervention =
                application.Interventions.CreateIntervention
                (interventionType, client, engineer);

            newIntervention.UpdateNotes(engineer, TextBox_Notes.Text);

            // Redirect to Interventions table
            Response.Redirect("Interventions.aspx");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Interventions.aspx");
        }

        protected void InterventionTypeLinqDataSource_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            e.Result = application.InterventionTypes;
        }

        protected void Button_ClientSearch_Click(object sender, EventArgs e)
        {
            // Filter the clients by the 'name' search term
            // as well as by their district
            string searchTerm = TextBox_ClientNameSearch.Text;
            List<Client> filteredClients =
                application.Clients
                .FilterByName(searchTerm)
                .FilterByDistrict(engineer.District)
                .CopyAsList();

            ListBox_Clients.DataSource = filteredClients;
            ListBox_Clients.DataTextField = "DescriptiveName";
            ListBox_Clients.DataValueField = "ID";
            ListBox_Clients.DataBind();
        }
    }
}