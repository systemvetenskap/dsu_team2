using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GolfBokning
{
    public partial class headSite : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["_logged"] as string))
            {
                // Check if session variable _logged is 1
                if (Session["_logged"].ToString() == "1")
                {
                    LoginLogout.Text = "Logga ut";
                    LoginLogout.Attributes["class"] = "logoutClick";
                }
            }
            else
            {
                LoginLogout.Text = "Logga in";
                LoginLogout.Attributes["href"] = "loginform.aspx";
            }
        }
    }
}