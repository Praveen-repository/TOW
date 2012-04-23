using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Web.Code.BAL;
using Web.Code.BO;

namespace Web.Controls
{
    public partial class UCRecognitions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sbTabes = new StringBuilder();
            StringBuilder sbPanels = new StringBuilder();
            int counter = 1;
            //Get recog list
            var recognitions = RecognitionManager.GetAllRecognitions();

            //get recog tabs
            foreach (Recognition recognition in recognitions)
            {

                //get recog Panels
                if (counter == 1)
                {
                    sbTabes.Append(string.Format(@"<div class=""tab selected first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, recognition.Title));
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"" style=""display:block"">
                                                            <h1>{1}</h1>
                                                            <p>{2}</p>
                                                    </div>", counter, recognition.Title, recognition.Details));
                }
                else
                {
                    sbTabes.Append(string.Format(@"<div class=""tab first"" id=""tab_menu_{0}"">
                                                    <div class=""link"">{1}</div>
                                                    <div class=""arrow""></div>
                                               </div>", counter, recognition.Title));
                    sbPanels.Append(string.Format(@"<div class=""tabcontent"" id=""tab_content_{0}"">
                                                            <h1>{1}</h1>
                                                            <p>{2}</p>
                                                    </div>", counter, recognition.Title, recognition.Details));
                }
                counter++;
            }

            RecognitionTabs.Text = sbTabes.ToString();
            RecognitionPanes.Text = sbPanels.ToString();
        }
    }
}