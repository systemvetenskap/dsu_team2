using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
namespace GolfBokning.clsBookings
{

    public class clsBooking
    {

        //SELECT makebooking('10309_44', '2016-03-01 12:00:00'::TIMESTAMP, '1','aa');
        //NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        string dbconn = Properties.Settings.Default.dbConnectionString;
        public void makeBooking(DateTime starttime, string membersID)
        {
            booked(starttime, membersID);
            //if (memberCanbook(starttime, membersID) == false)
            //{
            //    //Can not book 
            //    return;
            //}
            //else if(laneIsFree(starttime) < 1)
            //{
            //    //There are no free place on the course
            //    return;
            //}
            //else
            //{
            //    //Then can we book 

            //}
        }
        public void makeBooking(DateTime starttime, string membersID, string[] bookMembers, bool book = true, bool memb = true)
        {
            if (book)
            {
                booked(starttime, membersID, membersID);
            }

            for (int i = 0; i < bookMembers.Length; i++)
            {
                booked(starttime, bookMembers[i], membersID);
            }
        }
        /// <summary>
        /// Check if a member already are booked on the day if it can book it returns true and not it returns false
        /// 
        /// </summary>
        /// <param name="membersID"></param>
        /// <returns></returns>
        private bool memberCanbook(DateTime bookingDay, string membersID, int antToBok = 1)
        {
            NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
            string sql = "SELECT count(*) FROM booking" +
            " inner join booked on booking.id = booked.id_booking " +
            " inner join member on member.id = booked.id_member " +
            " WHERE member.golf_id=@membID and (DATE(starttime) = @dat";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("membId", membersID);
                cmd.Parameters.AddWithValue("dat", bookingDay.Date);

                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (int.Parse(Convert.ToString(dr[0])) > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// Chek how many places that are free on the course
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private int laneIsFree(DateTime start)
        {
            int sumLeft = 0;
            NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
            string sql = " SELECT (maxbook - count(*)) AS leftBokning FROM booking " +
                            " inner join booked on booking.id = booked.id_booking " +
                            " inner join member on member.id = booked.id_member " +
                            " inner join opening on opening.id = booking.id_opening " +
                            " WHERE  (starttime) = @dat " +
                            " group by maxbook; ";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("dat", start.Date);

                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (int.TryParse(Convert.ToString(dr[0]), out sumLeft))
                        {
                            return 0;
                        }
                    }
                    else sumLeft = 4;
                }
            }
            return sumLeft;
        }
        private bool laneIsOpen(DateTime starttime)
        {



            return true;
        }
        /// <summary>
        /// Here we can book  
        /// </summary>
        /// <returns></returns>
        private bool insertBookingToDatabase()
        {
            DataTable dt = new DataTable();
            for (int i = 5; i <= 19; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            for (int i = 0; i <= 5; i++)
            {
                dt.Rows.Add();
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                for (int z = 0; z <= 5; i++)
                {
                    dt.Rows[z][i] = z + "0";
                }
            }

            return true;
        }

        protected string booked(DateTime start, string memberID, string bookedBy = null)
        {

            NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
            string sql = "SELECT makebooking('@membId', '@sta::TIMESTAMP', '@boby','aa')";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
            {
                cmd.Parameters.AddWithValue("dat", start);
                cmd.Parameters.AddWithValue("membId", memberID);
                cmd.Parameters.AddWithValue("membId", memberID);

                using (NpgsqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return Convert.ToString(dr[0]);
                    }

                }
            }
            return "Something went wrong";
        }
        public string cancelBooking(string membersID, int bokingID, string delete, bool deleteAll = false)
        {
            string sql; // = "DELETE FROM booked where bookedby=@boby and id_booking=@idbo";
            if (deleteAll)
            {
                sql = "DELETE FROM booked where bookedby=@boby and id=@idbo";
            }
            else
            {
                sql = "DELETE FROM booked where id_member=@boby and id=@idbo";
            
            }
            try
            {
                NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
                cnn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@boby", membersID);
                cmd.Parameters.AddWithValue("@idbo", bokingID);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return "";
            }
            catch (NpgsqlException ex)
            {
                Debug.WriteLine(ex.ToString());
                return ex.Message;
            }
           
        }

