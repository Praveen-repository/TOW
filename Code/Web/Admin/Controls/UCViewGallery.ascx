<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewGallery.ascx.cs"
    Inherits="Web.Admin.Controls.UCViewGallery" %>
<%@ Register Src="~/Admin/Controls/UCShowResources.ascx" TagName="ShowResources"
    TagPrefix="UC" %>
<div id="ctrViewGallery">
    <h1>
        Gallery Detail</h1>
    <div id="ctrViewGalleryView" runat="server">
        <div id="ctrVG" class="frm">
            <div class="row">
                <asp:Label ID="lblVGId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVGId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVGName" Text="Name" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVGName" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVGResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:ShowResources ID="showResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVGBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                    ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVG" OnClick="btnEdit_Click" /></div>
        </div>
    </div>
</div>
