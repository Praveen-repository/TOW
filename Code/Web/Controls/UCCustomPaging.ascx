<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCustomPaging.ascx.cs"
    Inherits="Web.Controls.UCShowPaging" ViewStateMode="Disabled"  %>
<div class="Pager">
    <div id="left" style="float: left;">
        <span>Show Page </span>
        <asp:DropDownList ID="ddlPageNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageNumber_SelectedIndexChanged">
        </asp:DropDownList>
        <span>of</span>
        <asp:Label ID="lblShowRecords" runat="server"></asp:Label>
        <span>Pages </span>
        <div class="trec">Total Records: <asp:Label ID="lblTotalRecords" runat="server" Text="0"></asp:Label></div>
    </div>
    
    <div id="right" style="float: right;">
        <span>Display </span>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="5" Value="5"></asp:ListItem>
            <asp:ListItem Text="10" Value="10" Selected="true"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
            <asp:ListItem Text="25" Value="25"></asp:ListItem>
            <asp:ListItem Text="50" Value="50"></asp:ListItem>
        </asp:DropDownList>
        <span>Records per Page</span>
    </div>
</div>
