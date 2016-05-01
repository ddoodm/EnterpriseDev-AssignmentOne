<%@ Page Title="Clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="ENETCare.IMS.WebApp.ClientsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Clients in <%: User.District %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_Interventions" runat="server" Text="< Interventions" OnClick="Button_Interventions_Click" /></li>
        <li><asp:Button ID="Button_AddClient" runat="server" Text="Add a New Client" OnClick="Button_AddClient_Click"/></li>
    </ul>

    <div style="display:block; padding: 20px;">
        <asp:TreeView
            ID="TreeView_Clients"
            runat="server"
            CssClass="treeView"
            NodeStyle-CssClass="treeNode"
            RootNodeStyle-CssClass="treeRootNode"
            LeafNodeStyle-CssClass="treeLeafNode"
            ShowLines="true">
            <Nodes>
                <asp:TreeNode Expanded="True" Text="Clients">
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
    </div>

</asp:Content>
