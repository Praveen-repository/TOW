<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewResource.ascx.cs" Inherits="Web.Admin.Controls.UCViewResource" %>
<div id="ctrViewResource">
    <h1>
        Resource Detail</h1>
    <div id="ctrViewResourceView" runat="server">
        <div id="ctrVR" class="frm">
            <div class="row">
                <asp:Label ID="lblVRId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVRName" Text="Name" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRName" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVRTitle" Text="Title" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRTitle" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVRFilename" Text="File name" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRFilename" Text="" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <asp:Image ID="showResource" runat="server" />
            </div>
        </div>
    </div>
</div>