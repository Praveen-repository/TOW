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
    public partial class UCViewResource : System.Web.UI.UserControl
    {

        string action;
        int resourceId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.ResourceId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            resourceId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.ResourceId));

            //load Event details
            var viewResource = MasterManager.GetResourceById(resourceId);
            ltrVRId.Text = viewResource.Id.ToString();
            ltrVRName.Text = viewResource.Name;
            ltrVRTitle.Text = viewResource.Title;
            ltrVRFilename.Text = viewResource.FileName;
            showResource.ImageUrl = string.Format("{0}/{1}", Resources.PagePath.UploadWebPath, viewResource.FileName.Replace(@"\", "/"));
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateResource,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.ResourceId, resourceId) });
        }
    }
}