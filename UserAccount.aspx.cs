using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAccount : System.Web.UI.Page
{
    UserRoles ur = new UserRoles();
    string updateSuccess;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        ur = new UserRoles();
        if(HttpContext.Current.Session.IsNewSession)
        {
            Response.Redirect("UserLogin.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            updateSuccess = Request.QueryString["UpdateSuccess"];
            if (updateSuccess == "Password")
            {
                lblUpdateSuccess.Text = "Password Update Successful";
                lblTutorChangePassword.Visible = false;
            }
            else if(updateSuccess == "Course")
            {
                lblUpdateSuccess.Text = "Course Update Successful";
            }
            welcome();   
        }
    }

    private void welcome()
    {
        string roleID = Session["roleid"].ToString();

        lblWelcome.Text = "Welcome " + Session["username"].ToString() + "<br/>Role: " + ur.getRoleName(roleID);
        switch (roleID)
        {
            case "1":
                {
                    btnUpdateTutorCourse.Visible = false;
                    lblTutorChangePassword.Visible = false;
                    btnUserDetails.Text = "Student Details";
                    btnUserDetails.PostBackUrl = "StudentDetails.aspx";
                }
                break;
            case "2":
                {
                    btnUpdateTutorCourse.Visible = true;
                    lblTutorChangePassword.Visible = true;
                    btnUserDetails.Text = "Tutor Details";
                    btnUserDetails.PostBackUrl = "TutorDetails.aspx";
                }
                break;
        }
    }
}
