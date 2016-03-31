using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    class Intervention
    {
        /// <summary>
        /// The ID number (primary key) of this Intervention
        /// </summary>
        private uint id;

        /// <summary>
        /// The type of Intervention to be performed
        /// </summary>
        private InterventionType interventionType;

        /// <summary>
        /// The client for whom the intervention was created
        /// </summary>
        private Client client;

        /// <summary>
        /// The Site Engineer who proposed the Intervention
        /// </summary>
        private SiteEngineer engineer;

        /// <summary>
        /// Describes the state of this intervention
        /// </summary>
        private InterventionApproval approval;

        /// <summary>
        /// Describes the 'progress' and 'health' of the intervention
        /// </summary>
        private InterventionQualityControl qualityControl;

        /// <summary>
        /// The date on which the intervention shall be performed
        /// </summary>
        private DateTime date;

        /// <summary>
        /// The labour required (in hours).
        /// The value is stored as decimal in order to permit fractional values.
        /// </summary>
        private decimal labour;

        /// <summary>
        /// The projected cost of the Intervention.
        /// Default: interventionType.Cost; can be overridden by the Site Engineer
        /// </summary>
        private decimal cost;
    }
}
