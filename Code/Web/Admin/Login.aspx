<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/General.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Web.Admin.Login" %>

<%@ Register Src="~/Controls/UCLogin.ascx" TagName="login" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:login runat="server" ID="ucLogin" />
</asp:Content>
