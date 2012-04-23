using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Code.DAL;
using Web.BAL;

namespace Web.Code.BAL
{
    public class BaseManager
    {
        public static ConnectionType GetConnectionType() 
        {
            switch(Helper.GetCurrentUserType())
            {
                case Helper.UserType.Admin:
                    return ConnectionType.Admin;
                case Helper.UserType.Guest:
                    return ConnectionType.Guest;
                default:
                    return ConnectionType.Guest;
            }
        }
    }
}