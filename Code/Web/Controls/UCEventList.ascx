<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCEventList.ascx.cs"
    Inherits="Web.Controls.UCEventList" ViewStateMode="Disabled" %>
<div class="tabscontainer">
    <div class="tabs">
        <asp:Literal ID="EventTabs" runat="server" />
    </div>
    <div class="curvedContainer">
        <asp:Literal ID="EventPanes" runat="server" />
    </div>
</div>

<script language="JavaScript" type="text/javascript">
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

