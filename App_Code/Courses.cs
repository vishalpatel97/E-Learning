using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Courses
/// </summary>
public class Courses
{
    DatabaseConnection c;

    public Courses()
    {
        c = new DatabaseConnection();
    }

    public string getCourseName(string cid)
    {
        c.addParameter("@cid", cid);
        using (DataTable dt = c.executeReader("SELECT CourseName from Courses where CourseID=@cid"))
        {
            if (dt == null || dt.Rows.Count != 1)             //error in connection
                return "Database Error";
            else
                return dt.Rows[0].ItemArray[0].ToString();
        }
    }
}