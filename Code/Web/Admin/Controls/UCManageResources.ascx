<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageResources.ascx.cs"
    Inherits="Web.Admin.Controls.UCManageResources" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageResource">
    <h1>
        Manage Resources</h1>
    <div id="ctrManageResourceAddNew" class="action">
        <asp:Button ID="btnMRAAddNew" Text="Add New Resource" CssClass="btn" runat="server"
            OnClick="btnMRAAddNew_Click" />
    </div>
    <div id="ctrManageResourceGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMRResources" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging Id="cpResources" runat="server" ></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
