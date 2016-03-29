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
        List<Member> newTeam = new List<Member>();
        Classes.Holes hole1 = new Classes.Holes();
        List<int> resultsP1 = new List<int>();
        List<int> resultsP2 = new List<int>();
        List<int> resultsP3 = new List<int>();
        List<int> resultsP4 = new List<int>();
        List<Classes.Holes> listHolesP1 = new List<Classes.Holes>();
        List<Classes.Holes> listHolesP2 = new List<Classes.Holes>();
        List<Classes.Holes> listHolesP3 = new List<Classes.Holes>();
        List<Classes.Holes> listHolesP4 = new List<Classes.Holes>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                getTournaments();
                tableTeamHolder.Visible = false;
                resultsTableHolder.Visible = false;
            }
            
            for (int i = 1; i < 19; i++)
            {
                listan.Add(getHole(i));
                listHolesP1.Add(getHole(i));
                listHolesP2.Add(getHole(i));
                listHolesP3.Add(getHole(i));
                listHolesP4.Add(getHole(i));
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
            memb.id = Convert.ToInt32(ListBox1.SelectedValue);
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
                    insertResults(IdHolder, i + 1, slag, points, netto, memb.hcp, memb.id);
                }
                else
                {
                    updateResults(IdHolder, i + 1, slag, points, netto, memb.hcp, memb.id);
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

            string queryString = "SELECT id, tee, gender, scratch, slope FROM lane WHERE gender = '" + memb.gender.ToString() +"'";

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

            if (dt.Rows.Count < 18)
            {
                for (int i = 0; i < 18; i++)
                {
                    string lblhcptemp = "TextBox" + (i + 1).ToString();
                    TextBox tc = (TextBox)resultsTable.Rows[i].Cells[1].FindControl(lblhcptemp);
                    tc.Text = "";
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count ; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string lblhcptemp = "TextBox" + (i + 1).ToString();
                    TextBox tc = (TextBox)resultsTable.Rows[i].Cells[1].FindControl(lblhcptemp);
                    tc.Text = dr["strokes"].ToString();
                }
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


        public void insertResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto, double playerHcp, int id_member)
        {

            string queryString = "INSERT INTO results(id_tournament_member, id_hole, strokes, points, confirmed, netto, id_tournament, player_hcp, id_member) VALUES (" + id_tournament_member + ", " + id_hole + ", " + strokes + ", " + pointt + ", true, " + netto + ", " + ListBox2.SelectedValue + ", " + playerHcp + ", " + id_member + ");";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void updateResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto, double playerHcp, int id_member)
        {

            string queryString = "UPDATE results SET confirmed = true, strokes=" + strokes + ", points=" + pointt + ", netto=" + netto + ", id_tournament=" + ListBox2.SelectedValue + ", player_hcp=" + playerHcp + ", id_member=" + id_member + " WHERE id_tournament_member=" + id_tournament_member + " AND id_hole=" + id_hole + ";";

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
            bool ifIsTeam = isTeam();
            ListBox1.Visible = true;
            ListBox1.Items.Clear();
            if (ifIsTeam == false)
            {
                getAllContestants(ListBox1);
                tableTeamHolder.Visible = false;
                resultsTableHolder.Visible = true;
            }
            else
            {
                getAllTeams(ListBox1);
                tableTeamHolder.Visible = true;
                resultsTableHolder.Visible = false;
            }
            

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            newTeam.Clear();
            newTeam = createTeam();
            string nameHolder = "";
            bool ifIsTeam = isTeam();
            if (ifIsTeam == false)
            {
                getScoreForHole();
            }
            else
            {
                
                for (int i = 0; i < newTeam.Count; i++)
                {
                    int cellHolder = i+1;
                    nameHolder = "player" + cellHolder + "lbl";
                    HtmlTableCell thc = (HtmlTableCell)tableTeam.Rows[0].Cells[i].FindControl(nameHolder);
                    thc.InnerHtml = newTeam[i].firstName + " " + newTeam[i].lastName;
                   
                }
                if (newTeam.Count <4)
                {
                    player4lbl.InnerHtml = "";
                }
            }

            
        }



        private bool isTeam()
        {
            bool ifTeam = false;
            string queryString = "SELECT isteam FROM public.tournament WHERE id=" + ListBox2.SelectedValue + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ifTeam = (bool)reader["isteam"];
                        }
                    }
                }
            }
            return ifTeam;
        }
        protected void getAllTeams(ListBox lb)
        {
            string queryString = "SELECT id, name FROM team WHERE id_tournament=" + ListBox2.SelectedValue + "";

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
                            memb.teamid = (int)dr["id"];

                            ListItem li = new ListItem();
                            li.Value = memb.teamid.ToString();
                            li.Text = dr["name"].ToString();
                            lb.Items.Add(li);
                        }
                    }
                }
            }
        }
        private List<Member> createTeam()
        {
            Member team = new Member();
            team.team = new List<Member>();

            string sqlen = "SELECT member.id, firstname, lastname, hcp, gender FROM public.tournament_member, public.member WHERE member.id = id_member AND id_team = " + ListBox1.SelectedValue + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sqlen, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member teamMemb = new Member();
                            teamMemb.id = (int)reader["id"];
                            teamMemb.firstName = reader["firstname"].ToString();
                            teamMemb.lastName = reader["lastName"].ToString();
                            teamMemb.hcp = Convert.ToDouble(reader["hcp"]);
                            teamMemb.gender = reader["gender"].ToString();
                            team.team.Add(teamMemb);
                        }
                    }
                }
            }
            return team.team;
        }
        private void setPointsTeam()
        {
            int teamScoreForHole = 0;
            int teamTotalscore = 0;
            //int totalp1 = 0;
            //int totalp2 = 0;
            //int totalp3 = 0;
            //int totalp4 = 0;
            listHolesP1.Sort();
            listHolesP2.Sort();
            listHolesP3.Sort();
            listHolesP4.Sort();

            int points = 0;
            int total = 0;
            int totalSlag = 0;
            int slag = 0;
            int netto = 0;
            string lblHolder = "";
            int cellTemp = 0; // minus 1 på count?
            int playerTemp = 0;
            for (int ii = 0; ii < newTeam.Count; ii++)
            {
                teamScoreForHole = 0;
                teamTotalscore = 0;
                int tourMembId = getTounamentMembIdTeam(ii);
                for (int i = 0; i < tableTeam.Rows.Count -1; i++)
                {
                    cellTemp = i + 1;
                    playerTemp = ii + 1;
                    bool ifUpOrInsert = false;
                    lblHolder = "player" + playerTemp.ToString() + "h" + cellTemp.ToString();
                    TextBox htcv = (TextBox)tableTeam.Rows[cellTemp].Cells[1].FindControl(lblHolder);
                    
                    if (ii == 0)
                    {
                        resultsP1.Add(Convert.ToInt32(htcv.Text));
                        slag = resultsP1[i];
                        netto = slag - listHolesP1[i].erhallna;
                        calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);
                        if (i == 17)
                        {
                            insertTotalPointsTeam(total, ii);
                            total = 0;
                        }
                    }
                    else if (ii == 1)
                    {
                        resultsP2.Add(Convert.ToInt32(htcv.Text));
                        slag = resultsP2[i];
                        netto = slag - listHolesP2[i].erhallna;
                        calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);
                        if (i == 17)
                        {
                            insertTotalPointsTeam(total, ii);
                            total = 0;
                        }
                    }
                    else if (ii == 2)
                    {
                        resultsP3.Add(Convert.ToInt32(htcv.Text));
                        slag = resultsP3[i];
                        netto = slag - listHolesP3[i].erhallna;
                        calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);
                        if (i == 17)
                        {
                            insertTotalPointsTeam(total, ii);
                            total = 0;
                        }
                        if (newTeam.Count < 4)
                        {
                            teamScoreForHole = calcTeamScoreForHole(listHolesP3[i].holeIndex);
                            teamTotalscore += teamScoreForHole;
                        }
                    }
                    else if (ii == 3)
                    {
                        resultsP4.Add(Convert.ToInt32(htcv.Text));
                        slag = resultsP4[i];
                        netto = slag - listHolesP4[i].erhallna;
                        calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);
                        if (i == 17)
                        {
                            insertTotalPointsTeam(total, ii);
                            total = 0;

                        }
                        teamScoreForHole = calcTeamScoreForHole(listHolesP4[i].holeIndex);
                        teamTotalscore += teamScoreForHole;
                    }
                }
            }

            insertTotalTeamPoints(teamTotalscore);


        }

        // Hämtar slagen från listorna o räknar ut poäng... Ser om det är insert eller update, lägger in i databasen
        private void calcAndInsert(ref int points, ref int total, ref int totalSlag, ref int slag, ref int netto, int ii, int tourMembId, int i, ref bool ifUpOrInsert)
        {
            List<Classes.Holes> listHolder = new List<Classes.Holes>();
            int memberIdTemp = 0;
            double memberHcpTemp =0;
            if (ii == 0)
            {
                listHolder = listHolesP1;
                memberIdTemp = newTeam[0].id;
                memberHcpTemp = newTeam[0].hcp;
            }
            else if (ii == 1)
            {
                listHolder = listHolesP2;
                memberIdTemp = newTeam[1].id;
                memberHcpTemp = newTeam[1].hcp;
            }
            else if (ii == 2)
            {

               listHolder = listHolesP3;
               memberIdTemp = newTeam[2].id;
               memberHcpTemp = newTeam[2].hcp;
            }
            else if (ii == 3)
            {
                listHolder = listHolesP4;
                memberIdTemp = newTeam[3].id;
                memberHcpTemp = newTeam[3].hcp;
            }



            if ((netto - listHolder[i].par) >= 2)
            {
                points = 0;
            }
            else
            {
                points = listHolder[i].par - netto + 2;
            }
            ifUpOrInsert = InsertOrUpdateTeam(i + 1, ii);
            if (ifUpOrInsert == true)
            {
                insertResults(tourMembId, i + 1, slag, points, netto, memberHcpTemp, memberIdTemp);
            }
            else
            {
                updateResults(tourMembId, i + 1, slag, points, netto, memberHcpTemp, memberIdTemp);
            }
            total = total + points;
            totalSlag = totalSlag + slag;
        }

        private bool InsertOrUpdateTeam(int hole_id, int indexInTeamList)
        {
            bool check = false;
            string queryString = "SELECT id_tournament_member FROM results WHERE id_tournament = " + ListBox2.SelectedValue + " AND id_member= " + newTeam[indexInTeamList].id + " AND id_hole=" + hole_id + ";";

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
        //public void insertResultsTeam(int id_tournament_member, int id_hole, int strokes, int pointt, int netto)
        //{
           
        //    string queryString = "INSERT INTO results(id_tournament_member, id_hole, strokes, points, confirmed, netto, id_tournament, player_hcp, id_member) VALUES (" + id_tournament_member + ", " + id_hole + ", " + strokes + ", " + pointt + ", true, " + netto + ", " + ListBox2.SelectedValue + ", " + memb.hcp + ", " + ListBox1.SelectedValue + ");";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        public int getTounamentMembIdTeam(int indexTeamList)
        {
            Classes.clsTournament tour = new Classes.clsTournament();
            string queryString = "SELECT id FROM tournament_member WHERE id_member = '" + newTeam[indexTeamList].id + "' AND id_tournament = '" + ListBox2.SelectedValue + "';";

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

        protected void btnRegTeamResults_Click(object sender, EventArgs e)
        {
            newTeam.Clear();
            newTeam = createTeam();
            List<int> playersHcpList = new List<int>();
            getLaneTeeTeam();
            for (int i = 0; i < newTeam.Count; i++)
            {
                int playedHcp = 0;

                if (newTeam[i].hcp > 36)
                {
                    if (newTeam[i].gender == "Female")
                    {
                        playedHcp = Convert.ToInt32(newTeam[i].hcp + 5);
                    }
                    else
                    {
                        if (lane.tee == "Gul 57")
                        {
                            playedHcp = Convert.ToInt32(newTeam[i].hcp + 4);
                        }
                        else if (lane.tee == "Blå 54")
                        {
                            playedHcp = Convert.ToInt32(newTeam[i].hcp + 1);
                        }
                        else
                        {
                            playedHcp = Convert.ToInt32(newTeam[i].hcp - 2);
                        }
                    }
                }
                else
                {

                    double playHandicap = ((newTeam[i].hcp * lane.courseSlope) / 113) + (lane.scratch - 72);
                    playedHcp = Convert.ToInt32(playHandicap);
                    playersHcpList.Add(playedHcp);
                }
            }
            setHcpTeam(playersHcpList);
            setPointsTeam();
        }

        private void setHcpTeam(List<int> playersHcp)
        {
            int currHcp = 0;

            for (int ii = 0; ii < playersHcp.Count; ii++)
            {
                currHcp = playersHcp[ii];
                
                if (currHcp < 0)
                {
                    currHcp = currHcp * (-1);
                }
                for (int i = 0; i < currHcp; i++)
                {
                    if (i > 17)
                    {
                        if (i > 35)
                        {
                            if (i > 53)
                            {
                                if (ii==0)
                                {
                                    listHolesP1[i - 54].erhallna++;
                                }
                                else if (ii == 1)
                                {
                                    listHolesP2[i - 54].erhallna++;
                                }
                                else if (ii == 2)
                                {
                                    listHolesP3[i - 54].erhallna++;
                                }
                                else if (ii == 3)
                                {
                                    listHolesP4[i - 54].erhallna++;
                                }
                            }
                            else
                            {
                                if (ii == 0)
                                {
                                    listHolesP1[i - 36].erhallna++;
                                }
                                else if (ii == 1)
                                {
                                    listHolesP2[i - 36].erhallna++;
                                }
                                else if (ii == 2)
                                {
                                    listHolesP3[i - 36].erhallna++;
                                }
                                else if (ii == 3)
                                {
                                    listHolesP4[i - 36].erhallna++;
                                }
                            }
                        }
                        else
                        {
                            if (ii == 0)
                            {
                                listHolesP1[i - 18].erhallna++;
                            }
                            else if (ii == 1)
                            {
                                listHolesP2[i - 18].erhallna++;
                            }
                            else if (ii == 2)
                            {
                                listHolesP3[i - 18].erhallna++;
                            }
                            else if (ii == 3)
                            {
                                listHolesP4[i - 18].erhallna++;
                            }
                        }
                    }
                    else
                    {
                        if (ii == 0)
                        {
                            listHolesP1[i].erhallna++;
                        }
                        else if (ii == 1)
                        {
                            listHolesP2[i].erhallna++;
                        }
                        else if (ii == 2)
                        {
                            listHolesP3[i].erhallna++;
                        }
                        else if (ii == 3)
                        {
                            listHolesP4[i].erhallna++;
                        }
                    }
                }


            }



            
        }

        public void insertTotalTeamPoints(int teamPoints)
        {
            string qString = "UPDATE team SET total_points=" + teamPoints + " WHERE id=" + ListBox1.SelectedValue + ";";

            using (NpgsqlConnection connection2 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(qString, connection2))
                {
                    connection2.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private int calcTeamScoreForHole(int id_hole)
        {
            int scoreSum = 0;
            int score = 0;

            string query = "SELECT results.points FROM public.results, public.tournament_member WHERE results.id_tournament_member = tournament_member.id AND id_team=" + ListBox1.SelectedValue + " AND id_hole=" + id_hole + " ORDER BY points DESC LIMIT 3;";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            score = (int)reader["points"];
                            scoreSum += score;
                        }
                    }
                }
            }
            return scoreSum;
        }


        public void getLaneTeeTeam()
        {

            string queryString = "SELECT id, tee, gender, scratch, slope FROM lane WHERE gender = '" + newTeam[0].gender.ToString() + "'";

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


        public void insertTotalPointsTeam(int totalPoints, int indexInList)
        {
            int tourMembId = getTounamentMembIdTeam(indexInList);
            string queryString = "UPDATE tournament_member SET confirmed_total=true, points=" + totalPoints + " WHERE id = " + tourMembId + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

           
        }

    }
}