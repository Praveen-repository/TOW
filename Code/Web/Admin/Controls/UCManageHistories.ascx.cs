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
    public partial class UCManageHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMEHistories.DataKeyNames = new string[] { "Id" };
            gvMEHistories.RowEditing += new GridViewEditEventHandler(gvMEHistories_RowEditing);
            gvMEHistories.RowDeleting += new GridViewDeleteEventHandler(gvMEHistories_RowDeleting);
            gvMEHistories.SelectedIndexChanging += new GridViewSelectEventHandler(gvMEHistories_SelectedIndexChanging);
            gvMEHistories.RowDataBound += new GridViewRowEventHandler(gvMEHistories_RowDataBound);
            gvMEHistories.Sorting += new GridViewSortEventHandler(gvMEHistories_Sorting);
            cpHistory.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpHistory_PageChanged);

            if (!IsPostBack)
            {
                if (ViewState["historyPageIndex"] == null)
                    ViewState.Add("historyPageIndex", 0);
                LoadHistories("Id", 0, false, false);
            }
        }

        void gvMEHistories_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadHistories(e.SortExpression, 0, true, false);
            cpHistory.ResetPageNumber();

            //throw new NotImplementedException();
        }

        void cpHistory_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["historyPageIndex"] = e.CurrentPageNumber;

            gvMEHistories.PageIndex = e.CurrentPageNumber;

            LoadHistories(ViewState["historySortBy"].ToString(), e.CurrentPageNumber, false, false);

            //throw new NotImplementedException();
        }

        void gvMEHistories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

                // Get information about the to the row
                var historyTitle = ((Web.Code.BO.History)e.Row.DataItem).Title;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the History?/n {0} ');", historyTitle.Replace("'", @"\'"));
            }
        }

        void gvMEHistories_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewHistory,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.HistoryId,int.Parse(gvMEHistories.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMEHistories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            HistoryManager.DeleteHistory(int.Parse(gvMEHistories.DataKeys[e.RowIndex].Value.ToString()));

            LoadHistories(ViewState["historySortBy"].ToString(), Helper.IntParse(ViewState["historyPageIndex"].ToString()), false, true);


        }

        void gvMEHistories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateHistory,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.HistoryId, int.Parse(gvMEHistories.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        #region    initilize the add new
        private void LoadHistories(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = HistoryManager.GetAllHistories().Count;

            cpHistory.CurrentPageSize = 10;
            cpHistory.TotalRecords = totalRecords;
            //cpEvents.TotalPages = totalRecords % cpEvents.CurrentPageSize == 0 ? totalRecords / cpEvents.CurrentPageSize : totalRecords / cpEvents.CurrentPageSize + 1; 
            cpHistory.CurrentPageNumber = 1;
            cpHistory.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["historySortOrder"].ToString().ToLower();
                if (ViewState["historySortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["historySortOrder"] != null)
                    orderBy = ViewState["historySortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpHistory.CurrentPageNumber)
            {
                pageIndex = cpHistory.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMEHistories.DataSource = SortPageAsc(sortBy, pageIndex, cpHistory.CurrentPageSize);
            else
                gvMEHistories.DataSource = SortPageDesc(sortBy, pageIndex, cpHistory.CurrentPageSize);

            gvMEHistories.DataBind();

            ViewState["historySortBy"] = sortBy;
            ViewState["historySortOrder"] = orderBy;
        }


        protected List<History> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<History> history = HistoryManager.GetAllHistories();
            int startIndex = Helper.CalculatePageStartIndex(history.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(history.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    history = history
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
                case "title":
                    history = history
                        .OrderBy(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
                case "type":
                    history = history
                        .OrderBy(x => x.Type)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
            }
            return history;
        }

        protected List<History> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<History> history = HistoryManager.GetAllHistories();
            int startIndex = Helper.CalculatePageStartIndex(history.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(history.Count, pageIndex - 1, setPageSize);

            switch (sortBy.ToLower())
            {
                case "id":
                    history = history
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
                case "title":
                    history = history
                        .OrderByDescending(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
                case "type":
                    history = history
                        .OrderByDescending(x => x.Type)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<History>();
                    break;
            }
            return history;
        }


        #endregion

        protected void btnMGAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateHistory,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.HistoryId, 0) });
        }

    }
}