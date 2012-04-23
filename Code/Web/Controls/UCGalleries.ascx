<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGalleries.ascx.cs"
    Inherits="Web.Controls.UCGalleries" ViewStateMode="Disabled"  %>
<div>
    <asp:Literal ID="GalleriesList" runat="server" />
    <div id="galleryDetails">
        <div id="galleryDialog" title="Tug of War India: Photo Gallery">
            <iframe id="loader" src="" height="850px" width="950px"></iframe>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#galleryDialog").dialog({ autoOpen: false, height: 850, width: 950, modal: true, show: "blind", hide: "fade" });
                //$("#opener").click(function () { $("#galleryDialog").dialog("open"); $("#loader").attr('src', 'test.aspx'); $("#galleryDialog").attr('title', 'working....'); return false; });
            });
        </script>
    </div>
</div>
