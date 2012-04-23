<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="Web.gallery" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Image Gallery</title>
    <link rel="stylesheet" type="text/css" href="/Styles/demo.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/style_Responsive.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/elastislide.css" />
    <link href='http://fonts.googleapis.com/css?family=PT+Sans+Narrow&v1' rel='stylesheet'
        type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Pacifico' rel='stylesheet' type='text/css' />
    <noscript>
        <style>
            .es-carousel ul
            {
                display: block;
            }
        </style>
    </noscript>

    <script id="img-wrapper-tmpl" type="text/x-jquery-tmpl">	
			<div class="rg-image-wrapper">
				{{if itemsCount > 1}}
					<div class="rg-image-nav">
						<a href="#" class="rg-image-nav-prev">Previous Image</a>
						<a href="#" class="rg-image-nav-next">Next Image</a>
					</div>
				{{/if}}
				<div class="rg-image"></div>
				<div class="rg-loading"></div>
				<div class="rg-caption-wrapper">
					<div class="rg-caption" style="display:none;">
						<p></p>
					</div>
				</div>
			</div>
    </script>

</head>
<body>
    <div class="container">
        <div class="content">
            <h1>
                <asp:Literal ID="GalleryName" runat="server" /></h1>
            <div id="rg-gallery" class="rg-gallery">
                <div class="rg-thumbs">
                    <div class="es-carousel-wrapper">
                        <div class="es-nav">
                            <span class="es-nav-prev">Previous</span> <span class="es-nav-next">Next</span>
                        </div>
                        <div class="es-carousel">
                            <ul>
                                <asp:Literal ID="GalleryItem" runat="server" />
                            </ul>
                        </div>
                    </div>
                    <!-- End Elastislide Carousel Thumbnail Viewer -->
                </div>
                <!-- rg-thumbs -->
            </div>
        </div>
        <!-- content -->
    </div>
    <!-- container -->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

    <script type="text/javascript" src="/Scripts/jquery.tmpl.min.js"></script>

    <script type="text/javascript" src="/Scripts/jquery.easing.1.3.js"></script>

    <script type="text/javascript" src="/Scripts/jquery.elastislide.js"></script>

    <script type="text/javascript" src="/Scripts/gallery.js"></script>

</body>
</html>
