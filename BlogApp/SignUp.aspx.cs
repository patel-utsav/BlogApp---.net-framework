using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BlogApp
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignUpButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string username = UsernameTextBox.Text.Trim();
                string email = EmailTextBox.Text.Trim();
                string password = PasswordTextBox.Text;

                if (CreateUser(username, email, password))
                {
                    Session["Username"] = username;
                    // Change Redirect to prevent ThreadAbortException
                    Response.Redirect("~/Default.aspx", false);
                    Context.ApplicationInstance.CompleteRequest(); // Complete the request after redirection
                }
                else
                {
                    ErrorMessage.Text = "An error occurred during registration. Please try again.";
                    ErrorMessage.Visible = true;
                }
            }
        }

        private bool CreateUser(string username, string email, string password)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = WebConfigurationManager.ConnectionStrings["BlogDBConnection"].ConnectionString;


            string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";

            
            using(con)
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                return rowsAffected > 0;
            }
            
         
            
        }
    }
}