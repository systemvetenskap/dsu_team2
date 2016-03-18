using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace GolfBokning
{
    public partial class cms : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSave.Click += new EventHandler(ButtonSave_Click);
            DropDownListPage.SelectedIndexChanged += new EventHandler(DropDownListPage_SelectedIndexChanged);
            DropDownListPage.AutoPostBack = true;
            PanelSuccess.Visible = false;

            if (DropDownListPage.Items.Count == 0)
            {
                GetPages();
            }
            
            PanelUpdate.Visible = false;
        }

        private void GetPages()
        {
            string sql = "SELECT * FROM cms ORDER BY id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);


            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Value = dr["id"].ToString();
                li.Text = dr["page_name"].ToString();
                DropDownListPage.Items.Add(li);
            }
            conn.Close();
        }

        private void DropDownListPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(DropDownListPage.SelectedValue);
            PanelUpdate.Visible = true;
            string sql = "SELECT * FROM cms WHERE id = " + id;

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                LabelWhatPage.Text = "<h2>" + dr["page_name"].ToString() + "</h2>";
                ImageExist.ImageUrl = dr["image"].ToString();
                TextBoxText.Text = dr["text"].ToString();
            }
            conn.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(DropDownListPage.SelectedValue);

            string sql = "UPDATE cms SET image = @image, text = @text WHERE id = " + id + " RETURNING id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@image", "http://smock.foreuphosting5.com/wp-content/uploads/2015/04/tight.jpg");
            cmd.Parameters.AddWithValue("@text", TextBoxText.Text);

            int id_return = (int)cmd.ExecuteScalar();

            if (id_return != 0)
            {
                PanelSuccess.Visible = true;
                LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Sparat!";
            }
            conn.Close();
        }
    }
}