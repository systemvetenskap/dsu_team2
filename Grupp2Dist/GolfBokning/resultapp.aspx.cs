using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;

namespace GolfBokning
{
    public partial class resultapp : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        HttpCookie teeCookie = new HttpCookie("tee");
        HttpCookie holeCookie = new HttpCookie("tournament_hole");

        //List<Member> newTeam = new List<Member>();
        //List<int> resultsP1 = new List<int>();
        //List<int> resultsP2 = new List<int>();
        //List<int> resultsP3 = new List<int>();
        //List<int> resultsP4 = new List<int>();
        //List<Classes.Holes> listHolesP1 = new List<Classes.Holes>();//TEam
        //List<Classes.Holes> listHolesP2 = new List<Classes.Holes>();
        //List<Classes.Holes> listHolesP3 = new List<Classes.Holes>();
        //List<Classes.Holes> listHolesP4 = new List<Classes.Holes>();
        List<Classes.Holes> listan2 = new List<Classes.Holes>();  //Singel 
        Member memb = new Member();
        Lanes lane = new Lanes();
        static int currHole = 0;
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];
            DropDownListChoose.SelectedIndexChanged += new EventHandler(DropDownListChoose_SelectedIndexChanged);
            TextBox1.TextChanged += new EventHandler(btnCalc_Click);
            //rbtLstTee.SelectedIndexChanged += new EventHandler(rbtLstTee_SelectedIndexChanged);
            ButtonStart.Click += new EventHandler(ButtonStart_Click);
            DropDownListChoose.AutoPostBack = true;
           // rbtLstTee.AutoPostBack = true;

            if (!IsPostBack)
            {
                DropDownListChoose.Items.Add(new ListItem("Välj tävling...", ""));
                DropDownListChoose.SelectedIndex = 0;
                GetTournaments();
            }

            LabelName.Text = "Välkommen " + myCookie["_name"].Replace("Ã¶", "ö").Replace("Ã¥", "ä").Replace("Ã¤", "") + "!";
            PanelBoxes.Visible = false;
            PanelHoles.Visible = false;
            //tableTeamHolder.Visible = false;
           // PanelTees.Visible = false;
            //LabelHiddenHole.Visible = false;

            teeCookie = Request.Cookies["tee"];

            listan2.Clear(); //Tömmer listan för singel
            for (int i = 1; i < 19; i++)
            {
                listan2.Add(getHole(i)); //Singel lägger på ett hole objekt för det håålet i. I listan
                //listHolesP1.Add(getHole(i));
                //listHolesP2 = listHolesP1;
                //listHolesP3 = listHolesP1;
                //listHolesP4 = listHolesP1;
            }

            int idd = Convert.ToInt16(myCookie["_id"]);
            memb.id = idd;
            memb.hcp = getHcp();
            memb.gender = getGender();
            setHcpApp();
            listan2.Sort();
            lblHoleNrApp.Text = "Hål nr: " + listan2[currHole].holeIndex.ToString();
            LabelHiddenHole.Text = listan2[currHole].holeIndex.ToString();
            lblParApp.InnerHtml = listan2[currHole].par.ToString();
            lblHcpIndexApp.InnerHtml = listan2[currHole].hcpIndex.ToString();
            lblPlayHcpApp1.InnerHtml = listan2[currHole].erhallna.ToString();
            //Ser till att inte kolla efter dropdown.selected value innan det finns något
            //if (IsPostBack)
            //{
            //    getScoreForHole();
            //}
        }

        protected bool IfTeam(string id_tournament)
        {
            string sql = "SELECT * FROM tournament WHERE id = @id_tournament";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);

            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (Convert.ToBoolean(dr["isteam"]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        protected void ButtonStart_Click(object sender, EventArgs e)
        {

            teeCookie = Request.Cookies["tee"];
            if (teeCookie != null)
            {
                PanelHoles.Visible = true;

                //string[] teeArray = Server.UrlDecode(teeCookie.Value).Split(':');

                //if (teeArray[0].ToString() == LabelHiddenTournamentID.Text)
                //{
                //    if (LabelHiddenTeam.Text == "1")
                //    {
                //        tableTeamHolder.Visible = true;
                //        PanelTees.Visible = false;
                //        PanelHoles.Visible = false;
                //    }
                //    else
                //    {
                //        tableTeamHolder.Visible = false;
                //        PanelTees.Visible = false;
                //        PanelHoles.Visible = true;
                //    }
                //}
                //else
                //{
                //    PanelTees.Visible = true;
                //    PanelHoles.Visible = false;
                //    tableTeamHolder.Visible = false;
                //}
            }
            else
            {
                //PanelTees.Visible = true;
                PanelHoles.Visible = false;
                //tableTeamHolder.Visible = false;
            }
            getScoreForHole();
        }

        //protected void rbtLstTee_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (LabelHiddenTeam.Text == "1")
        //    {
        //        //tableTeamHolder.Visible = true;
        //        //PanelTees.Visible = false;
        //        PanelHoles.Visible = false;
        //    }
        //    else
        //    {
        //       // tableTeamHolder.Visible = false;
        //        //PanelTees.Visible = false;
        //        PanelHoles.Visible = true;
        //    }

        //}

        protected void DropDownListChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            //newTeam.Clear();
            //newTeam = createTeam();

            PanelChoose.Visible = false;
            PanelBoxes.Visible = true;
            LabelH2.Text = "Tävling: " + DropDownListChoose.SelectedItem.Text;
            LabelNameHole.Text = "Tävling: " + DropDownListChoose.SelectedItem.Text;
            //LabelTeeName.Text = "Tävling: " + DropDownListChoose.SelectedItem.Text;
            LabelHiddenLeader.Text = "1";
            GetTournament(Convert.ToInt16(DropDownListChoose.SelectedValue));
            LabelHiddenTournamentID.Text = DropDownListChoose.SelectedValue.ToString();

            bool isTeam = IfTeam(DropDownListChoose.SelectedValue);
            if (isTeam)
            {
                LabelHiddenTeam.Text = "1";
            }
            else
            {
                LabelHiddenTeam.Text = "0";
            }

        }

        protected void GetStarttime(int id_member, int id_tournament)
        {
            string sql = "SELECT starttime FROM tournament_member WHERE id_member = @id_member AND id_tournament = @id_tournament";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_member", id_member);
            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["starttime"] != DBNull.Value)
                {
                    string YStartTime = dr["starttime"].ToString();
                    YStartTime = YStartTime.Replace("0001-01-01 ", "");
                    YStartTime = YStartTime.Remove(YStartTime.Length - 3);
                    LabelYStarttime.Text = YStartTime;
                }
            }

            conn.Close();

        }

        protected void GetTournament(int id)
        {
            string sql = "SELECT * FROM tournament WHERE id = @id ORDER BY id";
            myCookie = Request.Cookies["LoginCookie"];
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string starttime = dr["starttime"].ToString();
                starttime = starttime.Replace("0001-01-01 ", "");
                starttime = starttime.Remove(starttime.Length - 3);
                string endtime = dr["endtime"].ToString();
                endtime = endtime.Replace("0001-01-01 ", "");
                endtime = endtime.Remove(endtime.Length - 3);

                string date = dr["date"].ToString();
                date = date.Replace(" 00:00:00", "");

                LabelTDesc.Text = dr["description"].ToString();
                LabelTNbrHoles.Text = dr["nbr_holes"].ToString();
                LabelTNbrComp.Text = dr["nbr_competitors"].ToString();
                LabelTStarttime.Text = date + " " + starttime;
                LabelTEndtime.Text = date + " " + endtime;
            }

            conn.Close();
            GetStarttime(Convert.ToInt16(myCookie["_id"]), id);
        }

        protected void GetTournaments()
        {
            myCookie = Request.Cookies["LoginCookie"];
            string sql = "SELECT * FROM tournament t INNER JOIN tournament_member tm ON t.id = tm.id_tournament WHERE id_member = @id_member";
            int id_member = Convert.ToInt16(myCookie["_id"]);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_member", id_member);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["name"].ToString();
                li.Value = dr["id"].ToString();
                DropDownListChoose.Items.Add(li);
            }

            conn.Close();
        }





        //------------------------------------------------------------------Dilan-----------------------------------------------------------------

        public Classes.Holes getHole(int hIndex) //Retunerar ett hole objekt
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
                Classes.Holes hole1 = new Classes.Holes();
                return hole1;

            }

        }

        private int calcPlayerHcp()
        {
            int playedHcp = 0;
            getLaneTee();
            if (memb.hcp > 36)
            {
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
                        playedHcp = Convert.ToInt32(memb.hcp - 2); //Om den är
                    }
                }
            }
            else
            {
                double playHandicap = ((memb.hcp * lane.courseSlope) / 113) + (lane.scratch - 72); //Formel rätt men vad gör den?
                playedHcp = Convert.ToInt32(playHandicap);
            }
            return playedHcp;
        }
        private void setHcpApp() //Lägger på erhållna slag 
        {
            int playedHcp = calcPlayerHcp();
            if (playedHcp < 0)
            {
                playedHcp = playedHcp * (-1); //Fel blir positv
            }
            for (int i = 0; i < playedHcp; i++) //Loopar fram till i är mindre än hcp slutar vid handiapp
            {
                if (i > 17) //Över 17 får man extra slag
                {
                    if (i > 35)
                    {
                        if (i > 53)
                        {
                            listan2[i - 54].erhallna++;
                        }
                        else
                        {
                            listan2[i - 36].erhallna++;
                        }
                    }
                    else
                    {
                        listan2[i - 18].erhallna++;
                    }
                }
                else
                {
                    listan2[i].erhallna++;
                }
            }
        }
        private void setPointsApp()
        {
            //listan.Sort();
            //räkna netto
            int slag = Convert.ToInt32(TextBox1.Text);
            int netto = slag - listan2[currHole].erhallna;
            int IdHolder = getTounamentMembId();
            int points = 0;
            lblNettoApp1.InnerHtml = netto.ToString();
            //räkna poäng
            if ((netto - listan2[currHole].par) >= 2)
            {
                points = 0;
            }
            else
            {
                points = listan2[currHole].par - netto + 2;
            }
            //räkna */-
            int plusMinus = netto - listan2[currHole].par;
            if (plusMinus > 0)
            {
                lblPlusMinus1.InnerHtml = "+" + plusMinus.ToString();
            }
            else
            {
                lblPlusMinus1.InnerHtml = plusMinus.ToString();
            }
            //Insert eller update till db
            if (LabelHiddenTournamentID.Text != "")
            {
                if (InsertOrUpdate(currHole + 1) == true)
                {
                    insertResults(IdHolder, currHole + 1, slag, points, netto, memb.hcp, memb.id);
                }
                else
                {
                    updateResults(IdHolder, currHole + 1, slag, points, netto, memb.hcp, memb.id);
                }
            }

        }
        private void getScoreForHole()
        {
            int currHoleTemp = currHole + 1;
            string sql = "SELECT strokes, points, netto FROM public.results WHERE id_tournament_member = " + getTounamentMembId() + " AND id_hole = " + currHoleTemp + ";";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);

            }
            foreach (DataRow dr in dt.Rows)
            {
                TextBox1.Text = dr["strokes"].ToString();
                lblNettoApp1.InnerHtml = dr["netto"].ToString();
                int netto = (int)dr["netto"];
                int plusMinus = netto - listan2[currHole].par;
                if (plusMinus > 0)
                {
                    lblPlusMinus1.InnerHtml = "+" + plusMinus.ToString();
                }
                else
                {
                    lblPlusMinus1.InnerHtml = plusMinus.ToString();
                }

                LabelHiddenHole.Text = listan2[currHole].holeIndex.ToString();
            }

            listCurrentPlacements(currHoleTemp);
            //int id_tournament = Convert.ToInt16(DropDownListChoose.SelectedValue);
            ////string sql2 = "SELECT * FROM results WHERE id_tournament = " + id_tournament + " AND id_hole = " + currHoleTemp + " ORDER BY netto ASC";
            //string query2 = "SELECT row_number() OVER (Order by netto) as rnum, id_tournament_member FROM results WHERE id_tournament = " + id_tournament + " AND id_hole = " + currHoleTemp + " ORDER BY netto ASC";
            //using (NpgsqlCommand cmd2 = new NpgsqlCommand(query2, conn))
            //    {
            //        conn.Open();
            //        using (NpgsqlDataReader reader = cmd2.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                // Fylla i en tabell som håller koll på resultaten på det hålet
            //            }
            //            conn.Close();
            //        }
            //    }

            //NpgsqlCommand cmd2 = new NpgsqlCommand(query2, conn);
            //conn.Open();
            //NpgsqlDataReader dr2 = cmd2.ExecuteReader();

            //while (dr2.Read())
            //{

            //}


        }
        public void getLaneTee()
        {

            string queryString = "SELECT id, tee, gender, scratch, slope FROM lane WHERE gender = '" + memb.gender.ToString() + "'";

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

        public int getTounamentMembId()
        {
            //if (LabelHiddenTournamentID.Text != "")
            //{
            myCookie = Request.Cookies["LoginCookie"];
            int idd = Convert.ToInt16(myCookie["_id"]);
            int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
            Classes.clsTournament tour = new Classes.clsTournament();
            string queryString = "SELECT id FROM tournament_member WHERE id_member = " + idd + " AND id_tournament = " + tournamentID + ";";

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
            //}
            //else
            //{
            //    return 0;
            //}
        }
        public string getGender()
        {
            myCookie = Request.Cookies["LoginCookie"];
            int idd = Convert.ToInt16(myCookie["_id"]);
            string queryString = "SELECT member.gender FROM public.member WHERE member.id = " + idd + ";";

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
            myCookie = Request.Cookies["LoginCookie"];
            int idd = Convert.ToInt16(myCookie["_id"]);
            string queryString = "SELECT member.hcp FROM public.member WHERE member.id = " + idd + ";";

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

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            PanelHoles.Visible = true;

            //if (rbtLstTee.SelectedValue == "")
            //{
            //    //Var god välj tee
            //}
            //else
            //{
                if (LabelHiddenTournamentID.Text != "")
                {
                    int tourMembId = getTounamentMembId();
                }

                memb.hcp = getHcp();
                memb.gender = getGender();
                setPointsApp();
                // lblPlayerPlaceApp.Text = "Nuvarande placering: " + listCurrentPlacements(currHole + 1);
            //}
            LabelHiddenHole.Text = listan2[currHole].holeIndex.ToString();
            getScoreForHole();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //bool ifTeam = isTeam();
            //if (ifTeam == true)
            //{
            //    if (LabelHiddenTournamentID.Text != "")
            //    {
            //        tableTeamHolder.Visible = true;
            //        currHole++;
            //        for (int i = 0; i < newTeam.Count; i++)
            //        {
            //            if (InsertOrUpdateTeam(currHole + 1, i) == false)
            //            {
            //                getScoreForHoleTeam();
            //            }
            //            else
            //            {
            //                player1h.Text = "";
            //                player2h.Text = "";
            //                player3h.Text = "";
            //                player4h.Text = "";

            //                lblNettoApp1TeamP1.InnerHtml = "";
            //                lblNettoApp1TeamP2.InnerHtml = "";
            //                lblNettoApp1TeamP3.InnerHtml = "";
            //                lblNettoApp1TeamP4.InnerHtml = "";

            //                lblPlusMinus1TeamP1.InnerHtml = "";
            //                lblPlusMinus1TeamP2.InnerHtml = "";
            //                lblPlusMinus1TeamP3.InnerHtml = "";
            //                lblPlusMinus1TeamP4.InnerHtml = "";
            //            }

            //            lblHoleNrAppTeam.InnerHtml = "Hål nr: " + listHolesP1[currHole].holeIndex.ToString();
                        
            //            lblParApp.InnerHtml = listan2[currHole].par.ToString();
            //            lblHcpIndexApp.InnerHtml = listan2[currHole].hcpIndex.ToString();
            //            lblPlayHcpApp1.InnerHtml = listan2[currHole].erhallna.ToString();
            //        }

                    

            //    }
            //}
            //else
            //{
                if (LabelHiddenTournamentID.Text != "")
                {
                    PanelHoles.Visible = true;
                    currHole++;
                    if (InsertOrUpdate(currHole + 1) == false)
                    {
                        getScoreForHole();
                    }
                    else
                    {
                        TextBox1.Text = "";
                        lblNettoApp1.InnerHtml = "";
                        lblPlusMinus1.InnerHtml = "";
                    }
                    lblHoleNrApp.Text = "Hål nr: " + listan2[currHole].holeIndex.ToString();
                    lblParApp.InnerHtml = listan2[currHole].par.ToString();
                    lblHcpIndexApp.InnerHtml = listan2[currHole].hcpIndex.ToString();
                    lblPlayHcpApp1.InnerHtml = listan2[currHole].erhallna.ToString();

                    //if (listCurrentPlacements(currHole + 1) <= 0)
                    //{
                    //    lblPlayerPlaceApp.Text = "Ange antal slag för att beräkna placering";
                    //}
                    //else
                    //{
                    //    lblPlayerPlaceApp.Text = "Nuvarande placering för hålet: " + listCurrentPlacements(currHole + 1);
                    //}
                }
            

            //}
                LabelHiddenHole.Text = listan2[currHole].holeIndex.ToString();
            getScoreForHole();

        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (LabelHiddenTournamentID.Text != "")
            {
                PanelHoles.Visible = true;
                currHole--;
                if (InsertOrUpdate(currHole + 1) == false)
                {
                    getScoreForHole();
                }
                else
                {
                    TextBox1.Text = "";
                    lblNettoApp1.InnerHtml = "";
                    lblPlusMinus1.InnerHtml = "";
                }
                lblHoleNrApp.Text = "Hål nr: " + listan2[currHole].holeIndex.ToString();
                lblParApp.InnerHtml = listan2[currHole].par.ToString();
                lblHcpIndexApp.InnerHtml = listan2[currHole].hcpIndex.ToString();
                lblPlayHcpApp1.InnerHtml = listan2[currHole].erhallna.ToString();

                //if (listCurrentPlacements(currHole + 1) <= 0)
                //{
                //    lblPlayerPlaceApp.Text = "Ange antal slag";
                //}
                //else
                //{
                //    lblPlayerPlaceApp.Text = "Nuvarande placering för hålet: " + listCurrentPlacements(currHole + 1);
                //}
            }
            LabelHiddenHole.Text = listan2[currHole].holeIndex.ToString();
            getScoreForHole();
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
        //private bool InsertOrUpdateForApp(int hole_id)
        //{
        //    int idd = Convert.ToInt16(myCookie["_id"]);
        //    bool check = false;
        //    string queryString = "SELECT id_member FROM app_results WHERE id_member = " + idd + " AND id_tournament=" + DropDownListChoose.SelectedValue + ";";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();

        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (!reader.HasRows)
        //                {
        //                    check = true;
        //                }
        //                else
        //                {
        //                    check = false;
        //                }
        //            }
        //        }
        //    }
        //    return check;
        //}
        public void updateResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto, double playerHcp, int id_member)
        {
            int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
            string queryString = "UPDATE results SET strokes=" + strokes + ", points=" + pointt + ", netto=" + netto + ", id_tournament=" + tournamentID + ", player_hcp=" + playerHcp + ", id_member=" + id_member + " WHERE id_tournament_member=" + id_tournament_member + " AND id_hole=" + id_hole + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void insertResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto, double playerHcp, int id_member)
        {
            int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
            string queryString = "INSERT INTO results(id_tournament_member, id_hole, strokes, points, confirmed, netto, id_tournament, player_hcp, id_member) VALUES (" + id_tournament_member + ", " + id_hole + ", " + strokes + ", " + pointt + ", false, " + netto + ", " + tournamentID + ", " + playerHcp + ", " + id_member + ");";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        //public void insertResultsApp (int netto)
        //{
        //    int idd = Convert.ToInt16(myCookie["_id"]);
        //    string queryForApp = "INSERT INTO app_results(id_tournament, id_member, h1) VALUES (" + DropDownListChoose.SelectedValue + ", " + idd + ", " + netto + ");";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryForApp, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}
        //public void updateResultsApp(int netto, int tournament, int member, int holeNr)
        //{
        //    int idd = Convert.ToInt16(myCookie["_id"]);
        //    string queryString = "UPDATE app_results SET h" + holeNr + "=" + netto + " WHERE id_tournament=" + DropDownListChoose.SelectedValue + " AND id_member=" + idd + ";";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //}

        //public void insertTotalPoints(int totalPoints)
        //{




        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        //private int getReultsUpToCurrHole(int holesCount)
        //{
        //    int nettoOut = 0;
        //    for (int i = 0; i < holesCount; i++)
        //    {
        //        string queryString = "SELECT results.netto FROM public.results, public.tournament, public.tournament_member WHERE tournament.id = tournament_member.id_tournament AND tournament_member.id = results.id_tournament_member AND id_hole=" + (holesCount + 1) + " AND results.id_tournament_member =" + getTounamentMembId() + ";";
        //        using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //        {
        //            using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //            {
        //                connection.Open();
        //                using (NpgsqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        nettoOut += (int)reader["netto"];
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return nettoOut;
        //}

        private void listCurrentPlacements(int id_hole)
        {
            int idd = Convert.ToInt16(myCookie["_id"]);
            int id_tournament = Convert.ToInt16(DropDownListChoose.SelectedValue);
            int totalPlayers = HowManyPlayerInHole();
            int tourMenbId = getTounamentMembId();
            string queryString = "SELECT row_number() OVER (order by netto, player_hcp) as rnum, id_tournament_member, netto FROM results WHERE id_tournament = " + id_tournament + " AND id_hole = " + id_hole + ";";
            int placement = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idTemp = Convert.ToInt32(reader["id_tournament_member"]);
                            if (idTemp == tourMenbId)
                            {
                                placement = Convert.ToInt32(reader["rnum"]);
                                lblPlayerPlaceApp.Text = "Nuvarande placering för hålet: " + placement.ToString() + " av totalt " + totalPlayers.ToString() + " spelare.";
                            }

                        }
                        if (placement == 0)
                        {
                            lblPlayerPlaceApp.Text = "Ange slag för att beräkna din nuvarande placering för detta hål.";
                        }
                    }
                }
            }
        }

        private int HowManyPlayerInHole()
        {
            int playersCount = 0;
            int id_tournament = Convert.ToInt16(DropDownListChoose.SelectedValue);
            string queryString = "SELECT COUNT(results.strokes) FROM public.results, public.tournament, public.tournament_member WHERE tournament.id = tournament_member.id_tournament AND tournament_member.id = results.id_tournament_member AND id_hole=" + (currHole + 1) + " And tournament.id=" + id_tournament + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playersCount = Convert.ToInt32(reader["count"]);
                        }
                    }
                }
            }
            return playersCount;
        }

        //------------------------------------------------------------------Teamz---------------


        //private bool isTeam()
        //{
        //    int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
        //    bool ifTeam = false;
        //    string queryString = "SELECT isteam FROM public.tournament WHERE id=" + tournamentID + ";";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    ifTeam = (bool)reader["isteam"];
        //                }
        //            }
        //        }
        //    }
        //    return ifTeam;
        //}
        //private List<Member> createTeam()
        //{
        //    int teamID = getTeamId();
        //    Member team = new Member();
        //    team.team = new List<Member>();

        //    string sqlen = "SELECT member.id, firstname, lastname, hcp, gender FROM public.tournament_member, public.member WHERE member.id = id_member AND id_team = " + teamID + ";";//hårdkodat teamID

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(sqlen, connection))
        //        {
        //            connection.Open();
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Member teamMemb = new Member();
        //                    teamMemb.id = (int)reader["id"];
        //                    teamMemb.firstName = reader["firstname"].ToString();
        //                    teamMemb.lastName = reader["lastName"].ToString();
        //                    teamMemb.hcp = Convert.ToDouble(reader["hcp"]);
        //                    teamMemb.gender = reader["gender"].ToString();
        //                    team.team.Add(teamMemb);
        //                }
        //            }
        //        }
        //    }
        //    return team.team;
        //}
        //private int getTeamId()
        //{
        //    int teamID = 0;
        //    int idd = Convert.ToInt16(myCookie["_id"]);
        //    int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
        //    string sql = "SELECT team.id FROM public.team, public.tournament_member WHERE team.id = tournament_member.id_team AND tournament_member.id_tournament = " + tournamentID + " AND id_member = " + idd + ";";
        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    teamID = (int)reader["id"];
        //                }
        //            }
        //        }
        //    }


        //    return teamID;
        //}
        //public int getTounamentMembIdTeam(int indexTeamList)
        //{
        //    int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
        //    Classes.clsTournament tour = new Classes.clsTournament();
        //    string queryString = "SELECT id FROM tournament_member WHERE id_member = '" + newTeam[indexTeamList].id + "' AND id_tournament = '" + tournamentID + "';";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();

        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    tour.idTournamentMemb = (int)reader["id"];
        //                }
        //            }
        //        }
        //    }
        //    return tour.idTournamentMemb;
        //}


        //private void setPointsTeam()
        //{
        //    int teamScoreForHole = 0;
        //    int teamTotalscore = 0;

        //    listHolesP1.Sort();
        //    listHolesP2.Sort();
        //    listHolesP3.Sort();
        //    listHolesP4.Sort();

        //    int points = 0;
        //    int total = 0;
        //    int totalSlag = 0;
        //    int slag = 0;
        //    int netto = 0;
        //    //string lblHolder = "";
        //    int cellTemp = 0; // minus 1 på count?
        //    int playerTemp = 0;
        //    for (int ii = 0; ii < newTeam.Count; ii++)
        //    {
        //        teamScoreForHole = 0;
        //        teamTotalscore = 0;
        //        int tourMembId = getTounamentMembIdTeam(ii);
        //        for (int i = 0; i < tableTeam.Rows.Count - 1; i++)
        //        {
        //            cellTemp = i + 1;
        //            playerTemp = ii + 1;
        //            bool ifUpOrInsert = false;

        //            if (ii == 0)
        //            {
        //                TextBox htcv = (TextBox)tableTeam.Rows[1].Cells[1].FindControl("player1h");
        //                resultsP1.Add(Convert.ToInt32(htcv.Text));
        //                slag = resultsP1[currHole];
        //                netto = slag - listHolesP1[currHole].erhallna;
        //                calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);
        //            }
        //            else if (ii == 1)
        //            {
        //                TextBox htcv = (TextBox)tableTeam.Rows[2].Cells[1].FindControl("player2h");
        //                resultsP2.Add(Convert.ToInt32(htcv.Text));
        //                slag = resultsP2[currHole];
        //                netto = slag - listHolesP2[currHole].erhallna;
        //                calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);

        //            }
        //            else if (ii == 2)
        //            {
        //                TextBox htcv = (TextBox)tableTeam.Rows[3].Cells[1].FindControl("player3h");
        //                resultsP3.Add(Convert.ToInt32(htcv.Text));
        //                slag = resultsP3[currHole];
        //                netto = slag - listHolesP3[currHole].erhallna;
        //                calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);

        //                //if (newTeam.Count < 4)
        //                //{
        //                //    teamScoreForHole = calcTeamScoreForHole(listHolesP3[i].holeIndex);
        //                //    teamTotalscore += teamScoreForHole;
        //                //}
        //            }
        //            else if (ii == 3)
        //            {
        //                TextBox htcv = (TextBox)tableTeam.Rows[4].Cells[1].FindControl("player4h");
        //                resultsP4.Add(Convert.ToInt32(htcv.Text));
        //                slag = resultsP4[currHole];
        //                netto = slag - listHolesP4[currHole].erhallna;
        //                calcAndInsert(ref points, ref total, ref totalSlag, ref slag, ref netto, ii, tourMembId, i, ref ifUpOrInsert);

        //                //teamScoreForHole = calcTeamScoreForHole(listHolesP4[i].holeIndex);
        //                //teamTotalscore += teamScoreForHole;
        //            }
        //        }
        //    }

        //    //insertTotalTeamPoints(teamTotalscore);


        //}

        //private void calcAndInsert(ref int points, ref int total, ref int totalSlag, ref int slag, ref int netto, int ii, int tourMembId, int i, ref bool ifUpOrInsert)
        //{
        //    List<Classes.Holes> listHolder = new List<Classes.Holes>();
        //    int memberIdTemp = 0;
        //    double memberHcpTemp = 0;
        //    if (ii == 0)
        //    {
        //        listHolder = listHolesP1;
        //        memberIdTemp = newTeam[0].id;
        //        memberHcpTemp = newTeam[0].hcp;
        //    }
        //    else if (ii == 1)
        //    {
        //        listHolder = listHolesP2;
        //        memberIdTemp = newTeam[1].id;
        //        memberHcpTemp = newTeam[1].hcp;
        //    }
        //    else if (ii == 2)
        //    {

        //        listHolder = listHolesP3;
        //        memberIdTemp = newTeam[2].id;
        //        memberHcpTemp = newTeam[2].hcp;
        //    }
        //    else if (ii == 3)
        //    {
        //        listHolder = listHolesP4;
        //        memberIdTemp = newTeam[3].id;
        //        memberHcpTemp = newTeam[3].hcp;
        //    }

        //    if ((netto - listHolder[currHole].par) >= 2)
        //    {
        //        points = 0;
        //    }
        //    else
        //    {
        //        points = listHolder[currHole].par - netto + 2;
        //    }
        //    ifUpOrInsert = InsertOrUpdateTeam(i + 1, ii);
        //    if (ifUpOrInsert == true)
        //    {
        //        insertResults(tourMembId, currHole + 1, slag, points, netto, memberHcpTemp, memberIdTemp);
        //    }
        //    else
        //    {
        //        updateResults(tourMembId, currHole + 1, slag, points, netto, memberHcpTemp, memberIdTemp);
        //    }
        //    //total = total + points;
        //    //totalSlag = totalSlag + slag;
        //}
        ////public void insertTotalPointsTeam(int totalPoints, int indexInList)
        ////{
        ////    int tourMembId = getTounamentMembIdTeam(indexInList);
        ////    string queryString = "UPDATE tournament_member SET confirmed_total=true, points=" + totalPoints + " WHERE id = " + tourMembId + ";";

        ////    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        ////    {
        ////        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        ////        {
        ////            connection.Open();
        ////            command.ExecuteNonQuery();
        ////        }
        ////    }


        ////}//fixa
        //private int calcTeamScoreForHole(int id_hole)
        //{
        //    int scoreSum = 0;
        //    int score = 0;
        //    int team = getTeamId();
        //    string query = "SELECT tournament_member.id_member, results.points FROM public.results, public.tournament_member WHERE results.id_tournament_member = tournament_member.id AND id_team=" + team + " AND " + id_hole + " ORDER BY results.points DESC;";// Hårdkodat teamID

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //        {
        //            connection.Open();
        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {

        //                    score = (int)reader["points"];
        //                    scoreSum += score;
        //                }
        //            }
        //        }
        //    }
        //    return scoreSum;
        //}//fixa
        ////public void insertTotalTeamPoints(int teamPoints)
        ////{
        ////    int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
        ////    string qString = "UPDATE team SET total_points=" + teamPoints + " WHERE id=" + tournamentID + ";";

        ////    using (NpgsqlConnection connection2 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        ////    {
        ////        using (NpgsqlCommand command = new NpgsqlCommand(qString, connection2))
        ////        {
        ////            connection2.Open();
        ////            command.ExecuteNonQuery();
        ////        }
        ////    }
        ////}
        //public void getLaneTeeTeam()
        //{

        //    string queryString = "SELECT id, tee, gender, scratch, slope FROM lane WHERE tee = '" + rbtLstTee.SelectedValue.ToString() + "' AND gender = '" + newTeam[0].gender.ToString() + "'";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {

        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {

        //            connection.Open();

        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {

        //                while (reader.Read())
        //                {
        //                    lane.laneID = (int)reader["id"];
        //                    lane.courseSlope = Convert.ToInt32(reader["slope"]);
        //                    lane.scratch = Convert.ToDouble(reader["scratch"]);
        //                    lane.tee = reader["tee"].ToString();
        //                }
        //            }
        //        }
        //    }
        //}
        //private void setHcpTeam(List<int> playersHcp)
        //{
        //    int currHcp = 0;

        //    for (int ii = 0; ii < playersHcp.Count; ii++)
        //    {
        //        currHcp = playersHcp[ii];

        //        if (currHcp < 0)
        //        {
        //            currHcp = currHcp * (-1);
        //        }
        //        for (int i = 0; i < currHcp; i++)
        //        {
        //            if (i > 17)
        //            {
        //                if (i > 35)
        //                {
        //                    if (i > 53)
        //                    {
        //                        if (ii == 0)
        //                        {
        //                            listHolesP1[i - 54].erhallna++;
        //                        }
        //                        else if (ii == 1)
        //                        {
        //                            listHolesP2[i - 54].erhallna++;
        //                        }
        //                        else if (ii == 2)
        //                        {
        //                            listHolesP3[i - 54].erhallna++;
        //                        }
        //                        else if (ii == 3)
        //                        {
        //                            listHolesP4[i - 54].erhallna++;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (ii == 0)
        //                        {
        //                            listHolesP1[i - 36].erhallna++;
        //                        }
        //                        else if (ii == 1)
        //                        {
        //                            listHolesP2[i - 36].erhallna++;
        //                        }
        //                        else if (ii == 2)
        //                        {
        //                            listHolesP3[i - 36].erhallna++;
        //                        }
        //                        else if (ii == 3)
        //                        {
        //                            listHolesP4[i - 36].erhallna++;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (ii == 0)
        //                    {
        //                        listHolesP1[i - 18].erhallna++;
        //                    }
        //                    else if (ii == 1)
        //                    {
        //                        listHolesP2[i - 18].erhallna++;
        //                    }
        //                    else if (ii == 2)
        //                    {
        //                        listHolesP3[i - 18].erhallna++;
        //                    }
        //                    else if (ii == 3)
        //                    {
        //                        listHolesP4[i - 18].erhallna++;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (ii == 0)
        //                {
        //                    listHolesP1[i].erhallna++;
        //                }
        //                else if (ii == 1)
        //                {
        //                    listHolesP2[i].erhallna++;
        //                }
        //                else if (ii == 2)
        //                {
        //                    listHolesP3[i].erhallna++;
        //                }
        //                else if (ii == 3)
        //                {
        //                    listHolesP4[i].erhallna++;
        //                }
        //            }
        //        }


        //    }

        //}

        //protected void btnRegTeamResults_Click(object sender, EventArgs e)
        //{
        //    //Behöver nog inte göra allt detta för varje hål. Lane tee gäller för hela tävlingen. Samma med hcp.. Men kanske måste göra så nu.
        //    newTeam.Clear();
        //    newTeam = createTeam();
        //    List<int> playersHcpList = new List<int>();
        //    getLaneTeeTeam();
        //    for (int i = 0; i < newTeam.Count; i++)
        //    {
        //        int playedHcp = 0;

        //        if (newTeam[i].hcp > 36)
        //        {
        //            if (newTeam[i].gender == "Female")
        //            {
        //                playedHcp = Convert.ToInt32(newTeam[i].hcp + 5);
        //            }
        //            else
        //            {
        //                if (lane.tee == "Gul 57")
        //                {
        //                    playedHcp = Convert.ToInt32(newTeam[i].hcp + 4);
        //                }
        //                else if (lane.tee == "Blå 54")
        //                {
        //                    playedHcp = Convert.ToInt32(newTeam[i].hcp + 1);
        //                }
        //                else
        //                {
        //                    playedHcp = Convert.ToInt32(newTeam[i].hcp - 2);
        //                }
        //            }
        //        }
        //        else
        //        {

        //            double playHandicap = ((newTeam[i].hcp * lane.courseSlope) / 113) + (lane.scratch - 72);
        //            playedHcp = Convert.ToInt32(playHandicap);
        //            playersHcpList.Add(playedHcp);
        //        }
        //    }
        //    setHcpTeam(playersHcpList);
        //    setPointsTeam();
        //    getScoreForHoleTeam();
        //    tableTeamHolder.Visible = true;
        //}


        //private bool InsertOrUpdateTeam(int hole_id, int indexInTeamList)
        //{
        //    bool check = false;
        //    int tournamentID = Convert.ToInt16(DropDownListChoose.SelectedValue);
        //    string queryString = "SELECT id_tournament_member FROM results WHERE id_tournament = " + tournamentID + " AND id_member= " + newTeam[indexInTeamList].id + " AND id_hole=" + hole_id + ";";

        //    using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
        //    {
        //        using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
        //        {
        //            connection.Open();

        //            using (NpgsqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (!reader.HasRows)
        //                {
        //                    check = true;
        //                }
        //                else
        //                {
        //                    check = false;
        //                }
        //            }
        //        }
        //    }
        //    return check;
        //}


       //private void getScoreForHoleTeam()
       // {
       //     int plusMinus = 0;
       //     int currHoleTemp = currHole + 1;

       //     //player 1 (neo)
       //     lblPlayerNameTeamP1.InnerHtml = newTeam[0].firstName + " " + newTeam[0].lastName[0];
       //     player1h.Text = resultsP1[currHoleTemp].ToString();
       //     lblPlayHcpApp1TeamP1.InnerHtml = listHolesP1[currHoleTemp].erhallna.ToString();
       //     lblNettoApp1TeamP1.InnerHtml = (resultsP1[currHoleTemp] - listHolesP1[currHoleTemp].erhallna).ToString();
       //     plusMinus = resultsP1[currHoleTemp] - listHolesP1[currHoleTemp].par;
       //     if (plusMinus > 0)
       //     {
       //         lblPlusMinus1TeamP1.InnerHtml = "+" + plusMinus.ToString();
       //     }
       //     else
       //     {
       //         lblPlusMinus1TeamP1.InnerHtml = plusMinus.ToString();
       //     }

       //     //player 2
       //     lblPlayerNameTeamP2.InnerHtml = newTeam[1].firstName + " " + newTeam[1].lastName[1];
       //     player2h.Text = resultsP2[currHoleTemp].ToString();
       //     lblPlayHcpApp1TeamP2.InnerHtml = listHolesP2[currHoleTemp].erhallna.ToString();
       //     lblNettoApp1TeamP2.InnerHtml = (resultsP2[currHoleTemp] - listHolesP2[currHoleTemp].erhallna).ToString();
       //     plusMinus = resultsP2[currHoleTemp] - listHolesP2[currHoleTemp].par;
       //     if (plusMinus > 0)
       //     {
       //         lblPlusMinus1TeamP2.InnerHtml = "+" + plusMinus.ToString();
       //     }
       //     else
       //     {
       //         lblPlusMinus1TeamP2.InnerHtml = plusMinus.ToString();
       //     }
       //     //player 3
       //     lblPlayerNameTeamP3.InnerHtml = newTeam[2].firstName + " " + newTeam[2].lastName[2];
       //     player3h.Text = resultsP3[currHoleTemp].ToString();
       //     lblPlayHcpApp1TeamP3.InnerHtml = listHolesP3[currHoleTemp].erhallna.ToString();
       //     lblNettoApp1TeamP3.InnerHtml = (resultsP3[currHoleTemp] - listHolesP3[currHoleTemp].erhallna).ToString();
       //     plusMinus = resultsP3[currHoleTemp] - listHolesP3[currHoleTemp].par;
       //     if (plusMinus > 0)
       //     {
       //         lblPlusMinus1TeamP3.InnerHtml = "+" + plusMinus.ToString();
       //     }
       //     else
       //     {
       //         lblPlusMinus1TeamP3.InnerHtml = plusMinus.ToString();
       //     }

       //     //player 4
       //     if (newTeam.Count > 3)
       //     {
       //         lblPlayerNameTeamP4.InnerHtml = newTeam[3].firstName + " " + newTeam[3].lastName[3];
       //         player4h.Text = resultsP4[currHoleTemp].ToString();
       //         lblPlayHcpApp1TeamP4.InnerHtml = listHolesP4[currHoleTemp].erhallna.ToString();
       //         lblNettoApp1TeamP4.InnerHtml = (resultsP4[currHoleTemp] - listHolesP4[currHoleTemp].erhallna).ToString();
       //         plusMinus = resultsP4[currHoleTemp] - listHolesP4[currHoleTemp].par;
       //         if (plusMinus > 0)
       //         {
       //             lblPlusMinus1TeamP4.InnerHtml = "+" + plusMinus.ToString();
       //         }
       //         else
       //         {
       //             lblPlusMinus1TeamP4.InnerHtml = plusMinus.ToString();
       //         }
       //     }
           
       // }
    }
}