<%@ Page Title="Edit Intervention" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="InterventionsEditPage.aspx.cs" Inherits="ENETCare.IMS.WebApp.InterventionEditPageWebUI" %>

<asp:Content ID="InterventionEditPage" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: Page.Title %></h1>

    <li><asp:Button ID="Button_Interventions" runat="server" Text="< Back" OnClick="Button_Back_Click" /></li>

    <section id="InterventionSection">

        <div>
            <h3><u>Core Information</u></h3>
        </div>

        <div id="InterventionData">
            <div class="form-group">
                <p>
                    <b>ID:</b>
                    <asp:Label ID="InterventionIDLabel" runat="server"></asp:Label>
                </p>
            </div>
            <div class="form-group">
                <b>Created by:</b>
                <asp:Label ID="SiteEngineerLabel" runat="server"></asp:Label>
            </div>
        </div>

        <div id="CoreInterventionSection">
            <div class="form-group">
                <p>
                    <b>Intervention Type:</b>
                    <asp:Label ID="InterventionTypeLabel" runat="server"></asp:Label>
                </p>
            </div>

            <div class="form-group">
                <p>
                    <b>Client:</b>
                    <asp:Label ID="ClientLabel" runat="server"></asp:Label>
                </p>
            </div>
            <div class="form-group">
                <p>
                    <b>Commencement Date:</b>
                    <asp:Label ID="DateLabel" runat="server"></asp:Label>

                </p>
            </div>
            <div class="form-group">
                <p>
                    <b>Labour Estimate: </b>
                    <asp:Label ID="LabourLabel" runat="server"></asp:Label> hours
                </p>
            </div>
            <div class="form-group">
                <p>
                <b>Cost Estimate:</b>
                 <asp:Label ID="CostLabel" runat="server"></asp:Label>
                </p>
            </div>
        </div>
        <br />
        <div id="AdminInterventionSection">

            <div>
                <u><h3>Admin Information</h3></u>
            </div>

            <div class="form-group">
                <p>
                    <b>State: </b>
                <asp:Label ID="StateLabel" runat="server"></asp:Label>
                <div id="InterventionStateButtonGroup" runat="server">
                    <asp:Button ID="ApproveButton" runat="server" Text="Approve" OnClick="ApproveButton_Click" />
                    <asp:Button ID="CompleteButton" runat="server" Text="Complete" OnClick="CompleteButton_Click" />
                    <asp:Button ID="CancelButton" runat="server" Text="Cancel"  OnClick="CancelButton_Click"/>
                </div>
                </p>
            </div>

            <div class="form-group">
                <p id="ApprovalUserGroup" runat="server">
                    <b>Approving User:</b>
                    <asp:Label ID="ApprovalUserLabel" runat="server"></asp:Label>
                </p>
            </div>

        </div>

        <br />

        <div id="QualityInterventionSection" runat="server">

            <div>
                <u><h3>Quality Information</h3></u>
            </div>

            <asp:Button ID="EditQualityInterventionButton" runat="server" Text="Edit" />
            <asp:Button ID="CancelEditQualityInterventionButton" runat="server" Text="Cancel" />

            <div class="form-group">
                <p><b>Notes</b></p>
                 <asp:TextBox ID="Intervention_Notes_Textbox" TextMode="MultiLine" runat="server" Height="104px" Rows="8" Width="380px"></asp:TextBox>
                 

            </div>

            <div id="HealthGroup" class="form-group" runat="server">
                <p>
                    <b>Health:</b>
                <asp:Label ID="HealthLabel" runat="server"></asp:Label>
                    </p>
            </div>

            <div id="DateLastVisitedGroup" class="form-group" runat="server">
                <p>
                    <b>Date of last visit:</b>
                    <asp:Label ID="LastDateLabel" runat="server"></asp:Label>
                </p>


            </div>

        </div>

    </section>

    <section id="ApprovalSection"></section>
    <section id="QualitySection"></section>


</asp:Content>
