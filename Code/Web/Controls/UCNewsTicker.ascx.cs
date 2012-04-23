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
    public partial class UCNewsTicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            //Get news list
            var newses = NewsManager.GetAllNews();

            foreach (var news in newses)
            {
                sb.Append(string.Format(@"
                <li>
                    <div class=""thumbnail"">
                        <img src=""/Styles/images/News.png"">
                    </div>
                    <div class=""info"">
                        <a href=""NewsDetails.aspx?_nid={0}"">{1}</a>
                        <span class=""cat"">[{2}], [{3}]</span>
                        <span class=""cat"">{4}</span>
                    </div>
                    <div class=""clear"">
                    </div>
                </li>
                ", news.Id, news.Heading, news.Dated, news.Location, string.Join("", news.Summary.Take(100).ToArray())));
            }

            NewsList.Text = sb.ToString();
        }
    }
}