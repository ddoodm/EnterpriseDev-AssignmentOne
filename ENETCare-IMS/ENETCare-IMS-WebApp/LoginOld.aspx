<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="LoginOld.aspx.cs" Inherits="ENETCare.IMS.WebApp.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginContainer">       
        Log in <br />
        Username<asp:TextBox ID="textName" runat="server" align="center"></asp:TextBox> <br />
        Password<asp:TextBox ID="textPass" TextMode="Password" runat="server" align="center"></asp:TextBox> <br />
        <asp:Button ID="buttonLogin" runat="server" Text="Log in" OnClick="buttonLogin_Click" Height="20px" />
        <asp:Label ID="labelError" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
