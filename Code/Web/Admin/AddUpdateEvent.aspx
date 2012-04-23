<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddUpdateEvent.aspx.cs" Inherits="Web.Admin.AddUpdateEvent" %>

<%@ Register Src="~/Admin/Controls/UCAddUpdateEvent.ascx" TagName="AddUpdateEvent"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdateEvent ID="ucAddUpdateEvent" runat="server" />
</asp:Content>
