using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BAL;
using System.Text;

namespace Web.Controls
{
    public partial class UCRulesTicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(500);

            var gameRules = MasterManager.GetRules();

            foreach (var gameRule in gameRules)
            {
                sb.Append(string.Format(@"<li><div class=""doc""><a href=""/Rules/{0}"" target=""_blank"">{1}</a></div></li>", gameRule.Link, gameRule.Title));
            }

            RulesList.Text = sb.ToString();
        }
    }
}