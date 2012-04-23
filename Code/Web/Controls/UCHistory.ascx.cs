using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web.Controls
{
    public partial class UCHistory : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbTabes = new StringBuilder();
            StringBuilder sbPanels = new StringBuilder();
            int counter = 1;
            //Get news list
            var histories = HistoryManager.GetAllHistories();

            //get History tabs
            var historyTypes = from x in histories select x.Type;
            historyTypes = historyTypes.Distinct();



            foreach (string historyType in historyTypes)
            {
                if (counter == 1)
                    sbTabes.Append(string.Format(@"<div class=""tab selected first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, historyType));
                else
                    sbTabes.Append(string.Format(@"<div class=""tab first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, historyType));

                var historiesByType = histories.FindAll(x => x.Type == historyType);

                //get History Panels
                if (counter == 1)
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"" style=""display:block"">", counter));
                else
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"">", counter));

                foreach (History history in historiesByType)
                {
                    sbPanels.Append(string.Format(@"<h1>{0}</h1>{1}", history.Title, history.Details));
                }
                sbPanels.Append(string.Format(@"</div>"));
                counter++;
            }

            HistoryTabs.Text = sbTabes.ToString();

            HistoryPanes.Text = sbPanels.ToString();
        }
    }
}