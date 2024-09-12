using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlogApp
{
    public partial class ManagePosts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetUserIdByUsername();

                LoadPosts();
            }
        }

        private void GetUserIdByUsername()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string username = Session["Username"].ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Users WHERE Username = @Username", con);
                cmd.Parameters.AddWithValue("@Username", username);

                object result = cmd.ExecuteScalar(); // Get the UserId
                if (result != null)
                {
                    Session["UserId"] = Convert.ToInt32(result); // Store UserId in Session
                }
                else
                {
                    // Handle case where the user does not exist
                    Session["UserId"] = null;
                    Response.Write("User not found.");
                }
            }
        }

        private void LoadPosts()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id, Title, Content FROM Posts WHERE UserId = @UserId", con);
                da.SelectCommand.Parameters.AddWithValue("@UserId", Session["UserId"]);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPosts.DataSource = dt;
                gvPosts.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd;
                if (string.IsNullOrEmpty(hfPostID.Value)) // Create new post
                {
                    cmd = new SqlCommand("INSERT INTO Posts (Title, Content, UserId) VALUES (@Title, @Content, @UserId)", con);
                }
                else // Update existing post
                {
                    cmd = new SqlCommand("UPDATE Posts SET Title = @Title, Content = @Content WHERE Id = @Id", con);
                    cmd.Parameters.AddWithValue("@Id", hfPostID.Value);
                }

                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@Content", txtContent.Text.Trim());
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);

                cmd.ExecuteNonQuery();
                LoadPosts(); // Reload posts after saving
            }

            hfPostID.Value = ""; // Reset hidden field
            txtTitle.Text = "";
            txtContent.Text = "";
        }

        protected void gvPosts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvPosts.Rows[e.NewEditIndex];
            hfPostID.Value = row.Cells[0].Text; // Assuming Id is in the first cell
            txtTitle.Text = row.Cells[1].Text;
            txtContent.Text = row.Cells[2].Text;
        }


        protected void gvPosts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
            int postId = Convert.ToInt32(gvPosts.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Posts WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", postId);
                cmd.ExecuteNonQuery();
            }

            LoadPosts();
        }
        protected void HomePageButton_Click(object sender, EventArgs e)
        {
            // Redirect to the home page (assuming the home page is "Default.aspx")
            Response.Redirect("~/Default.aspx");
        }

    }
}
