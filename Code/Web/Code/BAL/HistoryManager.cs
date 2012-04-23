using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Code.BO;
using Web.BAL;
using System.Data.SqlClient;
using Web.Code.DAL;
using System.Data;

namespace Web.Code.BAL
{
    public class HistoryManager : BaseManager
    {
     
        public static List<History> GetAllHistories()
        {
            List<History> histories = new List<History>(20);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterHistories) != null)
            {
                histories = (List<History>)Helper.GetFromCache(Resources.CacheKeys.MasterHistories);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataHistories = MSSQLHandler.ExecuteReader("GetAllHistories", parameters);

                foreach (DataRow row in dataHistories.Rows)
                {
                    histories.Add(new History()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Title = Convert.ToString(row["Title"]),
                        Details = Convert.ToString(row["Details"]),
                        Type = Convert.ToString(row["Type"])
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterHistories, histories);
            }
            return histories;
        }

        public static History GetHistoryById(int id)
        {
            return GetAllHistories().First(x => x.Id == id);
        }

        public static int SaveHistory(History historyToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[4];
            //add parameters
            parameter = new SqlParameter("@HistoryId", System.Data.SqlDbType.Int);
            parameter.Value = historyToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@HistoryTitle", System.Data.SqlDbType.VarChar, 250);
            parameter.Value = historyToSave.Title;
            parameters[1] = parameter;
            parameter = new SqlParameter("@HistoryType", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = historyToSave.Type;
            parameters[2] = parameter;
            parameter = new SqlParameter("@HistoryDetails", System.Data.SqlDbType.VarChar);
            parameter.Value = historyToSave.Details;
            parameters[3] = parameter;
           

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveHistory", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterHistories);

            return result;

        }

        public static void DeleteHistory(int historytId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@HistoryId", System.Data.SqlDbType.Int);
            parameter.Value = historytId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteHistory", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterHistories);
        }
    }
}