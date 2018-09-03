using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class StudentRegistration : System.Web.UI.Page
{
    
    users u;

    bool IsValidEmail(string email)
    {
        bool s = email.Contains("@dmu1.ac.uk");
        return s;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        u = new users();

        if (IsPostBack)
        {
            lblError.Text = "";
            return;

        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
       int flag = 0;
        if (txtUsername.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "" && txtEmailAddress.Text != "")
        {
            if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 12)
            {
                flag++;
                lblError.Text = "Username should have 5-12 characters";
            }
            else if (txtPassword.Text.Length < 6)
            {
                flag++;
                lblError.Text = "Password should have at least 6 characters";
            }
            else if (!String.Equals(txtPassword.Text, txtConfirmPassword.Text))
            {
                flag++;
                lblError.Text = "Passwords must match";
            }
            else if (!IsValidEmail(txtEmailAddress.Text))
            {
                flag++;
                lblError.Text = "Enter a valid email address";
            }

            if (flag == 0)
            {
                Int32 count = 0;
                u.uname = txtUsername.Text;
                count = u.CheckUserName();
                if(count == -1)
                    lblError.Text = "Database error";
                else if (count != 0)
                {
                    lblError.Text = "Username already exists";
                }
                else
                {
                    
                    u.uname = txtUsername.Text;
                    u.upass = txtConfirmPassword.Text;
                    u.rname = txtRealName.Text;
                    u.email = txtEmailAddress.Text;
                    u.roleid = "1";
                    u.courseid = ddlCourses.SelectedValue;

                    count = u.AddStudent();

                    if (count==-1)
                    {
                        lblError.Text = "Database error";
                    }
                    else
                    {
                        Response.Redirect("UserLogin.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                
            }
        }
        else
            lblError.Text = "Field Missing";
    }

}
