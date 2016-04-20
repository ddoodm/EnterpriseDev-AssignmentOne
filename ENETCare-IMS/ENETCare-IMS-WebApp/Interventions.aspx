<%@ Page Title="Interventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Interventions.aspx.cs" Inherits="ENETCare.IMS.WebApp.InterventionsWebUI" %>
<%@ Reference Control="~/Controls/EditTableItemButton.ascx" %>
<asp:Content ID="InterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Interventions in <%: User.District %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_CreateNewIntervention" runat="server" Text="Create New ..." OnClick="Button_CreateNewIntervention_Click" /></li>
        <li><asp:Button ID="Button_Clients" runat="server" Text="Clients ..." OnClick="Button_Clients_Click" /></li>
    </ul>

    <div class="enetImsTableContainer">
        <asp:Table ID="Table_Interventions" runat="server"
            CellSpacing="0"
            CssClass="enetImsTable">
            <asp:TableHeaderRow TableSection="TableHeader">
                <asp:TableHeaderCell></asp:TableHeaderCell>
                <asp:TableHeaderCell>Intervention Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Client</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date Started</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date of Last Visit</asp:TableHeaderCell>
                <asp:TableHeaderCell>Approval</asp:TableHeaderCell>
                <asp:TableHeaderCell>Life Left</asp:TableHeaderCell>
                <asp:TableHeaderCell>Notes</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
