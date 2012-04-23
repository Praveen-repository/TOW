<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="ManageEvents.aspx.cs" Inherits="Web.Admin.ManageEvents" %>

<%@ Register Src="~/Admin/Controls/UCManageEvents.ascx" TagName="ManageEvent" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageEvent ID="ucManageEvent" runat="server" />
</asp:Content>
