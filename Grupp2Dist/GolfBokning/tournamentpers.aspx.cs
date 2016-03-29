using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace GolfBokning
{
    public partial class tournamentpers : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        public string SuggestionListAll = "";
        public string SuggestionListDefault = "";
        public string SuggestionListCho = "";
        public string SuggestionListChoSort = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownListTournament.SelectedIndexChanged += new EventHandler(DropDownListTournament_SelectedIndexChanged);
            ButtonUpdate.Click += new EventHandler(ButtonUpdate_Click);
            DropLiTeam.SelectedIndexChanged += new EventHandler(DropLiTeam_SelectedIndexChanged);
            ButtonNewTeam.Click += new EventHandler(ButtonNewTeam_Click);
            //DropDownListTournament.AutoPostBack = true;

            if (!IsPostBack)
            {
                if (DropDownListTournament.Items.Count == 0)
                {
                    DropDownListTournament.Items.Add(new ListItem("Välj tävling...", ""));
                    DropDownListTournament.SelectedIndex = 0;
                    GetAllTournaments();
                }
            }
            
            ListBoxChosen.Height = 300;
            ListBoxAll.Height = 300;
            TeamFormst.Visible = false;
            PanelNTeam.Visible = false;
            //PanelResponse.Visible = false;
            //PanelHideShow.Visible = false;
        }

        private void GetAllMembers()
        {
            ListBoxAll.Items.Clear();
            string sql = "SELECT * FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname ASC";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Value = dr["id"].ToString();
                li.Text = dr["firstname"].ToString() + " " + dr["lastname"].ToString() + " (" + dr["golf_id"].ToString() + ")";
                ListBoxAll.Items.Add(li);
            }

            conn.Close();
        }

        private void GetAllTournaments()
        {
            string sql = "SELECT * FROM tournament ORDER BY id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["name"].ToString();
                li.Value = dr["id"].ToString();
                DropDownListTournament.Items.Add(li);
            }
            conn.Close();
            PanelHideShow.Visible = true;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {

        }
        private void ButtonNewTeam_Click(object sender, EventArgs e)
        {
            if (txtTeamName.Text != "")
            {
                TeamFormst.Visible = true;
                int selectedTour = int.Parse(DropDownListTournament.SelectedValue);
                PanelNTeam.Visible = true;
                string txt = txtTeamName.Text;
                using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
                {
                    cnn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO team (name, id_tournament) VALUES (@name, @tId)", cnn))
                    {
                        cmd.Parameters.AddWithValue("@name", txt);
                        cmd.Parameters.AddWithValue("@tId", selectedTour);
                        cmd.ExecuteNonQuery();
                    }
                }
                DropLiTeam.Items.Clear();
                DropLiTeam.Items.Add(new ListItem("Välj lag...", ""));
                filTeamDrop(int.Parse(DropDownListTournament.SelectedValue));
                txtTeamName.Text = "";
            }
        }

        [System.Web.Services.WebMethod]
        public static string MyFunction(string sendString, string teamID)
        {
            string sen = sendString;
            string val = Regex.Match(sendString, @"(_).*?(_)").ToString();
            val = val.Replace("_", "");
            sendString = Regex.Replace(sendString, @"(_).*?(_)", "");
            string[] split = sendString.Split(';');
            string output = "";
            List<string> liId = new List<string>();

            string sql3 = "(SELECT  id FROM tournament_member WHERE NOT EXISTS (select distinct id_tournament_member from results where results.id_tournament_member = tournament_member.id ) and tournament_member.id_tournament = " + val + ");";
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            conn.Open();
            NpgsqlCommand cmd2 = new NpgsqlCommand(sql3, conn);
            if (teamID != "0" && isTeamComp(int.Parse(val)))
            {
                return entryTeams(sen, int.Parse(teamID));
            }
            NpgsqlDataReader dr = cmd2.ExecuteReader();
            while(dr.Read())
            {
                string sql2 = "DELETE FROM tournament_member WHERE id = " + dr[0];
                NpgsqlConnection conn2 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
                conn2.Open();
                NpgsqlCommand cmd3 = new NpgsqlCommand(sql2, conn2);
                cmd3.ExecuteNonQuery();
                conn2.Close();
            }

            
            
            

            
           
            conn.Close();

            foreach (string spl in split)
            {
                if (spl != "")
                {
                    string sql = "SELECT admc(@id_tournament, @id_member)";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(val));
                    cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(spl));
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                       // Debug.WriteLine(dr[0].ToString());
                    }

                    //int id = (int)cmd.ExecuteScalar();

                    //if (id != 0)
                    //{
                    //    output = "OK";
                    //}
                    conn.Close();
                }
            }

            return output;
        }

        [System.Web.Services.WebMethod]
        public static string canBeDeleted(int tourID, int membID)
        {
            string sql = "SELECT  id FROM tournament_member WHERE NOT EXISTS (select distinct id_tournament_member from results where results.id_tournament_member = tournament_member.id ) and tournament_member.id_tournament = " + 
                tourID + " and tournament_member.id_member =" + membID;
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                using(NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }
                }
                
            }




        }
        private static string entryTeams(string sendString, int teamID)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            // Fixat från: "(_._)"
            string val = Regex.Match(sendString, @"(_).*?(_)").ToString();
            val = val.Replace("_", "");
            sendString = Regex.Replace(sendString, @"(_).*?(_)", "");
            string[] split = sendString.Split(';');
            string output = "";

            string sql2 = "DELETE FROM tournament_member WHERE id_team = " + teamID;

            NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, conn);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();


            foreach (string spl in split)
            {
                if (spl != "")
                {
                    string sql = "INSERT INTO tournament_member (id_tournament, id_member, id_team) VALUES (@id_tour, @id_mem, @it_tem) RETURNING id";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id_tour", Convert.ToInt16(val));
                    cmd.Parameters.AddWithValue("@id_mem", Convert.ToInt16(spl));
                    cmd.Parameters.AddWithValue("@it_tem", teamID);

                    int id = (int)cmd.ExecuteScalar();

                    if (id != 0)
                    {
                        output = "OK";
                    }
                    conn.Close();
                }
            }


            return output;
        }
        
        private void DropDownListTournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (DropDownListTournament.SelectedValue != "")
            {
                PanelNTeam.Visible = true;
                Label16.Text = "<h2>Deltagare</h2>";
                GetSuggAll();
                GetDefault();
                GetSuggChosen();
                GetSuggChosenSort();
                PanelHideShow.Visible = true;
                TeamFormst.Visible = false;
                DropLiTeam.Items.Clear();
                if (isTeamComp(int.Parse(DropDownListTournament.SelectedValue)))
                {
                    DropLiTeam.Items.Add(new ListItem("Välj lag...", ""));
                    PanelNTeam.Visible = false;
                    TeamFormst.Visible = true;
                    filTeamDrop(int.Parse(DropDownListTournament.SelectedValue));
                }
            }
        }
        private void DropLiTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropLiTeam.SelectedValue != "")
            {
                PanelNTeam.Visible = true;
                TeamFormst.Visible = true;
                Label16.Text = "<h2>Deltagare i " + DropLiTeam.SelectedItem.Text + "</h2>";
                int teamID = int.Parse(DropLiTeam.SelectedValue);
                getTeamMembers(teamID); //Adds team members 
                GetSuggAll();
                GetDefault();
            }
            else
            {
                TeamFormst.Visible = true;
            }

            txtTeamName.Text = "";
        }
        private void ButtonRemove_Click(object sender, EventArgs e)
        {

        }

        private void RemoveFromTournamentMember(int id_tournament)
        {
            conn.Open();
            string sql = "DELETE FROM tournament_member WHERE id_tournament = " + id_tournament;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Close();
        }
        private void filTeamDrop(int id)
        {
            string queryString = "SELECT id, name FROM team where id_tournament=@tourID ";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@tourID", id);
                    connection.Open();
                    using (NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        while(dr.Read())
                        {
                            ListItem liIt = new ListItem();
                            liIt.Value = dr[0].ToString();
                            liIt.Text = (string)dr[1];
                            DropLiTeam.Items.Add(liIt);
                        }

                    }

                }
            }
        }

        public static bool isTeamComp(int id)
        {
            string queryString = "SELECT isteam FROM tournament where id=@tourID ";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@tourID", id);
                    connection.Open();
                    using(NpgsqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return (bool)dr[0];
                        }
                        else
                        {
                            return false;
                        }

                    }
                   
                }
            }
        }
        private void getTeamMembers(int teamID)
        {
            string id = DropDownListTournament.SelectedValue;
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam, member.id FROM tournament_member " +
" INNER JOIN member on tournament_member.id_member = member.id where  tournament_member.id_team = @teID";

            ListBoxChosen.Items.Clear();
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@teID", teamID);
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem liIt = new ListItem();
                            liIt.Value = reader[1].ToString();
                            liIt.Text = reader[0].ToString();

                            ListBoxChosen.Items.Add(liIt);
                        }
                    }
                }
            }
        }

        public void GetDefault()
        {

            //string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member " +
            " LEFT  JOIN tournament_member ON (member.id = tournament_member.id_member and tournament_member.id_tournament = @tourID) " +
            " WHERE  NOT EXISTS " +
            " (SELECT id_member FROM tournament_member " +
            " WHERE member.id = tournament_member.id_member and tournament_member.id_tournament = @tourID) ORDER BY firstname asc";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@tourID", DropDownListTournament.SelectedValue);
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(SuggestionListDefault))
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListDefault += "\"<option value='" + value + "'>" + name + "</option>\"";
                            }
                            else
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListDefault += ", \"<option value='" + value + "'>" + name + "</option>\"";
                            }
                        }
                    }
                }
            }
        }

        public void GetSuggAll()
        {

          //  string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member " +
" LEFT  JOIN tournament_member ON (member.id = tournament_member.id_member and tournament_member.id_tournament = @tourID) " +
 " WHERE  NOT EXISTS " +
   " (SELECT id_member FROM tournament_member " +
    " WHERE member.id = tournament_member.id_member and tournament_member.id_tournament = @tourID) ORDER BY firstname asc";




            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@tourID", DropDownListTournament.SelectedValue);
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(SuggestionListAll))
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                //value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListAll += "\"" + name + ":" + value + "\"";
                            }
                            else
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                //value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListAll += ", \"" + name + ":" + value + "\"";
                            }
                        }
                    }
                }
            }
        }

        // Sorted on available members
        public void GetSuggChosenSort() 
        {
            string id = DropDownListTournament.SelectedValue;
                                  
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (tournament_member.id_member = member.id) WHERE tournament_member.id_tournament = " + id + " AND tournament_member.id_member = member.id ORDER BY member.firstname";
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(SuggestionListChoSort))
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                //value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListChoSort += "\"" + name + ":" + value + "\"";
                            }
                            else
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                //value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListChoSort += ", \"" + name + ":" + value + "\"";
                            }
                        }
                    }
                }
            }
        }

        public void GetSuggChosen() //Fills the member tab with members that already have benn entried
        {
            string id = DropDownListTournament.SelectedValue;
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (tournament_member.id_member = member.id) WHERE tournament_member.id_tournament = " + id + " AND tournament_member.id_member = member.id ORDER BY member.firstname";
            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.IsNullOrEmpty(SuggestionListCho))
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListCho += "\"<option value='" + value + "'>" + name + "</option>;\"";
                            }
                            else
                            {
                                string output = reader["fnam"].ToString();
                                string name = Regex.Replace(output, @"(.\(.*$)", "");

                                string value = Regex.Replace(output, @"^(.(?!(\(.*)))* ", "");
                                value = value.Replace("(", "");
                                value = value.Replace(")", "");
                                value = Regex.Replace(value, @"^[^_]+_", "");
                                SuggestionListCho += ", \"<option value='" + value + "'>" + name + "</option>;\"";
                            }
                        }
                    }
                }
            }
        }
    }
}