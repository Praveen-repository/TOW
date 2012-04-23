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
    public partial class UCViewPage : System.Web.UI.UserControl
    {
        string action;
        int pageId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.PageId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            pageId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.PageId));

            //load Event details
            var viewPage = PagesManager.GetPageById(pageId);
            ltrVPId.Text = viewPage.Id.ToString();
            ltrVPPageName.Text = viewPage.PageName;
            ltrVPParent.Text = PagesManager.PageHierarchy(pageId);
            ltrVPActive.Text = (viewPage.Active)? "Active":"Inactive" ;
            ftbVPHTML.Text = viewPage.Html;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdatePages,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.PageId, pageId) });
        }
    }
}