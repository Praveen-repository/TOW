using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web.Admin.Controls
{
    public partial class UCManageNews : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            gvMNNews.DataKeyNames = new string[] { "Id" };
            gvMNNews.RowEditing += new GridViewEditEventHandler(gvMNNews_RowEditing);
            gvMNNews.RowDeleting += new GridViewDeleteEventHandler(gvMNNews_RowDeleting);
            gvMNNews.SelectedIndexChanging += new GridViewSelectEventHandler(gvMNNews_SelectedIndexChanging);
            gvMNNews.RowDataBound += new GridViewRowEventHandler(gvMNNews_RowDataBound);
            gvMNNews.Sorting += new GridViewSortEventHandler(gvMNNews_Sorting);

            cpNews.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpNews_PageChanged);
            if (!Page.IsPostBack)
            {
                if (ViewState["newsPageIndex"] == null)
                    ViewState.Add("newsPageIndex", 0);

                LoadNews("Id", 0, false);
            }
            
        }

        void gvMNNews_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadNews(e.SortExpression, 0, true);
            cpNews.ResetPageNumber();
        }

        void cpNews_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["newsPageIndex"] = e.CurrentPageNumber;

            gvMNNews.PageIndex = e.CurrentPageNumber;

            LoadNews(ViewState["newsSortBy"].ToString(), e.CurrentPageNumber, false);
        }

        void gvMNNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

                // Get information about the to the row
                var newsHeading = ((Web.Code.BO.News)e.Row.DataItem).Heading;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the News?/n {0} ');", newsHeading.Replace("'", @"\'"));
            }
        }

        void gvMNNews_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewNews,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.NewsId,int.Parse(gvMNNews.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMNNews_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            NewsManager.DeleteNews(int.Parse(gvMNNews.DataKeys[e.RowIndex].Value.ToString()));

            LoadNews(ViewState["newsSortBy"].ToString(), Helper.IntParse(ViewState["newsPageIndex"].ToString()), false, true);
        }

        void gvMNNews_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateNews,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.NewsId, int.Parse(gvMNNews.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        #region    initilize the add new

        private void LoadNews(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadNews(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadNews(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = NewsManager.GetAllNews().Count;

            cpNews.CurrentPageSize = 10;
            cpNews.TotalRecords = totalRecords;
            cpNews.CurrentPageNumber = 1;
            cpNews.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["newsSortOrder"].ToString().ToLower();
                if (ViewState["newsSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["newsSortOrder"] != null)
                    orderBy = ViewState["newsSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpNews.CurrentPageNumber)
            {
                pageIndex = cpNews.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMNNews.DataSource = SortPageAsc(sortBy, pageIndex, cpNews.CurrentPageSize);
            else
                gvMNNews.DataSource = SortPageDesc(sortBy, pageIndex, cpNews.CurrentPageSize);

            gvMNNews.DataBind();



            ViewState["newsSortBy"] = sortBy;
            ViewState["newsSortOrder"] = orderBy;
        }

        protected List<News> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<News> news = NewsManager.GetAllNews();
            int startIndex = Helper.CalculatePageStartIndex(news.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(news.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    news = news
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "heading":
                    news = news
                        .OrderBy(x => x.Heading)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "dated":
                    news = news
                        .OrderBy(x => x.Dated)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "location":
                    news = news
                        .OrderBy(x => x.Location)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
            }
            return news;
        }

        protected List<News> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<News> news = NewsManager.GetAllNews();
            int startIndex = Helper.CalculatePageStartIndex(news.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(news.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    news = news
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "heading":
                    news = news
                        .OrderByDescending(x => x.Heading)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "dated":
                    news = news
                        .OrderByDescending(x => x.Dated)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
                case "location":
                    news = news
                        .OrderByDescending(x => x.Location)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<News>();
                    break;
            }
            return news;
        }

        #endregion

        protected void btnMNAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateNews,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.NewsId, 0) });
        }

    }
}