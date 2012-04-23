using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BO;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCAddUpdateRecognition : System.Web.UI.UserControl
    {
        string action;
        int recognitionId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //get request
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.Action) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);
            if (Helper.GetFromQueryString(Resources.QueryStringKeys.RecognitionId) == null)
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            action = Helper.GetFromQueryString(Resources.QueryStringKeys.Action).ToLower();
            recognitionId = Helper.IntParse(Helper.GetFromQueryString(Resources.QueryStringKeys.RecognitionId));

            if (action != "add" && action != "edit")
                Helper.GoToMessagePage(Resources.Messages.NoAction);

            if (!Page.IsPostBack)
            {
                switch (action)
                {
                    case "add":
                        LoadNewRecognitiontDetails();
                        break;
                    case "edit":
                        LoadRecognitionDetails(recognitionId);
                        break;
                }
            }
        }

        protected List<string> Validaterecognition(Recognition valTorecognition)
        {
            List<string> errorMsg = new List<string>(25);

            if (string.IsNullOrEmpty(valTorecognition.Details))
                errorMsg.Add(Resources.Messages.RecognitionDetailsMandatory);
            //check event duplicate
            if ((valTorecognition.Id > 0 && RecognitionManager.GetAllRecognitions().Exists(x => x.Id != valTorecognition.Id && x.Title == valTorecognition.Title)) || (!(valTorecognition.Id > 0) && RecognitionManager.GetAllRecognitions().Exists(x => x.Title == valTorecognition.Title)))
                errorMsg.Add(Resources.Messages.EventNameDuplidate);

            return errorMsg;
        }

        protected void LoadNewRecognitiontDetails()
        {
            SelectResources.SetSelectedResources(new List<Resource>());
        }

        protected void LoadRecognitionDetails(int recognitionId)
        {
            //load gallery details
            var viewrecognition = RecognitionManager.GetRecognitionById(recognitionId);
            ltrMRAId.Text = viewrecognition.Id.ToString();
            txtMRATitle.Text = viewrecognition.Title;
            ftbMRADetails.Text = viewrecognition.Details;
            SelectResources.SetSelectedResources(RecognitionManager.GetRecognitionResources(recognitionId));
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //get event values 
                Recognition tempRecognition = new Recognition()
                {
                    Id = recognitionId,
                    Title = txtMRATitle.Text,
                    Details = ftbMRADetails.Text
                };

                var errorList = Validaterecognition(tempRecognition);

                if (errorList.Count == 0)
                {
                    tempRecognition.Resources = SelectResources.GetSelectedResources();
                    RecognitionManager.SaveRecognition(tempRecognition);

                    Helper.GoToMessagePage(string.Format("Recognition {0} saved successfully.", tempRecognition.Title));
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
            Helper.RedirectTo(Resources.PagePath.AdminManageRecognitions, new string[] { "" });
        }
    }
}