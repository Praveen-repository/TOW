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
    public partial class UCAddUpdateEvent : System.Web.UI.UserControl
    {
        List<Category> selectedCategories = new List<Category>(10);
        List<Weight> selectedWeights = null;
        List<Result> selectedResults = null;

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
            eventId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.EventId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);


            gvAUEShowCategories.DataKeyNames = new string[] { "Name" };
            gvAUEShowCategories.RowDeleting += new GridViewDeleteEventHandler(gvAUEShowCategories_RowDeleting);

            gvAUEAWShowWeights.DataKeyNames = new string[] { "Class" };
            gvAUEAWShowWeights.RowDeleting += new GridViewDeleteEventHandler(gvAUEAWShowWeights_RowDeleting);

            gvAUEARShowResults.DataKeyNames = new string[] { "Category", "Weight" };
            gvAUEARShowResults.RowDeleting += new GridViewDeleteEventHandler(gvAUEARShowResults_RowDeleting);

            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewEventDetails();
                        break;
                    case "edit":
                        LoadEventDetails(eventId);
                        break;
                }
            }

        }

        protected List<string> ValidateEvent(Event valToEvent)
        {
            List<string> errorMsg = new List<string>(25);

            //check event duplicate
            if ((valToEvent.Id > 0 && EventManager.GetAllEvents().Exists(x => x.Id != valToEvent.Id && x.Name == valToEvent.Name)) || (!(valToEvent.Id > 0) && EventManager.GetAllEvents().Exists(x => x.Name == valToEvent.Name)))
                errorMsg.Add(Resources.Messages.EventNameDuplidate);

            //validate event start date > end date 
            if (valToEvent.StartDate > valToEvent.EndDate)
                errorMsg.Add(Resources.Messages.EventStartDateGreaterThenEndDate);

            return errorMsg;
        }

        void gvAUEARShowResults_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //read from viewstate
            if (ViewState["selectedResults"] != null)
            {
                selectedResults = (List<Result>)ViewState["selectedResults"];

                //remove result for temp memory
                selectedResults.Remove(new Result() { Category = (string)e.Keys["Category"], Weight = (string)e.Keys["Weight"] });

                //add to view state
                ViewState["selectedResults"] = selectedResults;

                //bind the categories list
                gvAUEARShowResults.DataSource = selectedResults;
                gvAUEARShowResults.DataBind();
            }
        }

        void gvAUEAWShowWeights_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //read from viewstate
            if (ViewState["selectedWeights"] != null)
            {
                selectedWeights = (List<Weight>)ViewState["selectedWeights"];

                //remove category for temp memory
                selectedWeights.Remove(new Weight() { Class = (string)e.Keys[0] });

                //add to view state
                ViewState["selectedWeights"] = selectedWeights;

                //bind the categories list
                gvAUEAWShowWeights.DataSource = selectedWeights;
                gvAUEAWShowWeights.DataBind();
            }
        }

        void gvAUEShowCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //read from viewstate
            if (ViewState["selectedCategories"] != null)
            {
                selectedCategories = (List<Category>)ViewState["selectedCategories"];

                //remove category for temp memory
                selectedCategories.Remove(new Category() { Name = (string)e.Keys[0] });

                //add to view state
                ViewState["selectedCategories"] = selectedCategories;

                //bind the categories list
                gvAUEShowCategories.DataSource = selectedCategories;
                gvAUEShowCategories.DataBind();
            }
        }

        protected void LoadNewEventDetails()
        {
            SelectResources.SetSelectedResources(new List<Resource>());
        }

        protected void LoadEventDetails(int eventId)
        {

            //load Event details
            var viewEvent = EventManager.GetEventById(eventId);
            ltrAUEId.Text = viewEvent.Id.ToString();
            txtAUEName.Text = viewEvent.Name;
            txtAUEYear.Text = viewEvent.Year;
            txtAUEStartDate.Text = viewEvent.StartDate.ToString("dd-MM-yyyy");
            txtAUEEndDate.Text = viewEvent.EndDate.ToString("dd-MM-yyyy");
            txtAUELocation.Text = viewEvent.Location;
            txtAUEState.Text = viewEvent.State;
            txtAUEDetails.Text = viewEvent.Details;
            ddlAUELevel.SelectedValue = viewEvent.Level;

            if (ViewState["selectedCategories"] == null)
                ViewState.Add("selectedCategories", EventManager.GetEventCategories(eventId));
            selectedCategories = (List<Category>)ViewState["selectedCategories"];

            if (ViewState["selectedWeights"] == null)
                ViewState.Add("selectedWeights", EventManager.GetEventWeights(eventId));
            selectedWeights = (List<Weight>)ViewState["selectedWeights"];

            if (ViewState["selectedResults"] == null)
                ViewState.Add("selectedResults", EventManager.GetEventResults(eventId));
            selectedResults = (List<Result>)ViewState["selectedResults"];

            //load event categories
            gvAUEShowCategories.DataSource = selectedCategories;
            gvAUEShowCategories.DataBind();

            //load event weight
            gvAUEAWShowWeights.DataSource = selectedWeights;
            gvAUEAWShowWeights.DataBind();

            //load event results
            gvAUEARShowResults.DataSource = selectedResults;
            gvAUEARShowResults.DataBind();

            //load event resources
            SelectResources.SetSelectedResources(EventManager.GetEventResources(eventId));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                Event tempEvent = new Event()
                {
                    Id = eventId,
                    Name = txtAUEName.Text,
                    StartDate = Helper.GetBritishDate(txtAUEStartDate.Text),
                    EndDate = Helper.GetBritishDate(txtAUEEndDate.Text),
                    Location = txtAUELocation.Text,
                    State = txtAUEState.Text,
                    Year = txtAUEYear.Text,
                    Details = txtAUEDetails.Text,
                    Level = ddlAUELevel.SelectedValue
                };

                var errorList = ValidateEvent(tempEvent);

                if (errorList.Count == 0)
                {

                    //read from viewstate
                    if (ViewState["selectedCategories"] == null)
                        ViewState.Add("selectedCategories", new List<Category>(20));
                    tempEvent.Categories = (List<Category>)ViewState["selectedCategories"];

                    //read from viewstate
                    if (ViewState["selectedWeights"] == null)
                        ViewState.Add("selectedWeights", new List<Weight>(20));
                    tempEvent.Weights = (List<Weight>)ViewState["selectedWeights"];

                    //read from viewstate
                    if (ViewState["selectedResults"] == null)
                        ViewState.Add("selectedResults", new List<Result>(20));
                    tempEvent.Results = (List<Result>)ViewState["selectedResults"];

                    tempEvent.Resources = SelectResources.GetSelectedResources();
                    EventManager.SaveEvent(tempEvent);

                    Helper.GoToMessagePage(string.Format("Event {0} saved successfully.", tempEvent.Name));
                }
                else
                {
                    lblMsg.Text = Helper.FormatMessageToUL(errorList);
                }
            }
            else
            {
                lblMsg.Text = Helper.FormatMessageToUL(new List<string>() { Resources.Messages.PageValidationFailed });
            }
        }

        protected void btnAUEAddCategory_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //read from viewstate
                if (ViewState["selectedCategories"] == null)
                    ViewState.Add("selectedCategories", new List<Category>(20));

                selectedCategories = (List<Category>)ViewState["selectedCategories"];

                //add category for temp memory
                if (!selectedCategories.Contains(new Category() { Name = txtAUEACCategory.Text }))
                    selectedCategories.Add(new Category() { Name = txtAUEACCategory.Text });

                //add to view state
                ViewState["selectedCategories"] = selectedCategories;

                //bind the categories list
                gvAUEShowCategories.DataSource = selectedCategories;
                gvAUEShowCategories.DataBind();
            }
        }

        protected void btnAUEAddWeight_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //read from viewstate
                if (ViewState["selectedWeights"] == null)
                    ViewState.Add("selectedWeights", new List<Weight>(20));

                selectedWeights = (List<Weight>)ViewState["selectedWeights"];

                //add weight for temp memory
                if (!selectedWeights.Contains(new Weight() { Class = txtAUEAWWeights.Text }))
                    selectedWeights.Add(new Weight() { Class = txtAUEAWWeights.Text });

                //add to view state
                ViewState["selectedWeights"] = selectedWeights;

                //bind the Weights list
                gvAUEAWShowWeights.DataSource = selectedWeights;
                gvAUEAWShowWeights.DataBind();
            }
        }

        protected void btnAUEAddResult_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //read from viewstate
                if (ViewState["selectedResults"] == null)
                    ViewState.Add("selectedResults", new List<Result>(20));

                selectedResults = (List<Result>)ViewState["selectedResults"];

                //add Result for temp memory
                if (!selectedResults.Contains(new Result()
                {
                    Category = txtAUEARCategory.Text,
                    Weight = txtAUEARWeight.Text
                }))
                    selectedResults.Add(new Result()
                {
                    Category = txtAUEARCategory.Text,
                    Weight = txtAUEARWeight.Text,
                    Gold = txtAUEARGold.Text,
                    Silver = txtAUEARSilver.Text,
                    Bronze = txtAUEARBronze.Text
                });

                //add to view state
                ViewState["selectedResults"] = selectedResults;

                //bind the results list
                gvAUEARShowResults.DataSource = selectedResults;
                gvAUEARShowResults.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminManageEvents, new string[] { "" });
        }
    }
}