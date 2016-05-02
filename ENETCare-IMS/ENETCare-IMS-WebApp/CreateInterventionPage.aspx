<%@ Page Title="Create an Intervention" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateInterventionPage.aspx.cs" Inherits="ENETCare.IMS.WebApp.CreateInterventionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:LinqDataSource ID="InterventionTypeLinqDataSource" runat="server" OnSelecting="InterventionTypeLinqDataSource_Selecting"></asp:LinqDataSource>

    <h1><%: Page.Title %></h1>

    <div class="formDataContainer">
        <h3>Intervention Type</h3>
        <asp:DropDownList
            ID="Dropdown_InterventionType"
            runat="server"
            Width="377px"
            DataSourceID="InterventionTypeLinqDataSource"
            DataTextField="Name"
            DataValueField="ID"
            AppendDataBoundItems="true"
            OnSelectedIndexChanged="Dropdown_InterventionType_SelectedIndexChanged"
            AutoPostBack="true">
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

        <h3>Client</h3>
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

        <h3>Date</h3>
        <asp:Calendar ID="Calendar_Date" runat="server" OnLoad="Calendar_Date_Load" />

        <h3>Optional Details</h3>
        <table>
            <tr>
                <td>Cost ($)</td>
                <td><asp:TextBox ID="TextBox_Cost" runat="server"></asp:TextBox></td>
                <td><asp:RegularExpressionValidator 
                    ValidationGroup="CreateInterventionGroup"
                    ControlToValidate="TextBox_Cost"
                    ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                    ErrorMessage="Please enter a valid cost, or leave blank for default"
                    ForeColor="Red"
                    runat="server"/></td>
            </tr>
            <tr>
                <td>Labour (Hours)</td>
                <td><asp:TextBox ID="TextBox_Labour" runat="server"></asp:TextBox></td>
                <td><asp:RegularExpressionValidator 
                    ValidationGroup="CreateInterventionGroup"
                    ControlToValidate="TextBox_Labour"
                    ValidationExpression="^(\d+(\.\d+)?)?$"
                    ErrorMessage="Please enter a valid number of hours, or leave blank for default"
                    ForeColor="Red"
                    runat="server"/></td>
            </tr>
        </table>

        <h3>Notes</h3>
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
