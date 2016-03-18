using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GolfBokning
{
    public partial class register : System.Web.UI.Page
    {
        // Connection 
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bind functions to button clicks
            ButtonRegister.Click += ButtonRegister_Click;

            // Run function to fill out Dropdown with ID for the element and the SQL-query
            AddItemsToDropDown(DropCategory, "SELECT * FROM category");
        }

        // Function to add items to dropdown list with id for the element and the SQL-query
        private void AddItemsToDropDown(DropDownList id, string sql)
        {
            // Npgsql commands and open connection
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            // Retrieve all categories from DB
            while (dr.Read())
            {
                // Add items to the Dropdown items
                id.Items.Add(new ListItem(dr["description"].ToString(), dr["id"].ToString()));
            }
            conn.Close();
        }

        string CreateGolfID(string id)
        {
            // Create a random object
            Random random = new Random();

            // Create prefix for golf_id (ex. 10238)
            // Starting with 1
            // Next number can be anything between 0 and 2
            // Following numbers are between 0 and 9
            // Convert to strings to avoid creating a sum-function
            string randomNumber = "1"
                + random.Next(0, 2).ToString()
                + random.Next(0, 9).ToString()
                + random.Next(0, 9).ToString()
                + random.Next(0, 9).ToString();

            return randomNumber + "_" + id;
        }

        void ButtonRegister_Click(object sender, EventArgs e)
        {
            // Encryption
            Encryption SHA256 = new Encryption();
            // Store variables from input-elements
            string firstName = TextFirstName.Text;
            string lastName = TextLastName.Text;
            string address = TextAddress.Text;
            string zipcode = TextZipCode.Text;
            string place = TextPlace.Text;
            string email = TextEmail.Text;
            string gender = RadioGender.SelectedValue;
            string category = DropCategory.SelectedValue;
            string password = SHA256.ComputeHash(TextPassword.Text, Supported_HA.SHA256, null);
            string ssn = TextSSN.Text;
            
            // SQL-query to check if an ID exists with that email
            string sqlCheck = "SELECT id FROM member WHERE email = '" + email + "'";

            // Npgsql commands and open connection
            NpgsqlCommand cmdCheck = new NpgsqlCommand(sqlCheck, conn);
            conn.Open();
            NpgsqlDataReader drCheck = cmdCheck.ExecuteReader();

            if (!drCheck.Read())
            {
                string sql = "INSERT INTO member (firstname, lastname, address, zipcode, place, email, gender, id_category, password, ssn) VALUES (@firstname, @lastname, @address, @zipcode, @place, @email, @gender, @id_category, @password, @ssn) RETURNING id";

                // Add parameters to secure the insert query
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@firstname", firstName);
                cmd.Parameters.AddWithValue("@lastname", lastName);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@zipcode", zipcode);
                cmd.Parameters.AddWithValue("@place", place);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@id_category", category);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@ssn", ssn);

                // Collect id from last insert to be used in golf_id
                int id = (int)cmd.ExecuteScalar();

                // Add suffix to prefix to complete golf_id
                string golf_id = CreateGolfID(id.ToString());

                // Update last inserted row and update "golf_id" column
                string sqlUpdate = "UPDATE member SET golf_id = @golf_id WHERE id = @id RETURNING id";
                NpgsqlCommand cmdUpdate = new NpgsqlCommand(sqlUpdate, conn);
                cmdUpdate.Parameters.AddWithValue("@golf_id", golf_id);
                cmdUpdate.Parameters.AddWithValue("@id", id);

                int id_logged = (int)cmdUpdate.ExecuteScalar();

                if (id_logged == id)
                {
                    // Create object 
                    Member member = new Member();

                    // Populate properties
                    member.id = id_logged;

                    member.CreateSession(false);

                    Response.Redirect("~/minasidor.aspx");
                }
            }
            else
            {
                //LabelResponse.Text = "Det finns redan en användare med denna e-mail. Vänligen välj en annan!";
            }
            conn.Close();
        }
    }
}