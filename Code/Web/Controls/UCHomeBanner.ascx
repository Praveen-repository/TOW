<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCHomeBanner.ascx.cs"
    Inherits="Web.Controls.UCHomeBanner" ViewStateMode="Disabled"  %>
<div id="banner">
    <div id="container">
        <div id="example">
            <div id="slides">
                <div class="slides_container">
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/_01.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/02.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/03.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/04.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/05.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/06.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/07.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/08.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/09.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/10.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/11.jpg" alt="" title="" />
                        </p>
                    </div>
                    <div class="slide">
                        <p>
                            <img src="/HomeSlide/12.jpg" alt="" title="" />
                        </p>
                    </div>
                </div>
                <a href="#" class="prev">
                    <img src="/Styles/images/arrow-prev.png" width="24" height="43" alt="Arrow Prev" /></a>
                <a href="#" class="next">
                    <img src="/Styles/images/arrow-next.png" width="24" height="43" alt="Arrow Next" /></a>
            </div>
            <img src="/Styles/images/frame01.png" width="580" height="341" alt="" id="frame" />
        </div>
    </div>
</div>
 <script type="text/javascript">
     $(function () {
         // Set starting slide to 1
         var startSlide = 1;
         // Get slide number if it exists
         if (window.location.hash) {
             startSlide = window.location.hash.replace('#', '');
         }
         // Initialize Slides
         $('#slides').slides({
             preload: true,
             preloadImage: '/Styles/images/loading.gif',
             generatePagination: true,
             play: 5000,
             pause: 1000,
             hoverPause: true,
             // Get the starting slide
             start: startSlide,
             animationComplete: function (current) {
                 // Set the slide number as a hash
                 window.location.hash = '#' + current;
             }
         });
     });

    </script>