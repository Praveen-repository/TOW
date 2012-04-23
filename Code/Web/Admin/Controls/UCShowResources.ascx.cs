using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BO;
using Web.BAL;

namespace Web.Admin.Controls
{
    public partial class UCShowResources : System.Web.UI.UserControl
    {
        List<Resource> resources = new List<Resource>(100);

        public List<Resource> Resources { get { return resources; } set { resources = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            gvShowResources.DataKeyNames = new string[] { "FileName" };
            gvShowResources.Sorting += new GridViewSortEventHandler(gvShowResources_Sorting);
            //gvShowResources.PageIndexChanging += new GridViewPageEventHandler(gvShowResources_PageIndexChanging);
            cpShowResources.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpShowResources_PageChanged);


            if (!IsPostBack)
                LoadShowResources("Id", 0, false, false);
        }

        void cpShowResources_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["showResourcePageIndex"] = e.CurrentPageNumber;

            gvShowResources.PageIndex = e.CurrentPageNumber;

            LoadShowResources(ViewState["showResourceSortBy"].ToString(), e.CurrentPageNumber, false, false);
        }

        void gvShowResources_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadShowResources(e.SortExpression, 0, true, false);
            cpShowResources.ResetPageNumber();
        }

        protected void LoadShowResources(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";
            if (ViewState["showResourcePageIndex"] == null)
                ViewState.Add("showResourcePageIndex", 0);


            int totalRecords = this.Resources.Count;

            cpShowResources.CurrentPageSize = 10;
            cpShowResources.TotalRecords = totalRecords;
            cpShowResources.CurrentPageNumber = 1;
            cpShowResources.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["showResourceSortOrder"].ToString().ToLower();
                if (ViewState["showResourceSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["showResourceSortOrder"] != null)
                    orderBy = ViewState["showResourceSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpShowResources.CurrentPageNumber)
            {
                pageIndex = cpShowResources.CurrentPageNumber;
            }

            ViewState["showResourcePageIndex"] = pageIndex;

            if (orderBy == "asc")
                gvShowResources.DataSource = SortShowPageAsc(sortBy, pageIndex, cpShowResources.CurrentPageSize);
            else
                gvShowResources.DataSource = SortShowPageDesc(sortBy, pageIndex, cpShowResources.CurrentPageSize);

            gvShowResources.DataBind();


            ViewState["showResourceSortBy"] = sortBy;
            ViewState["showResourceSortOrder"] = orderBy;
        }

        protected List<Resource> SortShowPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Resource> resources = this.Resources;
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
                case "title":
                    resources = resources
                        .OrderBy(x => x.Title)
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
            }
            return resources;
        }

        protected List<Resource> SortShowPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Resource> resources = this.Resources;
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

    }
}