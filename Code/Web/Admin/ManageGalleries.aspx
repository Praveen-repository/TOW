<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageGalleries.aspx.cs" Inherits="Web.Admin.ManageGalleries" %>
<%@ Register Src="~/Admin/Controls/UCManageGalleries.ascx" TagName="ManageGalleries" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageGalleries ID="ucManageManageGalleries" runat="server" />
</asp:Content>
