using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;
using System.Text.RegularExpressions;

namespace GolfBokning
{
    public partial class staffmember : System.Web.UI.Page
    {
        public string SuggestionListDefault = "";
        public string SuggestionListCho = "";
        public string SuggestionListAll = "";
        public string SuggestionListChoSort = "";

        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //TextBoxSearch.TextChanged += new EventHandler(TextBoxSearch_TextChanged);
            //TextBoxStaffSearch.TextChanged += new EventHandler(TextBoxStaffSearch_TextChanged);
            //BtnAdd.Click += BtnAdd_Click;
            //BtnRemove.Click += BtnRemove_Click;
            //ButtonSave.Click += new EventHandler(ButtonSave_Click);

            if (ListBoxMembers.Items.Count == 0)
            {
                //getAll(false, ListBoxMembers);
            }
            
            ListBoxMembers.Height = 200;

            if (ListBoxStaff.Items.Count == 0)
            {
                //getAll(true, ListBoxStaff);
            }

            GetDefault();
            GetSuggChosen();
            GetSuggAll();
            GetSuggChosenSort();

            ListBoxStaff.Height = 200;
            //LabelTest.Visible = false;
        }

        [System.Web.Services.WebMethod]
        public static string SaveDb(string sendString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string sqlRemove = "UPDATE member SET staff = FALSE RETURNING id";
            NpgsqlCommand cmdRemove = new NpgsqlCommand(sqlRemove, conn);
            conn.Open();
            int idRemove = (int)cmdRemove.ExecuteScalar();
            conn.Close();
            string output = "N";
            sendString = Regex.Replace(sendString, @"(_).*?(_)", "");
            string[] split = sendString.Split(';');
            foreach (string spl in split)
            {
                if (spl != "")
                {
                    conn.Open();
                    string sql = "UPDATE member SET staff = TRUE WHERE id = "+Convert.ToInt16(spl)+" RETURNING id";
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    int id = (int)cmd.ExecuteScalar();
                    conn.Close();
                    output = "Y";
                }
            }
            return output;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

        }

        public void GetDefault()
        {

            //string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member WHERE staff = false ORDER BY firstname asc";

            using (NpgsqlConnection connection = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(queryString, connection))
                {
                    //command.Parameters.AddWithValue("@tourID", DropDownListTournament.SelectedValue);
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
        public void GetSuggChosen() //Fills the member tab with members that already have benn entried
        {
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member WHERE staff = true ORDER BY firstname asc";
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
        public void GetSuggAll()
        {

            //  string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member LEFT OUTER JOIN tournament_member ON (member.id = tournament_member.id_member) WHERE tournament_member.id_member IS NULL ORDER BY firstname";
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member WHERE staff = false ORDER BY firstname asc";
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
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member WHERE staff = true ORDER BY firstname asc";
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
    }
}