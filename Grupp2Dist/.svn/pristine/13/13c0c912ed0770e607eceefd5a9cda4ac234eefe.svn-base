using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace GolfBokning
{
    public partial class news : System.Web.UI.Page
    {
        HttpCookie myCookie = new HttpCookie("LoginCookie");
        // Connection 
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonCreate.Click += new EventHandler(ButtonCreate_Click);
            ButtonUpdate.Click += new EventHandler(ButtonUpdate_Click);
            ButtonCreateNew.Click += new EventHandler(ButtonCreateNew_Click);
            ButtonRemoveNews.Click += new EventHandler(ButtonRemoveNews_Click);
            DropDownNews.SelectedIndexChanged += new EventHandler(DropDownNews_SelectedIndexChanged);
            DropDownNews.AutoPostBack = true;

            PanelSuccess.Visible = false;
            PanelResponse.Visible = false;
            ButtonUpdate.Visible = false;
            ButtonRemoveNews.Visible = false;
            PanelRemove.Visible = false;

            ButtonCreateNew.Text = "<span class='glyphicon glyphicon-plus'></span> Skapa ny";
            ButtonRemoveNews.Text = "<span class='glyphicon glyphicon-minus'></span> Ta bort";

            if (DropDownNews.Items.Count == 0)
            {
                LoadDropDown();
            }
            
        }

        private void ButtonRemoveNews_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(DropDownNews.SelectedValue);

            string sql = "DELETE FROM newsarticle WHERE id = " + id;

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                TextBoxHeading.Text = "";
                TextBoxSmallHeading.Text = "";
                TextBoxText.Text = "";
                TextBoxImageText.Text = "";
                LabelH2.Text = "<h2>Skapa nyhet</h2>";
                LabelImage.Text = "Ladda upp bild";
                ImageExist.Visible = false;
                ButtonCreate.Visible = true;
                ButtonUpdate.Visible = false;
                ButtonRemoveNews.Visible = false;
                PanelRemove.Visible = true;
                LabelRemove.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Borttagen!";
                DropDownNews.Items.Clear();
                conn.Close();
                LoadDropDown();
                conn.Open();
            }

            conn.Close();
        }

        private void DropDownNews_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(DropDownNews.SelectedValue);
            string sql = "SELECT * FROM newsarticle WHERE id = " + id;

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();

            NpgsqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                TextBoxHeading.Text = "";
                TextBoxSmallHeading.Text = "";
                TextBoxText.Text = "";
                TextBoxImageText.Text = "";
                TextBoxHeading.Text = dr["heading"].ToString();
                TextBoxSmallHeading.Text = dr["small_heading"].ToString();
                TextBoxText.Text = dr["text"].ToString();
                TextBoxImageText.Text = dr["image_text"].ToString();
                ImageExist.ImageUrl = dr["image"].ToString();
                ImageExist.CssClass = "imageExist";
                LabelImage.Text = "Nuvarande bild";
            }

            LabelH2.Text = "<h2>Redigera nyhet</h2>";
            ButtonCreate.Visible = false;
            ButtonUpdate.Visible = true;
            ButtonRemoveNews.Visible = true;
            PanelResponse.Visible = false;
            PanelRemove.Visible = false;
        }

        private void ButtonCreateNew_Click(object sender, EventArgs e)
        {
            TextBoxHeading.Text = "";
            TextBoxSmallHeading.Text = "";
            TextBoxText.Text = "";
            TextBoxImageText.Text = "";
            LabelH2.Text = "<h2>Skapa nyhet</h2>";
            LabelImage.Text = "Ladda upp bild";
            ImageExist.Visible = false;
            ButtonCreate.Visible = true;
            ButtonUpdate.Visible = false;
            ButtonRemoveNews.Visible = false;
            PanelResponse.Visible = false;
            PanelRemove.Visible = false;
        }

        private void LoadDropDown()
        {
            string sql = "SELECT * FROM newsarticle ORDER BY date DESC";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ListItem li = new ListItem();
                li.Text = dr["heading"].ToString();
                li.Value = dr["id"].ToString();
                DropDownNews.Items.Add(li);
            }

            conn.Close();
        }
        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];
            int id_article = Convert.ToInt16(DropDownNews.SelectedValue);

            NewsArticle newsArticle = new NewsArticle();
            newsArticle.heading = TextBoxHeading.Text;
            newsArticle.small_heading = TextBoxSmallHeading.Text;
            newsArticle.text = TextBoxText.Text;

            if (!string.IsNullOrEmpty(myCookie["_name"] as string))
            {
                newsArticle.written_by = myCookie["_name"].ToString();
            }
            else
            {
                newsArticle.written_by = "Admin";
            }

            newsArticle.image_text = TextBoxImageText.Text;

            string sql = "UPDATE newsarticle SET heading = @heading, small_heading = @small_heading, text = @text, date = Now(), written_by = @written_by, image = @image, image_text = @image_text WHERE id = " + id_article + " RETURNING id";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();

            cmd.Parameters.AddWithValue("@heading", newsArticle.heading);
            cmd.Parameters.AddWithValue("@small_heading", newsArticle.small_heading);
            cmd.Parameters.AddWithValue("@text", newsArticle.text);
            cmd.Parameters.AddWithValue("@written_by", newsArticle.written_by);
            cmd.Parameters.AddWithValue("@image", "http://smock.foreuphosting5.com/wp-content/uploads/2015/04/tight.jpg");
            cmd.Parameters.AddWithValue("@image_text", newsArticle.image_text);

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                // Success
                TextBoxHeading.Text = "";
                TextBoxSmallHeading.Text = "";
                TextBoxText.Text = "";
                TextBoxImageText.Text = "";
                LabelH2.Text = "<h2>Skapa nyhet</h2>";
                LabelImage.Text = "Ladda upp bild";
                ImageExist.Visible = false;
                ButtonCreate.Visible = true;
                ButtonUpdate.Visible = false;
                PanelSuccess.Visible = true;
                LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Sparat!";
                LabelSuccess.Visible = true;
                PanelResponse.Visible = false;
                PanelRemove.Visible = false;
                DropDownNews.Items.Clear();
                conn.Close();
                LoadDropDown();
                conn.Open();
            }
            conn.Close();
        }
        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            myCookie = Request.Cookies["LoginCookie"];
            PanelRemove.Visible = false;
            NewsArticle newsArticle = new NewsArticle();
            newsArticle.heading = TextBoxHeading.Text;
            newsArticle.small_heading = TextBoxSmallHeading.Text;
            newsArticle.text = TextBoxText.Text;

            if (!string.IsNullOrEmpty(myCookie["_name"] as string))
            {
                newsArticle.written_by = myCookie["_name"].ToString();
            }
            else
            {
                newsArticle.written_by = "Admin";
            }

            newsArticle.image_text = TextBoxImageText.Text;

            string sql = "INSERT INTO newsarticle (heading, small_heading, text, date, written_by, image, image_text) VALUES (@heading, @small_heading, @text, Now(), @written_by, @image, @image_text) RETURNING id";

            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@heading", newsArticle.heading);
            cmd.Parameters.AddWithValue("@small_heading", newsArticle.small_heading);
            cmd.Parameters.AddWithValue("@text", newsArticle.text);
            cmd.Parameters.AddWithValue("@written_by", newsArticle.written_by);
            cmd.Parameters.AddWithValue("@image", "http://smock.foreuphosting5.com/wp-content/uploads/2015/04/tight.jpg");
            cmd.Parameters.AddWithValue("@image_text", newsArticle.image_text);

            int id = (int)cmd.ExecuteScalar();

            if (id != 0)
            {
                PanelResponse.Visible = false;
                PanelSuccess.Visible = true;
                LabelSuccess.Text = "<span class='spacer-glyph glyphicon glyphicon-ok'></span> Nyhet skapad!";
                TextBoxHeading.Text = "";
                TextBoxSmallHeading.Text = "";
                TextBoxText.Text = "";
                TextBoxImageText.Text = "";
                DropDownNews.Items.Clear();
                conn.Close();
                LoadDropDown();
                conn.Open();
            }

            conn.Close();
        }
    }
}