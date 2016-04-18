using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ENETCare.IMS.Interventions;

namespace ENETCare.IMS
{
    /// <summary>
    /// Provides Data Access for ENETCare data sources
    /// </summary>
    public class ENETCareDAO
    {
        public Interventions.Interventions Interventions { get; private set; }
        public InterventionTypes InterventionTypes { get; private set; }
        public Districts Districts { get; private set; }
        public Clients Clients { get; private set; }

        public ENETCareDAO()
        {
            Districts = new Districts(this);
            Clients = new Clients(this);
            InterventionTypes = new InterventionTypes();
            Interventions = new Interventions.Interventions(this);
        }
    }
}
