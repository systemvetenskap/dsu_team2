using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning
{
    public class Bookingcell
    {
        public List<Member> players { get; set; }
        public Member player { get; set; }
        public double hpc { get; set; }
        public DateTime starttime { get; set; }
        public string cellID {get; set;}
        public bool isClosed { get; set; }
        public Bookingcell()
        {
            players = new List<Member>();        
        }
        public override string ToString()
        {
            return cellID.ToString();
        }
    }
}