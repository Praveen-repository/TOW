<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCManageEvents.ascx.cs"
    Inherits="Web.Admin.Controls.UCManageEvents" %>
    <%@ Register Src="~/Controls/UCCustomPaging.ascx" TagName="CustomPaging" TagPrefix="UC" %>
<div id="ctrManageEvent">
    <h1>
        Manage Events</h1>
    <div id="ctrManageEventAddNew" class="action">
        <asp:Button ID="btnMEAAddNew" Text="Add New Event" CssClass="btn" runat="server"
            OnClick="btnMEAAddNew_Click" />
    </div>
    <div id="ctrManageEventGrid" class="frm">
        <div class="row">
            <div class="grid">
                <asp:GridView ID="gvMEEvents" runat="server" AutoGenerateColumns="False"
                    AllowSorting="True">
                    <AlternatingRowStyle CssClass="altrtd" />
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd-MM-yyyy}"
                            SortExpression="StartDate" />
                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd-MM-yyyy}"
                            SortExpression="EndDate" />
                        <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                        <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                        <asp:CommandField SelectText="View" HeaderText="View" ShowSelectButton="true" ItemStyle-CssClass="actGrd"
                            HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField EditText="Edit" HeaderText="Edit" ButtonType="Link" ShowEditButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                        <asp:CommandField DeleteText="Delete" HeaderText="Delete" ButtonType="Link" ShowDeleteButton="true"
                            ItemStyle-CssClass="actGrd" HeaderStyle-CssClass="actSmall" />
                    </Columns>
                </asp:GridView>
                <UC:CustomPaging Id="cpEvents" runat="server" ></UC:CustomPaging>
            </div>
        </div>
    </div>
</div>
