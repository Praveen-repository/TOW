using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BAL;

namespace Web.Controls
{
    public partial class ShowPage : System.Web.UI.UserControl
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
            pageId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.PageId));

            lblLoadPage.Text = LoadPage(pageId);
        }

        protected string LoadPage(int pageId)
        {
            return PagesManager.GetPageById(pageId).Html;
        }
    }
}