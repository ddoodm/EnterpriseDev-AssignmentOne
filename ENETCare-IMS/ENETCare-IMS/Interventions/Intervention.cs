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
        #region Core Information
        /// <summary>
        /// The type of Intervention to be performed
        /// </summary>
        public InterventionType InterventionType { get; private set; }

        /// <summary>
        /// The client for whom the intervention was created
        /// </summary>
        public Client Client { get; private set; }

        /// <summary>
        /// The Site Engineer who proposed the Intervention
        /// </summary>
        public SiteEngineer SiteEngineer { get; private set; }

        /// <summary>
        /// The date on which the intervention shall be performed
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The labour required (in hours).
        /// The value is stored as decimal in order to permit fractional values.
        /// </summary>
        public decimal Labour { get; private set; }

        /// <summary>
        /// The projected cost of the Intervention.
        /// Default: interventionType.Cost; can be overridden by the Site Engineer
        /// </summary>
        public decimal Cost { get; private set; }

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
        private InterventionQualityManagement qualityControl;

        /// <summary>
        /// 
        /// </summary>
        private string notes;

        #endregion

        public District District
        {
            get { return Client.District; }
        }

        public void Approve(SiteEngineer engineer)
        {
            approval.Approve(engineer);
        }

        public void Approve(Manager manager)
        {
            approval.Approve(manager);
        }

        private Intervention (
                InterventionType interventionType,
                Client client,
                SiteEngineer siteEngineer,
                decimal labour,
                decimal cost,
                DateTime date)
        {
            this.InterventionType = interventionType;
            this.Client = client;
            this.SiteEngineer = siteEngineer;
            this.Labour = labour;
            this.Cost = cost;
            this.Date = date;

            // Initialize the Approval
            approval = new InterventionApproval(this);
        }

        public class Factory
        {
            /// <summary>
            /// Instantiates an Intervention given all optional data
            /// </summary>
            /// <param name="type">The type of the intervention</param>
            /// <param name="client">The client associated with the intervention</param>
            /// <param name="siteEngineer">The staff proposing the intervention</param>
            /// <param name="labour">The required labour (in hours) - overrides 'type'</param>
            /// <param name="cost">The required cost (in AUD) - overrides 'type'</param>
            /// <param name="date">The date of the intervention - overrides the present date</param>
            /// <returns>A new Intervention</returns>
            public static Intervention CreateIntervention (
                InterventionType type,
                Client client,
                SiteEngineer siteEngineer,
                decimal labour,
                decimal cost,
                DateTime date
                )
            {
                // The Client must exist in the same district as the Engineer.
                // The User Interface should disallow this operation.
                if (client.District != siteEngineer.District)
                    throw new ArgumentException("Cannot create Intervention.\nThe Client must exist in the same district as the Site Engineer.");

                return new Intervention(type, client, siteEngineer, labour, cost, date);
            }

            /// <summary>
            /// Instantiates an Intervention given no additional data
            /// </summary>
            /// <param name="type">The type of Intervention to create</param>
            /// <param name="client">The client associated with the Intervention</param>
            /// <param name="siteEngineer">The engineer proposing the Intervention</param>
            /// <returns></returns>
            public static Intervention CreateIntervention (
                InterventionType type,
                Client client,
                SiteEngineer siteEngineer
                )
            {
                // Use default values from the specified Intervention Type
                decimal labour = type.Labour;
                decimal cost = type.Cost;

                // Use the present date
                DateTime date = DateTime.Now;

                return CreateIntervention(type, client, siteEngineer, labour, cost, date);
            }
        }
    }
}
