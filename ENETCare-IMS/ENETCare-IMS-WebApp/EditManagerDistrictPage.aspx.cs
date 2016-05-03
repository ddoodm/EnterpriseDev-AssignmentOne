using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class EditManagerDistrictPageUI : System.Web.UI.Page
    {
        public const string INTERVENTION_ID_GET_PARAMETER = "id";

        private ENETCareDAO application;

        private Manager manager;

        bool isEditing = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            application = ENETCareDAO.Context;

            // Obtain the ID of the Intervention to be displayed
            string interventionIdString = Request.QueryString[INTERVENTION_ID_GET_PARAMETER];

            int interventionId;
            if (!int.TryParse(interventionIdString, out interventionId))
            {
                throw new NullReferenceException("Intervention to edit should be set.");
            }

            // Obtain the Intervention given its ID
            manager = application.Users.GetUserByID(interventionId) as Manager;

            DisplayData();

        }

        void DisplayData()
        {
            Manager_Name.Text = manager.Name;

            if (District_Selection_DropDown.Items.Count == 0)
            {
                foreach (District district in application.Districts.Where(d => d.ID != manager.District.ID))
                {
                    District_Selection_DropDown.Items.Add(district.Name);
                }
            }

            Current_District.Text = manager.District.Name;
        }

        protected void Button_Change_District_Click(object sender, EventArgs e)
        {
            District district = application.Districts.Where(d => d.Name == District_Selection_DropDown.SelectedItem.Text).First();
            manager.UpdateDistrict(district);
            application.Users.UpdateUser(manager);
            application.RefreshUsers();
            Response.Redirect("Accountants.aspx");
        }
    }
}