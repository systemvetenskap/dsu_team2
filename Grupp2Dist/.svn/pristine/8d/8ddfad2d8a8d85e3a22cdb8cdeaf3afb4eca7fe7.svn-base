using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace GolfBokning
{
    public partial class myAccount : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        protected void Page_Load(object sender, EventArgs e)
        {

            myCookie = Request.Cookies["LoginCookie"];
            ButtonMembUpdate.Click += ButtonMembUpdate_Click;
            PanelMySideUpdate.Visible = true;
            PanelSuccess.Visible = false;
            LabelHidden.Text = myCookie["_id"].ToString();
            //string city = (string)(Session["City"]);


            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(myCookie["_logged"] as string))
                {
                    // Check if session variable _logged is 1
                    if (myCookie["_logged"].ToString() == "1")
                    {
                        int id = Convert.ToInt16(myCookie["_id"]);
                        //fillTable(id);
                        Debug.WriteLine(id.ToString());
                        Member chosenMember = GetMember(id);
                        TextFirstName.Text = chosenMember.firstName;
                        TextLastName.Text = chosenMember.lastName;
                        TextBoxPhone.Text = chosenMember.phone;
                        TextAddress.Text = chosenMember.address;
                        TextZipCode.Text = chosenMember.zipcode;
                        TextPlace.Text = chosenMember.place;
                        TextEmail.Text = chosenMember.email;
                        TextSSN.Text = chosenMember.birth;
                        TextHcp.Text = chosenMember.hcp.ToString("0.0");
                        TextBoxGolfId.Text = chosenMember.golf_id.ToString();
                        if (chosenMember.gender == "Male")
                        {
                            RadioButtonGender.SelectedValue = "Male";
                        }
                        else
                        {
                            RadioButtonGender.SelectedValue = "Female";
                        }

                        //SelectGender(RadioButtonGender, chosenMember.gender.ToString());
                        //GetAllCategories(DropDownCategories, "SELECT id, description FROM category");

                    }
                }
                else
                {
                    return;
                }
            }
            
        }

        public Member GetMember(int membId)
        {
            Member member = new Member();
            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            conn.Open();
            string sqlstring = "SELECT * FROM member WHERE id='" + membId + "'ORDER BY id";
            NpgsqlCommand command = new NpgsqlCommand(sqlstring, conn);
            NpgsqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                member.id = Convert.ToInt32(dr["id"]);
                member.firstName = dr["firstname"].ToString();
                member.lastName = dr["lastname"].ToString();
                member.phone = dr["phone"].ToString();
                member.address = dr["address"].ToString();
                member.zipcode = dr["zipcode"].ToString();
                member.place = dr["place"].ToString();
                member.email = dr["email"].ToString();
                member.gender = dr["gender"].ToString();
                member.birth = dr["ssn"].ToString();
                member.golf_id = dr["golf_id"].ToString();
                if (!DBNull.Value.Equals(dr["hcp"]))
                {
                    member.hcp = Convert.ToDouble(dr["hcp"]);
                }
                member.id_category = Convert.ToInt16(dr["id_category"]);
            }
            conn.Close();
            return member;
        }
        private void ButtonMembUpdate_Click(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];
            string sql = "";
            
            if (TextPassword.Text != "")
            {
                sql = "UPDATE member SET firstname = @firstname, lastname = @lastname, phone = @phone, address = @address, zipcode = @zipcode, place = @place, password = @password WHERE id=" + Convert.ToInt16(myCookie["_id"]) + " RETURNING id";
            }
            else
            {
                sql = "UPDATE member SET firstname = @firstname, lastname = @lastname, phone = @phone, address = @address, zipcode = @zipcode, place = @place WHERE id=" + Convert.ToInt16(myCookie["_id"]) + " RETURNING id";
            }

            NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@firstname", TextFirstName.Text);
            cmd.Parameters.AddWithValue("@lastname", TextLastName.Text);
            cmd.Parameters.AddWithValue("@phone", TextBoxPhone.Text);
            cmd.Parameters.AddWithValue("@address", TextAddress.Text);
            cmd.Parameters.AddWithValue("@zipcode", TextZipCode.Text);
            cmd.Parameters.AddWithValue("@place", TextPlace.Text);

            if (TextPassword.Text != "")
            {
                Encryption SHA256 = new Encryption();
                string password = SHA256.ComputeHash(TextPassword.Text, Supported_HA.SHA256, null);
                cmd.Parameters.AddWithValue("@password", password);
            }

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                PanelSuccess.Visible = true;
                LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Sparat!";
            }
            else
            {
                PanelSuccess.Visible = true;
                LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Gick inte att spara!";
                
            }
            conn.Close();
        }

    }
}