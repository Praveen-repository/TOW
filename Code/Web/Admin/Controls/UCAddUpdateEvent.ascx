<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAddUpdateEvent.ascx.cs"
    Inherits="Web.Admin.Controls.UCAddUpdateEvent" %>
<%@ Register Src="~/Admin/Controls/UCSelectResources.ascx" TagName="SelectResources"
    TagPrefix="UC" %>
<div id="ctrAddUpdateEventMainCnt">
    <h1>
        Enter Event Details</h1>
    <div id="ctrAddUpdateEventfrmCnt">
        <div id="ctrAUE" class="frm">
            <asp:Label ID="lblMsg" CssClass="msg" runat="server"></asp:Label>
            <div class="row">
                <asp:Label ID="lblAUEId" Text="Id" CssClass="lblb" runat="server"></asp:Label><asp:Literal
                    ID="ltrAUEId" Text="" runat="server"></asp:Literal></div>
            <div class="row">
                <asp:Label ID="lblAUELevel" Text="Level" CssClass="lblb" runat="server"></asp:Label><asp:DropDownList  ID="ddlAUELevel" runat="server"><asp:ListItem Text="National" Value="National"></asp:ListItem><asp:ListItem Text="International" Value="International"></asp:ListItem><asp:ListItem Text="South Asia" Value="South Asia"></asp:ListItem></asp:DropDownList><asp:RequiredFieldValidator
                        ID="rvfAUELevel" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="ddlAUELevel"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEName" Text="Name" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEName" MaxLength="150" ValidationGroup="ctrAUE" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvAUEName" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEName"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEYear" Text="Year" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEYear" MaxLength="10" ValidationGroup="ctrAUE" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvAUEYear" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEYear"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="revAUEYear" runat="server" ValidationExpression="^[0-9]{4}\-[0-9]{2}$" ValidationGroup="ctrAUE"
                            ControlToValidate="txtAUEYear" CssClass="val" ErrorMessage="Please enter valid year value"></asp:RegularExpressionValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEStartDate" Text="Start Date" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEStartDate" ValidationGroup="ctrAUE" CssClass="txt85" MaxLength="12"
                    runat="server"></asp:TextBox>
                <a onclick="showCalendarControl(document.getElementById('<%= txtAUEStartDate.ClientID %>'))"
                    href="#">
                    <img src="/styles/images/calendar.png" alt="Calendar" /></a><asp:RequiredFieldValidator
                        ID="rfvAUEStartDate" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEStartDate"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator>
            </div>
            <div class="row">
                <asp:Label ID="lblAUEEndDate" Text="End Date" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEEndDate" ValidationGroup="ctrAUE" CssClass="txt85" MaxLength="12" runat="server"></asp:TextBox>
                <a onclick="showCalendarControl(document.getElementById('<%= txtAUEEndDate.ClientID %>'))"
                    href="#">
                    <img src="/styles/images/calendar.png" alt="Calendar" /></a><asp:RequiredFieldValidator
                        ID="rfvAUEEndDate" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEEndDate"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator><asp:CompareValidator ID="cvAUEStartDateEndDate"
                            runat="server" ErrorMessage="Event start date should be less then event end date"
                            ControlToCompare="txtAUEStartDate" ControlToValidate="txtAUEEndDate" ValidationGroup="ctrAUE"
                            Operator="GreaterThan" Type="Date" CssClass="val" CultureInvariantValues="True"></asp:CompareValidator>
            </div>
            <div class="row">
                <asp:Label ID="lblAUELocation" Text="Location" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUELocation" MaxLength="150" ValidationGroup="ctrAUE" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvAUELocation" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUELocation"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEState" Text="State" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEState" MaxLength="150" ValidationGroup="ctrAUE" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvAUEState" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEState"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEACategories" Text="Categories" CssClass="lblb" runat="server"></asp:Label><div
                    class="categories">
                    <div>
                        <asp:TextBox ID="txtAUEACCategory" MaxLength="150" ValidationGroup="ctrAUEAC" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvAUEACCategory" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEACCategory"
                            ValidationGroup="ctrAUEAC"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnAUEAddCategory" Text="Add Category" ValidationGroup="ctrAUEAC"
                            runat="server" OnClick="btnAUEAddCategory_Click" />
                    </div>
                    <div class="grid">
                        <asp:GridView ID="gvAUEShowCategories" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Category" />
                                <asp:CommandField CancelText="" HeaderText="Remove" CausesValidation="False" DeleteText="Remove"
                                    ShowCancelButton="False" ShowDeleteButton="True" HeaderStyle-CssClass="act" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblAUEAWWeights" Text="Weights" CssClass="lblb" runat="server"></asp:Label><div
                    class="weights">
                    <div>
                        <asp:TextBox ID="txtAUEAWWeights" MaxLength="150" ValidationGroup="ctrAUEAW" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                            ID="rfvAUEAWWeights" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEAWWeights"
                            ValidationGroup="ctrAUEAW"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnAUEAddWeight" Text="Add Weight" ValidationGroup="ctrAUEAW" runat="server"
                            OnClick="btnAUEAddWeight_Click" />
                    </div>
                    <div class="grid">
                        <asp:GridView ID="gvAUEAWShowWeights" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Class" HeaderText="Weight Class" />
                                <asp:CommandField CancelText="" HeaderText="Remove" CausesValidation="False" DeleteText="Remove"
                                    EditText="" InsertText="" NewText="" SelectText="" ShowCancelButton="False" ShowDeleteButton="True"
                                    UpdateText="" HeaderStyle-CssClass="act" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblAUEDetails" Text="Details" CssClass="lblb" runat="server"></asp:Label><asp:TextBox
                    ID="txtAUEDetails" MaxLength="1500" TextMode="MultiLine" Rows="10" Columns="20"
                    ValidationGroup="ctrAUE" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="rfvAUEDetails" runat="server" CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEDetails"
                        ValidationGroup="ctrAUE"></asp:RequiredFieldValidator></div>
            <div class="row">
                <asp:Label ID="lblAUEARResults" Text="Results" CssClass="lblb" runat="server"></asp:Label>
                <div class="results">
                    <div class="grid">
                        <table>
                            <tr>
                                <th>
                                    Category
                                </th>
                                <th>
                                    Weight
                                </th>
                                <th>
                                    Gold
                                </th>
                                <th>
                                    Silver
                                </th>
                                <th>
                                    Bronze
                                </th>
                                <th class="act">
                                    Add Result
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtAUEARCategory" MaxLength="150" ValidationGroup="ctrAUEAR" CssClass="txt70"
                                        runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAUEARCategory" runat="server"
                                            CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEARCategory" ValidationGroup="ctrAUEAR"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAUEARWeight" MaxLength="150" ValidationGroup="ctrAUEAR" CssClass="txt70"
                                        runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAUEARWeight" runat="server"
                                            CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEARWeight" ValidationGroup="ctrAUEAR"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAUEARGold" MaxLength="150" ValidationGroup="ctrAUEAR" CssClass="txt70"
                                        runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAUEARGold" runat="server"
                                            CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEARGold" ValidationGroup="ctrAUEAR"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAUEARSilver" MaxLength="150" ValidationGroup="ctrAUEAR" CssClass="txt70"
                                        runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAUEARSilver" runat="server"
                                            CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEARSilver" ValidationGroup="ctrAUEAR"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAUEARBronze" MaxLength="150" ValidationGroup="ctrAUEAR" CssClass="txt70"
                                        runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAUEARBronze" runat="server"
                                            CssClass="val" ErrorMessage="*" ControlToValidate="txtAUEARBronze" ValidationGroup="ctrAUEAR"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:Button ID="btnAUEAddResult" Text="Add Result" ValidationGroup="ctrAUEAR" runat="server"
                                        OnClick="btnAUEAddResult_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="grid">
                        <asp:GridView ID="gvAUEARShowResults" runat="server" AutoGenerateColumns="False">
                            <AlternatingRowStyle CssClass="altrtd" />
                            <Columns>
                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                <asp:BoundField DataField="Weight" HeaderText="Weight" />
                                <asp:BoundField DataField="Gold" HeaderText="Gold" />
                                <asp:BoundField DataField="Silver" HeaderText="Silver" />
                                <asp:BoundField DataField="Bronze" HeaderText="Bronze" />
                                <asp:CommandField CancelText="" HeaderText="Remove" CausesValidation="False" DeleteText="Remove"
                                    ShowCancelButton="False" ShowDeleteButton="True" HeaderStyle-CssClass="act" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lblAUEAResources" Text="Resources" CssClass="lblb" runat="server"></asp:Label>
                <UC:SelectResources ID="SelectResources" runat="server" />
            </div>
            <div class="row">
                <asp:Label ID="lblAUEBank1" Text="" CssClass="lbl" runat="server"></asp:Label><asp:Button
                    ID="btnSave" runat="server" Text="Save" ValidationGroup="ctrAUE" OnClick="btnSave_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" /></div>
        </div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        //  $('#ctrManageEventAdd').hide();
    });

    function addEventClick() {
        //show add event form
        $('#ctrManageEventAdd').show();
        $('#ctrAUEHeading').html('Add Event');
        return false;
    }
</script>
