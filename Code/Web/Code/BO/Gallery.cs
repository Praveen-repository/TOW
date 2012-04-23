using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code.BO
{
    [Serializable]
    public class Gallery : BaseBO
    {
        public string Name { get; set; }

        public List<Resource> Resources { get; set; }
    }

}