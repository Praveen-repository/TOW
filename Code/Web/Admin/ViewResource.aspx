<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="ViewResource.aspx.cs" Inherits="Web.Admin.ViewResources" %>

<%@ Register Src="~/Admin/Controls/UCViewResource.ascx" TagName="ViewResource" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewResource ID="ucViewResource" runat="server" />
</asp:Content>
