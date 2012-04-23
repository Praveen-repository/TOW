<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManagePages.ascx.cs"
    Inherits="Web.Admin.Controls.UCManagePages" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManagePages">
    <h1>
        Manage Pages</h1>
    <div id="ctrManagePagesAddNew" class="action">
        <asp:Button ID="btnMNAAddPage" Text="Add New News" CssClass="btn" 
            runat="server" onclick="btnMNAAddPage_Click" />
    </div>
    <div id="ctrManagePagesGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMNPages" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="PageName" HeaderText="Page Name" SortExpression="PageName" />
                        <asp:BoundField DataField="ParentHierarchy" HeaderText="Parents" SortExpression="Parents" />
                         <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging ID="cpPages" runat="server"></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
