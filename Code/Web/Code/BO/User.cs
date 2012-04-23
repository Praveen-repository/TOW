using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Code.BAL;

namespace Web.Code.BO
{
    public class User : BaseBO
    {
        public string Username;
        public string Password;
        public string Firstname;
        public string Lastname;
        public string Email;

        public List<Role> roles = new List<Role>(10);
        public List<Permission> Permissions = new List<Permission>(100);

    }

    public class Role : BaseBO
    {
        public string Name;
    }

    public class Permission : BaseBO
    {
        public string Pagename;
    }

    [Serializable]
    public class Page : BaseBO
    {
        public string PageName {get;set;}
        public int Parent { get; set; }
        public string Html { get; set; }
        public bool Active { get; set; }
        public string ParentHierarchy
        {
            get { return PagesManager.PageHierarchy(this.Id); }
        }
    }

}