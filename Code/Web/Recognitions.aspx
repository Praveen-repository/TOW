<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/User.Master" AutoEventWireup="true" CodeBehind="Recognitions.aspx.cs" Inherits="Web.Recognitions" %>
<%@ Register Src="~/Controls/UCRecognitions.ascx" TagPrefix="UC" TagName="Recognition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" runat="server">
    <UC:Recognition ID="CntRecognition" runat="server" />
</asp:Content>
