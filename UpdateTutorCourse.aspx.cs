using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class UpdateTutorCourse : System.Web.UI.Page
{
    UserRoles ur;
    Courses cr;
    users u;

    protected void Page_Load(object sender, EventArgs e)
    {
        ur = new UserRoles();
        cr = new Courses();
        u = new users();

        if (!IsPostBack)
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                Response.Redirect("UserLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Session["roleid"].ToString() != ur.Roleid("Tutor"))
                {
                    Response.Redirect("UserAccount.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    string cname = cr.getCourseName(Session["courseid"].ToString());
                    if (cname == "Database Error")
                        lblError.Text = cname;
                    else
                        txtCourse.Text = cname;
                }
            }
        }
    }

    protected void btnUpdateCourse_Click(object sender, EventArgs e)
    {
        bool flag = true;
        string newCourse = lstCourses.SelectedValue;
        if (lstCourses.SelectedValue == "")
        {
            flag = false;
            lblError.Text = "No course selected";
        }
        else
        {
            Session["courseid"] = newCourse;
           
            string cname = cr.getCourseName(newCourse);
            if (cname == "Database Error")
                lblError.Text = cname;
            else
                txtCourse.Text = cname;

            flag = u.updateCourse(newCourse,Session["userid"].ToString());
        }
        if (flag)
        {
            Response.Redirect("UserAccount.aspx?UpdateSuccess=Course");
            Context.ApplicationInstance.CompleteRequest();
        }
        else
            lblError.Text = "Database Error";
    }

}
