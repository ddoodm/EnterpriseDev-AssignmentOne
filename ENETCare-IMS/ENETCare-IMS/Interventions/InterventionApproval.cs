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
        public User ApprovingUser { get; private set; }

        private Intervention intervention;

        public InterventionApproval(Intervention intervention)
        {
            if (intervention == null)
                throw new ArgumentNullException("An Intervention Approval must be associated with an instantiated Intervention");

            this.intervention = intervention;

            this.state = new InterventionApprovalStateWrapper();
        }

        /*public void ChangeState(InterventionApprovalState targetState, SiteEngineer siteEngineer)
        {
            // Check that the Site Engineer is the creator of the Intervention
            if(!CanChangeState(siteEngineer))   //if (siteEngineer != intervention.SiteEngineer)
                throw new ArgumentException("Cannot modify an Intervention by a Site Engineer who did not propose the Intervention.");

            // Request to change states. Will throw an exception if current state is invalid.
            this.state.ChangeState(targetState);
        }
        */
        //public void ChangeState(InterventionApprovalState targetState, Manager manager)
        //{
        //    /* Check that the Manager operates in the same District as
        //       the Intervention's client */
        //    if(!CanChangeState(manager))     // if(manager.District != intervention.District)
        //        throw new ArgumentException("Cannot modify an Intervention by a Manager of a different district.");

        //    // Request to change states. Will throw an exception if current state is invalid.
        //    this.state.ChangeState(targetState);
        //}

        public void ChangeState(InterventionApprovalState targetState, User user)
        {
            // Check that the user can change the state of the Intervention
            if (!CanChangeState(user)) 
                throw new ArgumentException("Cannot modify an Intervention by a Site Engineer who did not propose the Intervention.");

            // Request to change states. Will throw an exception if current state is invalid.
            this.state.ChangeState(targetState);
        }


        public void Approve(User user)
        {
            ChangeState(InterventionApprovalState.Approved, user);
            ApprovingUser = user;
        }

        public void Cancel(User user)
        {
            ChangeState(InterventionApprovalState.Cancelled, user);
        }

        public void Complete(User user)
        {
            ChangeState(InterventionApprovalState.Completed, user);
        }

        /*
        public void Approve(SiteEngineer siteEngineer)
        {
            ChangeState(InterventionApprovalState.Approved, siteEngineer);
            ApprovingUser = siteEngineer;
        }

        public void Approve(Manager manager)
        {
            ChangeState(InterventionApprovalState.Approved, manager);
            ApprovingUser = manager;
        }
        */

        /*
        public void Cancel(SiteEngineer siteEngineer)
        {
            ChangeState(InterventionApprovalState.Cancelled, siteEngineer);
        }

        public void Cancel(Manager manager)
        {
            ChangeState(InterventionApprovalState.Cancelled, manager);
        }
        

        public void Complete(SiteEngineer siteEngineer)
        {
            ChangeState(InterventionApprovalState.Completed, siteEngineer);
        }

        public void Complete(Manager manager)
        {
            ChangeState(InterventionApprovalState.Completed, manager);
        }
        */

        public bool CanChangeState(User user)
        {
            if(user is Manager)
            {
                Manager manager = (Manager)user;
                return (manager.District == intervention.District);
               
            }
            else if(user is SiteEngineer)
            {
                SiteEngineer engineer = (SiteEngineer)user;
                return engineer == intervention.SiteEngineer;
              
            }
            else
            {
                return false;
            }
        }
    }

    
}
