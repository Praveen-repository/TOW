<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewGallery.aspx.cs" Inherits="Web.Admin.ViewGallery" %>
<%@ Register Src="~/Admin/Controls/UCViewGallery.ascx" TagName="ViewGallery" TagPrefix="UC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <UC:ViewGallery ID="ucViewGallery" runat="server" />
</asp:Content>
