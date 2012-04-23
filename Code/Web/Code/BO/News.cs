using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code.BO
{
    [Serializable]
    public class News : BaseBO
    {
        public string Heading { get; set; }
        public string Summary { get; set; }
        public DateTime Dated { get; set; }
        public string Details { get; set; }
        public string Location { get; set; }

        public List<Resource> Resources { get; set; }
    }

}