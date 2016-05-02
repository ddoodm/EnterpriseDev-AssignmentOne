using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;

using ENETCare.IMS.Interventions;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class CreateInterventionPage : System.Web.UI.Page
    {
        private ENETCareDAO application;
        private SiteEngineer engineer;

        private CultureInfo culture;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = ENETCareDAO.Context;
            engineer = (SiteEngineer)UserSession.Current.User;

            // Get application culture descriptor
            string cultureName = ConfigurationManager.AppSettings["Culture"];
            this.culture = CultureInfo.CreateSpecificCulture(cultureName);
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

        private decimal? ParseCost(string text)
        {
            // Permit null cost
            if (text.Trim() == String.Empty)
                return null;

            decimal tempCost;
            if (!decimal.TryParse(
                text,
                System.Globalization.NumberStyles.Currency,
                culture,
                out tempCost))
                throw new ArgumentException("Attempted to parse an invalid currency string.");

            return (decimal?)tempCost;
        }

        private decimal? ParseLabour(string text)
        {
            // Permit null labour
            if (text.Trim() == String.Empty)
                return null;

            decimal tempLabour;
            if (!decimal.TryParse(text, out tempLabour))
                throw new ArgumentException("Attempted to parse an invalid 'number of hours' string.");

            return tempLabour;
        }

        private void CreateIntervention()
        {
            InterventionType interventionType = GetSelectedInterventionType();
            Client client = GetSelectedClient();
            DateTime date = Calendar_Date.SelectedDate;
            string notes = TextBox_Notes.Text;

            decimal?
                cost = ParseCost(TextBox_Cost.Text),
                labour = ParseLabour(TextBox_Labour.Text);

            Intervention newIntervention =
                application.Interventions.CreateIntervention
                (interventionType, client, engineer, date, cost, labour, notes);
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            CreateIntervention();

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

        /// <summary>
        /// Refreshes default "cost" and "labour" TextBox placeholder values
        /// </summary>
        protected void Dropdown_InterventionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InterventionType type = GetSelectedInterventionType();
            TextBox_Cost.Attributes.Add("Placeholder", String.Format(culture, "{0:C}", type.Cost));
            TextBox_Labour.Attributes.Add("Placeholder", type.Labour.ToString());
        }

        /// <summary>
        /// Set the calendar's default date to today
        /// </summary>
        protected void Calendar_Date_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            Calendar_Date.TodaysDate = DateTime.Today;
            Calendar_Date.SelectedDate = Calendar_Date.TodaysDate;
        }
    }
}