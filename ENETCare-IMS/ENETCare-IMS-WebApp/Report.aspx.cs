using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class ReportPageUI : Page
    {
        private ENETCareDAO application;
        private List<EnetCareUser> users;
        private string[] ReportTypes = { "Total Costs by Engineer", "Average Costs by Engineer", "Costs by District", "Monthly Cost for District" };

        protected void Page_Load(object sender, EventArgs e)
        {
            application = new ENETCareDAO();

            users = application.Users.GetUsers();


            PopulateDropDowns();
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Accountants.aspx");
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {

        }

        protected void Button_Generate_Report_Click(object sender, EventArgs e)
        {
            switch (ReportsDropDown.SelectedIndex)
            {
                case 0:
                    {
                        GenerateTotalCostsReport();
                        break;
                    }
                case 1:
                    {
                        GenerateAverageCostsReport();
                        break;
                    }
                case 2:
                    {
                        GenerateDistrictCostsReport();
                        break;
                    }
                case 3:
                    {
                        //TODO: Make Proposed Interventions screen Datasource with Edit Button (so we can Complete some)
                        //TODO: Make drop down menu to select Districts if case 3 is selected.
                        GenerateMonthlyCostsReport((application.Districts.GetListCopy())[DistrictsDropDown.SelectedIndex]);
                        break;
                    }
            }

        }



        private void PopulateDropDowns()
        {
            if (ReportsDropDown.Items.Count == 0)
            {
                foreach (string type in ReportTypes)
                {
                    ReportsDropDown.Items.Add(type);
                }
            }

            if (DistrictsDropDown.Items.Count == 0)
            {
                foreach (District district in application.Districts.GetListCopy())
                    DistrictsDropDown.Items.Add(district.Name);
            }
        }

        void GenerateTotalCostsReport()
        {
            StringBuilder report = new StringBuilder();
            List<IMS.Interventions.Intervention> interventions = application.Interventions.GetInterventions();
            List<SiteEngineer> engineers = application.Users.GetSiteEngineers().OrderBy(e => e.Name).ToList();
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br />");
            report.Append("               TOTAL COSTS BY ENGINEER              ");
            report.Append("<br />");
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br /> <br />");
            foreach (SiteEngineer engineer in engineers)
            {
                decimal totalLaborHours = interventions.Where(i => i.SiteEngineer.ID == engineer.ID).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Labour).Sum();
                decimal totalCosts = interventions.Where(i => i.SiteEngineer.ID == engineer.ID).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Cost).Sum();

                report.Append(engineer.Name.ToUpper());
                report.Append("<br />");
                report.AppendFormat("Total Labour Hours: {0} hours", totalLaborHours);
                report.Append("<br />");
                report.AppendFormat("Total Costs: ${0}", totalCosts);

                if (engineer != engineers.Last())
                    report.Append("<br /><br />---oOo---<br /><br />");
            }

            ReportTextLabel.Text = report.ToString();
        }

        void GenerateAverageCostsReport()
        {
            StringBuilder report = new StringBuilder();
            List<IMS.Interventions.Intervention> interventions = application.Interventions.GetInterventions();
            List<SiteEngineer> engineers = application.Users.GetSiteEngineers().OrderBy(e => e.Name).ToList();
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br />");
            report.Append("              AVERAGE COSTS BY ENGINEER             ");
            report.Append("<br />");
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br /> <br />");
            foreach (SiteEngineer engineer in engineers)
            {
                decimal totalLaborHours = interventions.Where(i => i.SiteEngineer.ID == engineer.ID).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Labour).DefaultIfEmpty(0).Average();
                decimal totalCosts = interventions.Where(i => i.SiteEngineer.ID == engineer.ID).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Cost).DefaultIfEmpty(0).Average();

                report.Append(engineer.Name.ToUpper());
                report.Append("<br />");
                report.AppendFormat("Average Labour Hours: {0} hours", totalLaborHours);
                report.Append("<br />");
                report.AppendFormat("Average Costs: ${0}", totalCosts);

                if (engineer != engineers.Last())
                    report.Append("<br /><br />---oOo---<br /><br />");
            }

            ReportTextLabel.Text = report.ToString();
        }

        void GenerateDistrictCostsReport()
        {
            StringBuilder report = new StringBuilder();
            List<District> districts = application.Districts.OrderBy(d => d.Name).ToList();
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br />");
            report.Append("               TOTAL COSTS BY DISTRICT              ");
            report.Append("<br />");
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br /> <br />");

            decimal companyTotalHours = 0, companyTotalCosts = 0;

            foreach (District district in districts)
            {
                decimal totalLaborHours = application.Interventions.FilterByDistrict(district).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Labour).Sum();
                decimal totalCosts = application.Interventions.FilterByDistrict(district).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Select(i => i.Cost).Sum();

                companyTotalHours += totalLaborHours;
                companyTotalCosts += totalCosts;

                report.Append(district.Name.ToUpper());
                report.Append("<br />");
                report.AppendFormat("Total Labour Hours: {0} hours", totalLaborHours);
                report.Append("<br />");
                report.AppendFormat("Total Costs: ${0}", totalCosts);

                report.Append("<br /><br />---oOo---<br /><br />");


            }

            report.Append(@"            --//COMPANY TOTALS\\--             ");
            report.Append("<br /> <br />");
            report.AppendFormat("Total Labour Hours: {0} hours", companyTotalHours);
            report.Append("<br />");
            report.AppendFormat("Total Costs: ${0}", companyTotalCosts);

            ReportTextLabel.Text = report.ToString();
        }

        void GenerateMonthlyCostsReport(District district)
        {
            StringBuilder report = new StringBuilder();

            int[] months = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br />");
            report.Append("               TOTAL COSTS BY DISTRICT              ");
            report.Append("<br />");
            report.Append("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            report.Append("<br /> <br />");

            report.Append(district.Name.ToUpper());
            report.Append("<br />");

            foreach (int month in months)
            {
                decimal totalLaborHours = application.Interventions.FilterByDistrict(district).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Where(i => i.Date.Month == month).Select(i => i.Labour).Sum();
                decimal totalCosts = application.Interventions.FilterByDistrict(district).Where(i => i.ApprovalState == Interventions.InterventionApprovalState.Completed).Where(i => i.Date.Month == month).Select(i => i.Cost).Sum();

                report.AppendFormat("Total Labour Hours for {0}: {1} hours", GetMonthName(month), totalLaborHours);
                report.Append("<br />");
                report.AppendFormat("Total Costs for {0}: ${1}", GetMonthName(month), totalCosts);

                report.Append("<br /><br />---oOo---<br /><br />");
            }

            ReportTextLabel.Text = report.ToString();
        }

        string GetMonthName(int month)
        {
            switch (month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return string.Empty;

            }
        }

        protected void Index_Changed(object sender, EventArgs e)
        {
            switch (ReportsDropDown.SelectedIndex)
            {
                case 3:
                    {
                        DistrictsDropDown.Visible = true;
                        break;
                    }
                default:
                    {
                        DistrictsDropDown.Visible = false;
                        break;
                    }
            }
        }

    }
}