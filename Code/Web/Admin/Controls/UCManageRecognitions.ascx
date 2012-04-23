<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageRecognitions.ascx.cs"
    Inherits="Web.Admin.Controls.UCManageRecognitions" %>
<%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageRecognition">
    <h1>
        Manage Recognitions</h1>
    <div id="ctrManageRecognitionAddNew" class="action">
        <asp:Button ID="btnMRAAddNew" Text="Add New Recognition" CssClass="btn" runat="server"
            OnClick="btnMRAAddNew_Click" />
    </div>
    <div id="ctrManageRecognitionGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMRRecognitions" runat="server" AutoGenerateColumns="False" AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <PagerStyle CssClass="pger" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging ID="cpRecognitions" runat="server"></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
