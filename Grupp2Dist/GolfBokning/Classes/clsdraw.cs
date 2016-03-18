using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning
{
    public class clsdraw
    {
        public Member memid { get; set; }
        public int teamid { get; set; }
        public DateTime starttime { get; set; }
        public List<Member> participants { get; set; }
        public List<List<Member>> teams { get; set;}
        
        public clsdraw()
        {
            participants = new List<Member>();
            teams = new List<List<Member>>();
        }
        
        
    }
     
}