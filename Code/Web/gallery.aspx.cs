using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using System.Text;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web
{
    public partial class gallery : System.Web.UI.Page
    {
        int galleryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.GalleryId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            galleryId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.GalleryId));
            if (!Page.IsPostBack)
                GalleryItem.Text = ShowGallery(galleryId);
        }


        private string ShowGallery(int galleryId)
        {
            StringBuilder sb = new StringBuilder();

            var gallery = GalleryManager.GetGalleryById(galleryId);
            var galleryRes = GalleryManager.GetGalleryResources(galleryId);
            GalleryName.Text = gallery.Name;
            foreach (Resource resource in galleryRes)
            {
                sb.Append(string.Format(@"<li><a href=""#""><img src=""{0}/{2}"" data-large=""{0}/{1}"" alt=""{3}"" data-description="""" /></a></li>", Resources.PagePath.UploadWebPath, resource.FileName.Replace(@"\", @"/"), resource.FileName.Replace(@"\", @"/th_"), gallery.Name));
            }
            return sb.ToString();
        }

    }
}