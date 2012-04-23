<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageHistories.aspx.cs" Inherits="Web.Admin.ManageHistories" %>
<%@ Register Src="~/Admin/Controls/UCManageHistories.ascx" TagName="ManageHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageHistory ID="ucManageHistory" runat="server" />
</asp:Content>
