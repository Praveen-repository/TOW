using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.BAL;
using Web.Code.BO;
using System.IO;
using Web.Code.BAL;

namespace Web.Admin.Controls
{
    public partial class UCAddUpdateResource : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //validate the files and upload to path[create day wise folder and store files in that]

            var uploadedFiles = Request.Files;
            List<Resource> resources = new List<Resource>(100);

            if (uploadedFiles.Count > 0)
            {
                for (int counter = 0; counter < uploadedFiles.Count; counter++)
                {
                    var filePath = Helper.UploadFile(uploadedFiles[counter]);
                    
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        var tempPath = Path.GetDirectoryName(filePath).Split(new string[] { @"\" }, StringSplitOptions.None);
                        resources.Add(new Resource()
                        {
                            Name = Path.GetFileName(filePath),
                            FileName = Path.Combine(tempPath[tempPath.Length - 1], Path.GetFileName(filePath)),
                            Title = txtMRAResourceTitle.Text
                        });
                    }

                }
                // make database entry
                MasterManager.SaveResources(resources);

                Helper.GoToMessagePage(string.Format("Resources saved successfully."));

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}