using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BO;
using Web.BAL;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCAddUpdateGallery : System.Web.UI.UserControl
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
            galleryId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.GalleryId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewGallerytDetails();
                        break;
                    case "edit":
                        LoadGalleryDetails(galleryId);
                        break;
                }
                //LoadResources();
            }
        }

      

        protected List<string> ValidateGallery(Gallery valToGallery)
        {
            List<string> errorMsg = new List<string>(25);

            //check event duplicate
            if ((valToGallery.Id > 0 && GalleryManager.GetAllGalleries().Exists(x => x.Id != valToGallery.Id && x.Name == valToGallery.Name)) || (!(valToGallery.Id > 0) && GalleryManager.GetAllGalleries().Exists(x => x.Name == valToGallery.Name)))
                errorMsg.Add(Resources.Messages.EventNameDuplidate);

            return errorMsg;
        }

        protected void LoadNewGallerytDetails()
        {
            SelectResources.SetSelectedResources(new List<Resource>());
        }

        protected void LoadGalleryDetails(int galleryId)
        {

            //load gallery details
            var viewGallery = GalleryManager.GetGalleryById(galleryId);
            ltrMGAId.Text = viewGallery.Id.ToString();
            txtMGAName.Text = viewGallery.Name;

            SelectResources.SetSelectedResources(GalleryManager.GetGalleryResources(galleryId));

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                Gallery tempGallery = new Gallery()
                {
                    Id = galleryId,
                    Name = txtMGAName.Text,
                };

                var errorList = ValidateGallery(tempGallery);

                if (errorList.Count == 0)
                {

                    tempGallery.Resources = SelectResources.GetSelectedResources(); ;

                    GalleryManager.SaveGallery(tempGallery);
                    Helper.GoToMessagePage(string.Format("Gallery {0} saved successfully.", tempGallery.Name));

                }
                else
                {
                    lblMsg.Text = Helper.FormatMessageToUL(errorList);
                }
            }
            else
            {
                lblMsg.Text = Helper.FormatMessageToUL(new List<string>() { Resources.Messages.PageValidationFailed });
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminManageGalleries, new string[] { "" });
        }

    }
}