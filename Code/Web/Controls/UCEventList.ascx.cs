using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Web.Code.BAL;
using Web.Code.BO;
using Web.BAL;

namespace Web.Controls
{
    public partial class UCEventList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbTabes = new StringBuilder();
            StringBuilder sbPanels = new StringBuilder();
            int dialogHeight = 740;
            int dialogWidth = 800;
            StringBuilder sbTable = new StringBuilder();

            int counter = 1;
            //Get event list
            var events = EventManager.GetAllEvents();

            //get events tabs
            var eventLevels = from x in events select x.Level;
            eventLevels = eventLevels.Distinct();

            foreach (string eventLevel in eventLevels)
            {


                var eventsByLevel = events.FindAll(x => x.Level == eventLevel);

                //get events Panels
                if (counter == 1)
                {
                    sbTabes.Append(string.Format(@"<div class=""tab selected first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, eventLevel));
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"" style=""display:block"">", counter));
                }
                else
                {
                    sbTabes.Append(string.Format(@"<div class=""tab first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, eventLevel));
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"">", counter));
                }

                sbPanels.Append(string.Format(@"<h1>{0}</h1>", eventLevel));
                //create table here
                sbPanels.Append(string.Format(@"<table cellspacing=""1"" id=""{0}"" class=""tablesorter"">", eventLevel));
                sbPanels.Append(string.Format(@"<thead><tr><th style=""width:320px"">Name</th><th style=""width:60px"">Year</th><th style=""width:80px"">Location</th><th style=""width:80px"">State</th></tr></thead>"));
                sbPanels.Append(string.Format(@"<tfoot><tr><th>Name</th><th>Year</th><th>Location</th><th>State</th></tr></tfoot><tbody>"));

                foreach (Event _event in eventsByLevel)
                {
                    sbPanels.Append(string.Format(@"<tr><td><a id=""event_opener_{4}"">{0}</a></td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", _event.Name, _event.Year, _event.Location, _event.State, _event.Id));
                }
                sbPanels.Append(string.Format(@"<tbody></table>"));

                sbPanels.Append(string.Format(@"<div id=""eventDetails_{0}"">", eventLevel));
                foreach (Event _event in eventsByLevel)
                {
                    sbPanels.Append(string.Format(@"{0}", ShowEventDetails(_event)));
                    sbPanels.Append(string.Format(@"<div id=""scriptBlock{0}""><script type=""text/javascript"">", _event.Id));
                    sbPanels.Append(string.Format(@"$(function(){0}", "{"));
                    sbPanels.Append(string.Format(@"$(""#event_dialog_{0}"").dialog({1} autoOpen: false, height: {3}, width: {4}, modal: true, show: ""blind"", hide: ""slide""{2});", _event.Id, "{", "}", dialogHeight, dialogWidth));
                    sbPanels.Append(string.Format(@"$(""#event_opener_{0}"").click(function(){1} $( ""#event_dialog_{0}"" ).dialog(""open""); return false; {2});", _event.Id, "{", "}"));
                    sbPanels.Append(string.Format(@"{0});</script></div>", "}"));
                }

                sbPanels.Append(string.Format(@"</div>"));

                sbPanels.Append(string.Format(@"<div id=""pager_{0}"" class=""pager"">", eventLevel));
                sbPanels.Append(string.Format(@"<img src=""/Styles/images/first.png"" class=""first"" alt=""First"" />"));
                sbPanels.Append(string.Format(@"<img src=""/Styles/images/prev.png"" class=""prev"" alt=""Previous"" />"));
                sbPanels.Append(string.Format(@"<input type=""text"" class=""pagedisplay"" readonly />"));
                sbPanels.Append(string.Format(@"<img src=""/Styles/images/next.png"" class=""next"" alt=""Next"" />"));
                sbPanels.Append(string.Format(@"<img src=""/Styles/images/last.png"" class=""last"" alt=""Last"" />"));
                sbPanels.Append(string.Format(@"<select class=""pagesize"">"));
                sbPanels.Append(string.Format(@"<option selected=""selected"" value=""10"">10</option>"));
                sbPanels.Append(string.Format(@"<option value=""20"">20</option>"));
                sbPanels.Append(string.Format(@"<option value=""30"">30</option>"));
                sbPanels.Append(string.Format(@"<option value=""40"">40</option>"));
                sbPanels.Append(string.Format(@"</select>"));
                sbPanels.Append(string.Format(@"</div>"));

                sbPanels.Append(string.Format(@"<script type=""text/javascript"">"));
                sbPanels.Append(string.Format(@"$(document).ready(function(){0}", "{"));
                sbPanels.Append(string.Format(@"$(""#{0}"")", eventLevel));
                sbPanels.Append(string.Format(@".tablesorter({{ widthFixed: true, widgets: ['zebra'] }})"));
                sbPanels.Append(string.Format(@".tablesorterPager({{container: $(""#pager_{0}"") }});", eventLevel));
                sbPanels.Append(string.Format(@"{0});", "}"));
                sbPanels.Append(string.Format(@"</script>"));

                sbPanels.Append(string.Format(@"</div>"));
                counter++;
            }

            EventTabs.Text = sbTabes.ToString();
            EventPanes.Text = sbPanels.ToString();
        }

        private string ShowEventDetails(Event _event)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(@"<div id=""event_dialog_{0}"" title=""{1}"">", _event.Id, _event.Name));

            sb.Append(string.Format(@"<h1>{0} - ({1})</h1>", _event.Name, _event.Year));

            sb.Append(string.Format(@"<p> Dated: {0} - {1}, Location: {2} - {3}</p>", Helper.FormatDateToMMDDYYYY(_event.StartDate), Helper.FormatDateToMMDDYYYY(_event.EndDate), _event.Location, _event.State));


            sb.Append(string.Format(@"<div><span class=""dialog50"">{0}</span>", ShowCategories(_event.Id)));
            sb.Append(string.Format(@"<span class=""dialog50"">{0}</span></div>", ShowWeights(_event.Id)));

            sb.Append(string.Format(@"<h1>Details</h1><p>{0}</p>", _event.Details));

            sb.Append(string.Format(@"<p>{0}</p>", ShowResults(_event.Id)));

            sb.Append(string.Format(@"</div>", _event.Name));
            return sb.ToString();
        }

        private string ShowCategories(int eventId)
        {
            StringBuilder sb = new StringBuilder();

            var eventCategories = EventManager.GetEventCategories(eventId);

            sb.Append(string.Format(@"<h1>Categories</h1><ul>"));
            foreach (Category category in eventCategories)
            {
                sb.Append(string.Format(@"<li>{0}</li>", category.Name));
            }

            sb.Append(string.Format(@"</ul>"));
            return sb.ToString();
        }

        private string ShowWeights(int eventId)
        {
            StringBuilder sb = new StringBuilder();

            var eventWeights = EventManager.GetEventWeights(eventId);

            sb.Append(string.Format(@"<h1>Weights</h1><ul>"));
            foreach (Weight weight in eventWeights)
            {
                sb.Append(string.Format(@"<li>{0}</li>", weight.Class));
            }

            sb.Append(string.Format(@"</ul>"));
            return sb.ToString();
        }

        private string ShowResults(int eventId)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            string trClass = "odd";
            var eventResults = EventManager.GetEventResults(eventId);

            sb.Append(string.Format(@""));
            sb.Append(string.Format(@"<h1>Results</h1>"));
            sb.Append(string.Format(@"<div class=""evtres"">"));

            sb.Append(string.Format(@"<table id=""{0}""> ", eventId));
            sb.Append(string.Format(@"<thead><tr><th class=""header"">Category</th><th>Gold</th><th>Silver</th><th>Bronze</th></tr></thead>"));
            sb.Append(string.Format(@"<tbody>"));

            foreach (Result result in eventResults)
            {
                trClass = (counter % 2) == 0 ? "odd" : "even";
                sb.Append(string.Format(@"<tr class={4}><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", result.Category, result.Gold, result.Silver, result.Bronze, trClass));
                counter++;
            }

            sb.Append(string.Format(@"</tbody></table></div>"));
            return sb.ToString();
        }
    }
}