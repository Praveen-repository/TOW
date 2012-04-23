using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BAL;
using Web.Code.BO;
using Web.BAL;
using System.Linq.Expressions;

namespace Web.Admin.Controls
{
    public partial class UCManageEvents : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            gvMEEvents.DataKeyNames = new string[] { "Id" };
            gvMEEvents.RowEditing += new GridViewEditEventHandler(gvMEEvents_RowEditing);
            gvMEEvents.RowDeleting += new GridViewDeleteEventHandler(gvMEEvents_RowDeleting);
            gvMEEvents.SelectedIndexChanging += new GridViewSelectEventHandler(gvMEEvents_SelectedIndexChanging);
            gvMEEvents.RowDataBound += new GridViewRowEventHandler(gvMEEvents_RowDataBound);
            gvMEEvents.Sorting += new GridViewSortEventHandler(gvMEEvents_Sorting);
            cpEvents.PageChanged += new Code.Presentation.CustomPagingDelegate.PageChangedEventHandler(cpEvents_PageChanged);

            if (!Page.IsPostBack)
            {
                if (ViewState["eventPageIndex"] == null)
                    ViewState.Add("eventPageIndex", 0);

                LoadEvents("Id", 0, false);
            }
        }

        void cpEvents_PageChanged(object sender, Code.Presentation.CustomPageChangeArgs e)
        {
            ViewState["eventPageIndex"] = e.CurrentPageNumber;

            gvMEEvents.PageIndex = e.CurrentPageNumber;

            LoadEvents(ViewState["eventSortBy"].ToString(), e.CurrentPageNumber, false);
        }

        void gvMEEvents_Sorting(object sender, GridViewSortEventArgs e)
        {
            LoadEvents(e.SortExpression, 0, true);
            cpEvents.ResetPageNumber();
        }

        void gvMEEvents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[8].Controls[0];

                // Get information about the to the row
                var eventName = ((Web.Code.BO.Event)e.Row.DataItem).Name;

                db.OnClientClick = string.Format("return confirm('Are you sure you want to delete the Event?/n {0} ');", eventName.Replace("'", @"\'"));
            }
        }

        void gvMEEvents_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminViewEvent,
                new string[] { string.Format("{0}=view", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.EventId,int.Parse(gvMEEvents.DataKeys[e.NewSelectedIndex].Value.ToString())) });
        }

        void gvMEEvents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            EventManager.DeleteEvent(int.Parse(gvMEEvents.DataKeys[e.RowIndex].Value.ToString()));

            LoadEvents(ViewState["eventSortBy"].ToString(), Helper.IntParse(ViewState["eventPageIndex"].ToString()), false, true);

        }

        void gvMEEvents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateEvents,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.EventId, int.Parse(gvMEEvents.DataKeys[e.NewEditIndex].Value.ToString())) });
        }

        protected void btnMEAAddNew_Click(object sender, EventArgs e)
        {
            //Redirect to add new event
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateEvents,
                new string[] { string.Format("{0}=add", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.EventId, 0) });
        }

        #region    initilize the add new

        private void LoadEvents(string sortBy, int pageIndex, bool changeOrderBy)
        {
            LoadEvents(sortBy, pageIndex, changeOrderBy, false);
        }

        private void LoadEvents(string sortBy, int pageIndex, bool changeOrderBy, bool checkDelete)
        {
            string orderBy = "asc";

            int totalRecords = EventManager.GetAllEvents().Count;

            cpEvents.CurrentPageSize = 10;
            cpEvents.TotalRecords = totalRecords;
          
            cpEvents.CurrentPageNumber = 1;
            cpEvents.ResetRecordCount(totalRecords);

            if (changeOrderBy)
            {
                orderBy = ViewState["eventSortOrder"].ToString().ToLower();
                if (ViewState["eventSortBy"].ToString().ToLower() == sortBy.ToLower())
                    orderBy = (orderBy == "asc") ? "desc" : "asc";
                else
                    orderBy = "asc";
            }
            else
            {
                if (ViewState["eventSortOrder"] != null)
                    orderBy = ViewState["eventSortOrder"].ToString();
            }

            if (checkDelete && pageIndex > cpEvents.CurrentPageNumber)
            {
                pageIndex = cpEvents.CurrentPageNumber;
            }

            if (orderBy == "asc")
                gvMEEvents.DataSource = SortPageAsc(sortBy, pageIndex, cpEvents.CurrentPageSize);
            else
                gvMEEvents.DataSource = SortPageDesc(sortBy, pageIndex, cpEvents.CurrentPageSize);

            gvMEEvents.DataBind();



            ViewState["eventSortBy"] = sortBy;
            ViewState["eventSortOrder"] = orderBy;
        }

        protected List<Event> SortPageAsc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Event> events = EventManager.GetAllEvents();
            int startIndex = Helper.CalculatePageStartIndex(events.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(events.Count, pageIndex - 1, setPageSize);
            switch (sortBy.ToLower())
            {
                case "id":
                    events = events
                        .OrderBy(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "name":
                    events = events
                        .OrderBy(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "startdate":
                    events = events
                        .OrderBy(x => x.StartDate)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "enddate":
                    events = events
                        .OrderBy(x => x.StartDate)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "location":
                    events = events
                        .OrderBy(x => x.Location)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "state":
                    events = events
                        .OrderBy(x => x.State)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;

            }
            return events;
        }

        protected List<Event> SortPageDesc(string sortBy, int pageIndex, int setPageSize)
        {
            List<Event> events = EventManager.GetAllEvents();
            int startIndex = Helper.CalculatePageStartIndex(events.Count, pageIndex - 1, setPageSize);
            int pageSize = Helper.CalculatePageSize(events.Count, pageIndex - 1, setPageSize);

            switch (sortBy.ToLower())
            {
                case "id":
                    events = events
                        .OrderByDescending(x => x.Id)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "name":
                    events = events
                        .OrderByDescending(x => x.Name)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "startdate":
                    events = events
                        .OrderByDescending(x => x.StartDate)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "enddate":
                    events = events
                        .OrderByDescending(x => x.StartDate)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "location":
                    events = events
                        .OrderByDescending(x => x.Location)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;
                case "state":
                    events = events
                        .OrderByDescending(x => x.State)
                        .Skip(startIndex)
                        .Take(pageSize)
                        .ToList<Event>();
                    break;

            }
            return events;
        }

        #endregion

    }
}