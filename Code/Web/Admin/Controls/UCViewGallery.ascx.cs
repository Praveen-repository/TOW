using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCViewGallery : System.Web.UI.UserControl
    {

        string action;
        int galleryId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.GalleryId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            galleryId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.GalleryId));


            //load Event details
            var viewGallery = GalleryManager.GetGalleryById(galleryId);
            ltrVGId.Text = viewGallery.Id.ToString();
            ltrVGName.Text = viewGallery.Name;

            showResources.Resources = GalleryManager.GetGalleryResources(galleryId);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateGallery,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.GalleryId, galleryId) });
        }
    }
}