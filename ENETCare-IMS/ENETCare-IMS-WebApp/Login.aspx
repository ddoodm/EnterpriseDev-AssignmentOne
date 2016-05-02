<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="ENETCare.IMS.WebApp.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginContainer">       

        <asp:Login ID="LoginMain" runat="server" align="center" DestinationPageUrl="~/ProposedInterventions.aspx">
        </asp:Login>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginMain" />

    </div>
</asp:Content>
