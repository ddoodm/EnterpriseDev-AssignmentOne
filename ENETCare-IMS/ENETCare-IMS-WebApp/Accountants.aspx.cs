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
        private List<User> users;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = new ENETCareDAO();

            PopulateTempUsers();
            PopulateAccountTable();

        }

        private void PopulateTempUsers()
        {
            /**Temp populating Users to input into display accountant table
            TODO: Remove this method*/
            users = new List<User>();

            users.Add(new Manager("SmithJohnes", "Smith", "1234", application.Districts.GetDistrictByID(3), 12, 1000));
            users.Add(new SiteEngineer("JohnSmith", "John", "1234", application.Districts.GetDistrictByID(2), 12, 1000));

        }

        private void PopulateAccountTable()
        {
            for (int i = 0; i < users.Count; i++)
            {
                TableRow row = new TableRow();
                Table_Accountants.Rows.Add(row);

                TableCell typeCell = new TableCell();
                typeCell.Text = users[i].Title;
                row.Cells.Add(typeCell);

                TableCell nameCell = new TableCell();
                nameCell.Text = users[i].Name.ToString();
                row.Cells.Add(nameCell);

                TableCell districtCell = new TableCell();
                TableCell costCell = new TableCell();

                if (users[i] is Manager)
                {
                    districtCell.Text = ((Manager)users[i]).District.ToString();
                    costCell.Text = "$" + ((Manager)users[i]).MaxApprovableCost.ToString();
                }
                else if (users[i] is SiteEngineer)
                {
                    districtCell.Text = ((SiteEngineer)users[i]).District.ToString();
                    costCell.Text = "$" + ((SiteEngineer)users[i]).MaxApprovableCost.ToString();
                }

                row.Cells.Add(districtCell);
                row.Cells.Add(costCell);
            }
        }

        protected void Button_Generate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx");
        }

        protected void Button_Edit_Click(object sender, EventArgs e)
        {

        }


    }
}