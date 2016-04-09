<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Interventions.aspx.cs" Inherits="ENETCare_IMS_WebApp.Interventions" %>
<asp:Content ID="InterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Table ID="InterventionsTable" runat="server">
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

    <asp:DetailsView ID="InterventionsDetailsView" runat="server" Height="50px" Width="125px">

    </asp:DetailsView>
</asp:Content>
