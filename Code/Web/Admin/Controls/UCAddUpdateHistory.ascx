<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateHistory.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateHistory" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<div id="ctrManageHistory">
    <h1>
        Enter History Details</h1>
    <div id="ctrManageHistoryAdd">
        <div id="ctrMHA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMHAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMHAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMHATitle" Text="Title" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMHATitle" MaxLength="150" ValidationGroup="ctrMHA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMHAName" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMHATitle"
                        ValidationGroup="ctrMHA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMHAType" Text="Type" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMHAType" MaxLength="150" ValidationGroup="ctrMHA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMHAType" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMHAType"
                        ValidationGroup="ctrMHA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMHADetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
                <FTB:FreeTextBox ID="ftbMHADetails" runat="Server" />
            </div>
            <div class="row">
                <asp:Label ID="lblMHABlank" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                    ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMHA" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMHA"
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
            $('#ctrMHAHeading').html('Add Event');
            return false;
        }
    </script>

</div>
