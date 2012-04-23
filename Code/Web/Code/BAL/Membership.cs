using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Web.Code.DAL;
using Web.BAL;
using Web.Code.BO;
using System.Data;

namespace Web.Code.BAL
{
    public class Membership : BaseManager
    {
        public static bool isAuthentic(string username, string password)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[3];

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            parameter = new SqlParameter("@Username", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = username;
            parameters[0] = parameter;
            parameter = new SqlParameter("@Password", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = password;
            parameters[1] = parameter;
            parameter = new SqlParameter("@Result", System.Data.SqlDbType.Int);
            parameter.Direction = System.Data.ParameterDirection.Output;
            parameters[2] = parameter;

            MSSQLHandler.ExecuteNonQuery("AuthenticateUser", parameters);

            return ((int)parameters[2].Value == 1);

        }

        public static User GetUserByUsername(string username)
        {
            User user = null;
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            parameter = new SqlParameter("@Username", System.Data.SqlDbType.VarChar, 150);
            parameter.Value = username;
            parameters[0] = parameter;

            var dataTable = MSSQLHandler.ExecuteReader("GetUserByUsername", parameters);

            foreach (DataRow row in dataTable.Rows) 
            {
                user = new User()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Username = Convert.ToString(row["Username"]),
                    Password = Convert.ToString(row["Password"]),
                    Firstname = Convert.ToString(row["Firstname"]),
                    Lastname = Convert.ToString(row["Lastname"]),
                    Email = Convert.ToString(row["Email"])
                };
            }

            return user;
        }

        public static User FillUserRoles(User user)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            parameter = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
            parameter.Value = user.Id;
            parameters[0] = parameter;

            var dataTable = MSSQLHandler.ExecuteReader("GetUserRolesByUserId", parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                user.roles.Add(new Role()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = Convert.ToString(row["Name"])
                });
            }
            return user;
        }

        public static User FillUserPermissions(User user)
        {
            SqlParameter parameter = null;
            SqlParameter[] parameters = new SqlParameter[1];

            MSSQLHandler.CurrentConnectionType = GetConnectionType();
            //add parameters
            parameter = new SqlParameter("@UserId", System.Data.SqlDbType.Int);
            parameter.Value = user.Id;
            parameters[0] = parameter;

            var dataTable = MSSQLHandler.ExecuteReader("GetUserPermissionsByUserId", parameters);


            foreach (DataRow row in dataTable.Rows)
            {
                user.Permissions.Add(new Permission()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Pagename = Convert.ToString(row["Pagename"])
                });
            }
            return user;
        }

        public static void SetWebLoggedInUser(string username)
        {
            var user = GetUserByUsername(username);
            
            if (user != null)
            {
                user = FillUserRoles(user);
                user = FillUserPermissions(user);
            }

            //set user login details
            Helper.AddToSession(Resources.SessionKeys.LoggedIn, true);
            Helper.AddToSession(Resources.SessionKeys.UserId, user.Id);
            Helper.AddToSession(Resources.SessionKeys.Username, user.Username);
            Helper.AddToSession(Resources.SessionKeys.ObjUser, user);
        }
    }
}