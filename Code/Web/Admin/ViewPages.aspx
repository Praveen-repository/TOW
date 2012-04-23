<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewPages.aspx.cs" Inherits="Web.Admin.ViewPages" ValidateRequest="false"%>
<%@ Register Src="~/Admin/Controls/UCViewPage.ascx" TagName="ViewPage" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewPage ID="ucViewPage" runat="server" />
</asp:Content>
