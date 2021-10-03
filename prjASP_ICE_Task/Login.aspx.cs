using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace prjASP_ICE_Task
{
    public partial class Login : System.Web.UI.Page
    {
        String connectString = @"Server = LAPTOP-48LJIM25\SQLEXPRESS;" +
                                "Database = tblUserDetails;" +
                                "Integrated Security = True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           // try
            //{
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    con.Open();
                    String sQuery = "SELECT *" +
                                    "FROM tblUserDetails" +
                                    "WHERE UserName = '" + txtUsername.Text + "' " +
                                    "AND UserPassword = '" + txtPassword.Text + "' ;";

                    SqlCommand command = new SqlCommand(sQuery, con);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Response.Redirect("Default.aspx");                      
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Incorrect user details!" + 
                                        "\nPlease try again.";
                    }
                    reader.Close();
                    command.Dispose();
                }
            //}
           // catch (Exception)
           // {
                
           // }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length <= 0 || txtPassword.Text.Length <= 0)
            {
                lblError.Visible = true;
                lblError.Text = "There can't be empty fields!" +
                                "\nPlease fill in the field with your details.";
            }
            else
            {
               // try
                //{
                    using (SqlConnection connection = new SqlConnection(connectString))
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO tblUserDetails " +
                                                            "VALUES(@UserName, @UserPassword) ;", connection);
                        command.Parameters.AddWithValue("@UserName", txtUsername.Text);
                        command.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = command;

                        int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());
                        lblError.Visible = true;
                        lblError.Text = "New user successfully added";
                        adapter.Dispose();
                    }
                //}
               //catch (Exception)
                //{

               // }
                txtPassword.Text = "";
                txtUsername.Text = "";
            }
        }
    }
}