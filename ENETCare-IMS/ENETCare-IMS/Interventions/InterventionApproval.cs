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
        /// Identifies the state of the approval
        /// </summary>
        public InterventionApprovalStateWrapper State { get; private set; }

        /// <summary>
        /// The user who approved the Intervention,
        /// null if the Intervention has not been approved.
        /// </summary>
        public User ApprovingUser { get; private set; }

        private Intervention intervention;

        public InterventionApproval(Intervention intervention)
        {
            if (intervention == null)
                throw new ArgumentNullException("An Intervention Approval must be associated with an instantiated Intervention");

            this.intervention = intervention;

            State = new InterventionApprovalStateWrapper();
        }

        public void ChangeState(InterventionApprovalState targetState, SiteEngineer siteEngineer)
        {
            // Check that the Site Engineer is the creator of the Intervention
            if (siteEngineer != intervention.SiteEngineer)
                throw new ArgumentException("Cannot modify an Intervention by a Site Engineer who did not propose the Intervention.");

            // Request to change states. Will throw an exception if current state is invalid.
            State.ChangeState(targetState);

            if (targetState == InterventionApprovalState.Approved)
                ApprovingUser = siteEngineer;
        }

        public void ChangeState(InterventionApprovalState targetState, Manager manager)
        {
            /* Check that the Manager operates in the same District as
               the Intervention's client */
            if (manager.District != intervention.District)
                throw new ArgumentException("Cannot modify an Intervention by a Manager of a different district.");

            // Request to change states. Will throw an exception if current state is invalid.
            State.ChangeState(targetState);

            if (targetState == InterventionApprovalState.Approved)
                ApprovingUser = manager;
        }

        public void Approve(SiteEngineer siteEngineer)
        {
            ChangeState(InterventionApprovalState.Approved, siteEngineer);
        }

        public void Approve(Manager manager)
        {
            ChangeState(InterventionApprovalState.Approved, manager);
        }
    }
}
