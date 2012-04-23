<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCViewEvent.ascx.cs"
    Inherits="Web.Admin.Controls.UCViewEvent" %>
<%@ Register Src="~/Admin/Controls/UCShowResources.ascx" TagName="ShowResources"
    TagPrefix="UC" %>
<div id="ctrViewEvent">
    <h1>
        Event Detail</h1>
    <div id="ctrViewEventView" runat="server">
        <div id="ctrVE" class="frm">
            <div class="row">
                <asp:Label ID="lblVEId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVEName" Text="Name" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEName" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVEYear" Text="Year" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEYear" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVEStartDate" Text="Start Date" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEStartDate" Text="" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <asp:Label ID="lblVEEndDate" Text="End Date" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEEndDate" Text="" runat="server"></asp:Literal>
            </div>
            <div class="row">
                <asp:Label ID="lblVELocation" Text="Location" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVELocation" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVEState" Text="State" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEState" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVEDetails" Text="Details" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrVEDetails" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblVECategories" Text="Categories" CssClass="lblb" runat="server"></asp:Label><div
                    class="categories">
                    <div class="grid stable">
                        <asp:GridView ID="gvVEShowCategories" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Category" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblVEWeights" Text="Weights" CssClass="lblb" runat="server"></asp:Label><div
                    class="weights">
                    <div class="grid stable">
                        <asp:GridView ID="gvVEShowWeights" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Class" HeaderText="Weight Class" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblVEResults" Text="Results" CssClass="lblb" runat="server"></asp:Label>
                <div class="results">
                    <div class="grid">
                        <asp:GridView ID="gvVEShowResults" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                <asp:BoundField DataField="Weight" HeaderText="Weight" />
                                <asp:BoundField DataField="Gold" HeaderText="Gold" />
                                <asp:BoundField DataField="Silver" HeaderText="Silver" />
                                <asp:BoundField DataField="Bronze" HeaderText="Bronze" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblVEResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:ShowResources ID="showResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblVEBank1" Text="" CssClass="lblb" runat="server"></asp:Label><asp:Button
                    ID="btnEdit" runat="server" Text="Edit" ValidationGroup="ctrVE" OnClick="btnEdit_Click" /></div>
        </div>
    </div>
</div>
