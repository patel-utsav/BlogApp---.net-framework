using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlogApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();

            if (ValidateUser(username, password))
            {
                Session["Username"] = username;
                Response.Redirect("Default.aspx");
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.Visible = true;
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    try
                    {
                        con.Open();
                        int result = (int)cmd.ExecuteScalar();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        // Log the error (ex.Message)
                        return false;
                    }
                }
            }

        }
    }
}


