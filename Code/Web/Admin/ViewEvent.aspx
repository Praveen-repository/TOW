<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="ViewEvent.aspx.cs" Inherits="Web.Admin.ViewEvent" %>

<%@ Register Src="~/Admin/Controls/UCViewEvent.ascx" TagName="ViewEvent" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewEvent ID="ucViewEvent" runat="server" />
</asp:Content>
