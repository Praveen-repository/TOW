<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="ViewRecognition.aspx.cs" Inherits="Web.Admin.ViewRecognition" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/UCViewRecognition.ascx" TagName="ViewRecognition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:ViewRecognition ID="ucViewRecognition" runat="server" />
</asp:Content>
