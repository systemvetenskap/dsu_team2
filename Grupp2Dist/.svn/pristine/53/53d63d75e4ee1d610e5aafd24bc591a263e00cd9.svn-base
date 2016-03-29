using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GolfBokning
{
    public partial class about : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBookings.clsBooking clBok = new clsBookings.clsBooking();
            clBok.getDataTable(DateTime.Now);
            //GridView1.DataSource = clBok.getDataTable(DateTime.Now);
            //GridView1.DataBind();
        }
    }
}