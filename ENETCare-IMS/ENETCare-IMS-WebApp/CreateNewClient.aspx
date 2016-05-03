<%@ Page Title="Create an new Client" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewClient.aspx.cs" Inherits="ENETCare.IMS.WebApp.CreateNewClientWebUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Page.Title %></h1>

    <div class="formDataContainer">

        <p>Client Name</p>
        <asp:TextBox ID="ClientNameText" runat="server" Width="296px"></asp:TextBox>
        <br />
        <p>Client Location</p>
        <asp:TextBox
            ID="ClientLocationText"
            runat="server"
            TextMode="MultiLine"
            Height="122px"
            Width="380px"></asp:TextBox>
        <br />

        <p>District</p>
        <asp:TextBox ID="ClientDistrictText" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" OnClick="Button_Cancel_Click" />
        <asp:Button ID="Button_Create" runat="server" Text="Create Client" OnClick="Button_Create_Click" />
    </div>

</asp:Content>
