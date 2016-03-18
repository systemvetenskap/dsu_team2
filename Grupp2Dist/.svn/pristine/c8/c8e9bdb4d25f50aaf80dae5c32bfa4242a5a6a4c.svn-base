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
                LabelResultTabel.Text = CollectTournamentInfo(Convert.ToInt16(DropDownTournament.SelectedValue));
                getMembForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
            }
            

            DropDownTournament.AutoPostBack = true;
           // DropDownTournament.SelectedValue
            //DropDownTournament.Items.Add(clsTournament.eventName);
            //DropDownTournament.DataSource =FilltournamentList();
            //DropDownTournament.DataValueField = "id";
            //DropDownTournament.DataTextField = "eventName";
           // DropDownTournament.DataBind(); 
           // DropDownTournament.SelectedIndexChanged += new EventHandler(DropDownTournament_SelectedIndexChanged);
            //DropDownTournament .SelectedIndexChanged += new EventHandler(DropDownList_SelectedIndexChanged);

        }

        //string tournamentName = "Superturneringen <br/>";

        protected void DropDownTournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewScore.DataSource = null;
            GridViewScore.DataBind();
            ListBox1.Items.Clear();
            getMembForTour(Convert.ToInt32(DropDownTournament.SelectedValue));
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

            string sqlstring = "SELECT member.id, member.firstname, member.lastname, tournament_member.nettototal FROM public.member, public.tournament, public.tournament_member WHERE tournament_member.confirmed_total = true AND member.id = tournament_member.id_member AND tournament_member.id_tournament = tournament.id AND tournament.id = " + id + " ORDER BY tournament_member.nettototal ASC, member.hcp ASC;";


            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();
            
            //string nyRad = "<tr><td>Placering</td><td>Efternamn</td><td>Förnamn</td><td>Poäng</td></tr>";
            string nyRad = "<tr><td>Placering</td><td>Efternamn</td><td>Förnamn</td><td>Slag</td></tr>";
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

            conn.Close();
            string cssProfile = "table table-bordered table-striped";

            string tabell = "<table class=" + '"' + cssProfile + '"' + " >" + nyRad + "</table>";
            return tabell;

        }

        private DataTable getScoreCard (int membId, int tourId){

            string queryString = "SELECT results.id_hole AS hole_nr, results.strokes AS slag FROM public.results, public.member, public.tournament_member, public.tournament WHERE tournament_member.confirmed_total = true AND tournament_member.id = results.id_tournament_member AND tournament_member.id_member = member.id AND tournament_member.id_tournament = tournament.id AND member.id = " + membId + " AND tournament_member.id_tournament = " + tourId + ";";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection))
            {
                adp.Fill(dt);

            }
            return dt;
        }


        private void getMembForTour(int id)
        {
            string queryString = "SELECT tournament_member.nettototal, tournament_member.id_member, member.firstname, member.lastname FROM public.tournament_member, public.member WHERE tournament_member.confirmed_total = true AND tournament_member.id_member = member.id AND tournament_member.id_tournament = " + id + ";";

            NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            connection.Open();
            DataTable dt = new DataTable();

            using(NpgsqlDataAdapter adp = new NpgsqlDataAdapter(queryString, connection)){
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
            GridViewScore.DataSource = getScoreCard(Convert.ToInt32(ListBox1.SelectedValue), Convert.ToInt32(DropDownTournament.SelectedValue));
            GridViewScore.DataBind();
        }

    }
}