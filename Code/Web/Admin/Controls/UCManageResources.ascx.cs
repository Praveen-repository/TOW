using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BAL;
using Web.Code.BO;
using System.IO;

namespace Web.Admin.Controls
{
    public partial class UCManageResources : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMRResources.DataKeyNames = new string[] { "Id" };
            gvMRResources.RowEditing += new GridViewEditEventHandler(gvMRResources_RowEditing);
            gvMRResources.RowDeleting += new GridViewDeleteEventHandler(gvMRResources_RowDeleting);
            gvMRResources.SelectedIndexChanging += new GridViewSelectEventHandler(gvMRResources_SelectedIndexChanging);
            gvMRResources.RowDataBound += new GridViewRowEventHandler(gvMRResources_RowDataBound);
            gvMRResources.Sorting += new GridViewSortEventHandler(gvMRResources_Sorting);
            //gvMRResources.PageIndexChanging += new GridViewPageEventHandler(gvMRResources_PageIndexChanging);
            cpResources.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpResources_PageChanged);



            if (!Page.IsPostBack)
            {
                if (ViewState["resPageIndex"] == null)
                    ViewState.Add("resPageIndex", 0);

                LoadResources("Id", 0, false);
            }

        }

        void gvMRResources_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadResources(e.SortExpression, 0, true);
            cpResources.ResetPageNumber();
        }

        void cpResources_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["resPageIndex"] = e.CurrentPageNumber;

            gvMRResources.PageIndex = e.CurrentPageNumber;

            LoadResources(ViewState["resSortBy"].ToString(), e.CurrentPageNumber, false);
            //throw new NotImplementedException();
        }

        //void gvMRResources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvMRResources.PageIndex = e.NewPageIndex;
        //    LoadResources();
        //}

        void gvMRResources_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var resource = ((Web.Code.BO.Resource)e.Row.DataItem);
                //reference to view button
                LinkButton dbView = (LinkButton)e.Row.Cells[0].Controls[0];
                dbView.Attributes.Add("class", "screenshot");
                dbView.Attributes.Add("rel", string.Format("{0}/{1}", Resources.PagePath.UploadWebPath, resource.FileName.Replace(@"\", "/")));

                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                // Get information about the to the row
                var resourceTitle = ((Web.Code.BO.Resource)e.Row.DataItem).Title;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the Resource?/n {0} ');", resourceTitle.Replace("'", @"\'"));
            }
        }

        void gvMRResources_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewResource,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.ResourceId,int.Parse(gvMRResources.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMRResources_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MasterManager.DeleteResource(int.Parse(gvMRResources.DataKeys[e.RowIndex].Value.ToString()));
            LoadResources(ViewState["resSortBy"].ToString(), Helper.IntParse(ViewState["resPageIndex"].ToString()), false, true);
        }

        void gvMRResources_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateResource,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.ResourceId, int.Parse(gvMRResources.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        protected void btnMRAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateResource,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.ResourceId, 0) });
        }

        #region    initilize the add new
        private void LoadResources(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadResources(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadResources(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = MasterManager.GetAllResources().Count;

            cpResources.CurrentPageSize = 10;
            cpResources.TotalRecords = totalRecords;
            cpResources.CurrentPageNumber = 1;
            cpResources.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["resSortOrder"].ToString().ToLower();
                if (ViewState["resSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["resSortOrder"] != null)
                    orderBy = ViewState["resSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpResources.CurrentPageNumber)
            {
                pageIndex = cpResources.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMRResources.DataSource = SortPageAsc(sortBy, pageIndex, cpResources.CurrentPageSize);
            else
                gvMRResources.DataSource = SortPageDesc(sortBy, pageIndex, cpResources.CurrentPageSize);
            gvMRResources.DataBind();

            ViewState["resSortBy"] = sortBy;
            ViewState["resSortOrder"] = orderBy;

        }


        protected List<Resource> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Resource> resources = MasterManager.GetAllResources();
            int startIndex = Helper.CalculatePageStartIndex(resources.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(resources.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    resources = resources
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;
                case "name":
                    resources = resources
                        .OrderBy(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;
                case "title":
                    resources = resources
                        .OrderBy(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;
            }
            return resources;
        }

        protected List<Resource> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Resource> resources = MasterManager.GetAllResources();
            int startIndex = Helper.CalculatePageStartIndex(resources.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(resources.Count, pageIndex - 1, setPageSize);

            switch (sortBy.ToLower())
            {
                case "id":
                    resources = resources
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;
                case "name":
                    resources = resources
                        .OrderByDescending(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;
                case "title":
                    resources = resources
                        .OrderByDescending(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Resource>();
                    break;

            }
            return resources;
        }

        #endregion

    }
}