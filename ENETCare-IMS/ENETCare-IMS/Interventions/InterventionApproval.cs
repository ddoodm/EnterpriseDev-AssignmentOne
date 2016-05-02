using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    public class InterventionApproval
    {
        /// <summary>
        /// Internally maintained state of the Approval
        /// </summary>
        private InterventionApprovalStateWrapper state;

        /// <summary>
        /// Describes the current state of the application
        /// </summary>
        public InterventionApprovalState State
        {
            get { return state.CurrentState; }
        }

        /// <summary>
        /// The user who approved the Intervention,
        /// null if the Intervention has not been approved.
        /// </summary>
        public EnetCareUser ApprovingUser { get; private set; }

        private Intervention intervention;

        public InterventionApproval(Intervention intervention)
        {
            if (intervention == null)
                throw new ArgumentNullException("An Intervention Approval must be associated with an instantiated Intervention");

            this.intervention = intervention;

            this.state = new InterventionApprovalStateWrapper();
        }

        public void ChangeState(InterventionApprovalState targetState, EnetCareUser user)
        {
            // Check that the user can change the state of the Intervention
            if (!CanChangeState(user)) 
                throw new ArgumentException("Cannot modify an Intervention by a Site Engineer who did not propose the Intervention.");

            // Request to change states. Will throw an exception if current state is invalid.
            this.state.ChangeState(targetState);
        }

        public void Approve(EnetCareUser user)
        {
            ChangeState(InterventionApprovalState.Approved, user);
            ApprovingUser = user;
        }

        public void Cancel(EnetCareUser user)
        {
            ChangeState(InterventionApprovalState.Cancelled, user);
        }

        public void Complete(EnetCareUser user)
        {
            ChangeState(InterventionApprovalState.Completed, user);
        }

        public bool CanChangeState(EnetCareUser user)
        {
            if (user is Manager)
            {
                Manager manager = (Manager)user;
                return (manager.District == intervention.District);
            }
            else if (user is SiteEngineer)
            {
                SiteEngineer engineer = (SiteEngineer)user;
                return engineer == intervention.SiteEngineer;
            }
            else return false;
        }
    }

    
}
