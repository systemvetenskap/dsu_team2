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

namespace GolfBokning
{
    public partial class minasidor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //string city = (string)(Session["City"]);
            //Member aa = (Member)(Session["member"]);
            //if (!IsPostBack)
            //{
               // ButtonMembUpdate.Click += ButtonMembUpdate_Click;
            //}
            if (!string.IsNullOrEmpty(Session["_logged"] as string))
            {
                // Check if session variable _logged is 1
                if (Session["_logged"].ToString() == "1")
                {
                    int id = Convert.ToInt16(Session["_id"]);
                    fillTable(id);
                    Debug.WriteLine(id.ToString());                 
                    Member chosenMember = GetMember(id);
                    TextFirstName.Text = chosenMember.firstName;
                    TextLastName.Text = chosenMember.lastName;
                    TextAddress.Text = chosenMember.address;
                    TextZipCode.Text = chosenMember.zipcode;
                    TextPlace.Text = chosenMember.place;
                    TextEmail.Text = chosenMember.email;
                    TextSSN.Text = chosenMember.birth;
                    TextHcp.Text = chosenMember.hcp.ToString();
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
        [System.Web.Services.WebMethod]
        public static string removeBooking(string aa)
        {
            int v = (int)HttpContext.Current.Session["_id"];
            clsBookings.clsBooking clBo = new clsBookings.clsBooking();
            clBo.cancelBooking(v.ToString(), int.Parse(aa), "");
            return "Tog bort bokningsnummer:" + aa;
        }
        private void fillTable(int membersID)
        {
            int id = Convert.ToInt16(Session["_id"]);
            clsBookings.clsBooking clBok = new clsBookings.clsBooking();
            DataTable dt = clBok.getMembersBoking(membersID);
            if (dt.Rows.Count < 1)
            {
                return;
            }
            else
            {
                string htRo = "";
                foreach (DataRow dtR in dt.Rows)
                {
                    htRo += "<tr>";
                    htRo += "<td>" + dtR["tim"].ToString().Substring(10, 6) + "</td>";
                    htRo += "<td>" + dtR["dat"].ToString().Substring(0, 10) + "</td>";
                    htRo += "<td> <button onclick='GetMessage(" + '"' + dtR["boid"].ToString() + '"' + "); return false;'>Avboka tid </button></td>";
                    htRo += "</tr>";

                }
                tbTable.InnerHtml = htRo;
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
                member.address = dr["address"].ToString();
                member.zipcode = dr["zipcode"].ToString();
                member.place = dr["place"].ToString();
                member.email = dr["email"].ToString();
                member.gender = dr["gender"].ToString();
                member.birth = dr["ssn"].ToString();
                member.hcp = Convert.ToDouble(dr["hcp"]);
                member.id_category = Convert.ToInt16(dr["id_category"]);
            }
            conn.Close();
            return member;
        }
        private void ButtonMembUpdate_Click(object sender, EventArgs e)
        {
            string sql = "";
            try
            {
                 sql = "UPDATE member SET firstname='" + TextFirstName.Text + "', lastname='" + TextLastName.Text + "', address='" + TextAddress.Text +
                "', zipcode='" + TextZipCode.Text + "', place='" + TextPlace.Text + "' WHERE id='" + Convert.ToInt16(Session["_id"]) + "'";
                 Debug.WriteLine(sql);
                NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            Response.Write("<script>alert('" +sql + "')</script>");
            
            //TextFirstName.Text = chosenMember.firstName;
            //TextLastName.Text = chosenMember.lastName;
            //TextAddress.Text = chosenMember.address;
            //TextZipCode.Text = chosenMember.zipcode;
            //TextPlace.Text = chosenMember.place;
            //TextEmail.Text = chosenMember.email;
            //TextSSN.Text = chosenMember.birth;
            //TextHcp.Text = chosenMember.hcp.ToString();
        }
    }
}