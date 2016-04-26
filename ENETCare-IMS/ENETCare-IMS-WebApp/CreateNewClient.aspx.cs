using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENETCare.IMS.Users;

namespace ENETCare.IMS.WebApp
{
    public partial class CreateNewClientWebUI : System.Web.UI.Page
    {
        ENETCareDAO application;
        Districts districts;
        Clients clients;

        protected void Page_Load(object sender, EventArgs e)
        {
            application = UserSession.Current.Application;
            districts = application.Districts;
            clients = application.Clients;

            foreach(District district in districts.GetListCopy())
            {
                ClientDistrict.Items.Add(district.Name);
            }
        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            if(ClientNameText.Text.Trim() == string.Empty)
            {
                throw new ArgumentException("Client must have a name");
            }
            if(ClientLocationText.Text.Trim() == string.Empty)
            {
                throw new ArgumentException("Client must have a name");
            }

            string name = ClientNameText.Text.Trim();
            string location = ClientLocationText.Text.Trim();

            District district = districts.GetDistrictByID(ClientDistrict.SelectedIndex);

            Client client = clients.CreateClient(name, location, district);

            Response.Redirect("Clients.aspx");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clients.aspx");
        }
    }
}