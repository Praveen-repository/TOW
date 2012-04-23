<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageHistories.ascx.cs" Inherits="Web.Admin.Controls.UCManageHistory" %>
    <%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageHistory">
    <h1>Manage History</h1>
    <div id="ctrManageHistoryAddNew" class="action">
        <asp:Button ID="btnMGAAddNew" Text="Add New History" CssClass="btn" runat="server"
            OnClick="btnMGAAddNew_Click" />
    </div>
    <div id="ctrManageHistoryGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMEHistories" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging Id="cpHistory" runat="server" ></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>