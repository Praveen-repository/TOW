using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code;

namespace Web.Admin
{
    public partial class ManageGalleries : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public override string Pagename()
        {
            return "ManageGalleries";
        }

        public override List<string> RoleAuthorised()
        {
            return new List<string> { "Admin" };
        }

        public override bool AuthenticationRequired()
        {
            return true;
        }
    }
}