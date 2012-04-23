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
    public partial class UCViewHistory : System.Web.UI.UserControl
    {

        string action;
        int historyId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.HistoryId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            historyId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.HistoryId));

            //load Event details
            var viewHistory = HistoryManager.GetHistoryById(historyId);
            ltrVHId.Text = viewHistory.Id.ToString();
            ltrVHTitle.Text = viewHistory.Title;
            ltrVHType.Text = viewHistory.Type;
            ftbVHDetails.Text = viewHistory.Details;

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateHistory,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.HistoryId, historyId) });
        }
    }
}