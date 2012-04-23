<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/User.Master" AutoEventWireup="true" CodeBehind="Histories.aspx.cs" Inherits="Web.Histories" %>
<%@ Register Src="~/Controls/UCHistory.ascx" TagPrefix="UC" TagName="History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" runat="server">
    <UC:History ID="CntHistory" runat="server" />
</asp:Content>
