using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace GolfBokning
{
    public partial class resultapp : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            DropDownListChoose.SelectedIndexChanged += new EventHandler(DropDownListChoose_SelectedIndexChanged);

            DropDownListChoose.AutoPostBack = true;

            if (!IsPostBack)
            {
                GetTournaments();
            }
            
            myCookie = Request.Cookies["LoginCookie"];
            LabelName.Text = "Välkommen " + myCookie["_name"] + "!";
            PanelBoxes.Visible = false;
        }

        protected void DropDownListChoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelChoose.Visible = false;
            PanelBoxes.Visible = true;
            LabelH2.Text = "Tävling: " + DropDownListChoose.SelectedItem.Text;
            LabelHiddenLeader.Text = "1";
            GetTournament(Convert.ToInt16(DropDownListChoose.SelectedValue));
        }

        protected void GetStarttime(int id_member, int id_tournament)
        {
            string sql = "SELECT starttime FROM tournament_member WHERE id_member = @id_member AND id_tournament = @id_tournament";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_member", id_member);
            cmd.Parameters.AddWithValue("@id_tournament", id_tournament);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["starttime"] != DBNull.Value)
                {
                    string YStartTime = dr["starttime"].ToString();
                    YStartTime = YStartTime.Replace("0001-01-01 ", "");
                    YStartTime = YStartTime.Remove(YStartTime.Length - 3);
                    LabelYStarttime.Text = YStartTime;
                }
            }

            conn.Close();

        }

        protected void GetTournament(int id)
        {
            string sql = "SELECT * FROM tournament WHERE id = @id ORDER BY id";
            myCookie = Request.Cookies["LoginCookie"];
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id", id);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read()) {
                string starttime = dr["starttime"].ToString();
                starttime = starttime.Replace("0001-01-01 ", "");
                starttime = starttime.Remove(starttime.Length - 3);
                string endtime = dr["endtime"].ToString();
                endtime = endtime.Replace("0001-01-01 ", "");
                endtime = endtime.Remove(endtime.Length - 3);

                string date = dr["date"].ToString();
                date = date.Replace(" 00:00:00", "");

                LabelTDesc.Text = dr["description"].ToString();
                LabelTNbrHoles.Text = dr["nbr_holes"].ToString();
                LabelTNbrComp.Text = dr["nbr_competitors"].ToString();
                LabelTStarttime.Text = date + " " + starttime;
                LabelTEndtime.Text = date + " " + endtime;
            }

            conn.Close();
            GetStarttime(Convert.ToInt16(myCookie["_id"]), id);
        }

        protected void GetTournaments()
        {
            myCookie = Request.Cookies["LoginCookie"];
            string sql = "SELECT * FROM tournament t INNER JOIN tournament_member tm ON t.id = tm.id_tournament WHERE id_member = @id_member AND date > Now()";
            int id_member = Convert.ToInt16(myCookie["_id"]);
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id_member", id_member);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["name"].ToString();
                li.Value = dr["id"].ToString();
                DropDownListChoose.Items.Add(li);
            }

            conn.Close();
        }
    }
}