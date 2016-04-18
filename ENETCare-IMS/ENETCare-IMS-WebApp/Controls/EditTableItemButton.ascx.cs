using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENETCare.IMS.WebApp.Controls
{
    public partial class EditTableItemButton : System.Web.UI.UserControl
    {
        public const string CONTROL_TEMPLATE_PATH = "Controls/EditTableItemButton.ascx";

        private string targetURL;
        public string TargetURL
        {
            get { return targetURL; }
            set
            {
                targetURL = value;
                EditTableItemLink.NavigateUrl = value;
            }
        }

        // Default constructor
        public EditTableItemButton () : base() { }

        // Constructor used by LoadControl
        public EditTableItemButton (string TargetURL) : base()
        {
            this.TargetURL = TargetURL;
        }

        /// <summary>
        /// Initializes an EditTableItemButton Control on the page
        /// </summary>
        public static EditTableItemButton InstantiateControl(System.Web.UI.Page page, string TargetURL)
        {
            EditTableItemButton control =
                (EditTableItemButton)page.LoadControl(CONTROL_TEMPLATE_PATH);
            control.TargetURL = TargetURL;
            return control;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}