using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning
{
    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }
        public int age_from { get; set; }
        public int age_to { get; set; }
    }
}