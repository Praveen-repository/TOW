using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Code.BO;
using Web.BAL;
using System.Data.SqlClient;
using Web.Code.DAL;
using System.Data;
using System.Text;

namespace Web.Code.BAL
{
    public class PagesManager : BaseManager
    {
        public static List<Page> GetAllPages()
        {
            List<Page> page = new List<Page>(100);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterPages) != null)
            {
                page = (List<Page>)Helper.GetFromCache(Resources.CacheKeys.MasterPages);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataPage = MSSQLHandler.ExecuteReader("GetAllPages", parameters);

                foreach (DataRow row in dataPage.Rows)
                {
                    page.Add(new Page()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        PageName = Convert.ToString(row["PageName"]),
                        Parent = Convert.ToInt32(row["Parent"]),
                        Html = Convert.ToString(row["Html"]),
                        Active = Convert.ToBoolean(row["Active"])
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterPages, page);
            }
            return page;
        }

        public static Page GetPageById(int id)
        {
            return GetAllPages().First(x => x.Id == id);
        }

        public static int SavePage(Page pageToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[5];
            //add parameters
            parameter = new SqlParameter("@PageId", System.Data.SqlDbType.Int);
            parameter.Value = pageToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@PagePagename", System.Data.SqlDbType.VarChar, 250);
            parameter.Value = pageToSave.PageName;
            parameters[1] = parameter;
            parameter = new SqlParameter("@PageParent", System.Data.SqlDbType.Int);
            parameter.Value = pageToSave.Parent;
            parameters[2] = parameter;
            parameter = new SqlParameter("@PageHtml", System.Data.SqlDbType.VarChar);
            parameter.Value = pageToSave.Html;
            parameters[3] = parameter;
            parameter = new SqlParameter("@PageActive", System.Data.SqlDbType.Bit);
            parameter.Value = pageToSave.Active;
            parameters[4] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SavePage", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterPages);

            return result;

        }

        public static string PageHierarchy(int pageId)
        {
            StringBuilder sb = new StringBuilder(100);
            Page tempPage = GetPageById(pageId);
            //if (tempPage.Parent > 0)
            //    sb.Append(string.Format("{0}", GetPageById(tempPage.Parent).PageName));
            int counter = 0;
            while (tempPage.Parent > 0)
            {
                tempPage = GetPageById(tempPage.Parent);
                if (counter == 0)
                {
                    sb.Insert(0, string.Format("{0} ", tempPage.PageName));
                    counter++;
                }
                else
                    sb.Insert(0, string.Format("{0} > ", tempPage.PageName));
            }

            return sb.ToString();
        }

        public static bool IsPageParent(int pageId)
        {
            return GetAllPages().Exists(x => x.Parent == pageId);
        }

        public static bool IsHierarchyCircular(int parentId, int pageId)
        {
            List<int> parentList = new List<int>(10);

            Page tempPage = GetPageById(parentId);

            while (tempPage.Parent > 0)
            {
                parentList.Add(tempPage.Parent);
                tempPage = GetPageById(tempPage.Parent);
            }


            return parentList.Contains(pageId);
        }

        public static void DeletePage(int PageId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@PagesId", System.Data.SqlDbType.Int);
            parameter.Value = PageId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeletePage", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterPages);
        }
    }
}