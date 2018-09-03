using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class TutorDetails : System.Web.UI.Page
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

    protected void btnRemoveStudent_Click(object sender, EventArgs e)
    {
        bool flag = true;
        if (lstStudents.SelectedValue == "")
        {
            flag = false;
            lblSuccess.ForeColor = System.Drawing.Color.Red;
            lblSuccess.Text = "No student selected";
        }
        else
            flag = u.updateCourse("0", lstStudents.SelectedValue);

        if (flag)
        {
            lblSuccess.ForeColor = System.Drawing.Color.Green;
            lblSuccess.Text = "Removal successful";
            //refresh sql datasource
            lstStudents.DataBind();
        }
        else
        {
            lblSuccess.ForeColor = System.Drawing.Color.Red;
            lblStudents.Text = "Database error";
        }
    }
}
