using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENETCare.IMS.Interventions
{
    public enum InterventionApprovalState
    {
        Proposed, Approved, Cancelled, Completed
    }

    public class InterventionApprovalStateWrapper
    {
        private InterventionApprovalState currentState;

        public void ChangeState(InterventionApprovalState targetState)
        {
            // Check whether the requested state change is permitted
            if (!TryChangeState(targetState))
                throw new InvalidOperationException(
                    String.Format("Cannot change state from {0} to {1}",
                    Enum.GetName(typeof(InterventionApprovalState), currentState),
                    Enum.GetName(typeof(InterventionApprovalState), targetState)));
        }

        public bool TryChangeState(InterventionApprovalState targetState)
        {
            // Permit no change
            if (currentState == targetState)
                return true;

            // Use a State Machine to determine permitted state changes
            switch (currentState)
            {
                case InterventionApprovalState.Proposed:
                    // Cannot complete a proposed intervention
                    if (targetState == InterventionApprovalState.Completed)
                        return false;
                    break;

                case InterventionApprovalState.Approved:
                    // Cannot propose an approved intervention
                    if (targetState == InterventionApprovalState.Proposed)
                        return false;
                    break;

                case InterventionApprovalState.Cancelled:
                case InterventionApprovalState.Completed:
                    // Cannot modify a cancelled or completed intervention
                    return false;
            }

            // Allow change at this point
            currentState = targetState;
            return true;
        }
    }
}