        //Gets all bookings for a specifik time and stores them in a list for bookingstable
        public List<Bookingcell> getDataTable(DateTime starttime)
        {

            DataTable dt2 = getMemberForSpecificDay(starttime); //HAve all members booked on specific day
            List<Bookingcell> liBok = new List<Bookingcell>();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                bool exist = false;
                for (int z = 0; z < liBok.Count; z++)
                {
                    if (dt2.Rows[i]["stime"].ToString() == liBok[z].ToString())
                    {
                        Member mem = new Member();
                        mem.id = int.Parse(dt2.Rows[i]["id_member"].ToString());
                        mem.gender = dt2.Rows[i]["gender"].ToString();
                        mem.hcp = double.Parse(dt2.Rows[i]["hcp"].ToString());
                        liBok[z].players.Add(mem);
                        liBok[z].hpc += double.Parse(dt2.Rows[i]["hcp"].ToString());
                        exist = true;
                    }
                }
                if (!exist)
                {
                    GolfBokning.Bookingcell bc = new GolfBokning.Bookingcell();
                    Member mem = new Member();
                    mem.id = int.Parse(dt2.Rows[i]["id_member"].ToString());
                    mem.gender = dt2.Rows[i]["gender"].ToString();
                    mem.hcp = double.Parse(dt2.Rows[i]["hcp"].ToString());
                    bc.players.Add(mem);
                    bc.hpc = double.Parse(dt2.Rows[i]["hcp"].ToString());
                    bc.cellID = dt2.Rows[i]["stime"].ToString();
                    liBok.Add(bc);
                    
                    exist = true;
                }
            }
            return liBok;
        }
        private DataTable getMemberForSpecificDay(DateTime startTime)
        {
            DataTable dt = new DataTable();
            NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
            cnn.Open();
            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter("SELECT id_booking, starttime, id_member, hcp, gender, starttime, SUBSTRING((REPLACE(CAST(starttime::time as VARCHAR), ':', '')), 0, 5) AS stime, starttime::date as startdate FROM booking_view WHERE starttime::date = @da", cnn))
            {
                adp.SelectCommand.Parameters.AddWithValue("@da", startTime.Date);
                adp.Fill(dt);
            }
            return dt;
        }
        private DataTable getDatatable(int row, int col)
        {
            DataTable dt = new DataTable();
            for (int i = 5; i <= 19; i++)
            {
                dt.Columns.Add(i.ToString());
            }
            for (int i = 0; i <= 5; i++)
            {
                dt.Rows.Add();
            }
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    for (int z = 0; z <= 5; z++)
            //    {
            //        Bookingcell aa = new Bookingcell();                  
            //        dt.Rows[z][i] = aa;
            //    }
            //}
            return dt;
        }
        public DataTable getMembersBoking(int membID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT starttime::time AS tim, starttime::date as dat, booked.id as boid FROM booked  " +
" INNER JOIN booking on booking.id = booked.id_booking " +
" WHERE booked.id_member = @mem order by starttime";
//" WHERE starttime > now() and booked.id_member = @mem order by starttime";
            NpgsqlConnection cnn = new NpgsqlConnection(dbconn);
            cnn.Open();
            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, cnn))
            {
                adp.SelectCommand.Parameters.AddWithValue("@mem", membID);
                adp.Fill(dt);
            }
            return dt;
        }

    }
}
/*
<script>
$(document).ready(function() {
    $("tr").each(function() {
        $(this).children("td").each(function() {
            var temp = $(this).attr("ID");
            var temp2 = temp.replace(/cell/g, "lbl");
            var temp3 = temp.replace(/cell/g, "");
            var count = 0;
            $(this).children("span").attr("ID", temp2);
            $(this).children("p").each(function() {
                var newID = "p" + count++ + temp3;
                if (!$(this).hasClass("time")) {
                    $(this).attr("ID", newID);
                }
            });
            count = 0;
        });
    });
});
</script>
 */