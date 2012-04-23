<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateResource.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateResource" %>
<div id="ctrManageResource">
    <h1>
        Enter Resource Details</h1>
    <div id="ctrManageResourceAdd">
        <div id="ctrMRA" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblMRAId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrMRAId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblMRAResourceTitle" Text="Resource Title" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtMRAResourceTitle" MaxLength="250" ValidationGroup="ctrMRA" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvMRAResourceTitle" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtMRAResourceTitle"
                        ValidationGroup="ctrMRA"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblMRAResources" Text="Upload Resources" CssClass="lblb" runat="server"></asp:Label><input type="file" name="files[]" multiple />
                <asp:FileUpload ID="files" runat="server" CssClass="hide"/>
            </div>
            <div class="row">
                <asp:Label ID="lblMRABank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                    ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrMRA" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="ctrMRA"
                    OnClick="btnCancel_Click" /></div>
        </div>
</div>
