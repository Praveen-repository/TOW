<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCShowResources.ascx.cs"
    Inherits="Web.Admin.Controls.UCShowResources" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div class="grid">
    <asp:GridView ID="gvShowResources" runat="server" AutoGenerateColumns="False" AllowSorting="true">
        <AlternatingRowStyle CssClass="altrtd" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Filename" HeaderText="View" DataFormatString="{0}" />
        </Columns>
    </asp:GridView>
</div>
<uc:custompaging id="cpShowResources" runat="server"></uc:custompaging>
