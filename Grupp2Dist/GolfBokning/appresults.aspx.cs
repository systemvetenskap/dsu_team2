using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;

namespace GolfBokning
{
    public partial class appresults : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        //List<Classes.Holes> listan = new List<Classes.Holes>();
        List<Classes.Holes> listan2 = new List<Classes.Holes>();
        Member memb = new Member();
        Lanes lane = new Lanes();
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        //Classes.Holes currentHole = new Classes.Holes();
        static int currHole = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropdownTournament();
                
            }
            
            //listan.Clear();
            listan2.Clear();
            for (int i = 1; i < 19; i++)
            {
                //listan.Add(getHole(i));
                listan2.Add(getHole(i));
            }
            memb.id = 1054;
            memb.hcp = getHcp();
            memb.gender = getGender();
            setHcpApp();
            listan2.Sort();
            lblHoleNrApp.Text ="Hål nr: " + listan2[currHole].holeIndex.ToString();
            getScoreForHole();
            //lblPlayerPlaceApp.Text = "Nuvarande placering: " + listCurrentPlacements(currHole + 1);
            lblParApp.InnerHtml = listan2[currHole].par.ToString();
            lblHcpIndexApp.InnerHtml = listan2[currHole].hcpIndex.ToString();
            lblPlayHcpApp1.InnerHtml = listan2[currHole].erhallna.ToString();
            
        }

        private void setHcpApp()
        {
            int playedHcp = calcPlayerHcp();
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
            int plusMinus = slag - listan2[currHole].par;
            if (plusMinus > 0)
            {
                lblPlusMinus1.InnerHtml = "+" + plusMinus.ToString();
            }
            else
            {
                lblPlusMinus1.InnerHtml = plusMinus.ToString();
            }
            //Insert eller update till db
            if (InsertOrUpdate(currHole + 1) == true)
            {
                insertResults(IdHolder, currHole + 1, slag, points, netto);
            }
            else
            {
                updateResults(IdHolder, currHole + 1, slag, points, netto);
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
                int slag = (int)dr["strokes"];
                int plusMinus = slag - listan2[currHole].par;
                if (plusMinus > 0)
                {
                    lblPlusMinus1.InnerHtml = "+" + plusMinus.ToString();
                }
                else
                {
                    lblPlusMinus1.InnerHtml = plusMinus.ToString();
                }
            }
        }

        //få ut netto och räkna poäng
        //private void setPoints(List<int> allaSlag)
        //{
        //    listan.Sort();
        //    string lblhcptemp = "";
        //    string lblPlusMiunsTemp = "";
        //    //int points = 0;
        //    //int total = 0;
        //    //int totalSlag = 0;
        //    //int IdHolder = getTounamentMembId();
        //    for (int i = 0; i < 1; i++)
        //    {
        //        lblhcptemp = "lblNettoApp" + (i + 1).ToString();
        //        HtmlTableCell tc = (HtmlTableCell)resultsTable.Rows[i].Cells[4].FindControl(lblhcptemp);
        //        tc.InnerHtml = "";

        //        lblPlusMiunsTemp = "lblPlusMinus" + (i + 1).ToString();
        //        HtmlTableCell tcPM = (HtmlTableCell)resultsTable.Rows[i].Cells[5].FindControl(lblPlusMiunsTemp);
        //        tcPM.InnerHtml = ""; 
        //    }
        //    for (int i = 0; i < howManyFills().Count; i++)
        //    {
        //        int slag = allaSlag[i];
        //        int netto = slag - listan[currHole].erhallna;
                

        //        lblhcptemp = "lblNettoApp" + (i + 1).ToString();
        //        HtmlTableCell tc = (HtmlTableCell)resultsTable.Rows[i].Cells[4].FindControl(lblhcptemp);
        //        tc.InnerHtml = netto.ToString();

        //        int plusMinus = slag - listan[currHole].par;
        //        lblPlusMiunsTemp = "lblPlusMinus" + (i + 1).ToString();
        //        HtmlTableCell tcPM = (HtmlTableCell)resultsTable.Rows[i].Cells[5].FindControl(lblPlusMiunsTemp);
        //        if (plusMinus > 0)
        //        {
        //            tcPM.InnerHtml = "+" + plusMinus.ToString(); 
        //        }
        //        else
        //        {
        //            tcPM.InnerHtml = plusMinus.ToString(); 
                    
        //        }

        //        //if ((netto - listan[i].par) >= 2)
        //        //{
        //        //    points = 0;
        //        //}
        //        //else
        //        //{
        //        //    points = listan[i].par - netto + 2;
        //        //}

        //        ////insertResults(IdHolder, i + 1, allaSlag[i], points); add to list
        //        //total = total + points;
        //        //totalSlag = totalSlag + slag;
        //    }

        //    //insertTotalPoints(total);
        //    //insertTotalSlag(totalSlag);
        //}

        //Räkna ut och placera ut erhållna slag
        //private void setHcp(int playedHcp)
        //{

        //    if (playedHcp < 0)
        //    {
        //        playedHcp = playedHcp * (-1);
        //    }
        //    for (int i = 0; i < playedHcp; i++)
        //    {
        //        if (i > 17)
        //        {
        //            if (i > 35)
        //            {
        //                if (i > 53)
        //                {
        //                    listan[i - 54].erhallna++;
        //                }
        //                else
        //                {
        //                    listan[i - 36].erhallna++;
        //                }
        //            }
        //            else
        //            {
        //                listan[i - 18].erhallna++;
        //            }
        //        }
        //        else
        //        {
        //            listan[i].erhallna++;
        //        }
        //        //int showPlayerHcp = listan[i].erhallna;
        //    }
        //}

        private bool InsertOrUpdate(int hole_id) {
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


        private void FillDropdownTournament()
        {
            conn.Open();

            string sqlstring = "SELECT name, id FROM tournament";

            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();
            DropDownTourApp.Items.Clear();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Value = dr["id"].ToString();
                li.Text = dr["name"].ToString();

                DropDownTourApp.Items.Add(li);
            }
            conn.Close();
        }

        public int getTounamentMembId()
        {
            Classes.clsTournament tour = new Classes.clsTournament();
            string queryString = "SELECT id FROM tournament_member WHERE id_member = 1054 AND id_tournament = " + DropDownTourApp.SelectedValue + ";";

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
                Classes.Holes hole1 = new Classes.Holes();
                return hole1;

            }

        }

        public string getGender()
        {
            string queryString = "SELECT member.gender FROM public.member WHERE member.id = 1054;";

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
            string queryString = "SELECT member.hcp FROM public.member WHERE member.id = 1054;";

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

        private List<int> howManyFills()
        {
            int howMany = 0;
            List<int> listHowMany = new List<int>();

            int i = 0;
            foreach (HtmlTableRow td in resultsTable.Rows)
            {
                foreach (HtmlTableCell tc in td.Cells)
                {
                    if (i!=1)
                    {
                        int tempint = resultsTable.Rows.Count;

                        TextBox idStr = (TextBox)tc.FindControl("TextBox" + (i + 1).ToString());
                        if (idStr.Text != "")
                        {
                            howMany++;
                            listHowMany.Add(Convert.ToInt32(idStr.Text));
                        }
                        i++;
                    }
                    
                }
            }
            return listHowMany;
        }

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            if (rbtLstTee.SelectedValue == "")
            {
                //Var god välj tee
            }
            else
            {
                int tourMembId = getTounamentMembId();
                memb.hcp = getHcp();
                memb.gender = getGender();
                setPointsApp();
                lblPlayerPlaceApp.Text = "Nuvarande placering: " + listCurrentPlacements(currHole + 1);
            }
        }

        private int listCurrentPlacements(int id_hole)
        {

            string queryString = "SELECT row_number() OVER (Order by netto) as rnum, netto, member.id FROM public.results, public.member, public.tournament_member WHERE tournament_member.id_member = member.id AND tournament_member.id = results.id_tournament_member AND id_hole=" + id_hole + " ;";
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
                            int idTemp = Convert.ToInt32(reader["id"]);
                            if (idTemp == memb.id)
                            {
                                placement = Convert.ToInt32(reader["rnum"]);
                            }
                            
                        }
                        if (placement == 0)
                        {
                            placement = -1;
                        }
                    }
                }
            }
            return placement;
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
                        playedHcp = Convert.ToInt32(memb.hcp - 2);
                    }
                }
            }
            else
            {
                double playHandicap = ((memb.hcp * lane.courseSlope) / 113) + (lane.scratch - 72);
                playedHcp = Convert.ToInt32(playHandicap);
            }
            return playedHcp;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
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

                if (listCurrentPlacements(currHole + 1) <= 0)
                {
                    lblPlayerPlaceApp.Text = "Ange antal slag för att beräkna placering";
                }
                else
                {
                    lblPlayerPlaceApp.Text = "Nuvarande placering för hålet: " + listCurrentPlacements(currHole + 1);
                }
                
                
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
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

            if (listCurrentPlacements(currHole + 1) <= 0)
            {
                lblPlayerPlaceApp.Text = "Ange antal slag";
            }
            else
            {
                lblPlayerPlaceApp.Text = "Nuvarande placering för hålet: " + listCurrentPlacements(currHole + 1);
            }
        }

        public void updateResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto)
        {

            string queryString = "UPDATE results SET strokes=" + strokes + ", points=" + pointt + ", netto=" + netto + " WHERE id_tournament_member=" + id_tournament_member + " AND id_hole=" + id_hole + ";";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void insertResults(int id_tournament_member, int id_hole, int strokes, int pointt, int netto)
        {

            string queryString = "INSERT INTO results(id_tournament_member, id_hole, strokes, points, confirmed, netto) VALUES (" + id_tournament_member + ", " + id_hole + ", " + strokes + ", " + pointt + ", false, " + netto + ");";

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