<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewPage.ascx.cs"
    Inherits="Web.Admin.Controls.UCViewPage" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<h1>
    Page Detail</h1>
<div id="ctrViewPageView" runat="server">
    <div id="ctrVP" class="frm">
        <div class="row">
            <asp:Label ID="lblVPId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                ID="ltrVPId" Text="" runat="server"></asp:Literal></div>
        <div class="row">
            <asp:Label ID="lblVPPageName" Text="Page name" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                ID="ltrVPPageName" Text="" runat="server"></asp:Literal></div>
        <div class="row">
            <asp:Label ID="lblVPParent" Text="Parent" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                ID="ltrVPParent" Text="" runat="server"></asp:Literal>
        </div>
        <div class="row">
            <asp:Label ID="lblVPActive" Text="Active" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                ID="ltrVPActive" Text="" runat="server"></asp:Literal></div>
        <div class="row">
            <asp:Label ID="lblVPHTML" Text="HTML" CssClass="lblb" runat="server"></asp:Label>
            <FTB:FreeTextBox ID="ftbVPHTML" runat="Server" />
        </div>
        <div class="row">
            <asp:Label ID="lblVPBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVP" OnClick="btnEdit_Click" /></div>
    </div>
</div>
