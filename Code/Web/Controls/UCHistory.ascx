<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCHistory.ascx.cs" Inherits="Web.Controls.UCHistory"  ViewStateMode="Disabled" %>
<div class="tabscontainer">
    <div class="tabs">
        <asp:Literal ID="HistoryTabs" runat="server" />
    </div>
    <div class="curvedContainer">
        <asp:Literal ID="HistoryPanes" runat="server" />
    </div>
</div>

<script language="JavaScript">
    $(document).ready(function () {

        $(".tabs .tab[id^=tab_menu]").click(function () {
            var curMenu = $(this);
            $(".tabs .tab[id^=tab_menu]").removeClass("selected");
            curMenu.addClass("selected");

            var index = curMenu.attr("id").split("tab_menu_")[1];
            $(".curvedContainer .tabcontent").css("display", "none");
            $(".curvedContainer #tab_content_" + index).css("display", "block");
        });
    });
</script>

