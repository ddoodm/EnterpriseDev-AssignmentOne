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
    public partial class InterventionEditPageWebUI : System.Web.UI.Page
    {
        public const string INTERVENTION_ID_GET_PARAMETER = "id";

        private ENETCareDAO application;

        Intervention editIntervention;
        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain application context
            application = ENETCareDAO.Context;

            // Obtain the ID of the Intervention to be displayed
            string interventionIdString = Request.QueryString[INTERVENTION_ID_GET_PARAMETER];

            int interventionId;
            if (!int.TryParse(interventionIdString, out interventionId))
            {
                throw new NullReferenceException("Intervention to edit should be set.");
            }

            // Obtain the Intervention given its ID
            editIntervention = application.Interventions[interventionId];

            DisplayInterventionData();
            SetEditControlsForUserRole();
        }

        void DisplayInterventionData()
        {
            //Display Core Information
            InterventionIDLabel.Text = editIntervention.ID.ToString();
            SiteEngineerLabel.Text = editIntervention.SiteEngineer.Name;
            ClientLabel.Text = editIntervention.Client.Name;
            InterventionTypeLabel.Text = editIntervention.InterventionType.Name;
            DateLabel.Text = editIntervention.Date.ToString();
            //TODO: Edit code in Intervention for Labour and Cost to return default InterventionType values if not set to anything
            //Use a variable that's null initially, and only return it if it's not null.
            LabourLabel.Text = editIntervention.Labour.ToString();

            // Display currency in the format described by the application's culture
            string cultureName = ConfigurationManager.AppSettings["Culture"];
            CultureInfo culture = CultureInfo.CreateSpecificCulture(cultureName);
            CostLabel.Text = String.Format(culture, "{0:C}", editIntervention.Cost);

            //Display Admin Information
            StateLabel.Text = editIntervention.ApprovalState.ToString();
            SetApprovalButtons();
            
            //Display Quality Information
            Intervention_Notes_Textbox.Text = editIntervention.Notes;
            Intervention_Notes_Textbox.ReadOnly = true;
            ShowQualityInformation();
        }

        void SetApprovalButtons()
        {
            InterventionApprovalState state = editIntervention.ApprovalState;

            if (state == InterventionApprovalState.Proposed)
            {
               CompleteButton.Visible = false;
               ApprovalUserGroup.Visible = false;
            }
            else if (state == InterventionApprovalState.Approved)
            {
               ApproveButton.Visible = false;
               ApprovalUserGroup.Visible = true;
               CompleteButton.Visible = true;
               ApprovalUserLabel.Text = editIntervention.ApprovingUser.Name;
            }
            else if(state == InterventionApprovalState.Cancelled || state == InterventionApprovalState.Completed)
            {
                ApprovalUserGroup.Visible = false;
                CompleteButton.Visible = false;
                ApproveButton.Visible = false;
                ApprovalUserLabel.Visible = false;
                CancelButton.Visible = false;
            }
        }

        void ShowQualityInformation()
        {
            if(editIntervention.Health == null)
            {
                HealthGroup.Visible = false;
            }
            else
            {
                HealthLabel.Text = editIntervention.Health.ToString();
            }

            if(editIntervention.LastVisit == null)
            {
                DateLastVisitedGroup.Visible = false;
            }
            else
            {
                LastDateLabel.Text = editIntervention.LastVisit.ToString();
            }
        }

        void SetEditControlsForUserRole()
        {
            EnetCareUser user = UserSession.Current.User;

            InterventionStateButtonGroup.Visible = false;

            if (user is IInterventionApprover)
            {
                if (!editIntervention.UserCanChangeState((IInterventionApprover)user))
                {
                    InterventionStateButtonGroup.Visible = true;
                }
            }
            else if (!editIntervention.UserCanChangeQuality(user))
            {
                EditQualityInterventionButton.Visible = false;
            }
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            IInterventionApprover user = (IInterventionApprover)UserSession.Current.User;
            editIntervention.Approve(user);

            DisplayInterventionData();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            IInterventionApprover user = (IInterventionApprover)UserSession.Current.User;
            editIntervention.Cancel(user);

            DisplayInterventionData();
        }

        protected void CompleteButton_Click(object sender, EventArgs e)
        {
            IInterventionApprover user = (IInterventionApprover)UserSession.Current.User;
            editIntervention.Complete(user);

            DisplayInterventionData();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                Intervention_Notes_Textbox.ReadOnly = true;
                SiteEngineer user = (SiteEngineer) UserSession.Current.User;
                editIntervention.UpdateNotes(user, Intervention_Notes_Textbox.Text);
            }
            else
            {
                Intervention_Notes_Textbox.ReadOnly = false;
                EditQualityInterventionButton.Text = "Save Edits";
            }

            LastDateLabel.Text = editIntervention.LastVisit.ToString();
            HealthLabel.Text = editIntervention.Health.ToString();
        }

    }
}