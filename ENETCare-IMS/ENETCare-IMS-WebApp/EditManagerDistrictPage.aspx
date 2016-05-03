<%@ Page Title="EditManagerDistrictPage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditManagerDistrictPage.aspx.cs" Inherits="ENETCare.IMS.WebApp.EditManagerDistrictPageUI" %>


<asp:Content ID="InterventionsContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit Site Engineer</h1>
    <asp:Label ID ="Manager_Name" runat="server"></asp:Label>
    <p>---oOo---</p>
    <h2><u>Edit District</u><h3>Current District: <asp:Label ID="Current_District" runat="server"></asp:Label></h3></h2>
    <h3>Change To:
    <asp:DropDownList ID="District_Selection_DropDown" runat="server"></asp:DropDownList></h3>
    <asp:Button ID="Button_Change_District" runat="server" Text="Change District" OnClick="Button_Change_District_Click"/>
</asp:Content>
