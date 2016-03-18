using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace GolfBokning
{
    public partial class members : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCollectMember.Click += ButtonCollectMember_Click;
            ButtonUppdate.Click += ButtonUppdate_Click;
            TextBoxHiddenID.Visible = false;
        }

        //metod för att hämta medlem från databas
        public Member GetMember(string golfId)
        {
            Member member = new Member();
            conn.Open();
            string sqlstring = "SELECT * FROM member WHERE golf_id='" +golfId+ "'ORDER BY id";
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
                member.hcp = Convert.ToDouble(dr["hcp"]);
                member.ssn = dr["ssn"].ToString();
                member.id_category = Convert.ToInt16(dr["id_category"]);
            }
            conn.Close();
            return member;
        }

        //knapp att hämta medlemmar till textboxar
        private void ButtonCollectMember_Click(object sender, EventArgs e)
        {
            if (TextGolfId.Text != "")
            {
                string golfid = TextGolfId.Text;
                Member chosenMember = GetMember(golfid);
                TextBoxHiddenID.Text = chosenMember.id.ToString();
                TextFirstName.Text = chosenMember.firstName;
                TextLastName.Text = chosenMember.lastName;
                TextAddress.Text = chosenMember.address;
                TextZipcode.Text = chosenMember.zipcode;
                TextPlace.Text = chosenMember.place;
                TextEmail.Text = chosenMember.email;
                TextBoxSSN.Text = chosenMember.ssn;
                TextHcp.Text = chosenMember.hcp.ToString();

                SelectGender(RadioButtonGender, chosenMember.gender.ToString());
                GetAllCategories(DropDownCategories, "SELECT id, description FROM category");
                SelectCategory(DropDownCategories, chosenMember.id_category.ToString());
            }
        }

        private void GetAllCategories(DropDownList list, string sql)
        {
            // Npgsql commands and open connection
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            // Retrieve all categories from DB
            while (dr.Read())
            {
                // Add items to the Dropdown items
                list.Items.Add(new ListItem(dr["description"].ToString(), dr["id"].ToString()));
            }
            conn.Close();
        }

        private void SelectCategory(DropDownList list, string idCategory)
        {
            int categoryIndex = 0;
            foreach (ListItem li in list.Items)
            {
                if (li.Value == idCategory)
                {
                    categoryIndex = Convert.ToInt16(li.Value);
                }
            }

            list.SelectedIndex = categoryIndex - 1;
        }

        private void SelectGender(RadioButtonList list, string gender)
        {
            list.Items.FindByValue(gender).Selected = true;
        }

        //metod för att uppdatera medlemmar 
        public void updateMember(Member member)
        {
            string sql = "UPDATE member SET " +
                "firstname = @firstname, " +
                "lastname = @lastname, " +
                "address = @address, " +
                "zipcode = @zipcode, " +
                "place = @place, " +
                "email = @email, " +
                "gender = @gender, " +
                "hcp = @hcp, " +
                "id_category = @id_category, " +
                "ssn = @ssn " +
                "WHERE id = @id RETURNING id";


            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", member.id);
            cmd.Parameters.AddWithValue("@firstname", member.firstName);
            cmd.Parameters.AddWithValue("@lastname", member.lastName);
            cmd.Parameters.AddWithValue("@address", member.address);
            cmd.Parameters.AddWithValue("@zipcode", member.zipcode);
            cmd.Parameters.AddWithValue("@place", member.place);
            cmd.Parameters.AddWithValue("@email", member.email);
            cmd.Parameters.AddWithValue("@gender", member.gender);
            cmd.Parameters.AddWithValue("@hcp", member.hcp);
            cmd.Parameters.AddWithValue("@id_category", member.id_category);
            cmd.Parameters.AddWithValue("@ssn", member.ssn);

            conn.Open();
            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                TestLabel.Text = "Uppdaterat!";
            }
            else
            {
                TestLabel.Text = "Kunde inte uppdateras.";
            }
            conn.Close();
        }

        private void ButtonUppdate_Click(object sender, EventArgs e)
        {
            Member member = new Member();
            member.id = Convert.ToInt16(TextBoxHiddenID.Text);
            member.golf_id = TextGolfId.Text;
            member.firstName = TextFirstName.Text;
            member.lastName = TextLastName.Text;
            member.address = TextAddress.Text;
            member.zipcode = TextZipcode.Text;
            member.place = TextPlace.Text;
            member.email = TextEmail.Text;
            member.gender = RadioButtonGender.SelectedValue;
            member.hcp = Convert.ToDouble(TextHcp.Text);
            member.id_category = Convert.ToInt16(DropDownCategories.SelectedValue);
            member.ssn = TextBoxSSN.Text;

            updateMember(member);
        }

   }
}