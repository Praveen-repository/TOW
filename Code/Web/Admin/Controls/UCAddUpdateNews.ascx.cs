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
    public partial class UCAddUpdateNews : System.Web.UI.UserControl
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
            newsId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.NewsId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewNewstDetails();
                        break;
                    case "edit":
                        LoadNewsDetails(newsId);
                        break;
                }
            }
        }

        protected List<string> ValidateNews(News valTonews)
        {
            List<string> errorMsg = new List<string>(25);
            if (string.IsNullOrEmpty(valTonews.Details))
                errorMsg.Add(Resources.Messages.NewsDetailsMandatory);
            //check event duplicate
            if ((valTonews.Id > 0 && NewsManager.GetAllNews().Exists(x => x.Id != valTonews.Id && x.Heading == valTonews.Heading)) || (!(valTonews.Id > 0) && NewsManager.GetAllNews().Exists(x => x.Heading == valTonews.Heading)))
                errorMsg.Add(Resources.Messages.EventNameDuplidate);

            return errorMsg;
        }

        protected void LoadNewNewstDetails()
        {
            SelectResources.SetSelectedResources(new List<Resource>());
        }

        protected void LoadNewsDetails(int newsId)
        {
            //load gallery details
            var viewNews = NewsManager.GetNewsById(newsId);
            ltrMNAId.Text = viewNews.Id.ToString();
            txtMNAHeading.Text = viewNews.Heading;
            txtMNADated.Text = Helper.FormatDateToMMDDYYYY(viewNews.Dated);
            txtMNALocation.Text = viewNews.Location;
            txtMNASummary.Text = viewNews.Summary;

            ftbMNADetails.Text = viewNews.Details;
            SelectResources.SetSelectedResources(NewsManager.GetNewsResources(newsId));
          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                News tempNews = new News()
                {
                    Id = newsId,
                    Heading = txtMNAHeading.Text,
                    Dated = Helper.GetBritishDate(txtMNADated.Text),
                    Location = txtMNALocation.Text,
                    Summary = txtMNASummary.Text,
                    Details = ftbMNADetails.Text
                };

                var errorList = ValidateNews(tempNews);

                if (errorList.Count == 0)
                {
                    //read from viewstate
                    if (ViewState["selectedResources"] == null)
                        ViewState.Add("selectedResources", new List<Resource>(20));

                    tempNews.Resources = SelectResources.GetSelectedResources();

                    NewsManager.SaveNews(tempNews);

                    Helper.GoToMessagePage(string.Format("News {0} saved successfully.", tempNews.Heading));

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
            Helper.RedirectTo(Resources.PagePath.AdminManageNews, new string[] { "" });
        }
    }
}