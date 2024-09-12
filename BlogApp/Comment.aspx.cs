using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace BlogApp
{
    public partial class Comment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if (Session["Id"] != null)
                {
                    LoadPostDetails();
                    LoadComments();
                }
                //else
                //{
                //    Response.Redirect("~/Default.aspx");
                //}
            }
        }

        private void LoadPostDetails()
        {
            string postId = Session["Id"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Title, Content, CreatedAt FROM Posts WHERE Id = @PostID";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostID", postId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PostTitleLabel.Text = reader["Title"].ToString();
                            PostContentLabel.Text = reader["Content"].ToString();
                            PostDateLabel.Text = Convert.ToDateTime(reader["CreatedAt"]).ToString("MM/dd/yyyy");
                        }
                    }
                }
            }
        }

        private void LoadComments()
        {
            string postId = Session["Id"].ToString();
            string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Modified query to join Comments and Users table to get the Username
                string query = @"
                SELECT u.Username, c.Comment, c.CreatedAt
                FROM Comments c
                INNER JOIN Users u ON c.UserId = u.Id
                WHERE c.PostID = @PostID
                ORDER BY c.CreatedAt DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PostID", postId);
                    con.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        CommentsRepeater.DataSource = dt;
                        CommentsRepeater.DataBind();
                    }
                }
            }
        }


        protected void SubmitCommentButton_Click(object sender, EventArgs e)
        {
            string commentText = CommentTextBox.Text.Trim();
            string username = Session["Username"].ToString();  
            string postId = Session["Id"].ToString();

            if (!string.IsNullOrEmpty(commentText))
            {
                string connectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;
                int userId = 0;

                // Step 1: Retrieve UserId based on the username from the session
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string getUserIdQuery = "SELECT Id FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(getUserIdQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        con.Open();
                        userId = (int)cmd.ExecuteScalar();  // Retrieve the UserId from the Users table
                    }
                }

                // Step 2: Insert comment with UserId instead of Username
                if (userId > 0)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string insertCommentQuery = @"
                    INSERT INTO Comments (PostID, UserId, Comment, CreatedAt) 
                    VALUES (@PostID, @UserId, @CommentText, @CreatedAt)";

                        using (SqlCommand cmd = new SqlCommand(insertCommentQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@PostID", postId);
                            cmd.Parameters.AddWithValue("@UserId", userId);  // Use the retrieved UserId
                            cmd.Parameters.AddWithValue("@CommentText", commentText);
                            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Step 3: Reload comments after submission and clear the comment box
                    LoadComments();
                    CommentTextBox.Text = "";
                }
            }
        }
        protected void HomePageButton_Click(object sender, EventArgs e)
        {
            // Redirect to the home page (assuming the home page is "Default.aspx")
            Response.Redirect("~/Default.aspx");
        }

    }
}
