<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateGallery.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateGallery" %>
<%@ Register Src="~/Admin/Controls/UCSelectResources.ascx" TagName="SelectResources"
    TagPrefix="UC" %>
<div id="ctrManageGallery">
    <h1>
        Enter Gallery Details</h1>
    <div id="ctrManageGalleryAdd">
        <div id="ctrMGA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMGAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMGAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMGAName" Text="Name" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMGAName" MaxLength="150" ValidationGroup="ctrMGA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMGAName" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMGAName"
                        ValidationGroup="ctrMGA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMGAAResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:SelectResources ID="SelectResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblMGABank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                    ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMGA" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMGA"
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
            $('#ctrMGAHeading').html('Add Event');
            return false;
        }
    </script>
</div>
