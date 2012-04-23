using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Code.BO;
using System.Data.SqlClient;
using Web.Code.DAL;
using System.Data;
using Web.BAL;
using System.Text;

namespace Web.Code.BAL
{
    public class NewsManager : BaseManager
    {
        public static List<Resource> GetNewsResources(int forNews)
        {
            List<Resource> resources = new List<Resource>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@NewsId", System.Data.SqlDbType.Int);
            parameter.Value = forNews;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteReader("GetNewsResources", parameters);

            foreach (DataRow row in dataResults.Rows)
            {
                resources.Add(new Resource() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]), Title = Convert.ToString(row["Title"]), FileName = Convert.ToString(row["FileName"]) });
            }

            return resources;
        }

        public static List<News> GetAllNews()
        {
            List<News> news = new List<News>(100);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterNews) != null)
            {
                news = (List<News>)Helper.GetFromCache(Resources.CacheKeys.MasterNews);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataNews = MSSQLHandler.ExecuteReader("GetAllNews", parameters);

                foreach (DataRow row in dataNews.Rows)
                {
                    news.Add(new News()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Heading = Convert.ToString(row["Heading"]),
                        Summary = Convert.ToString(row["Summary"]),
                        Details = Convert.ToString(row["Details"]),
                        Location = Convert.ToString(row["Location"]),
                        Dated = Convert.ToDateTime(row["Dated"])
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterNews, news);
            }
            return news;
        }

        public static News GetNewsById(int id)
        {
            return GetAllNews().First(x => x.Id == id);
        }

        public static int SaveNews(News newsToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[7];
            //add parameters
            parameter = new SqlParameter("@NewsId", System.Data.SqlDbType.Int);
            parameter.Value = newsToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@NewsHeading", System.Data.SqlDbType.VarChar, 250);
            parameter.Value = newsToSave.Heading;
            parameters[1] = parameter;
            parameter = new SqlParameter("@NewsSummary", System.Data.SqlDbType.VarChar, 500);
            parameter.Value = newsToSave.Summary;
            parameters[2] = parameter;
            parameter = new SqlParameter("@NewsDetails", System.Data.SqlDbType.VarChar);
            parameter.Value = newsToSave.Details;
            parameters[3] = parameter;
            parameter = new SqlParameter("@NewsLocation", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = newsToSave.Location;
            parameters[4] = parameter;
            parameter = new SqlParameter("@NewsDated", System.Data.SqlDbType.DateTime);
            parameter.Value = newsToSave.Dated;
            parameters[5] = parameter;
            parameter = new SqlParameter("@NewsResources", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertNewsResourcesToXML(newsToSave.Resources);
            parameters[6] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveNews", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterNews);

            return result;

        }

        public static void DeleteNews(int newsId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@NewsId", System.Data.SqlDbType.Int);
            parameter.Value = newsId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteNews", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterNews);
        }

        private static string ConvertNewsResourcesToXML(List<Resource> gelleryResources)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Resource resource in gelleryResources)
            {
                tempXML.Append(string.Format("<row Resource='{0}' />", resource.Id));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }
    }
}