using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Interventions;

namespace ENETCare.IMS.WebApp
{
    public partial class EditInterventionPage : System.Web.UI.Page
    {
        public const string INTERVENTION_ID_GET_PARAMETER = "id";

        private Intervention intervention;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the ID of the Intervention to be displayed
            string interventionIdString = Request.QueryString[INTERVENTION_ID_GET_PARAMETER];

            int interventionId;
            if (!int.TryParse(interventionIdString, out interventionId))
            {
                // TODO: Handle exceptional 'bad ID' case here
            }
        }
    }
}