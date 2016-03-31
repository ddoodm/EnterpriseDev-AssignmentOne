using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.Interventions
{
    public class Intervention
    {
        /// <summary>
        /// The ID number (primary key) of this Intervention
        /// </summary>
        private uint id;

        #region Core Information
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
        private SiteEngineer siteEngineer;

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

        #endregion

        #region Administrative Information

        /// <summary>
        /// Describes the state of this intervention
        /// </summary>
        private InterventionApproval approval;

        #endregion

        #region Quality Management Information

        /// <summary>
        /// Describes the 'progress' and 'health' of the intervention
        /// </summary>
        private InterventionQualityControl qualityControl;

        /// <summary>
        /// 
        /// </summary>
        private string notes;

        #endregion

        private Intervention (
                InterventionType interventionType,
                Client client,
                SiteEngineer siteEngineer,
                decimal labour,
                decimal cost,
                DateTime date)
        {
            this.interventionType = interventionType;
            this.client = client;
            this.siteEngineer = siteEngineer;
            this.labour = labour;
            this.cost = cost;
            this.date = date;
        }

        public class Factory
        {
            public static Intervention CreateIntervention (
                InterventionType type,
                Client client,
                SiteEngineer siteEngineer,
                decimal labour,
                decimal cost,
                DateTime date
                )
            {
                return new Intervention(type, client, siteEngineer, labour, cost, date);
            }
        }
    }
}
