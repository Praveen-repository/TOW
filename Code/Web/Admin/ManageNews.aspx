<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageNews.aspx.cs" Inherits="Web.Admin.ManageNews" %>
<%@ Register Src="~/Admin/Controls/UCManageNews.ascx" TagName="ManageNews" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageNews ID="ucManageNews" runat="server" />
</asp:Content>
