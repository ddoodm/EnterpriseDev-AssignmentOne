<%@ Page Title="Create an Intervention" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateInterventionPage.aspx.cs" Inherits="ENETCare.IMS.WebApp.CreateInterventionPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Page.Title %></h1>

    <div class="formDataContainer">
        <p>Intervention Type</p>
        <asp:DropDownList ID="Dropdown_InterventionType" runat="server" Height="16px" Width="377px"></asp:DropDownList>
        <br />

        <p>Client</p>
        <asp:TextBox ID="TextBox_ClientNameSearch" runat="server" Width="296px"></asp:TextBox>
        <asp:Button ID="Button_ClientSearch" runat="server" Text="Search" />
        <br />
        <asp:ListBox ID="ListBox_Clients" runat="server" Height="122px" Width="380px"></asp:ListBox>
        <br />

        <p>Notes</p>
        <asp:TextBox ID="TextBox_Notes" TextMode="MultiLine" runat="server" Height="104px" Rows="8" Width="380px"></asp:TextBox>
        <br />

        <asp:Button ID="Button_Create" runat="server" Text="Create Intervention" />
        <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" />
    </div>

</asp:Content>
