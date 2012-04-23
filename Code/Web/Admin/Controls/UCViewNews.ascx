<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewNews.ascx.cs" Inherits="Web.Admin.Controls.UCViewNews" %>
<div id="ctrViewNews">
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="~/Admin/Controls/UCShowResources.ascx" TagName="ShowResources" TagPrefix="UC" %>
    <h1>
        News Detail</h1>
    <div id="ctrViewNewsView" runat="server">
        <div id="ctrVN" class="frm">
            <div class="row">
                <asp:Label ID="lblVNId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVNId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVNHeading" Text="Heading" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVNHeading" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVNDated" Text="Dated" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVNDated" Text="" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <asp:Label ID="lblVNLocation" Text="Location" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVNLocation" Text="" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <asp:Label ID="lblVNSummary" Text="Summary" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVNSummary" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVNDetails" Text="Details" CssClass="lblb" runat="server"></asp:Label>
                <ftb:freetextbox id="ftbVNDetails" runat="Server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVNResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:ShowResources ID="showResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVNBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                    ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVN" OnClick="btnEdit_Click" /></div>
        </div>
    </div>
</div>
