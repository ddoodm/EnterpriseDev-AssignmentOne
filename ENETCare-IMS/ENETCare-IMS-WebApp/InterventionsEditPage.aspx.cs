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
    public partial class InterventionEditPageWebUI : System.Web.UI.Page
    {
        public const string INTERVENTION_ID_GET_PARAMETER = "id";

        private ENETCareDAO application;

        Intervention editIntervention;
        bool isEditing = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain application context
            application = UserSession<SiteEngineer>.Current.Application;

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
            CostLabel.Text = editIntervention.Cost.ToString();

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
            if(editIntervention.ApprovalState == InterventionApprovalState.Proposed)
            {
               CompleteButton.Visible = false;
               ApprovalUserGroup.Visible = false;
            }
            else if (editIntervention.ApprovalState == InterventionApprovalState.Approved)
            {
               ApproveButton.Visible = false;
               ApprovalUserGroup.Visible = true;
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
            //Code goes here to limit Edit/Approval Buttons for particular user
            //PSEUDOCODE:
            /*
            User user = Users.GetUserFromDB(Session[SessionConstants.USER_ID]);
            if (!editIntervention.UserCanChangeState(user))
            {
                InterventionStateButtonGroup.Visible = false;
            }
            else if(!editIntervention.UserCanChangeQuality(user)
            {
                EditQualityInterventionButton.Visible = false;
            }

            */
        }

        protected void ApproveButton_Click(object sender, EventArgs e)
        {
            /* More pseudocode
            //User user = Users.GetUserFromDB(Session[SessionConstants.USER_ID]);
            //editIntervention.Approve(user)
            */

            DisplayInterventionData();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            /* More pseudocode
            //User user = Users.GetUserFromDB(Session[SessionConstants.USER_ID]);
            //editIntervention.Cancel(user)
            */

            DisplayInterventionData();
        }

        protected void CompleteButton_Click(object sender, EventArgs e)
        {
            /* More pseudocode
            //User user = Users.GetUserFromDB(Session[SessionConstants.USER_ID]);
            //editIntervention.Complete(user)
            */

            DisplayInterventionData();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                Intervention_Notes_Textbox.ReadOnly = true;
                /*PSEUDOCODE:
                //User user = Users.GetUserFromDB(Session[SessionConstants.USER_ID]);
                //editIntervention.UpdateNotes(user, Intervention_Notes_Textbox.Text);
                */
            }
            else
            {
                Intervention_Notes_Textbox.ReadOnly = false;
            }

            //TODO: Make Last Date label and Health label text boxes
            //LastDateLabel.ReadOnly = editIntervention.LastVisit.ToString();
            //HealthLabel.Text = editIntervention.Health.ToString();
        }

    }
}