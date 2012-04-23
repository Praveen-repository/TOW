<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageResources.aspx.cs" Inherits="Web.Admin.ManageResources" %>
<%@ Register Src="~/Admin/Controls/UCManageResources.ascx" TagName="ManageResources" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageResources ID="ucManageResources" runat="server" />
</asp:Content>
