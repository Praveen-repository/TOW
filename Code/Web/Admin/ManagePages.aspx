<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManagePages.aspx.cs" Inherits="Web.Admin.ManagePages" %>
<%@ Register Src="~/Admin/Controls/UCManagePages.ascx" TagName="ManageEvent" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageEvent ID="ucManageEvent" runat="server" />
</asp:Content>
