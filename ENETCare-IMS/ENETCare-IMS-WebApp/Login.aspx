<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="ENETCare.IMS.WebApp.Login" %>

<asp:Content ID="LoginContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="enetImsTableContainer" align="center">       
        Log in
    </div>
    <div class="enetImsTableContainer" align="center">  
        Username<asp:TextBox ID="textName" runat="server" align="center"></asp:TextBox>
    </div>
    <div class="enetImsTableContainer" align="center" >  
        Password<asp:TextBox ID="textPass" runat="server" align="center"></asp:TextBox>
    </div>
    <div class="enetImsTableContainer" align="center" >  
        <asp:Button ID="buttonLogin" runat="server" Text="Log in" OnClick="buttonLogin_Click" Height="20px" />
    </div>
    <div class="enetImsTableContainer" align="center" >  
        <asp:Label ID="labelError" runat="server" Text=""></asp:Label>
    </div>
    
</asp:Content>
