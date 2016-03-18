using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Npgsql;

namespace GolfBokning
{
    public partial class tournamentmembadd : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        List<string> listCount = new List<string>();
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            RadioButtonListClass.SelectedIndexChanged += new EventHandler(RadioButtonListClass_SelectedIndexChanged);
            RadioButtonListClass.AutoPostBack = true;

            for (int i = 0; i < 10; i++)
            {
                CountNbr(i);
            }

            if (RadioButtonListClass.Items.Count == 0)
            {
                GetClasses();
            }

            if (!IsPostBack)
            {
                GetTournaments(1);
            }
            myCookie = Request.Cookies["LoginCookie"];
            PanelBox.Visible = true;
            PanelBox.Height = 250;
            LabelHiddenId.Text = myCookie["_id"].ToString();
            LabelHiddenId.CssClass = "hidden_id";
        }

        protected void CountNbr(int id)
        {
            string sqlNbr = "SELECT count(*) as count FROM tournament_member WHERE id_tournament = @id_tournament";
            int count = 0;
            NpgsqlCommand cmdNbr = new NpgsqlCommand(sqlNbr, conn);
            cmdNbr.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id));
            conn.Open();
            NpgsqlDataReader dr = cmdNbr.ExecuteReader();

            while (dr.Read())
            {
                count = Convert.ToInt16(dr["count"]);
            }

            conn.Close();
            listCount.Add(id.ToString() + ":" + count.ToString());
        }

        private void RadioButtonListClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTournaments(Convert.ToInt16(RadioButtonListClass.SelectedValue));
        }
        [System.Web.Services.WebMethod]
        public static string JoinTournament(string id_tournament, string id_member)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";

            string sql = "INSERT INTO tournament_member (id_tournament, id_member) VALUES (@id_tournament, @id_member) RETURNING id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
            cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(id_member));

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                output = "OK";
            }
            conn.Close();

            return output;
        }

        [System.Web.Services.WebMethod]
        public static string CheckJoinTournament(string id_tournament, string id_member)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";

            string sql = "SELECT * FROM tournament_member WHERE id_tournament = @id_tournament AND id_member = @id_member";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", Convert.ToInt16(id_tournament));
            cmd.Parameters.AddWithValue("@id_member", Convert.ToInt16(id_member));

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                output = "error " + id_tournament.ToString();
            }
            conn.Close();
            return output;
        }

        [System.Web.Services.WebMethod]
        public static string CheckJoinTournamentFull(int id_tournament, int nbr)
        {
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            string output = "";
            int nbr_competitors = 0;

            string sql = "SELECT count(*) as count FROM tournament_member WHERE id_tournament = @id_tournament";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                nbr_competitors = Convert.ToInt16(dr["count"]);
            }

            if (nbr_competitors >= nbr)
            {
                output = "error " + id_tournament.ToString() + ";" + nbr_competitors.ToString();
            }

            conn.Close();
            return output;
        }

        protected void GetTournaments(int whatClass)
        {
            myCookie = Request.Cookies["LoginCookie"];
            string checkClass = whatClass.ToString();
            if (checkClass != "")
            {
                // 1 = Blandad 2 = Singel 3 = Lagspel
                string sql = "SELECT * FROM tournament WHERE id_class = " + whatClass + " AND date > Now()";
                // SELECT t.id, t.name, t.description, t.hcp_req, t.nbr_competitors, t.nbr_holes, t.id_class,t.entry_open,t.entry_close, t.publish_startlist, t.date, t.id_contact, t.starttime,t.endtime, t.id_lane, tm.id, tm.id_tournament, tm.id_member, tm.id_class, tm.starttime, tm.id_team, tm.points, count(tm.id) as count FROM tournament t INNER JOIN tournament_member tm ON t.id = tm.id_tournament WHERE t.id_class = 1 GROUP BY t.id, t.name, t.description, t.hcp_req, t.nbr_competitors, t.nbr_holes, t.id_class,t.entry_open,t.entry_close, t.publish_startlist, t.date, t.id_contact, t.starttime,t.endtime, t.id_lane, tm.id, tm.id_tournament, tm.id_member, tm.id_class, tm.starttime, tm.id_team, tm.points
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                conn.Open();

                NpgsqlDataReader dr = cmd.ExecuteReader();

                Table table = new Table();
                table.Attributes["class"] = "table table-bordered";
                TableHeaderRow thr = new TableHeaderRow();

                TableHeaderCell thcName = new TableHeaderCell();
                thcName.Text = "Namn";

                TableHeaderCell thcDate = new TableHeaderCell();
                thcDate.Text = "Datum";

                TableHeaderCell thcStarttime = new TableHeaderCell();
                thcStarttime.Text = "Starttid";

                TableHeaderCell thcHcp = new TableHeaderCell();
                thcHcp.Text = "HCP min";

                TableHeaderCell thcNbr = new TableHeaderCell();
                thcNbr.Text = "Platser kvar";

                TableHeaderCell thcJoin = new TableHeaderCell();
                thcJoin.Text = "Anmäl";


                thr.Cells.Add(thcName);
                thr.Cells.Add(thcDate);
                thr.Cells.Add(thcStarttime);
                thr.Cells.Add(thcHcp);
                thr.Cells.Add(thcNbr);
                thr.Cells.Add(thcJoin);

                table.Rows.Add(thr);

                while (dr.Read())
                {
                    TableRow tr = new TableRow();

                    TableCell tcName = new TableCell();
                    tcName.Text = dr["name"].ToString(); ;

                    TableCell tcDate = new TableCell();
                    string date = dr["date"].ToString();
                    date = date.Replace(" 00:00:00", "");
                    tcDate.Text = date;

                    TableCell tcStarttime = new TableCell();
                    string starttime = dr["starttime"].ToString();
                    starttime = starttime.Replace("0001-01-01 ", "");
                    starttime = starttime.Remove(starttime.Length - 3);
                    tcStarttime.Text = starttime;

                    TableCell tcHcp = new TableCell();
                    tcHcp.Text = dr["hcp_req"].ToString();
                    double hcp_rq = Convert.ToDouble(dr["hcp_req"]);


                    TableCell tcNbr = new TableCell();
                    
                    
                    foreach (string s in listCount)
                    {
                        string[] split = s.Split(':');
                        int id_tournament = Convert.ToInt16(split[0]);
                        int nbr = Convert.ToInt16(split[1]);

                        if (id_tournament == Convert.ToInt16(dr["id"]))
                        {
                            int count = Convert.ToInt16(dr["nbr_competitors"]) - nbr;
                            tcNbr.Text = count.ToString();
                        }
                    }

                    TableCell tcJoin = new TableCell();

                    double hcp = Convert.ToDouble(myCookie["_hcp"]);


                    if (hcp > hcp_rq)
                    {
                        tcJoin.Text = "<button data='" + dr["id"].ToString() + "' class='btn btn-default btn-dis' disabled>För högt HCP</button>";
                    }
                    else
                    {
                        tcJoin.Text = "<button class='btn btn-default "+dr["id"].ToString()+"' data-nbr='"+dr["nbr_competitors"].ToString()+"' data='"+dr["id"].ToString()+"' onclick='Join("+dr["id"].ToString()+"); return false;'>Anmäl</button>";
                    }
                       
                    tr.Cells.Add(tcName);
                    tr.Cells.Add(tcDate);
                    tr.Cells.Add(tcStarttime);
                    tr.Cells.Add(tcHcp);
                    tr.Cells.Add(tcNbr);
                    tr.Cells.Add(tcJoin);

                    table.Rows.Add(tr);
                }
                PanelTournaments.Controls.Add(table);
            }
            conn.Close();
        }

        protected void GetClasses()
        {
            string sql = "SELECT * FROM tournament_class ORDER BY id";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem rb = new ListItem();
                rb.Text = dr["description"].ToString();
                rb.Value = dr["id"].ToString();
                RadioButtonListClass.Items.Add(rb);
            }

            conn.Close();
            RadioButtonListClass.SelectedIndex = 0;
        }

    }
}