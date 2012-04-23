<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSelectResources.ascx.cs"
    Inherits="Web.Admin.Controls.UCSelectResources" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div class="resources">
    <div class="grid">
        <asp:GridView ID="gvAddResources" runat="server" AutoGenerateColumns="False" AllowSorting="true">
            <AlternatingRowStyle CssClass="altrtd" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id"  SortExpression="Id"/>
                <asp:BoundField DataField="Name" HeaderText="Name"  SortExpression="Name"/>
                <asp:BoundField DataField="Title" HeaderText="Title"  SortExpression="Title"/>
                <asp:CommandField CancelText="" HeaderText="Add" CausesValidation="False" DeleteText=""
                    EditText="" InsertText="" NewText="" SelectText="Add" ShowCancelButton="False"
                    ShowSelectButton="True" UpdateText="" HeaderStyle-CssClass="act" />
            </Columns>
        </asp:GridView>
    </div>
    <UC:CustomPaging ID="cpAddResources" runat="server"></UC:CustomPaging>
    <br />
    <div class="grid">
        <asp:GridView ID="gvShowResources" runat="server" AutoGenerateColumns="False" AllowSorting="true">
            <AlternatingRowStyle CssClass="altrtd" />
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Name" HeaderText="Name"  SortExpression="Name"/>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"/>
                <asp:BoundField DataField="Filename" HeaderText="View" DataFormatString="{0}" />
                <asp:CommandField CancelText="" HeaderText="Remove" CausesValidation="False" DeleteText="Remove"
                    EditText="" InsertText="" NewText="" SelectText="" ShowCancelButton="False" ShowDeleteButton="True"
                    UpdateText="" HeaderStyle-CssClass="act" />
            </Columns>
        </asp:GridView>
    </div>
    <UC:CustomPaging ID="cpShowResources" runat="server"></UC:CustomPaging>
</div>
