<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ENETCare.IMS.WebApp.ReportPageUI" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Reports</h1>
    <p>
        <asp:DropDownList ID="ReportsDropDown" runat="server" OnSelectedIndexChanged="Index_Changed"></asp:DropDownList>
    </p>
    <p>
        <asp:DropDownList ID="DistrictsDropDown" runat="server" Visible="false"></asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="GenerateReportButton" runat="server" Text="Generate Report" OnClick="Button_Generate_Report_Click"/>
    </p>
    
    <p>
        <asp:Label ID="ReportTextLabel" runat="server" Width="965px" Height="177px"></asp:Label>
    </p>
    <div>
        <ul class="buttonBar">
            <li style="float: left;">

                <asp:Button ID="Button_Cancel" runat="server" Text="Cancel" OnClick="Button_Cancel_Click"/>
            </li>

            <li style="float: right;">

                <asp:Button ID="Button_Print" runat="server" Text="Print" OnClick="Button_Print_Click" />
            </li>
        </ul>
    </div>
</asp:Content>
