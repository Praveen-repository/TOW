using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BAL;
using Web.BAL;

namespace Web.Admin.Controls
{
    public partial class UCViewEvent : System.Web.UI.UserControl
    {

        string action;
        int eventId;

        protected void Page_Load(object sender, EventArgs e)
        {

            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.EventId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            eventId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.EventId));

            //gvVEShowResources.PageIndexChanging += new GridViewPageEventHandler(gvVEShowResources_PageIndexChanging);

            //load Event details
            var viewEvent = EventManager.GetEventById(eventId);
            ltrVEId.Text = viewEvent.Id.ToString();
            ltrVEName.Text = viewEvent.Name;
            ltrVEYear.Text = viewEvent.Year;
            ltrVEStartDate.Text = viewEvent.StartDate.ToString("dd-MM-yyyy");
            ltrVEEndDate.Text = viewEvent.EndDate.ToString("dd-MM-yyyy");
            ltrVELocation.Text = viewEvent.Location;
            ltrVEState.Text = viewEvent.State;
            ltrVEDetails.Text = viewEvent.Details;

            //load event categories
            gvVEShowCategories.DataSource = EventManager.GetEventCategories(eventId);
            gvVEShowCategories.DataBind();

            //load event weight
            gvVEShowWeights.DataSource = EventManager.GetEventWeights(eventId);
            gvVEShowWeights.DataBind();

            //load event results
            gvVEShowResults.DataSource = EventManager.GetEventResults(eventId);
            gvVEShowResults.DataBind();

            //load event resources
            showResources.Resources = EventManager.GetEventResources(eventId);

        }

        //void gvVEShowResources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    //load event resources
        //    gvVEShowResources.DataSource = EventManager.GetEventResources(eventId);
        //    gvVEShowResources.DataBind();
        //}

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateEvents,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.EventId, eventId) });
        }
    }
}