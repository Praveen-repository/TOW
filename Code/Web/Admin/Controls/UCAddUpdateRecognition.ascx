<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateRecognition.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateRecognition" %>
<%@ Register Src="~/Admin/Controls/UCSelectResources.ascx" TagName="SelectResources"
    TagPrefix="UC" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<div id="ctrManageRecognition">
    <h1>
        Enter Recognition Details</h1>
    <div id="ctrManageRecognitionAdd">
        <div id="ctrMRA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMRAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMRAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMRATitle" Text="Title" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMRATitle" MaxLength="250" ValidationGroup="ctrMRA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMRATitle" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMRATitle"
                        ValidationGroup="ctrMRA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMRADetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
                <FTB:FreeTextBox ID="ftbMRADetails" runat="Server" />
            </div>
            <div class="row">
                <asp:Label ID="lblMRAAResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:SelectResources ID="SelectResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblMRABank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                    ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMRA" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMRA"
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
            $('#ctrMRAHeading').html('Add Event');
            return false;
        }
    </script>
