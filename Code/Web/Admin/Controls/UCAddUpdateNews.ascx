<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateNews.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateNews" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="~/Admin/Controls/UCSelectResources.ascx" TagName="SelectResources"
    TagPrefix="UC" %>
<div id="ctrManageNews">
    <h1>
        Enter News Details</h1>
    <div id="ctrManageNewsAdd">
        <div id="ctrMNA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMNAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMNAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMNAHeading" Text="Heading" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMNAHeading" MaxLength="250" ValidationGroup="ctrMNA" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvMNAHeading" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMNAHeading" ValidationGroup="ctrMNA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMNADated" Text="Dated" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMNADated" ValidationGroup="ctrMNA" CssClass="txt85" MaxLength="12" runat="server"></asp:TextBox>
                <a onclick="showCalendarControl(document.getElementById('<%= txtMNADated.ClientID %>'))"
                    href="#">
                    <img src="/styles/images/calendar.png" alt="Calendar" /></a><asp:RequiredFieldValidator
                        ID="rfvMNADated" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMNADated"
                        ValidationGroup="ctrMNA"></asp:RequiredFieldValidator>
            </div>
            <div class="row"><asp:Label ID="lblMNALocation" Text="Location" CssClass="lblb" runat="server"></asp:Label><asp:TextBox ID="txtMNALocation" MaxLength="150" ValidationGroup="ctrMNA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvMNALocation" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMNALocation"
                    ValidationGroup="ctrMNA"></asp:RequiredFieldValidator></div>
        <div class="row">
            <asp:Label ID="lblMNASummary" Text="Summary" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                ID="txtMNASummary" MaxLength="500" ValidationGroup="ctrMNA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvMNASummary" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMNASummary"
                    ValidationGroup="ctrMNA"></asp:RequiredFieldValidator></div>
        <div class="row">
            <asp:Label ID="lblMNADetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
            <FTB:FreeTextBox ID="ftbMNADetails" runat="Server" />
        </div>
        <div class="row">
            <asp:Label ID="lblMNAAResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                            <UC:SelectResources ID="SelectResources" runat="server" />

        </div>
        <div class="row">
            <asp:Label ID="lblMNABank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMNA" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMNA"
                OnClick="btnCancel_Click" /></div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //  $('#ctrManageEventAdd').hide();
    });

    function addEventClick() {
        //show add event form
        $('#ctrManageEventAdd').show();
        $('#ctrMNAHeading').html('Add Event');
        return false;
    }
</script>
