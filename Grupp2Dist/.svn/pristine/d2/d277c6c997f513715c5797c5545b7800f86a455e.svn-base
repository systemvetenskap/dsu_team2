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
    public partial class tournamentresult : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        List<Member> listmemb = new List<Member>();
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        int memberIdDrop = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelTournamentName.Text = tournamentName;
            DropDownTournament.SelectedIndexChanged += new EventHandler(DropDownTournament_SelectedIndexChanged);

            if (!IsPostBack)
            {
                FillDropdownTournament();


                //getMembForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
                if (DropDownTournament.Items.Count < 1)
                {
                    return;
                }
                LabelResultTabel.Text = CollectTournamentInfo(Convert.ToInt16(DropDownTournament.SelectedValue));
                getTeamForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
                getMembForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
            }
            if (DropDownTournament.Items.Count < 1)
            {
                return;
            }

            DropDownTournament.AutoPostBack = true;

        }

        //string tournamentName = "Superturneringen <br/>";

        protected void DropDownTournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool ifTeam = isTeam();

            GridViewScore.DataSource = null;
            GridViewScore.DataBind();
            ListBox1.Items.Clear();
            if (ifTeam == true)
            {
                getTeamForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
            }
            else
            {
                getMembForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
            }
            memberIdDrop = Convert.ToInt32(DropDownTournament.SelectedValue);
            LabelResultTabel.Text = CollectTournamentInfo(Convert.ToInt16(DropDownTournament.SelectedValue)); //Niklas variant
            //Classes.clsTournament tournament = new Classes.clsTournament();
            //tournament = Convert.ToInt32DropDownTournament.SelectedItem;
            // tournament.id = Convert.ToInt32(DropDownTournament.SelectedItem);  får inte göra så här!!!!
            //LabelResultTabel.Text = CollectTournamentInfo(Convert.ToInt32(DropDownTournament.SelectedValue));   
        }

        //metod fyll turneringstabell
        private string CollectTournamentInfo(int id)//out med personId
        {
            //int id = Convert.ToInt16(DropDownTournament.SelectedValue);
            conn.Open();
            bool team = isTeam();
            string nyRad;
            if (team == false)
            {
                string sqlstring = "SELECT member.id, member.firstname, member.lastname, tournament_member.nettototal FROM public.member, public.tournament, public.tournament_member WHERE tournament_member.confirmed_total = true AND member.id = tournament_member.id_member AND tournament_member.id_tournament = tournament.id AND tournament.id = " + id + " ORDER BY tournament_member.nettototal ASC, member.hcp ASC;";


                NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
                NpgsqlDataReader dr = command.ExecuteReader();

                //string nyRad = "<tr><td>Placering</td><td>Efternamn</td><td>Förnamn</td><td>Poäng</td></tr>";
                nyRad = "<tr><td>Placering</td><td>Efternamn</td><td>Förnamn</td><td>Slag</td></tr>";
                int place = 0;
                int i = 0;
                while (dr.Read())
                {
                    Member newMember = new Member();
                    string lastname = dr["lastname"].ToString();
                    string firstname = dr["firstname"].ToString();

                    //if (dr["points"] != System.DBNull.Value)
                    if (dr["nettototal"] != System.DBNull.Value)
                    {
                        newMember.id = (int)dr["id"];
                        place++;
                        //int points = Convert.ToInt32(dr["points"]);
                        int nettototal = Convert.ToInt32(dr["nettototal"]);
                        // nyRad += "<tr data="+ '"' + i + '"' + "><td>" + place + "</td><td>" + lastname + "</td><td>" + firstname + "</td><td>" + points + "</td></tr>";
                        nyRad += "<tr data=" + '"' + i + '"' + "><td>" + place + "</td><td>" + lastname + "</td><td>" + firstname + "</td><td>" + nettototal + "</td></tr>";
                        i++;
                        listmemb.Add(newMember);
                    }


                }

            }

            else
            {


                string sqlstring = "SELECT name, total_points FROM team WHERE team.id_tournament=" + id + " ORDER BY total_points DESC";

                NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
                NpgsqlDataReader dr = command.ExecuteReader();

                //string nyRad = "<tr><td>Placering</td><td>Efternamn</td><td>Förnamn</td><td>Poäng</td></tr>";
                nyRad = "<tr><td>Placering</td><td>Lagnamn</td><td>poäng<td></tr>";
                int place = 0;
                int i = 0;
                while (dr.Read())
                {
                    // Member newMember = new Member();
                    string teamname = dr["name"].ToString();

                    if (dr["total_points"] != System.DBNull.Value)
                    {
                        //newMember.id = (int)dr["id"]; //hur gör jag med team här???
                        place++;
                        int points = Convert.ToInt32(dr["total_points"]);
                        //int nettototal = Convert.ToInt32(dr["nettototal"]);
                        // nyRad += "<tr data="+ '"' + i + '"' + "><td>" + place + "</td><td>" + lastname + "</td><td>" + firstname + "</td><td>" + points + "</td></tr>";
                        nyRad += "<tr data=" + '"' + i + '"' + "><td>" + place + "</td><td>" + teamname + "</td><td>" + points + "</td></tr>";
                        i++;
                        // listmemb.Add(newMember);
                    }


                }


            }
            conn.Close();
            string cssProfile = "table table-bordered table-striped";

            string tabell = "<table class=" + '"' + cssProfile + '"' + " >" + nyRad + "</table>";
            return tabell;


        }

        private DataTable getScoreCard(int membId, int tourId)
        {
            string queryString = "SELECT results.id_hole AS hole_nr, results.strokes AS slag FROM public.results, public.member, public.tournament_member, public.tournament WHERE tournament_member.confirmed_total = true AND tournament_member.id = results.id_tournament_member AND tournament_member.id_member = member.id AND tournament_member.id_tournament = tournament.id AND member.id = " + membId + " AND tournament_member.id_tournament = " + tourId + " ORDER BY results.id_hole;";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection))
            {
                adp.Fill(dt);

            }
            connection.Close();
            return dt;
        }


        private void getMembForTour(int id)
        {
            //ListBox1.SelectedIndex = 0;
            string queryString = "SELECT tournament_member.nettototal, tournament_member.id_member, member.firstname, member.lastname FROM public.tournament_member, public.member WHERE tournament_member.confirmed_total = true AND tournament_member.id_member = member.id AND tournament_member.id_tournament = " + id + ";";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection))
            {
                adp.Fill(dt);

            }
            foreach (DataRow row in dt.Rows)
            {
                if (row["nettototal"].ToString() != "")
                {
                    ListItem lt = new ListItem();

                    lt.Text = row["firstname"] + " " + row["lastname"];
                    lt.Value = row["id_member"].ToString(); ;
                    ListBox1.Items.Add(lt);
                }

            }
            connection.Close();

        }

        //metod hämta lag till turnering

        private void getTeamForTour(int id)
        {//SELECT name, total_points, id FROM team WHERE id_tournament = 4


            string queryString = "SELECT name, total_points, id FROM team WHERE id_tournament=" + id + ";";//"SELECT tournament_member.nettototal, tournament_member.id_member, member.firstname, member.lastname FROM public.tournament_member, public.member WHERE tournament_member.confirmed_total = true AND tournament_member.id_member = member.id AND tournament_member.id_tournament = " + id + ";";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection))
            {
                adp.Fill(dt);

            }
            foreach (DataRow row in dt.Rows)
            {
                if (row["total_points"].ToString() != "")
                {
                    ListItem lt = new ListItem();

                    lt.Text = row["name"].ToString();
                    lt.Value = row["id"].ToString();
                    ListBox1.Items.Add(lt);
                }

            }
            connection.Close();
        }




        //metod fyll dropdown med turneringar
        private void FillDropdownTournament()
        {
            conn.Open();

            string sqlstring = "SELECT name, id FROM tournament WHERE date <= Now() ORDER BY date ASC, starttime ASC";

            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            //List<Classes.clsTournament> tournamentList = new List<Classes.clsTournament>();

            //  Classes.clsTournament tournament = new Classes.clsTournament();

            DropDownTournament.Items.Clear();

            while (dr.Read())
            {
                ListItem li = new ListItem();

                li.Value = dr["id"].ToString();
                li.Text = dr["name"].ToString();//Niklas förslag

                DropDownTournament.Items.Add(li);
                //tournament.eventName = dr["name"].ToString();  
                //tournament.id = Convert.ToInt32(dr["id"]);
                ////tournamentList.Add(tournament); 
                //DropDownTournament.Items.Add(tournament);

                //DropDownTournament.DataTextField = dr["name"].ToString(); Jimmy
                //DropDownTournament.DataValueField = dr["id"].ToString(); Jimmy

            }

            conn.Close();

            //    return tournamentList; //Glöm inte att fylla i !!!!!!!!!!11
        }



        //metod fyll persontabell
        private string CollectPersonlalTournamentInfo(int personId, int tavlingsId)   //skapa en label brevid den andra och fyll med denna tabell
        {
            conn.Open();

            string sqlstring = "";


            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            string nyRad = "<tr><td>Hål</td><td>Poäng</td></tr>";


            while (dr.Read())
            {

                string hole = dr[""].ToString();
                //string points = dr[""].ToString();

                if (dr["points"] != System.DBNull.Value)
                {

                    int points = Convert.ToInt32(dr["points"]);
                    nyRad += "<tr><td>" + hole + "</td><td>" + points + "</td></tr>";
                }


            }

            conn.Close();
            string cssProfile = "table table-bordered table-striped";

            string tabellPerson = "<table class=" + '"' + cssProfile + '"' + ">" + nyRad + "</table>";
            return tabellPerson;



        }


        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gör metod  getScoreCardTeam... få med mer info i selecten....  
            bool team = isTeam();
            if (team == true)
            {
                string hole = "";
                List<string> lstPoints = new List<string>();

                //GridViewScore.DataSource = getScoreCardTeam(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
                //GridViewScore.DataBind();
                for (int i = 1; i <= 18; i++)
                {


                    hole = "Hål Nr: " + i.ToString() + " | Poängsumma för hålet: ";
                    lstPoints.Add(hole + calcTeamScoreForHole(i).ToString());

                    //GridViewScore.Rows[i].Cells[0].Text = "Hål " + (i + 1).ToString();
                    //GridViewScore.Rows[i].Cells[1].Text = calcTeamScoreForHole(i).ToString();
                }
                GridViewScore.DataSource = lstPoints;
                GridViewScore.DataBind();
                //GridViewScore.DataBind();
                //GridViewScore.DataSource = getScoreCardTeam(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
                //GridViewScore.DataBind();
            }

            else
            {
                GridViewScore.DataSource = getScoreCard(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
                GridViewScore.DataBind();
            }

        }

        //private void fillInfo()
        //{
        //    bool team = isTeam();
        //    if (team == true)
        //    {
        //        GridViewScore.DataSource = getScoreCardTeam(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
        //        GridViewScore.DataBind();
        //        for (int i = 0; i < 18; i++)
        //        {
        //            GridViewScore.Rows[i].Cells[0].Text = "Hål " + (i + 1).ToString();
        //            GridViewScore.Rows[i].Cells[1].Text = calcTeamScoreForHole(i).ToString();
        //        }
        //        //GridViewScore.DataBind();
        //        //GridViewScore.DataSource = getScoreCardTeam(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
        //        //GridViewScore.DataBind();
        //    }

        //    else
        //    {
        //        GridViewScore.DataSource = getScoreCard(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
        //        GridViewScore.DataBind();
        //    }
        //}



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

        private DataTable getScoreCardTeam(int membId, int tourId)
        {

            // string queryString = "SELECT results.id_hole AS hole_nr, results.strokes AS slag FROM public.results, public.member, public.tournament_member, public.tournament WHERE tournament_member.confirmed_total = true AND tournament_member.id = results.id_tournament_member AND tournament_member.id_member = member.id AND tournament_member.id_tournament = tournament.id AND member.id = " + membId + " AND tournament_member.id_tournament = " + tourId + ";";
            string queryString = "SELECT results.id_hole as hole_nr, results.points FROM public.results, public.tournament_member WHERE results.id_tournament_member = tournament_member.id_tournament AND tournament_member.id_tournament = 6 LIMIT 18";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection))
            {
                adp.Fill(dt);

            }

            connection.Close();
            return dt;
        }


        private bool isTeam()
        {
            bool ifTeam = false;
            string queryString = "SELECT isteam FROM public.tournament WHERE id=" + DropDownTournament.SelectedValue + ";";

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
    }
}