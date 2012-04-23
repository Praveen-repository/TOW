<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewNews.aspx.cs" Inherits="Web.Admin.ViewNews" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/UCViewNews.ascx" TagName="ViewNews" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewNews ID="ucViewNews" runat="server" />
</asp:Content>
