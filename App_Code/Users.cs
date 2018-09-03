using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for users
/// </summary>
public class users
{
    DatabaseConnection c;
    public int flag=0;
    public string uid { get; set; }
    public string uname { get; set; }
    public string upass { get; set; }
    public string rname { get; set; }
    public string email { get; set; }
    public string roleid { get; set; }
    public string courseid { get; set; }
    public Guid psalt { get; set; }
    public string e { get; set; }

    public users()
    {
        c = new DatabaseConnection();
    }

    public int userValidation()
    {
        c.addParameter("@uname", uname);
        using (DataTable dt = c.executeReader("SELECT UserID,UserName,UserPassword,RoleID,CourseID,PasswordSalt from Users where UserName= @uname"))
        {
            if (dt == null)
                return -1;
            else
            {
                if (dt.Rows.Count == 0 || GetSHA1HashData(upass + dt.Rows[0].ItemArray[5].ToString()) != dt.Rows[0].ItemArray[2].ToString())
                    return 0;
                else
                {
                    uid = dt.Rows[0].ItemArray[0].ToString();
                    roleid = dt.Rows[0].ItemArray[3].ToString();
                    courseid = dt.Rows[0].ItemArray[4].ToString();
                    return 1;
                }
            }
        }
    }

    public string getPassword()
    {
        c.addParameter("@userid", uid);
        using (DataTable dt = c.executeReader("SELECT UserPassword,PasswordSalt from Users where UserID = @userid"))
        {
            if (dt == null)
            {
                flag = -1;
                return "";
            }
            else
            {
                string oldP = dt.Rows[0].ItemArray[0].ToString();
                string newP = GetSHA1HashData(upass + dt.Rows[0].ItemArray[1].ToString());
                if (oldP != newP)
                    return "";
                else
                    return upass;
            }
        }

    }

    public int updatePassword()
    {
        Guid ugid = System.Guid.NewGuid();
        string hashP = GetSHA1HashData(upass + ugid.ToString());
        c.addParameter("@newP", hashP);
        c.addParameter("@pSalt", ugid.ToString());
        c.addParameter("@userid", uid);
        int count = c.executeNonQuery("UPDATE Users SET UserPassword = @newP, PasswordSalt=@pSalt WHERE UserID = @userid");
        return count;
    }

    public string getEmail()
    {
        c.addParameter("@uid", uid);
        using (DataTable dt = c.executeReader("SELECT EmailAddress from Users where UserID = @uid"))
        {
            return dt.Rows[0].ItemArray[0].ToString();
        }
    }

    public int CheckUserName()
    {
        int count;
        c.addParameter("@uname", uname);
        count = c.executeScalar("SELECT COUNT(*) from Users where username=@uname");
        return count;
    }

    public int AddStudent()
    {
        int count=0;
        Guid ugid = System.Guid.NewGuid();
        string hashP = GetSHA1HashData(upass + ugid.ToString());

        c.addParameter("@uname", uname);
        c.addParameter("@pass", hashP);
        c.addParameter("@realname", rname);
        c.addParameter("@email", email);
        c.addParameter("@roleid", roleid);
        c.addParameter("@courseid", courseid);
        c.addParameter("@salt", ugid.ToString());
        count = c.executeNonQuery("Insert into Users(UserName, UserPassword, RealName, EmailAddress, RoleID, CourseID, PasswordSalt) values(@uname, @pass, @realname, @email, @roleid, @courseid,@salt)");
        return count;
    }

    public bool updateCourse(string cid, string uid)
    {
        c.addParameter("@cid", cid);
        c.addParameter("@uid", uid);
        int query = c.executeNonQuery("UPDATE Users SET CourseID = @cid WHERE UserID = @uid");
        if (query == -1)             //error in connection
            return false;
        else
            return true;
    }

    public string GetSHA1HashData(string text)
    {
        Encoding enc = Encoding.Default;
        byte[] buffer = enc.GetBytes(text);
        SHA1CryptoServiceProvider cryptoTransformSha1 =
        new SHA1CryptoServiceProvider();
        string hash = BitConverter.ToString(
            cryptoTransformSha1.ComputeHash(buffer)).Replace("-", "");

        return hash;
    }
}