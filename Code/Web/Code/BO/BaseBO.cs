using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Code.BO
{
    [Serializable]
    public class BaseBO
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class GameRule : BaseBO
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }
}