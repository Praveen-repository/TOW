using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Configuration;
using System.Text;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;

namespace Web.BAL
{
    public class Helper
    {

        public enum UserType { Admin, Guest };

        public class Configurations
        {
            public class Keys
            {
                public class Database
                {
                    public static string ConnectionName = "";
                }
            }
        }

        public static UserType GetCurrentUserType()
        {
            return UserType.Admin;
        }

        public static void AddToSession(string key, object value)
        {
            if (HttpContext.Current.Session[key] == null)
                HttpContext.Current.Session.Add(key, value);
            else
                HttpContext.Current.Session[key] = value;
        }

        public static object GetFromSession(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static void AddToCache(string key, object value)
        {
            if (HttpContext.Current.Cache[key] == null)
                HttpContext.Current.Cache.Add(key,
                    value,
                    new CacheDependency(HttpContext.Current.Server.MapPath("/web.config")),
                    Cache.NoAbsoluteExpiration,
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Normal,
                    null);
            else
                HttpContext.Current.Cache[key] = value;
        }

        public static void ClearCache(string key)
        {
            if (HttpContext.Current.Cache[key] != null)
                HttpContext.Current.Cache.Remove(key);
        }

        public static object GetFromCache(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static string GetFromQueryString(string key)
        {
            return HttpContext.Current.Request.QueryString[key];
        }

        public static void GoToMessagePage(string message)
        {
            HttpContext.Current.Response.Redirect(string.Format("{0}?{1}={2}",
                ConfigurationManager.AppSettings[Resources.ConfigurationKeys.AdminGenericMessagePage],
                Resources.QueryStringKeys.Message,
                message), true);
        }

        public static void GotoLoginPage() { }

        public static DateTime GetBritishDate(string date)
        {
            string[] datePart = date.Split(new char[] { '-' });
            return new DateTime(int.Parse(datePart[2]), int.Parse(datePart[1]), int.Parse(datePart[0]));
        }

        public static string FormatDateToMMDDYYYY(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public static string FormatMessageToUL(List<string> messages)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (string message in messages)
                sb.Append(string.Format("<li>{0}</li>", message));
            sb.Append("</ul>");

            return sb.ToString();
        }

        public static void RedirectTo(string gotoURL, string[] qsParam)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("1=1");
            foreach (string param in qsParam)
                sb.Append(string.Format("&{0}", param));

            HttpContext.Current.Response.Redirect(string.Format("{0}?{1}", gotoURL, sb.ToString()));
        }

        public static void RedirectTo(string gotoURL)
        {
            RedirectTo(gotoURL, new string[] { });
        }

        public static int IntParse(string value)
        {
            int i = 0;
            int.TryParse(value, out i);
            return i;
        }

        public static int GridPageSize()
        {
            return 10;
        }


        public static int CalculatePageStartIndex(int recordCount, int pageIndex, int pageSize)
        {
            int tempStartIndex = pageIndex * pageSize;
            if (tempStartIndex > recordCount)
                tempStartIndex = recordCount - pageSize;

            if (tempStartIndex < 0)
                return 0;
            else
                return tempStartIndex;
        }

        public static int CalculatePageSize(int recordCount, int pageIndex, int pageSize)
        {
            int tempStartIndex = CalculatePageStartIndex(recordCount, pageIndex, pageSize);
            if (tempStartIndex + pageSize > recordCount)
                return recordCount - tempStartIndex;

            return pageSize;
        }

        //Return thumbnail callback
        private static bool ThumbnailCallback()
        {
            return true;
        }

        public static void CreateThumbnail(string imageFile)
        {
            System.Drawing.Image thumbnail;
            System.Drawing.Image.GetThumbnailImageAbort callback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            string thumbnailFile = Path.Combine(Path.GetDirectoryName(imageFile), string.Format("th_{0}", Path.GetFileName(imageFile)));

            System.Drawing.Image imagesize = System.Drawing.Image.FromFile(imageFile);
            Bitmap bitmapNew = new Bitmap(imagesize);
            if (imagesize.Width < imagesize.Height)
            {
                thumbnail = bitmapNew.GetThumbnailImage(150 * imagesize.Width / imagesize.Height, 150, callback, IntPtr.Zero);
            }
            else
            {
                thumbnail = bitmapNew.GetThumbnailImage(150, imagesize.Height * 150 / imagesize.Width, callback, IntPtr.Zero);
            }
            //Save image in TumbnailImage folder
            thumbnail.Save(thumbnailFile, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static void UploadFiles(HttpFileCollection files)
        {
            for (int counter = 0; counter < files.Count; counter++)
            {
                UploadFile(files[counter]);
            }
        }

        public static string UploaderFolderPath()
        {
            return HttpContext.Current.Server.MapPath(Resources.PagePath.UploadPath);
        }

        public static string UploadFile(HttpPostedFile file)
        {
            //get the folder to store the file
            //create folder name todays date
            string filename = string.Empty;
            string filePath = string.Empty;

            if (!string.IsNullOrEmpty(file.FileName))
            {
                filename = string.Format("{0}_{1}{2}"
                    , Path.GetFileNameWithoutExtension(file.FileName)
                    , DateTime.Now.Ticks
                    , Path.GetExtension(file.FileName));

                string folderName = string.Format("{0}_{1}_{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                string folderPath = Path.Combine(HttpContext.Current.Server.MapPath(Resources.PagePath.UploadPath), folderName);

                Directory.CreateDirectory(folderPath);

                filePath = Path.Combine(folderPath, filename);

                if (!File.Exists(filePath))
                {
                    file.SaveAs(filePath);
                    CreateThumbnail(filePath);
                }
            }
            return filePath;
        }

        public static string MakeXMLCompatible(string data) {
            return data.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        } 

    }
}