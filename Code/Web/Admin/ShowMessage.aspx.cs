using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;

namespace Web.Admin
{
    public partial class ShowMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //show message....
            lblShowMessage.Text = (string)Helper.GetFromQueryString(Resources.QueryStringKeys.Message);
        }
    }
}