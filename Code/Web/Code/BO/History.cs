using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code.BO
{
    [Serializable]
    public class History : BaseBO
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public string Type { get; set; }
    }
}