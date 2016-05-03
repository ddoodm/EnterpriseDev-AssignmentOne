<%@ Page Title="Proposed Interventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="ProposedInterventions.aspx.cs" Inherits="ENETCare.IMS.WebApp.ProposedInterventionsWebUI" %>

<asp:Content ID="ProposedInterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Page.Title %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_ProposedInterventions" runat="server" Text="Proposed" OnClick="Button_ProposedInterventions_Click" /></li>
        <li><asp:Button ID="Button_ApprovedInterventions" runat="server" Text="Approved" OnClick="Button_ApprovedInterventions_Click"/></li>
        <li style="float: right;">
            <asp:Button ID="Button_Edit" runat="server" Text="Edit ..." OnClick="Button_Edit_Click"/>
        </li>
    </ul>

    <div class="enetImsTableContainer">
     
        <asp:GridView
            ID="Table_ProposedInterventions"
            AutoGenerateColumns="false"
            AllowSorting="true"
            PageSize="8"
            runat="server"
            CellSpacing="0"
            CssClass="enetImsTable" OnLoad="ProposedTable_Interventions_Load">
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
                <asp:BoundField HeaderText="Date" DataField="Date" dataformatstring="{0:d MMMM, yyyy}" htmlencode="false" />
                <asp:BoundField HeaderText="State" DataField="ApprovalState" />
                <asp:BoundField HeaderText="Health" DataField="Health" />
                <asp:BoundField HeaderText="Notes" DataField="Notes" />
            </Columns>
        </asp:GridView>
    </div>


    <!--<div class="enetImsTableContainer">
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
    -->
</asp:Content>