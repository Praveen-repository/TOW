<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Masters/Admin.Master" AutoEventWireup="true" CodeBehind="AddUpdateRecognition.aspx.cs" Inherits="Web.Admin.AddUpdateRecognition" ValidateRequest="false" %>
<%@ Register Src="~/Admin/Controls/UCAddUpdateRecognition.ascx" TagName="AddUpdateRecognition" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:AddUpdateRecognition ID="ucAddUpdateRecognition" runat="server" />
</asp:Content>
