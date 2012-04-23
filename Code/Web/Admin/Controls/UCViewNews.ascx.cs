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
    public partial class UCViewNews : System.Web.UI.UserControl
    {

        string action;
        int newsId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.NewsId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            newsId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.NewsId));


            //load Event details
            var viewNews = NewsManager.GetNewsById(newsId);
            ltrVNId.Text = viewNews.Id.ToString();
            ltrVNHeading.Text = viewNews.Heading;
            ltrVNDated.Text = viewNews.Dated.ToString("dd-MM-yyyy");
            ltrVNLocation.Text = viewNews.Location;
            ltrVNSummary.Text = viewNews.Summary;
            ftbVNDetails.Text = viewNews.Details;

            //load event resources
            showResources.Resources = NewsManager.GetNewsResources(newsId);            

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateNews,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.NewsId, newsId) });
        }
    }
}