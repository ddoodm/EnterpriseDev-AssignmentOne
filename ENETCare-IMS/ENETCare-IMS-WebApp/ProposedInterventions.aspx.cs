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
    public partial class ProposedInterventionsWebUI : System.Web.UI.Page
    {
        private ENETCareDAO application;
        private Interventions.Interventions interventions;

        private bool isDisplayingProposed = true;
        private int selectedRowIndex = 0;

        private List<Intervention> proposedInterventions = new List<Intervention>();
        private List<Intervention> approvedInterventions = new List<Intervention>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the Application instance
            // TODO: Do not use a Site Engineer session here; use Manager.
            application = ENETCareDAO.Context;
            interventions = application.Interventions;
            SortInterventions();
            Table_ProposedInterventions.DataSource = proposedInterventions;
            Table_ProposedInterventions.DataBind();
        }

        private void SortInterventions()
        {
            Manager manager = UserSession.Current.User as Manager;
            proposedInterventions = interventions.FilterByState(InterventionApprovalState.Proposed).Where(i => i.Client.District.ID == manager.District.ID).ToList();
            approvedInterventions = interventions.FilterByState(InterventionApprovalState.Approved).Where(i => i.Client.District.ID == manager.District.ID).ToList();
        }

  

        protected void Button_ProposedInterventions_Click(object sender, EventArgs e)
        {
            isDisplayingProposed = true;
            SortInterventions();
            Table_ProposedInterventions.DataSource = proposedInterventions;
            Table_ProposedInterventions.DataBind();
            Response.Write(Request.RawUrl.ToString());
        }

        protected void Button_ApprovedInterventions_Click(object sender, EventArgs e)
        {
            isDisplayingProposed = false;
            SortInterventions();
            Table_ProposedInterventions.DataSource = approvedInterventions;
            Table_ProposedInterventions.DataBind();
            Response.Write(Request.RawUrl.ToString());
        }


        protected void ProposedTable_Interventions_Load(object sender, EventArgs e)
        {
            // Set GridView to render its header in a HTML 'thead'
            //Table_ProposedInterventions.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
}