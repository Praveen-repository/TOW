using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web.Controls
{
    public partial class UCGalleries : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbTable = new StringBuilder();

            int counter = 1;
            //Get Galleries list
            var galleries = GalleryManager.GetAllGalleries();

            //create table here
            sbTable.Append(string.Format(@"<table cellspacing=""1"" id=""galleryList"" class=""tablesorter"">"));
            sbTable.Append(string.Format(@"<thead><tr><th></th><th style=""width:440px"">Name</th></tr></thead>"));
            sbTable.Append(string.Format(@"<tfoot><tr><th></th><th>Name</th></tr></tfoot><tbody>"));

            foreach (Gallery gallery in galleries)
            {
                sbTable.Append(string.Format(@"<tr><td><img src=""{0}"" alt=""{1}"" /></td><td><a id=""gallery_opener_{2}"">{3}</a></td></tr>", GetGalleryThumb(gallery.Id), gallery.Name, gallery.Id, gallery.Name));
            }
            sbTable.Append(string.Format(@"<tbody></table>"));

            sbTable.Append(string.Format(@"<div id=""galleryDetails"">"));
            foreach (Gallery gallery in galleries)
            {
                sbTable.Append(string.Format(@"<div id=""scriptBlock""><script type=""text/javascript"">"));
                sbTable.Append(string.Format(@"$(function(){0}", "{"));
                sbTable.Append(string.Format(@"$(""#gallery_opener_{0}"").click(function(){1} $( ""#galleryDialog"" ).dialog(""open""); $(""#loader"").attr('src', 'gallery.aspx?{3}={4}'); $(""#galleryDialog"").attr('title', 'working.....'); return false; {2});", gallery.Id, "{", "}", Resources.QueryStringKeys.GalleryId, gallery.Id, gallery.Name));
                sbTable.Append(string.Format(@"{0});", "}"));
                sbTable.Append(string.Format(@"</script></div>"));
            }

            sbTable.Append(string.Format(@"</div>"));

            sbTable.Append(string.Format(@"<div id=""pager_galleries"" class=""pager"">"));
            sbTable.Append(string.Format(@"<img src=""/Styles/images/first.png"" class=""first"" alt=""First"" />"));
            sbTable.Append(string.Format(@"<img src=""/Styles/images/prev.png"" class=""prev"" alt=""Previous"" />"));
            sbTable.Append(string.Format(@"<input type=""text"" class=""pagedisplay"" readonly />"));
            sbTable.Append(string.Format(@"<img src=""/Styles/images/next.png"" class=""next"" alt=""Next"" />"));
            sbTable.Append(string.Format(@"<img src=""/Styles/images/last.png"" class=""last"" alt=""Last"" />"));
            sbTable.Append(string.Format(@"<select class=""pagesize"">"));
            sbTable.Append(string.Format(@"<option selected=""selected"" value=""10"">10</option>"));
            sbTable.Append(string.Format(@"<option value=""20"">20</option>"));
            sbTable.Append(string.Format(@"<option value=""30"">30</option>"));
            sbTable.Append(string.Format(@"<option value=""40"">40</option>"));
            sbTable.Append(string.Format(@"</select>"));
            sbTable.Append(string.Format(@"</div>"));

            sbTable.Append(string.Format(@"<script type=""text/javascript"">"));
            sbTable.Append(@"$(function(){{}");
            sbTable.Append(string.Format(@"$(""#galleryList"")"));
            sbTable.Append(string.Format(@".tablesorter({{ widthFixed: true, widgets: ['zebra'] }})"));
            sbTable.Append(string.Format(@".tablesorterPager({{container: $(""#pager_galleries"") }});"));
            sbTable.Append(string.Format(@"{0});", "}"));
            sbTable.Append(string.Format(@"</script>"));

            counter++;

            GalleriesList.Text = sbTable.ToString();
        }


        private string GetGalleryThumb(int galleryId)
        {
            var resource = GalleryManager.GetGalleryResources(galleryId).FirstOrDefault();
            if (resource == null)
                return string.Format("{0}", "/Styles/images/BlankImage.png");
            return string.Format("{0}/{1}", Resources.PagePath.UploadWebPath, resource.FileName.Replace(@"\", @"/th_"));
        }
    }
}