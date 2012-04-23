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
    public class GalleryManager : BaseManager
    {
        public static List<Resource> GetGalleryResources(int forGallery)
        {
            List<Resource> resources = new List<Resource>(100);

            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@GalleryId", System.Data.SqlDbType.Int);
            parameter.Value = forGallery;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteReader("GetGalleryResources", parameters);

            foreach (DataRow row in dataResults.Rows)
            {
                resources.Add(new Resource() { Id = Convert.ToInt32(row["Id"]), Name = Convert.ToString(row["Name"]), Title = Convert.ToString(row["Title"]), FileName = Convert.ToString(row["FileName"]) });
            }

            return resources;
        }

        public static List<Gallery> GetAllGalleries()
        {
            List<Gallery> galleries = new List<Gallery>(100);

            if (Helper.GetFromCache(Resources.CacheKeys.MasterGalleries) != null)
            {
                galleries = (List<Gallery>)Helper.GetFromCache(Resources.CacheKeys.MasterGalleries);
            }
            else
            {
                SqlParameter[] parameters = new SqlParameter[0];

                MSSQLHandler.CurrentConnectionType = GetConnectionType();
                //add parameters
                var dataGalleries = MSSQLHandler.ExecuteReader("GetAllGalleries", parameters);

                foreach (DataRow row in dataGalleries.Rows)
                {
                    galleries.Add(new Gallery()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = Convert.ToString(row["Name"]),
                    });
                }

                Helper.AddToCache(Resources.CacheKeys.MasterGalleries, galleries);
            }
            return galleries;
        }

        public static Gallery GetGalleryById(int id)
        {
            return GetAllGalleries().First(x => x.Id == id);
        }

        public static int SaveGallery(Gallery galleryToSave)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[3];
            //add parameters
            parameter = new SqlParameter("@GalleryId", System.Data.SqlDbType.Int);
            parameter.Value = galleryToSave.Id;
            parameters[0] = parameter;
            parameter = new SqlParameter("@GalleryName", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = galleryToSave.Name;
            parameters[1] = parameter;
            parameter = new SqlParameter("@GalleryResources", System.Data.SqlDbType.NVarChar);
            parameter.Value = ConvertGalleryResourcesToXML(galleryToSave.Resources);
            parameters[2] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var result = MSSQLHandler.ExecuteNonQuery("SaveGallery", parameters);

            //update cache for events
            Helper.ClearCache(Resources.CacheKeys.MasterGalleries);

            return result;

        }

        public static void DeleteGallery(int gallerytId)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];
            //add parameters
            parameter = new SqlParameter("@GalleryId", System.Data.SqlDbType.Int);
            parameter.Value = gallerytId;
            parameters[0] = parameter;

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            var dataResults = MSSQLHandler.ExecuteNonQuery("DeleteGallery", parameters);

            Helper.ClearCache(Resources.CacheKeys.MasterGalleries);
        }

        private static string ConvertGalleryResourcesToXML(List<Resource> gelleryResources)
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