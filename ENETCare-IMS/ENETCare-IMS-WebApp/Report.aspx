<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ENETCare.IMS.WebApp.ReportPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Reports</h1>
    <p>
        <asp:PlaceHolder ID="DetailedReport" runat="server"></asp:PlaceHolder>
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
