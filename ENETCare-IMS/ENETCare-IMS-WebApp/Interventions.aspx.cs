﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Interventions;

namespace ENETCare.IMS.WebApp
{
    public partial class InterventionsWebUI : System.Web.UI.Page
    {
        Interventions.Interventions interventions;

        protected void Page_Load(object sender, EventArgs e)
        {
            Districts.PopulateDistricts();
            Clients.PopulateClients();

            interventions = new Interventions.Interventions();

            PopulateInterventionsTable();
        }

        private void PopulateInterventionsTable()
        {
            // TODO: This population code will be replaced with a
            // DataSource and a DataGrid
            for(int i = 0; i < interventions.Count; i++)
            {
                Intervention intervention = interventions[i];

                TableRow row = new TableRow();
                Table_Interventions.Rows.Add(row);

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
                lifeCell.Text = "Fix this";
                row.Cells.Add(lifeCell);
                TableCell notesCell = new TableCell();
                notesCell.Text = intervention.Notes;
                row.Cells.Add(notesCell);
            }
        }

        protected void Button_CreateNewIntervention_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateInterventionPage.aspx");
        }
    }
}