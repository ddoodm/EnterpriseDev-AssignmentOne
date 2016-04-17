using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ENETCare.IMS.WebApp
{


    /*
     * Temporary login stub until database stuff is sorted out. Login details
     * are checked against local credential lists
     * */
    public partial class Login : System.Web.UI.Page
    {
        enum userTypes
        {
            none = -1, siteEngineer, manager, accountant
        }
        int selectedUser;
        List<string> tempUserList;
        List<string> tempPassList;
        List<int> tempUserTypeList;
        protected void Page_Load(object sender, EventArgs e)
        {
            tempUserList = new List<string>();
            tempPassList = new List<string>();
            tempUserTypeList = new List<int>();
            selectedUser = (int)userTypes.none;

            addTempUser("henry", "1234", (int)userTypes.siteEngineer);
            addTempUser("daum", "1234", (int)userTypes.manager);
            addTempUser("yiannis", "1234", (int)userTypes.accountant);
            addTempUser("deinyon", "1234", (int)userTypes.siteEngineer);
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            selectedUser = loginUser(textName.Text, textPass.Text);

            if(selectedUser == (int)userTypes.none)
            {
                labelError.Text = "Invalid login";
            }
            else
            {
                //Change these as you need. The screens that each login type goes to
                switch(selectedUser)
                {
                    case (int)userTypes.siteEngineer: Response.Redirect("Interventions.aspx");  break;
                    case (int)userTypes.manager: Response.Redirect("Clients.aspx"); break;
                    case (int)userTypes.accountant: Response.Redirect("Accountants.aspx"); break;
                }
            }
        }

        private void addTempUser(string name, string pass, int type)
        {
            tempUserList.Add(name);
            tempPassList.Add(pass);
            tempUserTypeList.Add(type);
        }

        private int loginUser(string name, string pass)
        {
            for(int i=0; i<tempUserList.Count;++i)
            {
                if(tempUserList[i] == name && tempPassList[i] == pass)
                {
                    return tempUserTypeList[i];
                }
            }
            return (int)userTypes.none;
        }
    }
}