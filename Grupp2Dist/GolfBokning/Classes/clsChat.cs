using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning.Classes
{
    public class clsChat
    {
        List<string> liMessages { set; get; }

    }
    class chatMessages
    {
      public  string messages { set; get; }
      public DateTime sent { set; get; }
      public Member sentBy { set; get; }
      public string membName { set; get; }
    }
   
}
