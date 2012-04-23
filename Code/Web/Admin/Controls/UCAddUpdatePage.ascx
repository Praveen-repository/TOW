<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdatePage.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdatePage" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<div id="ctrManagePage">
    <h1>
        Enter Page Details</h1>
    <div id="ctrManagePageAdd">
        <div id="ctrMPA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMPAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMPAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMPAPageName" Text="Page Name" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMPAPageName" MaxLength="250" ValidationGroup="ctrMPA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMPAPageName" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMPAPageName"
                        ValidationGroup="ctrMPA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMPAParent" Text="Parent" CssClass="lblb" runat="server"></asp:Label>
                <asp:DropDownList ID="ddMPAParent" ValidationGroup="ctrMPA" runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvMPAParent" runat="server" CssClass="val" ErrorMessage="*"
                    ControlToValidate="ddMPAParent" ValidationGroup="ctrMPA"></asp:RequiredFieldValidator>
            </div>
            <div class="row">
                <asp:Label ID="lblMPADetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
                <FTB:FreeTextBox ID="ftbDetails" runat="Server" />
            </div>
        <div class="row">
            <asp:Label ID="lblMPAActive" Text="Active" CssClass="lblb" runat="server"></asp:Label>
            <asp:CheckBox ID="chkMPAActive" runat="server" />
        </div>
        <div class="row">
            <asp:Label ID="lblMPABank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMPA" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMPA"
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
        $('#ctrMPAHeading').html('Add Event');
        return false;
    }
</script>
