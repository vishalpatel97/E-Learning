using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

public partial class UserLogin : System.Web.UI.Page
{
    users u;
    
    protected void Page_Load(object sender, EventArgs e)
    {
         u = new users();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        int count = 0, flag = 0;

        //input validations
        if (txtUsername.Text.Length < 5 || txtUsername.Text.Length > 12 || txtPassword.Text.Length < 6)
        {
            flag++;
            lblError.Text = "Username should have 5-12 characters and <br/>Password must be of at least 6 characters";
        }

        //after input validation
        if (flag == 0)
        {
            u.uname = txtUsername.Text;
            u.upass = txtPassword.Text;
            count = u.userValidation();
            if(count==-1)
                lblError.Text = "Database Error";
            else if(count == 0)
                lblError.Text = "Invalid username/password";
            else
            {
                Session["userid"] = u.uid;
                Session["username"] = u.uname;
                Session["roleid"] = u.roleid;
                Session["courseid"] = u.courseid;

                Response.Redirect("UserAccount.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
