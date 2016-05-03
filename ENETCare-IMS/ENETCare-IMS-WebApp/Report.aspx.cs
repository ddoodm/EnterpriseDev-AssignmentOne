using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class ReportPage : Page
    {
        private ENETCareDAO application;
        private List<EnetCareUser> users;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = new ENETCareDAO();
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Accountants.aspx");
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {

        }

        //This should really be done through SQL LAMBA equations. 
        private decimal TotalCost(List<SiteEngineer> users)
        {
            decimal totalCost = 0M;

            for (int i = 0; i < users.Count; i++)
            {
                totalCost += users[i].MaxApprovableCost;
            }

            return totalCost;
        }

        private decimal AverageCost(List<SiteEngineer> users)
        {
            decimal avgCost = 0M;

            avgCost = TotalCost(users) / users.Count;

            return avgCost;

        }

        private List<SiteEngineer> SortEngineers(List<EnetCareUser> users)
        {
            List<SiteEngineer> tempEngineers = new List<SiteEngineer>();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i] is SiteEngineer)
                    tempEngineers.Add((SiteEngineer)users[i]);
            }

            return tempEngineers;
        }

        private decimal TotalCostEngineers(List<EnetCareUser> users)
        {
            return TotalCost(SortEngineers(users));
        }

        private decimal AverageCostEngineers(List<EnetCareUser> users)
        {
            return AverageCost(SortEngineers(users));
        }

        private decimal CostByDistrict(List<EnetCareUser> users, District districtID)
        {
            decimal totalCostByDistrict = 0M;

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i] is Manager)
                {
                    if (((Manager)users[i]).District == districtID)
                    {
                        totalCostByDistrict += ((Manager)users[i]).MaxApprovableCost;
                    }

                }
                else if (users[i] is SiteEngineer)
                {
                    if (((SiteEngineer)users[i]).District == districtID)
                    {
                        totalCostByDistrict += ((SiteEngineer)users[i]).MaxApprovableCost;
                    }
                }
            }

            return totalCostByDistrict;
        }

        private decimal MonthlyByDistrict(List<EnetCareUser> users, District districtID)
        {
            decimal monthlyCost = 0M;

            monthlyCost = CostByDistrict(users, districtID) / 12;

            return monthlyCost;
        }


    }
}