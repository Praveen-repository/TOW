<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLogin.ascx.cs" Inherits="Web.Controls.UCLogin" %>

<div id="ctrLogin" class="frm width350">
    <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
    <div class="row">
        <asp:Label ID="lblUsername" Text="Username" CssClass="lbl" runat="server"></asp:Label><asp:TextBox
            ID="txtUsername" MaxLength="150" ValidationGroup="ctrLogin" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                ID="rfvUsername" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtUsername"
                ValidationGroup="ctrLogin"></asp:RequiredFieldValidator></div>
    <div class="row">
        <asp:Label ID="lblPassword" Text="Password" CssClass="lbl" runat="server"></asp:Label><asp:TextBox
            ID="txtPassword" MaxLength="150" TextMode="Password" ValidationGroup="ctrLogin"
            runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvPassword" runat="server" CssClass="val" 
                ErrorMessage="*" ControlToValidate="txtPassword" ValidationGroup="ctrLogin"></asp:RequiredFieldValidator></div>
    <div class="row">
        <asp:Label ID="Label1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button ID="btnLogin" runat="server" Text="Login" ValidationGroup="ctrLogin"
            OnClick="btnLogin_Click" /></div>
</div>

