<%@ Page Title="Proposed Interventions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="ProposedInterventions.aspx.cs" Inherits="ENETCare.IMS.WebApp.ProposedInterventionsWebUI" %>

<asp:Content ID="ProposedInterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Page.Title %></h1>

    <ul class="buttonBar">
        <li><asp:Button ID="Button_ProposedInterventions" runat="server" Text="Proposed" OnClick="Button_ProposedInterventions_Click" /></li>
        <li><asp:Button ID="Button_ApprovedInterventions" runat="server" Text="Approved" OnClick="Button_ApprovedInterventions_Click"/></li>
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

</asp:Content>