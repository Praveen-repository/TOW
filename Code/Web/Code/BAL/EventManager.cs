using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Web.Code.DAL;
using Web.Code.BO;
using System.Data;
using Web.BAL;
using System.Text;

namespace Web.Code.BAL
{
    public class EventManager : BaseManager
    {
        public static List<Category> GetEventCategories(int forEvent)
        {
            List<Category> categories = new List<Category>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = forEvent;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataCategories = MSSQLHandler.ExecuteReader("GetEventCategories", parameters);

            foreach (DataRow row in dataCategories.Rows)
            {
                categories.Add(new Category() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]) });
            }

            return categories;
        }

        public static List<Weight> GetEventWeights(int forEvent)
        {
            List<Weight> weights = new List<Weight>(100);


            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = forEvent;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataCategories = MSSQLHandler.ExecuteReader("GetEventWeights", parameters);

            foreach (DataRow row in dataCategories.Rows)
            {
                weights.Add(new Weight() { Id = Convert.ToInt32(row["Id"]), Class = Convert.ToString(row["Class"]) });
            }

            return weights;
        }

        public static List<Result> GetEventResults(int forEvent)
        {
            List<Result> results = new List<Result>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = forEvent;
            parameters[0] = parameter;
            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteReader("GetEventResults", parameters);

            foreach (DataRow row in dataResults.Rows)
            {
                results.Add(new Result() { Id = Convert.ToInt32(row["Id"]), Category = Convert.ToString(row["Category"]), Weight = Convert.ToString(row["Weight"]), Gold = Convert.ToString(row["Gold"]), Silver = Convert.ToString(row["Silver"]), Bronze = Convert.ToString(row["Bronze"]) });
            }

            return results;
        }

        public static List<Resource> GetEventResources(int forEvent)
        {
            List<Resource> resources = new List<Resource>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = forEvent;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteReader("GetEventResources", parameters);

            foreach (DataRow row in dataResults.Rows)
            {
                resources.Add(new Resource() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]), Title = Convert.ToString(row["Title"]), FileName = Convert.ToString(row["FileName"]) });
            }

            return resources;
        }

        public static List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>(100);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterEvents) != null)
            {
                events = (List<Event>)Helper.GetFromCache(Resources.CacheKeys.MasterEvents);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataEvents = MSSQLHandler.ExecuteReader("GetAllEvents", parameters);

                foreach (DataRow row in dataEvents.Rows)
                {
                    events.Add(new Event()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = Convert.ToString(row["Name"]),
                        StartDate = Convert.ToDateTime(row["StartDate"]),
                        EndDate = Convert.ToDateTime(row["EndDate"]),
                        Year = Convert.ToString(row["Year"]),
                        Details = Convert.ToString(row["Details"]),
                        Location = Convert.ToString(row["Location"]),
                        State = Convert.ToString(row["State"]),
                        Level = Convert.ToString(row["Level"])
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterEvents, events);
            }
            return events;
        }

        public static Event GetEventById(int id)
        {
            return GetAllEvents().First(x => x.Id == id);
        }

        public static int SaveEvent(Event eventToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[13];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = eventToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@EventName", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = eventToSave.Name;
            parameters[1] = parameter;
            parameter = new SqlParameter("@EventStartDate", System.Data.SqlDbType.DateTime);
            parameter.Value = eventToSave.StartDate;
            parameters[2] = parameter;
            parameter = new SqlParameter("@EventEndDate", System.Data.SqlDbType.DateTime);
            parameter.Value = eventToSave.EndDate;
            parameters[3] = parameter;
            parameter = new SqlParameter("@EventYear", System.Data.SqlDbType.VarChar, 10);
            parameter.Value = eventToSave.Year;
            parameters[4] = parameter;
            parameter = new SqlParameter("@EventDetails", System.Data.SqlDbType.NVarChar, 150);
            parameter.Value = eventToSave.Details;
            parameters[5] = parameter;
            parameter = new SqlParameter("@EventLocation", System.Data.SqlDbType.NVarChar, 150);
            parameter.Value = eventToSave.Location;
            parameters[6] = parameter;
            parameter = new SqlParameter("@EventState", System.Data.SqlDbType.NVarChar, 150);
            parameter.Value = eventToSave.State;
            parameters[7] = parameter;
            parameter = new SqlParameter("@EventCategories", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertEventCategoriesToXML(eventToSave.Categories);
            parameters[8] = parameter;
            parameter = new SqlParameter("@EventWeights", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertEventWeightsToXML(eventToSave.Weights);
            parameters[9] = parameter;
            parameter = new SqlParameter("@EventResults", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertEventResultsToXML(eventToSave.Results);
            parameters[10] = parameter;
            parameter = new SqlParameter("@EventResources", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertEventResourcesToXML(eventToSave.Resources);
            parameters[11] = parameter;
            parameter = new SqlParameter("@EventLevel", System.Data.SqlDbType.VarChar, 50);
            parameter.Value = eventToSave.Level;
            parameters[12] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveEvent", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterEvents);

            return result;

        }

        public static void DeleteEvent(int eventId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@EventId", System.Data.SqlDbType.Int);
            parameter.Value = eventId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteEvent", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterEvents);
        }

        private static string ConvertEventCategoriesToXML(List<Category> eventCategories)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Category category in eventCategories)
            {
                tempXML.Append(string.Format("<row Name='{0}' />", Helper.MakeXMLCompatible(category.Name)));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }

        private static string ConvertEventWeightsToXML(List<Weight> eventWeights)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Weight weight in eventWeights)
            {
                tempXML.Append(string.Format("<row Class='{0}' />", Helper.MakeXMLCompatible(weight.Class)));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }

        private static string ConvertEventResultsToXML(List<Result> eventResults)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Result result in eventResults)
            {
                tempXML.Append(string.Format("<row Category='{0}' Weight='{1}' Gold='{2}' Silver='{3}' Bronze='{4}' />", Helper.MakeXMLCompatible(result.Category), Helper.MakeXMLCompatible(result.Weight), Helper.MakeXMLCompatible(result.Gold), Helper.MakeXMLCompatible(result.Silver), Helper.MakeXMLCompatible(result.Bronze)));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }

        private static string ConvertEventResourcesToXML(List<Resource> eventResources)
        {
            StringBuilder tempXML = new StringBuilder();
            tempXML.Append("<root>");
            foreach (Resource resource in eventResources)
            {
                tempXML.Append(string.Format("<row Resource='{0}' />", resource.Id));
            }
            tempXML.Append("</root>");
            return tempXML.ToString();
        }
    }
}