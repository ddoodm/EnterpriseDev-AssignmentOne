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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Districts.IsPopulated())
            {
                Districts.PopulateDistricts();
            }

            foreach(District district in Districts.DistrictList)
            {
                ClientDistrict.Items.Add(district.Name);
            }

        }

        protected void Button_Create_Click(object sender, EventArgs e)
        {
            

            int ID = Clients.GetNewClientID();

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

            District district = Districts.GetDistrictByID(ClientDistrict.SelectedIndex);

            Client client = new Client(ID, name, location, district);

            //TODO:
            Clients.AddClient(client);

            Response.Redirect("Clients.aspx");
        }

        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clients.aspx");
        }
    }
}