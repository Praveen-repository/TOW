<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageGalleries.ascx.cs"
    Inherits="Web.Admin.Controls.UCManageGalleries" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageGallery">
    <h1>
        Manage Gallerys</h1>
    <div id="ctrManageGalleryAddNew" class="action">
        <asp:Button ID="btnMGAAddNew" Text="Add New Gallery" CssClass="btn" runat="server"
            OnClick="btnMGAAddNew_Click" />
    </div>
    <div id="ctrManageGalleryGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMGGalleries" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging ID="cpGallery" runat="server"></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
