using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code.BO
{
    [Serializable]
    public class Recognition : BaseBO
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public List<Resource> Resources { get; set; }

    }
}