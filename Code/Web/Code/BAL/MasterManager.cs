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
using System.IO;

namespace Web.Code.BAL
{
    public class MasterManager : BaseManager
    {
        public static List<Resource> GetAllResources()
        {
            List<Resource> resources = new List<Resource>(10000);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterResources) != null)
            {
                resources = (List<Resource>)Helper.GetFromCache(Resources.CacheKeys.MasterResources);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataResources = MSSQLHandler.ExecuteReader("GetAllResources", parameters);

                foreach (DataRow row in dataResources.Rows)
                {
                    resources.Add(new Resource() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]), Title = Convert.ToString(row["Title"]), FileName = Convert.ToString(row["FileName"]) });

                }

                Helper.AddToCache(Resources.CacheKeys.MasterResources, resources);
            }
            return resources;
        }

        public static Resource GetResourceById(int id)
        {
            List<Resource> resources = GetAllResources();
            return resources.First(x => x.Id == id);
        }

        public static int SaveResources(List<Resource> resources)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];

            //add parameters
            parameter = new SqlParameter("@Resources", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertResourcesToXML(resources);
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveResources", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterResources);

            return result;

        }

        private static string ConvertResourcesToXML(List<Resource> resources)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Resource resource in resources)
            {
                tempXML.Append(string.Format("<row Name='{0}' Title='{1}' Filename='{2}' />", Helper.MakeXMLCompatible(resource.Name), Helper.MakeXMLCompatible(resource.Title), Helper.MakeXMLCompatible(resource.FileName)));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }

        public static void DeleteResource(int resourceId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@ResourceId", System.Data.SqlDbType.Int);
            parameter.Value = resourceId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteResource", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterResources);
        }

        public static List<GameRule> GetRules()
        {
            List<GameRule> gRules = new List<GameRule>(20);
            int counter = 1;
            //get rules datasource
            string gRuleSourcePath = HttpContext.Current.Server.MapPath(Resources.PagePath.GameRules);
            if (Directory.Exists(gRuleSourcePath))
            {
                var gRuleFiles = Directory.GetFiles(gRuleSourcePath);
                foreach (string gRuleFile in gRuleFiles)
                {
                    gRules.Add( new GameRule() { Id = counter++, Link = Path.GetFileName(gRuleFile), Title = Path.GetFileNameWithoutExtension(gRuleFile) });
                }
            }
            return gRules;
        }
    }
}