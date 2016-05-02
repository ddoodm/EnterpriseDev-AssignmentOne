using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ENETCare.IMS.Users;
using ENETCare.IMS.Interventions;

namespace ENETCare.IMS.WebApp
{
    public partial class ClientsList : System.Web.UI.Page
    {
        private ENETCareDAO application;

        private Interventions.Interventions interventions;

        protected new ILocalizedUser User { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            application = ENETCareDAO.Context;
            User = (ILocalizedUser)UserSession.Current.User;

            interventions = application.Interventions;

            SetUpTreeNode();

            TreeView_Clients.ExpandAll();
        }

        void SetUpTreeNode()
        {
            TreeView_Clients.Nodes.Clear();
            TreeView_Clients.Nodes.Add(new TreeNode("Clients"));

            TreeNode rootNode = TreeView_Clients.Nodes.Cast<TreeNode>().ToList().Find(n => n.Text.Equals("Clients"));

            // Filter clients list be the user's district
            Clients clients = application.Clients.FilterByDistrict(User.District);

            foreach (Client client in clients.CopyAsList())
            {
                string nodeText = client.Name + " - " + client.Location;
                TreeNode node = new TreeNode(nodeText);

                foreach (Intervention intervention in interventions.GetInterventionsWithClient(client))
                {
                    node.ChildNodes.Add(new TreeNode(intervention.InterventionType.Name));
                }
                node.Collapse();

                TreeNode existingNode = TreeView_Clients.Nodes.Cast<TreeNode>().ToList().Find(n => n.Text.Equals(nodeText));
                if (existingNode == null)
                {
                    rootNode.ChildNodes.Add(node);
                }
            }
            rootNode.Expand();

        }

        protected void Button_Interventions_Click(object sender, EventArgs e)
        {
            Response.Redirect("Interventions.aspx");
        }

        protected void Button_AddClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateNewClient.aspx");
        }
    }
}