using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class StudentDetails : System.Web.UI.Page
{
    users u;
    UserRoles ur;
    Courses cr;
       
 
    protected void Page_Load(object sender, EventArgs e)
    {
        u = new users();
        ur = new UserRoles();
        cr = new Courses();
        
        if (!IsPostBack)
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                Response.Redirect("UserLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                if (Session["roleid"].ToString() != ur.Roleid("Student"))
                {
                    Response.Redirect("UserAccount.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    u.uid = Session["userid"].ToString();
                    if (Session["courseid"].ToString() != "0")
                    {
                        string cname = cr.getCourseName(Session["courseid"].ToString());
                        if (cname == "Database Error")
                            lblError.Text = cname;
                        else
                            txtCourse.Text = cname;
                        populateModules();
                    }
                    else
                        lblError.Text = "Student has been removed from the course";
                }
            }
        }
        
    }

    protected void btnShowEmail_Click(object sender, EventArgs e)
    {
        string newCourse = lstTutors.SelectedValue;
        if (lstTutors.SelectedValue == "")
        {
            lblEmail.Text = "No tutor selected";
        }
        else
        {
            u.uid = newCourse;
            lblEmail.Text = u.getEmail();
        }
            
    }

    private void populateModules()
    {
        CourseModules cm = new CourseModules();
        Modules m = new Modules();

        DataTable dt1 = cm.getModules(Session["courseid"].ToString());
        if (dt1 == null)
            lblError.Text = "Database Error";
        else
        {
            DataTable dt = m.getModuleInfo(dt1);
            if (dt == null)
                lblError.Text = "Database Error";
            else
            {
                rptModules.DataSource = dt;
                rptModules.DataBind();
            }
        }
    }
}
