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
    public partial class UCManagePages : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            gvMNPages.DataKeyNames = new string[] { "Id" };
            gvMNPages.RowEditing += new GridViewEditEventHandler(gvMNPages_RowEditing);
            gvMNPages.RowDeleting += new GridViewDeleteEventHandler(gvMNPages_RowDeleting);
            gvMNPages.SelectedIndexChanging += new GridViewSelectEventHandler(gvMNPages_SelectedIndexChanging);
            gvMNPages.RowDataBound += new GridViewRowEventHandler(gvMNPages_RowDataBound);
            gvMNPages.Sorting += new GridViewSortEventHandler(gvMNPages_Sorting);

            cpPages.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpPages_PageChanged);
            if (!Page.IsPostBack)
            {
                if (ViewState["pagePageIndex"] == null)
                    ViewState.Add("pagePageIndex", 0);

                LoadPages("Id", 0, false);
            }

        }

        void gvMNPages_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadPages(e.SortExpression, 0, true);
            cpPages.ResetPageNumber();
        }

        void cpPages_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["pagePageIndex"] = e.CurrentPageNumber;

            gvMNPages.PageIndex = e.CurrentPageNumber;

            LoadPages(ViewState["pageSortBy"].ToString(), e.CurrentPageNumber, false);
        }

        void gvMNPages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[6].Controls[0];

                // Get information about the to the row
                var pageName = ((Web.Code.BO.Page)e.Row.DataItem).PageName;
                var pageId = ((Web.Code.BO.Page)e.Row.DataItem).Id;
                if (PagesManager.IsPageParent(pageId))
                    db.OnClientClick = string.Format("return javascript:function(){{alert('Can not delete page {0} Please first delete its child pages.'); return false;}}", pageName.Replace("'", @"\'"));
                else
                    db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the Page?/n {0} ');", pageName.Replace("'", @"\'"));
            }
        }

        void gvMNPages_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewPage,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.PageId,int.Parse(gvMNPages.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMNPages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            PagesManager.DeletePage(int.Parse(gvMNPages.DataKeys[e.RowIndex].Value.ToString()));
            
            LoadPages(ViewState["pageSortBy"].ToString(), Helper.IntParse(ViewState["pagePageIndex"].ToString()), false, true);
        }

        void gvMNPages_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdatePages,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.PageId, int.Parse(gvMNPages.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        #region    initilize the add new

        private void LoadPages(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadPages(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadPages(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = PagesManager.GetAllPages().Count;

            cpPages.CurrentPageSize = 10;
            cpPages.TotalRecords = totalRecords;
            cpPages.CurrentPageNumber = 1;
            cpPages.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["pageSortOrder"].ToString().ToLower();
                if (ViewState["pageSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["pageSortOrder"] != null)
                    orderBy = ViewState["pageSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpPages.CurrentPageNumber)
            {
                pageIndex = cpPages.CurrentPageNumber;
            }

            if (orderBy == "asc")
                 gvMNPages.DataSource = SortPageAsc(sortBy, pageIndex, cpPages.CurrentPageSize);
            else
                gvMNPages.DataSource = SortPageDesc(sortBy, pageIndex, cpPages.CurrentPageSize);

            gvMNPages.DataBind();



            ViewState["pageSortBy"] = sortBy;
            ViewState["pageSortOrder"] = orderBy;
        }

        protected List<Code.BO.Page> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Code.BO.Page> page = PagesManager.GetAllPages();
            int startIndex = Helper.CalculatePageStartIndex(page.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(page.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    page = page
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "pagename":
                    page = page
                        .OrderBy(x => x.PageName)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "parents":
                    page = page
                        .OrderBy(x => x.ParentHierarchy)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "active":
                    page = page
                        .OrderBy(x => x.Active)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
            }
            return page;
        }

        protected List<Code.BO.Page> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Code.BO.Page> page = PagesManager.GetAllPages();
            int startIndex = Helper.CalculatePageStartIndex(page.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(page.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    page = page
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "pagename":
                    page = page
                        .OrderByDescending(x => x.PageName)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "parents":
                    page = page
                        .OrderByDescending(x => x.ParentHierarchy)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
                case "active":
                    page = page
                        .OrderByDescending(x => x.Active)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Code.BO.Page>();
                    break;
            }
            return page;
        }

        #endregion

        protected void btnMNAAddPage_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdatePages,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.PageId, 0) });
        }

    }
}