<%@ Page Title="Create an Intervention" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateInterventionPage.aspx.cs" Inherits="ENETCare.IMS.WebApp.CreateInterventionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LinqDataSource ID="InterventionTypeLinqDataSource" runat="server" OnSelecting="InterventionTypeLinqDataSource_Selecting"></asp:LinqDataSource>

    <h1><%: Page.Title %></h1>

    <div class="formDataContainer">
        <p>Intervention Type</p>
        <asp:DropDownList
            ID="Dropdown_InterventionType"
            runat="server"
            Width="377px"
            DataSourceID="InterventionTypeLinqDataSource"
            DataTextField="Name"
            DataValueField="ID"
            AppendDataBoundItems="true">
            <asp:ListItem Text="Select..." Value="" />
        </asp:DropDownList>
        <br />
        <asp:RequiredFieldValidator
            runat="server"
            ValidationGroup="CreateInterventionGroup"
            ControlToValidate="Dropdown_InterventionType"
            ErrorMessage="Please select an Intervention Type"
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />

        <p>Client</p>
        <asp:TextBox ID="TextBox_ClientNameSearch" runat="server" Width="296px"></asp:TextBox>
        <asp:Button ID="Button_ClientSearch" runat="server" Text="Search" OnClick="Button_ClientSearch_Click" />
        <br />
        <asp:ListBox ID="ListBox_Clients" runat="server" Height="122px" Width="380px"></asp:ListBox>
        <br />
        <asp:RequiredFieldValidator
            ValidationGroup="CreateInterventionGroup"
            ControlToValidate="ListBox_Clients"
            ErrorMessage="Please find and select a client"
            ForeColor="Red"
            runat="server">
        </asp:RequiredFieldValidator>
        <br />

        <p>Notes</p>
        <asp:TextBox ID="TextBox_Notes" TextMode="MultiLine" runat="server" Height="104px" Rows="8" Width="380px"></asp:TextBox>
        <br />

        <asp:Button ID="Button_Cancel"
            runat="server"
            Text="Cancel"
            OnClick="Button_Cancel_Click" />

        <asp:Button ID="Button_Create"
            runat="server"
            Text="Create Intervention"
            ValidationGroup="CreateInterventionGroup"
            OnClick="Button_Create_Click" />
    </div>

</asp:Content>
