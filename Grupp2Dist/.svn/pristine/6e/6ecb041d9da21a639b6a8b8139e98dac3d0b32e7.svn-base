using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Npgsql;
using System.Threading;

namespace GolfBokning
{
    public partial class loginform : System.Web.UI.Page
    {
        // Connection 
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bind function to button click
            ButtonRegister.Click += ButtonRegister_Click;
            ButtonLogin.Click += ButtonLogin_Click;
            //ButtonTemp.Click += ButtonTemp_Click;

            //Check if session _logged is empty or not
            //Show response test
            //if (!string.IsNullOrEmpty(Session["_logged"] as string))
            //{
            //    // Check if session variable _logged is 1
            //    if (Session["_logged"].ToString() == "1")
            //    {
            //        LabelResponse.Text = "Inloggad.";
            //    }
            //}
            //else
            //{
            //    LabelResponse.Text = "Utloggad.";
            //}
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            // Run function to log in the user
            LogIn();
        }

        //void ButtonTemp_Click(object sende, EventArgs e)
        //{
        //    string sql = "SELECT id FROM member ORDER BY id";

        //    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

        //    conn.Open();
        //    string sqlUpdate = "";
        //    NpgsqlDataReader dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        Random random = new Random();
        //        string randomNumber = "19"
        //            + random.Next(3, 9).ToString()
        //            + random.Next(1, 9).ToString()
        //            + "-"
        //            + random.Next(0, 1).ToString()
        //            + random.Next(1, 9).ToString()
        //            + "-"
        //            + random.Next(10, 27).ToString();

        //        int id = Convert.ToInt16(dr["id"]);

        //        sqlUpdate += "UPDATE member SET ssn='" + randomNumber + "' WHERE id=" + id + ";";
        //        Thread.Sleep(10);
        //    }
        //    TextBoxTemp.Text = sqlUpdate;
        //}

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            // Redirect to register page
            Response.Redirect("~/register.aspx");
        }

        private void LogIn()
        {
            Encryption SHA256 = new Encryption();

            // Create variables for easy handling from textboxes
            string emailInput = TextEmailLogin.Text;
            string passwordInput = TextPasswordLogin.Text;

            string sql = "SELECT * FROM member WHERE email = '" + emailInput + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if ((dr["id"].ToString() != ""))
                {
                    // Create object 
                    Member member = new Member();

                    // Populate properties
                    member.id = Convert.ToInt16(dr["id"]);
                    member.firstName = dr["firstname"].ToString();
                    member.lastName = dr["lastname"].ToString();
                    member.address = dr["address"].ToString();
                    member.zipcode = dr["zipcode"].ToString();
                    member.place = dr["place"].ToString();
                    member.email = dr["email"].ToString();
                    member.gender = dr["gender"].ToString();
                    //member.hcp = Convert.ToDouble(dr["hcp"]);
                    member.golf_id = dr["golf_id"].ToString();
                    member.id_category = Convert.ToInt16(dr["id_category"]);
                    member.password = dr["password"].ToString();

                    if (SHA256.Confirm(passwordInput, member.password, Supported_HA.SHA256))
                    {
                        // Use member class function to create session
                        member.CreateSession(false);

                        // Check with category and redirect
                        // Check if (Admin/staff) redirect to other page
                        // In progress
                        if (member.id_category != 5)
                        {
                            Response.Redirect("~/minasidor.aspx");
                        }
                        else
                        {
                            //Response.Redirect("~/not_staff.aspx");
                        }
                    }
                    else
                    {
                        LabelResponse.Text = "Lösenordet stämmer inte.";
                    }
                }
                else
                {
                    LabelResponse.Text = "Det finns ingen e-mail registrerad.";
                }
                conn.Close();
            }
        }
    }
}