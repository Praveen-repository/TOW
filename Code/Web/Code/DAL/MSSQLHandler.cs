using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using System.Data;

namespace Web.Code.DAL
{
    public enum ConnectionType { Admin, Guest }

    public class MSSQLHandler
    {
        public static string AdminConnectionString = ConfigurationManager.ConnectionStrings[Resources.ConfigurationKeys.AdminConnectionName].ConnectionString;
        public static string GuestConnectionString = ConfigurationManager.ConnectionStrings[Resources.ConfigurationKeys.GuestConnectionName].ConnectionString;

        public static ConnectionType CurrentConnectionType { get; set; }

        private static string GetConnectionString()
        {
            switch (CurrentConnectionType)
            {
                case ConnectionType.Admin:
                    return AdminConnectionString;
                default:
                    return GuestConnectionString;
            }
        }

        public static DataTable ExecuteReader(string spName, SqlParameter[] parameters)
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = spName;
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.ConnectionString = GetConnectionString();
                    //add parameters
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);

                    //open connection
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    var reader = cmd.ExecuteReader();
                    data.Load(reader);
                    return data;
                }
            }
        }

        public static int ExecuteNonQuery(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = spName;
                    cmd.Connection = conn;
                    conn.ConnectionString = GetConnectionString();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //add parameters
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                    //open connection
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    return cmd.ExecuteNonQuery();

                }
            }
        }

        public static object ExecuteScalar(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = spName;
                    cmd.Connection = conn;
                    conn.ConnectionString = GetConnectionString();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //add parameters
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                    //open connection
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    return cmd.ExecuteScalar();
                }
            }
        }

        public static XmlReader ExecuteXMLReader(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = spName;
                    cmd.Connection = conn;
                    conn.ConnectionString = GetConnectionString();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //add parameters
                    foreach (SqlParameter parameter in parameters)
                        cmd.Parameters.Add(parameter);
                    //open connection
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    return cmd.ExecuteXmlReader();
                }
            }
        }

    }
}