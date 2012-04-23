<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ManageRecognition.aspx.cs" Inherits="Web.Admin.ManageRecognition" %>
<%@ Register Src="~/Admin/Controls/UCManageRecognitions.ascx" TagName="ManageRecognitions" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ManageRecognitions ID="ucManageRecognitions" runat="server" />
</asp:Content>
