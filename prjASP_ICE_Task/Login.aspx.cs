using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace prjASP_ICE_Task
{
    public partial class Login : System.Web.UI.Page
    {
        String connectString = @"Server = LAPTOP-48LJIM25\SQLEXPRESS;" +
                                "Database = Login;" +
                                "Integrated Security = True;";

        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        public string encryptPassword(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            String sUsername = txtUsername.Text.ToString();
            String sPassword = txtPassword.Text.ToString();

            if (txtUsername.Text.Length <= 0 || txtPassword.Text.Length <= 0)
            {
                lblError.Visible = true;
                lblError.Text = "There can't be empty fields!" +
                                "\nPlease fill in the field with your details.";
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectString))
                    {
                        con.Open();
                        string encPass = encryptPassword(sPassword);
                        String sQuery = "SELECT UserName, UserPassword FROM tblUserDetails WHERE (UserName =  '" + sUsername + "') AND (UserPassword = '" + encPass + "');";
                        SqlCommand command = new SqlCommand(sQuery, con);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            sUsername = "";
                            sPassword = "";
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
                }
                catch (Exception)
                {

                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            String sUsername = txtUsername.Text.ToString();
            String sPassword = txtPassword.Text.ToString();
            String encPass = encryptPassword(sPassword);

            if (txtUsername.Text.Length <= 0 || txtPassword.Text.Length <= 0)
            {
                lblError.Visible = true;
                lblError.Text = "There can't be empty fields!" +
                                "\nPlease fill in the field with your details.";
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectString))
                    {
                        SqlCommand command = new SqlCommand("INSERT INTO tblUserDetails " +
                                                            "VALUES(@UserName, @UserPassword) ;", connection);
                        command.Parameters.AddWithValue("@UserName", sUsername);
                        command.Parameters.AddWithValue("@UserPassword", encPass);
                        String passwords = encryptPassword(sPassword);

                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.InsertCommand = command;

                        int id = Convert.ToInt32(adapter.InsertCommand.ExecuteScalar());
                        lblError.Visible = true;
                        lblError.ForeColor = System.Drawing.Color.Black;
                        lblError.Text = "New user successfully added";
                        adapter.Dispose();
                    }
                }
               catch (Exception)
                {

                }
                txtUsername.Text = "";
                txtPassword.Text = "";
            }
        }
    }
}