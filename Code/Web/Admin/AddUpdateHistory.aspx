<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="AddUpdateHistory.aspx.cs" Inherits="Web.Admin.AddUpdateHistory" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/UCAddUpdateHistory.ascx" TagName="AddUpdateHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdateHistory ID="ucAddUpdateHistory" runat="server" />
</asp:Content>
