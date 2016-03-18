using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfBokning
{
    public class Member : System.Web.UI.Page
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string place { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string birth { get; set; }
        public double hcp { get; set; }
        public string golf_id { get; set; }
        public int id_category { get; set; }
        public string password { get; set; }
        public string ssn { get; set; }
        public bool staff { get; set; }

        public void CreateSession(bool staff) {
            if (staff)
            {
                Session["_staff"] = "1";
            }
            Session["_id"] = this.id;
            Session["_logged"] = "1";
        }

        public void DestroySession()
        {
            Session.Abandon();
        }

        public override string ToString()
        {
            return firstName + " " + lastName + " (" + golf_id + ")"; 
        }
     
    }
}