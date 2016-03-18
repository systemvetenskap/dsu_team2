using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GolfBokning;
using System.Diagnostics;
using System.Data;

namespace GolfBokning
{
    public partial class tournamentdraw : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTournaments();
            }
            
        }
        //Gets players and starttime from chosen tournament
        public List<clsdraw> drawlist(int tournamentid)
        {
            string sql = "SELECT id_member, id_team, id_tournament, tournament.starttime FROM tournament_member INNER JOIN member ON tournament_member.id_member = member.id INNER JOIN tournament ON tournament_member.id_tournament = tournament.id where id_tournament = '" + tournamentid + "'";

            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            List<clsdraw> participants = new List<clsdraw>();
            List<Member> testMe = new List<Member>();
            while (dr.Read())
            {
                bool exist = false;
                if (dr["id_team"] != System.DBNull.Value)
                {
                    for (int i = 0; i < testMe.Count; i++)
                    {
                        if (Convert.ToInt16(dr["id_team"]) == testMe[i].teamid)
                        {
                            Member player = new Member();
                            Member team = testMe[i];

                            player.id = Convert.ToInt16(dr["id_member"]);
                            team.team.Add(player);
                            testMe[i] = team;                          
                            exist = true;
                        }
                    }

                }

                if (!exist)
                {
                    clsdraw participant = new clsdraw();
                    Member player = new Member();

                    if (dr["id_team"] != System.DBNull.Value)
                    {
                        Member team = new Member();
                        player.id = Convert.ToInt16(dr["id_member"]);
                        team.teamid = Convert.ToInt16(dr["id_team"]);
                        team.id = Convert.ToInt16(dr["id_team"]);
                        team.team.Add(player);
                        team.startTime = Convert.ToDateTime(dr["starttime"]);
                        participant.teamid = Convert.ToInt16(dr["id_team"]);
                        participant.participants.Add(team);
                        testMe.Add(team);
                    }
                    else
                    {
                        player.id = Convert.ToInt16(dr["id_member"]);
                        player.startTime = Convert.ToDateTime(dr["starttime"]);
                        testMe.Add(player);
                        exist = true;
                    }

                }

            }
            conn.Close();
            participants = new List<clsdraw>();
            for (int i = 0; i < testMe.Count; i++)
            {
                clsdraw participant = new clsdraw();
                participant.memid = testMe[i];
                participant.teamid = testMe[i].teamid;
                participant.starttime = testMe[i].startTime;
                participants.Add(participant);

            }

            return participants;
        }
        //Updates database with starttimes for players
        public void instert(List<clsdraw> list, int id)
        {
            NpgsqlConnection cn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd;
            cn.Open();
            
            foreach (clsdraw player in list)
            {
                
                DateTime date = player.starttime;
                if (player.teamid != 0)
                {
                    foreach (Member m in player.participants)
                    {
                        foreach (Member t in m.team)
                        {

                            string sql = "UPDATE tournament_member SET starttime = '" + date + "' where id_team = '" + m.teamid + "' and id_tournament = '" + id + "'";
                            cmd = new NpgsqlCommand(sql, cn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    foreach (Member m in player.participants)
                    {
                        string sql = "UPDATE tournament_member SET starttime = '" + date + "' where id_member = '" + m.id + "' and id_tournament = '" + id + "'";
                        cmd = new NpgsqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                    }
                }
                //foreach (Member m in player.participants)
                //{
                //    string sql = "UPDATE tournament_member SET starttime = '" + date + "' where id_member = '" + m.id + "' and id_tournament = '" + id + "'";
                //    cmd = new NpgsqlCommand(sql, cn);
                //    cmd.ExecuteNonQuery();
                //}
            }
            cn.Close();
        }
        //Updates database with starttimes for teams
        public void insertteam(List<clsdraw> list, int id)
        {
            NpgsqlConnection cn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            NpgsqlCommand cmd;
            cn.Open();

            foreach (clsdraw team in list)
            {
                DateTime date = team.starttime;

                foreach (Member m in team.participants)
                {
                    foreach (Member t in m.team)
                    {
                        
                        string sql = "UPDATE tournament_member SET starttime = '" + date + "' where id_team = '" + m.teamid + "' and id_tournament = '" + id + "'";
                        cmd = new NpgsqlCommand(sql, cn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            cn.Close();
        }
        //Gets tournaments and fills DropDownList
        private void LoadTournaments()
        {
            string sql = "SELECT id, name FROM tournament";
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);

            conn.Open();
            using (NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);

                tournament.DataSource = dt;
                tournament.DataTextField = "name";
                tournament.DataValueField = "id";
                tournament.DataBind();
            }
            conn.Close();

            
        }
        //Gives players a random starttime
        public List<clsdraw> randomtime(List<clsdraw> participants, int nrplayer, int timebetwenballs)
        {
            List<clsdraw> drawings = new List<clsdraw>();
            DateTime date = participants[0].starttime;
            List<clsdraw> p = participants;
            p.Shuffle();
            int maxplayer;
            if (nrplayer == 1)
            {
                maxplayer = 0;
            }
            else
            {
                maxplayer = 1;
            }
            
            double minutes = timebetwenballs;
            bool exist = false;
            foreach (clsdraw i in p)
            {
                if (maxplayer == nrplayer)
                {
                    date = date.AddMinutes(minutes);
                    exist = false;
                }

                for (int j = 0; j < drawings.Count; j++)
                {
                    if (drawings[j].starttime == date)
                    {
                        Member rndplayer = i.memid;
                        drawings[j].starttime = date;
                        drawings[j].participants.Add(rndplayer);
                        maxplayer++;
                        exist = true;
                    }
                }

                if (!exist)
                {
                    clsdraw draw = new clsdraw();
                    Member randplayer = i.memid;
                    draw.starttime = date;
                    draw.participants.Add(randplayer);
                    draw.teamid = i.teamid;
                    drawings.Add(draw);
                    maxplayer = 1;
                    exist = true;
                }

            }
            return drawings;

        }
        
        protected void btnShuffle_Click(object sender, EventArgs e)
        {
            List<clsdraw> randomplayers = new List<clsdraw>();
            int tournid = Convert.ToInt16(tournament.SelectedValue);
            int nmbplayers = Convert.ToInt16(nrplayerperball.Text);
            int timespace = Convert.ToInt32(time.Text);
            
            randomplayers = randomtime(drawlist(tournid), nmbplayers, timespace);

            instert(randomplayers, tournid);
            getplayersfortournament(tournid);
            //insertteam(randomplayers, tournid);
        }

        //protected void shuffleteam_Click(object sender, EventArgs e)
        //{
        //    List<clsdraw> randomplayers = new List<clsdraw>();
        //    int tournid = Convert.ToInt16(tournament.SelectedValue);
        //    int nmbplayers = Convert.ToInt16(nrplayerperball.Text);
        //    int timespace = Convert.ToInt32(time.Text);

        //    randomplayers = randomtime(drawlist(tournid), nmbplayers, timespace);

        //    //instert(randomplayers, tournid);
        //    insertteam(randomplayers, tournid);
        //}

        public void getplayersfortournament(int id)
        {
            string sql = "SELECT to_char(tournament_member.starttime, 'HH24:MI:SS') AS starttid, member.golf_id, member.firstname, member.lastname, tournament.name FROM tournament_member INNER JOIN member on tournament_member.id_member = member.id INNER JOIN tournament ON tournament_member.id_tournament = tournament.id where tournament_member.id_tournament = '" + id +"' ORDER BY tournament_member.starttime";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);            
            conn.Open();
            
            DataTable dt = new DataTable();

            using(NpgsqlDataAdapter adp = new NpgsqlDataAdapter(sql, conn))
            {
                adp.Fill(dt);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            
        }

        protected void tournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    getplayersfortournament(Convert.ToInt16(tournament.SelectedValue));
        //}
    }
}