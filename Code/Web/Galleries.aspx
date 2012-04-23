<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/User.Master" AutoEventWireup="true" CodeBehind="Galleries.aspx.cs" Inherits="Web.Galleries" %>
<%@ Register Src="~/Controls/UCGalleries.ascx" TagPrefix="UC" TagName="GalleryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" runat="server">
    <UC:GalleryList ID="CntGalleryList" runat="server" />
</asp:Content>
