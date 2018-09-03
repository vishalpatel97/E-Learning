using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Modules
/// </summary>
public class Modules
{
    DatabaseConnection c;

    public Modules()
    {
        c = new DatabaseConnection();
    }

    public DataTable getModuleInfo(DataTable dt1)
    {
        DataTable dt2 = null;
        DataTable data = new DataTable();
        data.Columns.Add("ModuleCode");
        data.Columns.Add("ModuleName");

        for (int i = 0; i < dt1.Rows.Count; ++i)
        {
            c.addParameter("@mid", dt1.Rows[i].ItemArray[0].ToString());
            using (dt2 = c.fillDataTable("SELECT ModuleCode, ModuleName FROM Modules WHERE ModuleID = @mid"))
            {
                if (dt2 == null)
                    return null;
                else
                {
                    DataRow r = data.NewRow();
                    r["ModuleCode"] = dt2.Rows[0].ItemArray[0].ToString();
                    r["ModuleName"] = dt2.Rows[0].ItemArray[1].ToString();
                    data.Rows.Add(r);
                }
            }
        }
        return data;
    }
}