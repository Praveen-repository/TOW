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
    public partial class UCViewRecognition : System.Web.UI.UserControl
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
            recognitionId = int.Parse(Helper.GetFromQueryString(Resources.QueryStringKeys.RecognitionId));

            //load Event details
            var viewRecognition = RecognitionManager.GetRecognitionById(recognitionId);
            ltrVRId.Text = viewRecognition.Id.ToString();
            ltrVRTitle.Text = viewRecognition.Title;
            ftbVRDetails.Text = viewRecognition.Details;

            //load event resources
            showResources.Resources = RecognitionManager.GetRecognitionResources(recognitionId);
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Helper.RedirectTo(Resources.PagePath.AdminAddUpdateRecognitions,
                new string[] { string.Format("{0}=edit", Resources.QueryStringKeys.Action), 
                    string.Format("{0}={1}", Resources.QueryStringKeys.RecognitionId, recognitionId) });
        }
    }
}