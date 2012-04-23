<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="AddUpdateGallery.aspx.cs" Inherits="Web.Admin.AddUpdateGallery" %>
<%@ Register Src="~/Admin/Controls/UCAddUpdateGallery.ascx" TagName="AddUpdateGallery"
    TagPrefix="UC" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <UC:AddUpdateGallery ID="ucAddUpdateGallery" runat="server" />
</asp:Content>
