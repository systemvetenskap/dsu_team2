using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GolfBokning
{
    public partial class chatt : System.Web.UI.Page
    {
        string dbConn = Properties.Settings.Default.dbConnectionString;
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                liBox.Items.Clear();
                fillListbox();
            }

            liBox.SelectedIndexChanged += new EventHandler(liBox_SelectedIndexChanged);

           
        }
        //DropDownListTournament.SelectedIndexChanged += new EventHandler(DropDownListTournament_SelectedIndexChanged);
        private void liBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblChattID.Text = liBox.SelectedValue;
            if (liBox.SelectedValue.Length < 1)
            {
                return;
            }
            //string htmlTabl = getMessage(int.Parse(liBox.SelectedValue), "1994-02-19 17:50:00");
            //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "inputAllMessage('" + htmlTabl + "');", true);            
          
            //Sjukt störrig kod men var tvungen att få till att lägga till data till html tabell 
            //StringBuilder sb = new StringBuilder();
            //sb.Append(htmlTabl);
            //StringWriter sw = new StringWriter(sb);           
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //chaF.RenderControl(htw);

        }
        private void fillListbox()
        {

            myCookie = Request.Cookies["LoginCookie"];
            string id = myCookie["_id"].ToString();
            string sql = "SELECT (firstname || ' ' || lastname || '(' || golf_id || ')') as aa, chatt.id from member INNER JOIN chatt on (member.id = chatt.id_member) or (member.id = chatt.id_memb) " +
                " WHERE (chatt.id_member = @membid or chatt.id_memb = @membid) and member.id != @membid";
            using (NpgsqlConnection cnn = new NpgsqlConnection(dbConn))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@membid", id);
                    cnn.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListItem liIt = new ListItem();
                            liIt.Text = Convert.ToString(dr[0]);
                            liIt.Value = Convert.ToString(dr[1]);
                            liBox.Items.Add(liIt);
                        }
                    }
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string startChatt(int id_cookie, string golf_id)
        {
            string ret = getMembID(golf_id);
            if (ret == "FALSE")
            {
                return "Medlemen finns inte!";
            }
            ret = createChatt(id_cookie, int.Parse(ret));
            return "OK";
        }
        private static string getMembID(string id)
        {
            string sql = "SELECT id from member where member.golf_id = @membid";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@membid", id);
                    cnn.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return dr[0].ToString();
                        }
                        else
                        {
                            return "FALSE";
                        }
                    }
                }
            }
        }
        private static string createChatt(int memID, int mbID)
        {
            string sql = "INSERT INTO chatt (id_member, id_memb) VALUES ('" + memID + "', '" + mbID + "') RETURNING id";
            string ret = "False";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {

                    ret = ((int)cmd.ExecuteScalar()).ToString();
                }
            }
            return ret;
        }
        [System.Web.Services.WebMethod]
        public static string messSend(int id, int chattid, string text)
        {
            string sql = "INSERT INTO chatt_text (message, sent, id_chatt, sentby) VALUES (@mess, substring(now()::text,1, 19)::timestamp, @chid, @sby) RETURNING id";
            string ret = "False";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                cnn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@mess", text);
                    cmd.Parameters.AddWithValue("@sby", id);
                    cmd.Parameters.AddWithValue("@chid", chattid);
                    ret = ((int)cmd.ExecuteScalar()).ToString();
                }
            }
            return ret;
        }
        [System.Web.Services.WebMethod]
        public static string getMessage(int chattID, string idLastSent)
        {
            string ret = "";
            //DateTime daTim = DateTime.Parse(datTime);
            string sql = "SELECT (lastname || ' ' || firstname || ': ' || message), chatt_text.id  FROM chatt_text " +
" left join member on chatt_text.sentby = member.id " +
"where id_chatt = @chatID and chatt_text.id > '" + idLastSent + "' ORDER BY SENT ASC";
            using (NpgsqlConnection cnn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, cnn))
                {
                    cmd.Parameters.AddWithValue("@chatID", chattID);

                    cnn.Open();
                    using (NpgsqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ret += "<tr class='chattro' id='" + dr[1] + "'><td class='chattcel'>" + dr[0] + "</td></tr>";
                        }

                    }
                }
            }
            return ret;
        }
    }
}