using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
namespace GolfBokning
{
    public partial class tournamententry : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        public string SuggestionList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getQuerryExample();
        }
        [System.Web.Services.WebMethod]
        public static string fillTable()
        {
            string htRo = "";
            //int id = (int)HttpContext.Current.Session["_id"];
            List<Classes.clsTournament> liTour = new List<Classes.clsTournament>();
            Classes.clsTournament clTour; // = new Classes.clsTournament();

            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name, description, id FROM tournament", cnn))
                {
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            clTour = new Classes.clsTournament();
                            clTour.eventName = dr[0].ToString();
                            liTour.Add(clTour);
                            htRo += "<tr>";
                            htRo += "<td>" + dr["name"].ToString() + "</td>";
                            htRo += "<td>" + /*dr["dat"].ToString().Substring(0, 10)*/ "Datum" + "</td>";

                            htRo += "<td> <button class='btn btn-danger' onclick='showEntry(" + '"' + dr["id"].ToString() + '"' + "); return false;'>Avboka tid </button></td>";

                            htRo += "</tr>";
                        }
                    }
                }
            }
            return htRo;
        }

        [System.Web.Services.WebMethod]
        public static string addTeam(string id, string nam)
        {

            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();

                using (NpgsqlCommand cmd2 = new NpgsqlCommand("INSERT INTO team (id_tournament, name) VALUES ('" + id + "', '" + nam + "')", cnn))
                {
                    cmd2.ExecuteNonQuery();
                }

            }


            return id + "name " + nam; // htRo + "\n" + teaMid;
        }

        [System.Web.Services.WebMethod]
        public static string BookMan(string id, string inp, string teaMid)
        {
            string htRo = "";
            string[] splInp = inp.Split(';');
            for (int i = 0; i < splInp.Length; i++)
            {
                htRo += splInp[i] + " : ";
            }

            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                for (int i = 0; i < splInp.Length; i++)
                {
                    if (splInp[i].Length > 0)
                    {

                        int aa = 0;
                        if (splInp[i].IndexOf("_") > 0)
                        {
                            using (NpgsqlCommand cmd2 = new NpgsqlCommand("INSERT INTO tournament_member (id_tournament, id_member, id_team) VALUES ('" + id + "', (SELECT ID FROM member where golf_id = '" + splInp[i] + "'), '" + teaMid + "')", cnn))
                            {
                                cmd2.ExecuteNonQuery();
                            }
                        }
                        else
                        {

                        }


                    }
                }

            }
            return htRo + "\n" + teaMid;
        }//SELECT  id,name FROM team where id_tournament = '4'

        [System.Web.Services.WebMethod]
        public static string getTeams(string id)
        {
            string htRo = "";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();


                string sql = "SELECT  id,name FROM team where id_tournament = '" + id + "'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {


                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        //<option value="">saa</option>
                        while (dr.Read())
                        {
                            htRo += "<option value='" + dr["id"] + "'>" + dr["name"] + "</option> |";
                        }
                    }
                }
            }
            return htRo;
        }
        [System.Web.Services.WebMethod]
        public static string getMember(string id)
        {
            string htRo = "";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();


                string sql = "SELECT  member.id as membi, (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam from tournament_member " +
" INNER JOIN member on tournament_member.id_member = member.id  " +
" Where tournament_member.id_team = " + id;
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {


                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        //<option value="">saa</option>
                        while (dr.Read())
                        {
                            htRo += "<option value='" + dr["membi"] + "'>" + dr["fnam"] + "</option> |";
                        }
                    }
                }
            }
            return htRo;
        }

        public void getQuerryExample()
        {

            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member  ORDER BY firstname";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {

                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {

                    connection.Open();

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {

                            if (string.IsNullOrEmpty(SuggestionList))
                            {
                                SuggestionList += "\"" + reader["fnam"].ToString() + "\"";
                            }
                            else
                            {
                                SuggestionList += ", \"" + reader["fnam"].ToString() + "\"";
                            }

                        }
                    }
                }

            }
        }
    }
}