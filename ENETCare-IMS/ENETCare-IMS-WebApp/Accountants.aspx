﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Accountants.aspx.cs" Inherits="ENETCare_IMS_WebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Accounts</h1>
        <p>&nbsp;&nbsp;&nbsp;
            <asp:Image ID="Image_User" runat="server" Height="84px" Width="104px" />
            <asp:DropDownList ID="Dropdown_User" runat="server" style="z-index: 1; top: 154px; position: absolute; width: 95px; left: 468px; text-align: justify">
            </asp:DropDownList>
        </p>
       
        <ul class="buttonBar">
        <li><asp:Button ID="Button_GenerateReport" runat="server" Text="Generate Report" OnClick="Button_CreateNewIntervention_Click" /></li>
        <li style="float: right;">
            <asp:Button ID="Button_Edit_Accountant" runat="server" Text="Edit ..." />
        </li>
    </ul>
    </div>

    <div class="enetAccTableContainer">
        <asp:Table ID="Table_Accountants" runat="server"
            CellSpacing="0"
            CssClass="enetImsTable" Width="925px">
            <asp:TableHeaderRow TableSection="TableHeader">
                <asp:TableHeaderCell>Account Type</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>District</asp:TableHeaderCell>
                <asp:TableHeaderCell>Cost</asp:TableHeaderCell>                
            </asp:TableHeaderRow>
        </asp:Table>
    </div>

</asp:Content>
