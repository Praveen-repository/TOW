using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BO;
using Web.BAL;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCManageGalleries : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            gvMGGalleries.DataKeyNames = new string[] { "Id" };
            gvMGGalleries.RowEditing += new GridViewEditEventHandler(gvMGGalleries_RowEditing);
            gvMGGalleries.RowDeleting += new GridViewDeleteEventHandler(gvMGGalleries_RowDeleting);
            gvMGGalleries.SelectedIndexChanging += new GridViewSelectEventHandler(gvMGGalleries_SelectedIndexChanging);
            gvMGGalleries.RowDataBound += new GridViewRowEventHandler(gvMGGalleries_RowDataBound);
            gvMGGalleries.Sorting += new GridViewSortEventHandler(gvMGGalleries_Sorting);

            cpGallery.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpGallery_PageChanged);

            if (!Page.IsPostBack)
            {
                if (ViewState["galPageIndex"] == null)
                    ViewState.Add("galPageIndex", 0);

                LoadGalleries("Id", 0, false);
            }

        }

        void cpGallery_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["recPageIndex"] = e.CurrentPageNumber;

            gvMGGalleries.PageIndex = e.CurrentPageNumber;

            LoadGalleries(ViewState["galSortBy"].ToString(), e.CurrentPageNumber, false);

        }

        void gvMGGalleries_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadGalleries(e.SortExpression, 0, true);
            cpGallery.ResetPageNumber();
        }

        void gvMGGalleries_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                // Get information about the to the row
                var galleryName = ((Web.Code.BO.Gallery)e.Row.DataItem).Name;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the Gallery?/n {0} ');", galleryName.Replace("'", @"\'"));
            }
        }

        void gvMGGalleries_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewGallery,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.GalleryId,int.Parse(gvMGGalleries.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMGGalleries_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GalleryManager.DeleteGallery(int.Parse(gvMGGalleries.DataKeys[e.RowIndex].Value.ToString()));

            LoadGalleries(ViewState["galSortBy"].ToString(), Helper.IntParse(ViewState["galPageIndex"].ToString()), false, true);

        }

        void gvMGGalleries_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateGallery,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.GalleryId, int.Parse(gvMGGalleries.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        #region    initilize the add new

        private void LoadGalleries(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadGalleries(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadGalleries(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = GalleryManager.GetAllGalleries().Count;

            cpGallery.CurrentPageSize = 10;
            cpGallery.TotalRecords = totalRecords;
            cpGallery.CurrentPageNumber = 1;
            cpGallery.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["galSortOrder"].ToString().ToLower();
                if (ViewState["galSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["galSortOrder"] != null)
                    orderBy = ViewState["galSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpGallery.CurrentPageNumber)
            {
                pageIndex = cpGallery.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMGGalleries.DataSource = SortPageAsc(sortBy, pageIndex, cpGallery.CurrentPageSize);
            else
                gvMGGalleries.DataSource = SortPageDesc(sortBy, pageIndex, cpGallery.CurrentPageSize);

            gvMGGalleries.DataBind();



            ViewState["galSortBy"] = sortBy;
            ViewState["galSortOrder"] = orderBy;
        }

        protected List<Gallery> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Gallery> galleries = GalleryManager.GetAllGalleries();
            int startIndex = Helper.CalculatePageStartIndex(galleries.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(galleries.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    galleries = galleries
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Gallery>();
                    break;
                case "name":
                    galleries = galleries
                        .OrderBy(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Gallery>();
                    break;
            }
            return galleries;
        }

        protected List<Gallery> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Gallery> galleries = GalleryManager.GetAllGalleries();
            int startIndex = Helper.CalculatePageStartIndex(galleries.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(galleries.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    galleries = galleries
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Gallery>();
                    break;
                case "name":
                    galleries = galleries
                        .OrderByDescending(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Gallery>();
                    break;
            }
            return galleries;
        }



        #endregion

        protected void btnMGAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateGallery,
               new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.GalleryId, 0) });
        }

    }
}