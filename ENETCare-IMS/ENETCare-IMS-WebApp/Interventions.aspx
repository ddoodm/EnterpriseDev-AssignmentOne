<%@ Page Title="Interventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Interventions.aspx.cs" Inherits="ENETCare.IMS.WebApp.InterventionsWebUI" %>
<%@ Reference Control="~/Controls/EditTableItemButton.ascx" %>

<asp:Content ID="InterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Interventions for <%: User.Name %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_CreateNewIntervention" runat="server" Text="Create New ..." OnClick="Button_CreateNewIntervention_Click" /></li>
        <li><asp:Button ID="Button_Clients" runat="server" Text="Clients ..." OnClick="Button_Clients_Click" /></li>
    </ul>

    <div class="enetImsTableContainer">
        <asp:LinqDataSource
            ID="InterventionsDataSource"
            OnSelecting="InterventionsDataSource_Selecting"
            runat="server"></asp:LinqDataSource>
        <asp:GridView
            ID="Table_Interventions"
            DataSourceID="InterventionsDataSource"
            AutoGenerateColumns="false"
            AllowSorting="true"
            PageSize="8"
            runat="server"
            CellSpacing="0"
            CssClass="enetImsTable">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink
                            ID="EditLink"
                            runat="server"
                            NavigateUrl='<%# Eval("ID", "~/InterventionsEditPage?id={0}") %>'
                            Text="">
                            <img runat="server" alt="Edit" src="~/Content/EditTableItem.png"/>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Type" DataField="InterventionType.Name" />
                <asp:BoundField HeaderText="Client" DataField="Client.Name" />
                <asp:BoundField HeaderText="Proposer" DataField="SiteEngineer.Name" />
                <asp:BoundField HeaderText="Date" DataField="Date" dataformatstring="{0:d MMMM, yyyy}" htmlencode="false" />
                <asp:BoundField HeaderText="State" DataField="ApprovalState" />
                <asp:BoundField HeaderText="District" DataField="District" />
                <asp:BoundField HeaderText="Health" DataField="Health" NullDisplayText="-" />
                <asp:BoundField HeaderText="Last Visit" DataField="LastVisit" NullDisplayText="-" dataformatstring="{0:d MMMM, yyyy}" htmlencode="false" />
                <asp:BoundField HeaderText="Notes" DataField="Notes" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
