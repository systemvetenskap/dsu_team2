using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GolfBokning
{
    public partial class addResults : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        List<Classes.Holes> listan = new List<Classes.Holes>();
        Member memb = new Member();
        Lanes lane = new Lanes();
        Classes.Holes hole1 = new Classes.Holes();
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                getTournaments();
            }
            
            for (int i = 1; i < 19; i++)
            {
                listan.Add(getHole(i));
            }
        }

        public void getTournaments()
        {
            string queryString = "SELECT id, name FROM tournament;";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Classes.clsTournament tour = new Classes.clsTournament();
                            tour.eventName = reader["name"].ToString();
                            tour.id = (int)reader["id"];

                            ListItem li = new ListItem();
                            li.Value = tour.id.ToString();
                            li.Text = tour.ToString();
                            ListBox2.Items.Add(li);
                        }
                    }
                }
            }
        }

        protected void getAllContestants(ListBox lb)
        {
            string queryString = "SELECT member.id, golf_id, lastname, firstname, hcp, gender FROM tournament_member, member WHERE member.id = tournament_member.id_member AND id_tournament = " + ListBox2.SelectedValue + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader dr = command.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            memb = new Member();
                            memb.firstName = dr["firstname"].ToString();
                            memb.lastName = dr["lastname"].ToString();
                            memb.golf_id = dr["golf_id"].ToString();
                            memb.id = (int)dr["id"];
                            memb.hcp = Convert.ToDouble(dr["hcp"]);
                            memb.gender = dr["gender"].ToString();

                            ListItem li = new ListItem();
                            li.Value = memb.id.ToString();
                            li.Text = memb.ToString();
                            lb.Items.Add(li);
                        }
                    }
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
                memb.hcp = getHcp();
                memb.gender = getGender();
                //Klumpig import av slag per hål
                List<int> allaSlag = new List<int>();
                allaSlag.Add(Convert.ToInt32(TextBox1.Text));
                allaSlag.Add(Convert.ToInt32(TextBox2.Text));
                allaSlag.Add(Convert.ToInt32(TextBox3.Text));
                allaSlag.Add(Convert.ToInt32(TextBox4.Text));
                allaSlag.Add(Convert.ToInt32(TextBox5.Text));
                allaSlag.Add(Convert.ToInt32(TextBox6.Text));
                allaSlag.Add(Convert.ToInt32(TextBox7.Text));
                allaSlag.Add(Convert.ToInt32(TextBox8.Text));
                allaSlag.Add(Convert.ToInt32(TextBox9.Text));
                allaSlag.Add(Convert.ToInt32(TextBox10.Text));
                allaSlag.Add(Convert.ToInt32(TextBox11.Text));
                allaSlag.Add(Convert.ToInt32(TextBox12.Text));
                allaSlag.Add(Convert.ToInt32(TextBox13.Text));
                allaSlag.Add(Convert.ToInt32(TextBox14.Text));
                allaSlag.Add(Convert.ToInt32(TextBox15.Text));
                allaSlag.Add(Convert.ToInt32(TextBox16.Text));
                allaSlag.Add(Convert.ToInt32(TextBox17.Text));
                allaSlag.Add(Convert.ToInt32(TextBox18.Text));

                int playedHcp = 0;

                if (memb.hcp > 36)
                {
                    getLaneTee();
                    if (memb.gender == "Female")
                    {
                        playedHcp = Convert.ToInt32(memb.hcp + 5);
                    }
                    else
                    {
                        if (lane.tee == "Gul 57")
                        {
                            playedHcp = Convert.ToInt32(memb.hcp + 4);
                        }
                        else if (lane.tee == "Blå 54")
                        {
                            playedHcp = Convert.ToInt32(memb.hcp + 1);
                        }
                        else
                        {
                            playedHcp = Convert.ToInt32(memb.hcp - 2);
                        }
                    }
                }
                else
                {
                    getLaneTee();
                    double playHandicap = ((memb.hcp * lane.courseSlope) / 113) + (lane.scratch - 72);
                    playedHcp = Convert.ToInt32(playHandicap);
                }
                setHcp(playedHcp);
                setPoints(allaSlag);
                lblConfirm.Visible = true;
                lblConfirm.Text = "Rundan är registrerad";
        }

        //få ut netto och räkna poäng
        private void setPoints(List<int> allaSlag)
        {
            listan.Sort();
            int points = 0;
            int total = 0;
            int totalSlag = 0;
            int IdHolder = getTounamentMembId();
            for (int i = 0; i < allaSlag.Count; i++)
            {
                int slag = allaSlag[i];

                int netto = slag - listan[i].erhallna;
                if ((netto - listan[i].par) >= 2)
                {
                    points = 0;
                }
                else
                {
                    points = listan[i].par - netto + 2;
                }
                if (InsertOrUpdate(i + 1) == true)
                {
                    insertResults(IdHolder, i + 1, slag, points, netto);
                }
                else
                {
                    updateResults(IdHolder, i + 1, slag, points, netto);
                }
                total = total + points;
                totalSlag = totalSlag + slag;
            }
            
            insertTotalPoints(total);
            insertTotalSlag(totalSlag);
        }

        //Räkna ut och placera ut erhållna slag
        private void setHcp(int playedHcp)
        {
            if (playedHcp < 0)
            {
                playedHcp = playedHcp * (-1);
            }
                for (int i = 0; i < playedHcp; i++)
                {
                    if (i > 17)
                    {
                        if (i > 35)
                        {
                            if (i > 53)
                            {
                                listan[i - 54].erhallna++;
                            }
                            else
                            {
                                listan[i - 36].erhallna++;
                            }
                        }
                        else
                        {
                            listan[i - 18].erhallna++;
                        }
                    }
                    else
                    {
                        listan[i].erhallna++;
                    }
                }
        }
        private bool InsertOrUpdate(int hole_id)
        {
            bool check = false;
            string queryString = "SELECT id_tournament_member FROM results WHERE id_tournament_member = " + getTounamentMembId() + " AND id_hole=" + hole_id + ";";

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

        public Classes.Holes getHole(int hIndex)
        {

            string queryString = "SELECT id, hole, par, hcp FROM holes WHERE hcp = " + hIndex + " Order by hcp;";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            Classes.Holes hole = new Classes.Holes();
                            hole.holeIndex = (int)reader["hole"];
                            hole.hcpIndex = (int)reader["hcp"];
                            hole.par = (int)reader["par"];
                            hole.erhallna = 0;
                            return hole;
                        }
                    }
                }
                return hole1;

            }
           
        }

        public void getLaneTee()
        {

            string queryString = "SELECT id, tee, gender, scratch, slope FROM lane WHERE tee = '" + rbtLstTee.SelectedValue.ToString() + "' AND gender = '" + memb.gender + "'";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {

                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            lane.laneID = (int)reader["id"];
                            lane.courseSlope = Convert.ToInt32(reader["slope"]);
                            lane.scratch = Convert.ToDouble(reader["scratch"]);
                            lane.tee = reader["tee"].ToString();
                        }
                    }
                }
            }
        }
        private void getScoreForHole()
        {

            string sql = "SELECT strokes FROM public.results WHERE id_tournament_member = " + getTounamentMembId() + ";";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string lblhcptemp = "TextBox" + (i + 1).ToString();
                TextBox tc = (TextBox)resultsTable.Rows[i].Cells[1].FindControl(lblhcptemp);
                tc.Text = dr["strokes"].ToString();
            }
            checkIfConfirmed();
        }
        private void checkIfConfirmed()
        {
            string sql = "SELECT confirmed FROM public.results WHERE id_tournament_member = " + getTounamentMembId() + " AND confirmed = true;";

            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);
            }
            if (dt.Rows.Count < 18)
            {
                lblConfirm.Text = "Ännu inte konfirmerad";
            }
            else
            {
                lblConfirm.Text = "Rundan är registrerad";
            }
            lblConfirm.Visible = true;
        }

        public int getTounamentMembId()
        {
            Classes.clsTournament tour = new Classes.clsTournament();
            string queryString = "SELECT id FROM tournament_member WHERE id_member = '" + ListBox1.SelectedValue.ToString() + "' AND id_tournament = '" + ListBox2.SelectedValue + "';";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tour.idTournamentMemb = (int)reader["id"];
                        }
                    }
                }
            }
            return tour.idTournamentMemb;
        }


        public void insertResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto)
        {

            string queryString = "INSERT INTO results(id_tournament_member, id_hole, strokes, points, confirmed, netto) VALUES (" + id_tournament_member + ", " + id_hole + ", " + strokes + ", " + pointt + ", true, " + netto + ");";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void updateResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto)
        {

            string queryString = "UPDATE results SET confirmed = true, strokes=" + strokes + ", points=" + pointt + ", netto=" + netto + " WHERE id_tournament_member=" + id_tournament_member + " AND id_hole=" + id_hole + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void insertTotalPoints(int totalPoints)
        {

            string queryString = "UPDATE tournament_member SET confirmed_total=true, points=" + totalPoints + " WHERE id = " + getTounamentMembId() + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void insertTotalSlag(int totalSlag)
        {

            string queryString = "UPDATE tournament_member SET nettototal=" + totalSlag + " WHERE id = " + getTounamentMembId() + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public string getGender()
        {
            string queryString = "SELECT member.gender FROM public.member WHERE member.id = '" + ListBox1.SelectedValue + "';";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memb.gender = reader["gender"].ToString();
                        }
                    }
                }
            }
            return memb.gender;
        }
        public double getHcp()
        {
            string queryString = "SELECT member.hcp FROM public.member WHERE member.id = '" + ListBox1.SelectedValue + "';";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            memb.hcp = Convert.ToDouble(reader["hcp"]);
                        }
                    }
                }
            }
            return memb.hcp;
        }

        protected void rbtLstTee_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox1.Visible = true;
            ListBox1.Items.Clear();
            getAllContestants(ListBox1);
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getScoreForHole();
        }

        
    }
}