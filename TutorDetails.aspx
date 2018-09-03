<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TutorDetails.aspx.cs" Inherits="TutorDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:Button ID="btnUserAccount" runat="server" PostBackUrl="~/UserAccount.aspx" 
        style="z-index: 1; left: 104px; top: 15px; position: absolute" 
        Text="User Account" />
    
    <div> 
        <asp:Label ID="lblTutorDetails" runat="server" Font-Bold="True" 
            Font-Size="Larger" 
            style="z-index: 1; left: 100px; top: 48px; position: absolute" 
            Text="Tutor Details" Font-Underline="True"></asp:Label>
    </div>
    <asp:Label ID="lblCourse" runat="server" 
        style="z-index: 1; left: 102px; top: 90px; position: absolute" Text="Course:"></asp:Label>
    <asp:TextBox ID="txtCourse" runat="server" ReadOnly="True" 
        style="z-index: 1; left: 165px; top: 87px; position: absolute"></asp:TextBox>
        
    <asp:Label ID="lblError" runat="server" ForeColor="Red" 
        style="z-index: 1; left: 321px; top: 89px; position: absolute"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <asp:Label ID="lblStudents" runat="server" 
        style="z-index: 1; left: 102px; top: 125px; position: absolute" 
        Text="Students(s) on your course:"></asp:Label>
    <asp:ListBox ID="lstStudents" runat="server"  
        style="z-index: 1; left: 102px; top: 159px; position: absolute; height: 72px; width: 180px" DataSourceID="SqlDataSource1" DataTextField="RealName" DataValueField="UserID">
    </asp:ListBox>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\university.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [UserID], [RealName] FROM [Users] WHERE (([CourseID] = @CourseID) AND ([RoleID] = @RoleID))">
            <SelectParameters>
                <asp:SessionParameter Name="CourseID" SessionField="courseid" Type="Int32" />
                <asp:Parameter DefaultValue="1" Name="RoleID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    <asp:Button ID="btnRemoveStudent" runat="server" 
        style="z-index: 1; left: 102px; top: 250px; position: absolute" 
        Text="Remove Student" onclick="btnRemoveStudent_Click" />
        
    <asp:Label ID="lblSuccess" runat="server" ForeColor="#009900" 
        style="z-index: 1; left: 104px; top: 293px; position: absolute"></asp:Label>
               </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
