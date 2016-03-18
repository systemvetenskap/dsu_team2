using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Text.RegularExpressions;

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

            //DropDownListTournament.AutoPostBack = true;

            if (!IsPostBack)
            {
                if (DropDownListTournament.Items.Count == 0)
                {
                    GetAllTournaments();
                }
            }

            ListBoxChosen.Height = 300;
            ListBoxAll.Height = 300;

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

        [System.Web.Services.WebMethod]
        public static string MyFunction(string sendString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string val = Regex.Match(sendString, @"(_._)").ToString();
            val = val.Replace("_", "");
            sendString = Regex.Replace(sendString, @"(_._)", "");
            string[] split = sendString.Split(';');
            string output = "";

            string sql2 = "DELETE FROM tournament_member WHERE id_tournament = " + Convert.ToInt16(val);

            NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, conn);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();


            foreach (string spl in split)
            {
                if (spl != "")
                {
                    string sql = "INSERT INTO tournament_member (id_tournament, id_member) VALUES (@id_tournament, @id_member) RETURNING id";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(val));
                    cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(spl));

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
            GetSuggAll();
            GetDefault();
            GetSuggChosen();
            GetSuggChosenSort();
            PanelHideShow.Visible = true;
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            RemoveFromTournamentMember(Convert.ToInt16(DropDownListTournament.SelectedValue));
        }

        private void RemoveFromTournamentMember(int id_tournament)
        {
            conn.Open();
            string sql = "DELETE FROM tournament_member WHERE id_tournament = " + id_tournament;
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Close();
        }

        public void GetDefault()
        {

            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
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

            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
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

        public void GetSuggChosen()
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