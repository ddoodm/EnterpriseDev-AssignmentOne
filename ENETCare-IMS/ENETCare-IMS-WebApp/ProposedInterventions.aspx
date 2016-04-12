<%@ Page Title="ProposedInterventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="ProposedInterventions.aspx.cs" Inherits="ENETCare.IMS.WebApp.ProposedInterventionsWebUI" %>

<asp:Content ID="ProposedInterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Page.Title %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_ProposedInterventions" runat="server" Text="Proposed" OnClick="Button_ProposedInterventions_Click" /></li>
        <li><asp:Button ID="Button_ApprovedInterventions" runat="server" Text="Approved" OnClick="Button_ApprovedInterventions_Click"/></li>
        <li style="float: right;">
            <asp:Button ID="Button_Edit" runat="server" Text="Edit ..." />
        </li>
    </ul>

    <div class="enetImsTableContainer">
        <asp:Table ID="Table_In_Proposed_Interventions" runat="server"
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
        </asp:Table>
    </div>
</asp:Content>