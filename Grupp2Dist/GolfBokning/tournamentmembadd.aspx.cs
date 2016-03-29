using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Npgsql;

namespace GolfBokning
{
    public partial class tournamentmembadd : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        List<string> listCount = new List<string>();
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            RadioButtonListClass.SelectedIndexChanged += new EventHandler(RadioButtonListClass_SelectedIndexChanged);
            RadioButtonListClass.AutoPostBack = true;

            CountTournaments();

            if (RadioButtonListClass.Items.Count == 0)
            {
                GetClasses();
            }

            if (!IsPostBack)
            {
                GetTournaments(1);
                PanelTeam.Visible = false;
            }

            myCookie = Request.Cookies["LoginCookie"];
            PanelBox.Visible = true;
            PanelBox.Height = 250;
            LabelHiddenId.Text = myCookie["_id"].ToString();
            LabelHiddenId.CssClass = "hidden_id";
        }

        protected void CountTournaments()
        {
            string sql = "SELECT id FROM tournament ORDER BY id";
            NpgsqlConnection conn2 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn2);

            conn2.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                CountNbr(Convert.ToInt16(dr["id"]));
            }

            conn2.Close();
        }

        protected void CountNbr(int id)
        {
            string sqlNbr = "SELECT count(*) as count FROM tournament_member WHERE id_tournament = @id_tournament";
            int count = 0;
            NpgsqlConnection conn3 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmdNbr = new NpgsqlCommand(sqlNbr, conn3);
            cmdNbr.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id));

            conn3.Open();
            NpgsqlDataReader dr = cmdNbr.ExecuteReader();

            while (dr.Read())
            {
                count = Convert.ToInt16(dr["count"]);
            }

            conn3.Close();
            listCount.Add(id.ToString() + ":" + count.ToString());
        }

        private void RadioButtonListClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTournaments(Convert.ToInt16(RadioButtonListClass.SelectedValue));
        }

        [System.Web.Services.WebMethod]
        public static string JoinTournament(string id_tournament, string id_member)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";

            string sql = "INSERT INTO tournament_member (id_tournament, id_member) VALUES (@id_tournament, @id_member) RETURNING id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
            cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(id_member));

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                output = "OK";
            }
            conn.Close();

            return output;
        }

        [System.Web.Services.WebMethod]
        public static string JoinTeamTournament(string id_tournament, string member, string teamname)
        {
            string[] teamMembers = member.Split(';');


            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string sql = "SELECT count(*) FROM team where name = '" + teamname + "'";
            NpgsqlCommand cmd; // = new NpgsqlCommand(sql, conn);
            conn.Open();
            bool nameNotExist = true;
            int namAdd = 0;
            while (nameNotExist)
            {
                if (namAdd == 0)
                {
                    sql = "SELECT count(*) FROM team where name = '" + teamname + "'";
                }
                else
                {
                    sql = "SELECT count(*) FROM team where name = '" + teamname + " " + namAdd.ToString() + "'";
                }
                cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                int ant = 0;
                if (dr.Read())
                {
                    ant = int.Parse(Convert.ToString(dr[0]));
                }
                if (ant == 0)
                {
                   teamname = teamname + " " + namAdd.ToString();
                    nameNotExist = false;
                }
                else
                {
                    namAdd += 1;                    
                }
                dr.Close();
            }

            //for (int i = 0; i < 1500; i++ )
            //{
            //    NpgsqlDataReader dr = cmd.ExecuteReader();
            //    int ant = 0;
            //    if (dr.Read())
            //    {
            //        ant = int.Parse(Convert.ToString(dr[0]));
            //    }
            //}
           
           
            string output = "";
             sql = "INSERT INTO TEAM (name, id_tournament) VALUES (@name, @id_tournament) RETURNING id";
             cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
            cmd.Parameters.AddWithValue("@name", teamname);
            int Teamid = (int)cmd.ExecuteScalar();
            if (Teamid != 0)
            {
                output = "OK";
            }
            else
            {
                conn.Close();
                return "False";
            }
            int boBy = 0;
            foreach (string item in teamMembers)
            {
                if (item.Length > 0)
                {
                    int id_member = 0;
                    if (int.TryParse(item, out id_member))
                    {
                        boBy = id_member;
                    }
                    else
                    {
                        id_member = getMembersID(item);
                    }
                    if (id_member > 0)
                    {
                        sql = "INSERT INTO tournament_member (id_tournament, id_member, id_team, bookedby) VALUES (@id_tournament, @id_member, @id_team, @bob) RETURNING id";
                        cmd = new NpgsqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
                        cmd.Parameters.AddWithValue("@id_member", id_member);
                        cmd.Parameters.AddWithValue("@id_team", Convert.ToInt16(Teamid));
                        cmd.Parameters.AddWithValue("@bob", boBy);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            conn.Close();
            return output;
        }
        [System.Web.Services.WebMethod]
        private static int getMembersID(string golf_id)
        {
            string sql = "SELECT id FROM member where golf_id ='" + golf_id +"'";

            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return int.Parse(Convert.ToString(dr[0]));
                        }
                        else
                        {
                            return 0;
                        }
                    }

                }
            }



        }

        [System.Web.Services.WebMethod]
        public static string CheckJoinTournament(string id_tournament, string id_member)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";

            string sql = "SELECT * FROM tournament_member WHERE id_tournament = @id_tournament AND id_member = @id_member";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
            cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(id_member));

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                output = "error " + id_tournament.ToString();
            }
            conn.Close();
            return output;
        }




        [System.Web.Services.WebMethod]
        public static string getGolfer(string idGolf)
        {
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam, golf_id FROM member where golf_id = @gid";
            string ret = "FALSE";
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {

                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {

                    connection.Open();
                    command.Parameters.AddWithValue("@gid", idGolf);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return "'<option value='" + (string)reader[1] + "'>" + (string)reader[0] + "</option>"; // (string)reader[0];                            
                        }
                        else
                        {
                            return "FALSE";
                        }

                    }

                    // connection.Close();
                }

            }
        }

        [System.Web.Services.WebMethod]
        public static string CheckJoinTournamentFull(int id_tournament, int nbr)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";
            int nbr_competitors = 0;

            string sql = "SELECT count(*) as count FROM tournament_member WHERE id_tournament = @id_tournament";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                nbr_competitors = Convert.ToInt16(dr["count"]);
            }

            if (nbr_competitors >= nbr)
            {
                output = "error " + id_tournament.ToString() + ";" + nbr_competitors.ToString();
            }

            conn.Close();
            return output;
        }

        protected void GetTournaments(int whatClass)
        {
            myCookie = Request.Cookies["LoginCookie"];
            string checkClass = whatClass.ToString();
            if (checkClass != "")
            {
                // 1 = Blandad 2 = Singel 3 = Lagspel
                string sql = "SELECT * FROM tournament WHERE id_class = " + whatClass + " AND date > Now()";
                // SELECT t.id, t.name, t.description, t.hcp_req, t.nbr_competitors, t.nbr_holes, t.id_class,t.entry_open,t.entry_close, t.publish_startlist, t.date, t.id_contact, t.starttime,t.endtime, t.id_lane, tm.id, tm.id_tournament, tm.id_member, tm.id_class, tm.starttime, tm.id_team, tm.points, count(tm.id) as count FROM tournament t INNER JOIN tournament_member tm ON t.id = tm.id_tournament WHERE t.id_class = 1 GROUP BY t.id, t.name, t.description, t.hcp_req, t.nbr_competitors, t.nbr_holes, t.id_class,t.entry_open,t.entry_close, t.publish_startlist, t.date, t.id_contact, t.starttime,t.endtime, t.id_lane, tm.id, tm.id_tournament, tm.id_member, tm.id_class, tm.starttime, tm.id_team, tm.points
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                conn.Open();

                NpgsqlDataReader dr = cmd.ExecuteReader();

                Table table = new Table();
                table.Attributes["class"] = "table table-bordered";
                TableHeaderRow thr = new TableHeaderRow();

                TableHeaderCell thcName = new TableHeaderCell();
                thcName.Text = "Namn";

                TableHeaderCell thcDate = new TableHeaderCell();
                thcDate.Text = "Datum";

                TableHeaderCell thcStarttime = new TableHeaderCell();
                thcStarttime.Text = "Starttid";

                TableHeaderCell thcHcp = new TableHeaderCell();
                thcHcp.Text = "HCP min";

                TableHeaderCell thcNbr = new TableHeaderCell();
                thcNbr.Text = "Platser kvar";

                TableHeaderCell thcJoin = new TableHeaderCell();
                thcJoin.Text = "Anmäl";


                thr.Cells.Add(thcName);
                thr.Cells.Add(thcDate);
                thr.Cells.Add(thcStarttime);
                thr.Cells.Add(thcHcp);
                thr.Cells.Add(thcNbr);
                thr.Cells.Add(thcJoin);

                table.Rows.Add(thr);

                while (dr.Read())
                {
                    TableRow tr = new TableRow();

                    TableCell tcName = new TableCell();
                    tcName.Text = dr["name"].ToString(); ;

                    TableCell tcDate = new TableCell();
                    string date = dr["date"].ToString();
                    date = date.Replace(" 00:00:00", "");
                    tcDate.Text = date;

                    TableCell tcStarttime = new TableCell();
                    string starttime = dr["starttime"].ToString();
                    starttime = starttime.Replace("0001-01-01 ", "");
                    starttime = starttime.Remove(starttime.Length - 3);
                    tcStarttime.Text = starttime;

                    TableCell tcHcp = new TableCell();
                    tcHcp.Text = dr["hcp_req"].ToString();
                    double hcp_rq = Convert.ToDouble(dr["hcp_req"]);


                    TableCell tcNbr = new TableCell();


                    foreach (string s in listCount)
                    {
                        string[] split = s.Split(':');
                        int id_tournament = Convert.ToInt16(split[0]);
                        int nbr = Convert.ToInt16(split[1]);

                        if (id_tournament == Convert.ToInt16(dr["id"]))
                        {
                            int count = Convert.ToInt16(dr["nbr_competitors"]) - nbr;
                            tcNbr.Text = count.ToString();
                        }
                    }

                    TableCell tcJoin = new TableCell();

                    double hcp = Convert.ToDouble(myCookie["_hcp"]);

                    if (whatClass == 3) //Team compeition 
                    {
                        PanelTeam.Visible = true;
                        if (hcp > hcp_rq)
                        {
                            tcJoin.Text = "<button data-nbr='" + dr["nbr_competitors"].ToString() + "' data='" + dr["id"].ToString() + "' class='btn btn-default btn-dis' disabled>För högt HCP</button>";
                        }
                        else
                        {
                            tcJoin.Text = "<button class='tbn btn btn-default " + dr["id"].ToString() + "' data-nbr='" + dr["nbr_competitors"].ToString() + "' data='" + dr["id"].ToString() + "' onclick='JoinTeam(" + dr["id"].ToString().Trim() + "); return false;'>Anmäl</button>";
                        }
                    }
                    else
                    {
                        PanelTeam.Visible = false;
                        if (hcp > hcp_rq)
                        {
                            tcJoin.Text = "<button data-nbr='" + dr["nbr_competitors"].ToString() + "' data='" + dr["id"].ToString() + "' class='btn btn-default btn-dis' disabled>För högt HCP</button>";
                        }
                        else
                        {
                            tcJoin.Text = "<button class='btn btn-default " + dr["id"].ToString() + "' data-nbr='" + dr["nbr_competitors"].ToString() + "' data-gender='" + dr["tournament_gender"].ToString() + "' data='" + dr["id"].ToString() + "' onclick='Join(" + dr["id"].ToString() + "); return false;'>Anmäl</button>";
                        }
                    }


                    tr.Cells.Add(tcName);
                    tr.Cells.Add(tcDate);
                    tr.Cells.Add(tcStarttime);
                    tr.Cells.Add(tcHcp);
                    tr.Cells.Add(tcNbr);
                    tr.Cells.Add(tcJoin);

                    table.Rows.Add(tr);
                }
                PanelTournaments.Controls.Add(table);
            }
            conn.Close();
        }

        protected void GetClasses()
        {
            string sql = "SELECT * FROM tournament_class ORDER BY id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem rb = new ListItem();
                rb.Text = dr["description"].ToString();
                rb.Value = dr["id"].ToString();
                RadioButtonListClass.Items.Add(rb);
            }

            conn.Close();
            RadioButtonListClass.SelectedIndex = 0;
        }

    }
}