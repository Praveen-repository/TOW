<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewHistory.ascx.cs"
    Inherits="Web.Admin.Controls.UCViewHistory" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<div id="ctrViewHistory">
    <h1>
        History Detail</h1>
    <div id="ctrViewHistoryView" runat="server">
        <div id="ctrVH" class="frm">
            <div class="row">
                <asp:Label ID="lblVHId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVHId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVHTitle" Text="Title" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVHTitle" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVHType" Text="Type" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVHType" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVHDetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
                <FTB:FreeTextBox ID="ftbVHDetails" runat="Server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVHBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                    ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVH" OnClick="btnEdit_Click" /></div>
        </div>
    </div>
</div>
