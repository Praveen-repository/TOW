<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageNews.ascx.cs"
    Inherits="Web.Admin.Controls.UCManageNews" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageNews">
    <h1>
        Manage News</h1>
    <div id="ctrManageNewsAddNew" class="action">
        <asp:Button ID="btnMNAAddNew" Text="Add New News" CssClass="btn" runat="server" OnClick="btnMNAAddNew_Click" />
    </div>
    <div id="ctrManageNewsGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMNNews" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Heading" HeaderText="Heading" SortExpression="Heading"  />
                        <asp:BoundField DataField="Dated" HeaderText="Dated" DataFormatString="{0:dd-MM-yyyy}" SortExpression="Dated" />
                        <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging ID="cpNews" runat="server"></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
