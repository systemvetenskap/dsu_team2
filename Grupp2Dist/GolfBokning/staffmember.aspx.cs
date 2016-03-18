using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.IO;

namespace GolfBokning
{
    public partial class staffmember : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxSearch.TextChanged += new EventHandler(TextBoxSearch_TextChanged);
            TextBoxStaffSearch.TextChanged += new EventHandler(TextBoxStaffSearch_TextChanged);
            BtnAdd.Click += BtnAdd_Click;
            BtnRemove.Click += BtnRemove_Click;

            getAll(false, ListBoxMembers);
            ListBoxMembers.Height = 200;

            ListBoxStaff.Items.Clear();
            getAll(true, ListBoxStaff);
            ListBoxStaff.Height = 200;
        }
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            SortMembers(TextBoxSearch.Text);
        }

        private void TextBoxStaffSearch_TextChanged(object sender, EventArgs e)
        {
            SortStaffs(TextBoxStaffSearch.Text);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string golf_id = ListBoxMembers.SelectedValue;

            string sql = "UPDATE member SET staff = true WHERE golf_id = @golf_id RETURNING id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@golf_id", golf_id);

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                conn.Close();
                LabelTest.Text = "Fixat!";
                getAll(false, ListBoxMembers);
                Response.Redirect("staffmember.aspx");
            }
            else
            {
                conn.Close();
                LabelTest.Text = "Inte fixat!";
                getAll(true, ListBoxStaff);
                Response.Redirect("staffmember.aspx");
            }
        }

        protected void SortMembers(string input)
        {
            if (input.Length > 0)
            {
                Member member;

                string sql = "SELECT * FROM member WHERE staff = false AND golf_id LIKE '" + input + "%' ORDER BY firstname ASC";

                ListBoxMembers.Items.Clear();
                ListBoxMembers.SelectedIndex = -1;

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                conn.Open();
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    member = new Member();
                    member.firstName = dr["firstname"].ToString();
                    member.lastName = dr["lastname"].ToString();
                    member.golf_id = dr["golf_id"].ToString();
                    member.email = dr["email"].ToString();
                    member.staff = Convert.ToBoolean(dr["staff"]);

                    ListItem li = new ListItem();
                    li.Value = member.golf_id;
                    li.Text = member.ToString();
                    ListBoxMembers.Items.Add(li);
                }
                ListBoxMembers.SelectedIndex = 0;
                conn.Close();
            }
        }

        protected void SortStaffs(string input)
        {
            if (input.Length > 0)
            {
                Member member;

                string sql = "SELECT * FROM member WHERE staff = true AND golf_id LIKE '" + input + "%' ORDER BY firstname ASC";

                ListBoxStaff.Items.Clear();
                ListBoxStaff.SelectedIndex = -1;

                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                conn.Open();
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    member = new Member();
                    member.firstName = dr["firstname"].ToString();
                    member.lastName = dr["lastname"].ToString();
                    member.golf_id = dr["golf_id"].ToString();
                    member.email = dr["email"].ToString();
                    member.staff = Convert.ToBoolean(dr["staff"]);

                    ListItem li = new ListItem();
                    li.Value = member.golf_id;
                    li.Text = member.ToString();
                    ListBoxStaff.Items.Add(li);
                }
                ListBoxStaff.SelectedIndex = 0;
                conn.Close();
            }
        }

        protected void getAll(bool staff, ListBox lb)
        {
            string sql;
            Member member;

            if (staff)
            {
                sql = "SELECT * FROM member WHERE staff = true ORDER BY firstname ASC";
            }
            else
            {
                sql = "SELECT * FROM member WHERE staff = false ORDER BY firstname ASC";
            }
            

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                member = new Member();
                member.firstName = dr["firstname"].ToString();
                member.lastName = dr["lastname"].ToString();
                member.golf_id = dr["golf_id"].ToString();
                member.email = dr["email"].ToString();
                member.staff = Convert.ToBoolean(dr["staff"]);

                ListItem li = new ListItem();
                li.Value = member.golf_id; 
                li.Text = member.ToString();
                lb.Items.Add(li);
            }

            conn.Close();
        }
    }
}