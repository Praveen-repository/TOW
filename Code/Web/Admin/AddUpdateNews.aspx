<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddUpdateNews.aspx.cs" Inherits="Web.Admin.AddUpdateNews" ValidateRequest="false" %>

<%@ Register Src="~/Admin/Controls/UCAddUpdateNews.ascx" TagName="AddUpdateNews"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdateNews ID="ucAddUpdateNews" runat="server" />
</asp:Content>
