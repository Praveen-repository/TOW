<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/User.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Web.Events" %>
<%@ Register Src="~/Controls/UCEventList.ascx" TagPrefix="UC" TagName="EventList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageBody" runat="server">
    <UC:EventList ID="CntEventList" runat="server" />
</asp:Content>
