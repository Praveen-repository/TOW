using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code;
using Web.BAL;

namespace Web.Admin
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucLogin.LoginSuccessfull += new Web.Controls.UCLogin.LoginSuccess(ucLogin_LoginSuccessfull);
            ucLogin.LoginFailed += new Web.Controls.UCLogin.LoginFail(ucLogin_LoginFailed);
        }

        void ucLogin_LoginFailed(object sender, Web.Controls.UCLogin.LoginFailEventArg e)
        {

        }

        void ucLogin_LoginSuccessfull(object sender, Web.Controls.UCLogin.LoginSuccessEventArg e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminDashboard);
        }

        public override string Pagename()
        {
            return "Login";
        }

        public override List<string> RoleAuthorised()
        {
            List<string> roles = new List<string>();
            roles.Add(Resources.Roles.Guest);
            return roles;
        }

        public override bool AuthenticationRequired()
        {
            return false;
        }
    }
}