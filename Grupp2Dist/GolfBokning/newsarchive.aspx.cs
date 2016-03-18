using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace GolfBokning
{
    public partial class newsarchive : System.Web.UI.Page
    {
        NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.dbConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            GetMainNews(999);
        }

        private void likeButton_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            string id_button = btn.ID;
            id_button = id_button.Replace("likeButton_", "");

            string findThis = btn.Attributes["data"];

            string sql = "UPDATE newsarticle SET likes = likes + 1 WHERE id = " + id_button + " RETURNING likes";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.Parameters.AddWithValue("@id_newsarticle", id_button);
            int likes = (int)cmd.ExecuteScalar();

            if (likes != -1)
            {
                Response.Redirect("startside.aspx");
            }

            conn.Close();
        }

        private void GetMainNews(int limit)
        {
            int count = 0;
            string sql = "SELECT * FROM newsarticle ORDER BY date DESC LIMIT " + limit;

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            NpgsqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Panel childPanel = new Panel();
                childPanel.ID = "newsArticle" + count++.ToString();
                childPanel.CssClass = "newsArticle";

                Literal literalHeading = new Literal();
                literalHeading.Text = "<h1>" + dr["heading"].ToString() + "</h1>";

                Literal literalImage = new Literal();
                literalImage.Text = "<img src=" + dr["image"].ToString() + " class='bodyImage'>";

                Literal literalImageText = new Literal();
                literalImageText.Text = "<h3 class='imageText'>" + dr["image_text"].ToString() + "</h3>";

                Literal literalSmallHeading = new Literal();
                literalSmallHeading.Text = "<h2>" + dr["heading"].ToString() + "</h2>";

                Literal literalText = new Literal();
                literalText.Text = "<p class='bodyText'>" + dr["text"].ToString() + "</p>";

                string date = Convert.ToDateTime(dr["date"]).ToShortDateString().ToString();
                Literal literalWrittenBy = new Literal();
                literalWrittenBy.Text = "<p class='writtenBy'>Skrivet av: " + dr["written_by"].ToString() + " | " + date + "</p>";

                LinkButton likeButton = new LinkButton();
                likeButton.Text = "<span class='glyphicon glyphicon-thumbs-up thumb'></span>";
                likeButton.CssClass = "likeButton";
                likeButton.ID = "likeButton_" + dr["id"].ToString();
                likeButton.Click += new EventHandler(likeButton_Click);
                likeButton.Attributes["data"] = "ContentPlaceHolder1_labelLike_" + dr["id"].ToString();
                likeButton.Attributes["runat"] = "server";

                Label labelLike = new Label();
                labelLike.ID = "labelLike_" + dr["id"].ToString();
                labelLike.CssClass = "numberLikes";
                labelLike.Text = dr["likes"].ToString();
                labelLike.Attributes["runat"] = "server";

                Panel likePanel = new Panel();
                likePanel.CssClass = "likeDiv";

                likePanel.Controls.Add(labelLike);
                likePanel.Controls.Add(likeButton);

                childPanel.Controls.Add(literalHeading);
                childPanel.Controls.Add(literalImage);
                childPanel.Controls.Add(literalImageText);
                childPanel.Controls.Add(literalSmallHeading);
                childPanel.Controls.Add(literalText);
                childPanel.Controls.Add(literalWrittenBy);
                childPanel.Controls.Add(likePanel);

                PanelNewsArchive.Controls.Add(childPanel);

            }

            conn.Close();
        }
    }
}