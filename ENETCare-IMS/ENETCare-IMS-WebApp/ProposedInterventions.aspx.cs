﻿using System;
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
            PopulateInterventionsTable();
        }

        private void SortInterventions()
        {

            proposedInterventions = application.Interventions.GetInterventions().Where(i => i.ApprovalState == InterventionApprovalState.Proposed).ToList();
           
            approvedInterventions = application.Interventions.GetInterventions().Where(i => i.ApprovalState == InterventionApprovalState.Approved).ToList();
        }
        private void PopulateInterventionsTable()
        {
            // TODO: This population code will be replaced with a
            // DataSource and a DataGrid

            Table_In_Proposed_Interventions.Rows.Clear();

            if (isDisplayingProposed)
            {
                PopulateWithProposedInterventions();
            }
            else
            {
                PopulateWithApprovedInterventions();
            }
            
        }

        private void PopulateWithProposedInterventions()
        {
            foreach(Intervention proposedIntervention in proposedInterventions)
            {
                AddInterventionToTable(proposedIntervention);
            }
        }
        private void PopulateWithApprovedInterventions()
        {
            foreach(Intervention approvedIntervention in approvedInterventions)
            {
                AddInterventionToTable(approvedIntervention);
            }
        }

        protected void Button_ProposedInterventions_Click(object sender, EventArgs e)
        {
            isDisplayingProposed = true;
            PopulateInterventionsTable();
        }

        protected void Button_ApprovedInterventions_Click(object sender, EventArgs e)
        {
            isDisplayingProposed = false;
            PopulateInterventionsTable();
        }

        private void AddInterventionToTable(Intervention intervention)
        {
                TableRow row = new TableRow();
                Table_In_Proposed_Interventions.Rows.Add(row);

                TableCell typeCell = new TableCell();
                typeCell.Text = intervention.InterventionType.Name;
                row.Cells.Add(typeCell);
                TableCell clientCell = new TableCell();
                clientCell.Text = intervention.Client.Name;
                row.Cells.Add(clientCell);
                TableCell startDateCell = new TableCell();
                startDateCell.Text = intervention.Date.ToString();
                row.Cells.Add(startDateCell);
                TableCell lastVisitDateCell = new TableCell();
                lastVisitDateCell.Text = "?";
                row.Cells.Add(lastVisitDateCell);
                TableCell approvalCell = new TableCell();
                approvalCell.Text = intervention.ApprovalState.ToString();
                row.Cells.Add(approvalCell);
                TableCell lifeCell = new TableCell();
                if (intervention.Health != null)
                    lifeCell.Text = intervention.Health.ToString();
                else
                    lifeCell.Text = "---";
                row.Cells.Add(lifeCell);
                TableCell notesCell = new TableCell();
                notesCell.Text = intervention.Notes;
                row.Cells.Add(notesCell);
            
        }

        protected void Button_Edit_Click(object sender, EventArgs e)
        {

        }

    }
}