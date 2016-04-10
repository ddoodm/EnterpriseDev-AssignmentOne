<%@ Page Title="Interventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Interventions.aspx.cs" Inherits="ENETCare_IMS_WebApp.Interventions" %>
<asp:Content ID="InterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Page.Title %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_CreateNewIntervention" runat="server" Text="Create New ..." /></li>
        <li><asp:Button ID="Button_Clients" runat="server" Text="Clients ..." /></li>
        <li style="float: right;">
            <asp:Button ID="Button_Edit" runat="server" Text="Edit ..." />
        </li>
    </ul>

    <div class="enetImsTableContainer">
        <asp:Table ID="InterventionsTable" runat="server"
            CellSpacing="0"
            CssClass="enetImsTable">
            <asp:TableHeaderRow TableSection="TableHeader">
                <asp:TableHeaderCell>Intervention Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Client</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date Started</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date of Last Visit</asp:TableHeaderCell>
                <asp:TableHeaderCell>Approval</asp:TableHeaderCell>
                <asp:TableHeaderCell>Life Left</asp:TableHeaderCell>
                <asp:TableHeaderCell>Notes</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow>
                <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
                 <asp:TableCell>Test Cell</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
