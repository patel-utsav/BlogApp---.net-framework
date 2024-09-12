using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BlogApp
{
    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPosts();
                CheckLoginStatus();
            }
        }

        private void LoadPosts()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;

            try
            {
                using (con)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Posts", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    PostsRepeater.DataSource = dt;
                    PostsRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void CheckLoginStatus()
        {
            if (Session["Username"] != null)
            {
                AnonymousPanel.Visible = false;
                LoggedInPanel.Visible = true;
                UsernameLabel.Text = (string)Session["Username"];
            }
            else
            {
                AnonymousPanel.Visible = true;
                LoggedInPanel.Visible = false;
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("~/Default.aspx", true);
        }

        protected void PostsRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Comment")
            {
                if (Session["Username"] != null)
                {
                    string pId = e.CommandArgument.ToString();
                    Session["Id"] = e.CommandArgument.ToString();
                    Response.Write($"Redirecting to Comment.aspx with post ID: {pId}"); // For debugging
                    Response.Redirect("~/Comment.aspx");
                }
                else
                {
                    ErrorMessage.Text = "Please Login First To Make A Comment.";
                    ErrorMessage.Visible = true;
                }
            }
        }

    }
}