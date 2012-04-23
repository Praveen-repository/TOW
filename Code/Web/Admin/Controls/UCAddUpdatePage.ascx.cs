using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCAddUpdatePage : System.Web.UI.UserControl
    {
        string action;
        int pageId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.PageId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            pageId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.PageId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);


            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewPageDetails();
                        break;
                    case "edit":
                        LoadPageDetails(pageId);
                        break;
                }
            }
        }

        protected List<string> ValidatePage(Code.BO.Page valToPage)
        {
            List<string> errorMsg = new List<string>(25);
            if (string.IsNullOrEmpty(valToPage.Html))
                errorMsg.Add(Resources.Messages.PageHTMLMandatory);
            //check event duplicate
            if ((valToPage.Id > 0 && PagesManager.GetAllPages().Exists(x => x.Id != valToPage.Id && x.PageName == valToPage.PageName)) || (!(valToPage.Id > 0) && PagesManager.GetAllPages().Exists(x => x.PageName == valToPage.PageName)))
                errorMsg.Add(Resources.Messages.EventNameDuplidate);

            if(valToPage.Id > 0 && PagesManager.IsHierarchyCircular(valToPage.Parent, valToPage.Id))
            { errorMsg.Add(Resources.Messages.PageCircularHierarcy); }

            return errorMsg;
        }



        protected void LoadNewPageDetails()
        {
            LoadParentDropDown();
        }

        protected void LoadParentDropDown()
        {
            ddMPAParent.DataTextField = "PageName";
            ddMPAParent.DataValueField = "Id";
            ddMPAParent.DataSource = PagesManager.GetAllPages();
            ddMPAParent.DataBind();
            ddMPAParent.Items.Add(new ListItem("Select Parent", "0"));
            ddMPAParent.SelectedValue = "0";
        }

        protected void LoadPageDetails(int pageId)
        {
            LoadParentDropDown();
            //load gallery details
            var viewPage = PagesManager.GetPageById(pageId);
            ltrMPAId.Text = viewPage.Id.ToString();
            txtMPAPageName.Text = viewPage.PageName;
            ftbDetails.Text = viewPage.Html;
            ddMPAParent.SelectedValue = viewPage.Parent.ToString();
            chkMPAActive.Checked = viewPage.Active;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                Code.BO.Page tempPage = new Code.BO.Page()
                {
                    Id = pageId,
                    PageName = txtMPAPageName.Text,
                    Parent = Helper.IntParse(ddMPAParent.SelectedValue),
                    Html = ftbDetails.Text,
                    Active = chkMPAActive.Checked
                };

                var errorList = ValidatePage(tempPage);

                if (errorList.Count == 0)
                {
                    PagesManager.SavePage(tempPage);
                    Helper.GoToMessagePage(string.Format("Page {0} saved successfully.", tempPage.PageName));

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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminManagePages, new string[] { "" });
        }
    }
}