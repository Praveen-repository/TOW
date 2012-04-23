<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNewsTicker.ascx.cs"
    Inherits="Web.Controls.UCNewsTicker"  ViewStateMode="Disabled"  %>
<div class="box">
    <div class="hed">
        News<div class="icon">
        </div>
    </div>
    <div class="txt">
        <div class="newsticker-jcarousellite">
            <ul>
                <asp:Literal ID="NewsList" runat="server" />
            </ul>
        </div>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        $(".newsticker-jcarousellite").jCarouselLite({
            vertical: true,
            hoverPause: true,
            visible: 3,
            auto: 500,
            speed: 1000
        });
    });
</script>

