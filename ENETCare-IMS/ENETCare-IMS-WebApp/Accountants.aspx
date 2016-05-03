<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Accountants.aspx.cs" Inherits="ENETCare.IMS.WebApp.AccountantsPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Accounts</h1>
        <p>
            &nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image_User" runat="server" Height="104px" Width="104px" />
            <asp:DropDownList ID="Dropdown_User" runat="server" Style="z-index: 1; top: -4px; position: relative; width: 99px; left: 16px; text-align: justify">
                <asp:ListItem>Account Info</asp:ListItem>
                <asp:ListItem>Change Password</asp:ListItem>
                <asp:ListItem>Logout</asp:ListItem>
            </asp:DropDownList>
        </p>

        <ul class="buttonBar">
            <li>
                <asp:Button ID="Button_Generate" runat="server" Text="Generate A Report" OnClick="Button_Generate_Click"/>

            </li>
        </ul>
    </div>

    <h3>Site Engineers</h3>
    <div class="enetAccTableContainer">
         <asp:LinqDataSource
            ID="SiteEngineersDataSource"
            OnSelecting="SiteEngineersDataSource_Selecting"
            runat="server"></asp:LinqDataSource>
        <asp:GridView
            ID="Table_SiteEngineers"
            DataSourceID="SiteEngineersDataSource"
            AutoGenerateColumns="false"
            AllowSorting="true"
            PageSize="8"
            runat="server"
            CellSpacing="0"
            CssClass="enetImsTable" OnLoad="SiteEngineers_Load">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink
                            ID="EditLink"
                            runat="server"
                            NavigateUrl='<%# Eval("ID", "~/EditSiteEngineerDistrictPage?id={0}") %>'
                            Text="">
                            <img runat="server" alt="Edit" src="~/Content/EditTableItem.png"/>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Type" DataField="Title" />
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="District" DataField="District.Name" />
                <asp:BoundField HeaderText="Cost" DataField="MaxApprovableCost" />
                <asp:BoundField HeaderText="Labour Hours" DataField="MaxApprovableLabour" />
            </Columns>
        </asp:GridView>

    <h3>Managers</h3>
    <div class="enetAccTableContainer">
         <asp:LinqDataSource
            ID="ManagersDataSource"
            OnSelecting="ManagersDataSource_Selecting"
            runat="server"></asp:LinqDataSource>
        <asp:GridView
            ID="Table_Managers"
            DataSourceID="ManagersDataSource"
            AutoGenerateColumns="false"
            AllowSorting="true"
            PageSize="8"
            runat="server"
            CellSpacing="0"
            CssClass="enetImsTable" OnLoad="Managers_Load">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink
                            ID="EditLink"
                            runat="server"
                            NavigateUrl='<%# Eval("ID", "~/EditManagerDistrictPage?id={0}") %>'
                            Text="">
                            <img runat="server" alt="Edit" src="~/Content/EditTableItem.png"/>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Type" DataField="Title" />
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="District" DataField="District.Name" />
                <asp:BoundField HeaderText="Cost" DataField="MaxApprovableCost" />
                <asp:BoundField HeaderText="Labour Hours" DataField="MaxApprovableLabour" />
            </Columns>
        </asp:GridView>

    </div>
</div>

</asp:Content>
