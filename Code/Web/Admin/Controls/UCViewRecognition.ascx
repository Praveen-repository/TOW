<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewRecognition.ascx.cs"
    Inherits="Web.Admin.Controls.UCViewRecognition" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Src="~/Admin/Controls/UCShowResources.ascx" TagName="ShowResources"
    TagPrefix="UC" %>
<div id="ctrViewRecognition">
    <h1>
        Recognition Detail</h1>
    <div id="ctrViewRecognitionView" runat="server">
        <div id="ctrVR" class="frm">
            <div class="row">
                <asp:Label ID="lblVRId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVRTitle" Text="Title" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVRTitle" Text="" runat="server"></asp:Literal>
                <FTB:FreeTextBox ID="ftbVRDetails" runat="Server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVRResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:ShowResources ID="showResources" runat="server" />                
            </div>
            <div class="row">
                <asp:Label ID="lblVRBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                    ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVR" OnClick="btnEdit_Click" /></div>
        </div>
    </div>
</div>
