using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GolfBokning
{
    public partial class unbook : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        public string SuggestionList = "";
        public string isPersonal = "F";
        //Gets booked players on specifik day and fills the table
        public void fillbookingtable(DateTime date)
        {
            foreach (HtmlTableRow tr in bookingtable.Rows)
            {
                foreach (HtmlTableCell td in tr.Cells)
                {
                    foreach (Control cr in td.Controls)
                    {
                        if (cr is Label)
                        {
                            Label lbl = (Label)cr;
                            lbl.Text = "";
                        }
                        else if (cr is HtmlGenericControl)
                        {
                            HtmlGenericControl gc = (HtmlGenericControl)cr;

                            gc.InnerText = "";
                            gc.Attributes["class"] = "players";

                        }
                    }
                }
            }

            if (!IsPostBack)
            {
                table_calender.SelectedDate = DateTime.Now;
            }
            clsBookings.clsBooking list = new clsBookings.clsBooking();
            List<Bookingcell> lista = new List<Bookingcell>();

            lista = list.getDataTable(date);

            foreach (Bookingcell bcell in lista)
            {
                string cell = "cell" + bcell.cellID;
                var temp = bookingtable.FindControl(cell);

                if (cell == temp.ID.ToString())
                {
                    Label lbl = (Label)temp.FindControl("lbl" + bcell.cellID) as Label;
                    if (bcell.hpc.ToString() == "")
                    {
                        lbl.Text = "<span class='hcp_val'>" + Environment.NewLine + bcell.hpc.ToString() + "</span>";
                    }
                    else
                    {
                        lbl.Text = "Hcp: <span class='hcp_val'>" + Environment.NewLine + bcell.hpc.ToString() + "</span>";
                    }

                    HtmlGenericControl p0 = (HtmlGenericControl)temp.FindControl("p1" + bcell.cellID);
                    HtmlGenericControl p1 = (HtmlGenericControl)temp.FindControl("p2" + bcell.cellID);
                    HtmlGenericControl p2 = (HtmlGenericControl)temp.FindControl("p3" + bcell.cellID);
                    HtmlGenericControl p3 = (HtmlGenericControl)temp.FindControl("p4" + bcell.cellID);
                    HtmlGenericControl[] arr = { p0, p1, p2, p3 };

                    for (int i = 0; i < bcell.players.Count; i++)
                    {
                        if (arr[i].InnerText != "M" || arr[i].InnerText != "F")
                        {

                            string gender = "";

                            if (bcell.players[i].gender == "Male")
                            {
                                gender = "M";

                            }
                            else
                            {
                                gender = "F";
                            }
                            if (i == 0)
                            {
                                p0.InnerText = gender;
                                if (gender == "M")
                                {
                                    p0.Attributes["class"] = "bookedm players";
                                }
                                else
                                {
                                    p0.Attributes["class"] = "bookedf players";
                                }
                            }
                            else if (i == 1)
                            {
                                p1.InnerText = gender;
                                if (gender == "M")
                                {
                                    p1.Attributes["class"] = "bookedm players";
                                }
                                else
                                {
                                    p1.Attributes["class"] = "bookedf players";
                                }
                            }
                            else if (i == 2)
                            {
                                p2.InnerText = gender;
                                if (gender == "M")
                                {
                                    p2.Attributes["class"] = "bookedm players";
                                }
                                else
                                {
                                    p2.Attributes["class"] = "bookedf players";
                                }
                            }
                            else if (i == 3)
                            {
                                p3.InnerText = gender;
                                if (gender == "M")
                                {
                                    p3.Attributes["class"] = "bookedm players";
                                }
                                else
                                {
                                    p3.Attributes["class"] = "bookedf players";
                                }
                            }
                        }
                    }
                }
            }
        }
        //public void emptybookingtable()
        //{
        //    foreach (TableCellCollection tc in bookingtable)
        //    {

        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];
            getQuerryExample();
            if (!string.IsNullOrEmpty(myCookie["_staff"] as string))
            {
                //makeBooking.Enabled = false;
                //makeBooking.Visible = false;
                isPersonal = "T";

            }
            else
            {
                isPersonal = "F";
            }
            if (!IsPostBack)
            {

                fillbookingtable(DateTime.Today);
                //table_calender.Visible = false;
                lblVisaDatum.Text += "<h2>" + table_calender.SelectedDate.ToShortDateString() + "</h2>";
            }

        }

        [System.Web.Services.WebMethod]
        public static string makeBoking(string aa, int id_member_cookie, string v, string extMem = "", bool isMany = false)
        {
            string[] arrMem = null;
            if (isMany == true)
            {
                arrMem = extMem.Split(';');
                //return extMem;
            }


            clsBookings.clsBooking clBok = new clsBookings.clsBooking();

            int membID = id_member_cookie;

            string golfId = clBok.getGolf_ID(membID);
            //clsBookings.clsBooking clBo = new clsBookings.clsBooking();
            //clBo.cancelBooking(v.ToString(), int.Parse(aa), "");
            //string tmp = table_calender.SelectedDate.ToShortDateString();
            JavaScriptSerializer mku = new JavaScriptSerializer();
            aa = aa.Replace("ContentPlaceHolder1_cell", "");
            string va = aa.Substring(0, 2) + ":" + aa.Substring(2, 2) + ":00";
            va = v.Substring(0, 10) + " " + va;
            string ans = "";
            if (isMany)
            {
                ans = clBok.makeBooking(DateTime.Parse(va), golfId, arrMem);
            }
            else
            {
                ans = clBok.makeBooking(DateTime.Parse(va), golfId);
            }
            clBok = new clsBookings.clsBooking();
            return "Bokningsnummer:" + ans + " på tiden " + va;
        }


        //protected void btn_calender_Click(object sender, ImageClickEventArgs e)
        //{
        //    if (table_calender.Visible == false)
        //    {
        //        table_calender.Visible = true;
        //    }
        //    else
        //    {
        //        table_calender.Visible = false;
        //    }
        //}

        protected void table_calender_SelectionChanged(object sender, EventArgs e)
        {

            fillbookingtable(table_calender.SelectedDate);
            lblVisaDatum.Text = "<h2>" + table_calender.SelectedDate.ToShortDateString() + "</h2>";
        }
        public void getQuerryExample()
        {

            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member  ORDER BY firstname";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {

                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            if (string.IsNullOrEmpty(SuggestionList))
                            {
                                SuggestionList += "\"" + reader["fnam"].ToString() + "\"";
                            }
                            else
                            {
                                SuggestionList += ", \"" + reader["fnam"].ToString() + "\"";
                            }

                        }
                    }
                }

            }
        }
         [System.Web.Services.WebMethod]
        public static string fillListbox(string startime)
        {
            string sql = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam, booked.id as fid from booking " +
  " INNER JOIN booked ON booked.id_booking = booking.id " +
  " INNER JOIN member on member.id = booked.id_member " +
  " WHERE starttime = '" + startime + "'";
            string elem = "";
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            elem += "<option value='" + reader["fid"] + "'>" + reader["fnam"] + "</option>";
                        }
                    }
                }
            }
            return elem;
        }
         [System.Web.Services.WebMethod]
         public static string deletePass(string id)
         {
             string sql = "DELETE FROM booked where id='" + id + "'";
             //string elem = "";
             using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
             {
                 using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                 {
                     connection.Open();
                     command.ExecuteNonQuery();
                 }
             }
             return "Succes";
         }
    }
}