using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

public partial class UpdatePassword : System.Web.UI.Page
{
    
    users u;

    protected void Page_Load(object sender, EventArgs e)
    {
        u = new users();
        if(!IsPostBack)
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                Response.Redirect("UserLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        if (txtCurrentPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmPassword.Text != "")
        {
            if (txtCurrentPassword.Text.Length < 6)
                lblError.Text = "Current Password is less than 6 characters";
            else if (txtNewPassword.Text.Length < 6)
                lblError.Text = "New Password is less than 6 characters";
            else if (!String.Equals(txtNewPassword.Text, txtConfirmPassword.Text))
                lblError.Text = "Passwords must match";
            else
            {
                u.uid = Session["userid"].ToString();
                u.upass = txtCurrentPassword.Text;
                string p = u.getPassword();
                if(p=="" && u.flag==-1)
                    lblError.Text = "Database Error";
                else if (p == "" && u.flag == 0)
                    lblError.Text = "Incorrect password";
                else
                {
                    u.upass = txtConfirmPassword.Text;
                    int count = u.updatePassword();
                    if (count == -1)
                    {
                        lblError.Text = "Database error";
                    }
                    else
                    {
                        Response.Redirect("UserAccount.aspx?UpdateSuccess=Password");
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                            
            }
        }
        else
            lblError.Text = "Missing Fields";
    }

}
