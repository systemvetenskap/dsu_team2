using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Net.Mail;
using System.Data;

namespace GolfBokning
{
    public partial class stangbanan : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        // Connection 
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCLoseLane_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(TextBoxSlutdatum.Text) || string.IsNullOrWhiteSpace(TextBoxStartdatum.Text))
            {
                LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Du måste fylla i alla fält";

            }
            else
            {
                DateTime startTime = DateTime.ParseExact(TextBoxStartdatum.Text + " " + TextBoxStarttid.Text, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                if (startTime > endTime) //kontrollera om starttid är mindre än sluttid
                {
                    LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Starttid får inte vara större än sluttid";
                }

                else
                {
                    SendMail();
                    deleteBookning();
                    deleteBooked();
                    insertCloseLane();
                    LabelResponse.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Banan är stängd nu";
                }
            }
        }

        public void SendMail()
        {
            string memberMail;
            string sql = @"SELECT email 
                        FROM member 
                        WHERE id IN 

                        (SELECT DISTINCT id_member
                        FROM booked 
                        WHERE id_booking IN (SELECT id FROM booking WHERE starttime >= @starttime AND endtime <= @endtime))";
            

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@starttime", TextBoxStartdatum.Text + " " + TextBoxStarttid.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text);
          
            
            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                memberMail = dr["email"].ToString();
                string mail_text = "Hej \n\nVi vill meddela dig att banan ska vara ständ från " + TextBoxStartdatum.Text + " " + TextBoxStarttid.Text + " till " + TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text + ". Din tid har blivit avbokad \n\n MVH Halslaget GK ";
                MailMessage mail = new MailMessage("halslagetgk@gmail.com", memberMail, "Halslaget GK - Avbokning pga. banan är stängd", mail_text); // (from, to, subject, body.text)
                mail.To.Add(memberMail);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("halslagetgk@gmail.com", "MIUNgrupp2");
                client.EnableSsl = true;

                client.Send(mail);
            }

            conn.Close();
        }

        public void deleteBookning()
        {
            string sql = "delete from booked where id_booking in  (SELECT DISTINCT id FROM booking WHERE starttime >= @starttime and endtime <= @endtime)";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@starttime", TextBoxStartdatum.Text + " " + TextBoxStarttid.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text);

            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            conn.Close();
        }

        public void deleteBooked()
        {
            string sql = "delete from booking where starttime >= @starttime and endtime <= @endtime";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);;

            cmd.Parameters.AddWithValue("@starttime", TextBoxStartdatum.Text + " " + TextBoxStarttid.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text);

            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            conn.Close();
        }

        public void insertCloseLane()
        {
            string sql = "INSERT INTO booking (starttime, endtime, isclosed) VALUES (@starttime, @endtime, true)";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn); ;

            cmd.Parameters.AddWithValue("@starttime", TextBoxStartdatum.Text + " " + TextBoxStarttid.Text);
            cmd.Parameters.AddWithValue("@endtime", TextBoxSlutdatum.Text + " " + TextBoxSluttid.Text);

            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();
            dr.Read();

            conn.Close();
        }
    }
}