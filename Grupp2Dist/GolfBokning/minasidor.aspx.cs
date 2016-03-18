﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace GolfBokning
{
    public partial class minasidor : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonBokaTid.Click += ButtonBokaTid_Click;
            ButtonAllTournaments.Click += ButtonAllTournaments_Click;
            myCookie = Request.Cookies["LoginCookie"];
            LabelHidden.Text = myCookie["_id"].ToString();

            //string city = (string)(Session["City"]);

            Tbody1.InnerHtml = getmemberstournament(Convert.ToInt16(myCookie["_id"]));
            if (!IsPostBack)
            {
                fillListboxJournal(Convert.ToInt16(myCookie["_id"]));
                txtbox_datejournal.Text = NpgsqlTypes.NpgsqlDate.Today.ToString();
                
            }
            if (hasBlog(Convert.ToInt16(myCookie["_id"])) == false)
            {
                lblEntries.Visible = false;
                libox_journalentrys.Visible = false;
            }
            
            // Check if session variable _logged is 1
            if (myCookie["_id"].ToString() == "1")
            {
                int id = Convert.ToInt16(myCookie["_id"]);
                //fillTable(id);
                Debug.WriteLine(id.ToString());
                Member chosenMember = GetMember(id);
                //SelectGender(RadioButtonGender, chosenMember.gender.ToString());
                //GetAllCategories(DropDownCategories, "SELECT id, description FROM category");

            }

        }
        [System.Web.Services.WebMethod]
        public static string RemoveTournamentBooking(int id_tournament, int id_member)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";
            int id = 0;
            string sql = "DELETE FROM tournament_member WHERE id_tournament = @id_tournament AND id_member = @id_member RETURNING id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);
            cmd.Parameters.AddWithValue("@id_member", id_member);

            conn.Open();
            id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                output = "ok";
            }

            conn.Close();

            return output;
        }

        private void ButtonAllTournaments_Click(object sender, EventArgs e)
        {
            Response.Redirect("tournamentmembadd.aspx");
        }

        private void ButtonBokaTid_Click(object sender, EventArgs e)
        {
            Response.Redirect("booking.aspx");
        }

       

        [System.Web.Services.WebMethod]
        public static string removeBooking(string aa, int id_member_cookie)
        {
            int v = id_member_cookie;
            clsBookings.clsBooking clBo = new clsBookings.clsBooking();
            clBo.cancelBooking(v.ToString(), int.Parse(aa), "");
            return "Tog bort bokningsnummer:" + aa;
        }

        [System.Web.Services.WebMethod]
        public static string fillTable(int id_member_cookie)
        {
            int id = id_member_cookie;
            clsBookings.clsBooking clBok = new clsBookings.clsBooking();
            DataTable dt = clBok.getMembersBoking(id);
            if (dt.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                string htRo = "";
                foreach (DataRow dtR in dt.Rows)
                {
                    htRo += "<tr>";
                    htRo += "<td>" + dtR["tim"].ToString().Substring(10, 6) + "</td>";
                    htRo += "<td>" + dtR["dat"].ToString().Substring(0, 10) + "</td>";
                    htRo += "<td> <button class='btn btn-danger' onclick='GetMessage(" + '"' + dtR["boid"].ToString() + '"' + "); return false;'>Avboka tid </button></td>";
                    htRo += "</tr>";

                }
                //tbTable.InnerHtml = htRo;
                return htRo;
            }
        }
        
        public Member GetMember(int membId)
        {
            Member member = new Member();
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            conn.Open();
            string sqlstring = "SELECT * FROM member WHERE id='" + membId + "'ORDER BY id";
            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                member.id = Convert.ToInt32(dr["id"]);
                member.firstName = dr["firstname"].ToString();
                member.lastName = dr["lastname"].ToString();
                member.address = dr["address"].ToString();
                member.zipcode = dr["zipcode"].ToString();
                member.place = dr["place"].ToString();
                member.email = dr["email"].ToString();
                member.gender = dr["gender"].ToString();
                member.birth = dr["ssn"].ToString();
                member.golf_id = dr["golf_id"].ToString();
                if (!DBNull.Value.Equals(dr["hcp"]))
                {
                    member.hcp = Convert.ToDouble(dr["hcp"]);
                }
                member.id_category = Convert.ToInt16(dr["id_category"]);
            }
            conn.Close();
            return member;
        }
        
        public string getmemberstournament(int id)
        {
            string sql = "SELECT to_char(tournament_member.starttime, 'HH24:MI:SS'), tournament.name, tournament.date, tournament.id FROM tournament_member INNER JOIN member on tournament_member.id_member = member.id INNER JOIN tournament ON tournament_member.id_tournament = tournament.id where tournament_member.id_member = '" + id + "' and tournament.date > Now() ORDER BY tournament.date";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);
            }
            string tablerows = "";

            foreach (DataRow dr in dt.Rows)
            {
                tablerows += "<tr>";
                if (dr["to_char"].ToString() != "")
                {
                    tablerows += "<td>" + dr["to_char"].ToString().Substring(0, 5) + "</td>";
                }
                else
                {
                    tablerows += "<td>TBA</td>";
                }
                
                tablerows += "<td>" + dr["date"].ToString().Substring(0, 10) + "</td>";
                tablerows += "<td>" +dr["name"].ToString() + "</td>";
                tablerows += "<td><button class='btn btn-danger' onclick='RemoveTournament("+dr["id"].ToString()+"); return false;'>Avboka</button></td>";
                tablerows += "</tr>";
            }
            return tablerows;
            
        }

        

        public void insertjournalentry(int id)
        {
            if (txtbox_datejournal.Text=="ÅÅÅÅ-MM-DD")
            {
                txtbox_datejournal.Text = NpgsqlTypes.NpgsqlDate.Today.ToString();
            }

            string sql = "INSERT INTO member_blog (title, text, id_medlem, date) VALUES (@title, @text, @id, @date)";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@title", txtbox_head.Text.ToString());
            cmd.Parameters.AddWithValue("@text", txtbox_content.Text.ToString());
            cmd.Parameters.AddWithValue("@id", Convert.ToInt16(id));
            cmd.Parameters.AddWithValue("@date", txtbox_datejournal.Text.ToString());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void fillListboxJournal(int id)
        {
            string sql = "SELECT title, id, date FROM member_blog where id_medlem = " + id;
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);

            }
            
            foreach (DataRow row in dt.Rows)
            {               
                    ListItem lt = new ListItem();
                    lt.Text = row["title"].ToString() + ", Datum " + row["date"].ToString().Substring(0, 10);
                    lt.Value = row["id"].ToString(); ;
                    libox_journalentrys.Items.Add(lt);
                
            }
        }

        public void getJournalEntry(int id)
        {
            string sql = "SELECT title, text, date FROM member_blog where id = " + id;
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);

            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);

            }

            foreach (DataRow dr in dt.Rows)
            {

                txtbox_content.Text = dr["text"].ToString();
                txtbox_head.Text = dr["title"].ToString();
                //txtbox_datejournal.Text = dr["title"].ToString();
                //txtbox_datejournal.Text = NpgsqlTypes.NpgsqlDate.Today.ToString();
                txtbox_datejournal.Text = "ÅÅÅÅ-MM-DD";
            }
        }

        private bool hasBlog(int id_memb)
        {
            bool check = false;
            string queryString = "SELECT member_blog.id_medlem FROM public.member_blog WHERE id_medlem=" + id_memb + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            check = false;
                        }
                        else
                        {
                            check = true;
                        }
                    }
                }
            }
            return check;
        }

        private bool InsertOrUpdate(int id_memb, string title, string date)
        {
            bool check = false;
            string queryString = "SELECT id_medlem, date, id, title, text FROM public.member_blog WHERE id_medlem = " + id_memb + " AND title ='" + title + "' AND date = '" + date + "'";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                        }
                    }
                }
            }
            return check;
        }
        public void updateResults(string title, string text, string date, int id_memb)
        {

            string queryString = "UPDATE member_blog SET title='" + title + "', text='" + text + "', date='" + date + "' WHERE id=" + libox_journalentrys.SelectedValue + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void btnjournalentry_Click(object sender, EventArgs e)
        {
            if (txtbox_datejournal.Text == "ÅÅÅÅ-MM-DD")
            {
                txtbox_datejournal.Text = NpgsqlTypes.NpgsqlDate.Today.ToString();
            }
            lblEntries.Visible = true;
            libox_journalentrys.Visible = true;
            myCookie = Request.Cookies["LoginCookie"];
            if (InsertOrUpdate(Convert.ToInt16(myCookie["_id"]), txtbox_head.Text, txtbox_datejournal.Text) == true)
            {
                insertjournalentry(Convert.ToInt16(myCookie["_id"]));
            }
            else
            {
                updateResults(txtbox_head.Text, txtbox_content.Text, txtbox_datejournal.Text, Convert.ToInt16(myCookie["_id"]));
            }
            
            libox_journalentrys.Items.Clear();
            fillListboxJournal(Convert.ToInt16(myCookie["_id"]));
        }

        protected void libox_journalentrys_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            getJournalEntry(Convert.ToInt16(libox_journalentrys.SelectedValue));
        }
    }
}