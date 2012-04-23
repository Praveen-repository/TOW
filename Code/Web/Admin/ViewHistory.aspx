<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewHistory.aspx.cs" Inherits="Web.Admin.ViewHistory"  ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/UCViewHistory.ascx" TagName="ViewHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewHistory ID="ucViewHistory" runat="server" />
</asp:Content>
