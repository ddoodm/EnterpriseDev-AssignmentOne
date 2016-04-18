using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENETCare_IMS_WebApp
{
    public partial class ReportPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Accountants.aspx");
        }

        protected void Button_Print_Click(object sender, EventArgs e)
        {

        }
    }
}