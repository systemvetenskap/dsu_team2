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
        public string phone { get; set; }
        public bool payed { get; set; }
        public List<Member> team { get; set; }
        public int teamid { get; set; }
        public DateTime startTime { set; get; }
        public Member()
        {
            team = new List<Member>();
        }
        
        public void CreateSession(bool staff) {
            if (staff)
            {
                Session["_staff"] = "1";
            }
            Session["_id"] = this.id;
            Session["_logged"] = "1";
            Session["_name"] = this.firstName + " " + this.lastName;
            Session["_golf_id"] = this.golf_id;
            Session["_hcp"] = this.hcp;
        }

        public HttpCookie CreateCookie(bool staff)
        {
            HttpCookie loginCookie = new HttpCookie("LoginCookie");
            loginCookie.Expires = DateTime.Now.AddDays(-1d);
            if (staff)
            {
                loginCookie["_staff"] = "1";
            }
            
            loginCookie["_id"] = this.id.ToString();
            loginCookie["_logged"] = "1";
            loginCookie["_name"] = this.firstName + " " + this.lastName;
            loginCookie["_golf_id"] = this.golf_id;
            loginCookie["_hcp"] = this.hcp.ToString();
            loginCookie["_gender"] = this.gender;
            loginCookie.Expires = DateTime.Now.AddDays(2d);


            return loginCookie;
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