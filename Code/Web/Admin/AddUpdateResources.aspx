<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="AddUpdateResources.aspx.cs" Inherits="Web.Admin.AddUpdateResources" %>
<%@ Register Src="~/Admin/Controls/UCAddUpdateResource.ascx" TagName="AddUpdateResource" TagPrefix="UC"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <UC:AddUpdateResource ID="ucAddUpdateResource" runat="server" />
</asp:Content>
