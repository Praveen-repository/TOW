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
    public class RecognitionManager : BaseManager
    {
        public static List<Resource> GetRecognitionResources(int forRecognition)
        {
            List<Resource> resources = new List<Resource>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@RecognitionId", System.Data.SqlDbType.Int);
            parameter.Value = forRecognition;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteReader("GetRecognitionResources", parameters);

            foreach (DataRow row in dataResults.Rows)
            {
                resources.Add(new Resource() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]), Title = Convert.ToString(row["Title"]), FileName = Convert.ToString(row["FileName"]) });
            }

            return resources;
        }

        public static List<Recognition> GetAllRecognitions()
        {
            List<Recognition> Recognitions = new List<Recognition>(100);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterRecognitions) != null)
            {
                Recognitions = (List<Recognition>)Helper.GetFromCache(Resources.CacheKeys.MasterRecognitions);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataRecognition = MSSQLHandler.ExecuteReader("GetAllRecognitions", parameters);

                foreach (DataRow row in dataRecognition.Rows)
                {
                    Recognitions.Add(new Recognition()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Title = Convert.ToString(row["Title"]),
                        Details = Convert.ToString(row["Details"])
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterRecognitions, Recognitions);
            }
            return Recognitions;
        }

        public static Recognition GetRecognitionById(int id)
        {
            return GetAllRecognitions().First(x => x.Id == id);
        }

        public static int SaveRecognition(Recognition RecognitionToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[4];
            //add parameters
            parameter = new SqlParameter("@RecognitionId", System.Data.SqlDbType.Int);
            parameter.Value = RecognitionToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@RecognitionTitle", System.Data.SqlDbType.VarChar, 250);
            parameter.Value = RecognitionToSave.Title;
            parameters[1] = parameter;
            parameter = new SqlParameter("@RecognitionDetails", System.Data.SqlDbType.VarChar);
            parameter.Value = RecognitionToSave.Details;
            parameters[2] = parameter;
            parameter = new SqlParameter("@RecognitionResources", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertRecognitionResourcesToXML(RecognitionToSave.Resources);
            parameters[3] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveRecognition", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterRecognitions);

            return result;

        }

        public static void DeleteRecognition(int RecognitionId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@RecognitionId", System.Data.SqlDbType.Int);
            parameter.Value = RecognitionId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteRecognition", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterRecognitions);
        }

        private static string ConvertRecognitionResourcesToXML(List<Resource> gelleryResources)
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