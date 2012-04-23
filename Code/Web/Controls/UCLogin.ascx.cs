using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web.Controls
{

    public partial class UCLogin : System.Web.UI.UserControl
    {
        public class LoginSuccessEventArg : EventArgs
        {
            public User user;
        }

        public class LoginFailEventArg : EventArgs
        {
            public string ErrorMessage;
        }

        public delegate void LoginSuccess(object sender, LoginSuccessEventArg e);
        public delegate void LoginFail(object sender, LoginFailEventArg e);


        public event LoginSuccess LoginSuccessfull;
        public event LoginFail LoginFailed;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Membership.isAuthentic(txtUsername.Text, txtPassword.Text))
                {
                    Membership.SetWebLoggedInUser(txtUsername.Text);
                    OnLoginSuccessfull(new LoginSuccessEventArg() { user = Membership.GetUserByUsername(txtUsername.Text) });
                }
                else
                {

                    OnLoginFailed(new LoginFailEventArg() { ErrorMessage = "Incorrect username/password" });
                    lblMsg.Text = Resources.Messages.LoginFailed;
                }
            }
        }

        //events
        protected virtual void OnLoginSuccessfull(LoginSuccessEventArg e)
        {
            if (LoginSuccessfull != null)
            {
                //Invokes the delegates.
                LoginSuccessfull(this, e);
            }
        }

        protected virtual void OnLoginFailed(LoginFailEventArg e)
        {
            if (LoginFailed != null)
            {
                //Invokes the delegates.
                LoginFailed(this, e);
            }
        }
    }
}
