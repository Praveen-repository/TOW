using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BO;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCAddUpdateHistory : System.Web.UI.UserControl
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
            historyId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.HistoryId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewHistorytDetails();
                        break;
                    case "edit":
                        LoadHistoryDetails(historyId);
                        break;
                }

            }
        }

        protected List<string> ValidateHistory(History valToHistory)
        {
            List<string> errorMsg = new List<string>(25);

            if (string.IsNullOrEmpty(valToHistory.Details))
                errorMsg.Add(Resources.Messages.HistoryDetailsMandatory);
            //check event duplicate
            if ((valToHistory.Id > 0 && HistoryManager.GetAllHistories().Exists(x => x.Id != valToHistory.Id && x.Title == valToHistory.Title)) || (!(valToHistory.Id > 0) && HistoryManager.GetAllHistories().Exists(x => x.Title == valToHistory.Title)))
                errorMsg.Add(Resources.Messages.HistoryNameDuplicate);

            return errorMsg;
        }

        protected void LoadNewHistorytDetails()
        {

        }

        protected void LoadHistoryDetails(int historyId)
        {
            //load gallery details
            var viewHistory = HistoryManager.GetHistoryById(historyId);
            ltrMHAId.Text = viewHistory.Id.ToString();
            txtMHATitle.Text = viewHistory.Title;
            txtMHAType.Text = viewHistory.Type;
            ftbMHADetails.Text = viewHistory.Details;

            if (ViewState["selectedResources"] == null)
                ViewState.Add("selectedResources", HistoryManager.GetHistoryById(historyId));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                History tempHistory = new History()
                {
                    Id = historyId,
                    Title = txtMHATitle.Text,
                    Details = ftbMHADetails.Text,
                    Type = txtMHAType.Text
                };

                var errorList = ValidateHistory(tempHistory);

                if (errorList.Count == 0)
                {
                    HistoryManager.SaveHistory(tempHistory);
                    Helper.GoToMessagePage(string.Format("Event {0} saved successfully.", tempHistory.Title));
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
            Helper.RedirectTo(Resources.PagePath.AdminManageHistories, new string[] { "" });
        }
    }
}