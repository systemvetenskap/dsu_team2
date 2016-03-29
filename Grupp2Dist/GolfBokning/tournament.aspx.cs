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
    public partial class tournament : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        public string SuggestionList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getQuerryExample();

            ButtonCreate.Click += new EventHandler(ButtonCreate_Click);
            ButtonCreateNew.Click += new EventHandler(ButtonCreateNew_Click);
            ButtonUpdate.Click += new EventHandler(ButtonUpdate_Click);
            ButtonRemove.Click += new EventHandler(ButtonRemove_Click);

            DropDownListTournament.SelectedIndexChanged += new EventHandler(DropDownListTournament_SelectedIndexChanged);

            DropDownListTournament.AutoPostBack = true;
            PanelNew.Visible = false;
            ButtonUpdate.Visible = false;
            ButtonCreate.Visible = false;
            LabelID.Visible = true;

            //GetAllMembers();
            if (!IsPostBack)
            {
                if (DropDownListTournament.Items.Count == 0)
                {
                    DropDownListTournament.Items.Add(new ListItem("Välj tävling...", ""));
                    DropDownListTournament.SelectedIndex = 0;
                    GetAllTournaments();
                }

                if (DropDownListClass.Items.Count == 0)
                {
                    GetClasses();
                }
            }


            PanelResponse.Visible = false;
            PanelBox.Visible = true;
            PanelBox.Height = 250;
        }

        private void ButtonA_Click(object sender, EventArgs e)
        {

        }
        private void ButtonR_Click(object sender, EventArgs e)
        {

        }
        private void GetAllTournaments()
        {
            NpgsqlConnection conn2 = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string sql = "SELECT * FROM tournament ORDER BY id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn2);
            conn2.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["name"].ToString();
                li.Value = dr["id"].ToString();
                DropDownListTournament.Items.Add(li);
            }
            conn2.Close();
        }
        private void GetTournamentById(int id)
        {
            string sql = "SELECT * FROM tournament WHERE id = " + id;

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string golfid = "";
                TextBoxName.Text = dr["name"].ToString();
                TextBoxDescription.Text = dr["description"].ToString();
                TextBoxHCP.Text = dr["hcp_req"].ToString();
                TextBoxNbrCompetitors.Text = dr["nbr_competitors"].ToString();
                TextBoxNbrHoles.Text = dr["nbr_holes"].ToString();
                DropDownListClass.SelectedIndex = -1;
                DropDownListClass.Items.FindByValue(dr["id_class"].ToString()).Selected = true;

                bool containsText = Regex.IsMatch(TextBoxContact.Text, @"^[a-zA-Z]+$");
                if (containsText)
                {
                    golfid = Regex.Match(dr["id_contact"].ToString(), @"\(([^)]*)\)").Groups[1].Value;
                }
                else
                {
                    golfid = dr["id_contact"].ToString();
                }

                TextBoxContact.Text = golfid;
                TextBoxDatePicker.Text = dr["date"].ToString().Replace(" 00:00:00", "");

                string formatStarttime = dr["starttime"].ToString().Replace("0001-01-01 ", "");
                if (formatStarttime != "")
                {
                    TextBoxStart.Text = formatStarttime.Remove(formatStarttime.Length - 3);
                }

                string formatEndtime = dr["endtime"].ToString().Replace("0001-01-01 ", "");
                if (formatEndtime != "")
                {
                    TextBoxStop.Text = formatEndtime.Remove(formatEndtime.Length - 3);
                }

                string formatPublishTime = dr["publish_startlist"].ToString();
                TextBoxPublish.Text = formatPublishTime.Remove(formatPublishTime.Length - 3);

                ButtonUpdate.Visible = true;
                ButtonCreate.Visible = false;
            }
            conn.Close();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE tournament SET name = @name, description = @description, hcp_req = @hcp_req, nbr_competitors = @nbr_competitors, nbr_holes = @nbr_holes, id_class = @id_class, id_contact = @id_contact, date = @date, starttime = @starttime, endtime = @endtime, publish_startlist = @publish_startlist, isteam = @isteam WHERE id = " + Convert.ToInt16(DropDownListTournament.SelectedValue) + " RETURNING id";
            string golfid = "";

            bool containsText = Regex.IsMatch(TextBoxContact.Text, @"^[a-zA-Z]+$");

            if (containsText)
            {
                golfid = Regex.Match(TextBoxContact.Text, @"\(([^)]*)\)").Groups[1].Value;
            }
            else
            {
                golfid = TextBoxContact.Text;
            }

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@name", TextBoxName.Text);
            cmd.Parameters.AddWithValue("@description", TextBoxDescription.Text);
            cmd.Parameters.AddWithValue("@hcp_req", float.Parse(TextBoxHCP.Text));
            cmd.Parameters.AddWithValue("@nbr_competitors", TextBoxNbrCompetitors.Text);
            cmd.Parameters.AddWithValue("@nbr_holes", TextBoxNbrHoles.Text);
            cmd.Parameters.AddWithValue("@id_class", DropDownListClass.SelectedValue);
            cmd.Parameters.AddWithValue("@id_contact", golfid);
            cmd.Parameters.AddWithValue("@date", TextBoxDatePicker.Text);
            cmd.Parameters.AddWithValue("@starttime", TextBoxStart.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxStop.Text);
            cmd.Parameters.AddWithValue("@publish_startlist", TextBoxPublish.Text);

            if (DropDownListClass.SelectedValue == "3")
            {
                cmd.Parameters.AddWithValue("@isteam", "true");
            }
            else
            {
                cmd.Parameters.AddWithValue("@isteam", "false");
            }

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                PanelResponse.Visible = true;
                LabelResponse.Text = "Uppdaterat!";
            }

            conn.Close();
        }

        private void DropDownListTournament_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListTournament.Text != "")
            {
                PanelNew.Visible = true;
                GetTournamentById(Convert.ToInt16(DropDownListTournament.SelectedValue));
                LabelID.Text = DropDownListTournament.SelectedValue;
                PanelBox.Visible = false;
                PanelResponse.Visible = false;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO tournament (name, description, hcp_req, nbr_competitors, nbr_holes, id_class, id_contact, date, starttime, endtime, publish_startlist, isteam, tournament_gender) VALUES (@name, @description, @hcp_req, @nbr_competitors, @nbr_holes, @id_class, @id_contact, @date, @starttime, @endtime, @publish_startlist, @isteam, @gender) RETURNING id";
            string golfid = "";

            bool containsText = Regex.IsMatch(TextBoxContact.Text, @"^[a-zA-Z]+$");
            if (containsText)
            {
                golfid = Regex.Match(TextBoxContact.Text, @"\(([^)]*)\)").Groups[1].Value;
            }
            else
            {
                golfid = TextBoxContact.Text;
            }


            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@name", TextBoxName.Text);
            cmd.Parameters.AddWithValue("@description", TextBoxDescription.Text);
            cmd.Parameters.AddWithValue("@hcp_req", TextBoxHCP.Text);
            cmd.Parameters.AddWithValue("@nbr_competitors", TextBoxNbrCompetitors.Text);
            cmd.Parameters.AddWithValue("@nbr_holes", TextBoxNbrHoles.Text);
            cmd.Parameters.AddWithValue("@id_class", DropDownListClass.SelectedValue);
            cmd.Parameters.AddWithValue("@id_contact", golfid);
            cmd.Parameters.AddWithValue("@date", TextBoxDatePicker.Text);
            cmd.Parameters.AddWithValue("@starttime", TextBoxStart.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxStop.Text);
            cmd.Parameters.AddWithValue("@publish_startlist", TextBoxPublish.Text);
            if (DropDownListClass.SelectedItem.Text == "Singel")
            {
                cmd.Parameters.AddWithValue("@gender", rbtn_singelgender.SelectedItem.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@gender", "Mixed");
            }
           


            if (DropDownListClass.SelectedValue == "3")
            {
                cmd.Parameters.AddWithValue("@isteam", "true");
            }
            else
            {
                cmd.Parameters.AddWithValue("@isteam", "false");
            }

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                PanelResponse.Visible = true;
                DropDownListTournament.Items.Clear();
                LabelResponse.Text = "Tävling skapad!";
                DropDownListTournament.Items.Add(new ListItem("Välj tävling...", ""));
                DropDownListTournament.SelectedIndex = 0;
                GetAllTournaments();
            }

            conn.Close();
        }

        private void ButtonCreateNew_Click(object sender, EventArgs e)
        {
            PanelNew.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonCreate.Visible = true;

            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
            TextBoxHCP.Text = "";
            TextBoxNbrCompetitors.Text = "";
            TextBoxNbrHoles.Text = "";
            TextBoxContact.Text = "";
            TextBoxDatePicker.Text = "";
            TextBoxStart.Text = "";
            TextBoxStop.Text = "";
            TextBoxPublish.Text = "";
            DropDownListClass.SelectedIndex = 0;
        }

        private void GetClasses()
        {
            string sql = "SELECT * FROM tournament_class ORDER BY id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["description"].ToString();
                li.Value = dr["id"].ToString();
                DropDownListClass.Items.Add(li);
            }

            conn.Close();
        }

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            PanelNew.Visible = false;
            ButtonUpdate.Visible = false;
            ButtonCreate.Visible = false;

            TextBoxName.Text = "";
            TextBoxDescription.Text = "";
            TextBoxHCP.Text = "";
            TextBoxNbrCompetitors.Text = "";
            TextBoxNbrHoles.Text = "";
            string sql = "DELETE FROM tournament WHERE id = " + Convert.ToInt16(DropDownListTournament.SelectedValue);

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();

            PanelResponse.Visible = true;
            LabelResponse.Text = "Borttagen!";
            DropDownListTournament.Items.Clear();
            DropDownListTournament.Items.Add(new ListItem("Välj tävling...", ""));
            DropDownListTournament.SelectedIndex = 0;
            GetAllTournaments();
        }

        public void getQuerryExample()
        {
            string queryString = "SELECT (firstname || ' ' || lastname || ' (' || golf_id ||')') as fnam FROM member WHERE staff = true ORDER BY firstname";

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