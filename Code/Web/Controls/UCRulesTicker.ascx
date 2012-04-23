<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRulesTicker.ascx.cs"
    Inherits="Web.Controls.UCRulesTicker" ViewStateMode="Disabled"  %>
<div class="box">
    <div class="hed">
        Game Rules<div class="icon">
        </div>
    </div>
    <div class="txt">
        <ul id="ruleScroller">
            <asp:Literal ID="RulesList" runat="server" />
        </ul>
    </div>
</div>
<script type="text/javascript">
    (function ($) {
        $(function () {
            $("#ruleScroller").simplyScroll({
                auto: false,
                speed: 10
            });
        });
    })(jQuery);
</script>

