<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="AddUpdatePages.aspx.cs" Inherits="Web.Admin.AddUpdatePages" ValidateRequest="false"%>

<%@ Register Src="~/Admin/Controls/UCAddUpdatePage.ascx" TagName="AddUpdatePage"
    TagPrefix="uc" %>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdatePage ID="ucAddUpdatePage" runat="server" />
</asp:Content>
