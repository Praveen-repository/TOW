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
    public partial class UCManageRecognitions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvMRRecognitions.DataKeyNames = new string[] { "Id" };
            gvMRRecognitions.RowEditing += new GridViewEditEventHandler(gvMRRecognitions_RowEditing);
            gvMRRecognitions.RowDeleting += new GridViewDeleteEventHandler(gvMRRecognitions_RowDeleting);
            gvMRRecognitions.SelectedIndexChanging += new GridViewSelectEventHandler(gvMRRecognitions_SelectedIndexChanging);
            gvMRRecognitions.RowDataBound += new GridViewRowEventHandler(gvMRRecognitions_RowDataBound);
            gvMRRecognitions.Sorting += new GridViewSortEventHandler(gvMRRecognitions_Sorting);
            cpRecognitions.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpRecognitions_PageChanged);

            if (!Page.IsPostBack)
            {
                if (ViewState["recPageIndex"] == null)
                    ViewState.Add("recPageIndex", 0);

                LoadRecognitions("Id", 0, false);
            }
        }

        void gvMRRecognitions_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadRecognitions(e.SortExpression, 0, true);
            cpRecognitions.ResetPageNumber();
        }

        void cpRecognitions_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["recPageIndex"] = e.CurrentPageNumber;

            gvMRRecognitions.PageIndex = e.CurrentPageNumber;

            LoadRecognitions(ViewState["recSortBy"].ToString(), e.CurrentPageNumber, false);
        }

        void gvMRRecognitions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                // Get information about the to the row
                var recognitionTitle = ((Web.Code.BO.Recognition)e.Row.DataItem).Title;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the Recognition?/n {0} ');", recognitionTitle.Replace("'", @"\'"));
            }
        }

        void gvMRRecognitions_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewRecognition,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.RecognitionId,int.Parse(gvMRRecognitions.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMRRecognitions_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            RecognitionManager.DeleteRecognition(int.Parse(gvMRRecognitions.DataKeys[e.RowIndex].Value.ToString()));

            LoadRecognitions(ViewState["recSortBy"].ToString(), Helper.IntParse(ViewState["recPageIndex"].ToString()), false, true);
        }

        void gvMRRecognitions_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateRecognitions,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.RecognitionId, int.Parse(gvMRRecognitions.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        protected void btnMRAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateRecognitions,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.RecognitionId, 0) });
        }

        #region    initilize the add new

        private void LoadRecognitions(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadRecognitions(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadRecognitions(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = RecognitionManager.GetAllRecognitions().Count;

            cpRecognitions.CurrentPageSize = 10;
            cpRecognitions.TotalRecords = totalRecords;
            cpRecognitions.CurrentPageNumber = 1;
            cpRecognitions.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["recSortOrder"].ToString().ToLower();
                if (ViewState["recSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["recSortOrder"] != null)
                    orderBy = ViewState["recSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpRecognitions.CurrentPageNumber)
            {
                pageIndex = cpRecognitions.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMRRecognitions.DataSource = SortPageAsc(sortBy, pageIndex, cpRecognitions.CurrentPageSize);
            else
                gvMRRecognitions.DataSource = SortPageDesc(sortBy, pageIndex, cpRecognitions.CurrentPageSize);

            gvMRRecognitions.DataBind();



            ViewState["recSortBy"] = sortBy;
            ViewState["recSortOrder"] = orderBy;
        }

        protected List<Recognition> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Recognition> recognitions = RecognitionManager.GetAllRecognitions();
            int startIndex = Helper.CalculatePageStartIndex(recognitions.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(recognitions.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    recognitions = recognitions
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Recognition>();
                    break;
                case "title":
                    recognitions = recognitions
                        .OrderBy(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Recognition>();
                    break;
            }
            return recognitions;
        }

        protected List<Recognition> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Recognition> recognitions = RecognitionManager.GetAllRecognitions();
            int startIndex = Helper.CalculatePageStartIndex(recognitions.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(recognitions.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    recognitions = recognitions
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Recognition>();
                    break;
                case "title":
                    recognitions = recognitions
                        .OrderByDescending(x => x.Title)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Recognition>();
                    break;
            }
            return recognitions;
        }


        #endregion

    }
}