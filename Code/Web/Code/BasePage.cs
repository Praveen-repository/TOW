using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.BAL;
using System.Configuration;

namespace Web.Code
{

    abstract public class BasePage : System.Web.UI.Page
    {
        public abstract string Pagename();
        public abstract List<string> RoleAuthorised();
        public abstract bool AuthenticationRequired();

        protected bool Authentic()
        {
            if (AuthenticationRequired())
                return (Helper.GetFromSession(Resources.SessionKeys.LoggedIn) != null
                    && (bool)Helper.GetFromSession(Resources.SessionKeys.LoggedIn));
            else
                return true;
        }

        public void GotoLogin()
        {
            Helper.RedirectTo(Resources.PagePath.AdminLogin);
            //Helper.RedirectTo(ConfigurationManager.AppSettings[Resources.ConfigurationKeys.LoginPage]);
        }

        public void GotoError() { }

        public void GotoMessage() { }


        protected bool Authorised()
        {
            bool isAuthorised = false;
            if (AuthenticationRequired())
            {
                Code.BO.User user = Helper.GetFromSession(Resources.SessionKeys.ObjUser) as Code.BO.User;
                if (user != null)
                {
                    foreach (string role in RoleAuthorised())
                    {
                        isAuthorised = user.roles.Exists(x => x.Name == role);
                        if (isAuthorised)
                            break;
                    }
                }
            }
            else
                isAuthorised = true;
            return isAuthorised;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!Authentic())
                GotoLogin();

            if (!Authorised())
                GotoLogin();

            base.OnLoad(e);
        }
    }

}