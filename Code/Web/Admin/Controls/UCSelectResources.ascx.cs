using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BAL;
using Web.Code.BO;
using Web.BAL;

namespace Web.Admin.Controls
{
    public partial class UCSelectResources : System.Web.UI.UserControl
    {
        List<Resource> selectedResources = new List<Resource>(500);

        protected void Page_Load(object sender, EventArgs e)
        {
            gvShowResources.DataKeyNames = new string[] { "FileName" };
            gvShowResources.RowDeleting += new GridViewDeleteEventHandler(gvShowResources_RowDeleting);
            gvShowResources.Sorting += new GridViewSortEventHandler(gvShowResources_Sorting);
            //gvShowResources.PageIndexChanging += new GridViewPageEventHandler(gvShowResources_PageIndexChanging);
            cpShowResources.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpShowResources_PageChanged);
           
            
            gvAddResources.DataKeyNames = new string[] { "FileName" };
            gvAddResources.SelectedIndexChanging += new GridViewSelectEventHandler(gvAddResources_SelectedIndexChanging);
            //gvAddResources.PageIndexChanging += new GridViewPageEventHandler(gvAddResources_PageIndexChanging);
            gvAddResources.Sorting += new GridViewSortEventHandler(gvAddResources_Sorting);
            cpAddResources.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpAddResources_PageChanged);


            if (!IsPostBack)
                LoadResources("Id", 0, false, false);

        }

        void gvShowResources_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadShowResources(e.SortExpression, 0, true, false);
            cpShowResources.ResetPageNumber();
        }

        void cpShowResources_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["showResourcePageIndex"] = e.CurrentPageNumber;

            gvShowResources.PageIndex = e.CurrentPageNumber;

            LoadShowResources(ViewState["showResourceSortBy"].ToString(), e.CurrentPageNumber, false, false);
        }

        void gvAddResources_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadResources(e.SortExpression, 0, true, false);
            cpAddResources.ResetPageNumber();
        }

        void cpAddResources_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["addResourcePageIndex"] = e.CurrentPageNumber;

            gvAddResources.PageIndex = e.CurrentPageNumber;

            LoadResources(ViewState["resourceSortBy"].ToString(), e.CurrentPageNumber, false, false);
        }

        //void gvAddResources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvAddResources.PageIndex = e.NewPageIndex;
        //    LoadResources(ViewState["resourceSortBy"].ToString(), e.CurrentPageNumber, false, false);
        //}

        void gvAddResources_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //read from viewstate
            if (ViewState["selectedResources"] == null)
                ViewState.Add("selectedResources", new List<Resource>(20));

            selectedResources = (List<Resource>)ViewState["selectedResources"];


            var selectedResource = MasterManager.GetResourceById(int.Parse(gvAddResources.Rows[e.NewSelectedIndex].Cells[0].Text));
            //add category for temp memory
            if (!selectedResources.Contains(selectedResource))
                selectedResources.Add(selectedResource);

            //add to view state
            ViewState["selectedResources"] = selectedResources;

            LoadShowResources(ViewState["showResourceSortBy"].ToString(), Helper.IntParse(ViewState["showResourcePageIndex"].ToString()), false, true);
            //bind the categories list
            //gvShowResources.DataSource = selectedResources;
            //gvShowResources.DataBind();
        }

        //void gvShowResources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //read from viewstate
        //    if (ViewState["selectedResources"] == null)
        //        ViewState.Add("selectedResources", new List<Resource>(20));

        //    selectedResources = (List<Resource>)ViewState["selectedResources"];
        //    gvShowResources.PageIndex = e.NewPageIndex;

        //    gvShowResources.DataSource = selectedResources;
        //    gvShowResources.DataBind();
        //}

        void gvShowResources_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //read from viewstate
            if (ViewState["selectedResources"] != null)
            {
                selectedResources = (List<Resource>)ViewState["selectedResources"];

                //remove from temp memory
                selectedResources.Remove(new Resource() { FileName = (string)e.Keys["FileName"] });

                //add to view state
                ViewState["selectedResources"] = selectedResources;

                //bind the categories list
                LoadShowResources(ViewState["showResourceSortBy"].ToString(), Helper.IntParse(ViewState["showResourcePageIndex"].ToString()), false, true);

                //gvShowResources.DataSource = selectedResources;
                //gvShowResources.DataBind();
            }
        }

        private void LoadResources(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = MasterManager.GetAllResources().Count;

            cpAddResources.CurrentPageSize = 10;
            cpAddResources.TotalRecords = totalRecords;
            cpAddResources.CurrentPageNumber = 1;
            cpAddResources.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["resourceSortOrder"].ToString().ToLower();
                if (ViewState["resourceSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["resourceSortOrder"] != null)
                    orderBy = ViewState["resourceSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpAddResources.CurrentPageNumber)
            {
                pageIndex = cpAddResources.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvAddResources.DataSource = SortPageAsc(sortBy, pageIndex, cpAddResources.CurrentPageSize);
            else
                gvAddResources.DataSource = SortPageDesc(sortBy, pageIndex, cpAddResources.CurrentPageSize);

            gvAddResources.DataBind();



            ViewState["resourceSortBy"] = sortBy;
            ViewState["resourceSortOrder"] = orderBy;



            //gvAddResources.DataSource = MasterManager.GetAllResources();
            //gvAddResources.DataBind();
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

        protected void LoadShowResources(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete) 
        {
            string orderBy = "asc";
            if(ViewState["showResourcePageIndex"] == null)
                ViewState.Add("showResourcePageIndex", 0);
            

            int totalRecords = GetSelectedResources().Count;

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
            List<Resource> resources = GetSelectedResources();
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
            List<Resource> resources = GetSelectedResources();
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

        public List<Resource> GetSelectedResources()
        {
            //read from viewstate
            if (ViewState["selectedResources"] == null)
                ViewState.Add("selectedResources", new List<Resource>(20));

            selectedResources = (List<Resource>)ViewState["selectedResources"];
            return selectedResources;
        }

        public void SetSelectedResources(List<Resource> setSelectedResources)
        {
            //read from viewstate
            if (ViewState["selectedResources"] == null)
                ViewState.Add("selectedResources", setSelectedResources);

            ViewState["selectedResources"] = setSelectedResources;

            selectedResources = setSelectedResources;

            LoadShowResources("Id", 0, false, false);
            //gvShowResources.DataSource = (List<Resource>)ViewState["selectedResources"];

           // LoadShowResources("Id", 0, false, false);
            //gvShowResources.DataBind();
        }
    }
}