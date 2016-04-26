<%@ Page Title="Clients" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="ENETCare.IMS.WebApp.ClientsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Clients in <%: User.District %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_Interventions" runat="server" Text="< Interventions" OnClick="Button_Interventions_Click" /></li>
        <li><asp:Button ID="Button_AddClient" runat="server" Text="Add a New Client" OnClick="Button_AddClient_Click"/></li>
    </ul>

    <div class="enetImsTableContainer">
        <!--
        <asp:Table ID="Table_Clients" runat="server"
            CellSpacing="0"
            CssClass="enetImsTable">
            <asp:TableHeaderRow TableSection="TableHeader">
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Location</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>Foobar Family</asp:TableCell>
                <asp:TableCell>32 Fakelane Rd, Placearea</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Someother Family</asp:TableCell>
                <asp:TableCell>42 Streetname St, Anotherplace</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Bob</asp:TableCell>
                <asp:TableCell>Bob's House</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        -->

        <asp:TreeView ID="TreeView_Clients" runat="server"
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
        <!--
            <Nodes>
                <asp:TreeNode Expanded="True" Text="Stevens Family - 32 Fakelane Rd, Placearea">
                    <asp:TreeNode Text="Hepatitis Avoidance Training" />
                </asp:TreeNode>
                <asp:TreeNode Expanded="True" Text="Rupert von Ochtag Gon - 42 Streetname St, Anotherplace">
                    <asp:TreeNode Text="Supply and Install Portable Toilet" />
                    <asp:TreeNode Text="Supply and Install Storm-proof Home Kit" />
                </asp:TreeNode>
                <asp:TreeNode Expanded="True" Text="Markus Marks - 123 Fake St, Madeupland">
                    <asp:TreeNode Text="Supply and Install Portable Toilet" />
                    <asp:TreeNode Text="Hepatitis Avoidance Training" />
                </asp:TreeNode>
            </Nodes>
            -->
    </div>

</asp:Content>
