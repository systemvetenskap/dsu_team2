using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Npgsql;
using System.Xml;
using System.Diagnostics;

namespace GolfBokning
{
    public partial class sendMail : System.Web.UI.Page
    {

        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);



        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void ButtonSendMail_Click(object sender, EventArgs e)
        {
            SendMailMembers();
        }

        public void SendMailMembers()
        {

            string memberMail;
            string sql = "select * from member";

            if (DropDownListSendMail.SelectedValue == "1")
            {
                sql = "select * from member where staff = true order by id desc";
            }

            if (DropDownListSendMail.SelectedValue == "2")
            {
                sql = "select * from member where staff = false order by id desc";
            }

            if (DropDownListSendMail.SelectedValue == "3")
            {
                sql = "select * from member order by id desc";
            }


            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            string rubrik = TextMailRubrik.Text,
                   bodyText = bodyHtml.InnerText;

            conn.Open();
            List<string> emai = new List<string>();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                emai.Add(dr["email"].ToString());
            }

            conn.Close();

            for (int i = 0; i < emai.Count; i++)
            {
                try
                {
                    memberMail = emai[i];
                    MailMessage mail = new MailMessage("halslagetgk@gmail.com", memberMail, rubrik, bodyText); // (from, to, subject, body.text)
                    mail.To.Add(memberMail);
                    mail.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential("halslagetgk@gmail.com", "MIUNgrupp2");
                    client.EnableSsl = true;            
                    //client.SendMailAsync(mail);
                    client.Send(mail);
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            LabelMailSkickades.Visible = true;
            LabelMailSkickades.Text = "<span class='spacer-glyph glyphicon glyphicon-exclamation-sign'></span> Mailet har skickats";

        }

    }
}