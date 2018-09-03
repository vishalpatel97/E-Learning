using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseModules
/// </summary>
public class CourseModules
{
    DatabaseConnection c;
    public CourseModules()
    {
        c = new DatabaseConnection();
    }

    public DataTable getModules(string cid)
    {
        c.addParameter("@cid", cid);
        using (DataTable dt = c.executeReader("SELECT ModuleID FROM CourseModules WHERE CourseID = @cid"))
        {
            return dt;
        }
    }


}