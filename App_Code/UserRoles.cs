using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRoles
/// </summary>
public class UserRoles
{
    DatabaseConnection c;

    public UserRoles()
    {
        c = new DatabaseConnection();
    }

    public string Roleid(string roleName)
    {
        c.addParameter("@rname", roleName);
        using (DataTable dt = c.executeReader("SELECT RoleID from UserRoles where RoleName= @rname"))
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
    }

    public string getRoleName(string rid)
    {
        c.addParameter("@rid", rid);
        using (DataTable dt = c.executeReader("SELECT RoleName from UserRoles where RoleID= @rid"))
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
    }

}