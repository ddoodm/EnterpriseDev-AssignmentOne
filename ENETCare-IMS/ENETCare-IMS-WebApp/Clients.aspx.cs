using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class ClientsList : System.Web.UI.Page
    {
        protected SiteEngineer SiteEngineer { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            SiteEngineer = SiteEngineerSession.Current.User;
        }
    }
}